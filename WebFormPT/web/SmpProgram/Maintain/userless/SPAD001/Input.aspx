<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Input" %>

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
        <tr><td><cc1:DSCLabel id="lblFlowGUID" runat="server" Text="流程" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleOpenWindowField ID="FlowGUID" runat="server" 
                    showReadOnlyField="True" Width="480px" guidField="SMWBAAA001" 
                    keyField="SMWBAAA003" serialNum="001" tableName="SMWBAAA" 
                    IgnoreCase="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel id="lblUserGUID" runat="server" Text="員工" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleOpenWindowField ID="UserGUID" runat="server" 
                    showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                    serialNum="001" tableName="Users" IgnoreCase="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel id="lblActive" runat="server" Text="生失效" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleDropDownList ID="Active" runat="server" Width="50px" /></td>
        </tr>
    </table> 
    <div>
        <cc1:DataList ID="UserFlowDetailList" runat="server" Height="240px" 
            Width="600px" DialogWidth="600" />
    </div>
    </form>
</body>
</html>
