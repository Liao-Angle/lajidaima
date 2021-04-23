<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_System_Form_SPERP002_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>費用請款單明細</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="SourceId" runat="server" />
    <div>
    <table border=0 width=400px>    
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="PoTypelb" runat="server" Text="POType" Width="60px" /></td>
        <td ><cc1:SingleField ID="PoType" runat="server" Height="21px" ReadOnly="true" /></td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="PoCategorylb" runat="server" Text="類別" Width="120px" /></td>
        <td ><cc1:SingleField ID="PoCategory" runat="server" Height="21px" ReadOnly="true" /></td>
    </tr>
    <tr>   
        <td valign=top width=120px><cc1:DSCLabel ID="OriginatorGUIDlb" runat="server" Text="請購人" Width="120px" /></td>
        <%--<td><cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="200px" Height="21px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="Users"/></td>        --%>
        <td ><cc1:SingleField ID="OriginatorGUID" runat="server" Height="21px" ReadOnly="True" /></td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="ItemSpeclb" runat="server" Text="品名/規則" Width="120px" /></td>
        <td ><cc1:SingleField ID="ItemSpec" runat="server"  Height="21px" ReadOnly="True" /></td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="Remarklb" runat="server" Text="備註" Width="120px" /></td>
        <td ><cc1:SingleField ID="Remark" runat="server" Width="100px" Height="21px" ReadOnly="True" /></td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="採購數量" Width="120px" /></td>
        <td ><cc1:SingleField ID="Quantity" runat="server" Height="21px" ReadOnly="True" alignRight="True"/></td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="PriceUnitlb" runat="server" Text="採購單價" Width="120px" /></td>
        <td ><cc1:SingleField ID="PriceUnit" runat="server" Height="21px" ReadOnly="True" alignRight="True" isMoney="true" /></td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="Amountlb" runat="server" Text="採購金額" Width="120px" /></td>
        <td ><cc1:SingleField ID="Amount" runat="server" Height="21px" ReadOnly="True" alignRight="True" isMoney="true"/></td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DueDatelb" runat="server" Text="預交日" Width="120px" /></td>
        <td ><cc1:SingleField ID="DueDate" runat="server" Height="21px" ReadOnly="True" alignRight="True" isMoney="true" /></td>
        <%--<td ><cc1:SingleDateTimeField ID="DueDate" runat="server"  Height="21px" Width="200px"  ReadOnly="True" alignRight="True" /></td>--%>
    </tr>

    </table>
    </div>
    </form>
</body>
</html>
