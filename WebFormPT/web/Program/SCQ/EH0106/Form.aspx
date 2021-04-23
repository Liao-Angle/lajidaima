<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH0106_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>薪資差異申請單</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
         <table style="margin-left:4px; width: 800px;" border=0 cellspacing=0 
             cellpadding=1>  
        <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
        <cc1:SingleField ID="Subject" runat="server" Display="False" />          
        <tr valign=middle > 
		    <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>薪資差異申請單</b></font></td>
	    </tr>
	</table>
            <table width=800px border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
         <tr>
            <td colspan=4 align="left" class="BasicFormHeadHead">
                <label style="font-size:10px;font-weight:bold;">填單人</label></td>
        </tr>

         <tr>
            <td colspan=4 class="BasicFormHeadHead">
              <table width="100%">
                <tr align="center">
                    <td width="12%" align=right class=BasicFormHeadHead>申請人</td>
                    <td width="30%" align=right class=BasicFormHeadHead>
                <cc1:SingleOpenWindowField ID="User" runat="server"
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" 
                    valueIndex="1" idIndex="0" Width="180px"/>
                    </td>
                     <td  align=right class=BasicFormHeadHead>申請人分機</td>
                    <td  align=right class=BasicFormHeadHead>

            <cc1:SingleField ID="mobileuser" runat="server" Width="119px" />

                    </td>
                     <td  align=right class=BasicFormHeadHead>申請人部門</td>
                    <td  align=right class=BasicFormHeadHead>
     
            <cc1:SingleField ID="partNouser" runat="server" Width="119px" />
     
                    </td>
                </tr>
              </table>
            </td>
        </tr>
        <tr>
            <td  align=right class=BasicFormHeadHead>責任人:</td>
             <td  colspan="3"  align=right class=BasicFormHeadHead> 
                 <cc1:SingleDropDownList ID="zzr" runat="server" Width="111px" />
            </td>
        </tr>
        <tr>
            <td  align=right class=BasicFormHeadHead>差異說明：</td>
            <td colspan="3">
                <cc1:SingleField ID="CYSM" runat="server" Height="92px" Width="703px" MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td  align=right class=BasicFormHeadHead>備注說明：
            </td>
            <td colspan="3">
            <cc1:SingleField ID="BZSM" runat="server" Height="92px" Width="703px" MultiLine="True" />
            </td>
        </tr>
        <tr id="User1S" runat=server>
            <td colspan=4 align="left" class="BasicFormHeadHead">
                <label style="font-size:10px;font-weight:bold;">出勤，加班，類異常請務必填寫如下信息</label>
            </td>
        </tr>
        <tr id="User2S" runat=server>
            <td  align=right style=" width:20%" class=BasicFormHeadHead>員工：</td>
            <td  align=right style=" width:30%" class=BasicFormHeadHead>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" showReadOnlyField="True" 
                    guidField="EmpNo" keyField="EmpNo" serialNum="001"  tableName="hrusers"  
                    valueIndex="1" idIndex="0" Width="180px" />
            </td>
            <td align=right style=" width:20%"  class=BasicFormHeadHead>加班日期：</td>
            <td  align=right style=" width:30%" class=BasicFormHeadHead>
                <cc1:SingleDateTimeField ID="WorkDate" runat="server" Width="120px" 
                    ondatetimeclick="WorkDate_DateTimeClick" />
            </td>
        </tr>
        <tr  id="User3S" runat=server>
            <td align=right class=BasicFormHeadHead >
                開始時間：
            </td>
            <td align=right class=BasicFormHeadHead>  
                                <cc1:SingleDropDownList ID="STime" runat="server" Width="110px" 
                                    onselectchanged="STime1_SelectChanged"  />
            </td>
            <td align=right class=BasicFormHeadHead>
                結束時間：
            </td>
            <td align=right class=BasicFormHeadHead>  
                                <cc1:SingleDropDownList ID="ETime" runat="server" Width="110px" 
                                    onselectchanged="STime1_SelectChanged" />
            </td>
        </tr>

        <tr id="User4S" runat=server>
            <td align=right class=BasicFormHeadHead>班別： </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:SingleDropDownList ID="BB" runat="server" Width="110px" />
            </td>
            <td align=right class=BasicFormHeadHead>異常時數：</td>
            <td align=right class=BasicFormHeadHead>
            <cc1:SingleField ID="YCTime" runat="server" Width="119px" />
            </td>
        </tr>
        <tr id="User5S" runat=server>
            <td  align=right class=BasicFormHeadHead>異常類型：</td>
            <td  align=right class=BasicFormHeadHead>
           <cc1:SingleDropDownList ID="YCLX" runat="server" Width="111px" />
            </td>
            
            
        </tr>
        
        <tr>
        <td  align=right class=BasicFormHeadHead>查看三輥閘
            </td>
        <td  align=right class=BasicFormHeadHead>
               <cc1:GlassButton ID="CheakData" runat="server" BackColor="White"
                             OnClick="CheakData_Click"  Height="24px" Text="查看三輥閘數據" Width="159px"/>
            </td>
        </tr>
         <tr>
            <td colspan=4 align="left" class="BasicFormHeadHead">
                <label style="font-size:10px;font-weight:bold;">異常信息：</label></td>
        </tr>
        <tr>
            <td  colspan="4">
                    <cc1:OutDataList 
                     id="LeaveDateList" runat="server" Height="250px" 
                      ViewStateMode="Disabled" Width="100%" NoModify="True" IsExcelWithMultiType="True" 
                     showTotalRowCount="True" NoAllDelete="True" 
                        onsaverowdata="LeaveDateList_SaveRowData"></cc1:OutDataList>
            </td>
        </tr>



         <tr runat="server" id="showzg" visible="false">
            <td class="BasicFormHeadHead"  style="height: 17px">
                請選擇簽核主管<label style="font-weight:bold;">：</label></td>
              <td  class="BasicFormHeadHead" >
                  <cc1:SingleDropDownList ID="sqzszg" runat="server" Width="111px" />
             </td>
        </tr>
</table>
    </div>
    </form>
</body>
</html>
