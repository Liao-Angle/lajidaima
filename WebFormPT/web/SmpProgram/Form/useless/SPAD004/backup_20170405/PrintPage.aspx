<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="SmpProgram_Form_SPAD004_PrintPage" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf8">
	
	<script type="text/javascript">   
        // 列印
	  function fnPrint() {
		  try    
		  {
			// 儲存原本頁首頁尾的設定，然後設定空白
			var ret = saveAndClearSetting();
			// 列印
			window.print();
			// 回存原本頁首頁尾的設定
			if ( ret ) restoreSetting();
		  } catch (e) 
		  { alert("err="+e.description); }
	  }
		var hkey_path = "HKEY_CURRENT_USER\\Software\\Microsoft\\Internet Explorer\\PageSetup\\";
		var hkey_key_header = hkey_path+"header"; // 頁首
		var hkey_key_footer = hkey_path+"footer"; // 頁尾
		var hkey_key_margin_bottom = hkey_path+"margin_bottom"; // 邊界（下）
		var hkey_key_margin_left = hkey_path+"margin_left"; // 邊界（左）
		var hkey_key_margin_right = hkey_path+"margin_right"; // 邊界（右）
		var hkey_key_margin_top = hkey_path+"margin_top"; // 邊界（上）
		var old_header = "&w&b第 &p 頁，共 &P 頁";
		var old_footer = "&u&b&d";
		// 儲存原本頁首頁尾的設定，然後設定空白
		
		function saveAndClearSetting() {
			try {
				var RegWsh = new ActiveXObject("Wscript.Shell");
				old_header = RegWsh.RegRead(hkey_key_header);
				old_footer = RegWsh.RegRead(hkey_key_footer);
				RegWsh.RegWrite(hkey_key_header,"");
				RegWsh.RegWrite(hkey_key_footer,"");
				return true;
			} catch (e) {
				if ( e.description.indexOf("伺服程式無法產生物件") != -1 ) {
				  alert("請調整IE瀏覽器的安全性\n網際網路選項＼安全性＼自訂層級\n「起始不標示為安全的ActiveX控制項」設定為啟用或提示。");
				} // if
				else {
				  alert("ERR="+e.description);
				} // else
			} // catch
			return false;
	  }
	  // 回存原本頁首頁尾的設定
	  function restoreSetting() {
		try {
		  var RegWsh = new ActiveXObject("WScript.Shell");
		  RegWsh.RegWrite(hkey_key_header,old_header);
		  RegWsh.RegWrite(hkey_key_footer,old_footer);
		} catch (e) {
		  if ( e.description.indexOf("伺服程式無法產生物件") != -1 ) {
			alert("請調整IE瀏覽器的安全性\n網際網路選項＼安全性＼自訂層級\n「起始不標示為安全的ActiveX控制項」設定為啟用或提示。");
		  } // if
		  else {
			alert("ERR="+e.description);
		  } // else
		} // catch
	  }  
    </script>
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
			<Input Type="Button" Value="列印單據" onClick="fnPrint();" class="print"> 
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
		<!--<tr height="20px">
			<td class="SmpPrintPageHeadHead">費用負擔</td>
			<td class="SmpPrintPageHeadDetail" colSpan="4"><b>
			<asp:Label id="FeeCharge" runat="server" visible="false"></asp:Label>
			<asp:Label id="FeeChargeInfo" runat="server"></asp:Label></b>
			</td>
		</tr>-->
		<tr height="20px">
			<td class="SmpPrintPageHeadHead" colSpan="5">出差（預辦）事由：（說明：如費用由子公司負擔，需經子公司核准）</td>
		</tr>
		<tr>
			<td class="SmpPrintPageHeadDetail" colSpan="5">
			<b><asp:Label id="TrvlDesc" runat="server"></asp:Label></b>
			</td>
		</tr>

		<tr height="20px">
			<td class="SmpPrintPageHeadHead" colSpan="5"><b>預支申請</b></td>
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
			  <asp:Label id="PrePayJpy" runat="server" visible="false"></asp:Label>
			  <asp:Label id="PrePayJpyAmt" runat="server" visible="false"></asp:Label>
			  <asp:Label id="PrePayEur" runat="server" visible="false"></asp:Label>
			  <asp:Label id="PrePayEurAmt" runat="server" visible="false"></asp:Label>
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
			<font size=2><asp:Label id="SignUser6" runat="server"></asp:Label>
			<asp:Image runat="server" id="Img6" width="120px"/><br>
			<asp:Label id="Img6Time" runat="server"></asp:Label>
		  </td>
		  <td class="SmpPrintPageHeadDetail">
			<font size=2><asp:Label id="SignUser5" runat="server"></asp:Label>
			<asp:Image runat="server" id="Img5" widht="110" height="50" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img5Time" runat="server"></asp:Label>			
		  </td>
		  <td class="SmpPrintPageHeadDetail">
			<font size=2><asp:Label id="SignUser4" runat="server"></asp:Label>
		    <asp:Image runat="server" id="Img4" widht="110" height="50" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img4Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
			<font size=2><asp:Label id="SignUser3" runat="server"></asp:Label>
		    <asp:Image runat="server" id="Img3" widht="110" height="50" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img3Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
			<font size=2><asp:Label id="SignUser2" runat="server"></asp:Label>
		    <asp:Image runat="server" id="Img2" widht="110" height="50" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img2Time" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
			<font size=2><asp:Label id="SignUser1" runat="server"></asp:Label>
		    <asp:Image runat="server" id="Img1" widht="110" height="50" style="vertical-align:middle; width:120;table-layout:fixed;word-wrap:break-word;"/><br>
			<asp:Label id="Img1Time" runat="server"></asp:Label> 
		  </td>
		</tr>
		<tr>
		  <td class="SmpPrintPageHeadDetail" >
			<asp:Label id="Comment6" runat="server"></asp:Label>
		  </td>
		  <td class="SmpPrintPageHeadDetail">
			<asp:Label id="Comment5" runat="server"></asp:Label>			
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Label id="Comment4" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Label id="Comment3" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Label id="Comment2" runat="server"></asp:Label> 
		  </td>
		  <td class="SmpPrintPageHeadDetail">
		    <asp:Label id="Comment1" runat="server"></asp:Label> 
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
