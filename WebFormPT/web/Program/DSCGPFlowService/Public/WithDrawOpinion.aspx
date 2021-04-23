<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WithDrawOpinion.aspx.cs" Inherits="Program_DSCGPFlowService_Public_WithDrawOpinion" %>

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
        <td valign=top><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="撤銷意見" Width="80px" /><br />
            <cc1:GlassButton ID="PhraseButton" runat="server" OnClick="PhraseButton_Click" Text="常用片語"
                Width="89px" ImageUrl="~/Images/Phrase.gif" />
        </td>
        <td>
            <cc1:SingleField ID="SignOpinion" runat="server" Height="122px" Width="200px" MultiLine="True" />
        </td>
    </tr>
    <tr>
        <td colspan=2 align=center>
            <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
            <cc1:GlassButton ID="SendButton" runat="server" Text="撤銷" ConfirmText="確定撤銷?" OnClick="SendButton_Click" Width="100px" ImageUrl="~/Images/OK.gif" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
