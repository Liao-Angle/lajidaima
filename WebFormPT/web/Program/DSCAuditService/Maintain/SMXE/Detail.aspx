<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCAuditService_Maintain_SMXE_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body style="background-color:white">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <cc1:BarGraph ID="StaticBar" runat="server" Height="442px" Width="666px" />
                </td>
            </tr>
            <tr>
                <td height=100% >
                    <asp:Literal ID="ListData" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />&nbsp;</div>
    </form>
</body>
</html>
