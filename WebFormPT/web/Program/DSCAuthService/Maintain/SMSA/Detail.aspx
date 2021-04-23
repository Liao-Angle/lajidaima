<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCAuthService_Maintain_SMSA_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="權限項目代號" Width="120px" />
        <cc1:SingleField ID="SMSAAAA002" runat="server" Width="185px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="權限項目名稱" Width="120px" />
        <cc1:SingleField ID="SMSAAAA003" runat="server" Width="185px" />
        &nbsp;<br />
        <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="說明" Width="120px" />
        <cc1:SingleField ID="SMSAAAA004" runat="server" Width="346px" />
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
