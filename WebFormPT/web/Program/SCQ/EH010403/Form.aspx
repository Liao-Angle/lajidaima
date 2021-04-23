<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH010403_Form" %>

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
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工"></cc1:DSCLabel></td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" 
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" 
                    OnSingleOpenWindowButtonClick="RequestID_SingleOpenWindowButtonClick" 
                    valueIndex="1" idIndex="0" />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBRewardID" runat="server" Text="獎勵"></cc1:DSCLabel></td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="RewardID" 
                    runat="server" Width="87px" /> 
                <cc1:SingleDropDownList ID="Quantity" 
                    runat="server" Width="87px" /> 
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBDtName" runat="server" Text="職稱"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail><cc1:DSCLabel ID="DtName" runat="server" Text="" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px"><cc1:DSCLabel ID="LBTryUseDate" runat="server" Text="年資" />
                </td>
            <td class=BasicFormHeadDetail><cc1:DSCLabel ID="TryUseDate" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBReason" runat="server" Text="獎勵事由"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
