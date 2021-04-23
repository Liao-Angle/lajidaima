<%@ Page Language="C#" AutoEventWireup="true" CodeFile="md5.aspx.cs" Inherits="md5" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:BYWin ID="BYWin1" runat="server" guidField="OID" Height="183px" keyField="id"
            outputIndex="1" serialNum="001" tableName="Users" Width="447px" />
        <br />
        <cc1:RoutineWizard ID="RoutineWizard1" runat="server" Width="659px" />
        <br />
        <cc1:SingleDateTimeField ID="SingleDateTimeField1" runat="server" Width="438px" />
        <br />
        <cc1:SingleOpenWindowField ID="SingleOpenWindowField1" runat="server" showReadOnlyField="True"
            Width="480px" />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
            Height="421px" ProcessingMode="Remote" Width="660px">
            <ServerReport ReportPath="/MingTaiReport/IF04/?IRAV003=1&amp;IRAV013=1&amp;IRAB003_1=1&amp;IRAB003_2=1'"
                ReportServerUrl="http://10.20.10.19/reportserver" />
        </rsweb:ReportViewer>
    </form>
</body>
</html>
