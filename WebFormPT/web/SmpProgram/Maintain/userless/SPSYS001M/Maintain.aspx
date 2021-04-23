<%@ Page Language="C#" MasterPageFile="~/ProjectBaseWebUI/ListMaintain.master" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain_SPSYS001M_Maintain" Title="SMP共用查詢程式" %>
<%@ MasterType VirtualPath="~/ProjectBaseWebUI/ListMaintain.master" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <table>
        <tr><td>
            <asp:Label ID="Label1" runat="server" Text="Label">請選擇查詢報表</asp:Label>
        
            </td>        
            <td>
            <cc2:SingleDropDownList ID="AgentSchemaId" runat="server" 
                onselectchanged="AgentSchemaId_SelectChanged" Width="285px" />
            </td>
        </tr>
    </table>    
    </div>
</asp:Content>
