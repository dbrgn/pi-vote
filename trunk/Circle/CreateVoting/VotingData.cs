/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Circle.CreateVoting
{
  public class VotingData : Serializable
  {
    public const string VotingDataFileName = "saved.pi-voting-data";

    public MultiLanguageString Title { get; set; }

    public MultiLanguageString Descrption { get; set; }

    public MultiLanguageString Url { get; set; }

    public List<Question> Questions { get; private set; }

    public VotingData(MultiLanguageString title, MultiLanguageString description, MultiLanguageString url)
    {
      Title = title;
      Descrption = description;
      Url = url;
      Questions = new List<Question>();
    }

    public VotingData(DeserializeContext context, byte version)
      : base(context, version)
    { 
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Title);
      context.Write(Descrption);
      context.Write(Url);
      context.WriteList(Questions);
    }

    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      Title = context.ReadMultiLanguageString();
      Descrption = context.ReadMultiLanguageString();
      Url = context.ReadMultiLanguageString();
      Questions = context.ReadObjectList<Question>();
    }

    public void SaveTo(string dataPath)
    {
      string fileName = Path.Combine(dataPath, VotingDataFileName);

      Save(fileName);
    }

    public static VotingData TryLoad(string dataPath)
    { 
      string fileName = Path.Combine(dataPath, VotingDataFileName);

      try
      {
        if (File.Exists(fileName))
        {
          return Serializable.Load<VotingData>(fileName);
        }
        else
        {
          return null;
        }
      }
      catch
      {
        return null;
      }
    }
  }
}
