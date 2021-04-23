<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonSummary.aspx.cs" Inherits="SmpProgram_Form_SPAD004_PersonSummary" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body style="background:#ffffff">
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                身份證字號／護照號碼:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="IdNumber" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                出生年月日:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="Birthday" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
		<tr>
	        <td align="right" class="BasicFormHeadHead">
                身故受益人:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="Beneficiary" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>  		
		<tr>
	        <td align="right" class="BasicFormHeadHead">
                與被保險人關係:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="Relationship" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>  
        </table>
    </tr>
    </div>
	
    </form>
</body>
</html>
