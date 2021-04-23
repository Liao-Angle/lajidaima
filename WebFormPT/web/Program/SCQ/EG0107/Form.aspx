<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EG0107_Form" %>

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
        <table style="margin-left:4px" border=0 width=800px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="受訪者員工"></cc1:DSCLabel></td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" 
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" 
                    OnSingleOpenWindowButtonClick="RequestID_SingleOpenWindowButtonClick" 
                    valueIndex="1" idIndex="0" />
            </td>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBVisitDateTime" runat="server" Text="受訪時間" />
                </td>
            <td width="280px" class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="VisitDateTime" runat="server" 
                                    Width="200px" DateTimeMode="1" />  
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBMobile" runat="server" Text="聯絡電話"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Mobile" runat="server" Width="80%" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBExtension" runat="server" Text="分機"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Extension" runat="server" Width="80%" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBVisitorName" runat="server" Text="訪客姓名"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="VisitorName" runat="server" Width="80%" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBVisitorGender" runat="server" Text="訪客性別" />
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="VisitorGender" 
                    runat="server" Width="87px" /> 
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBVisitorCompany" runat="server" Text="訪客公司"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="VisitorCompany" runat="server" Width="100%" />
            </td>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBFollowingCount" runat="server" Text="隨行人數"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="FollowingCount" runat="server" Width="60px" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBReason" runat="server" Text="來訪事由" 
                    style="margin-left: 0px"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        <panel runat="server" id="EditPanel" visible="false">
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBIDType" runat="server" Text="訪客證件" 
                    style="margin-left: 0px"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="IDType" 
                    runat="server" Width="150px" /> 
            </td>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBIDNumber" runat="server" Text="證件編號" 
                    style="margin-left: 0px"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="IDNumber" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBComeInDateTime" runat="server" Text="入廠時間" 
                    style="margin-left: 0px"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:DSCLabel ID="ComeInDateTime" runat="server" 
                    style="margin-left: 0px"></cc1:DSCLabel>
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBAwayDateTime" runat="server" Text="出廠時間" 
                    style="margin-left: 0px"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:DSCLabel ID="AwayDateTime" runat="server" 
                    style="margin-left: 0px"></cc1:DSCLabel>
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBNote" runat="server" Text="備註"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Note" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        </panel>
        </table>
    </div>
    </form>
</body>
</html>
