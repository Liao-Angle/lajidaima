<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DayMode.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_DayMode" %>

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
                
                
                <br />
                <cc1:dscradiobutton id="EVERYDAY" runat="server" groupname="RUNMODE" text="每日"></cc1:dscradiobutton>
                <br />
                <cc1:dscradiobutton id="WEEKDAY" runat="server" groupname="RUNMODE" text="週間日"></cc1:dscradiobutton>
                <br />
                <cc1:dscradiobutton id="EVERYINTERVAL" runat="server" groupname="RUNMODE" text="每隔"></cc1:dscradiobutton>
                <cc1:SingleDropDownList ID="DAYINTERVAL" runat="server" Width="74px" />
                <cc1:DSCLabel ID="Dsclabel3" runat="server" Text="天" Width="26px" />
                <br />
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
