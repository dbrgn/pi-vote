
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
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// RPC request creates a new voting.
  /// </summary>
  public class DeleteVotingRequest : RpcRequest<VotingRpcServer, CreateVotingResponse>
  {
    /// <summary>
    /// Command to delete a voting.
    /// </summary>
    public class Command : Serializable
    {
      public Guid VotingId { get; private set; }

      public Command(Guid votingId)
      {
        VotingId = votingId;
      }

      public Command(DeserializeContext context)
        : base(context)
      { }

      public override void Serialize(SerializeContext context)
      {
        base.Serialize(context);
        context.Write(VotingId);
      }

      protected override void Deserialize(DeserializeContext context)
      {
        base.Deserialize(context);
        VotingId = context.ReadGuid();
      }
    }

    /// <summary>
    /// Signed command to delete the voting.
    /// </summary>
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
    public DeleteVotingRequest(DeserializeContext context)
      : base(context)
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
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.command = context.ReadObject<Signed<Command>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override CreateVotingResponse Execute(VotingRpcServer server)
    {
      bool valid = true;

      valid &= this.command.Verify(server.CertificateStorage);
      valid &= this.command.Certificate is AdminCertificate;

      if (valid)
      {
        server.DeleteVoting(this.command.Value.VotingId);

        return new CreateVotingResponse(RequestId);
      }
      else
      {
        throw new PiException(ExceptionCode.CommandNotFromAdmin, "Command is not signed by an admin.");
      }
    }
  }
}
