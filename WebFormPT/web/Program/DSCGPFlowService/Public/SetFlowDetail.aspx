<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetFlowDetail.aspx.cs" Inherits="Program_DSCGPFlowService_Public_SetFlowDetail" %>

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
        <td valign=top><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="角色名稱" Width="80px" />
        </td>
        <td>
            <cc1:SingleDropDownList ID="RoleName" runat="server" />
        </td>
    </tr>
    <tr>
        <td valign=top>
            <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="要加簽對象：" Width="83px" />
            <br />
            <cc1:SingleDropDownList ID="participantType" runat="server" Width="103px" />
            <br />
            <cc1:GlassButton ID="SelectParticipant" runat="server" Text="選擇對象" Width="99px" OnClick="SelectParticipant_Click" />
            <br />
            <cc1:GlassButton ID="DeleteButton" runat="server" Text="刪除對象" Width="99px" OnClick="DeleteButton_Click" />
        </td>
        <td>
            <cc1:MultiDropDownList ID="performers" runat="server" Height="107px" Width="279px" />
        </td>
    </tr>
    <tr>
        <td valign=top><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="工作分派模式" Width="80px" />
        </td>
        <td>
            <cc1:SingleDropDownList ID="multiUserMode" runat="server" />
        </td>
    </tr>
    <tr>
        <td valign=top><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="簽核模式" Width="80px" />
        </td>
        <td>
            <cc1:SingleDropDownList ID="performType" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan=2 align=center>
            <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
            &nbsp;
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
