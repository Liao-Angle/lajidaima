<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH0111_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>人員異動申請單</title>
    <style type="text/css">
        .style1
        {
            width: 164px;
        }
        .style17
        {
            width: 263px;
        }
        .style26
        {
            width: 269px;
        }
        .style27
        {
            width: 166px;
        }
        .style28
        {
            width: 134px;
        }
        .BasicFormHeadHead
        {
            text-align: center;
        }
        </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
     <table border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr>
			<td colSpan="5" class="BasicFormHeadHead" Width="100%"><b>				
				<cc1:GlassButton ID="PrintButton1" runat="server" Height="20px" Width="300" 
                    Text="列印異動單" onbeforeclicks="PrintButton_OnClick" Enabled="True" /></b>
			</td>
		</tr>
	</table>
    <div>
       <cc1:SingleField id="SheetNo" runat="server" Display="False"></cc1:SingleField>
       <cc1:SingleField id="Subject" runat="server" Display="False"></cc1:SingleField>
       <table style="margin-left:4px; width: 700px;" border="0" cellspacing="0" cellpadding="1">
     <tr>
      <td align="center" height="30" style="width: 700px">
            <font style="font-family: 標楷體; font-size: large;"><b>新普科技（重慶）有限公司</b></font></td>
     </tr>
     <tr><td align="center" height="30" style="width: 670px">
            <font style="font-family: 標楷體; font-weight: 700;" class="style1">二廠人員異動申請單</font></td></tr>
     </table>
     <table style="margin-left:4px; width: 700px;" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="工號" Width="68px" />
             </td>
             <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="198px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" 
                        onsingleopenwindowbuttonclick="JEmpNo_SingleOpenWindowButtonClick" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="到職日期" Width="72px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="ComeDate" runat="server" Width="168px" Height="16px" />
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
        
        <table style="margin-left:4px; width: 700px; display:none" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td width="100px" class="BasicFormHeadHead">
                   親屬關系               
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="qinshu" runat="server" Width="60px" 
                        Font-Size="Large" onselectchanged="sxj_SelectChanged" 
                        style="font-weight: 700" />
                </td>
                <td width="100px" class="BasicFormHeadHead" style="width: 110px">
                   上下級關係               
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="sxj" runat="server" Width="47px" Font-Size="Large" 
                        onselectchanged="sxj_SelectChanged" style="font-weight: 700" />
                </td>
                 <td width="100px" class="BasicFormHeadHead">
                   工號               
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="qempno" runat="server" Width="90px" 
                        ontextchanged="qempno_TextChanged" />
                </td>
                 <td width="100px" class="BasicFormHeadHead">
                   姓名               
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="qempname" runat="server" Width="90px" />
                </td>
                 <td width="100px" class="BasicFormHeadHead">
                   職務               
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="qdtname" runat="server" Width="90px" />
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
                    <cc1:SingleDropDownList ID="NPartNo" runat="server" Width="150px" />
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
                    <cc1:SingleDropDownList ID="NDtName" runat="server" Width="150px" />
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
                    <cc1:SingleDropDownList ID="NDt" runat="server" Width="150px" 
                        onselectchanged="Dt_SelectChanged" />
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
                    <cc1:SingleDropDownList ID="Nbx" runat="server" Width="60px" />
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
                       <cc1:SingleDropDownList ID="Nzgjg" runat="server" Width="60px" />
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
                       <cc1:SingleDropDownList ID="Nzwjg" runat="server" Width="60px" />
                </td>
            </tr>
            <tr>
               <td class="BasicFormHeadDetail" style="width: 80px;">
                   專業加給</td>
                   <td class="BasicFormHeadDetail" style="width: 70px;">
                       <cc1:SingleField ID="Ozyjg" runat="server" Width="60px" />
                </td>
                 <td class="BasicFormHeadDetail" style="width: 80px;">
                   <cc1:GlassButton ID="zy" runat="server" Text="專業加給" Height="16px" Width="69px" 
                         onclick="zy_Click" /></td>
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
               
                <td rowspan="2" class="BasicFormHeadHead" style="width: 13px;text-align: center;">
                主要工作
                </td>
                <td rowspan="2" class="style17">
                    <cc1:SingleField ID="Owork" runat="server" Height="55px" 
                        MultiLine="True" Width="295px" />
                </td>
                <td  class="BasicFormHeadHead" style="width: 70px;text-align: center;" >
                <cc1:GlassButton ID="xz" runat="server" Text="薪資解密" Height="16px" Width="69px" 
                        onclick="xz_Click1" />
                </td>
                <td rowspan="2" class="BasicFormHeadHead" style="width: 13px;text-align: center;">
                主要工作
                </td>
                <td rowspan="2" class="style17">
                    <cc1:SingleField ID="Nwork" runat="server" Height="55px" 
                        MultiLine="True" Width="295px" />
                </td>
            </tr>
            <tr>
                <td  class="BasicFormHeadHead" style="width: 70px;text-align: center;" >
                    <cc1:SingleField ID="SingleField3" runat="server" Width="67px" 
                        isPassword="True" />
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
                <td class="style26" ;>
                    
                    <cc1:SingleField ID="movelist" runat="server" Height="78px" 
                        MultiLine="True" Width="389px" style="text-align: left" />
                    
                </td>
                <td colspan="2" class="BasicFormHeadDetail" style=" width: 230px";>
                    員工本人簽字:
                </td>
            </tr>
         </table>
         <table style="margin-left:4px; width: 700px;" class="BasicFormHeadBorder" border="0" cellspacing="0" cellpadding="1">
            <panel id="HRXZ" runat="server" visible="false">
            <tr>
                <td class="BasicFormHeadHead">
                    調薪類型
                </td>
                <td class="BasicFormHeadDetail">
                    
                    <cc1:SingleDropDownList ID="txlx" runat="server" 
                        style="margin-left: 0px" Width="142px" />
                    
                </td>
                <td class="BasicFormHeadHead">
                    生效時間
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="Etime" runat="server" style="margin-left: 0px" 
                        Width="151px"></cc1:SingleDateTimeField>
                </td>
            </tr>
            </panel>
         </table>

    </div>
    </form>
</body>
</html>
