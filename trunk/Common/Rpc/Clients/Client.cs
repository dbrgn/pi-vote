
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

    /// <summary>
    /// Closes the client connection.
    /// </summary>
    public void Close()
    {
      this.run = false;
      this.masterThread.Join();
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
    /// <param name="callBack">Callback upon completion.</param>
    public void GetVotingList(GetVotingListCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetVotingListOperation(callBack));
      }
    }

    /// <summary>
    /// Send vote to server.
    /// </summary>
    /// <param name="votingid">Id of the voting.</param>
    /// <param name="optionIndex">Index of selected option.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void Vote(Guid votingid, int optionIndex, VoteCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new VoteOperation(votingid, optionIndex, callBack));
      }
    }

    /// <summary>
    /// Get result from server.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="callBack">Callback upon completion.</param>
    public void GetResult(Guid votingId, GetResultCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetResultOperation(votingId, callBack));
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
  }
}