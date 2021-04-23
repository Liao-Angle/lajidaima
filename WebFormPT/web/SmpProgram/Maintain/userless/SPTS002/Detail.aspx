<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_maintain_SPTS002_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>課程主檔維護作業-明細</title>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:SingleField ID="CompanyCode" runat="server" Display="False"/>
    <table>
        <tr>
            <td><cc1:DSCLabel ID="lblSubjectNo" runat="server" Width="90px" Text="課程代號"  TextAlign="2" /></td>	
			<td colspan="3" align="left"><cc1:SingleField ID="SubjectNo" runat="server" Width="120px" /></td>
        </tr>
        <tr>
            <td><cc1:DSCLabel ID="lblSubjectName" runat="server" Width="90px" Text="課程名稱"  TextAlign="2" /></td>	
			<td colspan="3" align="left"><cc1:SingleField ID="SubjectName" runat="server" Width="420px" /></td>
        </tr>
        <tr>
            <td><cc1:DSCLabel ID="lblDeptGUID" runat="server" Width="90px" Text="部門代號"  TextAlign="2" IsNecessary="true"/></td>
            <td colspan="3" align="left"><cc1:SingleOpenWindowField ID="DeptGUID" 
                    runat="server" Width="430px" 
                            showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                            tableName="OrgUnit" FixReadOnlyValueTextWidth="300px" 
                        FixValueTextWidth="90px" IgnoreCase="True" 
                    onbeforeclickbutton="DeptGUID_BeforeClickButton" /></td> 
			</td>
        </tr>
        <tr>
			<td><cc1:DSCLabel ID="lblInOut" runat="server" Width="90px" Text="內/外訓"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="InOut" runat="server" Width="120px"  /></td>
			<td align="right"><cc1:DSCLabel ID="lblSubjectType" runat="server" Width="120px" Text="課程類別"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="SubjectType" runat="server" Width="120px" /></td>
        </tr>
        <tr>
			<td><cc1:DSCLabel ID="lblTrainingHours" runat="server" Width="90px" Text="訓練時數"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleField ID="TrainingHours" runat="server" Width="120px" ontextchanged="TrainingHours_TextChanged"/></td>
			<td align="right"><cc1:DSCLabel ID="lblExpiryDate" runat="server" Width="120px" Text="失效日期"  TextAlign="2" /></td>
            <td><cc1:SingleDateTimeField ID="ExpiryDate" runat="server" Width="120px" /></td>
        </tr>


    </table>
    </form>
</body>
</html>
