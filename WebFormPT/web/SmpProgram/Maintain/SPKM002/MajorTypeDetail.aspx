<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MajorTypeDetail.aspx.cs" Inherits="SmpProgram_Maintain_SPKM002_MajorTypeDetail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>輸入</title>
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table id="ToolBar" border="0" cellspacing="0" cellpadding="0" width="100%" height="25px">
            <tr>
                <td>
                    <cc1:GlassButton ID="SaveButton" runat="server" ImageUrl="~/Images/OK.gif" Text="儲存"
                        Width="80px" ConfirmText="你確定要儲存設定嗎?" OnClick="SaveButton_Click" />
                </td>
            </tr>
        </table>
        <hr />
        <fieldset>
            <legend>文件分類設定作業</legend>
            <table>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="lblName" runat="server" Text="主分類名稱" Width="90px"></cc1:DSCLabel>
                    </td>
                    <td>
                        <cc1:SingleField ID="Name" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="lblDesc" runat="server" Text="主分類描述" Width="90px"></cc1:DSCLabel>
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
                <tr>
                    <td>
                        <cc1:DSCLabel ID="lblDept" runat="server" Text="制訂部門" Width="90px"></cc1:DSCLabel>
                    </td>
                    <td>
                        <cc1:SingleOpenWindowField ID="DeptGUID" runat="server" showReadOnlyField="True"
                            Width="254px" guidField="OID" keyField="id" serialNum="001" tableName="OrgUnit" />
                    </td>
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
                    <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Title="子分類" Enabled="True" Hidden="False">
                        <TabBody>
                            <div>
                                <cc1:DataList ID="SubList" runat="server" Height="240px" Width="600px"
                                    DialogWidth="600" NoDelete="True" />
                            </div>
                        </TabBody>
                    </cc1:DSCTabPage>
                </TabPages>
            </cc1:DSCTabControl>
        </div>
    </form>
</body>
</html>
