<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCAuditService_Maintain_SMXC_Detail" %>

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
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="應用程式代號" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA002" runat="server" ReadOnly="true" Width="147px" />
            </td>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="應用程式名稱" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA003" runat="server" ReadOnly="true" Width="147px" />
            </td>
        </tr>
        <tr>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="模組代號" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA004" runat="server" ReadOnly="true" Width="147px" />
            </td>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="模組名稱" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA005" runat="server" ReadOnly="true" Width="147px" />
            </td>
        </tr>
        <tr>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="程式代號" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA006" runat="server" ReadOnly="true" Width="147px" />
            </td>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="程式名稱" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA007" runat="server" ReadOnly="true" Width="147px" />
            </td>
        </tr>
        <tr>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="使用者代號" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA012" runat="server" ReadOnly="true" Width="147px" />
            </td>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="使用者姓名" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA013" runat="server" ReadOnly="true" Width="147px" />
            </td>
        </tr>
        <tr>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="警示層級" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA008" runat="server" ReadOnly="true" Width="147px" />
            </td>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="記錄時間" />
            </td>
            <td width=147px>
                <cc1:SingleField ID="SMXCAAA009" runat="server" ReadOnly="true" Width="147px" />
            </td>
        </tr>
        <tr>
            <td width=100px>
                <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="客戶端位置" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXCAAA014" runat="server" ReadOnly="true" Width="100%" />
            </td>
        </tr>
        <tr>
            <td width=100px nowrap=true valign=top>
                <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="瀏覽器版本" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXCAAA015" runat="server" ReadOnly="true" Width="398px"  Height="113px" MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td width=100px valign=top>
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="警示訊息" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXCAAA010" runat="server" ReadOnly="true" Width="398px" Height="113px" MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td width=100px valign=top>
                <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="追蹤內容" />
            </td>
            <td colspan=3 width=398px>
                <cc1:SingleField ID="SMXCAAA011" runat="server" ReadOnly="true" Width="398px" Height="113px" MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
