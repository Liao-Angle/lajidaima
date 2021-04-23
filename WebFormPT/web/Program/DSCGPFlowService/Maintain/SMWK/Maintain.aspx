<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWK_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>原稿資料夾</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=2 cellpadding=0>
        <tr>
            <td><cc1:dsclabel id="DSCLabel4" runat="server" text="資料匣" width="58px"></cc1:dsclabel></td>
            <td><cc1:singledropdownlist id="SMWYAAA022" runat="server" width="112px" OnSelectChanged="SMWYAAA022_SelectChanged"></cc1:singledropdownlist></td>
            <td><cc1:dsclabel id="DSCLabel1" runat="server" text="流程狀態" width="58px"></cc1:dsclabel></td>
            <td><cc1:singledropdownlist id="SMWYAAA020" runat="server" width="112px"></cc1:singledropdownlist></td>
            <td><cc1:dsclabel id="DSCLabel2" runat="server" text="流程代號" width="58px"></cc1:dsclabel></td>
            <td><cc1:singledropdownlist id="SMWYAAA003" runat="server" width="200px"></cc1:singledropdownlist></td>
            <td><cc1:glassbutton id="FilterButton" runat="server" onclick="FilterButton_Click" text="過濾" width="121px"></cc1:glassbutton></td>
        </tr>
        <tr>
            <td><cc1:dsclabel id="DSCLabel3" runat="server" text="主旨包含" width="58px"></cc1:dsclabel></td>
            <td colspan="5"><cc1:SingleField ID="SMWYAAA006" runat="server" Width="300px" /></td>
            <td><cc1:GlassButton id="ToggleButton" runat="server" onclick="ToggleButton_Click" text="刪除/復原" width="121px" />
            </td>
        </tr>
        </table>
        <table border=0>
            <tr>
                <td>
                 <cc1:DSCLabel id="TotalRows" runat="server" Text="Total: "/>
                </td>
                <td>
                <cc1:DSCLabel id="comma" runat="server" Text=" " />
                </td>
                <td>
                 <cc1:SingleField runat="server" id="CurrentPage" Width="30px" Height="20px" OnTextChanged="CurrentPage_TextChanged" />
                </td>
                <td>
                <cc1:DSCLabel id="TotalPages" runat="server" Width="120px" Text="/共10000頁" />
                </td>
            </tr>
        </table>
        <cc1:datalist id="ListTable" runat="server" height="355px" width="100%" showPageBox="false"  OnshowAllPageClick="ListTable_ShowAllPageClick" OnshowPagingClick="ListTable_ShowPagingClick"   IsHideShowAllPageButton=false OnChangePageSizeClick="ListTable_ChangePageSizeClick" OnLastPageClick="ListTable_LastPageClick" OnFirstPageClick="ListTable_FirstPageClick" OnNextPageClick="ListTable_NextPageClick" OnPrevPageClick="ListTable_PrevPageClick" OnCustomDisplayTitle="ListTable_CustomDisplayTitle" OnGetFlowStatusString="ListTable_GetFlowStatusString" IsPanelWindow="True" OnRefreshButtonClick="ListTable_RefreshButtonClick"></cc1:datalist>
    
    </div>
    </form>
</body>
</html>
