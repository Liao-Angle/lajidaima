<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="SmpProgram_Form_SPAD004_PrintPage" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<style type="text/css">
@media print
{
  .print {display: none;}
}

</style>
     

<body style="background:#ffffff">
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
			<Input Type="Button" Value="列印單據" onClick="javascript:print();" class="print"> 
        <table>
	<table style="WIDTH: 660px; MARGIN-LEFT: 4px" border="0" cellSpacing="0" cellPadding="1" id="table1">
		<tr height="40" align="middle">
			<td class="SmpPrintPageTitle"><b>國外出差申請單</b></td>
		</tr>
	</table>
	<table style="table-layout:fixed;WIDTH: 700px; MARGIN-LEFT: 4px" class="BasicFormHeadBorder" border="0" cellSpacing="0" cellPadding="1" id="table2">
		<tr height="20px">
			<td class="SmpPrintPageHeadHead"><font size="2" face="Arial"><b>單號</b></font></td>
			<td class="SmpPrintPageHeadDetail" colSpan="4">
			  <b><asp:Label id="SheetNo" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead"><font size="2" face="Arial"><b>主旨</b></font></td>
			<td class="SmpPrintPageHeadDetail" colSpan="4">
				<b><asp:Label id="Subject" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead" colSpan="5"><b>基本資料<b></td>
		</tr>
		<tr height="20px">
			<td align="center" class="SmpPrintPageHeadHead">申請單位</td>
			<td align="center" class="SmpPrintPageHeadHead">出差人員</td>
			<td align="center" class="SmpPrintPageHeadHead">職稱</td>
			<td align="center" class="SmpPrintPageHeadHead">職務代理人</td>
			<td align="center" class="SmpPrintPageHeadHead">審核人</td>
		</tr>
		<tr align="middle" height="20px">
			<td class=SmpPrintPageHeadDetail width="150"><b>
				<asp:Label id="deptId" runat="server" visible="false"></asp:Label>
				<asp:Label id="deptName" runat="server" visible="false"></asp:Label>
				<asp:Label id="deptInfo" runat="server"></asp:Label></b>
            </td>
			<td class=SmpPrintPageHeadDetail width="150"><b>
				<asp:Label id="empNumber" runat="server" visible="false"></asp:Label>
				<asp:Label id="empName" runat="server" visible="false"></asp:Label>
				<asp:Label id="empEName" runat="server" visible="false"></asp:Label>
				<asp:Label id="empInfo" runat="server"></asp:Label></b>
            </td>
			<td class=SmpPrintPageHeadDetail width="150">
				<b><asp:Label id="funcName" runat="server"></asp:Label></b>
            </td>
			<td class=SmpPrintPageHeadDetail>
				<b><asp:Label id="agentName" runat="server"></asp:Label></b>
            </td>
			<td class=SmpPrintPageHeadDetail>
				<b><asp:Label id="checkBy" runat="server"></asp:Label></b>
            </td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">預計出差日期</td>
			<td class="SmpPrintPageHeadDetail" colSpan="4">
			  <b><asp:Label id="StartTrvlDate" runat="server"></asp:Label>	
			  <cc1:DSCLabel ID="DSCLabel01" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
			  <asp:Label id="EndTrvlDate" runat="server"></asp:Label>	
			  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			  <asp:Label id="lbTrvlDateSumValue" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">出差地點</td>
			<td class="SmpPrintPageHeadDetail" colSpan="4"><b>
			<asp:Label id="SiteUs" runat="server" visible="false"></asp:Label>
			<asp:Label id="SiteUsDesc" runat="server" visible="false"></asp:Label>
			<asp:Label id="SiteJp" runat="server" visible="false"></asp:Label>
			<asp:Label id="SiteJpDesc" runat="server" visible="false"></asp:Label>
			<asp:Label id="SiteKr" runat="server" visible="false"></asp:Label>
			<asp:Label id="SiteKrDesc" runat="server" visible="false"></asp:Label>
			<asp:Label id="SiteSub" runat="server" visible="false"></asp:Label>
			<asp:Label id="SiteSubDesc" runat="server" visible="false"></asp:Label>
			<asp:Label id="SiteOther" runat="server" visible="false"></asp:Label>
			<asp:Label id="SiteOtherDesc" runat="server" visible="false"></asp:Label>
			
			<asp:Label id="SiteInfo" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">費用負擔</td>
			<td class="SmpPrintPageHeadDetail" colSpan="4"><b>
			<asp:Label id="FeeCharge" runat="server" visible="false"></asp:Label>
			<asp:Label id="FeeChargeInfo" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead" colSpan="5">出差（預辦）事由：（說明：如費用由子公司負擔，需經子公司核准）</td>
		</tr>
		<tr>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			<b><asp:Label id="TrvlDesc" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead" colSpan="5"><b>B 出差旅行平安保險申請　（實際出差日數如超過預計，請主動通知加保）</b></td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">身份證字號／護照號碼</td>
			<td class="SmpPrintPageHeadDetail">
			  <b><asp:Label id="IdNumber" runat="server"></asp:Label></b>
			</td>
			<td class="SmpPrintPageHeadHead">出生年月日</td>
			<td class="SmpPrintPageHeadDetail" colSpan="2">
			  <b><asp:Label id="Birthday" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">身故受益人</td>
			<td class="SmpPrintPageHeadDetail">
			  <b><asp:Label id="Beneficiary" runat="server"></asp:Label></b>
			</td>
			<td class="SmpPrintPageHeadHead">與被保險人關係</td>
			<td class="SmpPrintPageHeadDetail" colSpan="2">
			  <b><asp:Label id="Relationship" runat="server"></asp:Label></b>
			</td>
		</tr>		
		<tr height="20px">
			<td class="SmpPrintPageHeadHead" colSpan="5"><b>C 預支申請</b></td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">新台幣</td>
			<td class="SmpPrintPageHeadDetail" colSpan="4">
			  <b><asp:Label id="PrePayTwd" runat="server" visible="false"></asp:Label>
			  <asp:Label id="PrePayTwdAmt" runat="server" visible="false"></asp:Label>
  			  <asp:Label id="PrePayCny" runat="server" visible="false"></asp:Label>
	  		  <asp:Label id="PrePayCnyAmt" runat="server" visible="false"></asp:Label>
			  <asp:Label id="PrePayUsd" runat="server" visible="false"></asp:Label>
			  <asp:Label id="PrePayUsdAmt" runat="server" visible="false"></asp:Label>
			  <asp:Label id="PrePayOther" runat="server" visible="false"></asp:Label>
			  <asp:Label id="PrePayOtherAmt" runat="server" visible="false"></asp:Label>
			  <asp:Label id="PrePayInfo" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead" colSpan="5">預支備註</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			  <b><asp:Label id="PrePayComment" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">實際領取日</td>
			<td class="SmpPrintPageHeadDetail">
			  <b><asp:Label id="ActualGetDate" runat="server"></asp:Label></b>
			</td>
			<td class="SmpPrintPageHeadHead">領取人</td>
			<td class="SmpPrintPageHeadDetail" colSpan="2">
			  <b><asp:Label id="getMember" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			<font color="#ff0000" size="2"><b>** 此張單據已包含請假單功能, 無需再產生EF發起請假單 **</b></font><br>
			<font color="#ff0000" size="2"><b>** 如未填列預支申請而已完成簽核者,財務部即不再受理預支申請 **</b></font> </td>
		</tr>
	<table style="table-layout:fixed;WIDTH: 700px; MARGIN-LEFT: 4px" class="BasicFormHeadBorder" border="0" cellSpacing="0" cellPadding="1" id="table2">		
		<tr Height="30px">
		  <td align="center" class="SmpPrintPageHeadHead">董事長</td>
		  <td align="center" class="SmpPrintPageHeadHead">審核人5</td>
		  <td align="center" class="SmpPrintPageHeadHead">審核人4</td>
		  <td align="center" class="SmpPrintPageHeadHead">審核人3</td>
		  <td align="center" class="SmpPrintPageHeadHead">審核人2</td>
		  <td align="center" class="SmpPrintPageHeadHead">審核人1</td>
		</tr>
		<tr>
		  <td class="SmpPrintPageHeadDetail" height="70">
			<asp:Image runat="server" id="Img6" width="130px"/><br>
			<asp:Label id="Img6Time" runat="server"></asp:Label>
		  </td>
		  <td class="SmpPrintPageHeadDetail">
			<asp:Image runat="server" id="Img5" width="112px" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img5Time" runat="server"></asp:Label>			
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img4" width="112px" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img4Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img3" width="112px" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img3Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img2" width="112px" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img2Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img1" width="112px" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img1Time" runat="server"></asp:Label> 
		  </td>
		</tr>
	</table>
</table>


        </table>
    </tr>
    </div>
	
    </form>
</body>
</html>
