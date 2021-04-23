<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCAuditService_Maintain_SMXB_Detail" %>

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
        <cc1:SingleOpenWindowField ID="SMXBAAA002" runat="server" showReadOnlyField="True"
            Width="290px" guidField="SMXAAAA001" keyField="SMXAAAA002" serialNum="001" tableName="SMXAAAA" />
        <br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="模組代號" Width="120px" />
        <cc1:SingleField ID="SMXBAAA003" runat="server" Width="281px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="模組名稱" Width="120px" />
        <cc1:SingleField ID="SMXBAAA004" runat="server" Width="378px" />
        <br /><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="通知人員" Width="120px" />
        <cc1:SingleField ID="SMXBAAA005" runat="server" Width="315px" />
        <cc1:GlassButton ID="GlassButton1" runat="server" Text="選擇" Width="59px" OnClick="GlassButton1_Click" />
        <br />
        <cc1:DSCCheckBox ID="SMXBAAA006" runat="server" Text="是否Email通知" />
        &nbsp;<cc1:DSCCheckBox ID="SMXBAAA007" runat="server" Text="是否警示" />
        &nbsp;<cc1:DSCCheckBox ID="SMXBAAA008" runat="server" Checked="True" Text="是否紀錄Log" />
        <br />
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Height="294px" Text="程式設定" Width="518px">
            <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="程式代號" Width="120px" />
            <cc1:SingleField ID="SMXBAAB003" runat="server" Width="281px" />
            <br />
            <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="層級" Width="120px" />
            <cc1:SingleDropDownList ID="SMXBAAB004" runat="server" Width="278px" />
            <br />
            <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="通知人員" Width="120px" />
            <cc1:SingleField ID="SMXBAAB005" runat="server" Width="315px" />
            <cc1:GlassButton ID="GlassButton2" runat="server" Text="選擇" Width="59px" OnClick="GlassButton2_Click" />
            <cc1:DSCCheckBox ID="SMXBAAB006" runat="server" Text="是否Email通知" />
            <cc1:DSCCheckBox ID="SMXBAAB007" runat="server" Text="是否警示" />
            <cc1:DSCCheckBox ID="SMXBAAB008" runat="server" Checked="True" Text="是否紀錄Log" />
            <cc1:OutDataList ID="DetailList" runat="server" Height="213px" Width="509px" OnSaveRowData="DetailList_SaveRowData" OnShowRowData="DetailList_ShowRowData" />
        </cc1:DSCGroupBox>
        <cc1:OpenWin ID="OpenWin1" runat="server" OnOpenWindowButtonClick="OpenWin1_OpenWindowButtonClick" />
        <cc1:OpenWin ID="OpenWin2" runat="server" OnOpenWindowButtonClick="OpenWin2_OpenWindowButtonClick" />
    
    </div>
    </form>
</body>
</html>
