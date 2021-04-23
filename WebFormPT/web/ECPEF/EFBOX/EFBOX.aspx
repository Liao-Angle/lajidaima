<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EFBOX.aspx.cs" Inherits="ECPEF_EFBOX_EFBOX" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>原稿資料夾</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="搜尋條件">
                <table>
                    <tr>
                        <td style="height: 43px">
                            <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="列出此流程" Width="95px" style="z-index: 131; left: 0px; position: absolute; top: 0px"></cc1:DSCLabel>
                        </td>
                        <td style="height: 43px">
                            <cc1:SingleDropDownList ID="ProcessIDList" runat="server" Width="248px" style="z-index: 126; left: 0px; position: absolute; top: 0px"></cc1:SingleDropDownList>
                        </td>
                        <td style="height: 43px">
                            &nbsp;</td>
                        <td colspan="3" style="height: 43px">
                            &nbsp;</td>
                        <td style="height: 43px">
                            <cc1:SingleDropDownList ID="ViewTimes" runat="server" Width="100px"></cc1:SingleDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="主旨包含" Width="100%" />
                        </td>
                        <td>
                            <cc1:SingleField ID="Subject" runat="server" Width="100%" />
                        </td>
                        <td>
                            <cc1:DSCLabel ID="Dsclabel3" runat="server" Text="工作建立時間" Width="93px" />
                        </td>
                        <td>
                            <cc1:SingleDateTimeField ID="StartTime" runat="server" Width="133px" DateTimeMode="0" />
                        </td>
                        <td>
                            <cc1:DSCLabel ID="Dsclabel4" runat="server" Text="～" Width="20px" />
                        </td>
                        <td>
                            <cc1:SingleDateTimeField ID="EndTime" runat="server" Width="133px" DateTimeMode="0" />
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
                 <cc1:DSCLabel id="TotalRows" runat="server" Text="共0筆資料"/>
                </td>
                <td>
                <cc1:DSCLabel id="comma" runat="server" Text=" " />
                </td>
                <td>
                 <cc1:SingleField runat="server" id="CurrentPage" Width="30px" Height="20px" OnTextChanged="CurrentPage_TextChanged" />
                </td>
                <td>
                <cc1:DSCLabel id="TotalPages" runat="server" Width="120px" Text="/共0頁" />
                </td>
            </tr>
        </table>
            <cc1:DataList ID="ListTable" runat="server" Height="291px" Width="100%" OnshowAllPageClick="ListTable_ShowAllPageClick" OnshowPagingClick="ListTable_ShowPagingClick"   showPageBox="false" IsHideShowAllPageButton=false OnChangePageSizeClick="ListTable_ChangePageSizeClick" OnLastPageClick="ListTable_LastPageClick" OnFirstPageClick="ListTable_FirstPageClick" OnNextPageClick="ListTable_NextPageClick" OnPrevPageClick="ListTable_PrevPageClick" OnCustomDisplayTitle="ListTable_CustomDisplayTitle"
                IsPanelWindow="True" OnGetFlowStatusString="ListTable_GetFlowStatusString" OnRefreshButtonClick="ListTable_RefreshButtonClick"
                NoAdd="True" NoDelete="True" OnsetEnhancedRow="ListTable_setEnhancedRow" 
                ondeletedata="ListTable_DeleteData" 
                onbeforedeletedata="ListTable_BeforeDeleteData"></cc1:DataList>
        </div>
    </form>
</body>
</html>
