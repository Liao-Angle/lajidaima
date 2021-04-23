<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthMode.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_MonthMode" %>

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
                &nbsp;&nbsp;<br />
                <br />
                <cc1:DSCRadioButton ID="DAY" runat="server" GroupName="EXECUTEMODE" Text="日期：" />
                &nbsp;<cc1:SingleDropDownList ID="DAYS" runat="server" Width="99px" />
                <br />
                <cc1:DSCRadioButton ID="WEEK" runat="server" GroupName="EXECUTEMODE" Text="星期：" />
                &nbsp;<cc1:SingleDropDownList ID="ORDERS" runat="server" Width="99px" />
                <cc1:SingleDropDownList ID="WEEKDAY" runat="server" Width="99px" />
                <br /><cc1:DSCRadioButton ID="LASTDAY" runat="server" GroupName="EXECUTEMODE" Text="最後一日" />
                <br />
                <cc1:DSCRadioButton ID="LASTWEEKDAY" runat="server" GroupName="EXECUTEMODE" Text="最後一週間日" />
                <br />
                <br />
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="月份：" Width="225px" />
                <br />
                <table border=0>
                <tr>
                    <td>
                        <cc1:DSCCheckBox ID="M01" Text="一月" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="M02" Text="二月" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="M03" Text="三月" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="M04" Text="四月" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DSCCheckBox ID="M05" Text="五月" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="M06" Text="六月" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="M07" Text="七月" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="M08" Text="八月" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DSCCheckBox ID="M09" Text="九月" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="M10" Text="十月" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="M11" Text="十一月" runat="server" />
                    </td>
                    <td>
                        <cc1:DSCCheckBox ID="M12" Text="十二月" runat="server" />
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
