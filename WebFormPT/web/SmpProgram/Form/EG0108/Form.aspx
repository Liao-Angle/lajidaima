<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EG0108_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>外出申請單</title>
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
    <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
    <div>
        <table style="margin-left: 4px; width: 680px;" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td align="center" height="30" style="width: 680px">
                    <font style="font-family: 標楷體; font-weight: 700;"><span class="style1">人員外出申請單</span></font>
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 680px;" border="0" cellspacing="0" cellpadding="1"
            class="BasicFormHeadBorder">
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBxz" runat="server" Text="申請單號" Width="100px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqNo" runat="server" Width="155px" ReadOnly="True" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="申請日期" Width="100px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqTime" runat="server" Width="155px" ReadOnly="True" /></cc1:SingleField>
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="填單人/主管" Width="100px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqEmp" runat="server" Width="155px" ReadOnly="True" 
                        Height="16px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBDepartment" runat="server" Text="填單人部門" Width="100px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo" runat="server" Width="155px" ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead" colspan="4">
                    <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="人員外出列表" Width="190px"></cc1:DSCLabel>
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail" colspan="4" valign="top">
                    &nbsp;<cc1:OutDataList ID="perlist" runat="server" Height="292px"
                        ViewStateMode="Disabled" Width="680px" showExcel="True" NoModify="True" IsExcelWithMultiType="True"
                        showTotalRowCount="True" onsaverowdata="perlist_SaveRowData"></cc1:OutDataList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
