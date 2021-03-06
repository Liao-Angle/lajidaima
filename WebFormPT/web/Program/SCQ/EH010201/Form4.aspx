<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form4.aspx.cs" Inherits="Program_SCQ_Form_EH010201_Form" %>
 
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
         
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>加班申請單</title>
    <style type="text/css">
        #Excelin
        {
            width: 24px;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>  
        <cc1:SingleField ID="SheetNo" runat="server" />
        <cc1:SingleField ID="Subject" runat="server"  /><tr valign=middle > 
		    <td align=center style="height: 40px"><font style="font-family: 標楷體; font-size: large;"><b>加班申請單</b></font></td>
	    </tr>
	</table>
    <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>    
        <tr id="Rows" runat="server">
            <td colspan=4 style="width: 669px; height: 70px; margin-left: 40px;">
            <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="70px" Width="667px">
                <TabPages>
<cc1:DSCTabPage runat="server" Title="个人" Enabled="True" ImageURL="" Hidden="False" ID="DSCTabPage1"><TabBody>
<TABLE style="MARGIN-LEFT: 0px" class="BasicFormHeadBorder" cellSpacing=0 cellPadding=1 width=663 border=0><TBODY>
        <TR><TD style="WIDTH: 100px" class="BasicFormHeadHead" align=right><cc1:DSCLabel id="LBEmpNo" runat="server" Width="100%" Text="員工"></cc1:DSCLabel></TD>
            <TD class="BasicFormHeadDetail" width=220>&nbsp;<cc1:SingleOpenWindowField id="EmpNo" runat="server" Width="200px" idIndex="0" valueIndex="1" tableName="hrusers" serialNum="001" keyField="EmpNo" guidField="EmpNo" showReadOnlyField="True" __designer:wfdid="w1"></cc1:SingleOpenWindowField></TD></TR></TBODY></TABLE>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" Title="部门" Enabled="True" ImageURL="" Hidden="False" ID="DSCTabPage2"><TabBody>
                            <div style="text-align:center">
                            <cc1:SingleDropDownList ID="SingleDropDownList1" runat="server" />
                        </div>
                        
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" Title="线别" Enabled="True" ImageURL="" Hidden="False" ID="DSCTabPage3"><TabBody>
                             <div style="text-align:center">
                            <cc1:SingleDropDownList ID="SingleDropDownList2" runat="server" /></div>
                        
</TabBody>
</cc1:DSCTabPage>
</TabPages>
                </cc1:DSCTabControl>
            </td>
        </tr>
              <tr  id="Rows1" runat=server>
            <td width="80" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBWorkDate" runat="server" Text="加班日期" Width="100%"></cc1:DSCLabel></td>
            <td width="220" class=BasicFormHeadDetail >
                                <cc1:SingleDateTimeField ID="WorkDate" runat="server" 
                                    Width="120px" />
             </td>
        </tr>
        <tr  id="Rows2" runat=server>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBSTime" runat="server" Text="開始時間" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>  
                                <cc1:SingleDropDownList ID="STime1" runat="server" Width="60px" OnSelectChanged="STime1_SelectChanged" /><cc1:SingleDropDownList ID="STime3" runat="server" Width="60px" OnSelectChanged="STime1_SelectChanged" />
                                </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBETime" runat="server" Text="結束時間" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="width: 214px">  
                                <cc1:SingleDropDownList ID="ETime1" 
                                    runat="server" Width="80px" OnSelectChanged="STime1_SelectChanged" />
                                <cc1:SingleDropDownList ID="ETime3" 
                                    runat="server" Width="80px" OnSelectChanged="STime1_SelectChanged" />
                                </td>
        </tr>
        <tr  id="Rows3" runat=server>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBHours" runat="server" Text="時數" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="Hours" runat="server" Width="100px" ReadOnly="True" />
            </td>
        </tr>
        <tr  id="Rows4" runat=server>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBReason" runat="server" Text="加班原因" Width="100%"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr  id="Rows5" runat=server>
            <td valign=top align=right class=BasicFormHeadHead style="height: 68px">
                <cc1:DSCLabel ID="LBNote" runat="server" Text="備註" Width="100%"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail style="height: 68px">
                <cc1:SingleField ID="Note" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
                    </td>
                    </tr>
        <tr  id="Rows6" runat=server>
            <td colspan=4 class=BasicFormHeadDetail>
                &nbsp;</td>
        </tr>
        
        
        <tr>
             <td class=BasicFormHeadDetail colspan=4  valign=top>
                 &nbsp;<cc1:OutDataList 
                     id="OverTimeList" runat="server" Height="250px" 
                     OnSaveRowData="RequestList_SaveRowData" ViewStateMode="Disabled" Width="631px" 
                     showExcel="True" NoModify="True" IsExcelWithMultiType="True" 
                     showTotalRowCount="True"></cc1:OutDataList>

                      <span style="margin-top:0px; padding-top:0px"> 
                 <cc1:FileUpload ID="FileUpload1" runat="server" NoDelete="True" 
                             onaddoutline="FileUpload1_AddOutline" Width="18px" 
                     BorderStyle="None" ViewStateMode="Disabled" Height="230px" /></span>  

                      
                   
            </td>
        </tr>      
    </table>
    </div>
    </form>
</body>
</html>
