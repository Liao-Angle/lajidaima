<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPAD004_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title>國外出差申請單</title>
    <style type="text/css">
    </style>
</head>

<body>

<form id="form1" runat="server">
<div>
	<cc1:SingleField ID="SheetNo" runat="server" Display="False" />
	<cc1:SingleField ID="IsIncludeDateEve" runat="server" />
	<cc1:SingleField ID="TempSerialNo" runat="server" />
	<cc1:SingleField ID="ClassCode" runat="server" />
	<cc1:SingleField ID="CategoryCode" runat="server" />
	<cc1:SingleField ID="IsIncludeHoliday" runat="server" />
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>       
        <tr align="center"> 
			<td><font style="font-family: 標楷體; font-size: large;"><b>國外出差申請單</b></font></td>
	    </tr>
	
	</table>

	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr>
			<td colSpan="5" class="BasicFormHeadHead" Width="100%"><b>				
				<cc1:GlassButton ID="PrintButton" runat="server" Height="20px" Width="100%" 
                    Text="列印國外出差單" onbeforeclicks="PrintButton_OnClick" Enabled="True" /></b>
			</td>
		</tr>	
		<tr>
			<td class="BasicFormHeadHead">
				<font size="2" face="Arial"><b>主旨</b></font>
			</td>
            <td class="BasicFormHeadDetail" colSpan="4">
				<cc1:SingleField ID="Subject" runat="server" Height="20px" width="500px" />
			</td>
		</tr>
	    <tr height="30">
			<td class="BasicFormHeadHead" colSpan="5">
				<font size="2" face="Arial"><b>基本資料</b></font>
			</td>
		</tr>
		<tr height="30">
			<td align="left" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="LblCompanyCode" runat="server" Text="公司別" Width="85px" />
	        </td>
			<td colspan=4 class=BasicFormHeadDetail>
				<cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="138px"  />
	        </td>
		</tr>
		<tr height="30" align="center">
			<td class="BasicFormHeadHead" width=180" >
				<cc1:DSCLabel ID="lbDeptID" runat="server" Text="申請單位" Width="60" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" width=180" >
				<cc1:DSCLabel ID="lbOriginator" runat="server" Text="出差人員" Width="60px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" width="95">
				<cc1:DSCLabel ID="lbTitle" runat="server" Text="職稱" Width="35px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" width="172">
				<cc1:DSCLabel ID="lbAgent" runat="server" Text="職務代理人" Width="75px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" width="170">
				<cc1:DSCLabel ID="lbCheckby" runat="server" Text="審核人" Width="45px"></cc1:DSCLabel>
            </td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="150px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="OrgUnit" />
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="150px" 
	                 showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3" 
	                 tableName="Users" 
                    onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton" IgnoreCase="True"/>
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleField ID="Title" runat="server" Height="20px" width="80px" />
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="AgentGUID" runat="server" Width="140px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                     tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="AgentGUID_BeforeClickButton" IgnoreCase="True"	/>
			</td>
            <td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="CheckByGUID" runat="server" Width="140px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                     tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="CheckByGUID_BeforeClickButton" IgnoreCase="True" />
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadHead" align="left">
		        <cc1:DSCLabel ID="lbTrvlDate" runat="server" Text="預計出差日期" Width="100px" IsNecessary="true"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4" >
				<cc1:SingleDateTimeField ID="StartTrvlDate" runat="server" Width="120px" />&nbsp;&nbsp;
                <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
				<cc1:SingleDateTimeField ID="EndTrvlDate" runat="server" Width="120px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <cc1:DSCLabel ID="lbTrvlDateSumValue" runat="server" Text="" Width="150px"></cc1:DSCLabel>
            </td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadHead">
		        <cc1:DSCLabel ID="lbTrvlSite" runat="server" Text="出差地點" Width="70px" IsNecessary="true"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:DSCCheckBox ID="SiteUs" runat="server" Height="20px" Width="50px" Text="美國"/>
				<cc1:SingleField ID="SiteUsDesc" runat="server" Height="20px" Width="70px" />&nbsp;&nbsp;&nbsp;
				<cc1:DSCCheckBox ID="SiteJp" runat="server" Height="20px" Width="50px" Text="日本"/>
				<cc1:SingleField ID="SiteJpDesc" runat="server" Height="20px" Width="70px" />&nbsp;&nbsp;&nbsp;
				<cc1:DSCCheckBox ID="SiteKr" runat="server" Height="20px" Width="50px" Text="韓國"/>
				<cc1:SingleField ID="SiteKrDesc" runat="server" Height="20px" Width="70px" />&nbsp;&nbsp;&nbsp;<br>
				<cc1:DSCCheckBox ID="SiteSub" runat="server" Height="20px" Width="65px" Text="子公司"/>
				<cc1:SingleField ID="SiteSubDesc" runat="server" Height="20px" Width="70px" />&nbsp;&nbsp;&nbsp;
				<cc1:DSCCheckBox ID="SiteOther" runat="server" Height="20px" Width="50px" Text="其他"/>
				<cc1:SingleField ID="SiteOtherDesc" runat="server" Height="20px" Width="70px" />
		    </td>
	    </tr>						
	    <tr height="30px">				
			<td width="70px"  align="right" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbFeeCharge" runat="server" Text="費用負擔" Width="100px" IsNecessary="true"/></cc1:DSCLabel>
	        </td>        
	        <td colspan=5 width="500px" align="left" class="BasicFormHeadDetail">
				<cc1:DSCRadioButton ID="FeeCharge1" runat="server" Text="新普" Checked="true" />&nbsp;
	            <cc1:DSCRadioButton ID="FeeCharge2" runat="server" Text="新世" />&nbsp;
	            <cc1:DSCRadioButton ID="FeeCharge3" runat="server" Text="新普(重慶)" />&nbsp;
				<cc1:DSCRadioButton ID="FeeCharge5" runat="server" Text="中普" />
				<cc1:DSCRadioButton ID="FeeCharge6" runat="server" Text="太普" />
	        </td>				
	    </tr>
	    <tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5" align="left">
				<cc1:DSCLabel ID="DSCLabel14" runat="server" Text="出差（預辦）事由：（說明：如費用由子公司負擔，需經子公司核准）" IsNecessary="true"></cc1:DSCLabel>
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadDetail" colSpan="5">
                <cc1:SingleField ID="TrvlDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
		    </td>
	    </tr>
	    
        <tr  height="30">
		    <td class="BasicFormHeadHead" colspan="5"><b>
                <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="預支申請" Width="100px"></cc1:DSCLabel></b>
            </td>
	    </tr>
	    <tr height="30">
            <td class="BasicFormHeadDetail" colSpan="5" align="left">
				<cc1:DSCCheckBox ID="PrePayTwd" runat="server" Height="20px" Width="60px" Text="新台幣"/>
				<cc1:SingleField ID="PrePayTwdAmt" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="PrePayCny" runat="server" Height="20px" Width="60px" Text="人民幣"/>
				<cc1:SingleField ID="PrePayCnyAmt" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="PrePayUsd" runat="server" Height="20px" Width="50px" Text="美金"/>
				<cc1:SingleField ID="PrePayUsdAmt" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="PrePayJpy" runat="server" Height="20px" Width="50px" Text="日圓"/>
				<cc1:SingleField ID="PrePayJpyAmt" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="PrePayEur" runat="server" Height="20px" Width="50px" Text="歐元"/>
				<cc1:SingleField ID="PrePayEurAmt" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="PrePayOther" runat="server" Height="20px" Width="50px" Text="其他"/>
				<cc1:SingleField ID="PrePayOtherAmt" runat="server" Height="20px" Width="50px" />
		    </td>
        </tr>
		<tr height="30">
			<td class="BasicFormHeadHead" colspan="5">
				<cc1:DSCLabel ID="DSCLabel13" runat="server" Text="預支備註" Width="80px"></cc1:DSCLabel>
   		    </td>
		</tr>
		<tr height="30">
		    <td class="BasicFormHeadDetail" colSpan="5">
				<cc1:SingleField ID="PrePayComment" runat="server" Height="30px" Width="600px" MultiLine="true" />                    
            </td>
	    </tr>
	    <tr height="30">
		    <td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="DSCLabel11" runat="server" Text="實際領取日" Width="80px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail">
                <cc1:SingleDateTimeField ID="ActualGetDate" runat="server" DateTimeMode="0" Height="20px" Width="120px" />
			</td>
			<td class="BasicFormHeadDetail">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="DSCLabel16" runat="server" Text="領取人" Width="80px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail">
                <cc1:SingleOpenWindowField ID="GetMemberGUID" runat="server" Width="140px" 
                        showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                        tableName="Users" idIndex="2" valueIndex="3" IgnoreCase="True"
                    onbeforeclickbutton="GetMemberGUID_BeforeClickButton" />                    
            </td>
	    </tr>	
		<tr height="30">
		    <td class="BasicFormHeadDetail" colSpan="5">
				<font size="2" color="#ff0000"><b>** 此張單據已包含請假單功能, 無需再產生EF發起請假單 **</b></font></br>
                <font size="2" color="#ff0000"><b>** 如未填列預支申請而已完成簽核者,財務部即不再受理預支申請 **</b></font>
            </td>
	    </tr>		
    </table>
	</div>
</form>

</body>
</html>
