<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH010302_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .style1
        {
            width: 409px;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="100px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工" Width="100%"></cc1:DSCLabel></td>
            <td width="220px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" valueIndex="1" idIndex="0" />
            </td>
            <td width="100px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBLeaveTypeID" runat="server" Text="假別" Width="100%"></cc1:DSCLabel></td>
            <td width="220px" class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="LeaveTypeID" 
                    runat="server" Width="110px" /> 
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBDtName" runat="server" Text="職稱" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail><cc1:DSCLabel ID="DtName" runat="server" Text="" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBTryUseDate" runat="server" Text="年資" Width="100%" />
                </td>
            <td class=BasicFormHeadDetail><cc1:DSCLabel ID="TryUseDate" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBYearStatus" runat="server" Text="年度狀況" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan=3><cc1:DSCLabel ID="YearStatus" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBReason" runat="server" Text="請假事由" Width="100%"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead valign="top" rowspan=3>
                <cc1:DSCLabel ID="LBLeaveDate" runat="server" Text="請假時間" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                 <Panel ID="EditPanel" width="100%" runat="server">
                    <table width="100%">
                        <tr>
                            <td class=BasicFormHeadDetail>
                                <cc1:DSCCheckBox ID="check1" runat="server" />
                                <cc1:SingleDateTimeField ID="LeaveDate" runat="server" 
                                    Width="120px" />  
                                <cc1:SingleDropDownList ID="BeginTime" 
                                    runat="server" Width="80px" />
                                <cc1:SingleDropDownList ID="EndTime" 
                                    runat="server" Width="80px" />
                            </td>
                            <td class=BasicFormHeadDetail rowspan=2>
                                <cc1:GlassButton ID="AddData" runat="server" 
                                    ImageUrl="~\Images\AcceptCalibration.png" onclick="AddData_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class=BasicFormHeadDetail>
                                <cc1:DSCCheckBox ID="check2" runat="server" />
                                <cc1:SingleDateTimeField ID="LeaveSDate" runat="server" 
                                    Width="120px" />  
                                <cc1:SingleDateTimeField ID="LeaveEDate" runat="server" 
                                    Width="120px" />
                            </td>
                        </tr>
                    </table>
                 </Panel>
             </td>
        </tr>
        <tr>
             <td class=BasicFormHeadDetail colspan=3>
                <cc1:DataList ID="LeaveDateList" runat="server" Height="200px" Width="100%" 
                    isShowAll="True" NoModify="True" 
                    showTotalRowCount="True" NoAllDelete="True" NoAdd="True" 
                    onsetclickdata="LeaveDateList_setClickData" IsHideSelectAllButton="True" 
                    IsOpenWinUse="True" NoDelete="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
