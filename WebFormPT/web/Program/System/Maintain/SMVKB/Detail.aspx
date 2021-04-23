<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVKB_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCLabel id="LSMVKBAA002" runat="server" text="群組代號" width="71px"></cc1:DSCLabel>
        <cc1:singlefield id="SMVKBAA002" runat="server" width="140px"></cc1:singlefield>
        &nbsp;<cc1:DSCLabel id="LSMVKBAA003" runat="server" text="群組名稱" width="71px"></cc1:DSCLabel><cc1:singlefield
            id="SMVKBAA003" runat="server" width="140px"></cc1:singlefield>
        <cc1:OpenWin ID="openWin1" runat="server" OnOpenWindowButtonClick="openWin1_OpenWindowButtonClick" />
        <br />
        <br />
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="成員設定" Height="" Width="400px">
             <cc1:GlassButton ID="SelectButton" runat="server" Text="選擇成員" Width="135px" OnClick="SelectButton_Click" />
            <br/> 
             <cc1:OutDataList ID="ABList" runat="server" Height="290px" Width="100%"   />
        </cc1:DSCGroupBox>
    </div>
    </form>
</body>
</html>
