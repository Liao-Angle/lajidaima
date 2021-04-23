<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DSCRichEditSetting.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_DSCRichEdit_DSCRichEditSetting" %>

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
    <table border=0 cellspacing=0 cellpadding=5 class=BasicFormHeadBorder>
    <tr>
        <td class=BasicFormHeadHead>
            <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="縮排間隔(px)" />
        </td>
        <td class=BasicFormHeadDetail>
            <cc1:SingleField ID="IndentValue" runat="server" alignRight="True" isMoney="True" Width="254px" />
        </td>
    </tr>
    <tr>
        <td colspan="2" class=BasicFormHeadDetail>
            <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="清單項目設定" Width="578px">
            <table border=0 cellspacing=0 cellpadding=2 width=100% height=100%  class=BasicFormHeadBorder>
            <tr>
                <td class=BasicFormHeadHead>
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="前置字元" Width="79px" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="PrefixChar" runat="server" Width="77px" />
                </td>
                <td class=BasicFormHeadHead style="width: 23px">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="後置字元" Width="83px" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="PostfixChar" runat="server" Width="56px" />
                </td>
            </tr>
            <tr>
                <td class=BasicFormHeadHead>
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="序號清單" />
                </td>
                <td colspan=3 class=BasicFormHeadDetail>
                    <cc1:SingleField ID="ListOrder" runat="server" Width="494px" />
                </td>
            </tr>
            <tr>
                <td  class=BasicFormHeadDetail colspan=4 valign=top>
                    <table border=0 cellspacing=0 cellpadding=2>
                    <tr>
                        <td>
                            <cc1:OutDataList ID="LiList" runat="server" Height="209px" Width="556px" OnAddOutline="LiList_AddOutline" OnDeleteData="LiList_DeleteData" OnSaveRowData="LiList_SaveRowData" OnShowRowData="LiList_ShowRowData" />
                        </td>
                        <td valign=top>
                            <cc1:GlassButton ID="TOP" runat="server" Width="22px" Height="22px" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/DSCRichEdit/icon_up01.gif" OnClick="TOP_Click" CssClass="GeneralButton" /><br />
                            <cc1:GlassButton ID="UP" runat="server" Width="22px" Height="22px" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/DSCRichEdit/icon_up02.gif" OnClick="UP_Click" CssClass="GeneralButton" /><br />
                            <cc1:GlassButton ID="DOWN" runat="server" Width="22px" Height="22px" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/DSCRichEdit/icon_down02.gif" OnClick="DOWN_Click" CssClass="GeneralButton" /><br />
                            <cc1:GlassButton ID="BOTTOM" runat="server" Width="22px" Height="22px" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/DSCRichEdit/icon_down01.gif" OnClick="BOTTOM_Click" CssClass="GeneralButton" /><br />
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            </table>
            </cc1:DSCGroupBox>
        </td>
    </tr>
    <tr>
        <td class=BasicFormHeadDetail align=right>
            <cc1:GlassButton ID="OKButton" runat="server" Text="確定" Height="26px" Width="105px" OnClick="OKButton_Click" />
        </td>
        <td class=BasicFormHeadDetail align=left>
            <cc1:GlassButton ID="CancelButton" runat="server" Text="取消" Height="26px" Width="105px" OnClick="CancelButton_Click" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
