<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Schedule.aspx.cs" Inherits="Program_System_Maintain_SMVR_Schedule" %>

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
    <table border=0 width=510px>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="上班日期" Width="120px" /></td>
        <td >
            <cc1:SingleDateTimeField ID="SMVRAAB003" runat="server" DateTimeMode="0" Width="158px" />
        </td>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="時數" Width="120px" /></td>
        <td >
            <cc1:SingleField ID="SMVRAAB006" runat="server" alignRight="True" Fractor="1" isMoney="True" ReadOnly="True" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="上班時間" Width="120px" /></td>
        <td >
            <cc1:SingleDateTimeField ID="SMVRAAB004" runat="server" DateTimeMode="1" Width="158px" OnDateTimeClick="SMVRAAB004_DateTimeClick" />
        </td>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="下班時間" Width="120px" /></td>
        <td >
            <cc1:SingleDateTimeField ID="SMVRAAB005" runat="server" DateTimeMode="1" Width="158px" OnDateTimeClick="SMVRAAB005_DateTimeClick" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="午休開始時間" Width="120px" /></td>
        <td >
            <cc1:SingleDateTimeField ID="SMVRAAB007" runat="server" DateTimeMode="1" Width="158px" OnDateTimeClick="SMVRAAB007_DateTimeClick" />
        </td>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="午休結束時間" Width="120px" /></td>
        <td >
            <cc1:SingleDateTimeField ID="SMVRAAB008" runat="server" DateTimeMode="1" Width="158px" OnDateTimeClick="SMVRAAB008_DateTimeClick" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
