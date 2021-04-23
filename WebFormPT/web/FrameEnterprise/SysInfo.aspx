<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysInfo.aspx.cs" Inherits="FrameEnterprise_SysInfo" %>

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
        <asp:Literal ID="ServerInfo" runat="server" ></asp:Literal>
        <br />        
        <asp:Literal ID="VersionFile" runat="server" ></asp:Literal>
        <br />
        <br />
        <cc1:GlassButton ID="HelpButton" runat="server" OnClick="HelpButton_Click" Text="線上說明" Width="88px" /><cc1:GlassButton ID="DefaultObjectButton" runat="server" OnClick="DefaultObjectButton_Click" Text="平台物件說明" Width="105px" /><cc1:GlassButton ID="ProjectObjectButton" runat="server" OnClick="ProjectObjectButton_Click" Text="專案物件說明" Width="105px" />
    </div>
    </form>
</body>
</html>
