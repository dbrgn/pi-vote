﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

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
    /// Connecting to server?
    /// </summary>
    public bool Connecting { get; private set; }

    /// <summary>
    /// Server IP and port.
    /// </summary>
    private IPEndPoint serverEndPoint;

    /// <summary>
    /// Failure exception.
    /// </summary>
    private Exception exception;

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

    /// <summary>
    /// Connect to the server.
    /// </summary>
    /// <param name="serverEndPoint">IP address and port of the server.</param>
    public void Connect(IPEndPoint serverEndPoint)
    {
      this.serverEndPoint = serverEndPoint;
      Connecting = true;

      Thread connectThread = new Thread(ConnectAsync);
      connectThread.Start();

      while (Connecting)
      {
        Application.DoEvents();
        Thread.Sleep(10);
      }

      if (this.exception != null)
      {
        throw this.exception;
      }
    }

    private void ConnectAsync()
    {
      try
      {
        this.client.Connect(this.serverEndPoint);
        this.proxy = new VotingRpcProxy(this.client);
        this.proxy.Start();
        this.exception = null;
      }
      catch (Exception exception)
      {
        this.exception = exception;
      }

      Connecting = false;
    }

    /// <summary>
    /// Connect to the server.
    /// </summary>
    /// <param name="serverEndPoint">IP address and port of the server.</param>
    /// <param name="serverEndPoint">Proxy IP address and port.</param>
    public void Connect(IPEndPoint serverEndPoint, IPEndPoint proxyEndPoint)
    {
      this.client.Connect(serverEndPoint, proxyEndPoint);
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
    public void ActivateVoter()
    {
      this.voterEntity = new VoterEntity(this.certificateStorage);
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
    /// <param name="serverEndPoint">IP address and port of the server.</param>
    /// <param name="callBack">Callback upon connection.</param>
    public void Connect(IPEndPoint serverEndPoint, ConnectCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new ConnectOperation(serverEndPoint, callBack));
      }
    }

    /// <summary>
    /// Connect to the server.
    /// </summary>
    /// <param name="serverEndPoint">IP address and port of the server.</param>
    /// <param name="proxyEndPoint">Proxy IP address and port.</param>
    /// <param name="callBack">Callback upon connection.</param>
    public void Connect(IPEndPoint serverEndPoint, IPEndPoint proxyEndPoint, ConnectCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new ConnectOperation(serverEndPoint, proxyEndPoint, callBack));
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
    /// /// <param name="voterCertificate">Certificate of the voter.</param>
    /// <param name="vota">Selected options.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void Vote(VotingMaterial votingMaterial, VoterCertificate voterCertificate, IEnumerable<IEnumerable<bool>> vota, VoteCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new VoteOperation(votingMaterial, voterCertificate, vota, callBack));
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
    /// Gets the voting material.
    /// </summary>
    /// <param name="votingid">Id of the voting.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetVotings(List<Guid> votingIds, GetVotingsCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetVotingsOperation(votingIds, callBack));
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
    /// <param name="intitalCheckProofCount">Number of proofs to check initially.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetResult(Guid votingId, IEnumerable<Signed<VoteReceipt>> signedVoteReceipts, int intitalCheckProofCount, GetResultCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetResultOperation(votingId, signedVoteReceipts, intitalCheckProofCount, callBack));
      }
    }

    /// <summary>
    /// Get result from server.
    /// </summary>
    /// <param name="offlinePath">Path to the offline voting files.</param>
    /// <param name="signedVoteReceipts">Vote receipts signed by the server to check.</param>
    /// <param name="intitalCheckProofCount">Number of proofs to check initially.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetResult(string offlinePath, IEnumerable<Signed<VoteReceipt>> signedVoteReceipts, int intitalCheckProofCount, GetResultCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetResultOperation(offlinePath, signedVoteReceipts, intitalCheckProofCount, callBack));
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
    /// <param name="certificateStorage">Client's certificate storage.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetCertificateStorage(CertificateStorage certificateStorage, GetCertificateStorageCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetCertificateStorageOperation(certificateStorage, callBack));
      }
    }

    /// <summary>
    /// Get remote client config from server.
    /// </summary>
    /// <param name="clientName">Name of the client.</param>
    /// <param name="clientVersion">Version of the client.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetConfig(string clientName, string clientVersion, GetConfigCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetConfigOperation(clientName, clientVersion, callBack));
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
    /// <param name="signatureRequest">Signature Request signed and encrypted for the CA.</param>
    /// <param name="signatureRequestInfo">Signature Request Info signed and encrypted for the server.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void SetSignatureRequest(Secure<SignatureRequest> signatureRequest, Secure<SignatureRequestInfo> signatureRequestInfo, SetSignatureRequestCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new SetSignatureRequestOperation(signatureRequest, signatureRequestInfo, callBack));
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
    /// Get signature requests from server.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetStats(StatisticsDataType type, GetStatsCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetStatsOperation(type, callBack));
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
    /// <param name="askCallBack">Callback to ask for permission to partially decipher.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void CreateDeciphers(Guid votingId, AuthorityCertificate authorityCertificate, string authorityFileName, AskForPartiallyDecipherCallBack askCallBack, CreateDeciphersCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new CreateDeciphersOperation(votingId, authorityCertificate, authorityFileName, askCallBack, callBack));
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

    /// <summary>
    /// Set a certificate storage on the server.
    /// </summary>
    /// <param name="comand">Command to delete a voting.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void DeleteVoting(Signed<DeleteVotingRequest.Command> comand, DeleteVotingCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new DeleteVotingOperation(comand, callBack));
      }
    }

    /// <summary>
    /// Generates a sign check.
    /// </summary>
    /// <param name="signedCookie">Signed sign check cookie.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GenerateSignCheck(Signed<SignCheckCookie> signedCookie, GenerateSignCheckCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GenerateSignCheckOperation(signedCookie, callBack));
      }
    }
  }
}