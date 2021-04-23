<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWD_Detail" %>

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
        <cc1:GlassButton ID="CopySetting" runat="server" Text="複製設定" Width="101px" OnClick="CopySetting_Click" />
        <cc1:GlassButton ID="RestoreSetting" runat="server" Text="載入設定" Width="101px" OnClick="RestoreSetting_Click" showWaitingIcon="True" />
        <table border=0 cellpadding=0 cellspacing=0 width="711px">
        <tr>
            <td width=92px><cc1:DSCLabel ID="DSCLabel7" runat="server" Text="關聯名稱" Width="92px" /></td>
            <td><cc1:SingleField ID="SMWDAAA002" runat="server" Width="250px" ReadOnly="False" /></td>
            <td width=82px><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="流程定義名稱" Width="82px" /></td>
            <td><cc1:SingleDropDownList ID="SMWDAAA003" runat="server" Width="251px" /></td>
        </tr>
        <tr>
            <td><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="流程角色" Width="92px" /></td>            
            <td>
            <cc1:SingleOpenWindowField ID="SMWDAAA004" runat="server" showReadOnlyField="false" Width="253px" guidField="SMWCAAA001" keyField="SMWCAAA002"  serialNum="001" tableName="SMWCAAA" FixReadOnlyValueTextWidth="0px" FixValueTextWidth="220px" PartialReadOnly="true"  />
            </td>
            <td><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="作業畫面" Width="82px" /></td>
            <td><cc1:SingleOpenWindowField ID="SMWDAAA005" runat="server" showReadOnlyField="True" Width="253px" guidField="SMWAAAA001" keyField="SMWAAAA002" OnSingleOpenWindowButtonClick="SMWDAAA005_SingleOpenWindowButtonClick" serialNum="001" tableName="SMWAAAA" FixReadOnlyValueTextWidth="130px" FixValueTextWidth="90px" /></td>
        </tr>
        <tr>
            <td><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="作業畫面模式" Width="92px" /></td>
            <td><cc1:SingleDropDownList ID="SMWDAAA006" runat="server" Width="251px" OnSelectChanged="SMWDAAA006_SelectChanged" /></td>
            <td colspan=2><cc1:DSCCheckBox ID="SMWDAAA007" runat="server" Text="是否為主要顯示畫面" /></td>
        </tr>
        <tr>
            <td><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="附件控制" Width="92px" /></td>
            <td><cc1:SingleDropDownList ID="SMWDAAA008" runat="server" Width="251px" /></td>
            <td colspan=2><cc1:DSCCheckBox ID="SMWDAAA024" runat="server" Text="流程檢視是否顯示流程圖" /></td>
        </tr>
        <tr>
            <td><cc1:DSCLabel ID="DSCLabel114" runat="server" Text="權限項目" Width="92px" /></td>
            <td><cc1:SingleOpenWindowField ID="SMWDAAA016" runat="server" showReadOnlyField="True" Width="250px" guidField="SMSAAAA001" keyField="SMSAAAA002" serialNum="001" tableName="SMSAAAA" FixReadOnlyValueTextWidth="130px" FixValueTextWidth="90px" /></td>
            <td colspan=2><cc1:DSCCheckBox ID="SMWDAAA023" runat="server" Text="憑証列印是否列印條碼" /></td>
        </tr>
        <tr>
            <td><cc1:DSCLabel ID="DSCLabel11" runat="server" Text="AgentSchema" Width="82px" /></td>
            <td colspan=3><cc1:SingleField ID="SMWDAAA013" runat="server" Width="543px" ReadOnly="False" OnTextChanged="SMWDAAA013_TextChanged" /> <cc1:GlassButton ID="Analyzebutton" runat="server" OnClick="AnalyzeButton_Click" Text="解析" Width="56px" /></td>
        </tr>
       </table>
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="361px" Width="711px">
            <TabPages>
                <cc1:DSCTabPage ID="SendSetting" runat="server" Enabled="True" Title="送單控制" ImageURL="">
                    <TabBody>
                        <table width="100%" style="font-size:9pt">
                        <tr>
                            <td><cc1:DSCLabel ID="DSCLabel10" runat="server" Text="表單單號格式" Width="92px" /></td>
                            <td><cc1:SingleOpenWindowField ID="SMWDAAA012" runat="server" showReadOnlyField="True" Width="250px" guidField="SMVIAAA001" keyField="SMVIAAA002" serialNum="001" tableName="SMVIAAA" FixReadOnlyValueTextWidth="130px" FixValueTextWidth="80px" /></td>
                            <td><cc1:DSCLabel ID="DSCLabelY12" runat="server" Text="送單後控制" Width="82px" /></td>
                            <td><cc1:SingleDropDownList ID="SMWDAAA018" runat="server" Width="251px" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCLabel ID="DSCLabel9" runat="server" Text="主旨字串格式" Width="92px" /></td>
                            <td colspan=3 valign=baseline><cc1:SingleField ID="SMWDAAA011" runat="server" Width="532px" ReadOnly="False" /> <cc1:GlassButton ID="HelpButton" runat="server" OnClick="HelpButton_Click" Text="說明" Width="55px" /></td>
                        </tr>
                         <tr>
                            <td><cc1:DSCLabel ID="DSCLabel27" runat="server" Text="草稿筆數限制" Width="92px" /></td>
                            <td><cc1:SingleField ID="SMWDAAA033" runat="server" Width="250px" ReadOnly="False" isMoney="True" Fractor="0" /></td>
                            <td><cc1:DSCLabel ID="DSCLabel28" runat="server" Text="(0)表不限制" Width="92px" /></td>
                            <td></td>
                        </tr>
                         <tr>
                            <td colspan=4>
                                <cc1:DSCCheckBox ID="SMWDAAA034" runat="server" Text="送單後是否刪除草稿來源" />
                            </td>
                        </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="ApproveSetting" runat="server" Enabled="True" Title="簽核相關" ImageURL="">
                    <TabBody>
                        <table width="100%" style="font-size:9pt">
                        <tr>
                            <td><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="意見表達" Width="92px" /></td>
                            <td><cc1:SingleDropDownList ID="SMWDAAA009" runat="server" Width="251px" /></td>
                            <td><cc1:DSCLabel ID="DSCLabel8" runat="server" Text="意見表達設定" Width="82px" /></td>
                            <td><cc1:SingleOpenWindowField ID="SMWDAAA010" runat="server" showReadOnlyField="True" Width="229px" guidField="SMWHAAA001" keyField="SMWHAAA002" serialNum="001" tableName="SMWHAAA" FixReadOnlyValueTextWidth="100px" FixValueTextWidth="90px" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCLabel ID="DSCLabelX14" runat="server" Text="簽核後控制" Width="92px" /></td>
                            <td><cc1:SingleDropDownList ID="SMWDAAA031" runat="server" Width="251px" /></td>
                            <td><cc1:DSCLabel ID="DSCLabelX12" runat="server" Text="允許轉寄類型" Width="82px" /></td>
                            <td><cc1:SingleDropDownList ID="SMWDAAA022" runat="server" Width="251px" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCLabel ID="DSCLabel22" runat="server" Text="參考流程" Width="92px" /></td>
                            <td><cc1:SingleDropDownList ID="SMWDAAA025" runat="server" Width="251px" /></td>
                            <td colspan=2><cc1:DSCCheckBox ID="SMWDAAA030" runat="server" Text="所有參考流程結束後才可簽核" /></td>
                        </tr>
                        <tr>
                            <td colspan=2>&nbsp;</td>
                            <td colspan=2><cc1:DSCCheckBox ID="SMWDAAA057" runat="server" Text="是否採用Queue執行" /></td>
                        </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="InfoSetting" runat="server" Enabled="True" Title="顯示設定" ImageURL="">
                    <TabBody>
                        <cc1:DSCGroupBox runat="server" ID="BasicSetting" Width="100%" Text="基本設定">
                        <table width=100% style="font-size:9pt">
                        <tr>
                            <td><cc1:DSCCheckBox ID="SMWDAAA032" runat="server" Text="是否顯示表單資訊" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA313" runat="server" Text="預設展開表單資訊" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA017" runat="server" Text="是否顯示簽核意見" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA314" runat="server" Text="預設展開簽核資訊" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCCheckBox ID="SMWDAAA300" runat="server" Text="是否自動展開轉派區" /></td>
                            <td colspan=3>
                                <cc1:DSCLabel ID="DSCLabel32" Text="關卡排序" runat="server" Width="80px" />
                                <cc1:SingleDropDownList ID="SMWDAAA315" runat="server" Width="120px" />
                                <cc1:DSCLabel ID="DSCLabel33" Text="轉派排序" runat="server" Width="80px" />
                                <cc1:SingleDropDownList ID="SMWDAAA316" runat="server" Width="120px" />
                            </td>
                        </tr>
                        </table>
                        </cc1:DSCGroupBox>
                        <cc1:DSCGroupBox runat="server" ID="DSCGroupBox1" Width="100%" Text="簽核意見解析項目">
                        <table width=100% style="font-size:9pt">
                        <tr>
                            <td colspan=5><cc1:DSCLabel ID="LBX1" Text="活動關卡" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCCheckBox ID="SMWDAAA301" runat="server" Text="一般簽核" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA302" runat="server" Text="通知關卡" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA303" runat="server" Text="執行(會辦)關卡" /></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan=5><cc1:DSCLabel ID="DSCLabel29" Text="轉派歷程" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCCheckBox ID="SMWDAAA304" runat="server" Text="直接轉派" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA305" runat="server" Text="系統代理人轉派" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA306" runat="server" Text="系統逾時轉派" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA307" runat="server" Text="管理員代理轉派" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA308" runat="server" Text="負責人代理轉派" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCCheckBox ID="SMWDAAA309" runat="server" Text="工作取回" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA310" runat="server" Text="工作轉派" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA311" runat="server" Text="管理員工作轉派" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA312" runat="server" Text="負責人工作轉派" /></td>
                            <td>&nbsp;</td>
                        </tr>
                        </table>
                        </cc1:DSCGroupBox>
                        <cc1:DSCGroupBox runat="server" ID="FieldSetting" Width="100%" Text="簽核意見顯示欄位">
                        <table width=100% style="font-size:9pt">
                        <tr>
                            <td colspan=5><cc1:DSCLabel ID="DSCLabel30" Text="活動關卡" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCCheckBox ID="SMWDAAA321" runat="server" Text="類型" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA322" runat="server" Text="關卡名稱" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA323" runat="server" Text="處理者" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA324" runat="server" Text="處理結果" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA325" runat="server" Text="處理意見" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCCheckBox ID="SMWDAAA326" runat="server" Text="處理時間" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA327" runat="server" Text="狀態" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA328" runat="server" Text="轉寄" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA329" runat="server" Text="開始時間" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA330" runat="server" Text="處理時數" /></td>
                        </tr>
                        <tr>
                            <td colspan=5><cc1:DSCLabel ID="DSCLabel31" Text="轉派歷程" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><cc1:DSCCheckBox ID="SMWDAAA331" runat="server" Text="類型" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA332" runat="server" Text="處理人" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA333" runat="server" Text="受託人" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA334" runat="server" Text="處理意見" /></td>
                            <td><cc1:DSCCheckBox ID="SMWDAAA335" runat="server" Text="處理時間" /></td>
                        </tr>
                        </table>
                        </cc1:DSCGroupBox>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="ToolBarSetting" runat="server" Enabled="True" Title="工具列" ImageURL="">
                    <TabBody>
                        <table width="100%" style="font-size:9pt">
                            <tr>
                                <td style="width: 77px"><cc1:DSCLabel ID="DC001" runat="server" Text="儲存" Width="75px" />
                                </td>
                                <td style="width: 129px" >
                                    <cc1:DSCCheckBox ID="SMWDAAA101" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA201" runat="server" Width="94px" />
                                </td>
                                <td style="width: 78px"><cc1:DSCLabel ID="DC002" runat="server" Text="刪除" Width="75px" />
                                </td>
                                <td style="width: 149px">
                                    <cc1:DSCCheckBox ID="SMWDAAA102" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA202" runat="server" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DC003" runat="server" Text="發起" Width="75px" />
                                </td>
                                <td >
                                    <cc1:DSCCheckBox ID="SMWDAAA103" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA203" runat="server" Width="113px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 77px"><cc1:DSCLabel ID="DC004" runat="server" Text="簽核" Width="75px" />
                                </td>
                                <td style="width: 129px">
                                    <cc1:DSCCheckBox ID="SMWDAAA104" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA204" runat="server" Width="96px" />
                                </td>
                                <td style="width: 78px"><cc1:DSCLabel ID="DC005" runat="server" Text="附件" Width="75px" />
                                </td>
                                <td style="width: 149px">
                                    <cc1:DSCCheckBox ID="SMWDAAA105" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA205" runat="server" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DC006" runat="server" Text="儲存草稿" Width="75px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWDAAA106" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA206" runat="server" Width="115px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 77px"><cc1:DSCLabel ID="DC007" runat="server" Text="讀取草稿" Width="75px" />
                                </td>
                                <td style="width: 129px">
                                    <cc1:DSCCheckBox ID="SMWDAAA107" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA207" runat="server" Width="96px" />
                                </td>
                                <td style="width: 78px"><cc1:DSCLabel ID="DC008" runat="server" Text="檢視相關表單" Width="84px" />
                                </td>
                                <td style="width: 149px">
                                    <cc1:DSCCheckBox ID="SMWDAAA108" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA208" runat="server" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DC009" runat="server" Text="重新整理" Width="75px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWDAAA109" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA209" runat="server" Width="115px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 77px"><cc1:DSCLabel ID="DC010" runat="server" Text="流程檢視" Width="75px" />
                                </td>
                                <td style="width: 129px">
                                    <cc1:DSCCheckBox ID="SMWDAAA110" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA210" runat="server" Width="96px" />
                                </td>
                                <td style="width: 78px"><cc1:DSCLabel ID="DC011" runat="server" Text="設定流程" Width="75px" />
                                </td>
                                <td style="width: 149px">
                                    <cc1:DSCCheckBox ID="SMWDAAA111" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA211" runat="server" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DC012" runat="server" Text="加簽" Width="75px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWDAAA112" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA212" runat="server" Width="115px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 77px"><cc1:DSCLabel ID="DC013" runat="server" Text="撤銷" Width="75px" />
                                </td>
                                <td style="width: 129px">
                                    <cc1:DSCCheckBox ID="SMWDAAA113" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA213" runat="server" Width="96px" />
                                </td>
                                <td style="width: 78px"><cc1:DSCLabel ID="DC014" runat="server" Text="重辦" Width="75px" />
                                </td>
                                <td style="width: 149px">
                                    <cc1:DSCCheckBox ID="SMWDAAA114" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA214" runat="server" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DC015" runat="server" Text="轉寄" Width="75px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWDAAA115" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA215" runat="server" Width="115px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 77px"><cc1:DSCLabel ID="DC016" runat="server" Text="發起參考流程" Width="83px" />
                                </td>
                                <td style="width: 129px">
                                    <cc1:DSCCheckBox ID="SMWDAAA116" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA216" runat="server" Width="96px" />
                                </td>
                                <td style="width: 78px"><cc1:DSCLabel ID="DC017" runat="server" Text="複製表單" Width="75px" />
                                </td>
                                <td style="width: 149px">
                                    <cc1:DSCCheckBox ID="SMWDAAA117" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA217" runat="server" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DC018" runat="server" Text="歷史紀錄" Width="75px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWDAAA118" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA218" runat="server" Width="115px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 77px"><cc1:DSCLabel ID="DC019" runat="server" Text="列印憑證" Width="75px" />
                                </td>
                                <td style="width: 129px">
                                    <cc1:DSCCheckBox ID="SMWDAAA119" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA219" runat="server" Width="96px" />
                                </td>
                                <td style="width: 78px"><cc1:DSCLabel ID="DC020" runat="server" Text="回清單" Width="75px" />
                                </td>
                                <td style="width: 149px">
                                    <cc1:DSCCheckBox ID="SMWDAAA120" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA220" runat="server" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DC021" runat="server" Text="轉派" Width="75px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWDAAA121" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA221" runat="server" Width="115px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 77px"><cc1:DSCLabel ID="DC022" runat="server" Text="取回" Width="75px" />
                                </td>
                                <td style="width: 129px">
                                    <cc1:DSCCheckBox ID="SMWDAAA122" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA222" runat="server" Width="96px" />
                                </td>
                                <td style="width: 78px"><cc1:DSCLabel ID="DC056" runat="server" Text="撤簽" Width="75px" />
                                </td>
                                <td style="width: 149px">
                                    <cc1:DSCCheckBox ID="SMWDAAA156" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA256" runat="server" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DC023" runat="server" Text="自訂按鈕一" Width="75px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWDAAA151" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA251" runat="server" Width="115px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 77px"><cc1:DSCLabel ID="DC024" runat="server" Text="自訂按鈕二" Width="75px" />
                                </td>
                                <td style="width: 129px">
                                    <cc1:DSCCheckBox ID="SMWDAAA152" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA252" runat="server" Width="96px" />
                                </td>
                                <td style="width: 78px"><cc1:DSCLabel ID="DC025" runat="server" Text="自訂按鈕三" Width="75px" />
                                </td>
                                <td style="width: 149px">
                                    <cc1:DSCCheckBox ID="SMWDAAA153" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA253" runat="server" Width="120px" />
                                </td>
                                <td><cc1:DSCLabel ID="DC026" runat="server" Text="自訂按鈕四" Width="75px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWDAAA154" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA254" runat="server" Width="115px" />
                                </td>
                            </tr>
                            <tr>
                                <td><cc1:DSCLabel ID="DC027" runat="server" Text="自訂按鈕五" Width="75px" />
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="SMWDAAA155" runat="server" />
                                    <cc1:SingleField ID="SMWDAAA255" runat="server" Width="96px" />
                                </td>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                    </TabBody>
                 </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" Title="按鈕畫面" ImageURL="">
                    <TabBody>
                        <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="設定流程畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA014" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel24" runat="server" Text="簽核畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA027" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel25" runat="server" Text="撤銷畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA028" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel26" runat="server" Text="取回重辦畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA029" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="加簽畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA015" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabelA14" runat="server" Text="重辦設定畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA019" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabelA15" runat="server" Text="轉寄設定畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA020" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabelA16" runat="server" Text="轉派設定畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA021" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel23" runat="server" Text="參考流程畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA026" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabelA17" runat="server" Text="撤簽畫面" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA056" runat="server" Width="554px" ReadOnly="False" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="自訂按鈕一" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA051" runat="server" Width="307px" ReadOnly="False" /><cc1:DSCLabel ID="DSCLabelX5" runat="server" Text="開窗寬度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA451" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                        <cc1:DSCLabel ID="DSCLabelX8" runat="server" Text="開窗高度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA551" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="自訂按鈕二" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA052" runat="server" Width="307px" ReadOnly="False" /><cc1:DSCLabel ID="DSCLabelX9" runat="server" Text="開窗寬度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA452" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                        <cc1:DSCLabel ID="DSCLabelX10" runat="server" Text="開窗高度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA552" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel19" runat="server" Text="自訂按鈕三" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA053" runat="server" Width="307px" ReadOnly="False" /><cc1:DSCLabel ID="DSCLabelXZ14" runat="server" Text="開窗寬度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA453" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                        <cc1:DSCLabel ID="DSCLabelX15" runat="server" Text="開窗高度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA553" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel20" runat="server" Text="自訂按鈕四" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA054" runat="server" Width="307px" ReadOnly="False" /><cc1:DSCLabel ID="DSCLabelX16" runat="server" Text="開窗寬度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA454" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                        <cc1:DSCLabel ID="DSCLabelZ22" runat="server" Text="開窗高度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA554" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                        <br />
                        <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="自訂按鈕五" Width="92px" />
                        <cc1:SingleField ID="SMWDAAA055" runat="server" Width="307px" ReadOnly="False" /><cc1:DSCLabel ID="DSCLabelX27" runat="server" Text="開窗寬度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA455" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                        <cc1:DSCLabel ID="DSCLabelX28" runat="server" Text="開窗高度(px)" Width="82px" />
                        <cc1:SingleField ID="SMWDAAA555" runat="server" alignRight="True" isMoney="True"
                            Width="37px" />
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage5" runat="server" Enabled="True" ImageURL="" Title="加簽角色">
                    <TabBody>
                        <cc1:GlassButton ID="SelectRole" runat="server" Text="選擇角色" Width="96px" OnClick="SelectRole_Click" />
                        <cc1:OpenWin ID="RoleOpenWin" runat="server" OnOpenWindowButtonClick="RoleOpenWin_OpenWindowButtonClick" />
                        <br />
                        <cc1:OutDataList ID="ADList" runat="server" Height="280px" Width="689px" />
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="FieldsSetting" runat="server" Enabled="True" Title="畫面欄位" ImageURL="">
                    <TabBody>
                    <cc1:DSCLabel ID="DSCLabel211" runat="server" Text="欄位代號" Width="78px" />
                    <cc1:SingleField ID="SMWDAAB003" runat="server" ReadOnly="True" Width="156px" />
                    <cc1:DSCLabel ID="DSCLabel611" runat="server" Text="欄位類型" Width="78px" />
                    <cc1:SingleField ID="SMWDAAB004" runat="server" ReadOnly="True" Width="156px" />
                        &nbsp;<cc1:DSCCheckBox ID="SMWDAAB005" runat="server" Text="顯示 " />
                    <cc1:DSCCheckBox ID="SMWDAAB006" runat="server" Text="必填" />
                    <cc1:DSCCheckBox ID="SMWDAAB007" runat="server" Text="唯讀" />
                    <cc1:DSCCheckBox ID="SMWDAAB008" runat="server" Text="稽核" />
                        &nbsp;<br />
                    <cc1:OutDataList ID="ListTable" runat="server" Height="301px" Width="694px" OnSaveRowData="ListTable_SaveRowData" OnShowRowData="ListTable_ShowRowData" showExcel="True"/>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="ProcessSetting" runat="server" Enabled="True" Title="待辦檢視資料夾" ImageURL="">
                    <TabBody>
                        <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="資料夾欄位名稱" Width="117px" />
                        <cc1:SingleDropDownList ID="SMWDAAC003" runat="server" Width="97px" />
                        <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="對應欄位" Width="71px" />
                        <cc1:SingleDropDownList ID="SMWDAAC004" runat="server" Width="133px" /><cc1:DSCLabel ID="DSCLabel16" runat="server" Text="顯示名稱" Width="71px" />
                        <cc1:SingleField ID="SMWDAAC005" runat="server" Width="180px" />
                        <cc1:OutDataList ID="CTable" runat="server" Height="299px" Width="696px" OnSaveRowData="CTable_SaveRowData" OnShowRowData="CTable_ShowRowData" />
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
        <br />
    </div>
    </form>
</body>
</html>
