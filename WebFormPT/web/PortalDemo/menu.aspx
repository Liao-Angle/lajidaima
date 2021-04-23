<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="PortalDemo_menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        需登入<br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://127.0.0.1/webformpt/?runMethod=executeProgram&programID=InBox"
            Target="content">收件夾</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="http://127.0.0.1/webformpt/?runMethod=executeProgram&programID=InBox"
            Target="content">收件夾(關閉工具列等功能)</asp:HyperLink><br />
        <br />
        不須登入<br />
        <asp:HyperLink ID="HyperLink2" runat="server" Target="content">收件夾</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="http://127.0.0.1/webformpt/?runMethod=executeProgram&programID=InBox"
            Target="content">收件夾(關閉工具列等功能)</asp:HyperLink></div>
    </form>
</body>
</html>
