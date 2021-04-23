<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="SmpProgram_Form_STHR003DY_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var HKEY_Root, HKEY_Path, HKEY_Key;
        HKEY_Root = "HKEY_CURRENT_USER";
        HKEY_Path = "\\Software\\Microsoft\\Internet Explorer\\PageSetup\\";
        //设置网页打印的页眉页脚为空 
        function PageSetup_Null() {
            try {
                var Wsh = new ActiveXObject("WScript.Shell");
                HKEY_Key = "header";
                Wsh.RegWrite(HKEY_Root + HKEY_Path + HKEY_Key, "");
                HKEY_Key = "footer";
                Wsh.RegWrite(HKEY_Root + HKEY_Path + HKEY_Key, "");
            }
            catch (e)
            { }
        }

        //預覽 
        function yl(myDiv) {
            PageSetup_Null();
            bdhtml = document.getElementById(myDiv).innerHTML; //window.document.body.innerHTML;
            //alert(bdhtml);
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();

        }


        ///打印控件內容
        function printview(myDiv) {
            var newstr = document.getElementById(myDiv).innerHTML;
            // alert(newstr);
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = newstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
      
    </script>

    <style type="text/css">
             a {
	font-size:12px; 
	color:#242720; 
	text-decoration:none; 
    }     
    a:hover
    {
	color:#D0AD91; 
	text-decoration:none;
	border:0px;
    }
    .c
    {
        font-size:19px;
        height:10px;
        }
   
   @media print
{
  .print {display: none;}
}
.btn
{
	 border-width:0px;
	   border-style:Solid;
	   border-color:Black;
	    background-color:#3C77B2;
	    color:White;
	    /*控件變手行*/
	   cursor: pointer;	  
	}
	
    </style>

    </head>
<body style=" font-size:8pt; " leftmargin="0" topmargin="0">
    <form id="form1" runat="server">


    <div>
    <br />
        <Input Type="Button" Value="列印單據" onClick="javascript:print();" style="  border-width:0px;
	   border-style:Solid;
	   border-color:Black;
	    background-color:#3C77B2;
	    color:White;
	    /*控件變手行*/
	   cursor: pointer;	 " class="print"> 
     <%--  <a href="javascript:void(0)"  onclick="printview('print')"><H3>列印</H3> </a>--%>
       
    </div>  
    <br />
      <div id="print" class="c">
      

      <table>
      <tr><td style=" font-size:8pt; ">表單資訊</td></tr>
        <tr>
     <td>
     <div id="div3"  style="position:absolute;" runat="server">
    </div>
    <br />
     </td>
     </tr>
      </table>
      <br />
      <br />
      <br />
      <br />
     

    <table style="margin-left:4px; width: 600px;" border="0" cellspacing="0" cellpadding="1">
   
     <tr>
      <td align="center" height="15" style="width:600px">
        <font style="font-family: 標楷體; font-size:30px"><b>請假單</b></font></td>
     </tr>
     </table>
     <!--內容 class="BasicFormHeadBorder"-->
    <table border="0" cellpadding="1" cellspacing="0" class="BasicFormHeadBorder"    style="width: 600px; ">    
  <tr style=" height:20px;">
    <td class="BasicFormHeadHead" style=" text-align:right; width:150px;  ">
         <%--<cc1:DSCLabel ID="DSCLabel10" runat="server"  Text="主旨"  Width="35px" 
             Font-Size="50pt" Font-Strikeout="False" Height="20px"  />--%>
        <asp:Label ID="Label1" runat="server" Text="主旨" Font-Size="8pt"></asp:Label>

         </td>
    <td   class="BasicFormHeadDetail"  colspan="3">
   
        <%--<cc1:DSCLabel ID="DSCLabelzhuzhi" runat="server" Width="524px" />--%>
        <asp:Label ID="DSCLabelzhuzhi" runat="server" Font-Size="8pt"></asp:Label>
    </td>
  
    </tr>
     <tr style=" height:20px;">
     <td class="BasicFormHeadHead"  style="text-align:right; ">
   <%--  <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="公司別" Width="50px" 
             Font-Bold="False" />--%>
         <asp:Label ID="Label2" runat="server" Text="公司別" Font-Size="8pt"></asp:Label>

     </td>
     <td   class="BasicFormHeadDetail" >
         <%--<cc1:DSCLabel ID="DSCLabelGSB" runat="server" Width="163px" />--%>
         <asp:Label ID="DSCLabelGSB" runat="server" Font-Size="8pt"></asp:Label>
       </td>
     <td  class="BasicFormHeadHead" style=" text-align:right;">
     <%--   <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="請假人員部門" Width="95px" 
             style="margin-left: 163px"/>--%>
         <asp:Label ID="Label3" runat="server" Text="請假人員部門" Font-Size="8pt"></asp:Label>

       </td>
    <td  class="BasicFormHeadDetail" >
    <%-- <cc1:DSCLabel ID="DSCLabelBM" runat="server" Width="194px" Height="16px" />--%>
      <asp:Label ID="DSCLabelBM" runat="server" Font-Size="8pt"></asp:Label>
    </td>
    </tr>


    <tr style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
    <%-- <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="請假人員" Width="65px"/>--%>
     <asp:Label ID="Label4" runat="server" Text="請假人員" Font-Size="8pt"></asp:Label>

     </td>

     <td  class="BasicFormHeadDetail">
        <%--  <cc1:DSCLabel ID="DSCLabelNoName" runat="server" Width="176px" />--%>
         <asp:Label ID="DSCLabelNoName" runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right; ">
     <%--<cc1:DSCLabel ID="DSCLabel3" runat="server" Text="代理人員" Width="65px"/>--%>
         <asp:Label ID="Label5" runat="server" Text="代理人員" Font-Size="8pt"></asp:Label>

     </td>
     <td  class="BasicFormHeadDetail" style=" height:20px;" >
       <%-- <cc1:DSCLabel ID="DSCLabelDL" runat="server" Width="203px" />--%>
       &nbsp;
         <asp:Label ID="DSCLabelDL" runat="server" Font-Size="8pt"></asp:Label>
         <%--<asp:Image ID="Image1" runat="server" 
             ImageUrl="~/SmpProgram/Form/STHR003DY/123.gif" />--%>
     </td>
    </tr>

     <tr style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
     <%--<cc1:DSCLabel ID="DSCLabel4" runat="server" Text="流程類別" Width="65px"/>--%>
          <asp:Label ID="Label6" runat="server" Text="流程類別" Font-Size="8pt"></asp:Label>

     </td>

     <td  class="BasicFormHeadDetail" >
         <%-- <cc1:DSCLabel ID="DSCLabeltype" runat="server" Width="174px" Height="16px" />--%>
         <asp:Label ID="DSCLabeltype"  runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right;">
     <%--<cc1:DSCLabel ID="DSCLabel6" runat="server" Text="班別" Width="40px"/>--%>
         <asp:Label ID="Label7" runat="server" Text="班別" Font-Size="8pt"></asp:Label>

     </td>
     <td  class="BasicFormHeadDetail" style=" height:20px;" >
        <%--<cc1:DSCLabel ID="DSCLabelbb" runat="server" Width="220px" />--%>
         <asp:Label ID="DSCLabelbb"  runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>


     <tr style=" height:20px;">
      <td style="text-align:right;" class="BasicFormHeadHead">
     <%--<cc1:DSCLabel ID="DSCLabel5" runat="server" Text="假別" Width="40px"/>--%>

          <asp:Label ID="Label8" runat="server" Text="假別" Font-Size="8pt"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" >
         <%-- <cc1:DSCLabel ID="DSCLabelJB" runat="server" Width="180px" />--%>
         <asp:Label ID="DSCLabelJB" runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right; font-size:8pt;">
    <%-- <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="請假含假日" Width="75px"/>--%>
         <asp:Label ID="Label9" runat="server" Text="請假含假日" Font-Size="8pt"></asp:Label>

     </td>
     <td  class="BasicFormHeadDetail" >
        <%--<cc1:DSCLabel ID="DSCLabelQJHJR" runat="server" Width="204px" />--%>
         <asp:Label ID="DSCLabelQJHJR" runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

     <tr style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
    <%-- <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="起始日期" Width="65px"/>--%>
          <asp:Label ID="Label10" runat="server" Text="起始日期" Font-Size="8pt"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" >
         <%-- <cc1:DSCLabel ID="DSCLabelqsrq" runat="server" Width="182px" />--%>
         <asp:Label ID="DSCLabelqsrq" runat="server" Font-Size="8pt" 
             Font-Strikeout="False"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right;">

    <%-- <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="截止日期" Width="65px"/>--%>
         <asp:Label ID="Label11" runat="server" Text="截止日期" Font-Size="8pt"></asp:Label>
     </td>
     <td class="BasicFormHeadDetail">
        <%--<cc1:DSCLabel ID="DSCLabel1jzrq" runat="server" Width="212px" />--%>
         <asp:Label ID="DSCLabel1jzrq" runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

     <tr>
      <td class="BasicFormHeadHead"  style=" text-align:right;">
 <%--    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="起始時間" Width="65px"/>--%>
          <asp:Label ID="Label12" runat="server" Text="起始時間" Font-Size="8pt"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" >
          <%--<cc1:DSCLabel ID="DSCLabelqstime" runat="server" Width="187px" />--%>
         <asp:Label ID="DSCLabelqstime" runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style=" text-align:right;">

    <%-- <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="截止時間" Width="65px"/>--%>
     <asp:Label ID="Label13" runat="server" Text="截止時間" Font-Size="8pt" 
             Font-Overline="False"></asp:Label>
     </td>
     <td  class="BasicFormHeadDetail" style=" height:20px;">
       <%-- <cc1:DSCLabel ID="DSCLabelJZRQ" runat="server" Width="215px" />--%>
         <asp:Label ID="DSCLabelJZRQ" runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

    <tr style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
  <%--   <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="請假時數" Width="65px"/>--%>
          <asp:Label ID="Label14" runat="server" Text="請假時數" Font-Size="8pt" 
              Font-Overline="False"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" style=" height:20px;">
         <%-- <cc1:DSCLabel ID="DSCLabelQJSS" runat="server" Width="183px" />--%>
         <asp:Label ID="DSCLabelQJSS" runat="server" Font-Size="8pt"></asp:Label>
     </td>

     <td class="BasicFormHeadHead"  style="text-align:right;">
    <%-- <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="請假人員職稱" Width="90px"/>--%>
         <asp:Label ID="Label15" runat="server" Text="請假人員職稱" Font-Size="8pt"></asp:Label>
     </td>
     <td  class="BasicFormHeadDetail" style=" height:20px;">
       <%-- <cc1:DSCLabel ID="DSCLabelDtName" runat="server" Width="207px" Height="16px" />--%>
         <asp:Label ID="DSCLabelDtName" runat="server" Font-Size="8pt"></asp:Label>
     </td>
    </tr>

    <tr>
      <td class="BasicFormHeadHead"  style="text-align:right;">
    <%-- <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="說明" Width="45px" Font-Bold="True" 
              Font-Size="XX-Large"/>--%>
          <asp:Label ID="Label16" runat="server" Text="說明" Font-Size="8pt"></asp:Label>
     </td>

     <td  class="BasicFormHeadDetail" style=" height:20px;" colspan="3" >
       <%--   <cc1:SingleField ID="SingleFieldSM" runat="server" Height="56px" 
              MultiLine="True" Width="532px" />--%>
         <asp:TextBox ID="SingleFieldSM" runat="server" TextMode="MultiLine" 
             Width="518px" Height="70px" Font-Size="8pt"></asp:TextBox>
             
     </td>

    </tr>


    <tr  style=" height:20px;">
      <td class="BasicFormHeadHead"  style="text-align:right;">
     <%--<cc1:DSCLabel ID="DSCLabel18" runat="server" Text="審核人一" Width="65px"/>--%>
          <asp:Label ID="Label17" runat="server" Text="審核人一" Font-Size="8pt"></asp:Label>
          
     </td>

     <td  class="BasicFormHeadDetail" style=" height:20px;" >
          <%--<cc1:DSCLabel ID="DSCLabelSHRY" runat="server" Width="100px" />--%>
         <asp:Label ID="DSCLabelSHRY"  runat="server" Font-Size="8pt"></asp:Label>
         &nbsp;
       <%--   <asp:Image ID="Image2" runat="server" 
             ImageUrl="~/SmpProgram/Form/STHR003DY/123.gif" />--%>

     </td>

     <td class="BasicFormHeadHead"  style="text-align:right;">
   <%--  <cc1:DSCLabel ID="DSCLabel20" runat="server" Text="審核人二" Width="84px"/>--%>
   <asp:Label   ID="Label18" runat="server" Text="審核人二" Font-Size="8pt"></asp:Label>
     </td>
     <td class="BasicFormHeadDetail" style=" height:20px;" >&nbsp;

     <%--   <cc1:DSCLabel ID="DSCLabelSHRER" runat="server" Width="100px" />--%>
         <asp:Label ID="DSCLabelSHRER"  runat="server" Font-Size="8pt"></asp:Label>
       <%--    <asp:Image ID="Image4" runat="server" 
             ImageUrl="~/SmpProgram/Form/STHR003DY/123.gif" />--%>
     </td>
    </tr>

     <tr style=" height:20px;">


     <td class="BasicFormHeadDetail" colspan="4" >
         <%-- <cc1:DSCLabel ID="DSCLabelBZ" runat="server" Width="570px" />--%>
         <asp:Label ID="DSCLabelBZ" runat="server" Font-Size="8pt"></asp:Label>
         
     </td>

    
    </tr>

    </table>

    <br />
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
        <br />

    </div>

  
   
    </form>
</body>
</html>
