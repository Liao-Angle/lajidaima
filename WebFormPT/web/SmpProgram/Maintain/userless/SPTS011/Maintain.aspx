<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>年度教育訓練計劃執行檢核表</title>
    <style type="text/css">
        .style1
        {
            width: 92px;
        }
        .style3
        {
            width: 59px;
        }
        .style4
        {
            width: 167px;
        }
        .style5
        {
            width: 154px;
        }
        .style8
        {
            width: 92px;
            height: 31px;
        }
        .style9
        {
            width: 167px;
            height: 31px;
        }
        .style10
        {
            height: 31px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">       
        <table border="0" cellspacing="3" cellpadding=0 width="100%" style="font-size:9pt"> 
            <tr>
                <td><cc1:DSCLabel ID="LblCompanyCode" runat="server" Text="公司別" 
                        Width="110px" TextAlign="2" Height="16px" /></td>
	            <td><cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="113px" /> </td>                        
			    <td class="style8">
                    <cc1:DSCLabel ID="lblDept" runat="server" Width="110px" 
                        Text="開課部門" TextAlign="2"/></td>
                <td class="style9">
                    <cc1:SingleOpenWindowField ID="CheckDept" runat="server" Width="430px" 
			            showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
			            tableName="OrgUnit" FixReadOnlyValueTextWidth="240px" FixValueTextWidth="100px" 
                        style="margin-left: 0px" /></td>
                <td class="style10">
                    <cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" 
                        OnClick="SearchButton_Click" showWaitingIcon="True" Text="開始查詢" Width="102px" />
                </td>
            </tr>
			<tr>
				<td class="style3">
                    <cc1:DSCLabel ID="lblStartDate" runat="server" Width="110px" 
                        Text="計劃開課月份(起)" TextAlign="2" Height="16px"/></td>
				<td class="style5">
                    <cc1:SingleDateTimeField ID="StartDate" runat="server" 
                        Width="100px" DateTimeMode="5" /></td>
				<td class="style1">
                    <cc1:DSCLabel ID="lblEndDate" runat="server" Width="110px" 
                        Text="計劃開課月份(迄)" TextAlign="2" Height="16px"/></td>
				<td class="style4">
                    <cc1:SingleDateTimeField ID="EndDate" runat="server" 
                        Width="100px" DateTimeMode="5" /></td>
                <td></td>
			</tr>
        </table>                    
        </cc1:DSCGroupBox>

        <cc1:DSCGroupBox ID="DSCGroupBox2" runat="server" Text="達成率=已結案實際開課量/(計劃開課量+新增開課量)" Height="16px">
        <table>
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblActual" runat="server" IsNecessary="False" Text="已結案實際開課量" 
                        TextAlign="2" width="144px" /></td>
	            <td class="style1"><cc1:SingleField ID="Actual" runat="server" Width="110px" /></td>
                <td align="right">
                    <cc1:DSCLabel ID="LblPlan" runat="server" IsNecessary="False" Text="計劃開課量" 
                        TextAlign="2" width="95px" /></td>
	            <td class="style1"><cc1:SingleField ID="Plan" runat="server" Width="110px" /></td>
                <td align="right">
                    <cc1:DSCLabel ID="LblNewCourse" runat="server" IsNecessary="False" Text="新增開課量" 
                        TextAlign="2" width="94px" /></td>
	            <td class="style1"><cc1:SingleField ID="NewCourse" runat="server" Width="110px" /></td>
                <td align="right">
                    <cc1:DSCLabel ID="LblAchieveRate" runat="server" IsNecessary="False" Text="達成率" 
                        TextAlign="2" width="171px" /></td>
	            <td class="style1"><cc1:SingleField ID="AchieveRate" runat="server" Width="110px" /></td>
            </tr>
        </table>
        </cc1:DSCGroupBox> 
        
        <table>           
			<tr>
                <td width="100%">
                    <cc1:OutDataList ID="ReqList" runat="server" Height="315px" Width="1022px" 
                        showExcel="True" /></td>
                
            </tr>            
        </table>    
    </div>
    </form>
</body>
</html>
