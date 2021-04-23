<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWB_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="流程OID" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWBAAA002" runat="server" Width="385px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="流程代號" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWBAAA003" runat="server" Width="385px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="流程名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWBAAA004" runat="server" Width="385px" ReadOnly="False" />
        &nbsp;<br />
        <cc1:DSCCheckBox ID="SMWBAAA006" runat="server" Text="是否顯示表單資訊" /><cc1:DSCCheckBox ID="SMWBAAA005" runat="server" Text="是否顯示簽核意見" /><cc1:DSCCheckBox ID="SMWBAAA007" runat="server" Text="是否紀錄附件人員時間" />
        <br />
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Height="243px" Text="流程參數" Width="516px">
            &nbsp;<cc1:OutDataList ID="ABList" runat="server" Height="216px" ReadOnly="True"
                Width="506px" />
        </cc1:DSCGroupBox>
    </div>
    </form>
</body>
</html>
