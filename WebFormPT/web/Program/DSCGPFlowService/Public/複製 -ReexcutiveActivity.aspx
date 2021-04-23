<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReexcutiveActivity.aspx.cs" Inherits="Program_DSCGPFlowService_Public_ReexcutiveActivity" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>退回重辦</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border=0 width=450px>
        <tr>
            <td>
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="要退回的關卡" />
            </td>
            <td>
                <cc1:SingleDropDownList ID="BackActID" runat="server" Width=350px />
            </td>
        </tr>
        <tr>
            <td valign=top>
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="簽核意見" />
                <cc1:GlassButton ID="PhraseButton" runat="server" OnClick="PhraseButton_Click" Text="常用片語"
                    Width="85px" ImageUrl="~/Images/Phrase.gif" />
                <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
            </td>
            <td>
                <cc1:SingleField ID="SignOpinion" runat="server" Width=350px Height=100px MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td colspan=2>
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="退回重辦之後：" />
            </td>
        </tr>
        <tr>
            <td align=right>
                <cc1:DSCRadioButton ID="BackType0" runat="server" GroupName="BackType" Checked="True" />
            </td>
            <td>
                <asp:Image ID="Image1" runat="server" AlternateText="按照流程定義依序重新執行" ImageUrl="~/Program/DSCGPFlowService/Public/Image/reexecuteActivityFollowDefinition.gif" /></td>
        </tr>
        <tr>
            <td align=right>
                <cc1:DSCRadioButton ID="BackType2" runat="server" GroupName="BackType" />
            </td>
            <td>
                <asp:Image ID="Image2" runat="server" AlternateText="略過之前已經執行過的關卡, 直接回傳給我" ImageUrl="~/Program/DSCGPFlowService/Public/Image/reexecuteActivityComeBackAsNew.gif" /></td>
        </tr>
        <tr>
            <td colspan=2 align=center>
                <cc1:GlassButton ID="ConfirmButton" runat="server" ConfirmText="確認要退回重辦嗎？" OnClick="ConfirmButton_Click" Text="退回重辦" Width="97px" ImageUrl="~/Images/OK.gif" />
            </td>
        </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
