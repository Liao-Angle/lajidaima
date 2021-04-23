<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Analysis.aspx.cs" Inherits="SmpProgram_Form_SPPM006_Analysis" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>成績分佈統計</title>	
    <style type="text/css"> 
        .takeItRight
        {
        text-align=right;
        }
    </style>    
</head>

<body>
    <form id="form2" runat="server">
        <div>
        <table border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                評核人數:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="Total" runat="server" ReadOnly="True" class="takeItRight"></asp:TextBox>
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                已完成人數:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="Complete" runat="server" ReadOnly="True" class="takeItRight"></asp:TextBox>
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                未完成人數:
            </td>
            <td class=BasicFormHeadDetail>
                <asp:TextBox ID="UnComplete" runat="server" ReadOnly="True" class="takeItRight"></asp:TextBox>
            </td>
        </tr>
        </table>
        <hr />       
        </div>

        <asp:Label ID="Label1" runat="server" Text="成績分佈統計" Font-Bold="True" 
            Font-Size="Small"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="gvAchievementList" runat="server" Font-Size="X-Small"  
            Font-Bold="False" Width="351px" 
            onrowdatabound="gvAchievementList_RowDataBound">
            <FooterStyle Font-Bold="False" Font-Italic="False" />
            <HeaderStyle CssClass="BasicFormHeadHead" Font-Bold="False" Font-Size="X-Small" 
                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                ForeColor="Black" />
        </asp:GridView> 
       
    </form>

    
</body>
</html>


