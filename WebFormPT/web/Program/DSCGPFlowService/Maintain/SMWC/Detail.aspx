<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWC_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="角色名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWCAAA002" runat="server" Width="385px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="系統/流程引擎" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleDropDownList ID="SMWCAAA003" runat="server" Width="385px" />
        <br />
    </div>
    </form>
</body>
</html>
