<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain_SPPM007_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>考評記錄查詢</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr><td><cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢條件">
                <table>
	                <tr>
	                    <td><cc1:DSCLabel ID="LblAssessmentName" runat="server" Width="80px" Text="考核名稱" 
                                TextAlign="2"/></td>
	                    <td><cc1:SingleDropDownList ID="AssessmentPlanGUID" runat="server" 
                                showReadOnlyField="true" Width="300px" /></td>	
                        <td><cc1:DSCLabel ID="LblAchievementLevel" runat="server" Width="91px" Text="等級" 
                                TextAlign="2" Height="16px"/></td>
	                    <td><cc1:SingleDropDownList ID="AchievementLevel" runat="server" showReadOnlyField="true" 
                                Width="90px" /></td>						
	            		<td><cc1:GlassButton ID="GbSearch" runat="server" ImageUrl="~/Images/OK.gif" 
                                Text="開始查詢" Width="100px" showWaitingIcon="True" onclick="GbSearch_Click" /></td>						
	                </tr>
                    <tr>                      
                        <td><cc1:DSCLabel ID="lblCheckDept" runat="server" Text="部門名稱" TextAlign="2" 
                                Width="80px" /></td>                      
                        <td colspan="4">
                            <cc1:SingleOpenWindowField ID="CheckDept" runat="server" 
                                FixReadOnlyValueTextWidth="240px" FixValueTextWidth="100px" guidField="OID" 
                                keyField="id" serialNum="003" 
                                showReadOnlyField="True" style="margin-left: 0px" tableName="OrgUnit" 
                                Width="430px" Display="True" /></td>
                    </tr>
                </table>
                </cc1:DSCGroupBox>
            </td>
        </tr>
		<tr>
            <td width="100%" height="100%">
                <cc1:DataList ID="AchievementList" runat="server" Height="315px" 
                    Width="100%" showExcel="True" ReadOnly="False" NoAdd="True" 
                    NoDelete="True" />
            </td>
        </tr>
    </table> 
    </form>
</body>
</html>
