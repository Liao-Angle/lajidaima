<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCAuthService_Maintain_SMSD_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <link href="SMSD.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=3 cellpadding=0 width=100% style="font-size:9pt">
            <tr>
                <td width=100% >
                    <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="匯出說明">
                        <cc1:DSCLabel runat="server" ID="ExplainText1" Text="1. 若選擇Excel Xml, 匯出的檔案格式為Excel的『xml試算表』文件格式, 請使用Microsoft Excel開啟後另存成『Microsoft Office Excel活頁簿』格式." />
                        <cc1:DSCLabel runat="server" ID="ExplainText2" Text="2. 當系統有設定多個資料庫且選擇Xml格式時, 可在選項後方選擇要匯出的是目前資料庫或者所有資料庫." />
                        <cc1:DSCLabel runat="server" ID="ExplainText3" Text="3. 當檔案格式選擇『xml試算表』時, 僅能選擇匯出目前資料庫設定." />
                    </cc1:DSCGroupBox>
                </td>
            </tr>
            <tr>
                <td width=100% height=100%>
                    <cc1:DSCGroupBox ID="DSCGroupBox2" runat="server" Text="匯出">
                        <asp:Label runat="server" ID="ALABEL1">選取資料庫：</asp:Label><asp:DropDownList ID="DBSetting" runat="server">
                        <asp:ListItem Value="0">目前資料庫設定</asp:ListItem>
                        <asp:ListItem Value="1">所有資料庫設定</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label runat="server" ID="ALABEL2">匯出項目 ：</asp:Label><asp:DropDownList ID="ItemType" runat="server">
                        <asp:ListItem Value="Schema">群組權限設定</asp:ListItem>
                        <asp:ListItem Value="AuthItem">權限項目設定</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label runat="server" ID="ALABEL3">檔案格式 ：</asp:Label><asp:DropDownList ID="FileFormat" runat="server" OnSelectedIndexChanged="FileFormat_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="Xml">標準Xml</asp:ListItem>
                        <asp:ListItem Value="Excel">Excel Xml試算表</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="ExportButton" runat="server" Text="匯出" OnClick="ExportButton_Click" Width="137px" />
                    </cc1:DSCGroupBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
