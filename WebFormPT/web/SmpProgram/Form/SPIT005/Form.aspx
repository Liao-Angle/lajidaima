<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPIT005_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title>嘉普資訊需求申請單</title>
    <style type="text/css">
    </style>
</head>

<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>  
        <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
        <cc1:SingleField ID="Subject" runat="server" Display="False" />   
		<cc1:SingleField ID="BenefitDesc" runat="server" Display="False" 
            Visible="False" />   		

        <tr valign=middle > 
		    <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>嘉普資訊需求申請單</b></font></td>
	    </tr>
	</table>
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr height="30">
			<td class="BasicFormHeadHead" colspan="4">
				<font size="2" face="Arial"><b>申請人資料 </b>(請填寫實際申請人基本資料)</font>
			</td>
		</tr>
        <tr height="30">
			<td class="BasicFormHeadHead" colspan="4">
				<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
				<tr valign="middle">
					<td class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel1" runat="server" Text="公司" IsNecessary="true" Width="300" TextAlign="1"></cc1:DSCLabel>
					</td>
					<td class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel3" runat="server" Text="工號" IsNecessary="true" Width="80px" TextAlign="1"></cc1:DSCLabel>
					</td>
					<td class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel8" runat="server" Text="申請人(中)"  IsNecessary="true" Width="160" TextAlign="1"></cc1:DSCLabel>
					</td>
					<td class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel9" runat="server" Text="申請人(英)" IsNecessary="true" Width="105px" TextAlign="1"></cc1:DSCLabel>
					</td>
				</tr>

				<tr valign="middle" align="center">
					<td class=BasicFormHeadDetail width="300" >
						<cc1:SingleDropDownList ID="Company" runat="server" Width="300px" Font-Strikeout="False"  />
					</td>
					<td class=BasicFormHeadDetail width="80px">
						<cc1:SingleField ID="OriginatorNumber" runat="server" Height="20px" 
									width="80px" ontextchanged="OriginatorNumber_SingleFieldButtonClick" />
					</td>
					<td class=BasicFormHeadDetail width="160px">
						<cc1:SingleField ID="OriginatorCName" runat="server" Height="20px" width="160px"/>
					</td>
					<td class=BasicFormHeadDetail width="105px">
						<cc1:SingleField ID="OriginatorEName" runat="server" Height="20px" width="105px" />
					</td>
				</tr>
				<tr valign="middle">
					<td class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel2" runat="server" Text="部門" IsNecessary="true" TextAlign="1"></cc1:DSCLabel>
					</td>
					<td class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel10" runat="server" Text="職稱" IsNecessary="true" TextAlign="1"></cc1:DSCLabel>
					</td>
					
					<td class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel121" runat="server" Text="申請人分機" IsNecessary="true" TextAlign="1"></cc1:DSCLabel>
					</td>	
                    <td class="BasicFormHeadHead">
						<cc1:DSCLabel ID="DSCLabel122" runat="server" Text="嘉普單號" IsNecessary="true" TextAlign="1"></cc1:DSCLabel>
					</td>				        
				</tr>
				<tr valign="middle">
					<td class=BasicFormHeadDetail width="300px">
						<cc1:SingleOpenWindowField ID="OriginatorDeptGUID" runat="server" Width="300px" 
						  showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" FixReadOnlyValueTextWidth="190px" FixValueTextWidth="70px"
						  tableName="OrgUnit" />
					</td>
					<td class=BasicFormHeadDetail  width="80px">
						<cc1:SingleField ID="Title" runat="server" Height="20px" width="80px" />
					</td>

					<td class=BasicFormHeadDetail width="105px">
						<cc1:SingleField ID="Extension" runat="server" Height="20px" width="160px" />
					</td>
                    <td class=BasicFormHeadDetail width="105px">
						<cc1:SingleField ID="TESheetNo" runat="server" Height="20px" width="160px" />
					</td>						        
				</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="資訊需求內容清單" Width="660px" DialogHeight="460" IsNecessary="true">
				&nbsp;&nbsp;
				<asp:Label ID="LBRequestType" runat="server" Text="需求類別" Width="60px"></asp:Label>
				<cc1:SingleDropDownList ID="RequestType" runat="server" Width="210px" 
                     onselectchanged="RequestType_SelectChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LBRequestItem" runat="server" Text="需求項目" Width="60px"></asp:Label>
                <cc1:SingleDropDownList ID="RequestItem" runat="server" Width="210px" 
                     onselectchanged="RequestItem_SelectChanged"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
                &nbsp;&nbsp;&nbsp;&nbsp;<font color="red"><cc1:DSCLabel ID="lbRequestTips" runat="server" Text="" Width="550px"></cc1:DSCLabel></font><br>
            	&nbsp;&nbsp;&nbsp;<asp:Label ID="LBRequestDesc" runat="server" Text="需求說明" Width="60px"></asp:Label>
                <cc1:SingleField ID="RequestDesc" runat="server" align="right" style="margin-right: 0px" Width="400px" />
                <br />
                <cc1:OutDataList ID="RequestList" runat="server" Height="120px" 
                     OnSaveRowData="RequestList_SaveRowData" OnShowRowData="RequestList_ShowRowData" 
                     ViewStateMode="Disabled" Width="650px" />
                </cc1:DSCGroupBox>              
                <br />
                <cc1:GlassButton ID="SignFlowTE" runat="server" Height="40px" Width="120" 
                    onclick="SignFlow_OnClick"  />            
			</td>
		</tr>
		<tr>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:DSCGroupBox ID="DSCGroupBenefit" runat="server" Text="預期效益" Width="660px" DialogHeight="260" IsNecessary="true">
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<font size=2 color="ff00ff"><cc1:DSCLabel ID="LBBenefitDesc" runat="server" Text="需求類別為 [3.應用系統-程式報表修改] 或 [4.應用系統-系統設定與維護] 必須填寫效益!" Width="600px"></cc1:DSCLabel></font>
				<cc1:DSCLabel ID="LBExpBenefit" runat="server" Text="預期效益" IsNecessary="true" Width="120px" TextAlign="2"></cc1:DSCLabel>&nbsp;&nbsp;
				<cc1:SingleField ID="ExpBenefitDesc" runat="server" align="right" style="margin-right: 0px" Width="400px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
                
				<cc1:DSCLabel ID="LBExpBenefitSaving" runat="server" Text="預期節省" IsNecessary="true" Width="120px" TextAlign="2"></cc1:DSCLabel>
				&nbsp;&nbsp;
				<cc1:SingleDropDownList ID="BenefitType" runat="server" Width="60px" 
                    onselectchanged="BenefitType_SelectChanged" />
                <cc1:SingleField ID="ExpBenefitSaving" runat="server" align="right" style="margin-right: 0px" Width="100px" />
				
				&nbsp;&nbsp;
				<cc1:DSCLabel ID="LBBenefitHours" runat="server" Text="小時(每月)" Width="200px"></cc1:DSCLabel>
				<br>
				
				
				</cc1:DSCGroupBox>
			</td>
		</tr>
        <tr >
			<td class="BasicFormHeadHead" colSpan="4"><b><font size="2">
				<asp:Label ID="Label1" runat="server" Text="使用者需求說明" Width="120px" IsNecessary="true"></asp:Label></font></b>
			</td>
		</tr>
        <tr>
			<td class="BasicFormHeadDetail" colspan="4">
				<cc1:SingleField ID="UserDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
			</td>
		</tr>
        <tr>
			<td class="BasicFormHeadHead" colSpan="4"><font size="2">
				<b><asp:Label ID="Label2" runat="server" Text="MIS處理說明" Width="120px"></asp:Label></b></font>
                <br />
            </td>
		</tr>
        <tr>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:SingleField ID="MisDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
            </td>
		</tr>
        <tr valign="middle" align="center" >
			<!--<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel22" runat="server" Text="會簽人員1" TextAlign="1"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel23" runat="server" Text="會簽人員2" TextAlign="1"></cc1:DSCLabel>
            </td>-->
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="DSCLabel25" runat="server" Text="MIS承辦人" TextAlign="1"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="DSCLabel27" runat="server" Text="預計完成日" TextAlign="1"></cc1:DSCLabel>
            </td>
		</tr>
		<tr valign="middle" align="center">
			<!--<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="Countersign1GUID" runat="server" Width="150px"
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                     tableName="Users" idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="65px" FixValueTextWidth="55px"
                    onbeforeclickbutton="Countersign1GUID_BeforeClickButton" />
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="Countersign2GUID" 
                     runat="server" Width="150px" FixReadOnlyValueTextWidth="65px" FixValueTextWidth="55"
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                     tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="Countersign2GUID_BeforeClickButton" /> 
			</td>-->
			<td class="BasicFormHeadDetail">
				<cc1:SingleDropDownList ID="MisOwnerGUID" runat="server" Width="150px" 
                     onselectchanged="misUser_SelectChanged" />	
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleDateTimeField ID="EstimateCompleteDate" runat="server" Width="150px" />
			</td>
		</tr>
	</table>

    </div>       
</form>

</body>
</html>
