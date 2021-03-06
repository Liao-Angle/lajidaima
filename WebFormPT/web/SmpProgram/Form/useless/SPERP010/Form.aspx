<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPERP010_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>OM報價單</title>
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
    <cc1:SingleField ID="ApproverId" runat="server" ReadOnly="True" />
    <cc1:SingleField ID="IsResolved" runat="server" ReadOnly="True" />
	<cc1:SingleField ID="IsCheckUser" runat="server" ReadOnly="True" />
    </div>
    </form>
	<tr>
      <p runat="server" id="HtmlContentCode"></p>
	</tr>	
	<tr>
      <p runat="server" id="HtmlContentCodeExt"></p>
	</tr>
	<tr>
	  <td align="right" class="BasicFormHeadHead">
        <cc1:DSCLabel ID="LblCheckby1" runat="server" Text="審核人一" Width="85px" IsNecessary="False" TextAlign="2" />
      </td>
	  <td class=BasicFormHeadDetail>
        <cc1:SingleOpenWindowField ID="Reviewer1GUID" runat="server" Width="220px" 
             showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
             tableName="Users" IgnoreCase="True" />
      </td>
	  <td align="right" class="BasicFormHeadHead">
        <cc1:DSCLabel ID="LblCheckby2" runat="server" Text="審核人二" Width="85px" IsNecessary="False" TextAlign="2" />
      </td>
	  <td class=BasicFormHeadDetail>
        <cc1:SingleOpenWindowField ID="Reviewer2GUID" runat="server" Width="220px" 
             showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
             tableName="Users" IgnoreCase="True" />
      </td>
    </tr>
	<P></p>
</body>
</html>
