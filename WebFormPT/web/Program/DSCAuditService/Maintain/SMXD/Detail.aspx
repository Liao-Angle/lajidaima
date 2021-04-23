<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCAuditService_Maintain_SMXD_Detail" %>

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
                <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="列印名稱" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXDAAA002" runat="server" ReadOnly="true" Width="100%" />
            </td>
        </tr>
        <tr>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="列印次數" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXDAAA004" runat="server" ReadOnly="true" Width="100%" />
            </td>
        </tr>
        <tr>
            <td width=100px valign=top>
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="列印內容" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXDAAA003" runat="server" ReadOnly="true" Width="398px" Height="113px" MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Height="287px" Text="列印紀錄" Width="500px">
            <cc1:DataList ID="ABList" runat="server" Height="261px" NoAdd="True" NoDelete="True"
                NoModify="True" ReadOnly="True" Width="490px" />
        </cc1:DSCGroupBox>
    </form>
</body>
</html>
