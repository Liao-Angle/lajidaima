﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWQ_Maintain" %>

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
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="搜尋條件" >
        <table >
            <tr><td><cc1:dsclabel id="DSCLabel2" runat="server" text="流程狀態" width="66px"></cc1:dsclabel></td>
                <td><cc1:singledropdownlist id="SMWYAAA020" runat="server" width="112px"></cc1:singledropdownlist></td>
                <td><cc1:dsclabel id="DSCLabel5" runat="server" text="流程代號" width="68px"></cc1:dsclabel></td>
                <td colspan=3><cc1:singledropdownlist id="SMWYAAA003" runat="server" width="200px"></cc1:singledropdownlist></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="主旨包含" Width="80px" />
                </td>
                <td>
                    <cc1:SingleField ID="Subject" runat="server" Width="150px" />
                </td>
                <td>
                    <cc1:DSCLabel ID="Dsclabel3" runat="server" Text="工作建立時間" Width="93px" />
               </td>
                <td>
                    <cc1:SingleDateTimeField ID="StartTime" runat="server" Width="133px" DateTimeMode="1" />
                </td>
                <td>
                    <cc1:DSCLabel ID="Dsclabel4" runat="server" Text="～" Width="20px" />
                </td>
                <td>
                    <cc1:SingleDateTimeField ID="EndTime" runat="server" Width="133px" DateTimeMode="1" />
               </td>
                <td>
<cc1:GlassButton ID="FilterButton" runat="server" Height="20px" OnClick="FilterButton_Click"
                Text="搜尋" Width="43px" showWaitingIcon="True" />
                </td>
            </tr>
        </table>
        </cc1:DSCGroupBox>
        <table border=0>
            <tr>
                <td>
                 <cc1:DSCLabel id="TotalRows" runat="server" Text="共0筆資料 "/>
                </td>
                <td>
                <cc1:DSCLabel id="comma" runat="server" Text=" " />
                </td>
                <td>
                 <cc1:SingleField runat="server" id="CurrentPage" Width="30px" Height="20px" OnTextChanged="CurrentPage_TextChanged" />
                </td>
                <td>
                <cc1:DSCLabel id="TotalPages" runat="server"  Width="120px" Text="/共0頁" />
                </td>
            </tr>
        </table>
        <cc1:datalist id="ListTable" runat="server" height="355px" width="100%" OnshowAllPageClick="ListTable_ShowAllPageClick" OnshowPagingClick="ListTable_ShowPagingClick"  showPageBox="false" IsHideShowAllPageButton=false OnChangePageSizeClick="ListTable_ChangePageSizeClick" OnLastPageClick="ListTable_LastPageClick" OnFirstPageClick="ListTable_FirstPageClick" OnNextPageClick="ListTable_NextPageClick" OnPrevPageClick="ListTable_PrevPageClick" OnCustomDisplayTitle="ListTable_CustomDisplayTitle" IsPanelWindow="True" OnGetFlowStatusString="ListTable_GetFlowStatusString" OnRefreshButtonClick="ListTable_RefreshButtonClick"></cc1:datalist>
    
    </div>
    </form>
</body>
</html>
