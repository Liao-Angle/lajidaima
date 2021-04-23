<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain_SPPM011_Maintain" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 200px;
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
    <table width="100%">
        <tr><td>
            <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢條件" Width="100%">
                <table>
	                <tr>
	                    <td><cc1:DSCLabel ID="LblAssessmentName" runat="server" Width="80px" Text="考核名稱" 
                                TextAlign="2"/></td>
	                    <td colspan="3">
                            <cc1:SingleDropDownList ID="sddlAssessmentPlanGUID" runat="server" 
                                showReadOnlyField="true" Width="300px" /></td>
                        <td>&nbsp;</td>
	                    <td>&nbsp;</td>
                        <td style=" width:140px;"><cc1:GlassButton ID="GbFind" runat="server" ImageUrl="~/Images/OK.gif" 
                                Text="開始查詢" Width="120px" showWaitingIcon="True" onclick="GbFind_Click" /></td>  
                        <td style=" width:220px;">
                            <cc1:GlassButton ID="gbAchievementFind" runat="server" 
                                ImageUrl="~/Images/OK.gif"  Width="200px" onclick="gbAchievementFind_Click" 
                                Text="成績分佈統計匯總查詢" />
                        </td>   
                        <td>
                            <cc1:GlassButton ID="gbAprove" runat="server" ImageUrl="~/Images/OK.gif" 
                                onclick="gbAprove_Click" Text="一鍵核准" Width="100px" />
                        </td>                                                           
                    </tr>                   
                </table>
            </cc1:DSCGroupBox>            
            </td>                              
        </tr>
		<tr>
            <td height="100%">
                <cc1:DataList ID="dlAchievementList" runat="server" Height="315px" 
                    Width="100%" showExcel="True" ReadOnly="False" NoAdd="True" 
                    NoDelete="True" IsShowCheckBox="False" showSerial="True" 
                    showTotalRowCount="True" />
            </td>
        </tr>
    </table> 
    </form>
</body>
</html>
