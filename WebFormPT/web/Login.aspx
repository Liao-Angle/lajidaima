<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<HTML>
	<HEAD>
		<title>-</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
<script language=javascript>
function init() {
    //chrome與opera不支援此語法
    window.moveTo(0,0);
    window.resizeTo(window.screen.availWidth, window.screen.availHeight);
}
function addFavorite() {
    //此語法兼容於IE與firefox
    if (document.all) {
        window.external.AddFavorite('<%=com.dsc.kernal.utility.Utility.extractPath(Page.Request.Url.ToString())[0]%>', 'ECP');
    }
    else if (window.sidebar) {
        window.sidebar.addPanel('ECP', '<%=com.dsc.kernal.utility.Utility.extractPath(Page.Request.Url.ToString())[0]%>', '');
    }
}
</script>		
		<style type="text/css">
BODY { BACKGROUND-COLOR: rgb(247,245,235) }
.border { BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #999999 1px solid; COLOR: #ff0000; BORDER-BOTTOM: #999999 1px solid; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }
		</style>
	</HEAD>

	<body MS_POSITIONING="GridLayout" onload='init();'>
		<form id="Form1" method="post" runat="server">
		<table border=0 width=100% height=100%>
		<tr>
		    <td align=center valign=middle>
		    <table align=center valign=middle style="border-style:solid;border-color:rgb(153,153,153);border-width:1px" border=0 cellpadding=5 cellspacing=0>
		    <tr>
		        <td style="font-size:9pt;background-color:rgb(60,119,178);color:white"><asp:Literal ID="TITLE" runat="server"></asp:Literal></td>
		    </tr>
		    <tr>
		        <td style="padding:10px;background-color:#CCCAC2" width=300px  align=center valign=middle>
		            <table width=100% style="background-color:white;border-style:solid;border-color:rgb(153,153,153);border-width:1px" cellpadding=5 cellspacing=0>
		            <tr style="font-size:9pt">
		                <td>請輸入帳號</td>
		                <td><asp:TextBox id="UID" runat="server" CssClass="border" Width="200px"></asp:TextBox></td>
		            </tr>
		            <tr style="font-size:9pt">
		                <td>請輸入密碼</td>
		                <td><asp:TextBox id="PWD" runat="server" TextMode="Password" CssClass="border" Width="200px"></asp:TextBox></td>
		            </tr>
		            <asp:Panel ID="Panel1" runat="server">
		            <tr style="font-size:9pt">
		                <td>登入語系</td>
		                <td><asp:DropDownList ID="LANGUAGE" runat="server" Width="200px"/></td>
		            </tr>
		            </asp:Panel>
		            <asp:Panel ID="Panel2" runat="server">
		            <tr style="font-size:9pt">
		                <td>版面配置</td>
		                <td><asp:DropDownList ID="LAYOUT" runat="server" Width="200px"/></td>
		            </tr>
		            </asp:Panel>
		            </table>
		        </td>
		    </tr>
		    <tr style="background-color:#CCCAC2">
		        <td align=right>
		            <asp:Button id="Button1" runat="server" Text="  登入 " OnClick="Button1_Click" BackColor="#3C77B2" BorderColor="Black" BorderStyle="Solid" BorderWidth="0px" ForeColor="White"></asp:Button>
		            <asp:Button id="Button2" runat="server" Text="  加入我的最愛 " OnClientClick="addFavorite();return false;" BackColor="#3C77B2" BorderColor="Black" BorderStyle="Solid" BorderWidth="0px" ForeColor="White"></asp:Button>
		        </td>
		    </tr>
		    </table>
		    </td>
		</tr>
		</table>


			<asp:TextBox id="targetmodulef" style="Z-INDEX: 102; LEFT: 533px; POSITION: absolute; TOP: 170px"
				runat="server" Visible="False"></asp:TextBox>
				<iframe src="DSCWebControlRunTime/WaitingPanel.aspx" width="0px" height="0px"></iframe>
		</form>
	</body>
</HTML>
