<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="Program_SCQ_Form_EH0109_PrintPage" %>
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

    .style1
    {
        border-left: 1px none rgb(188,178,147);
        border-right: 1px solid rgb(188,178,147);
        border-top: 1px none rgb(188,178,147);
        border-bottom: 1px solid rgb(188,178,147);
        background-color: white;
        font-size: medium;
        font-family: Arial;
        height: 20px;
    }

    .style2
    {
        border-left: 1px none rgb(188,178,147);
        border-right: 1px solid rgb(188,178,147);
        border-top: 1px none rgb(188,178,147);
        border-bottom: 1px solid rgb(188,178,147);
        background-color: rgb(225,217,196);
        font-size: 9pt;
        font-family: Arial;
        width: 105px;
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
<br />
<br />
	 <table style="margin-left:4px; width: 700px;" border="0" cellspacing="0" cellpadding="1">
     <tr>
      <td align="center" height="30" style="width: 700px">
            <font style="font-family: 標楷體; font-size: large;"><b>重慶貽百電子有限公司</b></font></td>
     </tr>
     <tr><td align="center" height="30" style="width: 670px">
            <font style="font-family: 標楷體; font-weight: 700;" class="style1">人員異動申請單</font></td></tr>
     </table>
     <table style="margin-left:4px; width: 700px;" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="工號" Width="68px" />
             </td>
             <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="198px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="到職日期" Width="72px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="ComeDate" runat="server" Width="126px" 
                        Height="16px" />
                </td>
                 <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="年資" Width="72px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="NZ" runat="server" Width="103px" Height="16px" 
                        ReadOnly="True" />
                </td>
        </tr>
     </table>
      <table style="margin-left:4px; width: 700px;" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td width="100px" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="調任類型" Width="100px" />                    
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server" />
                    職務異動</td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox2" runat="server" />
                    薪資異動</td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox3" runat="server" />
                    部門異動
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox4" runat="server" />
                    其他
                </td>
            </tr>
        </table>
        <table style="margin-left:4px; width: 681px;" class="BasicFormHeadBorder" 
            border="0" cellspacing="0" cellpadding="1">
           <tr>
                <td width="100px" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="調任理由及考核評語" Width="150px" />                     
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                                        
                    <cc1:SingleField ID="py" runat="server" Height="70px" Width="544px" 
                        MultiLine="True" />                   
                </td>  
           </tr>
          
        </table>

        <table style="margin-left:4px; width: 700px;" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td rowspan="11" class="BasicFormHeadHead" style="text-align:center; width:33px">
                    異<br />
                    動<br />
                    前</td>
                <td class="BasicFormHeadDetail" style="width: 75px;">
                    部門
                </td>
                 <td colspan="2" class="BasicFormHeadDetail" style="width: 70px; height: 18px">
                    <cc1:SingleField ID="OPartNo" runat="server" Width="150px" />
                </td>
                <td rowspan="11" class="BasicFormHeadHead" style="text-align:center; width:33px">
                    異<br />
                    動<br />
                    后</td>
                 <td class="BasicFormHeadDetail" style="width: 75px;">
                    部門</td>
                <td colspan="2"class="BasicFormHeadDetail" style="width: 70px; height: 18px">
                    <cc1:SingleField ID="NPartNo" runat="server" Width="150px" />
                </td>
                
            </tr>
            <tr>
                <td class="BasicFormHeadDetail" style="width: 75px;">
                    職位 
                </td>
                <td colspan="2" class="BasicFormHeadDetail" style="width: 70px; height: 18px">
                    <cc1:SingleField ID="ODtName" runat="server" Width="150px" /> 
                </td>
                <td class="BasicFormHeadDetail" style="width: 75px;">
                    職位</td>
                <td colspan="2"class="BasicFormHeadDetail" style="width: 70px; height: 18px">
                    <cc1:SingleField ID="NDtName" runat="server" Width="150px" />
                </td>
            </tr>
             <tr>
                <td class="BasicFormHeadDetail" style="width: 75px;">
                    職等</td>
               <td colspan="2" class="BasicFormHeadDetail" style="width: 70px; height: 18px">
                    <cc1:SingleField ID="ODt" runat="server" Width="150px" /> 
                </td>
                <td class="BasicFormHeadDetail" style="width: 75px;">
                    職等</td>
               <td colspan="2"class="BasicFormHeadDetail" style="width: 70px; height: 18px">
                    <cc1:SingleField ID="NDt" runat="server" Width="150px" />
                </td>
            </tr>
           
            <tr>
                <td rowspan="7" class="BasicFormHeadDetail" style="width: 75px;">
                    薪資明細
                </td>
                <td class="BasicFormHeadDetail" style="width: 80px;">
                    本薪
                </td>
                <td class="BasicFormHeadDetail" style="width: 70px;">
                    <cc1:SingleField ID="Obx" runat="server" Width="60px" />
                </td>
                
                <td rowspan="7" class="BasicFormHeadDetail" style="width: 75px;">
                    薪資明細 
                </td>
                <td class="BasicFormHeadDetail" style="width: 80px;">
                    本薪
                </td>
                <td class="BasicFormHeadDetail" style="width: 70px;">
                    <cc1:SingleField ID="Nbx" runat="server" Width="60px" />
                </td>
            </tr>
            <tr>
               <td class="BasicFormHeadDetail" style="width: 80px;">
                   主管加給</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Ozgjg" runat="server" Width="60px" />
                </td>
                 <td class="BasicFormHeadDetail" style="width: 80px;">
                   主管加給</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Nzgjg" runat="server" Width="60px" />
                </td>
            </tr>
            <tr>
               <td class="BasicFormHeadDetail" style="width: 80px;">
                   職務加給</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Ozwjg" runat="server" Width="60px" />
                </td>
                 <td class="BasicFormHeadDetail" style="width: 80px;">
                   職務加給</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Nzwjg" runat="server" Width="60px" />
                </td>
            </tr>
            <tr>
               <td class="BasicFormHeadDetail" style="width: 80px;">
                   專業加給</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Ozyjg" runat="server" Width="60px" />
                </td>
                 <td class="BasicFormHeadDetail" style="width: 80px;">
                   專業加給</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Nzyjg" runat="server" Width="60px" />
                </td>
            </tr>
            <tr>
               <td class="BasicFormHeadDetail" style="width: 80px;">
                   加班補貼</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Ojbbt" runat="server" Width="60px" />
                </td>
                 <td class="BasicFormHeadDetail" style="width: 80px;">
                   加班補貼</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Njbbt" runat="server" Width="60px" />
                </td>
            </tr>
            <tr>
               <td class="BasicFormHeadDetail" style="width: 80px;">
                   全薪</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Oqx" runat="server" Width="60px" />
                </td>
                 <td class="BasicFormHeadDetail" style="width: 80px;">
                   全薪</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Nqx" runat="server" Width="60px" />
                </td>
            </tr>
            <tr>
               <td class="BasicFormHeadDetail" style="width: 80px;">
                   加班費基數</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Ojs" runat="server" Width="60px" />
                </td>
                 <td class="BasicFormHeadDetail" style="width: 80px;">
                   加班費基數</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Njs" runat="server" Width="60px" />
                </td>
            </tr>
            </table>
            <table style="margin-left:4px; width: 700px;" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
            <tr>
               
                 <td class="BasicFormHeadHead" style="width: 90px;">
                主要<br />
                     工作
                </td>
                <td class="style17">
                    <cc1:SingleField ID="Owork" runat="server" Height="55px" 
                        MultiLine="True" Width="305px" />
                </td>
                
                <td class="BasicFormHeadHead" style="width: 90px;">
                主要<br />
                    工作
                </td>
                <td class="style17">
                    <cc1:SingleField ID="Nwork" runat="server" Height="55px" 
                        MultiLine="True" Width="306px" />
                </td>
            </tr>
            </table>
            <table style="margin-left:4px; width: 700px;" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td rowspan="2" class="BasicFormHeadHead" style="width: 71px;text-align: center;">
                    獎懲記錄
                </td>
                <td class="BasicFormHeadHead" style="width: 75px;text-align: center;">
                    獎勵:   
                </td>
                <td class="BasicFormHeadDetail" style="width: 80px;">
                    大功  
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="jdg" runat="server" Width="54px" />
                    次</td>
                <td class="BasicFormHeadDetail" style="width: 70px;">
                    小功 
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="jxg" runat="server" Width="54px" />
                    次</td>
                <td class="BasicFormHeadDetail" style="width: 70px;">
                    嘉獎
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="jj" runat="server" Width="54px" />
                    次</td>
                <td rowspan="3" class="BasicFormHeadHead" style="width: 66px;text-align: center;">近一年</td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" style="width: 75px;text-align: center;">
                    處罰:
                </td>
                <td class="BasicFormHeadDetail" style="width: 80px;">
                    大過
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="fdg" runat="server" Width="54px" />
                    次</td>
                
                <td class="BasicFormHeadDetail" style="width: 70px;">
                    小過
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="fxg" runat="server" Width="54px" />
                    次</td>
                <td class="BasicFormHeadDetail" style="width: 70px;">
                    申誡
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="sj" runat="server" Width="54px" />
                    次</td>
            </tr>

            <tr>
                <td colspan="2" class="BasicFormHeadHead" style="width: 71px;text-align: center;">
                    假勤記錄:
                </td>
                 <td class="BasicFormHeadDetail" style="width: 80px;">
                    事假
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="SingleField1" runat="server" Width="54px" />
                    H</td>
                
                <td class="BasicFormHeadDetail" style="width: 70px;">
                    病假
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="SingleField2" runat="server" Width="54px" />
                    H</td>
                <td class="BasicFormHeadDetail" style="width: 70px;">
                    曠工
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="SingleField4" runat="server" Width="54px" />
                    H</td>
            </tr>
        </table>
         <table style="margin-left:4px; width: 700px;" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead">
                    考核記錄
                </td>
                <td class="BasicFormHeadDetail">
                    年中考核
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Mkh" runat="server" Width="68px" 
                        style="margin-left: 0px" />
                </td>
                <td class="BasicFormHeadDetail">
                    年終考核
                </td>
                <td class="BasicFormHeadDetail"><cc1:SingleField ID="Ekh" runat="server" 
                        Width="61px" /></td>
                <td class="BasicFormHeadHead">
                    最近考核
                </td>
            </tr>
         </table>
         <table style="margin-left:4px; width: 700px;" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 70px;">
                    調薪記錄:
                </td>
                <td class="BasicFormHeadDetail" style=" width: 280px";>
                    
                    <cc1:SingleField ID="movelist" runat="server" Height="78px" 
                        MultiLine="True" Width="453px" />
                    
                </td>
                <td colspan="2" class="BasicFormHeadDetail" style=" width: 200px";>
                    員工本人簽字:
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
    </div>
    </form>
</body>
</html>
