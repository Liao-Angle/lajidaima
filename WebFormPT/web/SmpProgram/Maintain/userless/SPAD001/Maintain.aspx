<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain" %>

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
        <tr><td><cc1:glassbutton id="SearchButton" runat="server" height="24px" text="搜尋" width="104px" OnClick="SearchButton_Click"></cc1:glassbutton></td>
            <td><cc1:glassbutton id="SaveButton" runat="server" height="24px" text="儲存" width="104px" OnClick="SaveButton_Click"></cc1:glassbutton></td>
        </tr>
    </table>
    <div>
        <cc1:DataList ID="UserFlowList" runat="server" Height="350px" Width="650px"/>
    
    </div>
    </form>
</body>
</html>
