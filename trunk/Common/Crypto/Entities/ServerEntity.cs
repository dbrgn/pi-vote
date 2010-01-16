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
  public class ServerEntity
  {
    private Dictionary<int, VotingServerEntity> votings;

    public ServerEntity()
    {
      this.votings = new Dictionary<int, VotingServerEntity>();
    }

    public void NewVote(Signed<VotingParameters> signedParameterContainer)
    { 
      //TODO perform authorization.

      VotingParameters parameterContainer = signedParameterContainer.Value;
      VotingServerEntity voting = new VotingServerEntity(parameterContainer);
      this.votings.Add(voting.Id, voting);
    }
  }
}
