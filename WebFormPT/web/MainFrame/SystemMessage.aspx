<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemMessage.aspx.cs" Inherits="MainFrame_SystemMessage" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 width=300 height=100% style="font-size:9pt">
        <tr>
            <td width=80px nowrap=true><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string005", "訊息類型")%>：</td>
            <td><asp:Literal ID="MessageType" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td width=80px><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string006", "通知時間")%>：</td>
            <td><asp:Literal ID="AlertTime" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td width=80px><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string007", "標題")%>：</td>
            <td><asp:Literal ID="Title" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td width=80px><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string008", "相關聯結")%>：</td>
            <td><asp:Literal ID="URL" runat="server"></asp:Literal></td>
        </tr>
        <tr height=100%>
            <td valign="top"><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string009", "訊息內容")%>：</td>
            <td valign="top"><asp:Literal ID="Content" runat="server"></asp:Literal></td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
