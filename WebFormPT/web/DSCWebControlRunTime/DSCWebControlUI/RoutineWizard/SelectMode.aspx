<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectMode.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_SelectMode" %>

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
                <cc1:dsclabel id="DSCLabel1" runat="server" text="這個工作的執行方式：" width="139px"></cc1:dsclabel>
                <br />
                <cc1:dscradiobutton id="DAYMODE" runat="server" groupname="RUNMODE"
                    text="每日"></cc1:dscradiobutton>
                <br />
                <cc1:dscradiobutton id="WEEKMODE" runat="server" groupname="RUNMODE" text="每週"></cc1:dscradiobutton>
                <br />
                <cc1:dscradiobutton id="MONTHMODE" runat="server" groupname="RUNMODE" text="每月"></cc1:dscradiobutton>
            </td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
        <tr>
            <td align=center>
                <cc1:GlassButton ID="NextPage" runat="server" Text="下一步" Width="86px" OnClick="NextPage_Click" />
                <cc1:GlassButton ID="CancelButton" runat="server" Text="取消" Width="78px" OnClick="CancelButton_Click" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
