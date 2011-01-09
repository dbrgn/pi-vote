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
  [SerializeEnum("Name of the certificate attribute.")]
  public enum CertificateAttributeName
  {
    None = 0,
    GroupId = 1,
    Language = 2
  }

  /// <summary>
  /// Attribute of a certificate.
  /// </summary>
  [SerializeObject("Attribute of a certificate.")]
  public abstract class CertificateAttribute : Serializable
  {
    /// <summary>
    /// Name of the certificate attribute.
    /// </summary>
    [SerializeField(0, "Name of the certificate attribute.")]
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
  [SerializeObject("Attribute of a certificate.")]
  [GenericArgument(0, "TValue", "Type of the attribute value.")]
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
  [SerializeObject("Integer attribute of certificate.")]
  [SerializeAdditionalField(typeof(int), "Value", 0, "Value of the certificate attribute.")]
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
  [SerializeObject("Boolean attribute of certificate.")]
  [SerializeAdditionalField(typeof(bool), "Value", 0, "Value of the certificate attribute.")]
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
  [SerializeObject("String attribute of certificate.")]
  [SerializeAdditionalField(typeof(string), "Value", 0, "Value of the certificate attribute.")]
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
