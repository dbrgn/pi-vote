/*
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
  /// RPC request creates a new voting.
  /// </summary>
  [SerializeObject("RPC request creates a new voting.")]
  public class DeleteVotingRequest : RpcRequest<VotingRpcServer, DeleteVotingResponse>
  {
    /// <summary>
    /// Command to delete a voting.
    /// </summary>
    [SerializeObject("Command to delete a voting.")]
    public class Command : Serializable
    {
      [SerializeField(0, "Id of the voting to delete.")]
      public Guid VotingId { get; private set; }

      public Command(Guid votingId)
      {
        VotingId = votingId;
      }

      public Command(DeserializeContext context, byte version)
        : base(context, version)
      { }

      public override void Serialize(SerializeContext context)
      {
        base.Serialize(context);
        context.Write(VotingId);
      }

      protected override void Deserialize(DeserializeContext context, byte version)
      {
        base.Deserialize(context, version);
        VotingId = context.ReadGuid();
      }
    }

    /// <summary>
    /// Signed command to delete the voting.
    /// </summary>
    [SerializeField(0, "Signed command to delete the voting.")]
    private Signed<Command> command;

    /// <summary>
    /// Creates a new voting creation request.
    /// </summary>
    /// <param name="requestId">Id of this request.</param>
    /// <param name="votingParameters">Parameters for the new voting.</param>
    /// <param name="authorities">List of authorities to oversee the voting.</param>
    public DeleteVotingRequest(
      Guid requestId,
      Signed<Command> command)
      : base(requestId)
    {
      this.command = command;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public DeleteVotingRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(command);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.command = context.ReadObject<Signed<Command>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override DeleteVotingResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      bool valid = true;

      valid &= this.command.Verify(server.CertificateStorage);
      valid &= this.command.Certificate is AdminCertificate;

      if (valid)
      {
        server.DeleteVoting(connection, this.command.Value.VotingId);

        return new DeleteVotingResponse(RequestId);
      }
      else
      {
        throw new PiException(ExceptionCode.CommandNotFromAdmin, "Command is not signed by an admin.");
      }
    }
  }
}
