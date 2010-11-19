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
    public CertificateAuthorityEntry Entry { get; private set; }

    public ListViewItem Item { get; private set; }

    public string FileName { get; private set; }

    public ListEntry(string fileName)
    {
      FileName = fileName;
      Entry = Serializable.Load<CertificateAuthorityEntry>(FileName);
    }

    public ListEntry(string fileName, CertificateAuthorityEntry entry)
    {
      FileName = fileName;
      Entry = entry;
    }

    public void UpdateItem(CACertificate caCertificate)
    {
      SignatureRequest request = Entry.RequestValue(caCertificate);
      Item.Text = Entry.Certificate.Id.ToString();
      Item.SubItems[1].Text = TypeName(Entry.Request.Certificate);

      if (Entry.Request.Certificate is VoterCertificate)
      {
        Item.SubItems[2].Text = GroupList.GetGroupName(((VoterCertificate)Entry.Request.Certificate).GroupId);
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
          SignatureResponse response = Entry.Response.Value;
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
        if (Entry.Response == null)
        {
          return CertificateStatus.New;
        }
        else
        {
          SignatureResponse response = Entry.Response.Value;

          switch (response.Status)
          {
            case SignatureResponseStatus.Accepted:
              if (Entry.Revoked)
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
      SignatureRequest request = Entry.RequestValue(caCertificate);
      Item = new ListViewItem(Entry.Certificate.Id.ToString());

      Item.SubItems.Add(TypeName(Entry.Request.Certificate));

      if (Entry.Request.Certificate is VoterCertificate)
      {
        Item.SubItems.Add(GroupList.GetGroupName(((VoterCertificate)Entry.Request.Certificate).GroupId));
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

      if (Entry.Response == null)
      {
        Item.SubItems.Add(string.Empty);
        Item.SubItems.Add(string.Empty);
        Item.SubItems.Add("New");
      }
      else
      {
        SignatureResponse response = Entry.Response.Value;
        switch (response.Status)
        {
          case SignatureResponseStatus.Accepted:
            Item.SubItems.Add(response.Signature.ValidFrom.ToString());
            Item.SubItems.Add(response.Signature.ValidUntil.ToString());

            if (Entry.Revoked)
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
        Entry.RequestValue(caCertificate).FullName.ToLower().Contains(token.ToLower()) ||
        Entry.Request.Certificate.Id.ToString().ToLower().Contains(token.ToLower());
    }

    public bool IsOfType(CertificateType type)
    {
      return type.IsOfType(Entry.Request.Certificate);
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
      if (Entry.Response == null)
      {
        return false;
      }
      else
      {
        SignatureResponse response = Entry.Response.Value;

        return
          date.Date >= response.Signature.ValidFrom.Date &&
          date.Date <= response.Signature.ValidUntil.Date;
      }
    }

    public void Save()
    {
      Entry.Save(this.FileName);
    }

    private string TypeName(Certificate certificate)
    {
      if (certificate is CACertificate)
      {
        return "CA";
      }
      else if (certificate is AdminCertificate)
      {
        return "Admin";
      }
      else if (certificate is AuthorityCertificate)
      {
        return "Authority";
      }
      else if (certificate is VoterCertificate)
      {
        return "Voter";
      }
      else if (certificate is ServerCertificate)
      {
        return "Server";
      }
      else
      {
        return "Unknown";
      }
    }
  }
}
