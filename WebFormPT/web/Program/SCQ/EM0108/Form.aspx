<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0108_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <table style="margin-left:4px" border=0 width=800px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBTitle" runat="server" Text="新增員工信息" Width="100%"></cc1:DSCLabel></td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <Panel ID=EditPanel runat=Server>
                <table style="margin-left:4px" border=0 width=100% cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
                    <tr>
                        <td width="100px" align=right class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="申請人" Width="100%"></cc1:DSCLabel></td>
                        <td width="220px" class=BasicFormHeadDetail>
                            <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" 
                                showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick"
                                tableName="hrusers" valueIndex="1" idIndex="0" />
                        </td>
                        <td width="80px" align=right class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBPartNo" runat="server" Text="部門" Width="100%"></cc1:DSCLabel></td>
                        <td width="220px" class=BasicFormHeadDetail>
                            <cc1:SingleField ID="PartNo" runat="server" ReadOnly="True" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign=top align=right class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBNote" runat="server" Text="權限說明" Width="100%"></cc1:DSCLabel>
                        </td>
                        <td colspan=3 class=BasicFormHeadDetail>
                            <cc1:SingleField ID="Note" runat="server" Width="100%" Height="64px" 
                                MultiLine="True" />
                        </td>
                    </tr>
                    <tr>
                        <td valign=top align=right class=BasicFormHeadHead></td>
                        <td colspan=3 class=BasicFormHeadDetail>
                            <cc1:GlassButton ID="AddData" runat="server" 
                                ImageUrl="~\Images\lock.png" onclick="AddData_Click" />
                        </td>
                    </tr>
                </table>
                </Panel>
            </td>
        </tr>
        <tr>
            <td class=BasicFormHeadDetail>
                <cc1:DataList ID="RequestList" runat="server" Height="200px" Width="100%" 
                    isShowAll="True" NoModify="True" 
                    showTotalRowCount="True" NoAllDelete="True" NoAdd="True" 
                    onsetclickdata="RequestList_setClickData" IsHideSelectAllButton="True" 
                    IsOpenWinUse="True" NoDelete="True" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
