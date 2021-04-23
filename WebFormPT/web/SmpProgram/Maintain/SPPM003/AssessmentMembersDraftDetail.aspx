<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssessmentMembersDraftDetail.aspx.cs" Inherits="SmpProgram_Maintain_SPPM003_AssessmentMembersDraftDetail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>明細</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td><cc1:DSCLabel ID="LblOrgName" runat="server" Width="90px" Text="公司別" /></td>
            <td><cc1:SingleField ID="orgName" runat="server" Width="150px" ReadOnly="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblempNumber" runat="server" Width="90px" Text="工號" /></td>
            <td><cc1:SingleOpenWindowField ID="UserGUID" runat="server" 
                        showReadOnlyField="True" Width="480px" guidField="empGUID" keyField="empNumber" 
                        serialNum="010" tableName="Users" IgnoreCase="True" 
                    onsingleopenwindowbuttonclick="UserGUID_SingleOpenWindowButtonClick" valueIndex="3" idIndex="2" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblDeptId" runat="server" Width="90px" Text="部門代號" /></td>
            <td><cc1:SingleOpenWindowField ID="deptOID" runat="server" 
                        Width="480px" showReadOnlyField="True" guidField="OID" keyField="id" 
                        serialNum="001" tableName="OrgUnit" IgnoreCase="True" ReadOnly="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblTitleName" runat="server" Width="90px" Text="職稱" /></td>
            <td><cc1:SingleField ID="titleName" runat="server" Width="150px" ReadOnly="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblFunctionName" runat="server" Width="90px" Text="身份別" /></td>
            <td><cc1:SingleField ID="functionName" runat="server" Width="150px" ReadOnly="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblDeptManagerName" runat="server" Width="90px" Text="部門主管" /></td>
            <td><cc1:SingleOpenWindowField ID="deptManagerNameGUID" runat="server" 
                        showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                        serialNum="001" tableName="Users" IgnoreCase="True" ReadOnly="True" /></td>
        </tr>
    </table>
    </form>
</body>
</html>
