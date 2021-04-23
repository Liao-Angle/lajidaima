<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showDetail.aspx.cs" Inherits="Calendar_showDetail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
    <link href="Calendar.css" rel="stylesheet" type="text/css" />
    <link href="../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:dsclabel id="DSCLabel1" runat="server" text="行事曆類型" width="103px"></cc1:dsclabel>
        <cc1:singlefield id="SMVGAAA003" runat="server" width="135px" ReadOnly="True"></cc1:singlefield>
        <br />
        <cc1:dsclabel id="DSCLabel2" runat="server" text="日期" width="103px"></cc1:dsclabel>
        <cc1:singlefield id="SMVGAAA006" runat="server" width="135px" ReadOnly="True"></cc1:singlefield>
        <br />
        <cc1:dsclabel id="DSCLabel3" runat="server" text="起始時間" width="103px"></cc1:dsclabel>
        <cc1:singlefield id="SMVGAAA007" runat="server" width="135px" ReadOnly="True"></cc1:singlefield>
        <br />
        <cc1:dsclabel id="DSCLabel4" runat="server" text="結束時間" width="103px"></cc1:dsclabel>
        <cc1:singlefield id="SMVGAAA008" runat="server" width="135px" ReadOnly="True"></cc1:singlefield>
        <br />
        <cc1:dsclabel id="DSCLabel5" runat="server" text="主旨" width="103px"></cc1:dsclabel>
        <cc1:singlefield id="SMVGAAA009" runat="server" width="456px" ReadOnly="True"></cc1:singlefield>
        <br />
        <br />
        <cc1:dsclabel id="DSCLabel6" runat="server" text="內容" width="103px"></cc1:dsclabel>
        <br />
        <cc1:singlefield id="SMVGAAA010" runat="server" height="185px" multiline="True"
            width="558px" ReadOnly="True"></cc1:singlefield>
    
    </div>
    </form>
</body>
</html>
