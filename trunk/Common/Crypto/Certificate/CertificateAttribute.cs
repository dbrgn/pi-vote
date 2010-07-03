using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Name of the certificate attribute.
  /// </summary>
  public enum CertificateAttributeName
  {
    None = 0,
    Canton = 1,
    Language = 2
  }

  /// <summary>
  /// Attribute of a certificate.
  /// </summary>
  public abstract class CertificateAttribute : Serializable
  {
    /// <summary>
    /// Name of the certificate attribute.
    /// </summary>
    public CertificateAttributeName Name { get; private set; }

    public CertificateAttribute(CertificateAttributeName name)
    {
      Name = name;
    }

    public CertificateAttribute(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write((int)Name);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Name = (CertificateAttributeName)context.ReadInt32();
    }

    public abstract CertificateAttribute Clone
    {
      get;
    }
  }

  /// <summary>
  /// Attribute of a certificate.
  /// </summary>
  /// <typeparam name="TValue">Type of the value.</typeparam>
  public abstract class CertificateAttribute<TValue> : CertificateAttribute
  {
    /// <summary>
    /// Value of the certificate attribute.
    /// </summary>
    public TValue Value { get; set; }

    public CertificateAttribute(CertificateAttributeName name, TValue value)
      : base(name)
    {
      Value = value;
    }

    public CertificateAttribute(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }
  }

  /// <summary>
  /// Integer attribute of certificate.
  /// </summary>
  public class Int32CertificateAttribute : CertificateAttribute<int>
  {
    public Int32CertificateAttribute(CertificateAttributeName name, int value)
      : base(name, value)
    { }

    public Int32CertificateAttribute(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Value);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Value = context.ReadInt32();
    }

    public override CertificateAttribute Clone
    {
      get { return new Int32CertificateAttribute(Name, Value); }
    }
  }

  /// <summary>
  /// Boolean attribute of certificate.
  /// </summary>
  public class BooleanCertificateAttribute : CertificateAttribute<bool>
  {
    public BooleanCertificateAttribute(CertificateAttributeName name, bool value)
      : base(name, value)
    { }

    public BooleanCertificateAttribute(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Value);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Value = context.ReadBoolean();
    }

    public override CertificateAttribute Clone
    {
      get { return new BooleanCertificateAttribute(Name, Value); }
    }
  }

  /// <summary>
  /// String attribute of certificate.
  /// </summary>
  public class StringCertificateAttribute : CertificateAttribute<string>
  {
    public StringCertificateAttribute(CertificateAttributeName name, string value)
      : base(name, value)
    { }

    public StringCertificateAttribute(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Value);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Value = context.ReadString();
    }

    public override CertificateAttribute Clone
    {
      get { return new StringCertificateAttribute(Name, Value); }
    }
  }
}
