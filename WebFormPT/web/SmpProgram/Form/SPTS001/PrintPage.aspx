<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="SmpProgram_Form_SPTS001_PrintPage" %>
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
				old_top = RegWsh.RegRead(hkey_key_margin_top);
				old_bottom = RegWsh.RegRead(hkey_key_margin_bottom);
				RegWsh.RegWrite(hkey_key_header,"");
				RegWsh.RegWrite(hkey_key_footer,"");
				RegWsh.RegWrite(hkey_key_margin_top,"0.3000");
				RegWsh.RegWrite(hkey_key_margin_bottom,"");
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

tr{
	max-height: "30px";
	border-style:solid;
	border-width:2px;
	font-size:12pt;
	text-align : center;	
	font-family:新細明體, Arial;
	line-height:33px;
	font-weight: bold;
	border-bottom-color:Black;
	border-right-color:Black;
	border:1px 1px 1px 1px;
	border-left:1px solid #999999;
}

table{
	text-align : center;		
}
</style>
     

<body style="background:#ffffff"　leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" width="760">
    <form  runat="server">
    <table border=0 cellspacing=0 cellpadding=0 class=BasicFormHeadBorder>
	  <tr align="left">
	    <td align="left">
			<Input Type="Button" Value="列印單據" onClick="fnPrint();" class="print">
		</td>
	  </tr>
    </table>	
<div style="text-align: center">
  <table width="750" bgcolor="#ffffff" cellspacing="0" cellpadding="0" align="center" style="margin:0 auto;width:750;">
    <tr> 
	  <td>	

	<table style="WIDTH: 750px; MARGIN-LEFT: 4px" border="0" cellSpacing="0" cellPadding="1">
		<tr height="45" align="middle">
		  <td>
			<!--<img src="../../../images/smplogo_form.jpg" width="360">-->
			<asp:Image runat="server" id="ImgLogo" width="360" style="vertical-align:middle; width:360;table-layout:fixed;word-wrap:break-word;"/>
		  </td>
		</tr>
		<tr height="35" align="middle">
			<td class="SmpPrintPageTitle"><b>教育訓練簽到表</b></td>
		</tr>
	</table>

	<table style="WIDTH: 750px;" border="0" cellSpacing="0" cellPadding="0">
		<tr height="30px">
			<td width="150" align="right"><b>課程名稱：</b></td>
			<td colSpan="3" align="left">
			  <b><asp:Label id="SubjectName" runat="server"></asp:Label></b>
			</td>
		</tr>
		<tr height="30px">
			<td width="150" align="right"><b>課程日期：</b></td>
			<td width="200" align="left">
				<b><asp:Label id="TrainDate" runat="server"></asp:Label></b>
			</td>
			<td width="150" align="right"><b>上課時間：</b></td>
			<td width="200" align="left">
				<b><asp:Label id="TrainTime" runat="server"></asp:Label></b>
			</td>
		</tr>
	</table>
	
	<table style="table-layout:auto;WIDTH: 750px; MARGIN-LEFT: 1px"  border="0" cellSpacing="1" cellPadding="1" >
		<!--<tr style="table-layout:auto; width=750px" height="30px">
		  <td style="table-layout:auto; width=50px">序號</td>
		  <td style="table-layout:auto; width=50px">工號</td>
		  <td style="table-layout:auto; width=120px">學員姓名</td>
		  <td style="table-layout:auto; width=170px">簽名(中文)</td>
		  <td style="table-layout:auto; width=80px">序號</td>
		  <td style="table-layout:auto; width=100px">工號</td>
		  <td style="table-layout:auto; width=250px">學員姓名</td>
		  <td style="table-layout:auto; width=350px">簽名(中文)</td>
		</tr>-->
		<tr>
		  <td width=750px" colSpan="8">
		    <asp:GridView ID="TraineeList" runat="server" Font-Size="X-Small" Font-Bold="False">
				<FooterStyle Font-Bold="False" Font-Italic="False" />
				<HeaderStyle CssClass="BasicFormHeadHead" Font-Bold="False" Font-Size="X-Small" Font-Italic="False" 
				         Font-Overline="False" Font-Strikeout="False" ForeColor="Black" />
			</asp:GridView>
	      </td>
		</tr>		
	</table>
	<br>
	<table  width="750" bgcolor="#ffffff"  align="center">
		<tr height="40px">
			<td width="150" align="right"><b>上課人數：</b></td>
			<td width="150" align="left"><b>__________ 人</b></td>
			<td width="150" align="right"><b>到課率：</b></td>
			<td width="150" align="left"><b>__________%</b></td>
		</tr>	
	</table>
	</div>	
    </form>
</td></tr></table>
</div>
</body>
</html>
