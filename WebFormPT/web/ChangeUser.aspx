<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeUser.aspx.cs" Inherits="ChangeUser" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<HTML>
	<HEAD>
		<title>-</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link href="StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
		<style type="text/css">
BODY { BACKGROUND-COLOR: rgb(247,245,235) }
.border { BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #999999 1px solid; COLOR: #ff0000; BORDER-BOTTOM: #999999 1px solid; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }
		</style>
	</HEAD>

	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<table border=0 width=100% height=100%>
		<tr>
		    <td align=center valign=middle style="width: 100%">
                &nbsp;<table align=center valign=middle style="border-style:solid;border-color:rgb(153,153,153);border-width:1px" border=0 cellpadding=5 cellspacing=0>
		    <tr>
		        <td style="font-size:9pt;background-color:rgb(60,119,178);color:white">
                    登入身分轉換</td>
		    </tr>
		    <tr>
		        <td style="padding:10px;background-color:#CCCAC2" width=300px  align=center valign=middle>
		            <table width=100% style="background-color:white;border-style:solid;border-color:rgb(153,153,153);border-width:1px" cellpadding=5 cellspacing=0>
		            <tr style="font-size:9pt">
		                <td style="width: 57px">選擇身分</td>
		                <td>
                            <cc1:SingleOpenWindowField ID="NewUser" runat="server" FixReadOnlyValueTextWidth="100px" FixValueTextWidth="80px" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users" Width="210px" />
                        </td>
		            </tr>
		            </table>
		        </td>
		    <%--</tr>--%>
		    <tr style="background-color:#CCCAC2">
		        <td align=right>
		            <asp:Button id="Button1" runat="server" Text="  身分轉換 " OnClick="Button1_Click" BackColor="#3C77B2" BorderColor="Black" BorderStyle="Solid" BorderWidth="0px" ForeColor="White"></asp:Button>
		            <asp:Button id="Button2" runat="server" Text="  原身分登入 " OnClick="Button2_Click" BackColor="#3C77B2" BorderColor="Black" BorderStyle="Solid" BorderWidth="0px" ForeColor="White"></asp:Button>
		        </td>
		    </tr>
		    </table>
            </td>
		</tr>
		</table>
		</form>
	</body>
</HTML>
