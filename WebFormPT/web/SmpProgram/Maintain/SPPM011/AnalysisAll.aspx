<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnalysisAll.aspx.cs" Inherits="SmpProgram_Maintain_SPPM011_AnalysisNew" %>

<%@ Register assembly="DSCWebControl" namespace="DSCWebControl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
     .GlassButton_Normal
     {
	  border-style :outset;
	  border-width :1pt;
	  font-size:10pt;
	  background-color:rgb(179,202,213);
	  line-height:20px;
	  font-family:Arial;
     }
    </style>
</head>
<body style="background-color: #FFFFFF">
    <form id="form1" runat="server">
    <div style=" text-align:right;">
        <asp:Button ID="btoutExcel" runat="server" onclick="btoutExcel_Click" 
            Text="匯出Excel" CssClass="GlassButton_Normal" Visible="False" />
    </div>
    <div style=" text-align:center;">  
   <asp:Label ID="Label1" runat="server" Text="成績分佈統計匯總" Font-Bold="True" 
            Font-Size="Small"></asp:Label>
    <br />
    <br />
    <table style="width: 650px" border="1">
        <tr>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="LBEmpNo" runat="server" Text="等級" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="A" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="B" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="C" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="D" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="E" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
        </tr>
        <tr>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="部門應占比例" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel7" runat="server" Text="0%~5%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel8" runat="server" Text="15.00%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel9" runat="server" Text="55.00%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel10" runat="server" Text="20.00%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
            <td class="BasicFormHeadDetail"><cc1:DSCLabel ID="DSCLabel11" runat="server" Text="5.00%" Width="100%" TextAlign="1"></cc1:DSCLabel></td>
        </tr>

    </table>
    <asp:GridView ID="gvAchievementScore" runat="server" 
            ondatabound="gvAchievementScore_DataBound" Width="650px" 
            AutoGenerateColumns="False" onrowdatabound="gvAchievementScore_RowDataBound">
        <Columns>
            <asp:BoundField DataField="工號" HeaderText="工號" />
            <asp:BoundField DataField="主管" HeaderText="主管" />
            <asp:BoundField DataField="部門" HeaderText="部門" />
            <asp:BoundField DataField="說明" HeaderText="說明" />
            <asp:BoundField DataField="A" HeaderText="A" />
            <asp:BoundField DataField="B" HeaderText="B" />
            <asp:BoundField DataField="C" HeaderText="C" />
            <asp:BoundField DataField="D" HeaderText="D" />
            <asp:BoundField DataField="E" HeaderText="E" />
            <asp:BoundField DataField="未評核" HeaderText="未評核" />
        </Columns>
        <RowStyle HorizontalAlign="Center" />
    </asp:GridView>
    <br />
        <cc1:DSCLabel ID="lblplanGUID" runat="server" Display="False" />
    <br />
    </div>

    </form>
</body>
</html>
