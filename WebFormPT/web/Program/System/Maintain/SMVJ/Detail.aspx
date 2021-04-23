<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVJ_Detail" %>

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
        <cc1:DSCGroupBox id="RelationBox" runat="server" height="190px" text="物件關聯" width="637px">
            <asp:Panel ID="TreePanel" runat="server" Height="150px" Width="628px" ScrollBars="Vertical">
            </asp:Panel>
        </cc1:DSCGroupBox>
            
        <br />
        <br />
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="337px" Width="635px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" Title="物件基本資料">
                    <TabBody>
                    <table border=0 cellpadding=0 cellspacing=0 style="width:613px">
                    <tr>
                        <td>
                        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="組件名稱" Width="104px" />
                        </td>
                        <td>
                        <cc1:DSCLabel ID="TAssemblyName" runat="server" Width="509px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="完整類別名稱" Width="104px" />
                        </td>
                        <td>
                        <cc1:DSCLabel ID="TChildClassString" runat="server" Width="509px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="資料表" Width="104px" />
                        </td>
                        <td>
                        <cc1:DSCLabel ID="TTableName" runat="server" Width="509px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="TimeStamp檢查" Width="104px" />
                        </td>
                        <td>
                        <cc1:DSCLabel ID="TTimeStamp" runat="server" Width="509px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="識別欄位" Width="104px" />
                        </td>
                        <td>
                        <cc1:DSCLabel ID="TIdentityField" runat="server" Width="509px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="鍵值欄位" Width="104px" />
                        </td>
                        <td>
                        <cc1:DSCLabel ID="TKeyField" runat="server" Width="509px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign=top>
                        <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="不更新欄位" Width="104px" />
                        </td>
                        <td>
                        <cc1:SingleField ID="TNonUpdateField" runat="server" MultiLine="True" Width="500px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign=top>
                        <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="允許空白欄位" Width="104px" />
                        </td>
                        <td>
                        <cc1:SingleField ID="TAllowEmptyField" runat="server" MultiLine="True" Width="500px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign=top>
                        <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="查詢字串" Width="104px" />
                        </td>
                        <td>
                        <cc1:SingleField ID="TQueryString" runat="server" MultiLine="True" Width="500px"/>
                        </td>
                    </tr>
                    </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" Title="欄位內容">
                    <TabBody>
                        <cc1:DataList ID="FieldList" runat="server" Height="305px" Width="623px" />
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
    
    </div>
    </form>
</body>
</html>
