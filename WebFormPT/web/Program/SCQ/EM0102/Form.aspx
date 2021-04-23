<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0102_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=800px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工" Width="100%"></cc1:DSCLabel></td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" valueIndex="1" idIndex="0" />
            </td>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBDepartment" runat="server" Text="部門" Width="100%"></cc1:DSCLabel>
            </td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="Department" runat="server" Width="200px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBAccount" runat="server" Text="登入帳號" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="Account" runat="server" Width="120px" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBPrilege" runat="server" Text="權限" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <Panel ID="CheckPanel" runat="server">
                    <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server" Text="讀取"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox2" runat="server" Text="新增"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox3" runat="server" Text="刪除"></cc1:DSCCheckBox>
                </Panel>
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBServer" runat="server" Text="Server" Width="100%"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="ServerIP" runat="server" Width="120px" ReadOnly="True" 
                    ValueText="10.3.11.38" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBSDate" runat="server" Text="起始日期" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="SDate" runat="server" 
                    Width="120px" />  
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBEDate" runat="server" Text="結束日期" Width="100%"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="EDate" runat="server" 
                    Width="120px" />  
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBReason" runat="server" Text="申請理由" Width="100%"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
