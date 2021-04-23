<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCAuthService_Maintain_SMSE_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=3 cellpadding=0 width=100% style="font-size:9pt">
            <tr>
                <td width=100% >
                    <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">
                    <table border=0 cellspacing=0 cellpadding=3>
                    <tr>
                        <td><cc1:DSCLabel ID="DSCLabel1" runat="server" Width="100px" Text="請選擇使用者："/></td>
                        <td><cc1:SingleOpenWindowField ID="CheckUser" runat="server" showReadOnlyField="true" guidField="OID" keyField="id" keyFieldType="STRING" serialNum="001" tableName="Users" Width="218px" /></td>
                        <td><cc1:DSCLabel ID="DSCLabel2" runat="server" Width="100px" Text="請選擇作業："/></td>
                        <td><cc1:SingleOpenWindowField ID="CheckProgram" runat="server" showReadOnlyField="true" guidField="SMVAAAB001" keyField="SMVAAAB002" keyFieldType="STRING" serialNum="001" tableName="SMVAAAB" Width="222px" /></td>
                    </tr>
                    <tr>
                        <td colspan=2>
                            <cc1:DSCCheckBox ID="ListHave" runat="server" Text="僅列出有權限的程式項目"/>
                        </td>
                        <td colspan=2 align=right><cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" Text="開始查詢" Width="102px" OnClick="SearchButton_Click" showWaitingIcon="True" /></td>
                    </tr>
                    </table>
                    </cc1:DSCGroupBox>
                </td>
            </tr>
            <tr>
                <td width=100% height=100%>
                    <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="360px" Width="100%">
                        <TabPages>
                            <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="隸屬群組">
                                <TabBody>
                        <cc1:OutDataList ID="GroupList" runat="server" Height="315px" Width="100%" />
                                </TabBody>
                            </cc1:DSCTabPage>
                            <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="使用者權限">
                                <TabBody>
                        <cc1:OutDataList ID="AuthList" runat="server" Height="319px" Width="100%" />
                                </TabBody>
                            </cc1:DSCTabPage>
                        </TabPages>
                    </cc1:DSCTabControl>
                </td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
