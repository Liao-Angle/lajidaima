<%@ Page Title="" Language="C#" MasterPageFile="~/ProjectBaseWebUI/ListMaintain.master" AutoEventWireup="true" CodeFile="LMaintain.aspx.cs" Inherits="SmpProgram_Maintain_SPPM002_LMaintain" %>
<%@ MasterType VirtualPath="~/ProjectBaseWebUI/ListMaintain.master" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr><td>
            <cc2:glassbutton ID="GlassButtonImport" runat="server" Height="20px" Text="執行批次匯入作業" Width="160px" />
            </td>
        </tr>
    </table>
</asp:Content>

