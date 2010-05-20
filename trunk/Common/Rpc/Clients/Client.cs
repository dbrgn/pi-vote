
/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Asynchronous client.
  /// </summary>
  public partial class VotingClient
  {
    /// <summary>
    /// TCP RPC client.
    /// </summary>
    private TcpRpcClient client;

    /// <summary>
    /// Voter entity for calculations.
    /// </summary>
    private VoterEntity voterEntity;

    /// <summary>
    /// Authority entity for calculations.
    /// </summary>
    private AuthorityEntity authorityEntity;

    /// <summary>
    /// Certificate storage.
    /// </summary>
    private CertificateStorage certificateStorage;

    /// <summary>
    /// Queue of pending operations.
    /// </summary>
    private Queue<Operation> operations;

    /// <summary>
    /// Voter RPC proxy.
    /// </summary>
    private VotingRpcProxy proxy;

    /// <summary>
    /// Master operation worker thread.
    /// </summary>
    private Thread masterThread;

    /// <summary>
    /// Continue to run operations.
    /// </summary>
    private bool run;

    /// <summary>
    /// Currently active operation.
    /// </summary>
    public Operation CurrentOperation { get; private set; }

    /// <summary>
    /// Create a new voter client.
    /// </summary>
    /// <param name="certificateStorage">Certificate storage</param>
    public VotingClient(CertificateStorage certificateStorage)
    {
      this.certificateStorage = certificateStorage;
      this.client = new TcpRpcClient();
      this.operations = new Queue<Operation>();
      this.run = true;
      this.masterThread = new Thread(RunMaster);
      this.masterThread.Start();
    }

    public void Connect(IPAddress serverIpAddress)
    {
      this.client.Connect(serverIpAddress);
      this.proxy = new VotingRpcProxy(this.client);
      this.proxy.Start();
    }

    /// <summary>
    /// Closes the client connection.
    /// </summary>
    public void Close()
    {
      this.run = false;
      this.masterThread.Join();

      if (this.proxy != null)
        this.proxy.Stop();

      this.client.Disconnect();
    }

    /// <summary>
    /// Enables voter entity.
    /// </summary>
    /// <param name="voterCertificate">Certificate of the voter.</param>
    public void ActivateVoter(VoterCertificate voterCertificate)
    {
      this.voterEntity = new VoterEntity(this.certificateStorage, voterCertificate);
    }

    /// <summary>
    /// Creates an authority entity.
    /// </summary>
    /// <param name="certificate">Certificate of the authority.</param>
    public void CreateAuthority(AuthorityCertificate certificate)
    {
      this.authorityEntity = new AuthorityEntity(this.certificateStorage, certificate);
    }

    /// <summary>
    /// Saves the authorities data to file.
    /// </summary>
    /// <param name="fileName">Name of file to save to.</param>
    public void SaveAuthority(string fileName)
    {
      this.authorityEntity.Save(fileName);
    }

    /// <summary>
    /// Loads an authority's data from file.
    /// </summary>
    /// <param name="fileName">Name of file to load data from.</param>
    /// <param name="certificate">Authority's certificate.</param>
    public void LoadAuthority(string fileName, AuthorityCertificate certificate)
    {
      this.authorityEntity = new AuthorityEntity(this.certificateStorage, certificate, fileName);
    }

    /// <summary>
    /// Master thread running operations.
    /// </summary>
    private void RunMaster()
    {
      while (this.run)
      {
        Operation operation = null;

        lock (this.operations)
        {
          if (this.operations.Count > 0)
          {
            operation = this.operations.Dequeue();
          }
        }

        if (operation == null)
        {
          Thread.Sleep(1);
        }
        else
        {
          CurrentOperation = operation;
          operation.Execute(this);
          operation = null;
        }
      }
    }

    /// <summary>
    /// Are we connected to the server?
    /// </summary>
    public bool Connected
    {
      get { return this.client.Connected; }
    }

    /// <summary>
    /// Connect to the server.
    /// </summary>
    /// <param name="serverIpAddress">IP address of the server.</param>
    /// <param name="callBack">Callback upon connection.</param>
    public void Connect(IPAddress serverIpAddress, ConnectCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new ConnectOperation(serverIpAddress, callBack));
      }
    }

    /// <summary>
    /// Get voting list from server.
    /// </summary>
    /// <param name="certificateStorage">Certificate storage.</param>
    /// <param name="dataPath">Path where program data is stored.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetVotingList(CertificateStorage certificateStorage, string dataPath, GetVotingListCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetVotingListOperation(certificateStorage, dataPath, callBack));
      }
    }

    /// <summary>
    /// Send vote to server.
    /// </summary>
    /// <param name="votingMaterial">Material to vote.</param>
    /// <param name="optionIndex">Index of selected option.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void Vote(VotingMaterial votingMaterial, IEnumerable<IEnumerable<bool>> vota, VoteCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new VoteOperation(votingMaterial, vota, callBack));
      }
    }

    /// <summary>
    /// Gets the voting material.
    /// </summary>
    /// <param name="votingid">Id of the voting.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetVotingMaterial(Guid votingid, GetVotingMaterialCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetVotingMaterialOperation(votingid, callBack));
      }
    }

    /// <summary>
    /// Download the whole voting from the server and
    /// store it in files.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="votingId">Path where program data is stored.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void DownloadVoting(Guid votingId, string dataPath, DownloadVotingCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new DownloadVotingOperation(votingId, dataPath, callBack));
      }
    }

    /// <summary>
    /// Get result from server.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="signedVoteReceipts">Vote receipts signed by the server to check.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetResult(Guid votingId, IEnumerable<Signed<VoteReceipt>> signedVoteReceipts, GetResultCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetResultOperation(votingId, signedVoteReceipts, callBack));
      }
    }

    /// <summary>
    /// Get result from server.
    /// </summary>
    /// <param name="offlinePath">Path to the offline voting files.</param>
    /// <param name="signedVoteReceipts">Vote receipts signed by the server to check.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetResult(string offlinePath, IEnumerable<Signed<VoteReceipt>> signedVoteReceipts, GetResultCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetResultOperation(offlinePath, signedVoteReceipts, callBack));
      }
    }

    /// <summary>
    /// Get signature response from server.
    /// </summary>
    /// <param name="certificateId">Id of the certificate.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetSignatureResponse(Guid certificateId, GetSignatureResponseCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetSignatureResponseOperation(certificateId, callBack));
      }
    }

    /// <summary>
    /// Get certificate storage from server.
    /// </summary>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetCertificateStorage(GetCertificateStorageCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetCertificateStorageOperation(callBack));
      }
    }

    /// <summary>
    /// Get all valid authority certificates.
    /// </summary>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetAuthorityCertificates(GetAuthorityCertificatesCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetAuthorityCertificatesOperation(callBack));
      }
    }

    /// <summary>
    /// Send a signature request to the server.
    /// </summary>
    /// <param name="signatureRequest">Signed signature request.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void SetSignatureRequest(Signed<SignatureRequest> signatureRequest, SetSignatureRequestCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new SetSignatureRequestOperation(signatureRequest, callBack));
      }
    }

    /// <summary>
    /// Send a signature responses to server.
    /// </summary>
    /// <param name="fileNames">Names of signature response files.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void SetSignatureResponses(IEnumerable<string> fileNames, SetSignatureResponsesCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new SetSignatureResponsesOperation(fileNames, callBack));
      }
    }

    /// <summary>
    /// Get signature requests from server.
    /// </summary>
    /// <param name="fileNames">Names of signature response files.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetSignatureRequests(string savePath, GetSignatureRequestsCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetSignatureRequestsOperation(savePath, callBack));
      }
    }

    /// <summary>
    /// Create a voting procedure.
    /// </summary>
    /// <param name="votingParameters">Parameters of voting procedure.</param>
    /// <param name="authorities">Authorities overseeing the voting procedure.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void CreateVoting(Signed<VotingParameters> votingParameters, IEnumerable<AuthorityCertificate> authorities, CreateVotingCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new CreateVotingOperation(votingParameters, authorities, callBack));
      }
    }

    /// <summary>
    /// Authority creates share parts and pushes them to the server.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="authorityFileName">Filename to save authority data.</param>
    /// <param name="authorityCertificate">Authority's certificate.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void CreateSharePart(Guid votingId, AuthorityCertificate authorityCertificate, string authorityFileName, CreateSharePartCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new CreateSharePartOperation(votingId, authorityCertificate, authorityFileName, callBack));
      }
    }

    /// <summary>
    /// Authority check share parts from server.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="authorityFileName">Filename to load authority data.</param>
    /// <param name="authorityCertificate">Authority's certificate.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void CheckShares(Guid votingId, AuthorityCertificate authorityCertificate, string authorityFileName, CheckSharesCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new CheckSharesOperation(votingId, authorityCertificate, authorityFileName, callBack));
      }
    }

    /// <summary>
    /// Authority create partial deciphers push to server.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="authorityFileName">Filename to load authority data.</param>
    /// <param name="authorityCertificate">Authority's certificate.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void CreateDeciphers(Guid votingId, AuthorityCertificate authorityCertificate, string authorityFileName, CreateDeciphersCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new CreateDeciphersOperation(votingId, authorityCertificate, authorityFileName, callBack));
      }
    }

    /// <summary>
    /// Set a certificate storage on the server.
    /// </summary>
    /// <param name="certificateStorage">Certificate storage to add to the server's data.</param>
    /// <param name="callBack">Callback upon completion</param>
    public void SetCertificateStorage(CertificateStorage certificateStorage, SetCertificateStorageCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new SetCertificateStorageOperation(certificateStorage, callBack));
      }
    }
  }
}