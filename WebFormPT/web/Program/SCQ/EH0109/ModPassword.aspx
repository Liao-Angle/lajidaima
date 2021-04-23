<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModPassword.aspx.cs" Inherits="SmpProgram_Maintain_LeaveJob_ModPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="1" cellspacing="0" 
            style=" border: thin solid #666699; width:350px; height:180px;">
      <tr style=" height:50px; font-size:large; background-color:#666699;">
    <td colspan="2">
        <asp:Label ID="Label1" runat="server" Text="薪資查看密碼修改" Width="100%" 
            ForeColor="White"></asp:Label>
    </td>
    </tr>
    <tr>
    <td align="right" 
            style="border-top-style: solid; border-right-style: solid; border-bottom-style: solid; border-top-width: thin; border-right-width: thin; border-bottom-width: thin; border-top-color: #666699; border-right-color: #666699; border-bottom-color: #666699;">
        <asp:Label ID="Label3" runat="server" Text="新密碼："></asp:Label>
    </td>
    <td style="border-top-style: solid; border-top-width: thin; border-top-color: #666699; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #666699">
        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="right" 
            style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #666699; border-right-style: solid; border-right-width: thin; border-right-color: #666699;">
        <asp:Label ID="Label4" runat="server" Text="再次確認新密碼："></asp:Label>
    </td>
    <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #666699">
        <asp:TextBox ID="tbPassword2" runat="server" TextMode="Password"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
    </td>
    <td>
        <asp:Button ID="btMod" runat="server" Text="修改密碼" onclick="btMod_Click" />
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
