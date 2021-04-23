<%@ Page Language="C#" MasterPageFile="~/ProjectBaseWebUI/WebiReportParameter.master" AutoEventWireup="true" CodeFile="Webi2423.aspx.cs" Inherits="Program_System_Report_Webi2423" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/ProjectBaseWebUI/WebiReportParameter.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:dsclabel id="DSCLabel1" runat="server" text="客戶統編" width="73px"></cc1:dsclabel>
    <cc1:singlefield id="CKey" runat="server"></cc1:singlefield>
</asp:Content>
