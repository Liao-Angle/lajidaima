<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="Program_SCQ_Form_EH010301_PrintPage" %>
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
			<td class="SmpPrintPageTitle"><b>請 假 單</b></td>
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
			<td align="center" class="SmpPrintPageHeadHead">公司別</td>
			<td align="center" class="SmpPrintPageHeadHead" colSpan="2">申請單位</td>
			<td align="center" class="SmpPrintPageHeadHead">出差人員</td>
			<td align="center" class="SmpPrintPageHeadHead">審核人</td>
		</tr>
		<tr align="middle" height="20px">
			<td class=SmpPrintPageHeadDetail>
				<b><asp:Label id="OrgName" runat="server"></asp:Label></b>
            </td>
			<td align="center" class=SmpPrintPageHeadDetail colSpan="2"><b>
				<asp:Label id="deptId" runat="server" visible="false"></asp:Label>
				<asp:Label id="deptName" runat="server" visible="false"></asp:Label>
				<asp:Label id="deptInfo" runat="server" ></asp:Label></b>
            </td>
			<td class=SmpPrintPageHeadDetail ><b>
				<asp:Label id="empNumber" runat="server" visible="false"></asp:Label>
				<asp:Label id="empName" runat="server" visible="false"></asp:Label>
				<asp:Label id="empEName" runat="server" visible="false"></asp:Label>
				<asp:Label id="empInfo" runat="server"></asp:Label></b>
            </td>			
			<td class=SmpPrintPageHeadDetail>
				<b><asp:Label id="checkBy" runat="server"></asp:Label></b>
            </td>
		</tr>
		<tr height="20px">
			<td align="center" class="SmpPrintPageHeadHead">出差日期</td>
			<td align="center" class="SmpPrintPageHeadHead">出差時間起</td>
			<td align="left" class="SmpPrintPageHeadHead">出差時間訖</td>
			<td align="left" class="SmpPrintPageHeadHead" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;</td>
		</tr>
		<tr align="middle" height="20px">
			<td class=SmpPrintPageHeadDetail width="150"><b>
				<asp:Label id="TripDate" runat="server"></asp:Label></b>
            </td>
			<td class=SmpPrintPageHeadDetail width="150">
				<asp:Label id="StartTime" runat="server"></asp:Label>
			</td>
			<td class=SmpPrintPageHeadDetail width="150">
				<asp:Label id="EndTime" runat="server"></asp:Label></b>	
            </td>			
			<td class=SmpPrintPageHeadDetail colSpan="2">
				&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">是否申請出差費用</td>
			<td class="SmpPrintPageHeadDetail" colSpan="4">
			  <asp:Label id="IsTripFee" runat="server"></asp:Label>				  
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">出差地點</td>
			<td class="SmpPrintPageHeadDetail" colSpan="4">
			<asp:Label id="TripSite" runat="server"></asp:Label>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">擬辦事項</td>
			<td class="SmpPrintPageHeadDetail" colSpan="4">
			<asp:Label id="Notes" runat="server"></asp:Label>
			</td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">車資</td>
			<td class="SmpPrintPageHeadHead">停車資</td>
			<td class="SmpPrintPageHeadHead">繕雜資</td>
			<td class="SmpPrintPageHeadHead">ETC費用</td>
			<td class="SmpPrintPageHeadHead">其他費用</td>
		</tr>
		<tr>
			<td class="SmpPrintPageHeadDetail"><asp:Label id="TrafficFee" runat="server"></asp:Label></td>
			<td class="SmpPrintPageHeadDetail"><asp:Label id="ParkingFee" runat="server"></asp:Label></td>
			<td class="SmpPrintPageHeadDetail"><asp:Label id="EatFee" runat="server"></asp:Label></td>
			<td class="SmpPrintPageHeadDetail"><asp:Label id="EtagFee" runat="server"></asp:Label></td>
			<td class="SmpPrintPageHeadDetail"><asp:Label id="OtherFee" runat="server"></asp:Label></td>			
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">去公里數</td>
			<td class="SmpPrintPageHeadHead">回公里數</td>
			<td class="SmpPrintPageHeadHead">里程數</td>
			<td class="SmpPrintPageHeadHead">油資</td>
			<td class="SmpPrintPageHeadHead">&nbsp;</td>
		</tr>
		<tr>
			<td class="SmpPrintPageHeadDetail"><asp:Label id="StartMileage" runat="server"></asp:Label></td>
			<td class="SmpPrintPageHeadDetail"><asp:Label id="EndMileage" runat="server"></asp:Label></td>
			<td class="SmpPrintPageHeadDetail"><asp:Label id="MileageSum" runat="server"></asp:Label></td>
			<td class="SmpPrintPageHeadDetail"><asp:Label id="PayeeFee" runat="server"></asp:Label></td>
			<td class="SmpPrintPageHeadDetail">&nbsp;&nbsp;&nbsp;&nbsp;</td>			
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
				<asp:Image runat="server" id="Img6" widht="150px"/><br>
				<asp:Label id="Img6Time" runat="server"></asp:Label>
			  </td>
			  <td class="SmpPrintPageHeadDetail">
				<asp:Image runat="server" id="Img5" widht="150px" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
				<asp:Label id="Img5Time" runat="server"></asp:Label>			
			  </td>
			  <td class="SmpPrintPageHeadDetail">
			    <asp:Image runat="server" id="Img4" widht="150" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
				<asp:Label id="Img4Time" runat="server"></asp:Label> 
			  </td>
			  <td class="SmpPrintPageHeadDetail">
			    <asp:Image runat="server" id="Img3" widht="150" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
				<asp:Label id="Img3Time" runat="server"></asp:Label> 
			  </td>
			  <td class="SmpPrintPageHeadDetail">
			    <asp:Image runat="server" id="Img2" widht="150" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
				<asp:Label id="Img2Time" runat="server"></asp:Label> 
			  </td>
			  <td class="SmpPrintPageHeadDetail">
			    <asp:Image runat="server" id="Img1" widht="150" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
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
