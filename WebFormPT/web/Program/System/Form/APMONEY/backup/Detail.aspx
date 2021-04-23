<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Form_APMONEY_Detail" %>

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
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="說明" Width="120px" /></td>
        <td ><cc1:SingleField ID="APDETAIL002" runat="server" Width="385px" Height="100px" ReadOnly="False" MultiLine="True" /></td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="金額" Width="120px" /></td>
        <td ><cc1:SingleField ID="APDETAIL003" runat="server" Width="385px" Height="21px" ReadOnly="False" alignRight="True" isAccount="True" isMoney="true" /></td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        <cc1:GlassButton ID="GlassButtonDownload" runat="server" Height="20px" 
                            Text="下載" Width="40px" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
