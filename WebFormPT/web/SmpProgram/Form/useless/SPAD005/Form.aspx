<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPAD005_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>國外出差異動申請單</title>
    <style type="text/css">
    </style>
</head>

<body>
<table style="margin-left:4px; width: 720px;" border=0 cellspacing=0 cellpadding=1><tr><td>
<form id="form1" runat="server">

	<table style="margin-left:4px; width: 720px;" border=0 cellspacing=0 cellpadding=1>
	    <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
	    <cc1:SingleField ID="Subject" runat="server" Display="False" /> 
	    <cc1:SingleField ID="IsIncludeDateEve" runat="server" />
	    <cc1:SingleField ID="TempSerialNo" runat="server" />
	    <cc1:SingleField ID="ClassCode" runat="server" />
	    <cc1:SingleField ID="CategoryCode" runat="server" />
	    <cc1:SingleField ID="IsIncludeHoliday" runat="server" />
        <tr align="center" height="40"> 
			<td><font style="font-family: 標楷體; font-size: large;"><b>國外出差異動申請單</b></font></td>
	    </tr>
	</table>

	<table style="margin-left:4px; width: 720px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder> 
		<tr>
			<td colSpan="5" class="BasicFormHeadHead"><b>				
				<cc1:GlassButton ID="PrintButton1" runat="server" Height="20px" Width="100%" 
                    Text="列印國外出差異動申請單" onbeforeclicks="PrintButton1_OnClick" Enabled="True" /></b>
			</td>
		</tr>		
		<tr height="30" width="120">				
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbChangType" runat="server" Text="異動類別" IsNecessary="true"/></cc1:DSCLabel>
	        </td>        
	        <td class="BasicFormHeadDetail" colSpan="4">
	                <cc1:DSCRadioButton ID="ChangeType1" runat="server" Text="出差日異動" Checked="true" />&nbsp;
	                <cc1:DSCRadioButton ID="ChangeType2" runat="server" Text="取消出差" />&nbsp;&nbsp;&nbsp;&nbsp;
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
			<td class="BasicFormHeadHead" >
				<cc1:DSCLabel ID="lbDept" runat="server" Text="申請單位" width="60px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbOriginator" runat="server" Text="出差人員" width="60px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbTitle" runat="server" Text="職稱" width="35px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbAgent" runat="server" Text="職務代理人" width="75px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbCheckBy" runat="server" Text="審核人" width="45px"></cc1:DSCLabel>
            </td>
		</tr>
		<tr height="30" align="center"> 
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="150px" 
				     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="OrgUnit" IgnoreCase="true"/>
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="150px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                     idIndex="2" valueIndex="3" 
                     tableName="Users" 
                    onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton" IgnoreCase="true" />
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleField ID="Title" runat="server" Height="20px" width="80px" />
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="AgentGUID" runat="server" Width="140px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                     tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="AgentGUID_BeforeClickButton" IgnoreCase="true" />
			</td>
            <td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="CheckByGUID" runat="server" Width="140px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3"  
                     tableName="Users" onbeforeclickbutton="CheckByGUID_BeforeClickButton" IgnoreCase="true" />
			</td>
		</tr>
		<tr height="30">
			<td align="left" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbOriForeignForm" runat="server" Text="勾稽原出差單據" Width="100px" IsNecessary="true"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4" >
				<cc1:SingleOpenWindowField ID="OriForeignForm" runat="server" Width="500px" 
				     showReadOnlyField="True" guidField="OID" keyField="UserId" serialNum="001" 
                     tableName="SPAD005" 
                    onsingleopenwindowbuttonclick="OriForeignForm_SingleOpenWindowButtonClick" 
                    IgnoreCase="true" onbeforeclickbutton="OriForeignForm_BeforeClickButton" />				
			</td>
		</tr>
		<tr height="30">
			<td align="left" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbTrvlDate" runat="server" Text="預計出差日期" Width="100px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4" >
				<cc1:SingleDateTimeField ID="StartTrvlDate" runat="server" Width="140px" />&nbsp;&nbsp;
                <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
				<cc1:SingleDateTimeField ID="EndTrvlDate" runat="server" Width="140px" />				
			</td>
		</tr>
		<tr height="30">
		    <td align="left" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbChgTrvlDate" runat="server" Text="異動後出差日期" Width="100px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4"> 	
				<cc1:SingleDateTimeField ID="ChgStartTrvlDate" runat="server" Width="140px" />&nbsp;&nbsp;
                <cc1:DSCLabel ID="DSCLabel018" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
				<cc1:SingleDateTimeField ID="ChgEndTrvlDate" runat="server" Width="140px" />
				<cc1:DSCLabel ID="lbHrTrvlDesc" runat="server" Text="產生假單日期：" Width="210px" 
                    TextAlign="2" ></cc1:DSCLabel>
                <cc1:SingleDateTimeField ID="HrStartTrvlDate" runat="server" Width="120px" />&nbsp;&nbsp;
                <cc1:DSCLabel ID="lbHrTrvlDate" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
				<cc1:SingleDateTimeField ID="HrEndTrvlDate" runat="server" Width="120px" />
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadHead">
		        <cc1:DSCLabel ID="lbTrvlSite" runat="server" Text="出差地點" Width="70px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:DSCCheckBox ID="SiteUs" runat="server" Height="20px" Width="50px" Text="美國"/>
				<cc1:SingleField ID="SiteUsDesc" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="SiteJp" runat="server" Height="20px" Width="50px" Text="日本"/>
				<cc1:SingleField ID="SiteJpDesc" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="SiteKr" runat="server" Height="20px" Width="50px" Text="韓國"/>
				<cc1:SingleField ID="SiteKrDesc" runat="server" Height="20px" Width="50px" />&nbsp;<br>
				<cc1:DSCCheckBox ID="SiteSub" runat="server" Height="20px" Width="65px" Text="子公司"/>
				<cc1:SingleField ID="SiteSubDesc" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="SiteOther" runat="server" Height="20px" Width="50px" Text="其他"/>
				<cc1:SingleField ID="SiteOtherDesc" runat="server" Height="20px" Width="50px" />
			</td>
		</tr>						
	    <tr height="30">				
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbFeeCharge1" runat="server" Text="費用負擔" Width="100px" />
	        </td>        
	        <td class="BasicFormHeadDetail" colSpan="4">
	            <cc1:DSCRadioButton ID="FeeCharge1" runat="server" Text="新普" Checked="true" />&nbsp;
	            <cc1:DSCRadioButton ID="FeeCharge2" runat="server" Text="新世" />&nbsp;
	            <cc1:DSCRadioButton ID="FeeCharge3" runat="server" Text="新普(重慶)" />&nbsp;				
				<cc1:DSCRadioButton ID="FeeCharge5" runat="server" Text="中普" />&nbsp;
				<cc1:DSCRadioButton ID="FeeCharge6" runat="server" Text="太普" />
	        </td>				
	        </tr>
	    <tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5">
		        <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="出差（預辦）事由：（說明：如費用由子公司負擔，需經子公司核准）" Width="500px"></cc1:DSCLabel>
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadDetail" colSpan="5">
                <cc1:SingleField ID="TrvlDesc" runat="server" Height="92px" Width="710px" MultiLine="True" />
		    </td>
	    </tr>
        <tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5">
				<cc1:DSCLabel ID="lbCancelDesc" runat="server" Text="異動或取消說明" Width="500px" IsNecessary="true"></cc1:DSCLabel>
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadDetail" colSpan="5">
                <cc1:SingleField ID="ChgTrvlDesc" runat="server" Height="92px" Width="710px" MultiLine="True" />
		    </td>
	    </tr>
	    
        <tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5">
				<b><cc1:DSCLabel ID="DSCLabel10" runat="server" Text="預支申請" Width="100px"></cc1:DSCLabel></b>
            </td>
	    </tr>
		<tr height="30" >
            <td  class="BasicFormHeadHead" colSpan="5">
			  <table style="margin-left:4px; width: 720px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
				<tr height="30">
					<td  class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel22" runat="server" Text="原預支申請" Width="100px"></cc1:DSCLabel>
					</td>
					<td class="BasicFormHeadDetail" colSpan="4">
						<cc1:DSCCheckBox ID="PrePayTwd" runat="server" Height="20px" Width="60px" Text="新台幣"/>
						<cc1:SingleField ID="PrePayTwdAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;
						<cc1:DSCCheckBox ID="PrePayCny" runat="server" Height="20px" Width="60px" Text="人民幣"/>
						<cc1:SingleField ID="PrePayCnyAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;
						<cc1:DSCCheckBox ID="PrePayUsd" runat="server" Height="20px" Width="50px" Text="美金"/>
						<cc1:SingleField ID="PrePayUsdAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;
						<cc1:DSCCheckBox ID="PrePayJpy" runat="server" Height="20px" Width="50px" Text="日圓"/>
						<cc1:SingleField ID="PrePayJpyAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;
						<cc1:DSCCheckBox ID="PrePayEur" runat="server" Height="20px" Width="50px" Text="歐元"/>
						<cc1:SingleField ID="PrePayEurAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;<br>
						<cc1:DSCCheckBox ID="PrePayOther" runat="server" Height="20px" Width="60px" Text="其他"/>
						<cc1:SingleField ID="PrePayOtherAmt" runat="server" Height="20px" Width="150px" />
					</td>
				</tr>
				<tr height="30">
					<td class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel23" runat="server" Text="異動後預支申請" Width="100px"></cc1:DSCLabel>
					</td>
					<td class="BasicFormHeadDetail" colSpan="4">
						<cc1:DSCCheckBox ID="ChgPrePayTwd" runat="server" Height="20px" Width="60px" Text="新台幣"/>
						<cc1:SingleField ID="ChgPrePayTwdAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;
						<cc1:DSCCheckBox ID="ChgPrePayCny" runat="server" Height="20px" Width="60px" Text="人民幣"/>
						<cc1:SingleField ID="ChgPrePayCnyAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;
						<cc1:DSCCheckBox ID="ChgPrePayUsd" runat="server" Height="20px" Width="50px" Text="美金"/>
						<cc1:SingleField ID="ChgPrePayUsdAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;
						<cc1:DSCCheckBox ID="ChgPrePayJpy" runat="server" Height="20px" Width="50px" Text="日圓"/>
						<cc1:SingleField ID="ChgPrePayJpyAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;
						<cc1:DSCCheckBox ID="ChgPrePayEur" runat="server" Height="20px" Width="50px" Text="歐元"/>
						<cc1:SingleField ID="ChgPrePayEurAmt" runat="server" Height="20px" Width="50px" />&nbsp;&nbsp;<br>
						<cc1:DSCCheckBox ID="ChgPrePayOther" runat="server" Height="20px" Width="60px" Text="其他"/>
						<cc1:SingleField ID="ChgPrePayOtherAmt" runat="server" Height="20px" Width="150px" />
					</td>
				</tr>
		      </table>
	        </td>
		</tr>
        
		<tr height="30">
		    <td class="BasicFormHeadHead">&nbsp;&nbsp;&nbsp;
                <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="預支備註" Width="100px"></cc1:DSCLabel>
   		    </td>
		    <td class="BasicFormHeadDetail" colSpan="4">
                <cc1:SingleField ID="PrePayComment" runat="server" Height="30px" Width="450px" MultiLine="true" />                    
            </td>
	    </tr>
	    <tr height="30">
		    <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="實際領取日" Width="80px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail">
                <cc1:SingleDateTimeField ID="ActualGetDate" runat="server" DateTimeMode="0" Height="20px" Width="120px" />
			</td>
			<td class="BasicFormHeadHead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="領取人" Width="80px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail">
                <cc1:SingleOpenWindowField ID="GetMemberGUID" runat="server" Width="140px" 
                        showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3" 
                        tableName="Users" 
                    onbeforeclickbutton="GetMemberGUID_BeforeClickButton" IgnoreCase="true" />                    
			</td>
	    </tr>			
    </table>

</form>
</td></tr></table>
</body>
</html>
