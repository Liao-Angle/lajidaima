<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWI_Detail" %>

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
        &nbsp;&nbsp;<cc1:SingleField ID="SMWIAAA002" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="資料夾名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWIAAA003" runat="server" Width="413px" ReadOnly="False" />
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="類型" Width="39px" />
        <cc1:SingleDropDownList ID="SMWIAAA007" runat="server" OnSelectChanged="SMWIAAA007_SelectChanged"
            Width="116px" />
        <cc1:DSCCheckBox ID="SMWIAAA006" runat="server" Text="是否允許群簽" OnClick="SMWIAAA006_Click" /><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="意見群組" Width="60px" />
        <cc1:SingleOpenWindowField ID="SMWIAAA008" runat="server" guidField="SMWHAAA001"
            keyField="SMWHAAA002" serialNum="001" showReadOnlyField="True" tableName="SMWHAAA"
            Width="220px" />
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="300px" Width="524px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="流程選擇">
                <TabBody>
                    &nbsp;<cc1:DSCRadioButton ID="SMWIAAA0040" runat="server" Checked="True" GroupName="SMWIAAA004"
                        Text="所有流程, 除以下之外" />
                    <cc1:DSCRadioButton ID="SMWIAAA0041" runat="server" GroupName="SMWIAAA004" Text="僅以下流程" />
                    <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="選擇流程" Width="78px" />
                    <cc1:SingleDropDownList ID="SMWIAAB003" runat="server" Width="148px" />
                    <br />
                    &nbsp;<cc1:OutDataList ID="ABTable" runat="server" Height="162px" Width="503px" OnSaveRowData="ABTable_SaveRowData" OnShowRowData="ABTable_ShowRowData" />
                </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="角色選擇">
                <TabBody>
                    &nbsp;<cc1:DSCRadioButton ID="SMWIAAA0050" runat="server" Checked="True" GroupName="SMWIAAA005"
                        Text="所有角色, 除以下之外" />
                    <cc1:DSCRadioButton ID="SMWIAAA0051" runat="server" GroupName="SMWIAAA005" Text="僅以下角色" />
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="選擇角色" Width="78px" />
                    <cc1:SingleDropDownList ID="SMWIAAC003" runat="server" Width="148px" />
                    <br />
                    &nbsp;<cc1:OutDataList ID="ACTable" runat="server" Height="163px" Width="503px" OnSaveRowData="ACTable_SaveRowData" OnShowRowData="ACTable_ShowRowData" />
                </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage3" runat="server" Enabled="True" ImageURL="" Title="欄位顯示">
                <TabBody>
                    <table border=0 cellpadding=0 cellspacing=3>
                    <tr>
                        <td>
                            <cc1:DSCCheckBox ID="CURRENTSTATE" runat="server" Text="是否顯示流程狀態" />
                        </td>
                         <td>
                            <cc1:DSCCheckBox ID="PROCESSNAME" runat="server" Text="是否顯示流程名稱"  />
                        </td>
                         <td>
                            <cc1:DSCCheckBox ID="IMPORTANT" runat="server" Text="是否顯示重要性"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:DSCCheckBox ID="SHEETNO" runat="server" Text="是否顯示單號"  />
                        </td>
                         <td>
                            <cc1:DSCCheckBox ID="WORKITEMNAME" runat="server" Text="是否顯示角色名稱"  />
                        </td>
                        <td>
                            <cc1:DSCCheckBox ID="ATTACH" runat="server" Text="是否顯示附件"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:DSCCheckBox ID="SUBJECT" runat="server" Text="是否顯示主旨"  />
                        </td>
                         <td>
                            <cc1:DSCCheckBox ID="USERNAME" runat="server" Text="是否顯示填表人"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:DSCCheckBox ID="CREATETIME" runat="server" Text="是否顯示填表時間"  />
                        </td>
                         <td>
                            <cc1:DSCCheckBox ID="VIEWTIMES" runat="server" Text="是否顯示讀取次數"  />
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
                <cc1:DSCTabPage ID="DSCTabPage5" runat="server" Enabled="True" ImageURL="" Title="群簽設定">
                <TabBody>
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="選擇不進行群簽之流程" Width="150px" />
                    <cc1:SingleDropDownList ID="SMWIAAD003" runat="server" Width="148px" />
                    <br />
                    &nbsp;<cc1:OutDataList ID="ADTable" runat="server" Height="162px" Width="503px" OnSaveRowData="ADTable_SaveRowData" OnShowRowData="ADTable_ShowRowData" />
                </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
    
    </div>
    </form>
</body>
</html>
