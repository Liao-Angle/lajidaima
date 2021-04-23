<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SignedBox_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>已簽核資料匣</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="搜尋條件" >
        <table >
            <tr>
                <td>
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="主旨包含" Width="80px" />
                </td>
                <td>
                    <cc1:SingleField ID="Subject" runat="server" Width="150px" />
                </td>
                <td>
                    <cc1:DSCLabel ID="Dsclabel3" runat="server" Text="工作建立時間" Width="100px" />
               </td>
                <td>
                    <cc1:SingleDateTimeField ID="StartTime" runat="server" Width="100px" DateTimeMode="0" />
                </td>
                <td>
                    <cc1:DSCLabel ID="Dsclabel4" runat="server" Text="～" Width="20px" />
                </td>
                <td>
                    <cc1:SingleDateTimeField ID="EndTime" runat="server" Width="100px" DateTimeMode="0" />
               </td>
                <td>
<cc1:GlassButton ID="FilterButton" runat="server" Height="20px" OnClick="FilterButton_Click"
                Text="搜尋" Width="43px" showWaitingIcon="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="表單代號" Width="80px" />
                </td>
                <td>
                    <cc1:SingleOpenWindowField IgnoreCase="true"  ID="FlowID" runat="server" guidField="SMWBAAA001" keyField="SMWBAAA003"
                        serialNum="001" showReadOnlyField="True" tableName="SMWQ" Width="260px" FixReadOnlyValueTextWidth="140px"
                        FixValueTextWidth="90px" guidIndex="1" idIndex="2" valueIndex="3" />
                    
                </td>
                <td>
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="表單單號" Width="80px" />
                </td>
                <td>
                    <cc1:SingleField ID="FormNo" runat="server" Width="100px" />
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="填單人" Width="80px" />
                </td>
                <td>
                    <cc1:SingleOpenWindowField IgnoreCase="true" ID="UserID" runat="server" guidField="OID" keyField="id"
                        serialNum="001" showReadOnlyField="True" tableName="Users" Width="260px" FixReadOnlyValueTextWidth="140px"
                        FixValueTextWidth="90px" guidIndex="0" idIndex="1" valueIndex="2" />
                </td>
            </tr>
        </table>
        </cc1:DSCGroupBox>
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
        <cc1:datalist id="ListTable" runat="server" height="355px" width="100%" OnshowAllPageClick="ListTable_ShowAllPageClick" OnshowPagingClick="ListTable_ShowPagingClick"  showPageBox="false" IsHideShowAllPageButton=false OnChangePageSizeClick="ListTable_ChangePageSizeClick" OnLastPageClick="ListTable_LastPageClick" OnFirstPageClick="ListTable_FirstPageClick" OnNextPageClick="ListTable_NextPageClick" OnPrevPageClick="ListTable_PrevPageClick" OnCustomDisplayTitle="ListTable_CustomDisplayTitle" IsPanelWindow="True" OnGetFlowStatusString="ListTable_GetFlowStatusString" OnRefreshButtonClick="ListTable_RefreshButtonClick"></cc1:datalist>
    
    </div>
    </form>
</body>
</html>
