<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SignWeb.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
      .style1
      {
        width: 100%;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="infoLabel" runat="server" 
      Text="Enter the certificate's id and fingerprint to report a signature"></asp:Label>
    <br />
    <table class="style1">
      <tr>
        <td>
          <asp:Label ID="idLabel" runat="server" Text="Certificate Id:"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="idTextBox" runat="server" Width="500px"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>
          <asp:Label ID="fingerprintLabel" runat="server" Text="Request Key:"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="requestKeyTextBox" runat="server" Width="500px"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>
          &nbsp;</td>
        <td>
          <asp:Button ID="reportSignatureButton" runat="server" 
            onclick="reportSignatureButton_Click" Text="Report signature" />
        </td>
      </tr>
    </table>
    </form>
</body>
</html>
