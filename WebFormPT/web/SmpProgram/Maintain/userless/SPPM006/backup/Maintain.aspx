<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain_SPPM006_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>考核成績分配</title>
    <style type="text/css">
        .style3
        {
            width: 93%;
        }
        .style4
        {
            width: 390px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr><td class="style3">
            <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢條件" Width="916px">
                <table>
	                <tr>
	                    <td><cc1:DSCLabel ID="LblAssessmentName" runat="server" Width="80px" Text="考核名稱" 
                                TextAlign="2"/></td>
	                    <td class="style4"><cc1:SingleDropDownList ID="AssessmentPlanGUID" runat="server" 
                                showReadOnlyField="true" Width="200px" /></td>
                        <td><cc1:GlassButton ID="GbSearch" runat="server" ImageUrl="~/Images/OK.gif" 
                                Text="開始查詢" Width="100px" showWaitingIcon="True" onclick="GbSearch_Click" /></td>
                        <td><cc1:GlassButton ID="GbCount" runat="server" ImageUrl="" 
                                Text="成績分佈統計" Width="100px" showWaitingIcon="True" /></td>
                    </tr>
                    <tr>                       
                        <td><cc1:DSCLabel ID="lblDept" runat="server" Width="80px" 
                                Text="部門名稱" TextAlign="2"/></td>
                        <td colspan="3"><cc1:SingleOpenWindowField ID="CheckDept" runat="server" Width="430px" 
			                showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
			                tableName="OrgUnit" FixReadOnlyValueTextWidth="240px" FixValueTextWidth="100px" 
                            style="margin-left: 0px" onbeforeclickbutton="CheckDept_BeforeClickButton" /></td>
                                                  
                    </tr> 
                    <tr> 
						<td><cc1:DSCLabel ID="LblAchievementLevel" runat="server" Width="80px" Text="等級" 
                                TextAlign="2" Height="16px"/></td>
	                    <td><cc1:SingleDropDownList ID="AchievementLevel" runat="server" showReadOnlyField="true" 
                                Width="90px" /></td>
                    </tr> 
                </table>
            </cc1:DSCGroupBox>            
            <cc1:DSCGroupBox ID="DSCGroupBox2" runat="server" Height="94px" Text="統計人數"  style="margin-top: 0px" Width="287px">
                <table>
                    <tr><td><cc1:DSCLabel ID="LblTotal" runat="server" Width="80px" Text="評核人數" 
                                TextAlign="2"/></td>
                        <td><cc1:SingleField ID="Total" runat="server" Width="80px" /></td></tr>
                    <tr> <td>
                            <cc1:DSCLabel ID="LblComplete" runat="server" Width="80px" Text="已完成人數" 
                                TextAlign="2"/></td>
                        <td ><cc1:SingleField ID="Complete" runat="server" Width="80px" /></td></tr>
                    <tr> <td><cc1:DSCLabel ID="LblUnComplete" runat="server" Width="80px" Text="未完成人數" 
                                TextAlign="2"/></td>
                        <td><cc1:SingleField ID="UnComplete" runat="server" Width="80px" /></td></tr>
                </table>
            </cc1:DSCGroupBox>
            </td>                              
        </tr>
		<tr>
            <td height="100%" class="style3">
                <cc1:DataList ID="AchievementList" runat="server" Height="315px" 
                    Width="100%" showExcel="True" ReadOnly="False" NoAdd="True" 
                    NoDelete="True" />
            </td>
        </tr>
    </table> 
    </form>
</body>
</html>
