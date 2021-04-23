<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectTime.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_SelectTime" %>

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
                <cc1:dsclabel id="DSCLabel1" runat="server" text="請選擇工作開始的時間及日期：" width="225px"></cc1:dsclabel>
                <br />
                <br />
                <table border=0>
                <tr>
                    <td>
                        <cc1:dscradiobutton id="SINGLETIME" runat="server" groupname="ST" text="指定時間："></cc1:dscradiobutton>
                    </td>
                    <td>
                        <cc1:SingleDateTimeField ID="STARTTIME" runat="server" Width="151px" DateTimeMode="2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:dscradiobutton id="HOURTIME" runat="server" groupname="ST" text="每整點執行"></cc1:dscradiobutton>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:dscradiobutton id="HALFTIME" runat="server" groupname="ST" text="每半小時執行"></cc1:dscradiobutton>
                    </td>
                    <td>
                    </td>
                </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
        <tr>
            <td align=center>
                <cc1:GlassButton ID="PrevPage" runat="server" Text="上一步" Width="86px" OnClick="PrevPage_Click" /><cc1:GlassButton ID="ConfirmButton" runat="server" Text="確定" Width="86px" OnClick="ConfirmButton_Click" />
                <cc1:GlassButton ID="CancelButton" runat="server" Text="取消" Width="78px" OnClick="CancelButton_Click" />
            </td>
        </tr>
        </table>    </div>
    </form>
</body>
</html>
