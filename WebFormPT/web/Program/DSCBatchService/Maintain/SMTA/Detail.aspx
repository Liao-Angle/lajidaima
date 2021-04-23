<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCBatchService_Maintain_SMTA_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>程式代碼輸入畫面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="批次作業代碼" Width="120px" />
        &nbsp;&nbsp;&nbsp; &nbsp;
        <cc1:SingleField ID="SMTAAAA002" runat="server" Width="178px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="批次作業名稱" Width="120px" />
        &nbsp;&nbsp;&nbsp; &nbsp;
        <cc1:SingleField ID="SMTAAAA003" runat="server" Width="366px" ReadOnly="False" />
        <br />
        <br />       
        <br />
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="423px" Width="565px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="基本設定">
                    <TabBody>
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="完整類別名稱" Width="120px" />
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<cc1:SingleField ID="SMTAAAA004" runat="server" Width="366px" ReadOnly="False" />
        <br />
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="分段執行批次作業" Width="119px" />
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;<cc1:DSCRadioButton ID="NotParaExecute" runat="server" Checked="True" Text="否" Width="45px" GroupName="T1" OnClick="NotParaExecute_Click" />
        &nbsp;<cc1:DSCRadioButton ID="ParaExecute"  runat="server" Text="是" Width="44px" GroupName="T1" OnClick="ParaExecute_Click" />
        &nbsp;&nbsp;&nbsp;<cc1:DSCLabel ID="DSCLabel6" runat="server" Text="批次執行分段數目" Width="119px" />
        <cc1:SingleField ID="SMTAAAA006" runat="server" Width="37px" ReadOnly="False" ValueText="1" alignRight="True" />

        <br />
       <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="立即執行批次作業" Width="119px" />
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;<cc1:DSCRadioButton ID="NotExecute" runat="server" Text="否" Width="45px" Checked="True" GroupName="T2" />
        &nbsp;<cc1:DSCRadioButton ID="ImExecute" runat="server" Text="是" Width="44px" CertificateMode="false" GroupName="T2" />
        <br />
        <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="相依於其他批次作業" Width="129px" />
        &nbsp; &nbsp;
        <cc1:SingleOpenWindowField ID="SMTAAAA008" runat="server" guidField="SMTAAAA001"
            keyField="SMTAAAA002" serialNum="001" showReadOnlyField="True" tableName="SMTAAAA"
            Width="386px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="批次執行失敗後動作" Width="127px" />
        &nbsp; &nbsp;
        <cc1:DSCRadioButton ID="ExeBatch" runat="server" Text="立刻重新執行" Width="105px" />
        &nbsp; &nbsp;&nbsp;<cc1:DSCRadioButton ID="NtExeBatch" runat="server" Text="等待下次執行時間再執行"
            Width="182px" Checked="True" />
        <br />
        <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="容許批次執行最長時間" Width="140px" />
        &nbsp;&nbsp;<cc1:SingleField ID="SMTAAAA013" runat="server" Width="80px" ReadOnly="False" ValueText="1" alignRight="True" />
        <cc1:SingleDropDownList ID="SMTAAAA014" runat="server" Width="69px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="容許度" Width="140px" />
        &nbsp;
        <cc1:SingleField ID="SMTAAAA015" runat="server" Width="80px" ReadOnly="False" ValueText="1" alignRight="True" />
        <cc1:SingleDropDownList ID="SMTAAAA016" runat="server" Width="69px" />
        <br />
        <br />
        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="批次執行排程" Width="140px" />
        &nbsp;<br />
        <cc1:RoutineWizard ID="SMTAAAA010" runat="server" Width="514px" />
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="ToolBarSetting" runat="server" Enabled="True" Title="參數設定">
                    <TabBody>
                        &nbsp;<table width="100%" style="font-size:9pt">
                            <tr>
                                <td style="width: 125px">
                                    參數名稱
                                </td>
                                <td >
                                    &nbsp;<cc1:SingleField ID="SMTAAAB003" runat="server" Width="319px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 125px">
                                    參數值&nbsp;</td>
                                <td>&nbsp;<cc1:SingleField ID="SMTAAAB004" runat="server" Width="319px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 125px; height: 47px">
                                    參數說明&nbsp;</td>
                                <td style="height: 47px" >
                                    &nbsp;<cc1:SingleField ID="SMTAAAB005" runat="server" Height="50px" MultiLine="True"
                                        ReadOnly="False" Width="318px" />
                                </td>
                            </tr>
                        </table>
                        <cc1:OutDataList ID="OutDataList1" Width="535px" runat="server" OnSaveRowData="OutDataList1_SaveRowData" OnShowRowData="OutDataList1_ShowRowData" Height="259px" />
                    </TabBody>
                 </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="FieldsSetting" runat="server" Enabled="True" Title="批次分段設定">
                    <TabBody>
                        &nbsp; &nbsp;<br /><table width="100%" style="font-size:9pt">
                            <tr>
                                <td > 
                                    <cc1:GlassButton ID="RefreshOrderButton" runat="server" OnClick="RefreshOrderButton_Click"
                Text="重新整理分段順序" Width="110px" /> 
                                 </td>
                           </tr> 
                            <tr>
                                <td style="width: 80px"><cc1:DSCLabel ID="DSCLabel12" runat="server" Text="分段順序" Width="140px" /></td>
                                <td style="width: 80px">
                                    &nbsp;<cc1:SingleField ID="SMTAAAC003" runat="server" Width="38px" />
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="width: 80px"><cc1:DSCLabel ID="DSCLabel13" runat="server" Text="分段名稱" Width="140px" /></td>
                                <td style="width: 341px" colspan="3">
                                    &nbsp;<cc1:SingleField ID="SMTAAAC004" runat="server" Width="400px" />
                                </td>
                            </tr>
                        <tr>
                                <td style="width: 80px"><cc1:DSCLabel ID="DSCLabel14" runat="server" Text="應用程式代號" Width="140px" /></td>
                                <td >
                                    &nbsp;<cc1:SingleField ID="SMTAAAC005" runat="server" Width="180px" />
                                </td>                                
                            </tr>
                           <tr>
                                <td style="width: 80px"><cc1:DSCLabel ID="DSCLabel15" runat="server" Text="模組代號" Width="140px" /></td>
                                <td >
                                &nbsp;<cc1:SingleField ID="SMTAAAC006" runat="server" Width="180px" />
                                </td>
                           </tr> 
                           <tr>
                                <td style="width: 80px"><cc1:DSCLabel ID="DSCLabel16" runat="server" Text="錯誤嘗試執行次數" Width="140px" /></td>
                                <td >
                                &nbsp;<cc1:SingleField ID="SMTAAAC007" runat="server" Width="38px" />
                                </td>
                            </tr> 
                    </table>
                    <cc1:OutDataList ID="ListTable" runat="server" Height="208px" Width="535px" OnSaveRowData="ListTable_SaveRowData" OnShowRowData="ListTable_ShowRowData" OnAddOutline="ListTable_AddOutline" />
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
        <br />
    </div>
    </form>
</body>
</html>
