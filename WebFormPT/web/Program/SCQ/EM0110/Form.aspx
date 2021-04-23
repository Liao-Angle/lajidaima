<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0110_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">

.SingleOpenWindowField_Normal
{
	border-style:solid;
	border-width:1px;
	font-size:10pt;
	background-color:Transparent;
	font-family:Arial;
}
.SingleOpenWindowField_Button
{
	    border: 1px none #999999;
            font-size:12px;
	        line-height:16px;
	        font-weight: normal;
	        color:#000000;
	background-image:url('../../../DSCWebControlRunTime/DSCWebControlImages/FIND.gif');

	        width:22px;
	        height:20px;
	        cursor:pointer;
	        vertical-align:middle;
	        margin-top:0px;
	        font-family:Arial;
}
.SingleOpenWindowField_ClearButton
{
	    border: 1px none #999999;
            font-size:12px;
	        line-height:16px;
	        font-weight: normal;
	        color:#000000;
	background-image:url('../../../DSCWebControlRunTime/DSCWebControlImages/CLEAR.gif');

	        width:22px;
	        height:20px;
	        cursor:pointer;
	        vertical-align:middle;
	        margin-top:0px;
	        font-family:Arial;
}
.SingleOpenWindowField_ReadOnly
{
	border-style:solid;
	border-width:1px;
	font-size:10pt;
	background-color:#E0E0E0;
	font-family:Arial;
}
    </style>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=800px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工" Width="100%"></cc1:DSCLabel></td>
            <td width="200px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="260px" 
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" 
                    OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" 
                    valueIndex="1" idIndex="0" FixReadOnlyValueTextWidth="120px" 
                    FixValueTextWidth="80px" />
            </td>
            <td width="100px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBMobile" runat="server" Text="電話號碼" Width="100%"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="Mobile" runat="server" Width="180px" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBIssueType" runat="server" Text="問題種類" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDropDownList ID="IssueType" 
                                    runat="server" Width="163px" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                &nbsp;</td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDropDownList ID="Finished" 
                                    runat="server" Width="163px" 
                    onselectchanged="Finished_SelectChanged" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBIssue" runat="server" Text="問題描述" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="Issue" runat="server" Width="100%" Height="103px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr id=Dispach runat=server>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBMISEmpNo" runat="server" Text="MIS派工" Width="100%" 
                   ></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleOpenWindowField ID="MISEmpNo" runat="server" Width="260px" 
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" 
                    valueIndex="1" idIndex="0" FixReadOnlyValueTextWidth="120px" 
                    FixValueTextWidth="80px" />
            </td>
        </tr>
        <tr id=EditStatus1 runat=server>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBPendingReason" runat="server" Text="未完成原因" Width="100%" 
                    style="margin-bottom: 0px"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="PendingReason" runat="server" Width="100%" Height="103px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr id=EditStatus2 runat=server>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBSolution" runat="server" Text="解決方法" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="Solution" runat="server" Width="100%" Height="103px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr id=Satisfy1 runat=server>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBSatisfyValue" runat="server" Text="滿意度調查" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:DSCRadioButton ID="SatisfyValue5" runat="server" GroupName="SatisfyValue" 
                    Text="非常滿意" />
                <cc1:DSCRadioButton ID="SatisfyValue4" runat="server" GroupName="SatisfyValue" 
                    Text="滿意" />
                <cc1:DSCRadioButton ID="SatisfyValue3" runat="server" GroupName="SatisfyValue" 
                    Text="普通" Checked="True" />
                <cc1:DSCRadioButton ID="SatisfyValue2" runat="server" GroupName="SatisfyValue" 
                    Text="不滿意" />
                <cc1:DSCRadioButton ID="SatisfyValue1" runat="server" GroupName="SatisfyValue" 
                    Text="非常不滿意" />
            </td>
        </tr>
        <tr id=Satisfy2 runat=server>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBSatisfyText" runat="server" Text="滿意度說明" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="SatisfyText" runat="server" Width="100%" Height="103px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr id=Status runat=server>
            <td align=right class=BasicFormHeadDetail colspan=4>
                
                <cc1:DataList ID="ApplicationList" runat="server" Height="200px" Width="100%" 
                    isShowAll="True" NoModify="True" 
                    showTotalRowCount="True" NoAllDelete="True" NoAdd="True" IsHideSelectAllButton="True" 
                    IsOpenWinUse="True" NoDelete="True" IsShowCheckBox="False" />
                
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
