<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_System_Form_SPAD003_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>請假單明細</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border=0 width=400px>    
    <tr>
        <td><cc1:DSCLabel ID="LblUserGUID" runat="server" Text="工號" Width="120px" 
                IsNecessary="True" /></td>
        <td>
            <cc1:SingleOpenWindowField ID="UserGUID" runat="server" Width="200px" 
                showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                tableName="Users" Height="80px" idIndex="2" valueIndex="3" />
        </td>
    </tr>
    <tr>   
        <td><cc1:DSCLabel ID="LblStartDateTime" runat="server" Text="起始時間" Width="120px" 
                IsNecessary="True" /></td>
        <td>
            <cc1:SingleDateTimeField ID="StartDateTime" runat="server" 
                Width="164px" DateTimeMode="1" ondatetimeclick="DateTimeClick" />
        </td>
    </tr>
    <tr>
        <td><cc1:DSCLabel ID="LblEndDateTime" runat="server" Text="截止時間" Width="120px" 
                IsNecessary="True" /></td>
        <td>
            <cc1:SingleDateTimeField ID="EndDateTime" runat="server" 
                Width="165px" DateTimeMode="1" ondatetimeclick="DateTimeClick" />
        </td>
    </tr>
    <tr>
        <td><cc1:DSCLabel ID="LblHours" runat="server" Text="時數" Width="120px" 
                IsNecessary="True" /></td>
        <td>
            <cc1:SingleField ID="Hours" runat="server" Width="60px" />
        </td>
    </tr>
    <tr>
        <td><cc1:DSCLabel ID="LblReason" runat="server" Text="加班原因" Width="120px" 
                IsNecessary="True" /></td>
        <td>
            <cc1:SingleField ID="Reason" runat="server" Width="230px" />
        </td>
    </tr>
    <tr>
        <td><cc1:DSCLabel ID="LblRemark" runat="server" Text="備註" Width="120px" /></td>
        <td>
            <cc1:SingleField ID="Remark" runat="server" Width="230px" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
