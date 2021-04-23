<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rollback.aspx.cs" Inherits="Program_DSCGPFlowService_Public_Rollback" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="font-size:10pt" width=100% >
    <tr>
        <td style="font-size:10pt" align=center><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="按下下方撤簽鈕以便進行撤簽" Width="150px" /></td>
    </tr>
    <tr>
        <td align=center>
            <cc1:GlassButton ID="SendButton" runat="server" Text="撤簽" ConfirmText="確定撤簽?" OnClick="SendButton_Click" Width="100px" ImageUrl="~/Images/OK.gif" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
