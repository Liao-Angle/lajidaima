<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPSMWK_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>輸入</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td><cc1:DSCLabel id="LblUserGUID" runat="server" Text="員工" Width="120px" 
                TextAlign="3"></cc1:DSCLabel></td>
            <td><cc1:SingleOpenWindowField ID="UserGUID" runat="server" 
                    showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                    serialNum="001" tableName="Users" /></td>
        </tr>
        <tr><td><cc1:DSCLabel id="LblPrivilegeType" runat="server" Text="權限種類" 
                Width="120px" TextAlign="3"></cc1:DSCLabel></td>
            <td><cc1:SingleDropDownList ID="PrivilegeType" runat="server" Width="140px" 
                    onselectchanged="PrivilegeType_SelectChanged" /></td>
        </tr>
        <tr><td><cc1:DSCLabel id="LblFlowGUID" runat="server" Text="流程/作業畫面" Width="120px" 
                TextAlign="3"></cc1:DSCLabel></td>
            <td><cc1:SingleOpenWindowField ID="FlowGUID" runat="server" showReadOnlyField="True" Width="480px"/></td>
        </tr>
        <tr><td><cc1:DSCLabel id="LblActive" runat="server" Text="生失效" Width="120px" 
                TextAlign="3"></cc1:DSCLabel></td>
            <td><cc1:SingleDropDownList ID="Active" runat="server" Width="50px" /></td>
        </tr>
    </table> 
    </form>
</body>
</html>
