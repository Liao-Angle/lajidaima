<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="SmpProgram_Form_SPAD007_PrintPage" %>
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
			<td class="SmpPrintPageTitle"><b>列印國外出差差旅費報銷單</b></td>
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
			<td class="SmpPrintPageHeadHead"><font size="2" face="Arial"><b>公司別</b></font></td>
			<td class="SmpPrintPageHeadDetail" colSpan="4">
			  <b><asp:Label id="CompanyCode" runat="server"></asp:Label></b>
			</td>
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
			<td class="SmpPrintPageHeadHead">勾稽出差申請單號</td>
			<td class=SmpPrintPageHeadDetail colSpan="4">
				<b><asp:Label id="OriForeignFormNo" runat="server"></asp:Label></b>
            </td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">預計出差日期</td>
			<td class=SmpPrintPageHeadDetail colSpan="4">
			  <b><asp:Label id="StartTrvlDate" runat="server"></asp:Label>	
			  <cc1:DSCLabel ID="DSCLabel01" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
			  <asp:Label id="EndTrvlDate" runat="server"></asp:Label>
            </td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">異動後出差日期</td>
			<td class=SmpPrintPageHeadDetail colSpan="4">
			  <b><asp:Label id="ChgStartTrvlDate" runat="server"></asp:Label>	
			  <cc1:DSCLabel ID="DSCLabel02" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
			  <asp:Label id="ChgEndTrvlDate" runat="server"></asp:Label>
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
			<td class="SmpPrintPageHeadHead" colSpan="5">異動或取消說明</td>
		</tr>
		<tr>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			<b><asp:Label id="ChgTrvlDesc" runat="server"></asp:Label></b>
			</td>
		</tr>
				
		<tr height="20px">
			<td class="SmpPrintPageHeadHead" colSpan="5"><b>C 預支申請</b></td>
		</tr>
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">原預支申請</td>
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
			<td class="SmpPrintPageHeadHead">異動後預支申請</td>
			<td class="SmpPrintPageHeadDetail" colSpan="4">
			  <b><asp:Label id="ChgPrePayTwd" runat="server" visible="false"></asp:Label>
			  <asp:Label id="ChgPrePayTwdAmt" runat="server" visible="false"></asp:Label>
  			  <asp:Label id="ChgPrePayCny" runat="server" visible="false"></asp:Label>
	  		  <asp:Label id="ChgPrePayCnyAmt" runat="server" visible="false"></asp:Label>
			  <asp:Label id="ChgPrePayUsd" runat="server" visible="false"></asp:Label>
			  <asp:Label id="ChgPrePayUsdAmt" runat="server" visible="false"></asp:Label>
			  <asp:Label id="ChgPrePayOther" runat="server" visible="false"></asp:Label>
			  <asp:Label id="ChgPrePayOtherAmt" runat="server" visible="false"></asp:Label>
			  <asp:Label id="ChgPrePayInfo" runat="server"></asp:Label></b>
			</td>
		</tr>
				
		<tr height="20px">
			<td class="SmpPrintPageHeadHead">實際出差日期</td>
			<td class=SmpPrintPageHeadDetail colSpan="4">
			  <b><asp:Label id="ActualStartTrvlDate" runat="server"></asp:Label>	
			  <cc1:DSCLabel ID="DSCLabel03" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
			  <asp:Label id="ActualEndTrvlDate" runat="server"></asp:Label>
            </td>
		</tr>

		<tr>
		  <td class="SmpPrintPageHeadDetail" colSpan="5" width="600px" >
		    <asp:Label ID="Label1" runat="server" Text="差旅費報銷明細清單" Font-Bold="True" Font-Size="Small"></asp:Label>
		    <asp:GridView ID="tripFeeSummary" runat="server" Font-Size="X-Small" Font-Bold="False" width="695px" >
		        <FooterStyle Font-Bold="False" Font-Italic="False" />
		        <HeaderStyle CssClass="BasicFormHeadHead" Font-Bold="False" Font-Size="X-Small" 
		            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Width="650px"
		            ForeColor="Black" />
		    </asp:GridView>
			<br>
	      </td>
		</tr>
		
		<tr>
		  <td class="SmpPrintPageHeadDetail" colSpan="5">
		    <table>
				<tr height="20px">
					<td align="center" class="SmpPrintPageHeadHead"> </td>					
					<td align="center" class="SmpPrintPageHeadHead">台幣</td>
					<td align="center" class="SmpPrintPageHeadHead">美金</td>
					<td align="center" class="SmpPrintPageHeadHead">日幣</td>
					<td align="center" class="SmpPrintPageHeadHead">韓幣</td>
					<td align="center" class="SmpPrintPageHeadHead">人民幣</td>
					<td align="center" class="SmpPrintPageHeadHead">馬幣</td>
					<td align="center" class="SmpPrintPageHeadHead">歐元</td>
					<td align="center" class="SmpPrintPageHeadHead">其他</td>
				</tr>
				<tr align="middle" height="20px">
					<td class="SmpPrintPageHeadDetail" width="150"><b>金額</b>
		            </td>			
					<td class="SmpPrintPageHeadDetail" width="150"><b>
						<asp:Label id="ActualPayTw" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayUs" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayJp" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayKr" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayCn" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayMa" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayOu" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayOther" runat="server"></asp:Label></b>
		            </td>
				</tr>		
				<tr align="middle" height="20px">
					<td class="SmpPrintPageHeadDetail" width="150"><b>換算NT</b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150"><b>
						<asp:Label id="ActualPayTwTw" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayUsTw" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayJpTw" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayKrTw" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayCnTw" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayMaTw" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayOuTw" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150">
						<b><asp:Label id="ActualPayOtherTw" runat="server"></asp:Label></b>
		            </td>
				</tr>				
			
			</table>
		  </td>
		</tr>
		<tr>
		  <td colSpan="5">
		    <table>
				<tr align="middle" height="20px">
					<td class="SmpPrintPageHeadHead" width="150"><b>總金額</b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150"><b>
						<asp:Label id="ActualAmount" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadHead" width="150"><b>應付金額</b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150"><b>
						<asp:Label id="ActualApAmt" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadHead" width="150"><b>預支還回</b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150"><b>
						<asp:Label id="ActualRtnApAmt" runat="server"></asp:Label></b>
		            </td>
					<td class="SmpPrintPageHeadHead" width="150"><b>還回日期</b>
		            </td>
					<td class="SmpPrintPageHeadDetail" width="150"><b>
						<asp:Label id="ActualRtnApDate" runat="server"></asp:Label></b>
		            </td>
				</tr>
			</table>
		  </td>
		</tr>
		
		<tr height="20px">
			<td class="SmpPrintPageHeadHead" colSpan="5">財務註記</td>
		</tr>
		<tr>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			<b><asp:Label id="FinDesc" runat="server"></asp:Label></b>
			</td>
		</tr>	
		<tr>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			<font color="#ff0000" size="2"><b>** 依照『營利事業所得稅查核準則』規定,國外出差費報銷,應保留飛機票票根或登機證票根及電子機票 ** </b></font><br>
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
			<asp:Image runat="server" id="Img5" widht="110px" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img5Time" runat="server"></asp:Label>			
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img4" widht="110" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img4Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img3" widht="110" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img3Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img2" widht="110" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img2Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Image runat="server" id="Img1" widht="110" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
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
