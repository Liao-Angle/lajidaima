<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVI_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="格式代碼" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMVIAAA002" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="格式名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMVIAAA003" runat="server" Width="385px" ReadOnly="False" />
        &nbsp; &nbsp;&nbsp;&nbsp;<br />
        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="說明" Width="120px" />
        <br />
        <cc1:SingleField ID="SMVIAAA004" runat="server" Width="513px" ReadOnly="False" Height="50px" MultiLine="True" />
        <br />
        <br />
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Height="354px" Text="清單" Width="514px">
            <cc1:GlassButton ID="RefreshOrderButton" runat="server" OnClick="RefreshOrderButton_Click"
                Text="重新整理順序" Width="110px" />
            <br />
            <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="順序" Width="78px" />
            <cc1:SingleField ID="SMVIAAB003" runat="server" Width="38px" ReadOnly="False" alignRight="True" Fractor="2" isMoney="True" ValueText="1" />
            &nbsp;<cc1:DSCLabel ID="DSCLabel22" runat="server" Text="類型" Width="41px" />
            &nbsp;<cc1:SingleDropDownList ID="SMVIAAB004" runat="server" Width="93px" />
            <cc1:DSCCheckBox ID="SMVIAAB006" runat="server" Text="是否為群組" />
            &nbsp;&nbsp;
            <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="字軌長度" Width="78px" />
            <cc1:SingleField ID="SMVIAAB005" runat="server" Width="38px" ReadOnly="False" alignRight="True" isMoney="True" ValueText="2" />
            <br />
            <cc1:SingleField ID="SMVIAAB007" runat="server" Height="83px" MultiLine="True" Width="496px" />
            <br />
            &nbsp;           
            <cc1:OutDataList ID="ListTable" runat="server" Height="188px" Width="503px" OnSaveRowData="ListTable_SaveRowData" OnShowRowData="ListTable_ShowRowData" OnAddOutline="ListTable_AddOutline" />
        </cc1:DSCGroupBox>
    
    </div>
    </form>
</body>
</html>
