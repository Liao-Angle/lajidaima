<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH010101_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>出勤修正單</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>

     <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>  
        <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
        <cc1:SingleField ID="Subject" runat="server" Display="False" />          
        <tr valign=middle > 
		    <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>出勤修正單</b></font></td>
	    </tr>
	</table>

        <table style="margin-left:4px" border=0 width=680px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工工號" Width="100%"></cc1:DSCLabel></td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" 
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" valueIndex="1" idIndex="0" 
                    onsingleopenwindowbuttonclick="EmpNo_SingleOpenWindowButtonClick" />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBHRModifyID" runat="server" Text="修正類別" Width="100%"></cc1:DSCLabel></td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="HRModifyID" 
                    runat="server" Width="110px" /> 
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBDate" runat="server" Text="修正日期" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="WorkDate" runat="server" 
                                    Width="120px" ondatetimeclick="WorkDate_DateTimeClick" /> (當日無法修正) 
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBShift" runat="server" Text="修正班別" Width="100%"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="Shift" 
                    runat="server" Width="110px" /> 
            </td>
        </tr>
 <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel  runat="server"  Text="第一筆" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                               
                <cc1:DSCLabel  runat="server"  id="strset" Width="100%"></cc1:DSCLabel>
                               
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel runat="server" Text="最後一筆" Width="100%"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail>

                <cc1:DSCLabel  runat="server" Width="100%" ID="endset"></cc1:DSCLabel>

            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBStime" runat="server" Text="上班" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="STime" runat="server" 
                                    Width="120px" DateTimeMode="4" />  
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBEtime" runat="server" Text="下班" Width="100%" />
                </td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="ETime" runat="server" 
                                    Width="120px" DateTimeMode="4" />  
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBReason" runat="server" Text="修正事由" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="Reason" 
                    runat="server" Width="225px" /> 
            </td>
            <td  colspan=2 valign=top align=center class=BasicFormHeadHead>

                            <cc1:GlassButton ID="CheakData" runat="server" BackColor="White"
                             OnClick="CheakData_Click"  Height="24px" Text="查看三輥閘數據" Width="159px"
                                 />
            </td>
        </tr>
         <tr runat="server" id="showzg" visible="false">
            <td class="BasicFormHeadHead" colspan="3" style="height: 17px">
          <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="請選擇簽核主管" Width="150px" Height="20px"></cc1:DSCLabel>
             </td>
              <td class="BasicFormHeadHead" colspan="3" style="height: 17px">
                  <cc1:SingleDropDownList ID="sqzszg" runat="server" Width="111px" />
             </td>
        </tr> 
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBStatus" runat="server" Text="年度狀態" Width="100%"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>


                <cc1:DSCLabel ID="Status" runat="server" Text="該員工本月已修正{0}次，累計已修正{1}次。" 
                    Width="100%"></cc1:DSCLabel>
            </td>
        </tr>
        </table>
        <%--<table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" 
            style="width: 680px;">
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:OutDataList ID="OutDataListJC" runat="server" Height="144px" Width="652px" 
                        onsaverowdata="OutDataListJC_SaveRowData" NoAllDelete="True" 
                        NoModify="True" />
                        <cc1:FileUpload ID="FileUpload1" runat="server" NoDelete="True" 
                             onaddoutline="FileUpload1_AddOutline" Width="20px" 
                     BorderStyle="None" ViewStateMode="Disabled" Height="144px" /></span>
                </td>
            </tr>
        </table>--%>
    </div>
    </form>
</body>
</html>
