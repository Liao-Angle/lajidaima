<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReGet.aspx.cs" Inherits="Program_DSCGPFlowService_Public_ReGet" %>

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
        <td valign=top>取回意見<br />
            <cc1:GlassButton ID="PhraseButton" runat="server" OnClick="PhraseButton_Click" Text="常用片語"
                Width="80px" ImageUrl="~/Images/Phrase.gif" />
        </td>
        <td>
            <cc1:SingleField ID="SignOpinion" runat="server" Height="122px" Width="200px" MultiLine="True" />
        </td>
    </tr>
    <tr>
        <td colspan=2 align=center>
            <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
            <cc1:GlassButton ID="SendButton" runat="server" Text="取回" ConfirmText="確定取回?" OnClick="SendButton_Click" Width="100px" ImageUrl="~/Images/OK.gif" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
