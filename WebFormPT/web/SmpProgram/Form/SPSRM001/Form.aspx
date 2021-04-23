<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPSRM001_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>供應商評鑑申請單</title>
    <script language="javascript" src="../../../JS/jquery-1.4.1.js"></script>
    <script language="javascript" src="../../../JS/jquery-1.4.1.min.js"></script>
    <style type="text/css">
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
        .DSCLabel
        {}
    </style>
    <script language="javascript">
        function onViewAttach(entityName, pk1Value, modify, title) {
            var url = $("#ViewAttachUrl").val();
            var param = { EntityName: entityName, Pk1Value: pk1Value, Modify: modify, Title: title };
            OpenWindowWithPost(url, 'height=420, width=782, top=100, left=100, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no', 'ViewAttach', param);	
        }

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
    <asp:HiddenField ID="SourceId" runat="server" />
    <asp:HiddenField ID="ViewAttachUrl" runat="server" />
    <cc1:SingleField ID="SheetNo" runat="server" Display="False" ReadOnly="True" />
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <tr><td align=center><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/smp-logo.jpg"/></td>
        </tr>
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>供應商評鑑申請單</b></font></td>
        </tr>
    </table>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="70px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td colspan="5" class="BasicFormHeadDetail">
                <cc1:SingleField ID="Subject" runat="server" Width="584px" />
            </td>
        </tr>
        <tr>
	        <td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="lblOriginator" runat="server" Text="申請人員" Width="70px" 
                    CssClass="DSCLabel" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class="BasicFormHeadDetail">
                <cc1:SingleOpenWindowField ID="OriginatorGUID" 
                    runat="server" Width="170px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                    tableName="Users" Height="80px" 
                    onbeforeclickbutton="CheckbyGUID_BeforeClickButton" IgnoreCase="True" /></td>
            <td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="lblCheckby" runat="server" Text="審核人" width="60px" 
                    CssClass="DSCLabel" TextAlign="2"/>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckbyGUID" runat="server" Width="170px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" IgnoreCase="True" />
            </td>
            <td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="lblIsNewContract" runat="server" Text="是否為新合約" width="90px" 
                    CssClass="DSCLabel" IsNecessary="True" TextAlign="2"/>
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleDropDownList ID="IsNewContract" runat="server" Width="50px" />
            </td>
        </tr>
        <tr><td colspan=6 align="right" class=BasicFormHeadDetail>
            <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
                <tr><td>
                    <ul runat="server" id="HtmlContentCode"></ul>
                    </td>
                </tr>
            </table>
            </td></tr>
    </table>
    </div>
    </form>
</body>
</html>
