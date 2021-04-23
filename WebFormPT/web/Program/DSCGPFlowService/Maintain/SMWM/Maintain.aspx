<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWM_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>流程管理作業</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=2 cellpadding=0>
        <tr>
            <td><cc1:dsclabel id="DSCLabel1" runat="server" text="流程狀態" width="78px"></cc1:dsclabel></td>
            <td><cc1:singledropdownlist id="SMWMAAA008" runat="server" width="200px"></cc1:singledropdownlist></td>
            <td><cc1:dsclabel id="DSCLabel2" runat="server" text="流程代號" width="78px"></cc1:dsclabel></td>
            <td><cc1:singledropdownlist id="SMWMAAA003" runat="server" width="200px"></cc1:singledropdownlist></td>
            <td></td>
        </tr>
        <tr>
            <td><cc1:dsclabel id="DSCLabel3" runat="server" text="查詢時間起" width="78px"></cc1:dsclabel></td>
            <td><cc1:SingleDateTimeField ID="SMWMAAA005S" runat="server" DateTimeMode="1" /></td>
            <td><cc1:dsclabel id="DSCLabel4" runat="server" text="查詢時間迄" width="78px"></cc1:dsclabel></td>
            <td><cc1:SingleDateTimeField ID="SMWMAAA005E" runat="server" DateTimeMode="1" /></td>
            <td></td>
        </tr>
        <tr>
            <td><cc1:dsclabel id="DSCLabel5" runat="server" text="單號" width="78px"></cc1:dsclabel></td>
            <td><cc1:SingleField id="SMWMAAA004" runat="server" width="200px"></cc1:SingleField></td>
            <td></td>
            <td></td>
            <td><cc1:glassbutton id="FilterButton" runat="server" onclick="FilterButton_Click" text="過濾" width="121px" showWaitingIcon="True"></cc1:glassbutton></td>
        </tr>         </table>
        <cc1:datalist id="ListTable" runat="server" height="355px" width="100%" OnCustomDisplayTitle="ListTable_CustomDisplayTitle" OnGetFlowStatusString="ListTable_GetFlowStatusString" IsPanelWindow="True" OnRefreshButtonClick="ListTable_RefreshButtonClick"></cc1:datalist>
    
    </div>
    </form>
</body>
</html>
