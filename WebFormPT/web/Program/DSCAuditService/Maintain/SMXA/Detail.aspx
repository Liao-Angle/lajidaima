<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCAuditService_Maintain_SMXA_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="應用程式代號" Width="120px" />
        <cc1:SingleField ID="SMXAAAA002" runat="server" Width="185px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="應用程式名稱" Width="120px" />
        <cc1:SingleField ID="SMXAAAA003" runat="server" Width="381px" />
        &nbsp;&nbsp;<br />
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
