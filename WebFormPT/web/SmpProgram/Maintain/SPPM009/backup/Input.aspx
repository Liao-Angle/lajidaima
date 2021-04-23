<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPPM003_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>考核計畫維護</title>

</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 780px">
        <legend>考核計畫</legend>
        <table style="margin-left:4px; width: 780px;" border=0 cellspacing=0 cellpadding=1 >  
            <tr><td><cc1:DSCLabel ID="LblCompanyCode" runat="server" Width="70px" Text="公司別" 
                    TextAlign="2" /></td>
                <td><cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="131px" /></td>
                <td><cc1:DSCLabel ID="LblAssessYear" runat="server" Width="70px" Text="考評年度" 
                        TextAlign="2" /></td>
                <td><cc1:SingleField ID="AssessYear" runat="server" Width="100px"/></td>
                <td><cc1:DSCLabel ID="LblStatus" runat="server" Width="70px" Text="狀態" 
                        TextAlign="2" /></td>
                <td><cc1:SingleDropDownList ID="Status" runat="server" Width="100px" ReadOnly="True" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblAssessDate" runat="server" Width="70px" Text="考評期間" 
                    TextAlign="2" /></td>
                <td width="220px"><cc1:SingleDateTimeField ID="AssessStartDate" runat="server" Width="100px"/>~
                    <cc1:SingleDateTimeField ID="AssessEndDate" runat="server" Width="100px"/></td>
                <td><cc1:DSCLabel ID="LblPlanEndDate" runat="server" Width="70px" Text="截止日" 
                        TextAlign="2" /></td>
                <td><cc1:SingleDateTimeField ID="PlanEndDate" runat="server" Width="100px" /></td>
                <td><cc1:DSCLabel ID="LblStartDate" runat="server" Width="70px" Text="開始日" 
                        TextAlign="2" /></td>
                <td><cc1:SingleField ID="StartDate" runat="server" Width="125px" ReadOnly="True" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblAssessmentName" runat="server" Width="80px" 
                    Text="考評名稱" TextAlign="2" /></td>
                <td colspan=3><cc1:SingleField ID="AssessmentName" runat="server" Width="300px" /></td>
                <td><cc1:DSCLabel ID="LblCloseDate" runat="server" Width="70px" Text="結案日" 
                        TextAlign="2" /></td>
                <td><cc1:SingleField ID="CloseDate" runat="server" Width="125px" ReadOnly="True" /></td>
            </tr>
        </table>
    </fieldset>
    <table style="width: 780px">
        <tr><td width="100%" height="100%">
                <cc1:DataList ID="UserAssessmentStageList" runat="server" Height="400px" 
                    Width="100%" showExcel="True" ReadOnly="False" NoAdd="True" 
                    NoDelete="True" isShowAll="True" showTotalRowCount="True" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
