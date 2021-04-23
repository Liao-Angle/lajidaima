<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0105_Form" %>

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
            <td valign=top align=right class=BasicFormHeadHead width=60px>
                <cc1:DSCLabel ID="LBHead1" runat="server" Text="申請人"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="249px" 
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" 
                    OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" 
                    valueIndex="1" idIndex="0" />
            </td>
            <td valign=top align=right class=BasicFormHeadHead width=60px>
                <cc1:DSCLabel ID="LBHead3" runat="server" Text="部門"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="PartNo" runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBHead4" runat="server" Text="職稱"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="DtName" runat="server" Width="122px" />
            </td>
            <td valign=top align=right class=BasicFormHeadHead>
                英文名</td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="EName" runat="server" Width="122px" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBIP" runat="server" Text="IP"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="IP" runat="server" Width="180px" />
            </td>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBIntranet" runat="server" Text="上網方式"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="Intranet" 
                    runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBMAC" runat="server" Text="MAC"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="MAC" runat="server" Width="180px" />
            </td>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSSID" runat="server" Text="無線區域"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="SSID" 
                    runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBArea" runat="server" Text="上網地址"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="AreaData" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBReason" runat="server" Text="申請原因"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
