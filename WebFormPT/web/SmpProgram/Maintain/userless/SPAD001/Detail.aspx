<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>明細</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td><cc1:DSCLabel ID="lblStateNo" runat="server" Width="90px" Text="關號" /></td>
            <td><cc1:SingleField ID="StateNo" runat="server" Width="100px" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="lblStateType" runat="server" Width="90px" Text="流程角色" /></td>
            <td><cc1:SingleDropDownList ID="StateType" runat="server" Width="100px" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="lblSignType" runat="server" Width="90px" Text="簽核種類" /></td>
            <td><cc1:SingleDropDownList ID="SignType" runat="server" Width="100px" 
                    onselectchanged="SignType_SelectChanged" />
            </td>
        </tr>
        <tr><td><cc1:DSCLabel ID="lblStateValueGUID" runat="server" Width="90px" Text="流程角色參數" /></td>
            <td><cc1:SingleOpenWindowField ID="StateValueGUID" runat="server" 
                    showReadOnlyField="True" Width="254px" guidField="OID" keyField="id" 
                    serialNum="001" tableName="Users" IgnoreCase="True" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
