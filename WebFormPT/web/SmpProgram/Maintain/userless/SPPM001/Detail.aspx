<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_Maintain_SPPM001_Detail" %>

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
        <tr><td><cc1:DSCLabel ID="LblItemNo" runat="server" Width="90px" Text="編號" /></td>
            <td><cc1:SingleField ID="ItemNo" runat="server" Width="100px" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblItemName" runat="server" Width="90px" Text="項目" /></td>
            <td><cc1:SingleField ID="ItemName" runat="server" Width="600px" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblContent" runat="server" Width="90px" Text="考評內容" /></td>
            <td><cc1:SingleField ID="Content" runat="server" Width="600px" MultiLine="True" 
                    Height="60px" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblFractionExp" runat="server" Width="90px" Text="分數說明" /></td>
            <td><cc1:SingleField ID="FractionExp" runat="server" Width="600px" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblMinFraction" runat="server" Width="90px" Text="最低分數" /></td>
            <td><cc1:SingleField ID="MinFraction" runat="server" Width="100px" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblMaxFraction" runat="server" Width="90px" Text="最高分數" /></td>
            <td><cc1:SingleField ID="MaxFraction" runat="server" Width="100px" /></td>
        </tr>
    </table>
    </form>
</body>
</html>
