using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Cli
{
  public class ListVotingsCommend : Command
  {
    public ListVotingsCommend(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      Begin("Fetching voting list...");
      Status.Client.GetVotingList(Status.CertificateStorage, Status.DataPath, GetVotingListCompleted);
      WaitForCompletion();
    }

    private void GetVotingListCompleted(IEnumerable<VotingDescriptor> votingList, Exception exception)
    {
      Complete(exception);
      
      if (exception == null)
      {
        StringTable table = new StringTable();
        table.AddColumn("Id");
        table.AddColumn("Title");
        table.AddColumn("Status");
        table.AddColumn("From");
        table.AddColumn("Until");

        foreach (var voting in votingList)
        {
          table.AddRow(voting.Id.ToString(), voting.Title.Text, voting.Status.Text(), voting.VoteFrom.ToShortDateString(), voting.VoteUntil.ToShortDateString());
        }

        Console.WriteLine(table.Render());
      }

      Ready();
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "list";
      }
    }

    public override string HelpText
    {
      get { return "Lists all votings."; }
    }
  }
}
