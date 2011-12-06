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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Kiosk
{
  public enum ClientControllerState
  {
    Start,
    GotCertificateStorage,
    GotServerCertificate,
    GotUserData,
    GotUserInput,
    Done
  }

  public class ClientController
  {
    private Client client;

    private PeriodicTask task;

    public bool Faulted { get { return this.task.State == PeriodicTaskState.Faulted; } }

    public Exception Fault { get { return this.task.Fault; } }

    public CertificateStorage CertificateStorage { get; private set; }

    public Certificate ServerCertificate { get; private set; }

    public SignatureRequest UserData { get; private set; }

    public RequestContainer RequestContainer { get; set; }

    public ClientControllerState State { get; private set; }

    public ClientController()
    {
      State = ClientControllerState.Start;

      this.client = new Client();

      this.task = new PeriodicTask(null, Work, null, null);
      this.task.Delay = 1;
      this.task.Start();
    }

    private void Work()
    {
      switch (State)
      {
        case ClientControllerState.Start:
          CertificateStorage = this.client.FetchCertificateStroage();

          if (CertificateStorage != null)
          {
            State = ClientControllerState.GotCertificateStorage;
          }

          break;
        case ClientControllerState.GotCertificateStorage:
          ServerCertificate = this.client.FetchServerCertificate();

          if (ServerCertificate != null)
          {
            State = ClientControllerState.GotServerCertificate;
          }
          
          break;
        case ClientControllerState.GotServerCertificate:
          UserData = this.client.FetchUserData();

          if (UserData != null)
          {
            State = ClientControllerState.GotUserData;
          }
          
          break;
        case ClientControllerState.GotUserData:
          // Pull user data in case it has changed
          var userData = this.client.FetchUserData();

          if (userData != null)
          {
            UserData = userData;
          }

          // Wait for user data and do nothing
          if (RequestContainer != null)
          {
            State = ClientControllerState.GotUserInput;
          }

          break;
        case ClientControllerState.GotUserInput:

          if (this.client.PushSignaturRequest(RequestContainer))
          {
            UserData = null;
            RequestContainer = null;
            State = ClientControllerState.Done;
          }

          break;
        case ClientControllerState.Done:
          // Wait for reboot and do nothing
          break;
        default:
          throw new InvalidOperationException("Unknown client controller state.");
      }
    }
  }
}
