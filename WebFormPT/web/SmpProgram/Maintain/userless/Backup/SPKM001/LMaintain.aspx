<%@ Page Title="" Language="C#" MasterPageFile="~/ProjectBaseWebUI/ListMaintainWithoutAuth.master"
    AutoEventWireup="true" CodeFile="LMaintain.aspx.cs" Inherits="SmpProgram_Maintain_SPKM001_LMaintain" %>

<%@ MasterType VirtualPath="~/ProjectBaseWebUI/ListMaintainWithoutAuth.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/SmpProgram/Maintain/PDFViewer/ViewDocument.aspx"
        ToolTip="View PDF" Visible="False">View Document</asp:HyperLink>

</asp:Content>
