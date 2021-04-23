<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_System_Maintain_DBAccess_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border=0 cellspacing=2 cellpadding=0 width=100% height=100% >
    <tr height=30px>
        <td id=TableNames style="width: 29%" >
            <cc1:SingleDropDownList ID="TableList" runat="server" Width=200px />
        </td>
        <td>
            <cc1:GlassButton ID="ShowRow" runat="server" OnClick="ShowRow_Click" Text="顯示資料"
                ToolTip="顯示資料" Width="132px" showWaitingIcon="True" /><cc1:GlassButton ID="Query" runat="server" OnClick="Query_Click" Text="執行"
                ToolTip="執行" Width="132px" showWaitingIcon="True" />
        </td>
    </tr>
    <tr>
        <td id=SQLList valign=top height=100px colspan=2>
            <cc1:SingleField ID="SQL" runat="server" Height="100px" MultiLine="True" Width="100%" />
        </td>

    </tr>
    <tr>
        <td id=RowList valign=top height=100% colspan=2>
            <cc1:DataList ID="DataViewer" runat="server" Width=100% NoAdd="true" NoDelete="true" Height="271px" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
