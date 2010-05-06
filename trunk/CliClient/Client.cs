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
using System.IO;
using System.Net;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.CliClient.VoteService;

namespace Pirate.PiVote.CliClient
{
  public class Client
  {
    private IBinaryRpcProxy client;
    private bool run;
    private CertificateStorage certs;
    private List<AuthorityCertificate> auths;
    private List<VoterCertificate> voters;
    private CACertificate root;
    private CACertificate intermediate;
    private AdminCertificate admin;
    private VotingClient voterClient;
    
    public Client()
    {
      this.certs = new CertificateStorage();

      this.root = GetCertificate<CACertificate>("Root", null);
      this.certs.AddRoot(this.root.OnlyPublicPart);

      this.intermediate = GetCertificate<CACertificate>("Intermediate", root);
      this.certs.Add(this.intermediate.OnlyPublicPart);

      this.admin = GetCertificate<AdminCertificate>("Admin", this.intermediate);
      this.certs.Add(this.admin.OnlyPublicPart);

      this.auths = new List<AuthorityCertificate>();
      for (int i = 0; i < 5; i++)
      {
        AuthorityCertificate auth = GetCertificate<AuthorityCertificate>(string.Format("Authority{0}", i), intermediate);
        this.certs.Add(auth.OnlyPublicPart);
        this.auths.Add(auth);
        auth.Valid(this.certs);
      }

      this.voters = new List<VoterCertificate>();
      for (int i = 0; i < 10; i++)
      {
        VoterCertificate voter = GetCertificate<VoterCertificate>(string.Format("Voter{0}", i), intermediate);
        this.certs.Add(voter.OnlyPublicPart);
        this.voters.Add(voter);
      }

      var rootCrl = new Signed<RevocationList>(new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddYears(1), new List<Guid>()), root);
      this.certs.AddRevocationList(rootCrl);
      var intCrl = new Signed<RevocationList>(new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddYears(1), new List<Guid>()), intermediate);
      this.certs.AddRevocationList(intCrl);

