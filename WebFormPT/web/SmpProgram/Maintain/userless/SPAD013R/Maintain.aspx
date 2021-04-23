<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>文具申請查詢作業</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=3 cellpadding=0 width=100% style="font-size:9pt">
            <tr>
                <td width=100% >
                    <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">
                    <table border=0 cellspacing=0 cellpadding=3>
                    <tr>
						<td><cc1:DSCLabel ID="lblCompany" runat="server" Width="120px" Text="公司別："/></td>
                        <td><cc1:SingleDropDownList ID="Company" runat="server" Width="150px" Font-Strikeout="False" /></td>
                        <td><cc1:DSCLabel ID="lblUser" runat="server" Width="120px" Text="申請人："/></td>
                        <td><cc1:SingleOpenWindowField ID="CheckUser" runat="server" showReadOnlyField="true" guidField="OID" keyField="id" keyFieldType="STRING" serialNum="001" tableName="Users" Width="218px" /></td>
						<td><cc1:DSCLabel ID="lblDept" runat="server" Width="120px" Text="部門："/></td>
                        <td><cc1:SingleOpenWindowField ID="CheckDept" runat="server" Width="150px" 
			                  showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
			                  tableName="OrgUnit" /></td>
                    </tr>
					<tr>
						<td><cc1:DSCLabel ID="lblStartDate" runat="server" Width="120px" Text="預計購入日期起："/></td>
						<td><cc1:SingleDateTimeField ID="StartDate" runat="server" Width="140px" /></td>
						<td><cc1:DSCLabel ID="lblEndDate" runat="server" Width="120px" Text="預計購入日期訖："/></td>
						<td><cc1:SingleDateTimeField ID="EndDate" runat="server" Width="140px" /></td>
					</tr>
                    <tr>
                        <td colspan=4 align=right><cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" Text="開始查詢" Width="102px" OnClick="SearchButton_Click" showWaitingIcon="True" /></td>
                    </tr>
                    </table>
                    </cc1:DSCGroupBox>
                </td>
            </tr>
			<tr>
                <td width="100%" height="100%">
                    <cc1:OutDataList ID="ReqList" runat="server" Height="315px" Width="100%" showExcel="True" />
				    </cc1:OutDataList>   
                </td>
            </tr>
            
        </table>    
    </div>
    </form>
</body>
</html>
