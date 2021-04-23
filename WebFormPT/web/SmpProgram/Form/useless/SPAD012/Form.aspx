<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPAD012_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title>廠務系統維修單</title>
    <style type="text/css">
    </style>
</head>

<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>  
        <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
        <cc1:SingleField ID="Subject" runat="server" Display="False" />          
		<cc1:SingleField ID="CheckByGUID" runat="server" Display="False" />
		<cc1:SingleField ID="OriginatorGUID" runat="server" Display="False" />
		
        <tr valign=middle > 
		    <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>廠務系統維修單</b></font></td>
	    </tr>
	</table>
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr height="30">
			<td class="BasicFormHeadHead" colspan="4">
				<font size="2" face="Arial"><b>申請人資料 </b>(請填寫實際申請人基本資料)</font>
			</td>
		</tr>
        <tr valign="middle">
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="CompanyLabel" runat="server" Text="公司" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="OriNumberLabel" runat="server" Text="工號" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="CNameLabel" runat="server" Text="申請人(中)"  IsNecessary="true"></cc1:DSCLabel>
            </td>
		    <td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="ENameLabel" runat="server" Text="申請人(英)" IsNecessary="true"></cc1:DSCLabel>
            </td>
		</tr>
        <tr valign="middle" align="center">
			<td class=BasicFormHeadDetail>
				<cc1:SingleDropDownList ID="Company" runat="server" Width="150px" Font-Strikeout="False" />
            </td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleField ID="OriginatorNumber" runat="server" Height="20px" 
                            width="150px" ontextchanged="OriginatorNumber_SingleFieldButtonClick" />
            </td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleField ID="OriginatorCName" runat="server" Height="20px" width="150px"/>
            </td>
			<td class=BasicFormHeadDetail>
                <cc1:SingleField ID="OriginatorEName" runat="server" Height="20px" width="150px" />
            </td>
		</tr>
		<tr valign="middle">
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DeptLabel" runat="server" Text="部門" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="TitelLabel" runat="server" Text="職稱" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="ExtensionLabel" runat="server" Text="申請人分機" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel01" runat="server" Text="  " ></cc1:DSCLabel>
			</td>
							        
		</tr>
		<tr valign="middle" align="center">
			<td class=BasicFormHeadDetail>
				<cc1:SingleOpenWindowField ID="OriginatorDeptGUID" runat="server" Width="150px" 
                  showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                  tableName="OrgUnit" />
			</td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleField ID="Title" runat="server" Height="20px" width="150px" />
            </td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleField ID="Extension" runat="server" Height="20px" width="150px" />
			</td>
			<td class=BasicFormHeadDetail>
			    <cc1:DSCLabel ID="DSCLabel03" runat="server" Text=" " ></cc1:DSCLabel>
			</td>							        
		</tr>
		
        <tr >
			<td class="BasicFormHeadHead" colSpan="4"><b><font size="2">
				<asp:Label ID="UserDescLabel" runat="server" Text="使用者需求說明" Width="120px" IsNecessary="true"></asp:Label></font></b>
			</td>
		</tr>
        <tr>
			<td class="BasicFormHeadDetail" colspan="4">
				<cc1:SingleField ID="UserDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
			</td>
		</tr>
        <tr>
			<td class="BasicFormHeadHead" colSpan="4"><font size="2">
				<b><asp:Label ID="FacDescLabel" runat="server" Text="廠務處理說明" Width="120px"></asp:Label></b></font>
                <br />
            </td>
		</tr>
        <tr>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:SingleField ID="FacDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
            </td>
		</tr>
		<tr valign="middle" align="center" >
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="ProcessingHoursLabel" runat="server" Text="處理時間" ></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleField ID="ProcessingHours" runat="server" width="120px" isMoney="True" Fractor="2" />
				<cc1:DSCLabel ID="DSCLabelProcessingHours" runat="server" Text="小時"  width="40px"  ></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="DSCLabel27" runat="server" Text="預計完成日" ></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleDateTimeField ID="EstimateCompleteDate" runat="server" Width="150px" />
            </td>
		</tr>
		<tr>
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="FacOwnerLabel" runat="server" Text="廠務承辦人" ></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadDetail" colSpan="3">
				<cc1:SingleDropDownList ID="FacOwnerGUID" runat="server" Width="150px" 
                     onselectchanged="FacUser_SelectChanged" />
            </td>
		</tr>


		
	</table>

    </div>       
</form>

</body>
</html>