      this.certs.Save("all.certs");
    }

    private TCertificate GetCertificate<TCertificate>(string name, CACertificate issuer)
      where TCertificate : Certificate
    { 
      string fileName = string.Format("{0}.cert", name);

      if (File.Exists(fileName))
      {
        return (TCertificate)Certificate.Load(fileName);
      }
      else
      {
        TCertificate certificate = null;
        if (typeof(TCertificate) == typeof(VoterCertificate))
        {
          certificate = (TCertificate)Activator.CreateInstance(typeof(TCertificate));
        }
        else
        {
          certificate = (TCertificate)Activator.CreateInstance(typeof(TCertificate), new object[] { name });
        }
        certificate.CreateSelfSignature();
        if (issuer != null)
          certificate.AddSignature(issuer, DateTime.Now.AddYears(1));
        certificate.Save(fileName);
        return certificate;
      }
    }

    public void Start()
    {
      try
      {
        Do();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.ReadLine();
      }
    }

    private void Connect()
    {
      this.client = new TcpRpcClient();
      ((TcpRpcClient)this.client).Connect(IPAddress.Loopback);
      //this.client = new VoteServiceSoapClient();
    }

    private void Do()
    {
      this.run = true;

      while (this.run)
      {
        Console.Write("Enter command: ");
        string command = Console.ReadLine().ToLower();

        Connect();

        switch (command)
        { 
          case "quit":
          case "exit":
            this.run = false;
            break;
          case "admin":
            AdminMode();
            break;
          case "auth":
            AuthorityMode();
            break;
          case "voter":
            VoterMode();
            break;
        }
      }
    }

    private void AdminMode()
    {
      Console.WriteLine();
      Console.WriteLine("Admin Mode");
      Console.WriteLine();
      Console.WriteLine("Creating new voting procedure now.");

      Console.Write("Enter title: ");
      string title = Console.ReadLine();
      Console.Write("Enter description: ");
      string desc = Console.ReadLine();
      Console.Write("Enter question: ");
      string quest = Console.ReadLine();

      VotingParameters votingParameters = 
        new VotingParameters(
          new MultiLanguageString(title), 
          new MultiLanguageString(desc), 
          DateTime.Now, 
          DateTime.Now.AddDays(1));

      Question question = new Question(new MultiLanguageString(quest), new MultiLanguageString(string.Empty), 1);

      Console.Write("Enter option name: ");
      string optionName = Console.ReadLine();
      while (!optionName.IsNullOrEmpty())
      {
        question.AddOption(new Option(new MultiLanguageString(optionName), new MultiLanguageString(string.Empty)));
        Console.Write("Enter option name: ");
        optionName = Console.ReadLine();
      }

      votingParameters.AddQuestion(question);

      Console.Write("Requesting server to create voting procedure...");
      VotingRpcProxy proxy = new VotingRpcProxy(this.client);
      Signed<VotingParameters> signedVotingParameters = new Signed<VotingParameters>(votingParameters, this.admin);
      proxy.CreateVoting(signedVotingParameters, this.auths.Select(auth => (AuthorityCertificate)auth.OnlyPublicPart));
      Console.WriteLine("Done");
      Console.WriteLine("Voting id is {0}.", votingParameters.VotingId);

      Console.Write("Hit enter to end voting: ");
      Console.ReadLine();

      Console.Write("Ending voting...");
      proxy.EndVoting(votingParameters.VotingId);
      Console.WriteLine("Done");

      Console.WriteLine();
    }

    private void AuthorityMode()
    {
      Console.WriteLine();
      Console.WriteLine("Authority Mode");
      Console.WriteLine();
      Console.WriteLine("Preparing for voting.");
      Console.Write("Enter index (0..4): ");
      int index = Convert.ToInt32(Console.ReadLine());

      Guid votingId = Guid.NewGuid();
      AuthorityCertificate cert = this.auths[index];
      CertificateStorage certificateStorage = new CertificateStorage();
      certificateStorage.AddRoot((CACertificate)this.root.OnlyPublicPart);
      AuthorityEntity auth = new AuthorityEntity(certificateStorage, cert);

      Console.Write("Fetching parameters...");
      VotingRpcProxy proxy = new VotingRpcProxy(this.client);
      var p = proxy.FetchParameters(votingId, (AuthorityCertificate)cert.OnlyPublicPart);
      auth.Prepare(p.Key, p.Value);
      Console.WriteLine("Done");

      Console.Write("Fetching authority list...");
      var al = proxy.FetchAuthorityList(votingId);
      auth.SetAuthorities(al);
      Console.WriteLine("Done");

      Console.Write("Pushing shares...");
      proxy.PushShares(votingId, auth.GetShares());
      Console.WriteLine("Done");

      Console.Write("Waiting for peers...");
      List<Guid> authoritiesDone = new List<Guid>();
      while (proxy.FetchVotingStatus(votingId, out authoritiesDone) != VotingStatus.Sharing)
      {
        Thread.Sleep(1000);
      }
      Console.WriteLine("Done");

      Console.Write("Fetching shares...");
      var shares = proxy.FetchAllShares(votingId);
      Console.WriteLine("Done");

      Console.Write("Verifying shares...");
      var response = auth.VerifyShares(shares);
      Console.WriteLine("Done");

      Console.Write("Pushing share response...");
      proxy.PushShareResponse(votingId, response);
      Console.WriteLine("Done");

      Console.Write("Waiting for peers...");
      while (proxy.FetchVotingStatus(votingId, out authoritiesDone) == VotingStatus.Sharing)
      {
        Thread.Sleep(1000);
      }
      Console.WriteLine("Done");

      if (proxy.FetchVotingStatus(votingId, out authoritiesDone) != VotingStatus.Voting)
      {
        Console.WriteLine("Voting aborted.");
        Console.WriteLine();
        return;
      }
      
      Console.WriteLine("Voting begun.");

      Console.Write("Waiting for voters...");
      while (proxy.FetchVotingStatus(votingId, out authoritiesDone) != VotingStatus.Deciphering)
      {
        Thread.Sleep(1000);
      }
      Console.WriteLine("Done");

      Console.Write("Getting envelopes.");
      var votingMaterial = proxy.FetchVotingMaterial(votingId);
      int envelopeCount = proxy.FetchEnvelopeCount(votingId);

      auth.TallyBegin(votingMaterial);

      for (int envelopeIndex = 0; envelopeIndex < envelopeCount; envelopeIndex++)
      {
        auth.TallyAdd(envelopeIndex, proxy.FetchEnvelope(votingId, envelopeIndex), new Progress(null));
        Console.Write(".");
      }

      var pds = auth.PartiallyDecipher();
      Console.WriteLine("Done");

      Console.Write("Pushing partial deciphers...");
      proxy.PushPartailDecipher(votingId, pds);
      Console.WriteLine("Done");

      Console.WriteLine("Voting completed.");
      Console.ReadLine();
    }

    private void VoterMode()
    {
      Console.WriteLine("Voter Mode");

      Console.Write("Enter voter index 0..9: ");
      int voterIndex = Convert.ToInt32(Console.ReadLine());

      var certificateStorage = new CertificateStorage();
      certificateStorage.AddRoot(this.root.OnlyPublicPart);
      this.voterClient = new VotingClient(certificateStorage);
      this.voterClient.ActivateVoter(this.voters[voterIndex]);

      this.voterClient.Connect(IPAddress.Loopback, Connected);
    }

    private void Connected(Exception exception)
    {
      if (exception == null)
      {
        Console.WriteLine();
        Console.WriteLine("Connected to voting server.");

        this.voterClient.GetVotingList(certs, VotingList);
      }
      else
      {
        Console.WriteLine(exception.ToString());
      }
    }

    private void VotingList(IEnumerable<VotingDescriptor> votingList, Exception exception)
    {
      if (votingList == null)
      {
        Console.WriteLine(exception.ToString());
      }
      else
      {
        Console.WriteLine("Current votes are:");

        foreach (var voting in votingList)
        {
          Console.WriteLine("  {0}: {1}, {2}", voting.Id, voting.Title, voting.Status);
        }

        Console.Write("  Select one: ");
        int votingIndex = Convert.ToInt32(Console.ReadLine());
        var selectedVoting = votingList.ElementAt(votingIndex);

        Console.WriteLine();

        switch (selectedVoting.Status)
        {
          case VotingStatus.New:
          case VotingStatus.Deciphering:
          case VotingStatus.Aborted:
          case VotingStatus.Sharing:
            Console.WriteLine("You cant do anything with this voting at the moment.");
            break;
          case VotingStatus.Voting:
            Console.WriteLine("You can now vote.");
            Console.WriteLine(selectedVoting.Title);
            Console.WriteLine(selectedVoting.Description);

            List<IEnumerable<bool>> vota = new List<IEnumerable<bool>>();

            foreach (QuestionDescriptor question in selectedVoting.Questions)
            {
              Console.WriteLine(question.Text);

              foreach (var option in question.Options)
              {
                Console.WriteLine("  {0}, {1}", option.Text, option.Description);
              }

              Console.Write("  Select one: ");
              int optionIndex = Convert.ToInt32(Console.ReadLine());

              bool[] votaItem = new bool[question.Options.Count()];
              votaItem[optionIndex] = true;

              vota.Add(votaItem);
            }

            this.voterClient.Vote(selectedVoting.Id, vota, VoteCallBack);

            break;
          case VotingStatus.Finished:
            Console.WriteLine("Getting result now.");
            this.voterClient.GetResult(selectedVoting.Id, new List<Signed<VoteReceipt>>(), GetResult);
            break;
        }
      }
    }

    private void VoteCallBack(Signed<VoteReceipt> voteReceipt, Exception exception)
    {
      if (exception == null)
      {
        Console.WriteLine();
        Console.WriteLine("Vote is cast!");
      }
      else
      {
        Console.WriteLine(exception.ToString());
      }
    }

    private void GetResult(VotingResult result, IDictionary<Guid, VoteReceiptStatus> voteReceiptsStatus, Exception exception)
    {
      if (result == null)
      {
        Console.WriteLine(exception.ToString());
      }
      else
      {
        Console.WriteLine();
        Console.WriteLine(result.Title);
        Console.WriteLine(result.Description);
        Console.WriteLine("  Total ballots cast: {0}", result.TotalBallots);
        Console.WriteLine("  Invalid ballots cast: {0}", result.InvalidBallots);
        Console.WriteLine("  Valid ballots cast: {0}", result.ValidBallots);

        foreach (QuestionResult question in result.Questions)
        {
          Console.WriteLine(question.Text);

          foreach (var option in question.Options)
          {
            Console.WriteLine("    {0}: {1}", option.Text, option.Result);
          }
        }
      }
    }
  }
}