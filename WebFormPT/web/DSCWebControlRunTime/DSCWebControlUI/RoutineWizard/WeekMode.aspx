<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeekMode.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_WeekMode" %>

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
        <table border=0 width=100% cellspacing=5 cellpadding=0>
        <tr>
            <td>
                <cc1:DSCLabel ID="Dsclabel4" runat="server" Text="這個工作的執行方式：" Width="225px" />
                &nbsp;<br />
                <br /><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="每隔" Width="45px" />
                &nbsp;<cc1:SingleDropDownList ID="WEEKINTERVAL" runat="server" Width="99px" />
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="週" Width="28px" />
                <br />
                &nbsp;
                <br /><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="請由以下選取工作日期：" Width="225px" />
                <br />
                <table border=0>
                <tr>
                    <td>
                        <cc1:DSCCheckBox ID="SUNDAY" Text="星期日" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="MONDAY" Text="星期一" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="TUESDAY" Text="星期二" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="WEDNESDAY" Text="星期三" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DSCCheckBox ID="THURSDAY" Text="星期四" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="FRIDAY" Text="星期五" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="SATURDAY" Text="星期六" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                </table>
                <br />
                <table border=0>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="開始日期：" Width="82px" />
                    </td>
                    <td>
                        <cc1:SingleDateTimeField ID="STARTDATE" runat="server" Width="151px" />
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
        <tr>
            <td align=center>
                <cc1:GlassButton ID="PrevPage" runat="server" Text="上一步" Width="86px" OnClick="PrevPage_Click" />
                <cc1:GlassButton ID="NextPage" runat="server" Text="下一步" Width="86px" OnClick="NextPage_Click" />
                <cc1:GlassButton ID="CancelButton" runat="server" Text="取消" Width="76px" OnClick="CancelButton_Click" />
            </td>
        </tr>
        </table>    
    </div>
    </form>
</body>
</html>
