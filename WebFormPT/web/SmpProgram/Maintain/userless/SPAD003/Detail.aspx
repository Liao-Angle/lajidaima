<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_Maintain_SPAD003_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>明細</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblSeq" runat="server" Width="90px" Text="序" />
            </td>
            <td>
                <cc1:SingleField ID="Seq" runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblItemName" runat="server" Width="90px" Text="料號" />
            </td>
            <td>
                <cc1:SingleField ID="ItemName" runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblQty" runat="server" Width="90px" Text="數量" />
            </td>
            <td>
                <cc1:SingleField ID="Qty" runat="server" Width="100px" />
            </td>
        </tr>
    </table>
    <div>
        <cc1:DataList ID="ShipsList" runat="server" Height="240px" Width="600px" DialogWidth="600" />
    </div>
    </form>
</body>
</html>
