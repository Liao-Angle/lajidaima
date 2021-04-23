<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWA_Detail" %>

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
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="作業畫面代碼" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWAAAA002" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="作業畫面名稱" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWAAAA003" runat="server" Width="385px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="主旨格式" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWAAAA004" runat="server" Width="385px" ReadOnly="True" />
        <br />
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="作業畫面URL" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWAAAA005" runat="server" Width="309px" ReadOnly="False" />
        &nbsp;&nbsp;<cc1:GlassButton ID="AnalyzeButton" runat="server" OnClick="AnalyzeButton_Click"
            Text="解析" Width="69px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="AgentSchema" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMWAAAA009" runat="server" Width="385px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="權限項目" Width="120px" />
        &nbsp;&nbsp;<cc1:SingleOpenWindowField ID="SMWAAAA008" runat="server" guidField="SMSAAAA001"
            keyField="SMSAAAA002" serialNum="001" showReadOnlyField="True" tableName="SMSAAAA"
            Width="365px" />
        <br />        
        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="說明" Width="120px" />
        <br />
        <cc1:SingleField ID="SMWAAAA006" runat="server" Width="513px" ReadOnly="False" Height="43px" MultiLine="True" />
        <br />
        <cc1:DSCCheckBox ID="SMWAAAA007" runat="server" Text="本作業畫面在無流程相關設定時是否改用維護作業模式顯示欄位" />
        <br />
        <cc1:DSCLabel ID="DSCLabel60" runat="server" Text="送出後模式" Width="120px" />
        <cc1:SingleDropDownList ID="SMWAAAA030" runat="server" Width="386px" />
        <br />
        <br />
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="323px" Width="513px">
            <TabPages>
                <cc1:DSCTabPage ID="ToolBarSetting" runat="server" Enabled="True" Title="工具列設定">
                    <TabBody>
                        <cc1:DSCCheckBox ID="SMWAAAA010" runat="server" Text="是否使用此設定取代預設按鈕顯示設定" />
                        <br />
                        <table width="100%" style="font-size:9pt">
                            <tr>
                                <td width=50px>&nbsp;
                                </td>
                                <td><cc1:DSCLabel ID="DSCLabel11" runat="server" Text="新增" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DSCLabel12" runat="server" Text="顯示" Width="120px" />
                                </td>
                            </tr>
                            <tr>
                                <td><cc1:DSCLabel ID="DSCLabel13" runat="server" Text="儲存" Width="120px" />
                                </td>
                                <td >
                                    <cc1:DSCCheckBox ID="SMWAAAA011" runat="server" />
                                    <cc1:SingleField ID="SMWAAAA021" runat="server" Width="131px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWAAAA014" runat="server" />
                                    <cc1:SingleField ID="SMWAAAA024" runat="server" Width="133px" />
                                </td>
                            </tr>
                            <tr>
                                <td><cc1:DSCLabel ID="DSCLabel14" runat="server" Text="刪除" Width="120px" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td >
                                    <cc1:DSCCheckBox ID="SMWAAAA015" runat="server" />
                                    <cc1:SingleField ID="SMWAAAA025" runat="server" Width="133px" />
                                </td>
                            </tr>
                            <tr>
                                <td><cc1:DSCLabel ID="DSCLabel15" runat="server" Text="重新整理" Width="120px" />
                                </td>
                                <td >
                                    <cc1:DSCCheckBox ID="SMWAAAA012" runat="server" />
                                    <cc1:SingleField ID="SMWAAAA022" runat="server" Width="130px" />
                                </td>
                                <td >
                                    <cc1:DSCCheckBox ID="SMWAAAA016" runat="server" />
                                    <cc1:SingleField ID="SMWAAAA026" runat="server" Width="133px" />
                                </td>
                            </tr>
                            <tr>
                                <td><cc1:DSCLabel ID="DSCLabel16" runat="server" Text="歷史紀錄" Width="120px" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td >
                                    <cc1:DSCCheckBox ID="SMWAAAA017" runat="server" />
                                    <cc1:SingleField ID="SMWAAAA027" runat="server" Width="134px" />
                                </td>
                            </tr>
                            <tr>
                                <td><cc1:DSCLabel ID="DSCLabel17" runat="server" Text="回清單" Width="120px" />
                                </td>
                                <td >
                                    <cc1:DSCCheckBox ID="SMWAAAA013" runat="server" />
                                    <cc1:SingleField ID="SMWAAAA023" runat="server" Width="130px" />
                                </td>
                                <td >
                                    <cc1:DSCCheckBox ID="SMWAAAA018" runat="server" />
                                    <cc1:SingleField ID="SMWAAAA028" runat="server" Width="134px" />
                                </td>
                            </tr>
                            <tr>
                                <td><cc1:DSCLabel ID="DSCLabel18" runat="server" Text="列印憑證" Width="120px" />
                                </td>
                                <td >&nbsp;
                                </td>
                                <td >
                                    <cc1:DSCCheckBox ID="SMWAAAA019" runat="server" />
                                    <cc1:SingleField ID="SMWAAAA029" runat="server" Width="134px" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                 </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="FieldsSetting" runat="server" Enabled="True" Title="畫面欄位設定">
                    <TabBody>
                    <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="欄位代號" Width="78px" />
                    <cc1:SingleField ID="SMWAAAB003" runat="server" ReadOnly="True" Width="156px" />
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="欄位類型" Width="78px" />
                    <cc1:SingleField ID="SMWAAAB004" runat="server" ReadOnly="True" Width="156px" />
                    <br />
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="新增狀態:" Width="78px" />
                    <cc1:DSCCheckBox ID="SMWAAAB005" runat="server" Text="顯示 " />
                    <cc1:DSCCheckBox ID="SMWAAAB006" runat="server" Text="必填" />
                    <cc1:DSCCheckBox ID="SMWAAAB007" runat="server" Text="唯讀" />
                    <cc1:DSCCheckBox ID="SMWAAAB008" runat="server" Text="稽核" />
                    <br />
                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="修改狀態:" Width="78px" />
                    <cc1:DSCCheckBox ID="SMWAAAB009" runat="server" Text="顯示 " />
                    <cc1:DSCCheckBox ID="SMWAAAB010" runat="server" Text="必填" />
                    <cc1:DSCCheckBox ID="SMWAAAB011" runat="server" Text="唯讀" />
                    <cc1:DSCCheckBox ID="SMWAAAB012" runat="server" Text="稽核" />
                    <br />
                    <cc1:OutDataList ID="ListTable" runat="server" Height="208px" Width="503px" OnSaveRowData="ListTable_SaveRowData" OnShowRowData="ListTable_ShowRowData" />
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
        <br />
    </div>
    </form>
</body>
</html>
