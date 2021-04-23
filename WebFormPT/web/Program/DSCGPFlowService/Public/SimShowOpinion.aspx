<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SimShowOpinion.aspx.cs" Inherits="Program_DSCGPFlowService_Public_SimShowOpinion" %>

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
    <table border=0 width=100% align=center>
        <tr>
            <td align=center>
                <cc1:DSCLabel ID="SignLabel" runat="server" Text="請問是否確認?" Width="200px" />
            </td>
        </tr>
        <tr>
            <td align=center valign=middle>
                <cc1:GlassButton ID="SendButton" runat="server" Text="送出簽核" OnClick="SendButton_Click" Width="100px" ImageUrl="~/Images/OK.gif" />
                <cc1:GlassButton ID="StopButton" runat="server" Text="取消簽核" OnClick="StopButton_Click" Width="100px" ImageUrl="~/Images/NO.gif" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
