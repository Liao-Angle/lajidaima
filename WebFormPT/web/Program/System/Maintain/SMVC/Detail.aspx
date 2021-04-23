<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVC_Detail" %>

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
        <cc1:SingleDropDownList ID="SMVCAAA003" runat="server" ReadOnly="True"
            Width="181px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="通知時間" Width="120px" />
        
        <cc1:SingleField ID="SMVCAAA004" runat="server" Width="180px" ReadOnly="True" />
        <br />
        <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="是否已讀取" Width="120px" />
        <cc1:SingleDropDownList ID="SMVCAAA005" runat="server" ReadOnly="True" Width="181px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="是否已通知" Width="120px" />
        <cc1:SingleDropDownList ID="SMVCAAA006" runat="server" ReadOnly="True" Width="181px" />
        <br /><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="標題" Width="120px" />
        <cc1:SingleField ID="SMVCAAA007" runat="server" Width="385px" ReadOnly="True" />
        <br />
        <br />
        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="訊息內容" Width="120px" />
        <br />
        <cc1:SingleField ID="SMVCAAA008" runat="server" Width="506px" Height="136px" MultiLine="True" ReadOnly="True" />
        <br />
        <br />
        <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="訊息網址" Width="120px" />
        <cc1:SingleField ID="SMVCAAA009" runat="server" Width="380px" ReadOnly="True" />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
