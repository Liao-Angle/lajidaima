<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttendSummary.aspx.cs" Inherits="SmpProgram_Form_SPAD002_AttendSummary" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
	<script type="text/javascript">
	function searchURL(){
	    window.location = "http://www.myurl.com/search/" + (input text value);
	}
	</script>
</head>
<body style="background:#ffffff">
    <form id="form1" runat="server" readonly>
    <div>
        <table border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                員工代號:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="UserId" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                員工姓名:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="UserName" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                部門名稱:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="OrgUnitName" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        </table>
        <hr />

    </tr>
    </div>
	

    <asp:Label ID="Label1" runat="server" Text="出勤明細" Font-Bold="True" 
        Font-Size="Small"></asp:Label>

    <br />
    <br />
    <asp:GridView ID="gvAttendSummary" runat="server" Font-Size="X-Small" 
        Font-Bold="False" readonly>
        <FooterStyle Font-Bold="False" Font-Italic="False" />
        <HeaderStyle CssClass="BasicFormHeadHead" Font-Bold="False" Font-Size="X-Small" 
            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
            ForeColor="Black" />
    </asp:GridView>

    </form>
</body>
</html>
