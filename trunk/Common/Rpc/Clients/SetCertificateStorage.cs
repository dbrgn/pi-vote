
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
using System.IO;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Asynchronous client.
  /// </summary>
  public partial class VotingClient
  {
    /// <summary>
    /// Callback for the certificate storage set operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void SetCertificateStorageCallBack(Exception exception);

    /// <summary>
    /// Certificate storage set operation.
    /// </summary>
    private class SetCertificateStorageOperation : Operation
    {
      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private SetCertificateStorageCallBack callBack;

      /// <summary>
      /// Certificate storage to add to the server's data.
      /// </summary>
      private CertificateStorage certificateStorage;

      /// <summary>
      /// Create a new signature responses set operation.
      /// </summary>
      /// <param name="certificateStorage">Certificate storage to add to the server's data.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public SetCertificateStorageOperation(CertificateStorage certificateStorage, SetCertificateStorageCallBack callBack)
      {
        this.certificateStorage = certificateStorage;
        this.callBack = callBack;
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voter client to execute against.</param>
      public override void Execute(VotingClient client)
      {
        try
        {
          Text = LibraryResources.ClientSetCertificateStorage;
          Progress = 0d;

          client.proxy.PushCertificateStorage(this.certificateStorage);
          
          Text = LibraryResources.ClientSetCertificateStorage;
          Progress = 1d;

          this.callBack(null);
        }
        catch (Exception exception)
        {
          this.callBack(exception);
        }
      }
    }
  }
}
