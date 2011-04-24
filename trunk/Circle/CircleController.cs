﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle
{
  public class CircleController
  {
    private bool run;
    private Exception exception;
    private IEnumerable<VotingDescriptor2> votingList;
    private VotingMaterial votingMaterial;
    private Signed<VoteReceipt> voteReceipt;
    private Dictionary<Certificate, string> certificates;
    private VotingClient.Operation currentOperation;
    private Signed<SignatureResponse> signatureResponse;
    private SignatureResponseStatus responseStatus;
    private VotingDescriptor votingDescriptor;
    private bool acceptShares;
    private Signed<BadShareProof> signedBadShareProof;
    private VotingResult votingResult;
    private IDictionary<Guid, VoteReceiptStatus> voteReceiptsStatus;
    private Dictionary<Guid, VotingContainer> votings;
    private bool userCanceled;

    public CircleStatus Status { get; private set; }

    public string SubText
    {
      get
      {
        var operation = Status.VotingClient.CurrentOperation;

        if (operation != null)
        {
          this.currentOperation = operation;
        }

        if (this.currentOperation != null)
        {
          return this.currentOperation.SubText;
        }
        else
        {
          return "Working";
        }
      }
    }
    
    public string Text
    {
      get
      {
        var operation = Status.VotingClient.CurrentOperation;

        if (operation != null)
        {
          this.currentOperation = operation;
        }

        if (this.currentOperation != null)
        {
          return this.currentOperation.Text;
        }
        else
        {
          return "Working";
        }
      }
    }

    public double Progress
    {
      get
      {
        var operation = Status.VotingClient.CurrentOperation;

        if (operation != null)
        {
          this.currentOperation = operation;
        }

        if (this.exception != null)
        {
          return 0d;
        }
        else
        {
          if (this.currentOperation != null)
          {
            return this.currentOperation.Progress;
          }
          else
          {
            return 0d;
          }
        }
      }
    }

    public double SubProgress
    {
      get
      {
        var operation = Status.VotingClient.CurrentOperation;

        if (operation != null)
        {
          this.currentOperation = operation;
        }

        if (this.exception != null)
        {
          return 0d;
        }
        else
        {
          if (this.currentOperation != null)
          {
            return this.currentOperation.SubProgress;
          }
          else
          {
            return 0d;
          }
        }
      }
    }
    
    public CircleController()
    {
      this.votings = new Dictionary<Guid, VotingContainer>();
      Status = new CircleStatus();
    }

    private void Begin()
    {
      this.run = true;
      this.exception = null;
    }

    private bool WaitForCompletion()
    {
      while (this.run)
      {
        Thread.Sleep(10);
        Application.DoEvents();
      }

      if (this.exception == null)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public Tuple<SignatureResponseStatus, string> GetSignatureResponse(Certificate certificate)
    {
      Begin();
      Status.VotingClient.GetSignatureResponse(certificate.Id, GetSignatureResponseComplete);

      if (WaitForCompletion())
      {
        switch (this.responseStatus)
        {
          case SignatureResponseStatus.Accepted:
          case SignatureResponseStatus.Declined:
            var response = this.signatureResponse.Value;

            if (response.SubjectId.Equals(certificate.Id) &&
              this.signatureResponse.Verify(Status.CertificateStorage))
            {
              if (this.responseStatus == SignatureResponseStatus.Accepted)
              {
                certificate.AddSignature(response.Signature);
                certificate.Save(this.certificates[certificate]);
              }

              return new Tuple<SignatureResponseStatus, string>(this.responseStatus, response.Reason);
            }
            else
            {
              return new Tuple<SignatureResponseStatus, string>(SignatureResponseStatus.Unknown, string.Empty);
            }
          default:
            return new Tuple<SignatureResponseStatus, string>(this.responseStatus, string.Empty);
        }
      }
      else
      {
        return new Tuple<SignatureResponseStatus, string>(SignatureResponseStatus.Unknown, string.Empty);
      }
    }

    private void GetSignatureResponseComplete(SignatureResponseStatus status, Signed<SignatureResponse> response, Exception exception)
    {
      this.responseStatus = status;
      this.signatureResponse = response;
      this.exception = exception;
      this.run = false;
    }

    public bool TrySetSignatureRequest(Secure<SignatureRequest> secureSignatureRequest, Secure<SignatureRequestInfo> secureSignatureRequestInfo)
    {
      Begin();
      Status.VotingClient.SetSignatureRequest(secureSignatureRequest, secureSignatureRequestInfo, SetSignatureRequestComplete);

      if (WaitForCompletion())
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    private void SetSignatureRequestComplete(Exception exception)
    {
      this.run = false;
      this.exception = exception;
    }

    public void DeactiveCertificate(Certificate certificate)
    {
      File.Move(this.certificates[certificate], this.certificates[certificate] + Files.BakExtension);
      this.certificates.Remove(certificate);
    }

    public void AddAndSaveCertificate(Certificate certificate)
    {
      string certificateFileName = Path.Combine(Status.DataPath, certificate.Id.ToString() + Files.CertificateExtension);
      certificate.Save(certificateFileName);
      this.certificates.Add(certificate, certificateFileName);
    }

    public string GetFileName(Certificate certificate)
    {
      return this.certificates[certificate];
    }

    public AuthorityCertificate GetAuthorityCertificate(VotingDescriptor2 voting)
    {
      var certificates = GetAuthorityCertificates(voting.VoteFrom);

      return certificates.Where(certificate => voting.Authorities.Any(authority => authority.IsIdentic(certificate)) &&
                          !voting.AuthoritiesDone.Contains(certificate.Id))
                          .FirstOrDefault();
    }

    public IEnumerable<AuthorityCertificate> GetAuthorityCertificates()
    {
      return
        this.certificates.Keys
        .Where(certificate => certificate is AuthorityCertificate)
        .Select(certificate => certificate as AuthorityCertificate);
    }

    public IEnumerable<AuthorityCertificate> GetAuthorityCertificates(DateTime validAt)
    {
      return
        this.certificates.Keys
        .Where(certificate => certificate is AuthorityCertificate &&
                              certificate.Validate(Status.CertificateStorage, validAt) == CertificateValidationResult.Valid)
        .Select(certificate => certificate as AuthorityCertificate);
    }

    public IEnumerable<VoterCertificate> GetVoterCertificates(VotingDescriptor2 voting)
    {
      return this.certificates.Keys
        .Where(certificate => certificate is VoterCertificate &&
          ((VoterCertificate)certificate).GroupId == voting.GroupId)
          .Select(certificate => certificate as VoterCertificate);
    }

    public VoterCertificate GetVoterCertificate(VotingDescriptor2 voting)
    {
      return this.certificates.Keys
        .Where(certificate => certificate is VoterCertificate &&
          ((VoterCertificate)certificate).GroupId == voting.GroupId)
          .FirstOrDefault() as VoterCertificate;
    }

    public VoterCertificate GetVoterCertificateThatCanVote(VotingDescriptor2 voting)
    {
      var receipts = LoadVoteReceipts(voting.Id);

      return this.certificates.Keys
        .Where(certificate => certificate is VoterCertificate &&
          ((VoterCertificate)certificate).GroupId == voting.GroupId &&
          !receipts.Any(receipt => receipt.Value.VoterId.Equals(certificate.Id)))
          .FirstOrDefault() as VoterCertificate;
    }

    public void LoadCertificates()
    {
      this.certificates = new Dictionary<Certificate, string>();
      DirectoryInfo directory = new DirectoryInfo(Status.DataPath);

      foreach (var file in directory.GetFiles(Files.CertificatePattern))
      {
        try
        {
          this.certificates.Add(Serializable.Load<Certificate>(file.FullName), file.FullName);
        }
        catch
        { 
        }
      }

      foreach (var certificate in this.certificates.Keys)
      {
        switch (certificate.Validate(Status.CertificateStorage))
        {
          case CertificateValidationResult.NoSignature:
            var response = GetSignatureResponse(certificate);

            switch (response.First)
            {
              case SignatureResponseStatus.Accepted:
                string acceptedMessage = string.Format(
                  "Your signature request for certificate {0} of type {1} was accepted.",
                  certificate.Id.ToString(), certificate.TypeText);
                MessageForm.Show(acceptedMessage, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
              case SignatureResponseStatus.Declined:
                string declinedMessage = string.Format(
                  "Your signature request for certificate {0} of type {1} was declined for the following reason: {2}",
                  certificate.Id.ToString(), certificate.TypeText, response.Second);
                MessageForm.Show(declinedMessage, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            }

            break;
        }
      }
    }

    public void Prepare()
    {
      Status.CertificateStorage = new CertificateStorage();
      Status.VotingClient = new VotingClient(Status.CertificateStorage);

      if (!Status.CertificateStorage.TryLoadRoot())
      {
        throw new InvalidOperationException("Cannot find root certificate.");
      }

      IPEndPoint serverEndPoint = Status.ServerEndPoint;

      if (serverEndPoint == null)
      {
        throw new InvalidOperationException("Cannot resolve server address.");
      }

      Status.VotingClient.Connect(serverEndPoint);

      Begin();
      Status.VotingClient.GetCertificateStorage(Status.CertificateStorage, OnGetCertificateStorageComplete);

      if (WaitForCompletion())
      {
        Status.VotingClient.GetConfig(OnGetConfigComplete);

        if (!WaitForCompletion())
        {
          throw this.exception;
        }
      }
      else
      {
        throw this.exception;
      }
    }

    private void VoteComplete(Signed<VoteReceipt> voteReceipt, Exception exception)
    {
      this.voteReceipt = voteReceipt;
      this.exception = exception;
      this.run = false;
    }

    private void GetVotingMaterialComplete(VotingMaterial votingMaterial, Exception exception)
    {
      this.votingMaterial = votingMaterial;
      this.exception = exception;
      this.run = false;
    }

    public void Disconnect()
    {
      Status.VotingClient.Close();
    }

    public VotingDescriptor2 GetVoting(Guid votingId)
    {
      Begin();

      List<Guid> votingIds = new List<Guid>();
      votingIds.Add(votingId);
      Status.VotingClient.GetVotings(votingIds, OnGetVotingsComplete);

      if (WaitForCompletion())
      {
        return this.votingList.First();
      }
      else
      {
        throw this.exception;
      }
    }

    public IEnumerable<VotingDescriptor2> GetVotingList()
    {
      this.votings.Clear();

      Begin();
      Status.VotingClient.GetVotings(null, OnGetVotingsComplete);

      if (WaitForCompletion())
      {
        return this.votingList;
      }
      else
      {
        throw this.exception;
      }
    }

    private void OnGetVotingsComplete(IEnumerable<VotingContainer> votingList, Exception exception)
    {
      foreach (var voting in votingList)
      {
        var id = voting.Material.Parameters.Value.VotingId;

        if (this.votings.ContainsKey(id))
        {
          this.votings[id] = voting;
        }
        else
        {
          this.votings.Add(id, voting);
        }
      }

      this.votingList = votingList.Select(voting => new VotingDescriptor2(voting));
      this.exception = exception;
      this.run = false;
    }

    private void OnGetConfigComplete(IRemoteConfig remoteConfig, IEnumerable<Group> groups, Exception exception)
    {
      Status.RemoteConfig = remoteConfig;
      Status.Groups = groups;
      this.exception = exception;
    }

    private void OnGetCertificateStorageComplete(Certificate serverCertificate, Exception exception)
    {
      Status.ServerCertificate = serverCertificate;
      this.exception = exception;
      this.run = false;
    }

    public VotingDescriptor CreateShares(Certificate certificate, VotingDescriptor2 voting)
    {
      string fileName = string.Format("{0}@{1}.pi-auth", certificate.Id.ToString(), voting.Id.ToString());
      string filePath = Path.Combine(Status.DataPath, fileName);

      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(certificate, Resources.AuthorityCreateSharesUnlockAction))
      {
        Begin();
        Status.VotingClient.CreateSharePart(voting.Id, (AuthorityCertificate)certificate, filePath, CreateSharesCompleteCallBack);

        if (!WaitForCompletion())
        {
          MessageForm.Show(this.exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        certificate.Lock();

        return this.votingDescriptor;
      }
      else
      {
        return voting;
      }
    }

    private void CreateSharesCompleteCallBack(VotingDescriptor votingDescriptor, Exception exception)
    {
      this.exception = exception;
      this.votingDescriptor = votingDescriptor;
      this.run = false;
    }

    public VotingDescriptor CheckShares(Certificate certificate, VotingDescriptor2 voting)
    {
      string fileName = string.Format("{0}@{1}.pi-auth", certificate.Id.ToString(), voting.Id.ToString());
      string filePath = Path.Combine(Status.DataPath, fileName);

      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(certificate, Resources.AuthorityCreateSharesUnlockAction))
      {
        Begin();
        Status.VotingClient.CheckShares(voting.Id, (AuthorityCertificate)certificate, filePath, CheckSharesComplete);

        if (WaitForCompletion())
        {
          if (this.acceptShares)
          {
            MessageForm.Show("All shares where accepted.", Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
          else
          {
            MessageForm.Show("Some shares were not accepted.", Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
          }
        }
        else
        {
          MessageForm.Show(this.exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        certificate.Lock();

        return this.votingDescriptor;
      }
      else
      {
        return voting;
      }
    }

    private void CheckSharesComplete(VotingDescriptor votingDescriptor, bool accept, Signed<BadShareProof> signedBadShareProof, Exception exception)
    {
      this.exception = exception;
      this.votingDescriptor = votingDescriptor;
      this.acceptShares = accept;
      this.signedBadShareProof = signedBadShareProof;
      this.run = false;
    }

    public VotingDescriptor Decipher(Certificate certificate, VotingDescriptor2 voting)
    {
      string fileName = string.Format("{0}@{1}.pi-auth", certificate.Id.ToString(), voting.Id.ToString());
      string filePath = Path.Combine(Status.DataPath, fileName);

      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(certificate, Resources.AuthorityCreateSharesUnlockAction))
      {
        this.userCanceled = false;
        Begin();
        Status.VotingClient.CreateDeciphers(voting.Id, (AuthorityCertificate)certificate, filePath, AskForPartiallyDecipher, CreateDeciphersComplete);

        if (!WaitForCompletion() && !this.userCanceled)
        {
          MessageForm.Show(this.exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        certificate.Lock();

        return this.votingDescriptor;
      }
      else
      {
        return voting;
      }
    }

    private bool AskForPartiallyDecipher(int validEnvelopeCount)
    {
      this.userCanceled = MessageForm.Show(
        string.Format("Valid envelope count {0}. Continue?", validEnvelopeCount),
        Resources.MessageBoxTitle,
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question)
        != DialogResult.Yes;
      return !this.userCanceled;
    }

    private void CreateDeciphersComplete(VotingDescriptor votingDescriptor, Exception exception)
    {
      this.exception = exception;
      this.votingDescriptor = votingDescriptor;
      this.run = false;
    }

    public VotingResult Tally(VotingDescriptor2 voting, out IDictionary<Guid, VoteReceiptStatus> voteReceiptsStatus)
    {
      Begin();
      Status.VotingClient.ActivateVoter();
      Status.VotingClient.GetResult(voting.Id, LoadVoteReceipts(voting.Id), GetResultComplete);

      if (!WaitForCompletion())
      {
        MessageForm.Show(this.exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      voteReceiptsStatus = this.voteReceiptsStatus;
      return this.votingResult;
    }

    public IEnumerable<Signed<VoteReceipt>> LoadVoteReceipts(Guid votingId)
    {
      DirectoryInfo dataDirectory = new DirectoryInfo(Status.DataPath);

      foreach (FileInfo file in dataDirectory.GetFiles(Files.VoteReceiptPattern))
      {
        Signed<VoteReceipt> signedVoteReceipt = Serializable.Load<Signed<VoteReceipt>>(file.FullName);
        VoteReceipt voteReceipt = signedVoteReceipt.Value;

        if (voteReceipt.VotingId.Equals(votingId))
        {
          yield return signedVoteReceipt;
        }
      }
    }

    private void GetResultComplete(VotingResult result, IDictionary<Guid, VoteReceiptStatus> voteReceiptsStatus, Exception exception)
    {
      this.votingResult = result;
      this.voteReceiptsStatus = voteReceiptsStatus;
      this.exception = exception;
      this.run = false;
    }

    public void SetSignatureRequest(Secure<SignatureRequest> signatureRequest, Secure<SignatureRequestInfo> signatureRequestInfo)
    {
      Begin();
      Status.VotingClient.SetSignatureRequest(signatureRequest, signatureRequestInfo, SetSignatureRequestCompleted);

      if (WaitForCompletion())
      {
        MessageForm.Show("Your request is stored on the server.", Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      else
      {
        MessageForm.Show(this.exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SetSignatureRequestCompleted(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    public void Vote(VoterCertificate voterCertificate, VotingDescriptor2 voting, IEnumerable<IEnumerable<bool>> vota)
    {
      Begin();
      Status.VotingClient.ActivateVoter();
      Status.VotingClient.Vote(this.votings[voting.Id].Material, voterCertificate, vota, VoteComplete);

      if (WaitForCompletion())
      {
        this.voteReceipt.Save(
          Path.Combine(Status.DataPath,
          Files.VoteReceiptFileName(voterCertificate.Id, voting.Id)));
        MessageForm.Show("Your vote is cast.", Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      else
      {
        MessageForm.Show(this.exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
