<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectMaintain.aspx.cs" Inherits="SampleProgram_ProjectMaintain" %>

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
        <cc1:glassbutton id="SearchButton" runat="server" height="24px" text="搜尋" width="104px" OnClick="SearchButton_Click"></cc1:glassbutton>
        <cc1:glassbutton id="SaveButton" runat="server" height="24px" text="儲存" width="104px" OnClick="SaveButton_Click"></cc1:glassbutton>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" /><br />
        <br />
        <cc1:DataList ID="ProjectList" runat="server" Height="320px" Width="660px"/>
    
    </div>
    </form>
</body>
</html>
