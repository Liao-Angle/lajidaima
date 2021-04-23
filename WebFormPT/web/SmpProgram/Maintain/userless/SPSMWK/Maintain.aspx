<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain_SPSMWK_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>新普表單查詢作業</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=2 cellpadding=0>
        <tr>
            <td><cc1:dsclabel id="DSCLabel4" runat="server" text="資料匣" width="70px"></cc1:dsclabel></td>
            <td><cc1:singledropdownlist id="SMWYAAA022" runat="server" width="112px" OnSelectChanged="SMWYAAA022_SelectChanged"></cc1:singledropdownlist></td>
            <td><cc1:dsclabel id="DSCLabel1" runat="server" text="流程狀態" width="70px"></cc1:dsclabel></td>
            <td><cc1:singledropdownlist id="SMWYAAA020" runat="server" width="112px"></cc1:singledropdownlist></td>
            <td><cc1:dsclabel id="DSCLabel2" runat="server" text="流程代號" width="70px"></cc1:dsclabel></td>
            <td><cc1:singledropdownlist id="SMWYAAA003" runat="server" width="200px"></cc1:singledropdownlist></td>
            <td><cc1:glassbutton id="FilterButton" runat="server" onclick="FilterButton_Click" text="過濾" width="60px"></cc1:glassbutton></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><cc1:dsclabel id="DSCLabel3" runat="server" text="主旨包含" width="70px"></cc1:dsclabel></td>
            <td colspan="3">
                <cc1:SingleField ID="SMWYAAA006" runat="server" Width="300px" /></td>
            <td><cc1:dsclabel id="DSCLabel5" runat="server" text="單號" width="70px"></cc1:dsclabel></td>
            <td><cc1:SingleField ID="SheetNo" runat="server" Width="200px" /></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><cc1:DSCLabel ID="Dsclabel7" runat="server" Text="建立時間" Width="70px" /></td>
            <td colspan="3">
                <cc1:SingleDateTimeField ID="StartTime" runat="server" Width="133px" DateTimeMode="1" />
                <cc1:DSCLabel ID="Dsclabel8" runat="server" Text="～" Width="20px" />
                <cc1:SingleDateTimeField ID="EndTime" runat="server" Width="133px" DateTimeMode="1" />
            </td>
            <td><cc1:dsclabel id="DSCLabel6" runat="server" text="填表人/關系人" width="104px" 
                    Height="16px"></cc1:dsclabel></td>
            <td><cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="242px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" idIndex="2" valueIndex="3" />
            </td>
            <td></td>
            <td></td>
        </tr>
        </table>
        <cc1:datalist id="ListTable" runat="server" height="355px" width="100%" OnCustomDisplayTitle="ListTable_CustomDisplayTitle" OnGetFlowStatusString="ListTable_GetFlowStatusString" IsPanelWindow="True" OnRefreshButtonClick="ListTable_RefreshButtonClick"></cc1:datalist>
    
    </div>
    </form>
</body>
</html>
