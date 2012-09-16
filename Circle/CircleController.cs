/*
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
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

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
    private IEnumerable<AuthorityCertificate> authorityCertificates;
    private byte[] encryptedCode;
    private string statsData;

    public UserConfig Config { get; private set; }

    public CircleStatus Status { get; private set; }

    public void UpdateStatus()
    {
      var operation = Status.VotingClient.CurrentOperation;

      if (operation != null)
      {
        this.currentOperation = operation;
      }
    }

    public string SubText
    {
      get
      {
        if (this.currentOperation != null)
        {
          return this.currentOperation.SubText;
        }
        else
        {
          return string.Empty;
        }
      }
    }
    
    public string Text
    {
      get
      {
        if (this.currentOperation != null)
        {
          return this.currentOperation.Text;
        }
        else
        {
          if (Status.VotingClient.Connecting)
          {
            return Resources.ControllerStatusConnecting;
          }
          else
          {
            return Resources.ControllerStatusWorking;
          }
        }
      }
    }

    public double Progress
    {
      get
      {
        if (this.currentOperation != null)
        {
          return this.currentOperation.SingleProgress;
        }
        else
        {
          return 0d;
        }
      }
    }

    public bool HasProgress
    {
      get
      {
        if (this.currentOperation != null)
        {
          return this.currentOperation.HasSingleProgress;
        }
        else
        {
          return false;
        }
      }
    }
    
    public CircleController()
    {
      this.votings = new Dictionary<Guid, VotingContainer>();
      Status = new CircleStatus();
      Config = new UserConfig(Path.Combine(Status.DataPath, Files.CircleUserConfigFileName));
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
      var newFileName = this.certificates[certificate] + Files.BakExtension;

      if (File.Exists(newFileName))
      {
        File.Delete(newFileName);
      }

      File.Move(this.certificates[certificate], newFileName);

      this.certificates.Remove(certificate);
    }

    public IEnumerable<Certificate> Certificates
    {
      get
      {
        return this.certificates.Keys;
      }
    }

    public void SaveCertificate(Certificate certificate)
    {
      if (this.certificates.ContainsKey(certificate))
      {
        certificate.Save(this.certificates[certificate]);
      }
      else
      {
        AddAndSaveCertificate(certificate);
      }
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

    public IEnumerable<AuthorityCertificate> GetAuthorities()
    {
      Begin();
      Status.VotingClient.GetAuthorityCertificates(GetAuthorityCertificatesComplete);

      if (WaitForCompletion())
      {
        return authorityCertificates;
      }
      else
      {
        throw this.exception;
      }
    }

    private void GetAuthorityCertificatesComplete(IEnumerable<AuthorityCertificate> authorityCertificates, Exception exception)
    {
      this.authorityCertificates = authorityCertificates;
      this.exception = exception;
      this.run = false;
    }

    public AuthorityCertificate GetAuthorityCertificate(VotingDescriptor2 voting)
    {
      var certificates = GetAuthorityCertificates(voting.VoteFrom);

      return certificates.Where(certificate => voting.Authorities.Any(authority => authority.IsIdentic(certificate)) &&
                          !voting.AuthoritiesDone.Contains(certificate.Id))
                          .FirstOrDefault();
    }

    public AdminCertificate GetAdminCertificateEvenInvalid()
    {
      return
        this.certificates.Keys
        .Where(certificate => certificate is AdminCertificate)
        .Select(certificate => certificate as AdminCertificate)
        .FirstOrDefault();
    }

    public AdminCertificate GetAdminCertificate()
    {
      return
        this.certificates.Keys
        .Where(certificate => certificate is AdminCertificate &&
          certificate.Validate(Status.CertificateStorage) == CertificateValidationResult.Valid)
        .Select(certificate => certificate as AdminCertificate)
        .FirstOrDefault();
    }

    public Certificate GetNotaryCertificate()
    {
      return
        this.certificates.Keys
        .Where(certificate => (certificate is NotaryCertificate || certificate is AuthorityCertificate) &&
          certificate.Validate(Status.CertificateStorage) == CertificateValidationResult.Valid)
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

    public IEnumerable<VoterCertificate> GetVoterCertificates()
    {
      return this.certificates.Keys
        .Where(certificate => certificate is VoterCertificate)
          .Select(certificate => certificate as VoterCertificate);
    }

    public IEnumerable<VoterCertificate> GetVoterCertificates(int groupId)
    {
      return this.certificates.Keys
        .Where(certificate => certificate is VoterCertificate &&
          ((VoterCertificate)certificate).GroupId == groupId)
          .Select(certificate => certificate as VoterCertificate);
    }

    public IEnumerable<VoterCertificate> GetValidVoterCertificates()
    {
      return this.certificates.Keys
        .Where(certificate => certificate is VoterCertificate &&
                              certificate.Validate(Status.CertificateStorage) == CertificateValidationResult.Valid)
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
          certificate.Validate(Status.CertificateStorage) == CertificateValidationResult.Valid &&
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
          case CertificateValidationResult.Valid:
            
            DeleteContinueSignatureRequestFiles(certificate);
            break;
          case CertificateValidationResult.NoSignature:
            var response = GetSignatureResponse(certificate);

            switch (response.First)
            {
              case SignatureResponseStatus.Accepted:
                string acceptedMessage = string.Format(
                  Resources.ControllerLoadCertificatesAccepted,
                  certificate.Id.ToString(), certificate.TypeText);
                MessageForm.Show(acceptedMessage, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DeleteContinueSignatureRequestFiles(certificate);
                break;
              case SignatureResponseStatus.Declined:
                string declinedMessage = string.Format(
                  Resources.ControllerLoadCertificatesDeclined,
                  certificate.Id.ToString(), certificate.TypeText, response.Second);
                MessageForm.Show(declinedMessage, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            }

            break;
        }
      }
    }

    private void DeleteContinueSignatureRequestFiles(Certificate certificate)
    {
      string signatureRequestFileName = Path.Combine(Status.DataPath, certificate.Id.ToString() + Files.SignatureRequestDataExtension);
      string signatureRequestInfoFileName = Path.Combine(Status.DataPath, certificate.Id.ToString() + Files.SignatureRequestInfoExtension);

      if (File.Exists(signatureRequestFileName) &&
          File.Exists(signatureRequestInfoFileName))
      {
        File.Delete(signatureRequestFileName);
        File.Delete(signatureRequestInfoFileName);
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

      Begin();
      Status.VotingClient.Connect(serverEndPoint);

      Status.VotingClient.GetCertificateStorage(Status.CertificateStorage, GetCertificateStorageComplete);

      if (WaitForCompletion() ||
         ((this.exception is PiException) &&
         ((PiException)this.exception).Code == ExceptionCode.ServerCertificateInvalid))
      {
        Begin();

        var assembly = Assembly.GetExecutingAssembly();
        var clientName = assembly.GetName().Name;
        var clientVersion = assembly.GetName().Version.ToString(); 
        
        Status.VotingClient.GetConfig(clientName, clientVersion, GetConfigComplete);

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

    public void CheckUpdate()
    {
      UpdateChecker.CheckUpdate(Status.RemoteConfig, Resources.UpdateDialogTitle, Resources.UpdateDialogText);
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
        List<VotingDescriptor2> votings = new List<VotingDescriptor2>(this.votingList);

        DirectoryInfo appDataDirectory = new DirectoryInfo(Status.DataPath);

        foreach (DirectoryInfo offlineDirectory in appDataDirectory.GetDirectories())
        {
          if (File.Exists(Path.Combine(offlineDirectory.FullName, Files.VotingMaterialFileName)))
          {
            votings.Add(new VotingDescriptor2(offlineDirectory.FullName));
          }
        }

        return votings;
      }
      else
      {
        throw this.exception;
      }
    }

    private void OnGetVotingsComplete(IEnumerable<VotingContainer> votingList, Exception exception)
    {
      if (exception == null)
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
      }

      this.exception = exception;
      this.run = false;
    }

    private void GetConfigComplete(IRemoteConfig remoteConfig, IEnumerable<Group> groups, Exception exception)
    {
      Status.RemoteConfig = remoteConfig;
      Status.Groups = groups;
      this.exception = exception;
      this.run = false;
    }

    private void GetCertificateStorageComplete(Certificate serverCertificate, Exception exception)
    {
      Status.ServerCertificate = serverCertificate;
      this.exception = exception;
      this.run = false;
    }

    public void CreateVoting(Signed<VotingParameters> votingParameters, IEnumerable<AuthorityCertificate> authorities)
    {
      Begin();
      Status.VotingClient.CreateVoting(votingParameters, authorities, CreateVotingCompleted);

      if (!WaitForCompletion())
      {
        throw this.exception;
      }
    }

    private void CreateVotingCompleted(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    public VotingDescriptor CreateShares(Certificate certificate, VotingDescriptor2 voting)
    {
      string fileName = string.Format("{0}@{1}.pi-auth", certificate.Id.ToString(), voting.Id.ToString());
      string filePath = Path.Combine(Status.DataPath, fileName);

      if (Circle.Vote.VotingDialog.ShowVoting(this, null, voting) == DialogResult.OK)
      {
        if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(certificate, GuiResources.UnlockActionAuthorityCreateShares))
        {
          try
          {
            Begin();
            Status.VotingClient.CreateSharePart(voting.Id, (AuthorityCertificate)certificate, filePath, CreateSharesCompleteCallBack);

            if (!WaitForCompletion())
            {
              throw this.exception;
            }
          }
          finally
          {
            certificate.Lock();
          }

          return this.votingDescriptor;
        }
      }

      return voting;
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

      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(certificate, GuiResources.UnlockActionAuthorityCheckShares))
      {
        try
        {
          Begin();
          Status.VotingClient.CheckShares(voting.Id, (AuthorityCertificate)certificate, filePath, CheckSharesComplete);

          if (WaitForCompletion())
          {
            if (this.acceptShares)
            {
              MessageForm.Show(Resources.ControllerCheckSharesOk, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
              MessageForm.Show(Resources.ControllerCheckSharesFailed, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          }
          else
          {
            throw this.exception;
          }
        }
        finally
        {
          certificate.Lock();
        }

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

      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(certificate, GuiResources.UnlockActionAuthorityDecipher))
      {
        try
        {
          this.userCanceled = false;
          Begin();
          Status.VotingClient.CreateDeciphers(voting.Id, (AuthorityCertificate)certificate, filePath, AskForPartiallyDecipher, CreateDeciphersComplete);

          if (!WaitForCompletion() && !this.userCanceled)
          {
            throw this.exception;
          }
        }
        finally
        {
          certificate.Lock();
        }

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
        string.Format(GuiResources.AskForPartiallyDecipher, validEnvelopeCount),
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

    public void Delete(VotingDescriptor2 voting, AdminCertificate adminCertificate)
    {
      var command = new DeleteVotingRequest.Command(voting.Id);
      var signedCommand = new Signed<DeleteVotingRequest.Command>(command, adminCertificate);

      Begin();
      Status.VotingClient.DeleteVoting(signedCommand, DeleteVotingComplete);

      if (!WaitForCompletion())
      {
        throw this.exception;
      }
    }

    private void DeleteVotingComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    public void Download(VotingDescriptor2 voting)
    {
      Begin();
      Status.VotingClient.ActivateVoter();
      Status.VotingClient.DownloadVoting(voting.Id, Status.DataPath, DownloadVotingComplete);

      if (!WaitForCompletion())
      {
        throw this.exception;
      }
    }

    private void DownloadVotingComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    public VotingResult Tally(VotingDescriptor2 voting, out IDictionary<Guid, VoteReceiptStatus> voteReceiptsStatus)
    {
      Begin();
      Status.VotingClient.ActivateVoter();
      Status.VotingClient.GetResult(voting.Id, LoadVoteReceipts(voting.Id), Config.InitialCheckProofCount, GetResultComplete);

      if (!WaitForCompletion())
      {
        throw this.exception;
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
        MessageForm.Show(Resources.ControllerSetSignatureRequestOk, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      else
      {
        throw this.exception;
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
        MessageForm.Show(Resources.ControllerVoteCast, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      else
      {
        throw this.exception;
      }
    }

    public void DownloadSignatureRequests(string savePath)
    {
      Begin();
      Status.VotingClient.GetSignatureRequests(savePath, GetSignatureRequestsComplete);

      if (!WaitForCompletion())
      {
        throw this.exception;
      }
    }

    private void GetSignatureRequestsComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    public string DownloadStats(StatisticsDataType type)
    {
      Begin();
      Status.VotingClient.GetStats(type, GetStatsComplete);

      if (!WaitForCompletion())
      {
        throw this.exception;
      }

      return this.statsData;
    }

    private void GetStatsComplete(string data, Exception exception)
    {
      this.statsData = data;
      this.exception = exception;
      this.run = false;
    }

    public void UploadSignatureResponses(IEnumerable<string> fileNames)
    {
      Begin();
      Status.VotingClient.SetSignatureResponses(fileNames, SetSignatureResponsesComplete);

      if (!WaitForCompletion())
      {
        throw this.exception;
      }
    }

    private void SetSignatureResponsesComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    public void UploadCertificateStorage(string fileName)
    {
      CertificateStorage certificateStorage = Serializable.Load<CertificateStorage>(fileName);
      Begin();
      Status.VotingClient.SetCertificateStorage(certificateStorage, SetCertificateStorageComplete);

      if (!WaitForCompletion())
      {
        throw this.exception;
      }
    }

    private void SetCertificateStorageComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    public byte[] GenerateNotaryCertificate(Certificate notaryCertificate)
    {
      SignCheckCookie cookie = new SignCheckCookie();
      Signed<SignCheckCookie> signedCookie = new Signed<SignCheckCookie>(cookie, notaryCertificate);

      Begin();
      Status.VotingClient.GenerateSignCheck(signedCookie, GenerateSignCheckComplete);

      if (!WaitForCompletion())
      {
        throw this.exception;
      }

      byte[] code = notaryCertificate.Decrypt(this.encryptedCode);

      return code;
    }

    private void GenerateSignCheckComplete(byte[] encryptedCode, Exception exception)
    {
      this.encryptedCode = encryptedCode;
      this.exception = exception;
      this.run = false;
    }
  }
}
