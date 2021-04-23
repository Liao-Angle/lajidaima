<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnalysisDetail.aspx.cs" Inherits="SmpProgram_Form_SPPM006_AnalysisDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type"content="text/html;charset=gb2312" />
    <title></title>	
    <style type="text/css"> 
        .takeItRight
        {
         text-align:right;
        }
    </style>    
</head>

<body style=" text-align:center;">
    <form id="form2" runat="server">
        <asp:Label ID="Label1" runat="server" Text="成績分佈人員明細" Font-Bold="True" 
            Font-Size="Small"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="gvAchievementList" runat="server" Font-Size="Small"  
            Font-Bold="False" Width="651px" 
            onrowdatabound="gvAchievementList_RowDataBound">
            <FooterStyle Font-Bold="False" Font-Italic="False" />
            <HeaderStyle CssClass="BasicFormHeadHead" Font-Bold="False" Font-Size="Small" 
                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                ForeColor="Black" Height="25px" />
            <RowStyle Height="20px" />
        </asp:GridView> 
  
    </form>

    
</body>
</html>


