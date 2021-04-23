<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_System_Form_STDDOC_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .style1
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
            background-color: rgb(225,217,196);
            font-size: 9pt;
            font-family: Arial;
            height: 32px;
        }
        .style2
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
            background-color: white;
            font-size: 9pt;
            font-family: Arial;
            height: 32px;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class="BasicFormHeadBorder">
        <tr>
            <td width="80px" align=right class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBSTDDOC013" runat="server" Text="發文日期"></cc1:DSCLabel>
            </td>
            <td width="240px" class="BasicFormHeadDetail">
                <cc1:SingleDateTimeField ID="STDDOC013" runat="server" DateTimeMode="3" 
                    Width="232px" />
            </td>
            <td width="80px" align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBSTDDOC001" runat="server" Text="發文者"></cc1:DSCLabel>
            </td>
            <td width="240px" class="BasicFormHeadDetail">
                <cc1:SingleOpenWindowField ID="STDDOC001" runat="server" Width="254px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
            </td>
        </tr>
        <tr>
            <td width="80px" align="right" class="style1">
                <cc1:DSCLabel ID="LBSTDDOC006" runat="server" Text="緊急度"></cc1:DSCLabel>
            </td>
            <td width="240px" class="style2">
                <cc1:SingleDropDownList ID="STDDOC006" runat="server" />
            </td>
            <td width="80px" align="right" class="style1">
                <cc1:DSCLabel ID="LBSTDDOC007" runat="server" Text="重要性"></cc1:DSCLabel>
            </td>
            <td width="240px" class="style2">
                <cc1:SingleDropDownList ID="STDDOC007" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="80px" align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBSTDDOC008" runat="server" Text="敏感度"></cc1:DSCLabel>
            </td>
            <td width="240px" class="BasicFormHeadDetail">
                <cc1:SingleDropDownList ID="STDDOC008" runat="server" />
            </td>
            <td colspan=2 class="BasicFormHeadDetail">
                <cc1:DSCCheckBox ID="STDDOC012" runat="server" Text="需進秘書室" Width="258px" />
            </td>
        </tr>
        <tr>
            <td width="80px" valign="top" align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBSTDDOC009" runat="server" Text="主旨"></cc1:DSCLabel>
            </td>
            <td colspan=3 class="BasicFormHeadDetail">
                <cc1:SingleField ID="STDDOC009" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td width="80px" valign=top align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTDDOC010" runat="server" Text="辦法"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="STDDOC010" runat="server" Width="100%" Height="64px" MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td width="80px" valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTDDOC011" runat="server" Text="說明"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="STDDOC011" runat="server" Width="100%" Height="64px" MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
