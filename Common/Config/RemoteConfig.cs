/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote
{
  /// <summary>
  /// Config file for the voting server.
  /// </summary>
  [SerializeObject("Config file for the voting server.")]
  public class RemoteConfig : Serializable, IRemoteConfig
  {
    public RemoteConfig(IRemoteConfig config)
      : base()
    {
      SystemName = config.SystemName;
      WelcomeMessage = config.WelcomeMessage;
      Image = config.Image;
      Url = config.Url;
      UpdateVersion = config.UpdateVersion;
      UpdateUrl = config.UpdateUrl;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public RemoteConfig(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(SystemName);
      context.Write(WelcomeMessage);
      context.Write(Image);
      context.Write(Url);
      context.Write(UpdateVersion);
      context.Write(UpdateUrl);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      SystemName = context.ReadMultiLanguageString();
      WelcomeMessage = context.ReadMultiLanguageString();
      Image = context.ReadBytes();
      Url = context.ReadString();
      UpdateVersion = context.ReadString();
      UpdateUrl = context.ReadString();
    }
    
    /// <summary>
    /// Name of the eVoting system.
    /// </summary>
    [SerializeField(0, "Name of the eVoting system.")]
    public MultiLanguageString SystemName { get; private set; }

    /// <summary>
    /// Welcome message to users.
    /// </summary>
    [SerializeField(1, "Welcome message to users.")]
    public MultiLanguageString WelcomeMessage { get; private set; }

    /// <summary>
    /// Image file on the start wizard item.
    /// </summary>
    [SerializeField(2, "Image file on the start wizard item.")]
    public byte[] Image { get; private set; }

    /// <summary>
    /// Url of the project.
    /// </summary>
    [SerializeField(3, "Url of the project.")]
    public string Url { get; private set; }

    /// <summary>
    /// The newest available version one could update to.
    /// </summary>
    [SerializeField(4, "The newest available version one could update to.")]
    public string UpdateVersion { get; private set; }

    /// <summary>
    /// Url were one can get the update.
    /// </summary>
    [SerializeField(5, "Url were one can get the update.")]
    public string UpdateUrl { get; private set; }
  }
}
