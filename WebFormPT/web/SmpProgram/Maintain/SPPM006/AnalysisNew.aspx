<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnalysisNew.aspx.cs" Inherits="SmpProgram_Maintain_SPPM011_AnalysisNew" %>

<%@ Register assembly="DSCWebControl" namespace="DSCWebControl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
    </head>
<body >
    <form id="form1" runat="server">
    <div>  
    <table border="0" cellpadding="1" cellspacing="0" style=" width:400px; height :100px;">
    <tr>
    <td style=" width:100px;height:30px;">
    
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="評核人數:" TextAlign="2" 
            Width="100%" />
    
    </td>
    <td>
    
        <asp:TextBox ID="sfyphNum6" runat="server"></asp:TextBox>
    
    </td>
   
    </tr>
        <tr>
    <td style=" height:30px;">
    
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="已完成人數:" TextAlign="2" 
            Width="100%" />
    
    </td>
    <td>
        <asp:TextBox ID="sfCompleteNum6" runat="server"></asp:TextBox>   
    </td>
  
    </tr>
        <tr>
    <td style=" height:30px;">
    
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="未完成人數:" TextAlign="2" 
            Width="100%" />
    
    </td>
    <td>
        <asp:TextBox ID="sfNoCompleteNum6" runat="server"></asp:TextBox>    
    </td>
  
    </tr>
        </table>

        <table style="width: 550px" border="1">
        <tr>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="LBEmpNo" runat="server" Text="等級" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="A" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="B" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="C" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel7" runat="server" Text="D" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel8" runat="server" Text="E" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
        </tr>
        <tr>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel9" runat="server" Text="部門應占比例" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel10" runat="server" Text="0%~5%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel11" runat="server" Text="15.00%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel12" runat="server" Text="55.00%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel13" runat="server" Text="20.00%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel14" runat="server" Text="5.00%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
        </tr>

    </table>
    <hr />  
   <asp:Label ID="Label1" runat="server" Text="成績分佈統計" Font-Bold="True" 
            Font-Size="Small"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="gvAchievementScore6" runat="server" 
            ondatabound="gvAchievementScore6_DataBound" Width="550px" 
            onrowdatabound="gvAchievementScore6_RowDataBound">
        <RowStyle HorizontalAlign="Center" />
    </asp:GridView>
    <br />
        <asp:Label ID="lblplanGUID" runat="server" Text="" Visible="False"></asp:Label>
        <asp:Label ID="lblAssessUserGUID" runat="server" Text="" Visible="False"></asp:Label>
    </div>

    </form>
</body>
</html>
