<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPPM002_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>自定評核人員</title>
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 720px">
        <legend>自定評核人員維護</legend>
        <table style="margin-left:4px; width: 650px;" border=0 cellspacing=0 cellpadding=1 >  
            <tr><td><cc1:DSCLabel ID="LblOrgName" runat="server" Width="100px" Text="公司別" TextAlign="2" /></td>
                <td>
                    <cc1:SingleField ID="orgName" runat="server" 
                        Width="180px" ReadOnly="True" /></td>
            </tr>
            <tr><td><cc1:DSCLabel id="LblUserGUID" runat="server" Text="員工" Width="100px"  TextAlign="2"></cc1:DSCLabel></td>
                <td><cc1:SingleOpenWindowField ID="UserGUID" runat="server" 
                        showReadOnlyField="True" Width="480px" guidField="empGUID" 
                        keyField="empNumber" valueIndex="3" idIndex="2"
                        serialNum="004" tableName="Users" IgnoreCase="True" 
                        onsingleopenwindowbuttonclick="UserGUID_SingleOpenWindowButtonClick" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblTitleName" runat="server" Width="100px" Text="職稱" TextAlign="2" /></td>
                <td>
                    <cc1:SingleField ID="titleName" runat="server" 
                        Width="180px" ReadOnly="True" /></td>
            </tr>
            <tr><td>
                    <cc1:DSCLabel id="LblDeptId" runat="server" Text="部門" Width="100px"  TextAlign="2"></cc1:DSCLabel></td>
                <td>
                    <cc1:SingleOpenWindowField ID="deptOID" runat="server" 
                        Width="480px" showReadOnlyField="True" guidField="OID" keyField="id" 
                        serialNum="001" tableName="OrgUnit" IgnoreCase="True" ReadOnly="True" /></td>
            </tr>
            <tr><td><cc1:DSCLabel id="LblFirstAssessUserGUID" runat="server" Text="一階評核人員" 
                    Width="100px"  TextAlign="2"></cc1:DSCLabel></td>
                <td><cc1:SingleOpenWindowField ID="FirstAssessUserGUID" runat="server" 
                        showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                        serialNum="001" tableName="Users" IgnoreCase="True" /></td>
            </tr>
            <tr><td><cc1:DSCLabel id="LblSecondAssessUserGUID" runat="server" Text="二階評核人員" 
                    Width="100px"  TextAlign="2"></cc1:DSCLabel></td>
                <td><cc1:SingleOpenWindowField ID="SecondAssessUserGUID" runat="server" 
                        showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                        serialNum="001" tableName="Users" IgnoreCase="True" /></td>
            </tr>
            <tr><td><cc1:DSCLabel id="LblAchievementUserGUID" runat="server" Text="成績分配人員" Width="100px"  TextAlign="2"></cc1:DSCLabel></td>
                <td><cc1:SingleOpenWindowField ID="AchievementUserGUID" runat="server" 
                        showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                        serialNum="001" tableName="Users" IgnoreCase="True" /></td>
            </tr>
        </table>
    </fieldset>
    </form>
</body>
</html>
