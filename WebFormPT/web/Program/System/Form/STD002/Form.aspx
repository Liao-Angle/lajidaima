<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_System_Form_STD002_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTD002001" runat="server" Text="請假人"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail><cc1:SingleOpenWindowField ID="STD002001" runat="server" Width="254px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTD002004" runat="server" Text="代理人"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="STD002004" runat="server" Width="254px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="Users" OnSingleOpenWindowButtonClick="STD002004_SingleOpenWindowButtonClick"/>
            </td>
        </tr>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTD002005" runat="server" Text="假別"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="STD002005" runat="server" />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
                &nbsp;</td>
            <td width="240px" class=BasicFormHeadDetail>&nbsp;
            </td>
        </tr>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTD002006" runat="server" Text="請假起始"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="STD002006" runat="server" />
                </td>
            <td width="80px" align=right class=BasicFormHeadHead><cc1:DSCLabel ID="LBSTD002007" runat="server" Text="請假截止" />
                </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="STD002007" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="80px" valign=top align=right class=BasicFormHeadHead>
                </td>
            <td colspan=3 class=BasicFormHeadDetail>
            </td>
        </tr>
        <tr>
            <td width="80px" valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTD002008" runat="server" Text="說明"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="STD002008" runat="server" Width="100%" Height="64px" MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
