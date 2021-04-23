<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="SmpProgram_Maintain_SPAD004_PrintPage" %>
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
	<table style="WIDTH: 740px; MARGIN-LEFT: 4px" border="0" cellSpacing="0" cellPadding="1" id="table1">
		<tr height="40" align="middle">
			<td class="SmpPrintPageTitle"><b>國內出差費用彙總表</b></td>
		</tr>
	</table>
	<table style="table-layout:fixed;WIDTH: 740px; MARGIN-LEFT: 4px" class="BasicFormHeadBorder" border="0" cellSpacing="0" cellPadding="1" id="table2">
		<tr height="25px">
			<td class="SmpPrintPageHeadHead" align="right"><b>單號</b></td>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			  <b><asp:Label id="SheetNo" runat="server"></asp:Label></b>
			</td>
		</tr>		
		<tr height="25px">
			<td class="SmpPrintPageHeadHead" align="right"><b>公司別</b></td>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
				<b><asp:Label id="CompanyCode" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="25px">
			<td class="SmpPrintPageHeadHead" align="right"><b>承辦人員</b></td>
			<td class="SmpPrintPageHeadDetail">
				<b><asp:Label id="empNumber" runat="server" visible="false"></asp:Label>
				</b><b><asp:Label id="empName" runat="server" visible="false"></asp:Label>
				</b><b><asp:Label id="empEName" runat="server" visible="false"></asp:Label>
				</b><b><asp:Label id="empInfo" runat="server"></asp:Label></b>
			</td>
			<td class="SmpPrintPageHeadHead" align="right"><b>請款部門</b></td>
			<td class="SmpPrintPageHeadDetail" colSpan="3">				
				<b><asp:Label id="deptInfo" runat="server"></asp:Label></b>
				<asp:Label id="deptId" runat="server" visible="false"></asp:Label>
				<asp:Label id="deptName" runat="server" visible="false"></asp:Label>
			</td>			
		</tr>
		<tr height="25px">
			<td class="SmpPrintPageHeadHead" align="right"><b>請款總額</b></td>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			  <b><asp:Label id="totalAmount" runat="server"></asp:Label></b>
			</td>
		</tr>		
		<tr height="25px">
			<td class="SmpPrintPageHeadHead" align="right"><b>其他說明</b></td>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			  <b><asp:Label id="otherDesc" runat="server"></asp:Label></b>
			</td>
		</tr>
		
		<tr>
		  <td class="SmpPrintPageHeadDetail" colSpan="6" width="700px" >
		    <asp:Label ID="Label1" runat="server" Text="國內出差差旅費報銷彙總明細清單" Font-Bold="True" Font-Size="Small"></asp:Label>
		    <asp:GridView ID="tripFeeSummary" runat="server" Font-Size="X-Small" Font-Bold="False" width="740px" >
		        <FooterStyle Font-Bold="False" Font-Italic="False" />
		        <HeaderStyle CssClass="BasicFormHeadHead" Font-Bold="False" Font-Size="X-Small" 
		            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Width="740px"
		            ForeColor="Black" />
		    </asp:GridView>
			<br>
	      </td>
		</tr>
		
		
	<table style="table-layout:fixed;WIDTH: 740px; MARGIN-LEFT: 4px" class="BasicFormHeadBorder" border="0" cellSpacing="0" cellPadding="1" id="table2">		
		<tr Height="30px">
		  <td align="center" class="SmpPrintPageHeadHead"><b>董事長</b></td>
		  <td align="center" class="SmpPrintPageHeadHead"><b>主管4</b></td>
		  <td align="center" class="SmpPrintPageHeadHead"><b>主管3</b></td>
		  <td align="center" class="SmpPrintPageHeadHead"><b>主管2</b></td>
		  <td align="center" class="SmpPrintPageHeadHead"><b>主管1</b></td>
		  <td align="center" class="SmpPrintPageHeadHead"><b>承辦人</b></td>
		</tr>
		<tr>
		  <td class="SmpPrintPageHeadDetail" height="40">
			<asp:Image runat="server" id="Img6" widht="150px" />
		  </td>
		  <td class="SmpPrintPageHeadDetail">
			<asp:Image runat="server" id="Img5" widht="150px" />
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img4" widht="150" />
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img3" widht="150" />
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img2" widht="150" />
		  </td>
		  <td class="SmpPrintPageHeadDetail" align="center">
		    <asp:Label id="signUser1" runat="server"></asp:Label>
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
