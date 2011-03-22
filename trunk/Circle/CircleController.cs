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
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Circle
{
  public class CircleController
  {
    private bool run;
    private Exception exception;
    private IEnumerable<VotingDescriptor> votingList;
    private VotingMaterial votingMaterial;
    private Signed<VoteReceipt> voteReceipt;
    private Dictionary<Certificate, string> certificates;
    private VotingClient.Operation currentOperation;
    private Signed<SignatureResponse> signatureResponse;
    private SignatureResponseStatus responseStatus;

    public CircleStatus Status { get; private set; }

    public string Text
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
          return "Failed: " + this.exception.Message;
        }
        else
        {
          if (this.currentOperation != null)
          {
            return "Working: " + this.currentOperation.Text + " " + this.currentOperation.SubText;
          }
          else
          {
            return "Working";
          }
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
      Status = new CircleStatus();
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
      this.run = true;
      Status.VotingClient.GetSignatureResponse(certificate.Id, GetSignatureResponseComplete);

      if (WaitForCompletion())
      {
        var response = this.signatureResponse.Value;

        if (response.SubjectId.Equals(certificate.Id) &&
          this.signatureResponse.Verify(Status.CertificateStorage))
        {
          if (this.responseStatus == SignatureResponseStatus.Accepted)
          {
            certificate.AddSignature(response.Signature);
          }

          return new Tuple<SignatureResponseStatus, string>(this.responseStatus, response.Reason);
        }
        else
        {
          return new Tuple<SignatureResponseStatus, string>(SignatureResponseStatus.Unknown, string.Empty);
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
      this.run = true;

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

    public VoterCertificate GetVoterCertificate(int groupId)
    {
      return this.certificates.Keys
        .Where(certificate => certificate is VoterCertificate &&
          ((VoterCertificate)certificate).GroupId == groupId)
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
    }

    public void Prepare()
    {
      Status.CertificateStorage = new CertificateStorage();
      Status.CertificateStorage.TryLoadRoot();
      Status.VotingClient = new VotingClient(Status.CertificateStorage);

      try
      {
        Status.VotingClient.Connect(Status.ServerEndPoint);
      }
      catch
      {
        return;
      }

      this.run = true;
      Status.VotingClient.GetCertificateStorage(Status.CertificateStorage, OnGetCertificateStorageComplete);

      if (WaitForCompletion())
      {
        Status.VotingClient.GetConfig(OnGetConfigComplete);

        if (WaitForCompletion())
        { 
        }
      }
    }

    public void Vote(VoterCertificate voterCertificate, VotingDescriptor voting, IEnumerable<IEnumerable<bool>> vota)
    {
      this.run = true;
      Status.VotingClient.ActivateVoter();
      Status.VotingClient.GetVotingMaterial(voting.Id, GetVotingMaterialComplete);

      if (WaitForCompletion())
      {
        this.run = true;
        Status.VotingClient.Vote(this.votingMaterial, voterCertificate, vota, VoteComplete);

        if (WaitForCompletion())
        {
          this.voteReceipt.Save(
            Path.Combine(Status.DataPath,
            string.Format("{0}@{1}.pi-receipt",
            voterCertificate.Id.ToString(), this.votingMaterial.Parameters.Value.VotingId.ToString())));
        }
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

    public IEnumerable<VotingDescriptor> GetVotingList()
    {
      this.run = true;
      Status.VotingClient.GetVotingList(Status.CertificateStorage, Status.DataPath, OnGetVotingListComplete);

      if (WaitForCompletion())
      {
        return this.votingList;
      }
      else
      {
        return null;
      }
    }

    private void OnGetVotingListComplete(IEnumerable<VotingDescriptor> votingList, Exception exception)
    {
      this.votingList = votingList;
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
  }
}
