<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVB_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>程式代碼輸入畫面</title>
</head>
<script language=javascript>
function AB03BChange()
{
    alert('before');
}
function AB03AChange()
{
    alert('after');
}
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="50px" Width="658px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="基本設定">
                <TabBody>
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="程式代號" Width="120px" />
                    <cc1:SingleField ID="SMVAAAB002" runat="server" Width="184px" />
                    <br />
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="程式名稱" Width="120px" />
                    <cc1:SingleField ID="SMVAAAB003" runat="server" Width="185px" />
                    &nbsp;<br />
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="程式網址" Width="120px" />
                    <cc1:SingleField ID="SMVAAAB004" runat="server" Width="444px" />
                    <br />
                    <br />
                    
                    <cc1:DSCGroupBox ID="DSCGroupBox2" runat="server" Text="程式網址說明範例" Width="636px" style="font-size:8pt">
                        <cc1:DSCLabel runat="server" ID="ExplainText1" Text="1. 一般網頁: 直接鍵入相對網址, 例如: Program/System/SMVB/Maintain.aspx" />
                        <cc1:DSCLabel runat="server" ID="ExplainText2" Text="2. 表單畫面: 相對網址後接上?UIStatus=0&UIType=Process" />
                        <cc1:DSCLabel runat="server" ID="ExplainText3" Text="3. 非表單新增: 相對網址後接上?UIStatus=7&UIType=General" />
                        <cc1:DSCLabel runat="server" ID="ExplainText4" Text="4. 各式處理資料夾: Program/DSCGPFlowService/Maintain/SMWL/Maintain.aspx?BoxID=處理資料夾代號" />
                        <cc1:DSCLabel runat="server" ID="ExplainText5" Text="5. 直接開啟Webi報表: ProjectBaseWebUI/WebiReportNormal.aspx?ReportID=報表ID" />
                        <cc1:DSCLabel runat="server" ID="ExplainText6" Text="6. 各式原稿資料夾: Program/DSCGPFlowService/Maintain/SMWK/Maintain.aspx?BoxID=原稿資料夾代號" />
                        <cc1:DSCLabel runat="server" ID="ExplainText7" Text="7. 各式已轉派清單: Program/DSCGPFlowService/Maintain/SMWO/Maintain.aspx?BoxID=已轉派清單代號" />
                        <cc1:DSCLabel runat="server" ID="ExplainText8" Text="8. 各式相關表單: Program/DSCGPFlowService/Maintain/SMWQ/Maintain.aspx?BoxID=相關表單定義代號" />
                    </cc1:DSCGroupBox><br />
                    <br />
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="權限項目" Width="120px" />
                    <cc1:SingleOpenWindowField ID="SMVAAAB009" runat="server" guidField="SMSAAAA001"
                        keyField="SMSAAAA002" serialNum="001" showReadOnlyField="True" tableName="SMSAAAA"
                        Width="325px" FixReadOnlyValueTextWidth="150px" FixValueTextWidth="120px" />
                    <br /><br />
                    &nbsp;<cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Height="54px" Text="顯示設定"
                        Width="562px">&nbsp;<cc1:DSCCheckBox ID="SMVAAAB007" runat="server" Text="是否最大視窗顯示" OnClick="SMVAAAB007_Click" />
                        <cc1:DSCCheckBox ID="SMVAAAB008" runat="server" Text="是否強制新視窗執行" />
                        <br />
                        &nbsp;
                        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="視窗寬度" Width="69px" />
                        <cc1:SingleField ID="SMVAAAB005" runat="server" Width="47px" ValueText="650" />
                        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="px" Width="69px" />
                        <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="視窗高度" Width="69px" />
                        <cc1:SingleField ID="SMVAAAB006" runat="server" Width="47px" ValueText="350" />
                        <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="px" Width="69px" />
                        <br />
                        <br />
                    </cc1:DSCGroupBox>
                    <br />
                    <br />
                </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="說明檔設定">
                <TabBody>
                <table border=0 width=550px>
                <tr>
                    <td valign=top>
                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="作業目的" Width="120px" />
                    </td>
                    <td>
                    <cc1:SingleField ID="SMVAAAB011" runat="server" Width="444px" MultiLine="true" Height="60px" />
                    </td>
                </tr>
                <tr>
                    <td valign=top>
                    <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="使用時機" Width="120px" />
                    </td>
                    <td>
                    <cc1:SingleField ID="SMVAAAB012" runat="server" Width="444px" MultiLine="true" Height="60px" />
                    </td>
                </tr>
                <tr>
                    <td valign=top>
                    <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="操作說明" Width="120px" />
                    </td>
                    <td>
                    <cc1:SingleField ID="SMVAAAB013" runat="server" Width="444px" MultiLine="true" Height="150px" />
                    </td>
                </tr>
                <tr>
                    <td valign=top>
                    <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="注意事項" Width="120px" />
                    </td>
                    <td>
                    <cc1:SingleField ID="SMVAAAB014" runat="server" Width="444px" MultiLine="true" Height="60px" />
                    </td>
                </tr>
                <tr>
                    <td valign=top>
                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="說明文件" Width="120px" />
                    </td>
                    <td>
                    <cc1:SingleField ID="SMVAAAB010" runat="server" Width="444px" />
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
