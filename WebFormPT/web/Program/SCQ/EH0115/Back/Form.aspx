<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH0115_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>銷假申請單</title>
    </head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
        <cc1:SingleField ID="Subject" runat="server" Display="False" />
        <table style="margin-left: 4px; width: 800px;" border="0" cellspacing="0" cellpadding="1">
            <tr valign="middle">
                <td align="center" height="40">
                    <font style="font-family: 標楷體; font-size: large;"><b>銷假申請單</b></font>
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px" border="0" width="800px" cellspacing="0" cellpadding="1"
            class="BasicFormHeadBorder">
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工工號" Width="100%"></cc1:DSCLabel>
                </td>
                <td width="240px" class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="240px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" 
                        OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBDepartment" runat="server" Text="部門" Width="100%" Style="font-size: x-small">
                    </cc1:DSCLabel>
                </td>
                <td width="164px" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Department" runat="server" Width="164px" ReadOnly="True" Height="24px" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBDtName" runat="server" Text="職稱" Width="100%" Style="font-size: x-small"
                        Height="16px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="DtName" runat="server" Width="195px" ReadOnly="True" 
                        Height="19px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBLeaveType" runat="server" Text="銷假類別" Width="100%" Style="font-size: x-small">
                    </cc1:DSCLabel>
                </td>
                <td width="240px" class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="LeaveType" runat="server" Width="160px" Height="16px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" rowspan="2">
                    <cc1:DSCLabel ID="LBLeaveDate" runat="server" Text="銷假時間" Width="100%" Height="50px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    日期：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <cc1:SingleDateTimeField ID="LeaveDate" runat="server" Width="119px" />
                </td>
                <td class="BasicFormHeadDetail">
                    開始時間：<cc1:SingleDropDownList 
                        ID="BeginTime" runat="server" Width="88px" />
                </td>
                <td class="BasicFormHeadDetail">
                     結束時間：<cc1:SingleDropDownList ID="EndTime" runat="server" 
                         Width="88px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    開始日期：<cc1:SingleDateTimeField ID="LeaveSDate" runat="server" Width="120px" />
                    
                </td>
                <td  colspan="1" class="BasicFormHeadDetail">
                   結束日期： <cc1:SingleDateTimeField ID="LeaveEDate" runat="server" Width="120px" />
                </td>
                <td  class="BasicFormHeadDetail">&nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBReason" runat="server" Text="銷假事由" Width="100%" Height="59px"
                        Style="font-size: x-small; text-align: center"></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Reason" runat="server" Width="99%" Height="64px" MultiLine="True" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBHrwh" runat="server" Text="HR維護單號" Width="102%" Height="59px"
                        Style="font-size: x-small; text-align: center"></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="BillNo" runat="server" Width="99%" Height="64px" MultiLine="True" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
