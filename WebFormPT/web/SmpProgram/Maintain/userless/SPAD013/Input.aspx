<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPAD013_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>文具品項價格輸入</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td><cc1:DSCLabel id="lblCategoryId" runat="server" Text="產品類別碼" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="CategoryId" runat="server" Width="100px" /></td>
        </tr>
		<tr><td><cc1:DSCLabel id="lblCategoryName" runat="server" Text="產品類別說明" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="CategoryName" runat="server" Width="250px" /></td>
        </tr>
		<tr><td><cc1:DSCLabel id="lblTypeId" runat="server" Text="特性碼" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="TypeId" runat="server" Width="100px" /></td>
        </tr>
		<tr><td><cc1:DSCLabel id="lblTypeName" runat="server" Text="特性說明" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="TypeName" runat="server" Width="250px" /></td>
        </tr>
		<tr><td><cc1:DSCLabel id="lblAttributeId" runat="server" Text="屬性碼" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="AttributeId" runat="server" Width="120px" /></td>
        </tr>
		<tr><td><cc1:DSCLabel id="lblAttributeName" runat="server" Text="屬性說明" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="AttributeName" runat="server" Width="220px" /></td>
        </tr>
		<tr><td><cc1:DSCLabel id="lblProdDesc" runat="server" Text="品名" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="ProdDesc" runat="server" Width="300px" /></td>
        </tr>
		<tr><td><cc1:DSCLabel id="lblUnit" runat="server" Text="單位" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="Unit" runat="server" Width="80px" /></td>
        </tr>
        <tr><td><cc1:DSCLabel id="lblPrice" runat="server" Text="單價" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="Price" runat="server" Width="80px" /></td>
        </tr>
        <tr><td><cc1:DSCLabel id="lblActive" runat="server" Text="生失效" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleDropDownList ID="Active" runat="server" Width="80px" /></td>
        </tr>
		<!--<tr><td><cc1:DSCLabel id="lblAttribute2" runat="server" Text="屬性碼2" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="Attribute2" runat="server" Width="120px" /></td>
        </tr>
		<tr><td><cc1:DSCLabel id="lblAttribute3" runat="server" Text="屬性碼3" Width="90px"></cc1:DSCLabel></td>
            <td><cc1:SingleField ID="Attribute3" runat="server" Width="120px" /></td>
        </tr>-->
    </table> 
    </form>
</body>
</html>
