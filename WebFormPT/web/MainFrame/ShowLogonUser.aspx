<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowLogonUser.aspx.cs" Inherits="MainFrame_ShowLogonUser" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="380px" Width="584px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" Hidden="False" ImageURL=""
                    Title="訊息發送">
                    <TabBody>
                    <table border=0 cellspacing=5 cellpadding=0 width=540px>
                        <tr>
                        <td colspan=3><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="發送訊息给勾選的使用者:" /></td>
                        </tr>
                        <tr>
                        <td><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="標題: " Width="100px"/></td>
                        <td><cc1:SingleField ID="MessageTitle" runat="server" Width=350px/></td>
                        <td>
                            <cc1:SingleDropDownList ID="MessageType" runat="server" />
                        </td>
                        </tr>
                        <tr>
                        <td valign=top><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="訊息內容: " Width="100px"/></td>
                        <td colspan=2><cc1:SingleField ID="MessageContent" runat="server" Width="100%" MultiLine="True"/></td>
                        </tr>
                        <tr>
                        <td><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="URL: " Width="100px"/></td>
                        <td><cc1:SingleField ID="MessageURL" runat="server" Width=350px/></td>
                        <td>
                        <cc1:GlassButton ID="SendMessage" runat="server" Height="18px" Text="發送" Width="103px" CssClass="GlassButton" OnClick="SendMessage_Click" />
                        </td>
                        </tr>
                        <tr>
                        <td colspan=3><cc1:OutDataList ID="DList" runat="server" Height="210px" Width="556px" /></td>
                        </tr>
                    </table>                    
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" Hidden="False" ImageURL=""
                    Title="切換使用者">
                    <TabBody>
                    <table border=0 cellspacing=5 cellpadding=0 width=540>
                        <tr>
                        <td><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="切換帳號:" /></td>
                        <td>
                            <cc1:SingleOpenWindowField ID="SHIFTUSER" runat="server" guidField="OID" keyField="id" keyFieldType="STRING" serialNum="001" showReadOnlyField="true" tableName="Users" FixReadOnlyValueTextWidth="150" />
                        </td>
                        </tr>
                        <tr>
                            <td colspan=2 align=center>
                                <cc1:GlassButton ID="SHIFTBUTTON" runat="server" OnClick="SHIFTBUTTON_Click" Text="切換"
                                    Width="108px" />
                            </td>
                        </tr>
                    </table>
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
    </div>        
    </form>
</body>
</html>
