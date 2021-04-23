<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDetail.aspx.cs" Inherits="SmpProgram_Form_SPKM005_ViewDetail" %>
<%@ Register assembly="DSCWebControl" namespace="DSCWebControl" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../../../StyleSheet/Enterprise.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script language=javascript>
        function openDocWindow(title, url, bWidth, bHeight, frameType, isMax, newWindow) {
            window.parent.openDocWindowGeneral(title, url, bWidth, bHeight, frameType, isMax, newWindow);
        }
    </script>
</head>
<body scroll="no" >
    <form id="form1" runat="server">
    <cc1:DSCGroupBox ID="GroupFilter" runat="server" Text="搜尋條件">
    <table width="100%"> 
        <tr>
            <td width="90px">
                <cc1:DSCLabel ID="LblDocName" runat="server" Text="文件名稱包含" Width="90px" />
            </td>
            <td width="120px">
                <cc1:SingleField ID="DocName" runat="server" Width="120px" />
            </td>
            <td width="90px">
                <cc1:DSCLabel ID="LblAbstractValue" runat="server" Text="文件摘要包含" Width="90px" />
            </td>
            <td width="120px">
                <cc1:SingleField ID="AbstractValue" runat="server" Width="120px" />
            </td>
            <td>
                <cc1:GlassButton ID="ButtonSearch" runat="server" Height="20px" Text="過濾" 
                    Width="40px" onclick="ButtonSearch_Click" />
            </td>
            <td width="100%"></td>
        </tr>
    </table>
    </cc1:DSCGroupBox>
    <table width="100%">    
        <tr>
            <td>
            <div>
                <cc1:DataList ID="DocList" runat="server" Height="320px" Width="100%" 
                    IsMaintain="True" IsPanelWindow="True" isShowAll="false" IsShowCheckBox="False" 
                    NoAdd="True" NoDelete="True" NoModify="True" EnableTheming="True" 
                    IsHideSelectAllButton="True" />
            </div>
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
