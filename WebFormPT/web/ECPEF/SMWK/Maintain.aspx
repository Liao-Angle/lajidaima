<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="ECPEF_SMWK_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>原稿資料夾</title>
    <script language=javascript>
        function confirmOld() {
            if (confirm('你確定要讀取全部資料嗎?此動作需耗費時間')) {
                return true;
            } else {
                return false;
            }
        }
    </script>
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
            <td><cc1:glassbutton id="FilterButton" runat="server" onclick="FilterButton_Click" text="搜尋" width="100px"></cc1:glassbutton></td>
        </tr>
        <tr>
            <td><cc1:dsclabel id="DSCLabel3" runat="server" text="主旨包含" width="58px"></cc1:dsclabel></td>
            <td><cc1:SingleField ID="SMWYAAA006" runat="server" Width="150px" /></td>
	    <td><cc1:DSCLabel ID="Dsclabel8" runat="server" Text="工作建立時間" Width="93px" /></td>
            <td><cc1:SingleDateTimeField ID="StartTime" runat="server" Width="133px" DateTimeMode="0" /></td>
            <td><cc1:DSCLabel ID="Dsclabel9" runat="server" Text="～" Width="20px" /></td>
            <td><cc1:SingleDateTimeField ID="EndsTime" runat="server" Width="133px" 
                    DateTimeMode="0" ReadOnly="False"/></td>
            <td><cc1:GlassButton id="ToggleButton" runat="server" onclick="ToggleButton_Click" text="隱藏/復原" width="100px" /></td>
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
