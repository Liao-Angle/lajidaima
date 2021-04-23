<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="SmpProgram_Form_EH010301_PrintPage" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<style type="text/css">
@media print
{
  .print {display: none;}
}

</style>
     

<body style="background:#ffffff">
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
			<Input Type="Button" Value="列印單據" onClick="javascript:print();" class="print"> 
        <table>
	<table><br>
      <tr><td style=" font-size:8pt; ">表單資訊</td></tr>
        <tr>
     <td>
     <div id="divFormInfo"  style="position:absolute;" runat="server">
    </div>
    <br />
     </td>
     </tr>
      </table>
<br>
<br />
<br />
	<table style="margin-left:4px; width: 600px;" border="0" cellspacing="0" cellpadding="1">
   
     <tr>
      <td align="center" height="15" style="width:600px">
        <font style="font-family: 標楷體; font-size:30px"><b>請假單</b></font></td>
     </tr>
     </table>
     <!--內容 class="BasicFormHeadBorder"-->
    <table border="0" cellpadding="1" cellspacing="0" class="BasicFormHeadBorder"    style="width: 700px; ">    
  <tr style=" height:20px;">
    <td class="BasicFormHeadHead" style=" text-align:right; width:150px;  ">
         <asp:Label ID="Label1" runat="server" Text="主旨" Font-Size="8pt"></asp:Label>
    </td>
    <td   class="BasicFormHeadDetail"  colspan="3">
        <asp:Label ID="lblSubject" runat="server" Font-Size="8pt"></asp:Label>
    </td>
  
    </tr>
     <tr style=" height:20px;">
     <td class="BasicFormHeadHead"  style="text-align:right; ">
         <asp:Label ID="Label2" runat="server" Text="公司別" Font-Size="8pt"></asp:Label>
     </td>
     <td   class="BasicFormHeadDetail" >
         <asp:Label ID="lblCompany" runat="server" Font-Size="8pt"></asp:Label>
       </td>
     <td  class="BasicFormHeadHead" style=" text-align:right;">
         <asp:Label ID="Label3" runat="server" Text="請假人員部門" Font-Size="8pt"></asp:Label>
       </td>
    <td  class="BasicFormHeadDetail" >
      <asp:Label ID="lblDeptName" runat="server" Font-Size="8pt"></asp:Label>
    </td>
    </tr>
    <tr style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
     <asp:Label ID="Label4" runat="server" Text="請假人員" Font-Size="8pt"></asp:Label>

     </td>

     <td  class="BasicFormHeadDetail">
         <asp:Label ID="lblEmpNumber" runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right; ">
         <asp:Label ID="Label5" runat="server" Text="代理人員" Font-Size="8pt"></asp:Label>

     </td>
     <td  class="BasicFormHeadDetail" style=" height:20px;" >
       &nbsp;
         <asp:Label ID="lblAgent" runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

     <tr style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
          <asp:Label ID="Label6" runat="server" Text="流程類別" Font-Size="8pt"></asp:Label>

     </td>

     <td  class="BasicFormHeadDetail" >
         <asp:Label ID="lblFlowType"  runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right;">
         <asp:Label ID="Label7" runat="server" Text="班別" Font-Size="8pt"></asp:Label>

     </td>
     <td  class="BasicFormHeadDetail" style=" height:20px;" >
         <asp:Label ID="lblClassType"  runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>


     <tr style=" height:20px;">
      <td style="text-align:right;" class="BasicFormHeadHead">
          <asp:Label ID="Label8" runat="server" Text="假別" Font-Size="8pt"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" >
         <asp:Label ID="lblLeaveType" runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right; font-size:8pt;">
         <asp:Label ID="Label9" runat="server" Text="請假含假日" Font-Size="8pt"></asp:Label>

     </td>
     <td  class="BasicFormHeadDetail" >
         <asp:Label ID="lblIsHoliday" runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

     <tr style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
          <asp:Label ID="Label10" runat="server" Text="起始日期" Font-Size="8pt"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" >
         <asp:Label ID="lblStartDate" runat="server" Font-Size="8pt" 
             Font-Strikeout="False"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right;">
         <asp:Label ID="Label11" runat="server" Text="截止日期" Font-Size="8pt"></asp:Label>
     </td>
     <td class="BasicFormHeadDetail">
         <asp:Label ID="lblEndDate" runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

     <tr>
      <td class="BasicFormHeadHead"  style=" text-align:right;">
          <asp:Label ID="Label12" runat="server" Text="起始時間" Font-Size="8pt"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" >
         <asp:Label ID="lblStartTime" runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style=" text-align:right;">
     <asp:Label ID="Label13" runat="server" Text="截止時間" Font-Size="8pt" 
             Font-Overline="False"></asp:Label>
     </td>
     <td  class="BasicFormHeadDetail" style=" height:20px;">
         <asp:Label ID="lblEndTime" runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

    <tr style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
          <asp:Label ID="Label14" runat="server" Text="請假時數" Font-Size="8pt" 
              Font-Overline="False"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" style=" height:20px;">
         <asp:Label ID="lblLeaveHours" runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right;">
         <asp:Label ID="Label15" runat="server" Text="請假人員職稱" Font-Size="8pt"></asp:Label>
     </td>
     <td  class="BasicFormHeadDetail" style=" height:20px;">
         <asp:Label ID="lblDepty" runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

    <tr>
      <td class="BasicFormHeadHead"  style="text-align:right;">
          <asp:Label ID="Label16" runat="server" Text="說明" Font-Size="8pt"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" style=" height:20px;" colspan="3" >
         <asp:TextBox ID="lblDescription" runat="server" TextMode="MultiLine" 
             Width="518px" Height="70px" Font-Size="8pt"></asp:TextBox>
             
     </td>

    </tr>


    <tr  style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
          <asp:Label ID="Label17" runat="server" Text="審核人一" Font-Size="8pt"></asp:Label>
          
     </td>

     <td  class="BasicFormHeadDetail" style=" height:20px;" >
         <asp:Label ID="lblCheckby1"  runat="server" Font-Size="8pt"></asp:Label>
         &nbsp;
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right;">
   <asp:Label   ID="Label18" runat="server" Text="審核人二" Font-Size="8pt"></asp:Label>
     </td>
     <td class="BasicFormHeadDetail" style=" height:20px;" >&nbsp;
         <asp:Label ID="lblCheckby2"  runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

     <tr style=" height:20px;">


     <td class="BasicFormHeadDetail" colspan="4" >
         <asp:Label ID="DSCLabelBZ" runat="server" Font-Size="8pt"></asp:Label>
         
     </td>

    
    </tr>

    </table>
	
	<table>
       <tr><td style=" font-size:8pt; ">簽核意見</td></tr>
        <tr>
     <td>
     <div id="div2"  style="position:absolute;" runat="server">
    </div>
    <br />
     </td>
     </tr>
      </table>
      <br />
       <br />
	
    </form>
</body>
</html>
