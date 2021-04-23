<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmpShowOpinion.aspx.cs" Inherits="Program_DSCGPFlowService_Public_SmpShowOpinion" %>

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
    <table style="font-size:10pt">
    <tr>
        <td><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="簽核結果" Width="80px" /></td>
        <td>
            <cc1:SingleDropDownList ID="SignResult" runat="server" Width="200px" />
        </td>
    </tr>
    <tr>
        <td valign=top><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="簽核意見" Width="80px" /><br />
            <cc1:GlassButton ID="PhraseButton" runat="server" OnClick="PhraseButton_Click" Text="常用片語"
                Width="86px" ImageUrl="~/Images/Phrase.gif" />
        </td>
        <td>
            <cc1:SingleField ID="SignOpinion" runat="server" Height="122px" MultiLine="True" Width="200px" />
        </td>
    </tr>
    <tr>
        <td colspan=2 align=center>
            <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
            <cc1:GlassButton ID="SendButton" runat="server" Text="送出簽核" OnClick="SendButton_Click" Width="100px" ImageUrl="~/Images/OK.gif" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
