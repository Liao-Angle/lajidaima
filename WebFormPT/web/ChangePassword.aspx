<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<HTML>
	<HEAD>
		<title>-</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
BODY { BACKGROUND-COLOR: rgb(247,245,235) }
.border { BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #999999 1px solid; COLOR: #ff0000; BORDER-BOTTOM: #999999 1px solid; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }
		</style>
	</HEAD>

	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<table border=0 width=100% height=100%>
		<tr>
		    <td align=center valign=middle>
		    <table align=center valign=middle style="border-style:solid;border-color:rgb(153,153,153);border-width:1px" border=0 cellpadding=5 cellspacing=0>
		    <tr>
		        <td style="font-size:9pt;background-color:rgb(60,119,178);color:white">密碼已經過期, 您必須變更密碼後重新登入</td>
		    </tr>
		    <tr>
		        <td style="padding:10px;background-color:#CCCAC2" width=300px  align=center valign=middle>
		            <table width=100% style="background-color:white;border-style:solid;border-color:rgb(153,153,153);border-width:1px" cellpadding=5 cellspacing=0>
		            <tr style="font-size:9pt">
		                <td style="width: 113px">請輸入新密碼</td>
		                <td><asp:TextBox id="PWD" runat="server" TextMode="Password" CssClass="border" Width="178px"></asp:TextBox></td>
		            </tr>
		            <tr style="font-size:9pt">
		                <td style="width: 113px">再次輸入新密碼</td>
		                <td><asp:TextBox id="PWD2" runat="server" TextMode="Password" CssClass="border" Width="178px"></asp:TextBox></td>
		            </tr>
		            </table>
		        </td>
		    </tr>
		    <tr style="background-color:#CCCAC2">
		        <td align=right>
		            <asp:Button id="Button1" runat="server" Text="  變更密碼 " OnClick="Button1_Click" BackColor="#3C77B2" BorderColor="Black" BorderStyle="Solid" BorderWidth="0px" ForeColor="White"></asp:Button>
		        </td>
		    </tr>
		    </table>
		    </td>
		</tr>
		</table>
		</form>
	</body>
</HTML>
