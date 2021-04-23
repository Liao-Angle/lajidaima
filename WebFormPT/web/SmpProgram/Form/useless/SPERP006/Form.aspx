<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPERP006_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>原物料請購單</title>
    <script language="javascript" src="../../../JS/jquery-1.4.1.js"></script>
    <script language="javascript" src="../../../JS/jquery-1.4.1.min.js"></script>
    <style type="text/css">
        #HtmlContentCode
        {
            text-align: left;
        }
    </style>
    <script language="javascript">
        function OpenWindowWithPost(url, windowoption, name, params) {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", name);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {
                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }
            window.open("", name, windowoption);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
    </script>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <cc1:SingleField ID="KeyId" runat="server" Display="False" ReadOnly="True" />
    <cc1:SingleField ID="SheetNo" runat="server" ReadOnly="True" />
    <cc1:SingleField ID="OriginatorId" runat="server" ReadOnly="True" />
    <cc1:SingleField ID="ReviewerId" runat="server" ReadOnly="True" />
    <cc1:SingleField ID="ApproverId" runat="server" ReadOnly="True" />
    <cc1:SingleField ID="IsResolved" runat="server" ReadOnly="True" />
    </div>
    </form>
    <p runat="server" id="HtmlContentCode"></p>
</body>
</html>
