<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="SmpProgram_Form_SPAD010_PrintPage" %>
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
			<td class="SmpPrintPageTitle"><b>國內出差旅費核銷單</b></td>
		</tr>
	</table>
	<table style="table-layout:fixed;WIDTH: 740px; MARGIN-LEFT: 4px" class="BasicFormHeadBorder" border="0" cellSpacing="0" cellPadding="1" id="table2">
		<tr height="25px">
			<td class="SmpPrintPageHeadHead"><font size="2" face="Arial"><b>單號</b></font></td>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			  <b><asp:Label id="SheetNo" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="25px">
			<td class="SmpPrintPageHeadHead"><font size="2" face="Arial"><b>主旨</b></font></td>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
				<b><asp:Label id="Subject" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="25px">
			<td class="SmpPrintPageHeadHead"><font size="2" face="Arial"><b>公司別</b></font></td>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
				<b><asp:Label id="CompanyCode" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="25px">
			<td class="SmpPrintPageHeadHead" align="left">請款人員</td>
			<td class="SmpPrintPageHeadDetail">
				<b><asp:Label id="empNumber" runat="server" visible="false"></asp:Label>
				</b><b><asp:Label id="empName" runat="server" visible="false"></asp:Label>
				</b><b><asp:Label id="empEName" runat="server" visible="false"></asp:Label>
				</b><b><asp:Label id="empInfo" runat="server"></asp:Label></b>
			</td>
			<td class="SmpPrintPageHeadHead" align="left">請款部門</td>
			<td class="SmpPrintPageHeadDetail" colSpan="3">				
				<b><asp:Label id="deptInfo" runat="server"></asp:Label></b>
				<asp:Label id="deptId" runat="server" visible="false"></asp:Label>
				<asp:Label id="deptName" runat="server" visible="false"></asp:Label>
			</td>			
		</tr>
		
		<tr height="50px">
			<td class="SmpPrintPageHeadHead">請款總額</td>
			<td class="SmpPrintPageHeadDetail">
			  <b><asp:Label id="totalAmount" runat="server"></asp:Label></b>
			</td>
			<td class="SmpPrintPageHeadHead" align="left">審核人</td>
			<td class=SmpPrintPageHeadDetail>			
				<b><asp:Label id="checkBy" runat="server"></asp:Label></b>
            </td>
			<td class="SmpPrintPageHeadHead">領款人</td>
			<td class="SmpPrintPageHeadDetailSpad010">
			  <b><asp:Label id="payeeBy" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="25px">
			<td class="SmpPrintPageHeadHead">其他說明</td>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			  <b><asp:Label id="otherDesc" runat="server"></asp:Label></b>
			</td>
		</tr>
		
		<tr>
		  <td class="SmpPrintPageHeadDetail" colSpan="6" width="700px" >
		    <asp:Label ID="Label1" runat="server" Text="國內出差差旅費報銷明細清單" Font-Bold="True" Font-Size="Small"></asp:Label>
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
			<asp:Image runat="server" id="Img5" widht="150px" style="vertical-align:middle; width:130;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img5Time" runat="server"></asp:Label>			
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img4" widht="150" style="vertical-align:middle; width:130;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img4Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img3" widht="150" style="vertical-align:middle; width:130;table-layout:fixed;word-wrap:break-word;"  /><br>
			<asp:Label id="Img3Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img2" widht="150" style="vertical-align:middle; width:130;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img2Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img1" widht="150"  style="vertical-align:middle; width:130;table-layout:fixed;word-wrap:break-word;"  /><br>
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
