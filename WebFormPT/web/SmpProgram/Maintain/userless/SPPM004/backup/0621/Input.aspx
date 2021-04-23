<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPPM004_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>考核表評核作業</title>
</head>
<body>
    <table width="100%">
        <tr><td><cc1:GlassButton ID="GbSendAssessmentScore" runat="server" Height="16px" 
                        Text="送出成績" Width="100px" ConfirmText="送出成績後將不能再異動資料, 請確認?" 
                onclick="GbSendAssessmentScore_Click" /></td>
        </tr>
    </table>
    <form id="form1" runat="server">
    <fieldset style="width: 780px">
        <legend>評核作業維護</legend>
        <table style="margin-left:4px; width: 780px;" border=0 cellspacing=0 cellpadding=1 > 
        <tr><td><cc1:DSCLabel ID="LblAssessmentName" runat="server" Width="90px" 
                Text="考評名稱" TextAlign="2" /></td>
                <td colspan=5>
                    <cc1:SingleField ID="AssessmentName" runat="server" Width="314px" 
                        ReadOnly="True" /></td>
                <td><cc1:DSCLabel ID="LblStage" runat="server" Width="90px" Text="評核階段" 
                        TextAlign="2" /></td>
                <td><cc1:SingleDropDownList ID="Stage" runat="server" Width="100px" ReadOnly="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblEmpName" runat="server" Width="90px" Text="姓名" 
                TextAlign="2" /></td>
            <td><cc1:SingleField ID="empName" runat="server" Width="80px" ReadOnly="True" /></td>
            <td><cc1:DSCLabel ID="LblDeptName" runat="server" Width="80px" Text="部門" 
                    TextAlign="2" Height="16px" /></td>
            <td><cc1:SingleField ID="deptName" runat="server" Width="120px" ReadOnly="True" /></td>
            <td><cc1:DSCLabel ID="LblAssessmentDays" runat="server" Width="80px" Text="評核天數" TextAlign="2" /></td>
            <td><cc1:SingleField ID="AssessmentDays" runat="server" Width="40px" 
                    ReadOnly="True" /></td>
            <td><cc1:DSCLabel ID="LblStatus" runat="server" Width="90px" Text="評核狀態" 
                    TextAlign="2" /></td>
            <td><cc1:SingleDropDownList ID="Status" runat="server" Width="100px" 
                    ReadOnly="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblSelfComments" runat="server" Width="90px" Text="自評意見" 
                TextAlign="2" /></td>
            <td colspan=5>
                <cc1:SingleField ID="SelfComments" runat="server" Width="450px" 
                    MultiLine="True" /></td>
            <td><cc1:DSCLabel ID="LblSelfScore" runat="server" Width="90px" Text="自評總分" 
                    TextAlign="2" Height="16px" /></td>
            <td><cc1:SingleField ID="SelfScore" runat="server" Width="80px" ReadOnly="True" /></td>
        </tr>
        <tr><td>
            <cc1:DSCLabel ID="LblFirstComments" runat="server" Width="90px" 
                Text="一階評核意見" TextAlign="2" Display="False" /></td>
            <td colspan=5>
                <cc1:SingleField ID="FirstComments" runat="server" Width="450px" 
                    MultiLine="True" Display="False" /></td>
            <td><cc1:DSCLabel ID="LblFirstScore" runat="server" Width="90px" Text="一階評核總分" 
                    TextAlign="2" Display="False" /></td>
            <td><cc1:SingleField ID="FirstScore" runat="server" Width="80px" ReadOnly="True" 
                    Display="False" /></td>
        </tr>
        <tr><td>
            <cc1:DSCLabel ID="LblSecondComments" runat="server" Width="90px" 
                Text="二階評核意見" TextAlign="2" Display="False" /></td>
            <td colspan=5>
                <cc1:SingleField ID="SecondComments" runat="server" Width="450px" 
                    MultiLine="True" Display="False" /></td>
            <td><cc1:DSCLabel ID="LblSecondScore" runat="server" Width="90px" Text="二階評核總分" 
                    TextAlign="2" Display="False" /></td>
            <td><cc1:SingleField ID="SecondScore" runat="server" Width="80px" ReadOnly="True" 
                    Display="False" /></td>
        </tr>
    </table>
    </fieldset>
    <table style="width: 780px">
        <tr><td width="100%" height="100%">
                <cc1:DataList ID="AssessmentScoreDetailList" runat="server" Height="295px" 
                    Width="100%" showExcel="True" ReadOnly="False" NoAdd="True" 
                    NoDelete="True" IsHideSelectAllButton="True" IsShowCheckBox="False" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
