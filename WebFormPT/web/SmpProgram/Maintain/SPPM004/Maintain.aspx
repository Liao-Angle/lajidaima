<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain_SPPM004_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>評核作業</title>
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
                        <td><cc1:DSCLabel ID="LblStatus" runat="server" Width="80px" Text="評核狀態" 
                                TextAlign="2" Height="16px"/></td>
	                    <td><cc1:SingleDropDownList ID="Status" runat="server" showReadOnlyField="true" 
                                Width="90px" /></td>
                         <td><cc1:DSCLabel ID="lblAccount" runat="server" Text="帳號" TextAlign="2" Width="50px" /> </td>
                         <td><cc1:SingleField ID="sfAccount" runat="server" Width="60px" /> </td>
                         <td><cc1:DSCLabel ID="lblPassword" runat="server" Text="密碼" TextAlign="2"  Width="40px" /></td>
                         <td align="left"><cc1:SingleField ID="sfPassword" runat="server" Width="60px" isPassword="True" /></td>     						
	            		<td><cc1:GlassButton ID="GbSearch" runat="server" ImageUrl="~/Images/OK.gif" 
                                Text="開始查詢" Width="100px" showWaitingIcon="True" onclick="GbSearch_Click" /></td>
                        <td><cc1:GlassButton ID="GbSubmit" runat="server" ImageUrl="~/Images/GeneralForward.gif" 
                                 showWaitingIcon="True" Text="批次提交成績" Width="120px"  
                                ConfirmText="提交成績後將不能再異動資料, 請確認?" Display="True" onclick="GbSubmit_Click" /> </td>                
	                </tr>
                </table>
                </cc1:DSCGroupBox>
            </td>
        </tr>
		<tr>
            <td width="100%" height="100%">
                <cc1:DataList ID="AssessmentStageList" runat="server" Height="383px" 
                    Width="100%" showExcel="True" ReadOnly="False" NoAdd="True" 
                    NoDelete="True" showTotalRowCount="True" isShowAll="True" 
                    IsShowCheckBox="False" PageSize="1" />
            </td>
        </tr>
    </table> 
    </form>
</body>
</html>
