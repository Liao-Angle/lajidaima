<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPIT004_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>My Default</title>
    <style type="text/css">

    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>維修記錄單</b></font></td>
        </tr>
    </table>
    <cc1:SingleField ID="OriginatorGUID" runat="server" Display="False" Width="169px" />
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr><td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=5>
                <cc1:SingleField ID="Subject" runat="server" Width="570px" />
            </td></tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCallUserGUID" runat="server" Text="叫修人員" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CallUserGUID" runat="server" Width="130px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" idIndex="2" valueIndex="3" 
                    tableName="Users" Height="80px" FixReadOnlyValueTextWidth="62px" 
                    FixValueTextWidth="40px" 
                    onsingleopenwindowbuttonclick="CallUserGUID_SingleOpenWindow" 
                    />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCallOrgUnitGUID" runat="server" Text="叫修單位" Width="80px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CallOrgUnitGUID" runat="server" 
                    Width="215px" showReadOnlyField="True" guidField="OID" keyField="id" 
                    serialNum="001" tableName="OrgUnit" FixReadOnlyValueTextWidth="120px" 
                    FixValueTextWidth="60px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCallExtension" runat="server" Text="分機" Width="50px" 
                    TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="CallExtension" runat="server" Width="60px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCallDateTime" runat="server" Text="叫修時間" Width="80px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="CallDateTime" runat="server" 
                    Width="125px" DateTimeMode="1" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblProcessingHours" runat="server" Text="處理時間" Width="80px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="ProcessingHours" runat="server" Width="60px" />
                &nbsp;(小時)
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblIssueDescription" runat="server" Text="問題描述" Width="80px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=5 class=BasicFormHeadDetail>
                <cc1:SingleField ID="IssueDescription" runat="server" Height="120px" Width="570px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblProcessingMethod" runat="server" Text="處理方法" Width="80px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=5 class=BasicFormHeadDetail>
                <cc1:SingleField ID="ProcessingMethod" runat="server" Height="120px" Width="570px" 
                    MultiLine="True" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
