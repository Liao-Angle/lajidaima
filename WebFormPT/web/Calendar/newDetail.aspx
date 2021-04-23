<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newDetail.aspx.cs" Inherits="Calendar_newDetail" %>

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
        &nbsp;<cc1:SingleDropDownList ID="SMVGAAA003" runat="server" Width="146px" />
        <br />
        <cc1:dsclabel id="DSCLabel2" runat="server" text="日期" width="103px"></cc1:dsclabel>
        &nbsp;<cc1:SingleDateTimeField ID="SMVGAAA006" runat="server" Width="143px" />
        <br />
        <cc1:dsclabel id="DSCLabel3" runat="server" text="起始時間" width="103px"></cc1:dsclabel>
        &nbsp;<cc1:SingleDropDownList ID="SMVGAAA007" runat="server" Width="146px" />
        <br />
        <cc1:dsclabel id="DSCLabel4" runat="server" text="結束時間" width="103px"></cc1:dsclabel>
        &nbsp;<cc1:SingleDropDownList ID="SMVGAAA008" runat="server" Width="146px" />
        <br />
        <cc1:dsclabel id="DSCLabel5" runat="server" text="主旨" width="103px"></cc1:dsclabel>
        &nbsp;<cc1:singlefield id="SMVGAAA009" runat="server" width="446px" ReadOnly="False"></cc1:singlefield>
        <br />
        <br />
        <cc1:dsclabel id="DSCLabel6" runat="server" text="內容" width="103px"></cc1:dsclabel>
        <br />
        <cc1:singlefield id="SMVGAAA010" runat="server" height="185px" multiline="True"
            width="558px" ReadOnly="False"></cc1:singlefield>
        <br />
        <cc1:GlassButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="儲存"
            Width="121px" ConfirmText="你確定要儲存此行事曆項目嗎?" />
    
    </div>
    </form>
</body>
</html>
