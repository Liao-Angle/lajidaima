<%@ Page Language="C#" MasterPageFile="~/ProjectBaseWebUI/ListMaintain.master" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWB_Maintain" Title="作業畫面設定作業" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/ProjectBaseWebUI/ListMaintain.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<cc1:dsccheckbox id="SyncMode" runat="server" text="同步更新時是否移除系統段資料"></cc1:dsccheckbox>
    <cc1:glassbutton id="SyncButton" runat="server" height="19px" onclick="SyncButton_Click"
        text="同步更新" width="222px" ConfirmText="確定要進行同步更新嗎?" showWaitingIcon="True"></cc1:glassbutton>
</asp:Content>

