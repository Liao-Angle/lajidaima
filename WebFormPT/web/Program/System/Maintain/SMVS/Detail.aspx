<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVS_Detail" %>

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
    <table border=0 width=510px>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="下載標題" Width="120px" /></td>
        <td >
            <cc1:SingleField ID="SMVSAAA002" runat="server" Width="370px" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="HTML內容" Width="120px" /></td>
        <td >
            <cc1:SingleField ID="SMVSAAA003" runat="server" Height="280px" MultiLine="True" Width="370px" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="權限項目" Width="120px" /></td>
        <td >
            <cc1:SingleOpenWindowField ID="SMVSAAA005" runat="server" guidField="SMSAAAA001"
            keyField="SMSAAAA002" serialNum="001" showReadOnlyField="True" tableName="SMSAAAA"
            Width="365px" />        
        </td>
    </tr>
    <tr>
        <td colspan=2>
            <cc1:DSCCheckBox ID="SMVSAAA004" runat="server" Text="是否上線" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
