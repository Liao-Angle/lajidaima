<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0103_Form" %>

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
            <td align=right class=BasicFormHeadHead valign="top" rowspan=2 width=80px>
                <cc1:DSCLabel ID="LBApplication" runat="server" Text="申請人資料"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail width="280px">
                 <Panel ID="EditPanel" width="100%" runat="server">
                    <table width="100%">
                        <tr>
                            <td class=BasicFormHeadHead>
                                <cc1:DSCLabel ID="LBHead1" runat="server" Text="申請人"></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadHead>
                                <cc1:DSCLabel ID="LBHead3" runat="server" Text="部門"></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadHead>
                                <cc1:DSCLabel ID="LBHead4" runat="server" Text="職稱"></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadHead>
                                <cc1:DSCLabel ID="LBHead6" runat="server" Text="動作"></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadHead>
                                <cc1:DSCLabel ID="LBHead5" runat="server" Text="權限"></cc1:DSCLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="200px" 
                                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                                    tableName="hrusers" 
                                    OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" 
                                    valueIndex="1" idIndex="0" />
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleField ID="PartNo" runat="server" Width="100px" />
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleField ID="DtName" runat="server" Width="60px" />
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleDropDownList ID="ActionID" 
                                    runat="server" Width="80px" />
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleDropDownList ID="RoleID" 
                                    runat="server" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td class=BasicFormHeadDetail align="center" colspan=5>
                                <cc1:GlassButton ID="AddData" runat="server" 
                                    ImageUrl="~\Images\lock.png" onclick="AddData_Click" 
                                    style="margin-left: 24px" />
                            </td>
                        </tr>
                        </table>
                 </Panel>
             </td>
        </tr>
        <tr>
             <td class=BasicFormHeadDetail>
                <cc1:DataList ID="ApplicationList" runat="server" Height="200px" Width="100%" 
                    isShowAll="True" NoModify="True" 
                    showTotalRowCount="True" NoAllDelete="True" NoAdd="True" 
                    onsetclickdata="ApplicationList_setClickData" IsHideSelectAllButton="True" 
                    IsOpenWinUse="True" NoDelete="True" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBApplication2" runat="server" Text="申請說明"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="AppData" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBNote" runat="server" Text="備註說明"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Note" runat="server" Width="100%" Height="64px" MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
