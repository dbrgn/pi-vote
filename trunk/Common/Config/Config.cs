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

namespace Pirate.PiVote
{
  /// <summary>
  /// XML Config file.
  /// </summary>
  public abstract class Config
  {
    /// <summary>
    /// Full name and path of the file.
    /// </summary>
    private string fileName;

    /// <summary>
    /// XML document structure.
    /// </summary>
    private XDocument document;

    /// <summary>
    /// Format string for dates and times.
    /// </summary>
    private const string DateTimeFormatString = "YYYYMdd-hhmmss";

    /// <summary>
    /// Loads or creates a XML config file.
    /// </summary>
    /// <param name="fileName">Full file name and path.</param>
    public Config(string fileName)
    {
      this.fileName = fileName;

      if (File.Exists(this.fileName))
      {
        this.document = XDocument.Load(this.fileName);
      }
      else
      {
        this.document = new XDocument();
      }

      Validate();
    }

    /// <summary>
    /// Root for all config entries.
    /// </summary>
    private XElement ConfigRoot
    {
      get
      {
        if (this.document.Elements("Config").Count() < 1)
        {
          this.document.Add(new XElement("Config"));
        }

        return this.document.Element("Config");
      }
    }

    /// <summary>
    /// Read a string from the XML.
    /// </summary>
    /// <param name="name">Name of the config entry.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <returns>Saved or default value.</returns>
    protected string ReadString(string name, string defaultValue)
    {
      if (ConfigRoot.Elements(name).Count() > 0)
      {
        return ConfigRoot.Element(name).Value;
      }
      else
      {
        ConfigRoot.Add(new XElement(name, defaultValue));
        return defaultValue;
      }
    }

    protected int ReadInt32(string name, int defaultValue)
    {
      return Convert.ToInt32(ReadString(name, defaultValue.ToString()));
    }

    protected long ReadInt64(string name, long defaultValue)
    {
      return Convert.ToInt64(ReadString(name, defaultValue.ToString()));
    }

    protected DateTime ReadDateTime(string name, DateTime defaultValue)
    {
      return DateTime.ParseExact(ReadString(name, defaultValue.ToString(DateTimeFormatString)), DateTimeFormatString, CultureInfo.InvariantCulture);
    }

    protected Guid ReadGuid(string name, Guid defaultValue)
    {
      return new Guid(ReadString(name, defaultValue.ToString()));
    }

    protected float ReadSingle(string name, float defaultValue)
    {
      return Convert.ToSingle(ReadString(name, defaultValue.ToString()));
    }

    protected double ReadDouble(string name, double defaultValue)
    {
      return Convert.ToDouble(ReadString(name, defaultValue.ToString()));
    }

    protected bool ReadBoolean(string name, bool defaultValue)
    {
      return Convert.ToBoolean(ReadString(name, defaultValue.ToString()));
    }

    protected byte[] ReadBytes(string name, byte[] defaultValue)
    {
      return Convert.FromBase64String(ReadString(name, Convert.ToBase64String(defaultValue)));
    }

    /// <summary>
    /// Write a string to the config XML.
    /// </summary>
    /// <param name="name">Name of the config entry.</param>
    /// <param name="value">Value to save.</param>
    protected void Write(string name, string value)
    {
      if (ConfigRoot.Elements(name).Count() > 0)
      {
        ConfigRoot.Element(name).Value = value;
      }
      else
      {
        ConfigRoot.Add(new XElement(name, value));
      }
    }

    protected void Write(string name, int value)
    {
      Write(name, value.ToString());
    }

    protected void Write(string name, long value)
    {
      Write(name, value.ToString());
    }

    protected void Write(string name, DateTime value)
    {
      Write(name, value.ToString(DateTimeFormatString));
    }

    protected void Write(string name, Guid value)
    {
      Write(name, value.ToString());
    }

    protected void Write(string name, float value)
    {
      Write(name, value.ToString());
    }

    protected void Write(string name, double value)
    {
      Write(name, value.ToString());
    }

    protected void Write(string name, bool value)
    {
      Write(name, value.ToString());
    }

    protected void Write(string name, byte[] value)
    {
      Write(name, Convert.ToBase64String(value));
    }
    
    /// <summary>
    /// Saves the config to an XML file.
    /// </summary>
    public void Save()
    {
      document.Save(fileName);
    }

    /// <summary>
    /// Validates the config data.
    /// </summary>
    /// <remarks>
    /// Throws exceptions when bad data is found.
    /// </remarks>
    protected abstract void Validate();
  }
}
