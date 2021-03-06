<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPERP012_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>銷貨異常說明書</title>
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
   <tr>
      <p runat="server" id="HtmlContentCode"></p>
	</tr>	
	</br>
	<tr>
      <p runat="server" id="HtmlContentCodeExt"></p>
	</tr>
	<tr>
	  <td align="right" class="BasicFormHeadHead">
        <cc1:DSCLabel ID="LblManager" runat="server" Text="主管" Width="85px" IsNecessary="False" TextAlign="2" />
      </td>
	  <td class=BasicFormHeadDetail>
        <cc1:SingleOpenWindowField ID="ManagerGUID" runat="server" Width="220px" 
             showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
             tableName="Users" IgnoreCase="True" />
      </td>
	  <td align="right" class="BasicFormHeadHead">
        <cc1:DSCLabel ID="LblReviewer1" runat="server" Text="業務" Width="85px" IsNecessary="False" TextAlign="2" />
      </td>
	  <td class=BasicFormHeadDetail>
        <cc1:SingleOpenWindowField ID="Reviewer2GUID" runat="server" Width="220px" 
             showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
             tableName="Users" IgnoreCase="True" />
      </td>
	  <td align="right" class="BasicFormHeadHead">
        <cc1:DSCLabel ID="LblReviewer2" runat="server" Text="業務主管" Width="85px" IsNecessary="False" TextAlign="2" />
      </td>
	  <td class=BasicFormHeadDetail>
        <cc1:SingleOpenWindowField ID="Reviewer3GUID" runat="server" Width="220px" 
             showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
             tableName="Users" IgnoreCase="True" />
      </td>
    </tr>
	</br>
	<tr>
	  <td align="right" class="BasicFormHeadHead">
        <cc1:DSCLabel ID="LblMFQ" runat="server" Text="通知MFQ" Width="85px" IsNecessary="False" TextAlign="2" />
      </td>
	  <td class=BasicFormHeadDetail>
        <cc1:SingleOpenWindowField ID="Reviewer4GUID" runat="server" Width="220px" 
             showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
             tableName="Users" IgnoreCase="True" />
      </td>
	  <td align="right" class="BasicFormHeadHead">
        <cc1:DSCLabel ID="LblQA" runat="server" Text="通知QA" Width="85px" IsNecessary="False" TextAlign="2" />
      </td>
	  <td class=BasicFormHeadDetail>
        <cc1:SingleOpenWindowField ID="Reviewer5GUID" runat="server" Width="220px" 
             showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
             tableName="Users" IgnoreCase="True" />
      </td>

    </tr>
</body>
</html>
