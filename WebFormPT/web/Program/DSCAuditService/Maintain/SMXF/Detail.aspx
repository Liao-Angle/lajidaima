<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCAuditService_Maintain_SMXF_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>程式代碼輸入畫面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=2 cellpadding=0 style="font-size:9pt" width="500px">
        <tr>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="列印者代號" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXDAAB003" runat="server" ReadOnly="true" Width="147px" />
            </td>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="列印者姓名" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXDAAB004" runat="server" ReadOnly="true" Width="147px" />
            </td>
        </tr>
        <tr>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="列印位置" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXDAAB005" runat="server" ReadOnly="true" Width="147px" />
            </td>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="列印時間" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXDAAB006" runat="server" ReadOnly="true" Width="147px" />
            </td>
        </tr>
        <tr>
            <td width=100px nowrap=true valign=top>
                <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="瀏覽器版本" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXDAAB007" runat="server" ReadOnly="true" Width="398px"  Height="113px" MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td width=100px valign=top>
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="列印名稱" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXDAAA002" runat="server" ReadOnly="true" Width="398px" Height="113px" MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td width=100px valign=top>
                <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="列印內容" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXDAAA003" runat="server" ReadOnly="true" Width="398px" Height="113px" MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
