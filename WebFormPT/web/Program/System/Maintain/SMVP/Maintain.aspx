<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_System_Maintain_SMVP_Maintain" %>

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
    <table border=0 cellpadding=0 cellspacing=3>
    <tr>
        <td colspan=2>
            <cc1:GlassButton ID="SaveButton" Text="儲存" ImageUrl="~/Images/OK.gif" runat="server" Width="110px" OnClick="SaveButton_Click" />
        </td>
    </tr>
    </table>
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="400px" Width="756px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" Hidden="False" ImageURL=""
                    Title="一般設定">
                    <TabBody>
                        <table border=0 cellpadding=0 cellspacing=3 width=700>
                        <tr>
                            <td width="50%">
                                <cc1:DSCCheckBox ID="SMVPAAA015" runat="server" Text="允許多語系(預設繁體中文)" />
                            </td>
                            <td width="50%">
                                <cc1:DSCCheckBox ID="SMVPAAA017" runat="server" Text="允許下載元件聯結" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA018" runat="server" Text="啟用系統密碼管理" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA016" runat="server" Text="系統/個人選單視為一般視窗" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA002" runat="server" Text="是否允許設定個人化選單" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA003" runat="server" Text="是否允許自定面板" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel8" Text="權限" runat="server" Width="50px" /><cc1:SingleOpenWindowField ID="SMVPAAA024" runat="server" guidField="SMSAAAA001" keyField="SMSAAAA002" keyFieldType="STRING" serialNum="001" showReadOnlyField="true" tableName="SMSAAAA" FixReadOnlyValueTextWidth="150" />
                            </td>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel7" Text="權限" runat="server" Width="50px" /><cc1:SingleOpenWindowField ID="SMVPAAA025" runat="server" guidField="SMSAAAA001" keyField="SMSAAAA002" keyFieldType="STRING" serialNum="001" showReadOnlyField="true" tableName="SMSAAAA" FixReadOnlyValueTextWidth="150" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA004" runat="server" Text="是否允許使用者自訂新視窗開啟功能" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA008" runat="server" Text="是否顯示功能列" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan=2>
                                <cc1:DSCLabel ID="LBSMVPAAA005" Text="頁面保護" runat="server" Width="91px" />
                                <cc1:DSCCheckBox ID="SMVPAAA0051" runat="server" Text="允許複製" />
                                <cc1:DSCCheckBox ID="SMVPAAA0052" runat="server" Text="允許選擇文字" />
                                <cc1:DSCCheckBox ID="SMVPAAA0053" runat="server" Text="允許右鍵功能" />
                                <cc1:DSCCheckBox ID="SMVPAAA0054" runat="server" Text="允許滑鼠拖曳" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA006" runat="server" Text="是否預設顯示標題" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA007" runat="server" Text="是否允許切換標題" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel2" Text="最大資料筆數" runat="server" Width="91px" />
                                <cc1:SingleField ID="SMVPAAA014" runat="server" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA020" runat="server" Text="是否啟用MS元件共存模式" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA032" runat="server" Text="是否啟用切換帳號功能" />
                            </td>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel6" Text="測試人員帳號" runat="server" Width="91px" />
                                <cc1:SingleOpenWindowField ID="SMVPAAA033" runat="server" guidField="OID" keyField="id" keyFieldType="STRING" serialNum="001" showReadOnlyField="true" tableName="Users" FixReadOnlyValueTextWidth="150" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA034" runat="server" Text="是否啟用版型切換" />
                            </td>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel9" Text="版面管理者權限" runat="server" Width="110px" /><cc1:SingleOpenWindowField ID="SMVPAAA035" runat="server" guidField="SMSAAAA001" keyField="SMSAAAA002" keyFieldType="STRING" serialNum="001" showReadOnlyField="true" tableName="SMSAAAA" FixReadOnlyValueTextWidth="150" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA038" runat="server" Text="是否開啟ReportingServices結合ECP驗證功能" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA040" runat="server" Text="系統選單是否允許搜尋" />
                            </td>
                        </tr>
                        <tr>
                        <td>
                        <cc1:DSCCheckBox ID="SMVPAAA041" runat="server" Text="主版面是否允許切換語言" />
                        </td>
                        <td><cc1:DSCCheckBox ID="SMVPAAA042" runat="server" Text="是否啟用M-Office整合功能" />
                        </td>
                        </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage4" runat="server" Enabled="True" Hidden="False" ImageURL=""
                    Title="稽核設定">
                    <TabBody>
                        <table border=0 cellpadding=0 cellspacing=3 width=700px>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA009" runat="server" Text="啟用偵錯記錄檔" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA010" runat="server" Text="列印稽核資訊" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan=2>
                                <cc1:DSCLabel ID="LBSMVPAAA011" Text="列印左邊界" runat="server" Width="91px" />
                                <cc1:SingleField ID="SMVPAAA011" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan=2>
                                <cc1:DSCLabel ID="LBSMVPAAA012" Text="列印頁首" runat="server" Width="91px" />
                                <cc1:SingleField ID="SMVPAAA012" runat="server" Width="455px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan=2>
                                <cc1:DSCLabel ID="LBSMVPAAA013" Text="列印頁尾" runat="server" Width="91px" />
                                <cc1:SingleField ID="SMVPAAA013" runat="server" Width="455px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan=2>
                                <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" CssClass="DSCGroupBox" Font-Size="9pt"
                                    Text="頁首頁尾格式說明">
                                <cc1:DSCLabel ID="DSCLabel1" Text="%USERID% 使用者代號; %USERNAME% 使用者姓名; %PRINTTIME% 列印時間; %LOGINIP% 列印電腦IP; &w 網頁標題; &u 網址; &d 簡短日期; &D 完整日期; &t 時間 (12小時制); &T 時間 (24小時制); &p 頁碼; &P 總頁數; && 記號; &b 分隔符號。" runat="server" Width="541px" />
                                </cc1:DSCGroupBox>
                            </td>
                        </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" Hidden="False" ImageURL=""
                    Title="流程相關設定">
                    <TabBody>
                        <table border=0 cellpadding=0 cellspacing=3 width=700>
                        <tr>
                            <td nowrap="true">
                                <cc1:DSCLabel ID="LBLSMVPAAA023" runat="server" Text="簽核設定" Width="150px" />
                                <cc1:SingleDropDownList ID="SMVPAAA023" runat="server" Width="150px" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA036" runat="server" Text="啟用多組代理人設定" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel3" Text="流程引擎處理次數" runat="server" Width="150px" />
                                <cc1:SingleField ID="SMVPAAA021" isMoney="true" runat="server" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA026" runat="server" Text="啟用View讀取待處理資料夾" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel4" Text="流程引擎錯誤等待毫秒" runat="server" Width="150px" />
                                <cc1:SingleField ID="SMVPAAA022" isMoney="true" runat="server" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA029" runat="server" Text="原稿搜尋限制條件" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel5" Text="原稿最大資料筆數" runat="server" Width="150px" />
                                <cc1:SingleField ID="SMVPAAA028" runat="server" />
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA027" runat="server" Text="開啟原稿預設載入資料" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCLabel ID="LBLSMVPAAA039" runat="server" Text="流程引擎授權模式" Width="150px" />
                                <cc1:SingleDropDownList ID="SMVPAAA039" runat="server" Width="150px" />                            
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA037" runat="server" Text="開啟批次取回代理清單功能" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA043" runat="server" Text="開啟流程相關人員檢視控制" />                           
                            </td>
                        </tr>                         
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage3" runat="server" Enabled="True" Hidden="False" ImageURL=""
                    Title="登入相關設定">
                    <TabBody>
                        <table border=0 cellpadding=0 cellspacing=3>
                        <tr>
                            <td>
                                <cc1:DSCCheckBox ID="SMVPAAA030" runat="server" Text="是否啟用IP限制" />
                            </td>
                            <td nowrap="true">
                                <cc1:SingleDropDownList ID="SMVPAAA031" runat="server" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan=2>
                            <cc1:DSCGroupBox ID="BListGroup" Text="IP設定列表" runat=server>
                                <cc1:DSCLabel ID="DSCLabelB6" Text="IP:" runat="server" Width="30px" />
                                <cc1:IPField ID="SMVPAAB002" runat="server" />
                                <cc1:OutDataList ID="SMVPBList" runat="server" Width="600px" Height="250px" OnShowRowData="SMVPBList_ShowRowData" OnSaveRowData="SMVPBList_SaveRowData" />
                            </cc1:DSCGroupBox>
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
