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
    private VoterClient voterClient;
    
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
      this.certs.SetRevocationList(rootCrl);
      var intCrl = new Signed<RevocationList>(new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddYears(1), new List<Guid>()), intermediate);
      this.certs.SetRevocationList(intCrl);

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

      Console.Write("Enter voting name: ");
      string votingName = Console.ReadLine();

      VotingParameters votingParameters = new VotingParameters(votingName);

      Console.Write("Enter option name: ");
      string optionName = Console.ReadLine();
      while (!optionName.IsNullOrEmpty())
      {
        votingParameters.AddOption(new Option(optionName, string.Empty));
        Console.Write("Enter option name: ");
        optionName = Console.ReadLine();
      }

      Console.Write("Initializing crypto...");
      votingParameters.Initialize(1);
      Console.WriteLine("Done");

      Console.Write("Requesting server to create voting procedure...");
      AdminRpcProxy proxy = new AdminRpcProxy(this.client, this.admin);
      int votingId = proxy.CreateVoting(votingParameters, this.auths.Select(auth => (AuthorityCertificate)auth.OnlyPublicPart));
      Console.WriteLine("Done");
      Console.WriteLine("Voting id is {0}.", votingId);

      Console.Write("Hit enter to end voting: ");
      Console.ReadLine();

      Console.Write("Ending voting...");
      proxy.EndVoting(votingId);
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

      int votingId = 1;
      AuthorityCertificate cert = this.auths[index];
      AuthorityEntity auth = new AuthorityEntity((CACertificate)this.root.OnlyPublicPart, cert);

      Console.Write("Fetching parameters...");
      AuthorityRpcProxy proxy = new AuthorityRpcProxy(this.client, cert);
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
      while (proxy.FetchVotingStatus(votingId) != VotingStatus.Sharing)
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
      while (proxy.FetchVotingStatus(votingId) == VotingStatus.Sharing)
      {
        Thread.Sleep(1000);
      }
      Console.WriteLine("Done");

      if (proxy.FetchVotingStatus(votingId) != VotingStatus.Voting)
      {
        Console.WriteLine("Voting aborted.");
        Console.WriteLine();
        return;
      }
      
      Console.WriteLine("Voting begun.");

      Console.Write("Waiting for voters...");
      while (proxy.FetchVotingStatus(votingId) != VotingStatus.Deciphering)
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
        auth.TallyAdd(proxy.FetchEnvelope(votingId, envelopeIndex));
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

      this.voterClient = new VoterClient((CACertificate)this.root.OnlyPublicPart, this.voters[voterIndex]);

      this.voterClient.Connect(IPAddress.Loopback, Connected);
    }

    private void Connected(Exception exception)
    {
      if (exception == null)
      {
        Console.WriteLine();
        Console.WriteLine("Connected to voting server.");

        this.voterClient.GetVotingList(VotingList);
      }
      else
      {
        Console.WriteLine(exception.ToString());
      }
    }

    private void VotingList(IEnumerable<VoterClient.VotingDescriptor> votingList, Exception exception)
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
          Console.WriteLine("  {0}: {1}, {2}", voting.Id, voting.Name, voting.Status);
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
            Console.WriteLine(selectedVoting.Name);

            foreach (var option in selectedVoting.Options)
            {
              Console.WriteLine("  {0}, {1}", option.Text, option.Description);
            }

            Console.Write("  Select one: ");
            int optionIndex = Convert.ToInt32(Console.ReadLine());
            this.voterClient.Vote(selectedVoting.Id, optionIndex, VoteCallBack);

            break;
          case VotingStatus.Finished:
            Console.WriteLine("Getting result now.");
            this.voterClient.GetResult(selectedVoting.Id, GetResult);
            break;
        }
      }
    }

    private void VoteCallBack(Exception exception)
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

    private void GetResult(VotingResult result, Exception exception)
    {
      if (result == null)
      {
        Console.WriteLine(exception.ToString());
      }
      else
      {
        Console.WriteLine();
        Console.WriteLine(result.VotingName);
        Console.WriteLine("  Total ballots cast: {0}", result.TotalBallots);
        Console.WriteLine("  Invalid ballots cast: {0}", result.InvalidBallots);
        Console.WriteLine("  Valid ballots cast: {0}", result.ValidBallots);

        foreach (var option in result.Options)
        {
          Console.WriteLine("    {0}: {1}", option.Text, option.Result);
        }
      }
    }
  }
}