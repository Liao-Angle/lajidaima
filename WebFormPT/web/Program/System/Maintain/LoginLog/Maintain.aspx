<%@ Page Language="C#" MasterPageFile="~/ProjectBaseWebUI/ReadOnlyMaintain.master" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_System_Maintain_LoginLog_Maintain" Title="登入紀錄檢視作業" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/ProjectBaseWebUI/ReadOnlyMaintain.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:GlassButton id="ClearLogButton" runat="server" width="203px" Text="清除所有記錄" OnClick="ClearLogButton_Click" ConfirmText="你確定要刪除所有登入紀錄嗎?"></cc1:GlassButton>
    &nbsp;<cc1:GlassButton ID="UpdateButton" runat="server" Text="註記所有未登出資訊" Width="203px" OnClick="UpdateButton_Click" ConfirmText="你確定要註記所有未登出紀錄嗎?" />
    <br />
    <br />
</asp:Content>

