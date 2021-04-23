<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVF_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="類型代號" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMVFAAA002" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="類型名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMVFAAA003" runat="server" Width="385px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="是否使用" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleDropDownList ID="SMVFAAA004" runat="server" ReadOnly="False"
            Width="71px" />
        &nbsp;
        <br /><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="顏色R(0-255)" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMVFAAA005" runat="server" Width="73px" ReadOnly="False" ValueText="12" />
        &nbsp;<br />
        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="顏色G(0-255)" Width="120px" />
        &nbsp;
        <cc1:SingleField ID="SMVFAAA006" runat="server" Width="73px" ReadOnly="False" ValueText="12" /><br /><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="顏色B(0-255)" Width="120px" />
        &nbsp;
        <cc1:SingleField ID="SMVFAAA007" runat="server" Width="73px" ReadOnly="False" ValueText="12" />
        <br />
        &nbsp;&nbsp; &nbsp;<br />
    
    </div>
    </form>
</body>
</html>
