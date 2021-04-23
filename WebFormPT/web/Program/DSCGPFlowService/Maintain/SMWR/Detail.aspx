<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWR_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>程式代碼輸入畫面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="資料夾代碼" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWRAAA022" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="資料夾名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWRAAA023" runat="server" Width="413px" ReadOnly="False" />
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="300px" Width="524px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="流程選擇">
                <TabBody>
                    &nbsp;<cc1:DSCRadioButton ID="SMWRAAA0240" runat="server" Checked="True" GroupName="SMWRAAA024"
                        Text="所有流程, 除以下之外" />
                    <cc1:DSCRadioButton ID="SMWRAAA0241" runat="server" GroupName="SMWRAAA024" Text="僅以下流程" />
                    <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="選擇流程" Width="78px" />
                    <cc1:SingleDropDownList ID="SMWRAAB003" runat="server" Width="148px" />
                    <br />
                    &nbsp;<cc1:OutDataList ID="ABTable" runat="server" Height="162px" Width="503px" OnSaveRowData="ABTable_SaveRowData" OnShowRowData="ABTable_ShowRowData" />
                </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage3" runat="server" Enabled="True" ImageURL="" Title="欄位顯示">
                <TabBody>
                    <table border=0 cellpadding=0 cellspacing=2 width=100% >
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA002" runat="server" Text="單號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA003" runat="server" Text="流程代號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA004" runat="server" Text="流程名稱" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA005" runat="server" Text="流程實例序號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA006" runat="server" Text="主旨" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA007" runat="server" Text="重要性" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA008" runat="server" Text="填表人代號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA009" runat="server" Text="填表人姓名" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA010" runat="server" Text="填表單位代號" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA011" runat="server" Text="填表單位名稱" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA012" runat="server" Text="關係人代號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA013" runat="server" Text="關係人姓名" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA014" runat="server" Text="關係人單位代號" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA015" runat="server" Text="關係人單位名稱" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA016" runat="server" Text="流程發起單位" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA017" runat="server" Text="填表日期" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA018" runat="server" Text="作業畫面識別碼" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA019" runat="server" Text="資料單頭識別碼" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA020" runat="server" Text="流程狀態" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMWRAAA021" runat="server" Text="附件" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
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
