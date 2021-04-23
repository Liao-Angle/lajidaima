<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVD_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="訊息類型" Width="120px" />
        &nbsp;<cc1:SingleDropDownList ID="SMVDAAA003" runat="server" ReadOnly="False"
            Width="181px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="通知時間" Width="120px" />
        &nbsp;<cc1:SingleDateTimeField ID="SMVDAAA004" runat="server" Width="213px" DateTimeMode="1" />
        <br />
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="標題" Width="120px" />
        &nbsp;<cc1:SingleField ID="SMVDAAA007" runat="server" Width="385px" ReadOnly="False" />
        <br />
        <br />
        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="訊息內容" Width="120px" />
        <br />
        <cc1:SingleField ID="SMVDAAA008" runat="server" Width="506px" Height="136px" MultiLine="True" ReadOnly="False" />
        <br />
        <br />
        <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="訊息網址" Width="120px" />
        &nbsp;<cc1:SingleField ID="SMVDAAA009" runat="server" Width="380px" ReadOnly="False" />
        &nbsp;<br />
        <br />
    
    </div>
    </form>
</body>
</html>
