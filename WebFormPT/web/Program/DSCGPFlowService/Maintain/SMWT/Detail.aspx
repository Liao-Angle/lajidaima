<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWT_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="資料夾代碼" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWTAAA002" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="資料夾名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWTAAA003" runat="server" Width="413px" ReadOnly="False" />
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="300px" Width="524px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="流程選擇">
                <TabBody>
                    &nbsp;<cc1:DSCRadioButton ID="SMWTAAA0040" runat="server" Checked="True" GroupName="SMWTAAA004"
                        Text="所有流程, 除以下之外" />
                    <cc1:DSCRadioButton ID="SMWTAAA0041" runat="server" GroupName="SMWTAAA004" Text="僅以下流程" />
                    <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="選擇流程" Width="78px" />
                    <cc1:SingleDropDownList ID="SMWTAAB003" runat="server" Width="148px" />
                    <br />
                    &nbsp;<cc1:OutDataList ID="ABTable" runat="server" Height="162px" Width="503px" OnSaveRowData="ABTable_SaveRowData" OnShowRowData="ABTable_ShowRowData" />
                </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="角色選擇">
                <TabBody>
                    &nbsp;<cc1:DSCRadioButton ID="SMWTAAA0050" runat="server" Checked="True" GroupName="SMWTAAA005"
                        Text="所有角色, 除以下之外" />
                    <cc1:DSCRadioButton ID="SMWTAAA0051" runat="server" GroupName="SMWTAAA005" Text="僅以下角色" />
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="選擇角色" Width="78px" />
                    <cc1:SingleDropDownList ID="SMWTAAC003" runat="server" Width="148px" />
                    <br />
                    &nbsp;<cc1:OutDataList ID="ACTable" runat="server" Height="163px" Width="503px" OnSaveRowData="ACTable_SaveRowData" OnShowRowData="ACTable_ShowRowData" />
                </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
    
    </div>
    </form>
</body>
</html>
