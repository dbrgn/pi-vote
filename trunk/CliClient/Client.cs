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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.CliClient.VoteService;

namespace Pirate.PiVote.CliClient
{
  public class Client
  {
    private VoteServiceSoapClient service;
    private bool run;
    private CertificateStorage certs;
    private List<AuthorityCertificate> auths;
    private List<VoterCertificate> voters;
    private CACertificate root;
    private CACertificate intermediate;
    private AdminCertificate admin;
    
    public Client()
    {
      this.service = new VoteServiceSoapClient();

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

    private void Do()
    {
      this.run = true;

      while (this.run)
      {
        Console.Write("Enter command: ");
        string command = Console.ReadLine().ToLower();

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
      AdminRpcProxy proxy = new AdminRpcProxy(this.service, this.admin);
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
      AuthorityRpcProxy proxy = new AuthorityRpcProxy(this.service, cert);
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
        Thread.Sleep(1);
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
        Thread.Sleep(1);
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
        Thread.Sleep(1);
      }
      Console.WriteLine("Done");

      Console.Write("Getting ballots...");
      var ballots = proxy.FetchEnvelopes(votingId);
      Console.WriteLine("Done");

      Console.Write("Calculating partial decipher...");
      var pds = auth.PartiallyDecipher(ballots);
      Console.WriteLine("Done");

      Console.Write("Pushing partial deciphers...");
      proxy.PushPartailDecipher(votingId, pds);
      Console.WriteLine("Done");

      Console.WriteLine("Voting completed.");
      Console.ReadLine();
    }


    private void VoterMode()
    {
      Console.WriteLine("VoterMode Mode");

      Console.Write("Enter voter index 0..9: ");
      int voterIndex = Convert.ToInt32(Console.ReadLine());

      var proxy = new VoterRpcProxy(new VoteServiceSoapClient(), this.voters[voterIndex]);
      int votingId = 1;

      Console.Write("Getting voting material...");
      var vm = proxy.FetchVotingMaterial(votingId);
      Console.WriteLine("Done");

      var voter = new VoterEntity(voterIndex + 1, (CACertificate)this.root.OnlyPublicPart, this.voters[voterIndex]);

      Console.WriteLine("Vote now!");
      Console.WriteLine(vm.Parameters.VotingName);

      int index = 0;
      foreach (Option option in vm.Parameters.Options)
      {
        Console.WriteLine("{0}: {1} / {2}", index, option.Text, option.Description);
        index++;
      }

      Console.Write("Enter vote: ");
      int optionIndex = Convert.ToInt32(Console.ReadLine());
      int[] vota = new int[vm.Parameters.OptionCount];
      vota[optionIndex] = 1;

      Console.Write("Computing envelope...");
      var envelope = voter.Vote(vm, vota);
      Console.WriteLine("Done");

      Console.Write("Pusihing envelope...");
      proxy.PushEnvelope(votingId, envelope);
      Console.WriteLine("Done");

      Console.WriteLine("Vote is cast!");

      Console.Write("Waiting for voters...");
      while (proxy.FetchVotingStatus(votingId) == VotingStatus.Voting)
      {
        Thread.Sleep(1);
      }
      Console.WriteLine("Done");

      Console.Write("Waiting for deciphering...");
      while (proxy.FetchVotingStatus(votingId) == VotingStatus.Deciphering)
      {
        Thread.Sleep(1);
      }
      Console.WriteLine("Done");

      Console.Write("Fetching result...");
      var container = proxy.FetchtVotingResult(votingId);
      Console.WriteLine("Done");

      Console.Write("Verifying result...");
      var result = voter.Result(container);
      Console.WriteLine("Done");

      Console.WriteLine("Result is:");
      Console.WriteLine("Cast votes {0}.", result.TotalBallots);
      Console.WriteLine("Invalid votes {0}.", result.InvalidBallots);
      foreach (OptionResult option in result.Options)
      {
        Console.WriteLine("{0} / {1} : {2}", option.Text, option.Description, option.Result);
      }

      Console.WriteLine();
    }
  }
}