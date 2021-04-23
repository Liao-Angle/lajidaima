<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SampleProgram_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<cc1:DSCLabel id="DSCLabel1" runat="server" Text="起始時間" Width="94px"></cc1:DSCLabel>
        <cc1:SingleDateTimeField ID="StartDate" runat="server" Width="178px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="結束時間" Width="94px" />
        <cc1:SingleDateTimeField ID="EndDate" runat="server" Width="178px" />
    </div>
    </form>
</body>
</html>
