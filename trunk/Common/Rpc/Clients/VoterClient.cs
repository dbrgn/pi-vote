
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
  /// Asynchronous voter client.
  /// </summary>
  public class VoterClient
  {
    /// <summary>
    /// Callback for the connection operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void ConnectCallBack(Exception exception);

    /// <summary>
    /// Callback for the voting list get operation.
    /// </summary>
    /// <param name="votingList">Voting list or null in case of failure.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetVotingListCallBack(IEnumerable<VotingDescriptor> votingList, Exception exception);
    
    /// <summary>
    /// Callback for the vote operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void VoteCallBack(Exception exception);

    /// <summary>
    /// Callback for the result get operation.
    /// </summary>
    /// <param name="result">Voting result or null in case of failure.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetResultCallBack(VotingResult result, Exception exception);

    /// <summary>
    /// Abstract opertion of the voting client.
    /// </summary>
    private abstract class Operation
    {
      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <remarks>
      /// Don't forget to callback when implementing.
      /// </remarks>
      /// <param name="client">Voter client to execute against.</param>
      public abstract void Execute(VoterClient client);
    }

    /// <summary>
    /// Connect operation.
    /// </summary>
    private class ConnectOperation : Operation
    {
      /// <summary>
      /// IP address of the server.
      /// </summary>
      private IPAddress serverIpAddress;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private ConnectCallBack callBack;

      /// <summary>
      /// Create a new connect operation.
      /// </summary>
      /// <param name="serverIpAddress">IP address of server.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public ConnectOperation(IPAddress serverIpAddress, ConnectCallBack callBack)
      {
        this.serverIpAddress = serverIpAddress;
        this.callBack = callBack;
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voter client to execute against.</param>
      public override void Execute(VoterClient client)
      {
        try
        {
          client.client.Connect(this.serverIpAddress);
          client.proxy = new VoterRpcProxy(client.client, client.voterEntity.Certificate);
          this.callBack(null);
        }
        catch (Exception exception)
        {
          this.callBack(exception);
        }
      }
    }

    /// <summary>
    /// Voting list get operation.
    /// </summary>
    private class GetVotingListOperation : Operation
    {
      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetVotingListCallBack callBack;
      
      /// <summary>
      /// Create a new voting list get operation.
      /// </summary>
      /// <param name="callBack">Callback upon completion.</param>
      public GetVotingListOperation(GetVotingListCallBack callBack)
      {
        this.callBack = callBack;
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voter client to execute against.</param>
      public override void Execute(VoterClient client)
      {
        try
        {
          List<VotingDescriptor> votingList = new List<VotingDescriptor>();
          IEnumerable<int> votingIds = client.proxy.FetchVotingIds();

          foreach (int votingId in votingIds)
          {
            VotingStatus status = client.proxy.FetchVotingStatus(votingId);
            VotingMaterial material = client.proxy.FetchVotingMaterial(votingId);
            votingList.Add(new VotingDescriptor(material, status));
          }

          this.callBack(votingList, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, exception);
        }
      }
    }

    /// <summary>
    /// Voting descriptor.
    /// </summary>
    public class VotingDescriptor
    {
      private readonly int id;
      private readonly string name;
      private readonly VotingStatus status;
      private readonly List<OptionDescriptor> options;

      /// <summary>
      /// Id of the voting.
      /// </summary>
      public int Id { get { return this.id; } }

      /// <summary>
      /// Name of the voting.
      /// </summary>
      public string Name { get { return this.name; } }

      /// <summary>
      /// Status of the voting.
      /// </summary>
      public VotingStatus Status { get { return this.status; } }

      /// <summary>
      /// Options in the voting.
      /// </summary>
      public IEnumerable<OptionDescriptor> Options { get { return this.options; } }

      /// <summary>
      /// Create a new voting descriptor.
      /// </summary>
      /// <param name="material">Material of voting to describe.</param>
      /// <param name="status">Status of the voting.</param>
      public VotingDescriptor(VotingMaterial material, VotingStatus status)
      {
        this.id = material.VotingId;
        this.name = material.Parameters.VotingName;
        this.status = status;
        this.options = new List<OptionDescriptor>();

        material.Parameters.Options.Foreach(option => this.options.Add(new OptionDescriptor(option)));
      }
    }

    /// <summary>
    /// Voting option descriptor.
    /// </summary>
    public class OptionDescriptor
    {
      private readonly string text;
      private readonly string description;

      /// <summary>
      /// Text of the option.
      /// </summary>
      public string Text { get { return this.text; } }

      /// <summary>
      /// Description of the option.
      /// </summary>
      public string Description { get { return this.description; } }

      /// <summary>
      /// Creates a new option decriptor.
      /// </summary>
      /// <param name="option">Option to decscribe.</param>
      public OptionDescriptor(Option option)
      {
        this.text = option.Text;
        this.description = option.Description;
      }
    }

    /// <summary>
    /// Vote cast operation.
    /// </summary>
    private class VoteOperation : Operation
    {
      /// <summary>
      /// Id of the voting.
      /// </summary>
      private int votingId;

      /// <summary>
      /// Index of chosen option.
      /// </summary>
      private int optionIndex;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private VoteCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="optionIndex">Index of chosen option.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public VoteOperation(int votingId, int optionIndex, VoteCallBack callBack)
      {
        this.votingId = votingId;
        this.optionIndex = optionIndex;
        this.callBack = callBack;
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voter client to execute against.</param>
      public override void Execute(VoterClient client)
      {
        try
        {
          var material = client.proxy.FetchVotingMaterial(this.votingId);

          int[] vota = new int[material.Parameters.OptionCount];
          vota[this.optionIndex] = 1;

          var envelope = client.voterEntity.Vote(material, vota);

          client.proxy.PushEnvelope(this.votingId, envelope);

          this.callBack(null);
        }
        catch (Exception exception)
        {
          this.callBack(exception);
        }
      }
    }

    /// <summary>
    /// Result get operation.
    /// </summary>
    private class GetResultOperation : Operation
    {
      /// <summary>
      /// Id of the voting.
      /// </summary>
      private int votingId;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetResultCallBack callBack;

      /// <summary>
      /// Queue with enveloped to verify and add.
      /// </summary>
      private Queue<Signed<Envelope>> envelopeQueue;

      /// <summary>
      /// Continue to run worker threads?
      /// </summary>
      private bool workerRun;

      /// <summary>
      /// Voter client to work with.
      /// </summary>
      private VoterClient client;

      /// <summary>
      /// Create a new result get operation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public GetResultOperation(int votingId, GetResultCallBack callBack)
      {
        this.votingId = votingId;
        this.callBack = callBack;
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voter client to execute against.</param>
      public override void Execute(VoterClient client)
      {
        try
        {
          var material = client.proxy.FetchVotingMaterial(votingId);
          int envelopeCount = client.proxy.FetchEnvelopeCount(votingId);
          client.voterEntity.TallyBegin(material);

          this.client = client;
          this.envelopeQueue = new Queue<Signed<Envelope>>();
          this.workerRun = true;
          List<Thread> workers = new List<Thread>();
          Environment.ProcessorCount.Times(() => workers.Add(new Thread(TallyAddWorker)));
          workers.ForEach(worker => worker.Start());

          for (int envelopeIndex = 0; envelopeIndex < envelopeCount; envelopeIndex++)
          {
            while (this.envelopeQueue.Count > Environment.ProcessorCount * 2)
            {
              Thread.Sleep(1);
            }

            var envelope = client.proxy.FetchEnvelope(votingId, envelopeIndex);

            lock (this.envelopeQueue)
            {
              this.envelopeQueue.Enqueue(envelope);
            }
          }

          while (this.envelopeQueue.Count > 0)
          {
            Thread.Sleep(1);
          }

          this.workerRun = false;
          workers.ForEach(worker => worker.Join());

          for (int authorityIndex = 1; authorityIndex < material.Parameters.AuthorityCount + 1; authorityIndex++)
          {
            client.voterEntity.TallyAddPartialDecipher(client.proxy.FetchPartialDecipher(votingId, authorityIndex));
          }

          var result = client.voterEntity.TallyResult;

          this.callBack(result, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, exception);
        }
      }

      /// <summary>
      /// Thread that adds envelopes to tally.
      /// </summary>
      private void TallyAddWorker()
      {
        while (this.workerRun)
        {
          Signed<Envelope> envelope = null;

          lock (this.envelopeQueue)
          {
            if (this.envelopeQueue.Count > 0)
            {
              envelope = this.envelopeQueue.Dequeue();
            }
          }

          if (envelope == null)
          {
            Thread.Sleep(1);
          }
          else
          {
            this.client.voterEntity.TallyAdd(envelope);
          }
        }
      }
    }

    /// <summary>
    /// TCP RPC client.
    /// </summary>
    private TcpRpcClient client;

    /// <summary>
    /// Voter entity for calculations.
    /// </summary>
    private VoterEntity voterEntity;

    /// <summary>
    /// Root certificate.
    /// </summary>
    private CACertificate rootCertificate;

    /// <summary>
    /// Queue of pending operations.
    /// </summary>
    private Queue<Operation> operations;

    /// <summary>
    /// Voter RPC proxy.
    /// </summary>
    private VoterRpcProxy proxy;

    /// <summary>
    /// Master operation worker thread.
    /// </summary>
    private Thread masterThread;

    /// <summary>
    /// Continue to run operations.
    /// </summary>
    private bool run;

    /// <summary>
    /// Create a new voter client.
    /// </summary>
    /// <param name="rootCertificate">Root certificate of CA.</param>
    /// <param name="voterCertificate">Certificate of the voter.</param>
    public VoterClient(CACertificate rootCertificate, VoterCertificate voterCertificate)
    {
      this.rootCertificate = rootCertificate;
      this.voterEntity = new VoterEntity(rootCertificate, voterCertificate);
      this.client = new TcpRpcClient();
      this.operations = new Queue<Operation>();
      this.run = true;
      this.masterThread = new Thread(RunMaster);
      this.masterThread.Start();
    }

    /// <summary>
    /// Master thread running operations.
    /// </summary>
    private void RunMaster()
    {
      while (this.run)
      {
        Operation currentOperation = null;

        lock (this.operations)
        {
          if (this.operations.Count > 0)
          {
            currentOperation = this.operations.Dequeue();
          }
        }

        if (currentOperation == null)
        {
          Thread.Sleep(1);
        }
        else
        {
          currentOperation.Execute(this);
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
    public void Vote(int votingid, int optionIndex, VoteCallBack callBack)
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
    public void GetResult(int votingId, GetResultCallBack callBack)
    {
      lock (this.operations)
      {
        this.operations.Enqueue(new GetResultOperation(votingId, callBack));
      }
    }
  }
}
