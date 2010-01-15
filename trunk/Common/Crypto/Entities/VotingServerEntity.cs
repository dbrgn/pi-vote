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

namespace Pirate.PiVote.Crypto
{
  public enum VotingStatus
  {
    New,
    Sharing,
    Voting,
    Aborted,
    Deciphering,
    Finished,
  }

  public class VotingServerEntity
  {
    private ParameterContainer parameters;
    private Dictionary<int, Certificate> authorities;
    private Dictionary<int, SignedContainer<ShareContainer>> shares;
    private Dictionary<int, SignedContainer<ShareResponse>> responses;
    private List<SignedContainer<BallotContainer>> ballots;
    private Dictionary<int, SignedContainer<PartialDeciphersContainer>> partialDeciphers;

    public VotingStatus Status { get; set; }

    public int Id
    {
      get { return this.parameters.VotingId; }
    }

    public VotingServerEntity(ParameterContainer parameters)
    {
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      this.parameters = parameters;
      this.authorities = new Dictionary<int, Certificate>();
      this.shares = new Dictionary<int, SignedContainer<ShareContainer>>();
      this.responses = new Dictionary<int, SignedContainer<ShareResponse>>();
      this.ballots = new List<SignedContainer<BallotContainer>>();
      this.partialDeciphers = new Dictionary<int, SignedContainer<PartialDeciphersContainer>>();
      Status = VotingStatus.New;
    }

    public ParameterContainer Parameters { get { return this.parameters; } }

    public int AddAuthority(Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException("certificate");
      if (this.authorities.Count >= this.parameters.Parameters.AuthorityCount)
        throw new InvalidOperationException("Already enough authorities.");

      int authorityIndex = this.authorities.Count + 1;
      this.authorities.Add(authorityIndex, certificate);

      return authorityIndex;
    }

    public AuthorityList Authorities
    {
      get { return new AuthorityList(Id, this.authorities.Values); }
    }

    public void DepositShares(SignedContainer<ShareContainer> shares)
    {
      if (shares == null)
        throw new ArgumentNullException("shares");
      if (Status != VotingStatus.New)
        throw new InvalidOperationException("Wrong status for operation.");

      ShareContainer shareContainer = shares.Value;

      if (!this.authorities.ContainsKey(shareContainer.AuthorityIndex))
        throw new ArgumentException("Bad authority index.");

      if (!shares.Verify())
        throw new ArgumentException("Bad signature.");

      if (!shares.Certificate.IsIdentic(this.authorities[shareContainer.AuthorityIndex]))
        throw new ArgumentException("Not signed by proper authority.");

      if (this.shares.ContainsKey(shareContainer.AuthorityIndex))
        throw new ArgumentException("Authority has already deposited shares.");

      this.shares.Add(shareContainer.AuthorityIndex, shares);

      if (this.shares.Count == this.parameters.Parameters.AuthorityCount)
      {
        Status = VotingStatus.Sharing;
      }
    }

    public AllSharesContainer GetAllShares()
    {
      if (Status != VotingStatus.Sharing)
        throw new InvalidOperationException("Wrong status for operation.");

      return new AllSharesContainer(Id, this.shares.Values);
    }

    public void DepositShareResponse(SignedContainer<ShareResponse> response)
    {
      if (response == null)
        throw new ArgumentNullException("shares");
      if (Status != VotingStatus.Sharing)
        throw new InvalidOperationException("Wrong status for operation.");

      ShareResponse shareResponse = response.Value;

      if (!this.authorities.ContainsKey(shareResponse.AuthorityIndex))
        throw new ArgumentException("Bad authority index.");

      if (!response.Verify())
        throw new ArgumentException("Bad signature.");

      if (!response.Certificate.IsIdentic(this.authorities[shareResponse.AuthorityIndex]))
        throw new ArgumentException("Not signed by proper authority.");

      if (this.responses.ContainsKey(shareResponse.AuthorityIndex))
        throw new ArgumentException("Authority has already deposited shares.");

      this.responses.Add(shareResponse.AuthorityIndex, response);

      if (this.responses.Count == this.parameters.Parameters.AuthorityCount)
      {
        if (this.responses.Values.All(r => r.Value.AcceptShares))
        {
          Status = VotingStatus.Voting;
        }
        else
        {
          Status = VotingStatus.Aborted;
        }
      }
    }

    public VotingMaterial GetVotingMaterial()
    {
      if (Status != VotingStatus.Voting)
        throw new InvalidOperationException("Wrong status for operation.");

      return new VotingMaterial(Id, this.parameters, this.responses.Values);
    }

    public void Vote(SignedContainer<BallotContainer> ballot)
    {
      if (Status != VotingStatus.Voting)
        throw new InvalidOperationException("Wrong status for operation.");
      if (!ballot.Verify())
        throw new ArgumentException("Bad signature.");
      if (!CanVote(ballot.Certificate))
        throw new ArgumentException("Not allowed to vote.");

      this.ballots.Add(ballot);
    }

    private bool CanVote(Certificate certificate)
    {
      return true;
    }

    public void EndVote()
    {
      if (Status != VotingStatus.Voting)
        throw new InvalidOperationException("Wrong status for operation.");

      Status = VotingStatus.Deciphering;
    }

    public AllBallotsContainer GetAllBallots()
    {
      if (Status != VotingStatus.Deciphering)
        throw new InvalidOperationException("Wrong status for operation.");

      return new AllBallotsContainer(Id, this.ballots, new VotingMaterial(Id, this.parameters, this.responses.Values));
    }

    public void DepositPartialDecipher(SignedContainer<PartialDeciphersContainer> signedPartialDecipherContainer)
    {
      if (signedPartialDecipherContainer == null)
        throw new ArgumentNullException("partialDecipherContainer");
      if (Status != VotingStatus.Deciphering)
        throw new InvalidOperationException("Wrong status for operation.");

      PartialDeciphersContainer partialDecipherContainer = signedPartialDecipherContainer.Value;

      if (!this.authorities.ContainsKey(partialDecipherContainer.AuthorityIndex))
        throw new ArgumentException("Bad authority index.");

      if (!signedPartialDecipherContainer.Verify())
        throw new ArgumentException("Bad signature.");

      if (!signedPartialDecipherContainer.Certificate.IsIdentic(this.authorities[partialDecipherContainer.AuthorityIndex]))
        throw new ArgumentException("Not signed by proper authority.");

      if (this.partialDeciphers.ContainsKey(partialDecipherContainer.AuthorityIndex))
        throw new ArgumentException("Authority has already deposited shares.");

      this.partialDeciphers.Add(partialDecipherContainer.AuthorityIndex, signedPartialDecipherContainer);

      if (this.partialDeciphers.Count == this.parameters.Parameters.AuthorityCount)
      {
        Status = VotingStatus.Finished;
      }
    }

    public VotingContainer GetVotingResult()
    {
      if (Status != VotingStatus.Finished)
        throw new InvalidOperationException("Wrong status for operation.");

      return new VotingContainer(Id, this.ballots, this.partialDeciphers.Values);
    }
  }
}
