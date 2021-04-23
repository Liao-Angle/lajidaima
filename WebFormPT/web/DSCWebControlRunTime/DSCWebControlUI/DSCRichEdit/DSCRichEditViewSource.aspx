<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DSCRichEditViewSource.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_DSCRichEdit_DSCRichEditViewSource" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <cc1:SingleField ID="SourceContent" runat="server" Height="600px" MultiLine="True" ReadOnly="True" Width="100%" />    
    </div>
    </form>
</body>
</html>
