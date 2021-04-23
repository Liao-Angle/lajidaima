<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVT_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>程式代碼輸入畫面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border=0 width=510px>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="使用者" Width="120px" /></td>
        <td >
            <cc1:SingleOpenWindowField ID="SMVTAAA002" runat="server" guidField="OID"
            keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users"
            Width="365px" />        
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
