<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetFlow.aspx.cs" Inherits="Program_DSCGPFlowService_Public_SetFlow" %>

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
        <td valign=top><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="加入關卡" Width="80px" />
        </td>
        <td>
            <cc1:DataList ID="DetailList" runat="server" Height="193px" InputForm="SetFlowDetail.aspx" OnAddOutline="DetailList_AddOutline" OnCustomDisplayField="DetailList_CustomDisplayField" OnDeleteData="DetailList_DeleteData" Width="498px" />
        </td>
        <td valign=top>
            <br />
            <br />
            <cc1:GlassButton ID="TOP" runat="server" Width="22px" Height="22px" ImageUrl="~/Program/DSCGPFlowService/Public/Image/icon_up01.gif" OnClick="TOP_Click" CssClass="GeneralButton" ToolTip="移至第一關" /><br />
            <cc1:GlassButton ID="UP" runat="server" Width="22px" Height="22px" ImageUrl="~/Program/DSCGPFlowService/Public/Image/icon_up02.gif" OnClick="UP_Click" CssClass="GeneralButton" /><br />
            <cc1:GlassButton ID="DOWN" runat="server" Width="22px" Height="22px" ImageUrl="~/Program/DSCGPFlowService/Public/Image/icon_down02.gif" OnClick="DOWN_Click" CssClass="GeneralButton" /><br />
            <cc1:GlassButton ID="BOTTOM" runat="server" Width="22px" Height="22px" ImageUrl="~/Program/DSCGPFlowService/Public/Image/icon_down01.gif" OnClick="BOTTOM_Click" CssClass="GeneralButton" /><br />
        </td>
    </tr>
    <tr>
        <td colspan=2 align=center>
            <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
            <cc1:GlassButton ID="SendButton" runat="server" Text="設定流程" ConfirmText="確定?" OnClick="SendButton_Click" Width="100px" ImageUrl="~/Images/OK.gif" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
