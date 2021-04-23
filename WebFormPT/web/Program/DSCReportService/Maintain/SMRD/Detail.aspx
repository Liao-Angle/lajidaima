<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCReportService_Maintain_SMRD_Detail" %>

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
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="權限群組代號" Width="120px" /></td>
        <td >
            <cc1:SingleOpenWindowField ID="SMRDAAA002" runat="server" FixReadOnlyValueTextWidth="150px" FixValueTextWidth="150px" guidField="SMSAABA001" keyField="SMSAABA002" serialNum="001" showReadOnlyField="True" tableName="SMSAABA" Width="369px" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="Webi登入帳號" Width="120px" /></td>
        <td >
            <cc1:SingleField ID="SMRDAAA003" runat="server" Width="370px" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="Webi登入密碼" Width="120px" /></td>
        <td >
            <cc1:SingleField ID="SMRDAAA004" runat="server" Width="302px" />
            <cc1:DSCCheckBox ID="NoPWD" runat="server" OnClick="NoPWD_Click" Text="無密碼" />
            <br />
            <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="(若不修改密碼則此欄位保持空白)" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
