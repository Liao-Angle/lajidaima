<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveSummary.aspx.cs" Inherits="SmpProgram_Form_SPAD001_LeaveSummary" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
        <table border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                年度特休時數:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="YearHours" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                已休特休時數(由到職日起算):
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="UsedHours" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                未休特休時數:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="NotUsedHours" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        </table>
        <hr />
    </div>

    <asp:Label ID="Label1" runat="server" Text="請假明細" Font-Bold="True" 
        Font-Size="Small"></asp:Label>

    <br />
    <br />
    <asp:GridView ID="gvLeaveSummary" runat="server" Font-Size="X-Small" 
        Font-Bold="False">
        <FooterStyle Font-Bold="False" Font-Italic="False" />
        <HeaderStyle CssClass="BasicFormHeadHead" Font-Bold="False" Font-Size="X-Small" 
            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
            ForeColor="Black" />
    </asp:GridView>

    </form>
</body>
</html>
