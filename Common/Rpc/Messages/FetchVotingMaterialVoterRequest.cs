﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// RPC request to fetch voting material and status.
  /// May fetch material and status of one, some or all votings at once.
  /// </summary>
  [SerializeObject("RPC request to fetch voting material and status.")]
  [RpcRequest("Downloads voting materials and status for several votings at once.")]
  [RpcInput("List of ids of votings or null list for all votings.")]
  [RpcOutput("List of tuples containing voting material, voting status and a list of ids of the involved authorites.")]
  public class FetchVotingMaterialVoterRequest : RpcRequest<VotingRpcServer, FetchVotingMaterialVoterResponse>
  {
    /// <summary>
    /// List of ids of the votings to get.
    /// May be null in case all votings are requested.
    /// </summary>
    [SerializeField(0, "List of ids of the votings to get.")]
    private List<Guid> votingIds;

    /// <summary>
    /// Creates a new fetch voting material request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="votingIds">Ids of the requested voting or null to request all votings.</param>
    public FetchVotingMaterialVoterRequest(
      Guid requestId,
      IEnumerable<Guid> votingIds)
      : base(requestId)
    {
      if (votingIds == null)
      {
        this.votingIds = null;
      }
      else
      {
        this.votingIds = new List<Guid>(votingIds);
      }
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchVotingMaterialVoterRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);

      context.Write(this.votingIds != null);
      if (this.votingIds != null)
        context.WriteList(this.votingIds);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);

      if (context.ReadBoolean())
      {
        this.votingIds = context.ReadGuidList();
      }
      else
      {
        this.votingIds = null;
      }
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchVotingMaterialVoterResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      var votingMaterials = 
        (this.votingIds == null ? server.FetchVotingIds() : this.votingIds)
        .Select(votingId => server.GetVoting(votingId))
        .Select(voting => new Tuple<VotingMaterial, VotingStatus, List<Guid>>(voting.GetVotingMaterial(), voting.Status, voting.AuthoritiesDone));

      return new FetchVotingMaterialVoterResponse(RequestId, votingMaterials);
    }
  }
}
