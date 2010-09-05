/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
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
    public RemoteConfig(DeserializeContext context)
      : base(context)
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
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
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
    public MultiLanguageString SystemName { get; private set; }

    /// <summary>
    /// Welcome message to users.
    /// </summary>
    public MultiLanguageString WelcomeMessage { get; private set; }

    /// <summary>
    /// Image file on the start wizard item.
    /// </summary>
    public byte[] Image { get; private set; }

    /// <summary>
    /// Url of the project.
    /// </summary>
    public string Url { get; private set; }

    /// <summary>
    /// The newest available version one could update to.
    /// </summary>
    public string UpdateVersion { get; private set; }

    /// <summary>
    /// Url were one can get the update.
    /// </summary>
    public string UpdateUrl { get; private set; }
  }
}
