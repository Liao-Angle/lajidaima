<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reassign.aspx.cs" Inherits="Program_DSCGPFlowService_Public_Reassign" %>

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
        <td valign=top><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="請選擇要轉派的人員：" Width="139px" />
            <br />
            <cc1:GlassButton ID="UserButton" runat="server" Text="常用使用者" Width="95px" OnClick="UserButton_Click" ImageUrl="~/Images/Users.gif" /></td>
        <td valign=top><cc1:SingleOpenWindowField ID="ToUserID" runat="server" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users" Width="298px" FixReadOnlyValueTextWidth="150px" FixValueTextWidth="110px" /></td>
    </tr>
    <tr>
        <td valign=top><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="轉派類型：" Width="139px" /></td>
        <td valign=top>
            <cc1:SingleDropDownList ID="assignmentType" runat="server" Width="300px" />
        </td>
    </tr>
    <tr>
        <td valign=top><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="請填寫轉派意見：" Width="139px" />
            <br />
            <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
            <cc1:GlassButton ID="PhraseButton" runat="server" Text="常用片語" Width="77px" OnClick="PhraseButton_Click" ImageUrl="~/Images/Phrase.gif" />
        </td>
        <td valign=top><cc1:SingleField ID="AssignOpinion" runat="server" Height="90px" MultiLine="True" Width="300px" />
            &nbsp;
        </td>
    </tr>
    <tr>
        <td valign=top align=center colspan=2><cc1:GlassButton ID="ConfirmButton" runat="server" Text="確認轉派" Width="100px" OnClick="ConfirmButton_Click" ImageUrl="~/Images/OK.gif" />
            <cc1:OpenWin ID="OpenWin2" runat="server" OnOpenWindowButtonClick="OpenWin2_OpenWindowButtonClick" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
