using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.CaGui
{
  public class ListEntry
  {
    public ListViewItem Item { get; private set; }

    public string FileName { get; private set; }

    private CertificateAuthorityEntry entry;

    private Certificate certificate;

    private SignatureRequest request;

    private SignatureResponse response;

    public CertificateAuthorityEntry Entry { get { return this.entry; } }

    public Certificate Certificate { get { return this.certificate; } }

    public SignatureRequest Request { get { return this.request; } }

    public bool Revoked { get { return this.entry.Revoked; } }

    public ListEntry(string fileName, CACertificate caCertificate)
    {
      FileName = fileName;
      this.entry = Serializable.Load<CertificateAuthorityEntry>(FileName);

      this.certificate = this.entry.Request.Certificate;
      this.request = this.entry.RequestValue(caCertificate);

      if (this.entry.Response != null)
      {
        this.response = this.entry.Response.Value;
      }
    }

    public ListEntry(string fileName, CertificateAuthorityEntry entry, CACertificate caCertificate)
    {
      FileName = fileName;
      this.entry = entry;

      this.certificate = this.entry.Request.Certificate;
      this.request = this.entry.RequestValue(caCertificate);

      if (this.entry.Response != null)
      {
        this.response = this.entry.Response.Value;
      }
    }

    public void UpdateItem(CACertificate caCertificate)
    {
      Item.Text = this.certificate.Id.ToString();
      Item.SubItems[1].Text = this.certificate.ToType().Text();

      if (this.certificate is VoterCertificate)
      {
        Item.SubItems[2].Text = GroupList.GetGroupName(((VoterCertificate)this.certificate).GroupId);
      }
      else
      {
        Item.SubItems[2].Text = string.Empty;
      }

      if (request.FamilyName.IsNullOrEmpty())
      {
        Item.SubItems[3].Text = request.FirstName;
      }
      else
      {
        Item.SubItems[3].Text = string.Format("{0}, {1}", request.FamilyName, request.FirstName);
      }

      switch (Status)
      {
        case CertificateStatus.Valid:
        case CertificateStatus.Revoked:
          SignatureResponse response = this.entry.Response.Value;
          Item.SubItems[4].Text = response.Signature.ValidFrom.ToString();
          Item.SubItems[5].Text = response.Signature.ValidUntil.ToString();
          break;
        case CertificateStatus.Refused:
          Item.SubItems[4].Text = "N/A";
          Item.SubItems[5].Text = "N/A";
          break;
        default:
          Item.SubItems[4].Text = string.Empty;
          Item.SubItems[5].Text = string.Empty;
          break;
      }

      Item.SubItems[6].Text = Status.Text();
    }

    public CertificateStatus Status
    {
      get
      {
        if (this.response == null)
        {
          return CertificateStatus.New;
        }
        else
        {
          switch (this.response.Status)
          {
            case SignatureResponseStatus.Accepted:
              if (this.entry.Revoked)
              {
                return CertificateStatus.Revoked;
              }
              else
              {
                return CertificateStatus.Valid;
              }
            case SignatureResponseStatus.Declined:
              return CertificateStatus.Refused;
            default:
              return CertificateStatus.None;
          }
        }
      }
    }

    public ListViewItem CreateItem(CACertificate caCertificate)
    {
      Item = new ListViewItem(this.Certificate.Id.ToString());

      Item.SubItems.Add(this.certificate.ToType().Text());

      if (this.certificate is VoterCertificate)
      {
        Item.SubItems.Add(GroupList.GetGroupName(((VoterCertificate)this.certificate).GroupId));
      }
      else
      {
        Item.SubItems.Add(string.Empty);
      }

      if (request.FamilyName.IsNullOrEmpty())
      {
        Item.SubItems.Add(request.FirstName);
      }
      else
      {
        Item.SubItems.Add(string.Format("{0}, {1}", request.FamilyName, request.FirstName));
      }

      if (this.response == null)
      {
        Item.SubItems.Add(string.Empty);
        Item.SubItems.Add(string.Empty);
        Item.SubItems.Add("New");
      }
      else
      {
        switch (this.response.Status)
        {
          case SignatureResponseStatus.Accepted:
            Item.SubItems.Add(response.Signature.ValidFrom.ToString());
            Item.SubItems.Add(response.Signature.ValidUntil.ToString());

            if (this.entry.Revoked)
            {
              Item.SubItems.Add("Revoked");
            }
            else
            {
              Item.SubItems.Add("Valid");
            }
            break;
          case SignatureResponseStatus.Declined:
            Item.SubItems.Add("N/A");
            Item.SubItems.Add("N/A");
            Item.SubItems.Add("Refused");
            break;
          default:
            break;
        }
      }

      Item.Tag = this;

      return Item;
    }

    public bool ContainsToken(string token, CACertificate  caCertificate)
    {
      return
        this.request.FullName.ToLower().Contains(token.ToLower()) ||
        this.certificate.Id.ToString().ToLower().Contains(token.ToLower());
    }

    public DateTime ValidFrom
    {
      get
      {
        if (this.response == null)
        {
          return DateTime.MaxValue;
        }
        else
        {
          return this.response.Signature.ValidFrom;
        }
      }
    }

    public DateTime ValidUntil
    {
      get
      {
        if (this.response == null)
        {
          return DateTime.MaxValue;
        }
        else
        {
          return this.response.Signature.ValidUntil;
        }
      }
    }

    public bool IsOfType(CertificateType type)
    {
      return type.IsOfType(this.certificate);
    }

    public bool IsOfStatus(CertificateStatus status)
    {
      switch (status)
      {
        case CertificateStatus.None:
          return false;
        case CertificateStatus.All:
          return true;
        default:
          return Status == status;
      }
    }

    public bool IsOfDate(DateTime date)
    {
      if (this.response == null)
      {
        return false;
      }
      else
      {
        return
          date.Date >= this.response.Signature.ValidFrom.Date &&
          date.Date <= this.response.Signature.ValidUntil.Date;
      }
    }

    public void Save()
    {
      this.entry.Save(this.FileName);
    }

    public void SaveResponse(string fileName)
    {
      this.entry.Response.Save(fileName);
    }

    public void Revoke()
    {
      this.entry.Revoke();
    }

    public void Sign(CACertificate caCertificate, DateTime validFrom, DateTime validUntil)
    {
      this.entry.Sign(caCertificate, validFrom, validUntil);
      this.response = this.entry.Response.Value;
    }

    public void Refuse(CACertificate caCertificate, string reason)
    {
      this.entry.Refuse(caCertificate, reason);
      this.response = this.entry.Response.Value;
    }

    public bool VerifyRequestSimple()
    {
      return this.entry.Request.VerifySimple();
    }
  }
}
