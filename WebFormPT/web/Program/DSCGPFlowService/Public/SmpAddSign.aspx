<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmpAddSign.aspx.cs" Inherits="Program_DSCGPFlowService_Public_AddSign" %>

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
        <td valign=top>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td valign=top><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="加入方式" Width="80px" />
        </td>
        <td colspan=2>
            <cc1:DSCRadioButton ID="BEFORE" GROUPNAME="ADDSIGNMODE" runat="server" Text="跑完加簽關卡後再給我" OnClick="BEFORE_Click" />
            <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="處理意見:" Width="75px" />
            <cc1:SingleField ID="Comments" runat="server" ReadOnly="True" Width="274px" />
            <br />
            <cc1:DSCRadioButton ID="AFTER" GROUPNAME="ADDSIGNMODE"  runat="server" Checked="True" Text="處理完後再跑加簽關卡" OnClick="AFTER_Click" />
            &nbsp;
        </td>
    </tr>
    <tr>
        <td valign=top><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="加入關卡" Width="80px" />
        </td>
        <td>
            <cc1:DataList ID="DetailList" runat="server" Height="193px" InputForm="AddSignDetail.aspx" OnAddOutline="DetailList_AddOutline" OnCustomDisplayField="DetailList_CustomDisplayField" OnDeleteData="DetailList_DeleteData" Width="498px" />
        </td>
        <td valign=top>
            <br />
            <br />
            <cc1:GlassButton ID="TOP" runat="server" Width="22px" Height="22px" ImageUrl="~/Program/DSCGPFlowService/Public/Image/icon_up01.gif" OnClick="TOP_Click" CssClass="GeneralButton" /><br />
            <cc1:GlassButton ID="UP" runat="server" Width="22px" Height="22px" ImageUrl="~/Program/DSCGPFlowService/Public/Image/icon_up02.gif" OnClick="UP_Click" CssClass="GeneralButton" /><br />
            <cc1:GlassButton ID="DOWN" runat="server" Width="22px" Height="22px" ImageUrl="~/Program/DSCGPFlowService/Public/Image/icon_down02.gif" OnClick="DOWN_Click" CssClass="GeneralButton" /><br />
            <cc1:GlassButton ID="BOTTOM" runat="server" Width="22px" Height="22px" ImageUrl="~/Program/DSCGPFlowService/Public/Image/icon_down01.gif" OnClick="BOTTOM_Click" CssClass="GeneralButton" /><br />
        </td>
    </tr>
    <tr>
        <td colspan=2 align=center>
            <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
            <cc1:GlassButton ID="SendButton" runat="server" Text="加簽" ConfirmText="確定加簽?" OnClick="SendButton_Click" Width="100px" ImageUrl="~/Images/OK.gif" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
