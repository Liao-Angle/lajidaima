<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Info.aspx.cs" Inherits="Program_DSCGPFlowService_Public_Info" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border=0 width=550px cellspacing=5 cellpadding=0>
    <tr>
        <td valign=top><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="請選擇要通知的類型：" Width="139px" /></td>
        <td valign=top colspan=2>
            <cc1:SingleDropDownList ID="NoticeType" runat="server" Width="300px" />
        </td>
    </tr>
    <tr>
        <td valign=top><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="請選擇要通知的人員：" Width="139px" />
            <br />
            <cc1:GlassButton ID="UserButton" runat="server" ImageUrl="~/Images/Users.gif" OnClick="UserButton_Click"
                Text="常用使用者" Width="95px" />
            <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
        </td>
        <td valign=top style="width: 302px">
            <cc1:MultiDropDownList ID="AcceptorOID" runat="server" Height="138px" Width="298px" />
        </td>
        <td valign=top>
            <cc1:GlassButton ID="SelectUser" runat="server" Text="+" Width="23px" OnClick="SelectUser_Click" />
            <br />
            <cc1:GlassButton ID="DeleteUser" runat="server" Text="X" Width="23px" OnClick="DeleteUser_Click" />
        </td>
    </tr>
    <tr>
        <td valign=top align=center colspan=3><cc1:GlassButton ID="ConfirmButton" runat="server" Text="確認通知" Width="100px" OnClick="ConfirmButton_Click" ImageUrl="~/Images/OK.gif" /></td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
