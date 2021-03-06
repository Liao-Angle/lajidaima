<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail_1_2.aspx.cs" Inherits="SmpProgram_Maintain_SPKM001_Detail_1_2" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>子分類輸入</title>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset>
       <legend>文件分類設定作業-子分類</legend>
    <table>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblMajorName" runat="server" Text="主分類名稱" Width="90px"></cc1:DSCLabel>
            </td>
            <td>
                <cc1:SingleField ID="MajorName" runat="server" Width="200px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblMajorDesc" runat="server" Text="主分類描述" Width="90px"></cc1:DSCLabel>
            </td>
            <td>
                <cc1:SingleField ID="MajorDesc" runat="server" Width="300px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblName" runat="server" Text="子分類名稱" Width="90px"></cc1:DSCLabel>
            </td>
            <td>
                <cc1:SingleField ID="Name" runat="server" Width="200px" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblDesc" runat="server" Text="子分類描述" Width="90px"></cc1:DSCLabel>
            </td>
            <td>
                <cc1:SingleField ID="Desc" runat="server" Width="300px" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblEnable" runat="server" Text="生失效" Width="90px"></cc1:DSCLabel>
            </td>
            <td>
                <cc1:SingleDropDownList ID="Enable" runat="server" Width="50px" />
            </td>
        </tr>
    </table>
    </fieldset>
    <div>
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="280px" Width="600px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Title="管理者" Enabled="True" Hidden="False">
                    <TabBody>
                        <div>
                            <cc1:DataList ID="AdmList" runat="server" Height="240px" Width="600px" DialogWidth="600" />
                        </div>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Title="歸屬群組" Enabled="True" Hidden="False">
                    <TabBody>
                        <div>
                            <asp:Label ID="Label1" runat="server" Text="*歸屬群組為文件的製作單位，擁有文件變更/作廢/閱讀各版本附件等權限" ForeColor="Red"></asp:Label>
                            <cc1:DataList ID="GroupList" runat="server" Height="240px" Width="600px" DialogWidth="600" />
                        </div>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage3" runat="server" Title="文件類別" Enabled="True" Hidden="False">
                    <TabBody>
                        <div>
                            <cc1:DataList ID="DocList" runat="server" Height="240px" Width="600px" DialogWidth="600" NoDelete="True" />
                        </div>
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
    </div>

    </form>
</body>
</html>
