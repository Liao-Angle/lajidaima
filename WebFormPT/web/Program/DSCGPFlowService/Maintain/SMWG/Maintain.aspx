<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWG_Maintain" %>

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
        <cc1:GlassButton ID="SaveButton" runat="server" ConfirmText="你確定要儲存嗎?" OnClick="SaveButton_Click"
            Text="儲存" Width="136px" ImageUrl="~/Images/OK.gif" />
        <br />
        <br />
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="363px" Width="636px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="顯示設定">
                    <TabBody>
                    <table border=0 cellpadding=0 cellspacing=2 width=100% >
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA002" runat="server" Text="單號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA003" runat="server" Text="流程代號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA004" runat="server" Text="流程名稱" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA005" runat="server" Text="流程實例序號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA006" runat="server" Text="主旨" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA007" runat="server" Text="重要性" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA008" runat="server" Text="填表人代號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA009" runat="server" Text="填表人姓名" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA010" runat="server" Text="填表單位代號" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA011" runat="server" Text="填表單位名稱" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA012" runat="server" Text="關係人代號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA013" runat="server" Text="關係人姓名" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA014" runat="server" Text="關係人單位代號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA015" runat="server" Text="關係人單位名稱" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA016" runat="server" Text="流程發起單位" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA017" runat="server" Text="填表日期" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA018" runat="server" Text="作業畫面識別碼" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA019" runat="server" Text="資料單頭識別碼" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA020" runat="server" Text="流程狀態" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWKAAA021" runat="server" Text="附件" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="延伸欄位設定">
                    <TabBody>
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="欄位名稱" Width="69px" />
                    <cc1:SingleDropDownList ID="SMWGAAA002" runat="server" Width="163px" />
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="變數名稱" Width="69px" />
                    <cc1:SingleField ID="SMWGAAA003" runat="server" Width="162px" />
                    <br />
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="顯示名稱" Width="69px" />
                    <cc1:SingleField ID="SMWGAAA004" runat="server" Width="162px" />
                    
                    <br />
                    <cc1:OutDataList ID="ListTable" runat="server" Height="276px" OnSaveRowData="ListTable_SaveRowData" OnShowRowData="ListTable_ShowRowData"
                        Width="637px" />
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage4" runat="server" Enabled="True" Hidden="False" ImageURL=""
                    Title="欄位順序">
                    <TabBody>
                        <table border=0 cellspacing=1 cellpadding=0>
                        <tr>
                            <td>
                                <cc1:MultiDropDownList ID="FieldOrderList" runat="server" isMultiple="false" Height="262px" Width="274px" />
                            </td>
                            <td valign=middle style="width: 51px">
                                <cc1:GlassButton ID="OrderMoveUp" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/up_small.gif" OnClick="OrderMoveUp_Click" />
                                <cc1:GlassButton ID="OrderMoveDown" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/down_small.gif" OnClick="OrderMoveDown_Click" />
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
