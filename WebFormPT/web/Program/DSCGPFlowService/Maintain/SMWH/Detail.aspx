<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWH_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="意見群組代碼" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWHAAA002" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="意見群組名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWHAAA003" runat="server" Width="385px" ReadOnly="False" />
        &nbsp; &nbsp;<br />
        <br />
        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="說明" Width="120px" />
        <br />
        <cc1:SingleField ID="SMWHAAA004" runat="server" Width="513px" ReadOnly="False" Height="50px" MultiLine="True" />
        <br />
        <br />
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Height="174px" Text="意見內容清單" Width="514px">
            &nbsp;<cc1:DSCLabel ID="DSCLabel21" runat="server" Text="意見值" Width="54px" />
            <cc1:SingleField ID="SMWHAAB003" runat="server" Width="67px" ReadOnly="False" />
            &nbsp;<cc1:DSCLabel ID="DSCLabel22" runat="server" Text="意見內容" Width="71px" />
            <cc1:SingleField ID="SMWHAAB004" runat="server" Width="121px" ReadOnly="False" /><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="流程處理" Width="71px" />
            <cc1:SingleDropDownList ID="SMWHAAB005" runat="server" Width="94px" />
            <br />
            <br />
            &nbsp;<cc1:OutDataList ID="ListTable" runat="server" Height="264px" Width="503px" OnSaveRowData="ListTable_SaveRowData" OnShowRowData="ListTable_ShowRowData" />
        </cc1:DSCGroupBox>
    
    </div>
    </form>
</body>
</html>
