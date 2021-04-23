<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCBatchService_Maintain_SMTC_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>程式代碼輸入畫面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="批次作業代碼" Width="120px" />
        &nbsp;&nbsp;&nbsp; &nbsp;
        <cc1:SingleField ID="SMTCAAA002" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="分段順序" Width="120px" />
        &nbsp;&nbsp;&nbsp; &nbsp;
        <cc1:SingleField ID="SMTCAAA003" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="執行起始時間" Width="120px" />
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<cc1:SingleField ID="SMTCAAA004" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="執行結束時間" Width="119px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<cc1:SingleField ID="SMTCAAA005" runat="server" Width="178px" ReadOnly="False" />
        &nbsp;&nbsp;

        <br />
       <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="執行狀態" Width="119px" />
        &nbsp;&nbsp;&nbsp; &nbsp; <cc1:SingleField ID="SMTCAAA006" runat="server" Width="178px" ReadOnly="False" />
        <br />       
        <br />
        <cc1:DSCGroupBox ID="gpbox" Text="處理進度" runat="server">
                        &nbsp;<cc1:OutDataList ID="OutDataList1" runat="server"  NoAdd="True" NoAllDelete="True" NoDelete="True" NoModify="True" Width="500px" Height="300px" />
        </cc1:DSCGroupBox>
        <br />
    </div>
    </form>
</body>
</html>
