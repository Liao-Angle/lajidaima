<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH010501_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .style1
        {
            height: 24px;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="100px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBApplyDate" runat="server" Text="申請日期" Width="100%"></cc1:DSCLabel></td>
            <td width="220px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="ApplyDate" runat="server" Width="150px" ReadOnly="True" />
            </td>
            <td width="100px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBIssueDate" runat="server" Text="預定錄用日期" Width="100%"></cc1:DSCLabel></td>
            <td width="220px" class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="IssueDate" runat="server" 
                                    Width="120px" />  
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBDepartment" runat="server" Text="申請部門" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Department" runat="server" Width="150px" ReadOnly="True" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBQuantity" runat="server" Text="現有編制人數" Width="100%"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Quantity" runat="server" Width="60px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBIssueType" runat="server" Text="類別" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="IssueType" 
                    runat="server" Width="110px" /> 
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBIssueQty" runat="server" Text="需求人數" Width="100%"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="IssueQuantity" runat="server" Width="60px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBReason" runat="server" Text="需求原因" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="Reason" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBIssuePosition" runat="server" Text="職位需求" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="IssuePosition" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBGender" runat="server" Text="性別" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="Gender" 
                    runat="server" Width="110px" /> 
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBMilitary" runat="server" Text="兵役" Width="100%"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="Military" 
                    runat="server" Width="110px" /> 
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBAgeMax" runat="server" Text="年齡最高" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="AgeMax" runat="server" Width="60px" ReadOnly="True" />
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBAgeMin" runat="server" Text="年齡最低" Width="100%"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="AgeMin" runat="server" Width="60px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEducation" runat="server" Text="學歷" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="Education" 
                    runat="server" Width="110px" /> 
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSubject" runat="server" Text="科系" Width="100%"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Subject" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px" colspan="4">
                <cc1:DSCLabel ID="LBTechnical" runat="server" Text="具體技能與人格特質" Width="100%"></cc1:DSCLabel>
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadDetail style="height: 24px" colspan="4">
                <cc1:SingleField ID="Technical" runat="server" Width="100%" Height="68px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead colspan="4">
                <cc1:DSCLabel ID="LBJobDetail" runat="server" Text="工作職務說明 (技術人員以上需填寫)" 
                    Width="100%"></cc1:DSCLabel>
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadDetail colspan="4">
                <cc1:SingleField ID="JobDetail" runat="server" Width="100%" Height="68px" 
                    MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
