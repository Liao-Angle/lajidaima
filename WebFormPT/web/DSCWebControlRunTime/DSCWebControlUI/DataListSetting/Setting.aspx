<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Setting.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_DataListSetting_Setting" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="361px" Width="662px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage8" runat="server" Enabled="True" ImageURL="" Title="一般設定" Hidden="False">
                    <TabBody>
                        <table border=0 cellspacing=3 cellpadding=0>
                            <tr>
                                <td colspan=2>
                                    <cc1:DSCCheckBox ID="aggregateInTitle" runat="server" Text="是否將彙總欄位顯示在分群標題" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="若無指定金額欄位時, 統計欄位小數點數量" />
                                </td>
                                <td>
                                    <cc1:SingleField ID="aggregateDigit" runat="server" isMoney="True" alignRight="True" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan=2>
                                    <cc1:DSCCheckBox ID="multiLine" runat="server" Text="是否顯示多行" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="多行顯示時最大行數(-1表示不限制)" />
                                </td>
                                <td>
                                    <cc1:SingleField ID="multiLineRowCount" runat="server" isMoney="True" alignRight="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="金額欄位isAccount為true時的表示顏色(#000000格式)" />
                                </td>
                                <td>
                                    <cc1:SingleField ID="moneyFieldColor" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabelFrozenField" runat="server" Text="凍結窗格" />
                                </td>
                                <td>
                                    <cc1:SingleDropDownList ID="FrozenField" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage15" runat="server" Enabled="True" ImageURL="" Title="顯示欄位" Hidden="False">
                    <TabBody>
                        <table border=0 cellspacing=3 cellpadding=0>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="顯示欄位" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="隱藏欄位" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:MultiDropDownList ID="DisplayFrom" runat="server" Height="270px" Width="255px" />
                                </td>
                                <td style="width:51px">
                                    <cc1:GlassButton ID="DisplayRight" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/add_small.gif" OnClick="DisplayRight_Click" />
                                    <cc1:GlassButton ID="DisplayLeft" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/remove_small.gif" OnClick="DisplayLeft_Click" />
                                </td>
                                <td>
                                    <cc1:MultiDropDownList ID="DisplayTo" runat="server" Height="270px" Width="255px" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" Hidden="False" ImageURL=""
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
                <cc1:DSCTabPage ID="DSCTabPage9" runat="server" Enabled="True" ImageURL="" Title="金額欄位" Hidden="False">
                    <TabBody>
                        <table border=0 cellspacing=3 cellpadding=0>
                            <tr>
                                <td colspan=2>
                                    <cc1:DSCLabel ID="DSCLabel20" runat="server" Text="小數點位數" Width="80px" />
                                    <cc1:SingleField ID="DIGIT" runat="server" ValueText="2" Width="30px" isMoney="True" Fractor="0" />
                                    <cc1:DSCCheckBox ID="ISACCOUNT" runat="server" Text="是否負數以()表示" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="可選擇欄位" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel19" runat="server" Text="設定欄位" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:MultiDropDownList ID="MoneyFrom" runat="server" Height="270px" Width="255px" />
                                </td>
                                <td>
                                    <cc1:GlassButton ID="MoneyRight" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/add_small.gif" OnClick="MoneyRight_Click" />
                                    <br />
                                    <cc1:GlassButton ID="MoneyLeft" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/remove_small.gif" OnClick="MoneyLeft_Click" />
                                </td>
                                <td>
                                    <cc1:MultiDropDownList ID="MoneyTo" runat="server" Height="270px" Width="255px" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="分群欄位" Hidden="False">
                    <TabBody>
                        <table border=0 cellspacing=3 cellpadding=0>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="可選擇欄位" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="分群欄位" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:MultiDropDownList ID="GroupFrom" runat="server" Height="270px" Width="255px" />
                                </td>
                                <td>
                                    <cc1:GlassButton ID="GroupRight" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/add_small.gif" OnClick="GroupRight_Click" />
                                    <cc1:GlassButton ID="GroupLeft" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/remove_small.gif" OnClick="GroupLeft_Click" />
                                </td>
                                <td>
                                    <cc1:MultiDropDownList ID="GroupTo" runat="server" Height="270px" Width="255px" />
                                </td>
                                <td>
                                    <cc1:GlassButton ID="GroupUp" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/up_small.gif" OnClick="GroupUp_Click" />
                                    <cc1:GlassButton ID="GroupDown" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/down_small.gif" OnClick="GroupDown_Click" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage3" runat="server" Enabled="True" ImageURL="" Title="小計欄位" Hidden="False">
                    <TabBody>
                        <table border=0 cellspacing=3 cellpadding=0>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="可選擇欄位" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="統計欄位" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:MultiDropDownList ID="SumFrom" runat="server" Height="270px" Width="255px" />
                                </td>
                                <td>
                                    <cc1:GlassButton ID="SumRight" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/add_small.gif" OnClick="SumRight_Click" />
                                    <br />
                                    <cc1:GlassButton ID="SumLeft" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/remove_small.gif" OnClick="SumLeft_Click" />
                                </td>
                                <td>
                                    <cc1:MultiDropDownList ID="SumTo" runat="server" Height="270px" Width="255px" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage4" runat="server" Enabled="True" ImageURL="" Title="平均欄位" Hidden="False">
                    <TabBody>
                        <table border=0 cellspacing=3 cellpadding=0>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="可選擇欄位" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="統計欄位" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:MultiDropDownList ID="AverageFrom" runat="server" Height="270px" Width="255px" />
                                </td>
                                <td>
                                    <cc1:GlassButton ID="AverageRight" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/add_small.gif" OnClick="AverageRight_Click" />
                                    <br />
                                    <cc1:GlassButton ID="AverageLeft" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/remove_small.gif" OnClick="AverageLeft_Click" />
                                </td>
                                <td>
                                    <cc1:MultiDropDownList ID="AverageTo" runat="server" Height="270px" Width="255px" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>            
                <cc1:DSCTabPage ID="DSCTabPage5" runat="server" Enabled="True" ImageURL="" Title="標準差欄位" Hidden="False">
                    <TabBody>
                        <table border=0 cellspacing=3 cellpadding=0>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="可選擇欄位" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="統計欄位" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:MultiDropDownList ID="STDEVFrom" runat="server" Height="270px" Width="255px" />
                                </td>
                                <td>
                                    <cc1:GlassButton ID="STDEVRight" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/add_small.gif" OnClick="STDEVRight_Click" />
                                    <br />
                                    <cc1:GlassButton ID="STDEVLeft" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/remove_small.gif" OnClick="STDEVLeft_Click" />
                                </td>
                                <td>
                                    <cc1:MultiDropDownList ID="STDEVTo" runat="server" Height="270px" Width="255px" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage6" runat="server" Enabled="True" ImageURL="" Title="最大值欄位" Hidden="False">
                    <TabBody>
                        <table border=0 cellspacing=3 cellpadding=0>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="可選擇欄位" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="統計欄位" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:MultiDropDownList ID="MaxFrom" runat="server" Height="270px" Width="255px" />
                                </td>
                                <td>
                                    <cc1:GlassButton ID="MaxRight" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/add_small.gif" OnClick="MaxRight_Click" />
                                    <br />
                                    <cc1:GlassButton ID="MaxLeft" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/remove_small.gif" OnClick="MaxLeft_Click" />
                                </td>
                                <td>
                                    <cc1:MultiDropDownList ID="MaxTo" runat="server" Height="270px" Width="255px" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage> 
                <cc1:DSCTabPage ID="DSCTabPage7" runat="server" Enabled="True" ImageURL="" Title="最小值欄位" Hidden="False">
                    <TabBody>
                        <table border=0 cellspacing=3 cellpadding=0>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="可選擇欄位" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="統計欄位" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:MultiDropDownList ID="MinFrom" runat="server" Height="270px" Width="255px" />
                                </td>
                                <td>
                                    <cc1:GlassButton ID="MinRight" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/add_small.gif" OnClick="MinRight_Click" />
                                    <br />
                                    <cc1:GlassButton ID="MinLeft" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/remove_small.gif" OnClick="MinLeft_Click" />
                                </td>
                                <td>
                                    <cc1:MultiDropDownList ID="MinTo" runat="server" Height="270px" Width="255px" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>                                           
            </TabPages>
        </cc1:DSCTabControl>
        <br />
        <br />
        <cc1:GlassButton ID="OKButton" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/OK.gif"
            Text="確定" Width="94px" OnClick="OKButton_Click" />
        <cc1:GlassButton ID="NOButton" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/NO.gif"
            Text="取消" Width="94px" OnClick="NOButton_Click" />
    
    </div>
    </form>
</body>
</html>
