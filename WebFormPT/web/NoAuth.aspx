<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoAuth.aspx.cs" Inherits="NoAuth" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border=0 width=100% height=100%>
    <tr>
        <td width=100% height=100% align=center valign=middle style="font-size:9pt"><%=com.dsc.locale.LocaleString.getSystemMessageString("web_general.language.ini", "global", "NOUSEAUTH", "您沒有使用權限") %></td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
