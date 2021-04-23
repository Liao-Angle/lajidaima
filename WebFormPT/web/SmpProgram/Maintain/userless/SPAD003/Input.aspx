<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPAD003_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>輸入</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td><cc1:DSCLabel id="lblEmployeeGUID" runat="server" Text="員工" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleOpenWindowField ID="EmployeeGUID" runat="server" 
                    showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                    serialNum="001" tableName="Users" ReadOnly="True"/></td>
        </tr>
		<tr><td><cc1:DSCLabel id="lblManagerGUID" runat="server" Text="直屬主管" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleOpenWindowField ID="ManagerGUID" runat="server" 
                    showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                    serialNum="001" tableName="Users" /></td>
        </tr>

    </table> 
    </form>
</body>
</html>
