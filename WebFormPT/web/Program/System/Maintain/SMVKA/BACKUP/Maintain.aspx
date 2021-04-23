<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_System_Maintain_SMVKA_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>代理人設定作業</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <cc1:DataList ID="dateList" runat="server" Height="350px" Width="660px" OnDeleteData="dateList_DeleteData" />
    </div>
    </form>
</body>
</html>
