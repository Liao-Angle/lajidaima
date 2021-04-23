<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH0115_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>停薪留職</title>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
        <tr>
            <td colspan="5" class="BasicFormHeadHead" width="100%">
                <b>
                    <cc1:GlassButton ID="PrintButton1" runat="server" Height="20px" Width="300" Text="列印停薪劉職單"
                        OnBeforeClicks="PrintButton_OnClick" Enabled="True" />
                </b>
            </td>
        </tr>
    </table>
    <div>
    </div>
    <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
    <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
    <cc1:SingleField ID="EmpID" runat="server" Display="False"></cc1:SingleField>
    <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1">
        <panel runat="server" id="Panel1" visible="false">
        <tr>
            <td align="center" height="30" style="width: 700px">
                <font style="font-family: 標楷體; font-size: large;"><b>新普科技（重慶）有限公司</b></font>
            </td>
        </tr>
        </panel>
        <panel runat="server" id="Panel2" visible="false">
        <tr>
            <td align="center" height="30" style="width: 700px">
                <font style="font-family: 標楷體; font-size: large;"><b>重慶貽百電子有限公司</b></font>
            </td>
        </tr>
        </panel>
        <tr>
            <td align="center" height="30" style="width: 670px">
                <font style="font-family: 標楷體; font-weight: 700;" class="style1">停薪留職申請單</font>
            </td>
        </tr>
    </table>
    <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
        cellspacing="0" cellpadding="1">
        <tr>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="工號" Width="68px" />
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="357px" showReadOnlyField="True"
                    guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                    idIndex="0" OnSingleOpenWindowButtonClick="JEmpNo_SingleOpenWindowButtonClick" />
            </td>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                部門
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleField ID="PartNo" runat="server" Width="180px" Height="16px" />
            </td>
        </tr>
    </table>
    <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
        cellspacing="0" cellpadding="1">
        <tr>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="入職日期" Width="68px" />
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleField ID="ComeDate" runat="server" Width="155px" Height="16px" />
            </td>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="職位" Width="68px" />
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleField ID="DtName" runat="server" Width="155px" Height="16px" />
            </td>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="職等" Width="68px" />
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleField ID="Dt" runat="server" Width="145px" Height="16px" />
            </td>
        </tr>
    </table>
    <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
        cellspacing="0" cellpadding="1">
        <tr>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="原因" Width="68px" />
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleField ID="Reason" runat="server" Height="80px" Style="margin-right: 71px"
                    MultiLine="True" Width="621px" />
            </td>
        </tr>
    </table>
    <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
        cellspacing="0" cellpadding="1">
        <tr>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="停薪留職日期" Width="111px" />
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleDateTimeField ID="TryDateB" runat="server" Width="177px" />
            </td>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px; text-align: center;">
                ~
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleDateTimeField ID="TryDateE" runat="server" Width="192px" />
            </td>
            <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="員工簽名:" Width="193px" 
                    style="margin-left: 0px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
