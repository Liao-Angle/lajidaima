<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVH_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="參數代號" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMVHAAA002" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="參數名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMVHAAA003" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="參數內容值" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMVHAAA004" runat="server" Width="385px" ReadOnly="False" />
        <br />
        <cc1:DSCCheckBox ID="SMVHAAA006" runat="server" Text="是否加密" 
            onclick="SMVHAAA006_Click" />
        <br />
        <cc1:DSCRadioButton ID="drrDefaultKey" runat="server" GroupName="KG"  ReadOnly="true"
            Text="預設加密金鑰" onclick="drrDefaultKey_Click" />
        <br />
        <cc1:DSCRadioButton ID="drrCustomKey" runat="server" GroupName="KG"  ReadOnly="true"
            onclick="drrCustomKey_Click" Text="自訂加密金鑰，類別名稱" />                                   
            <cc1:SingleField ID="SMVHAAA008" runat="server" Width="300px"  ReadOnly="True" />
        <br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="說明" Width="120px" />
        &nbsp;&nbsp;&nbsp;&nbsp;<br />
        <cc1:SingleField ID="SMVHAAA005" runat="server" Height="106px" MultiLine="True"
            Width="514px" />
        <br />
        <br />
        &nbsp;&nbsp; &nbsp;<br />
    
    </div>
    </form>
</body>
</html>
