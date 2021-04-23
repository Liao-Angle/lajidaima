<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVR_Detail" %>

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
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="522px" Width="536px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="基本設定">
                <TabBody>
                    <table border=0 width=510>
                    <tr>
                        <td valign=top width=120><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="班別代號" Width="120px" /></td>
                        <td >
                            <cc1:SingleField ID="SMVRAAA002" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td valign=top width=120><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="班別名稱" Width="120px" /></td>
                        <td >
                            <cc1:SingleField ID="SMVRAAA003" runat="server" Width="353px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign=top width=120><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="說明" Width="120px" /></td>
                        <td >
                            <cc1:SingleField ID="SMVRAAA004" runat="server" Height="84px" MultiLine="True" Width="353px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan=2 style="font-size:10pt">
                            <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="行事曆" Height="237px">
                                <cc1:DataList ID="ABList" runat="server" Height="263px" Width="492px" />
                            </cc1:DSCGroupBox>
                        </td>
                    </tr>
                    </table>                
                </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="批次設定">
                <TabBody>
                    <table border=0 width=510>
                    <tr>
                        <td valign=top style="width: 149px"><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="自動產生行事曆" Width="120px" /></td>
                        <td colspan=3>
                            <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="年度" Width="51px" />
                            <cc1:SingleDropDownList ID="YEARF" runat="server" Width="91px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign=top style="width: 149px">
                            <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="上班時間" Width="56px" />
                            <cc1:DSCCheckBox ID="PrevDay1" runat="server" Text="前一天" />
                        </td>
                        <td valign=bottom style="width: 88px">
                            <cc1:SingleDateTimeField ID="SMVRAAB004" runat="server" DateTimeMode="2" Width="78px" />
                        </td>
                        <td valign=top style="width: 155px"><cc1:DSCLabel ID="DSCLabel9" runat="server" Text="下班時間" Width="57px" /><cc1:DSCCheckBox ID="NextDay1" runat="server" Text="後一天" /></td>
                        <td >
                            <cc1:SingleDateTimeField ID="SMVRAAB005" runat="server" DateTimeMode="2" Width="78px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign=top style="width: 149px"><cc1:DSCLabel ID="DSCLabel10" runat="server" Text="午休開始時間" Width="83px" /><cc1:DSCCheckBox ID="PrevDay2" runat="server" Text="前一天" /></td>
                        <td style="width: 88px" >
                            <cc1:SingleDateTimeField ID="SMVRAAB007" runat="server" DateTimeMode="2" Width="78px" />
                        </td>
                        <td valign=top style="width: 155px"><cc1:DSCLabel ID="DSCLabel11" runat="server" Text="午休結束時間" Width="85px" /><cc1:DSCCheckBox ID="NextDay2" runat="server" Text="後一天" /></td>
                        <td >
                            <cc1:SingleDateTimeField ID="SMVRAAB008" runat="server" DateTimeMode="2" Width="78px" />
                        </td>
                    </tr>    
                    <tr>
                        <td>
                            <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="上班日" />
                        </td>
                        <td colspan=3>
                            <cc1:DSCCheckBox ID="SUNDAY" runat="server" Text="日" />
                            <cc1:DSCCheckBox ID="MONDAY" runat="server" Checked=true Text="一" />
                            <cc1:DSCCheckBox ID="TUESDAY" runat="server" Checked=true Text="二" />
                            <cc1:DSCCheckBox ID="WEDNESDAY" runat="server" Checked=true Text="三" />
                            <cc1:DSCCheckBox ID="THURSDAY" runat="server" Checked=true Text="四" />
                            <cc1:DSCCheckBox ID="FRIDAY" runat="server" Checked=true Text="五" />
                            <cc1:DSCCheckBox ID="SATURDAY" runat="server" Text="六" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan=4>
                            <cc1:GlassButton ID="GenerateSchedule" runat="server" Text="自動產生" Width="107px" OnClick="GenerateSchedule_Click" />
                        </td>
                    </tr>                
                    <tr>
                        <td colspan=4><hr /></td>
                    </tr>                
                    <tr>
                        <td valign=top style="width: 149px"><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="刪除年度行事曆" Width="120px" /></td>
                        <td colspan=3>
                            <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="年度" Width="51px" />
                            <cc1:SingleDropDownList ID="YEARF2" runat="server" Width="91px" />
                            <cc1:GlassButton ID="DeleteSchedule" runat="server" Text="刪除" Width="107px" ConfirmText="你確定要刪除此年度行事曆?" OnClick="DeleteSchedule_Click" />
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
