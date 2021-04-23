<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCAuditService_Maintain_SMXE_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="分析時間區段" Width="589px">
        <table border=0 width=100% cellspacing=3 cellpadding=0>
        <tr>
            <td>
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="開始時間" />
            </td>
            <td>
                <cc1:SingleDateTimeField ID="StartTime" runat="server" DateTimeMode="1" />
            </td>
            <td>
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="結束時間" />
            </td>
            <td>
                <cc1:SingleDateTimeField ID="EndTime" runat="server" DateTimeMode="1" />
            </td>
        </tr>
        </table>
        </cc1:DSCGroupBox>
        <br />
        <cc1:DSCGroupBox ID="DSCGroupBox2" runat="server" Text="分析方式" Width="589px">
        <table border=0 width=100% cellspacing=3 cellpadding=0>
        <tr>
            <td>
                <cc1:DSCRadioButton ID="TimeInterval" GroupName="METHOD" Text="時間區隔" runat="server" Checked="true" />
            </td>
            <td>
                <cc1:DSCRadioButton ID="TIMonth" GroupName="TI" Text="月" runat="server" Checked="true" />
                <cc1:DSCRadioButton ID="TIDay" GroupName="TI" Text="日" runat="server" />
                <cc1:DSCRadioButton ID="TIHour" GroupName="TI" Text="時" runat="server" />
                <cc1:DSCRadioButton ID="TIMinute" GroupName="TI" Text="分" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DSCRadioButton ID="TypeInterval" GroupName="METHOD" Text="性質分類" runat="server" />
            </td>
            <td>
                <cc1:DSCRadioButton ID="TYApplication" GroupName="TY" Text="應用程式" runat="server" Checked="true" />
                <cc1:DSCRadioButton ID="TYModule" GroupName="TY" Text="模組別" runat="server" />
                <cc1:DSCRadioButton ID="TYProgram" GroupName="TY" Text="程式別" runat="server" />
                <cc1:DSCRadioButton ID="TYUser" GroupName="TY" Text="使用者" runat="server" />
                <cc1:DSCRadioButton ID="TYLevel" GroupName="TY" Text="錯誤層級" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign=top style="font-size:9pt">
                <cc1:DSCRadioButton ID="SQLInterval" GroupName="METHOD" Text="SQL語句" runat="server" /><br />
                FLD:欄位值<br>
                CCS:統計值<br />
            </td>
            <td>
                <cc1:SingleField ID="SQLField" runat="server" Height="86px" MultiLine="True" Width="458px" />
            </td>
        </tr>
        </table>
        </cc1:DSCGroupBox>
        <br />
        <cc1:DSCGroupBox ID="DSCGroupBox3" runat="server" Text="顯示設定" Width="589px">
        <table border=0 width=100% cellspacing=3 cellpadding=0>
        <tr>
            <td>
                <cc1:SingleDropDownList ID="BarType" runat="server" />
            </td>
        </tr>
        </table>
        </cc1:DSCGroupBox>
        <br />
        <cc1:GlassButton ID="OKButton" runat="server" ImageUrl="~/Images/OK.gif" OnClick="OKButton_Click"
            Text="分析" Width="101px" />
    
    </div>
    </form>
</body>
</html>
