<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="Program_SCQ_Form_EH0112_PrintPage" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<style type="text/css">
    @media print
    {
        .print
        {
            display: none;
        }
    }
    
    .style1
    {
        border-left: 1px none rgb(188,178,147);
        border-right: 1px solid rgb(188,178,147);
        border-top: 1px none rgb(188,178,147);
        border-bottom: 1px solid rgb(188,178,147);
        background-color: white;
        font-size: medium;
        font-family: Arial;
        height: 20px;
    }
</style>
<body style="background: #ffffff">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <input type="Button" value="列印單據" onclick="javascript:print();" class="print">
        </table>
        <table>
            <br>
            <tr>
                <td style="font-size: 8pt;">
                    表單資訊
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divFormInfo" style="position: absolute;" runat="server">
                    </div>
                    <br />
                </td>
            </tr>
        </table>
        <br>
        <br />
        <br />
        <br />
        <br />
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
                        idIndex="0" />
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
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="員工簽名:" Width="193px" Style="margin-left: 0px" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="font-size: 8pt;">
                    簽核意見
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div2" style="position: absolute;" runat="server">
                    </div>
                    <br />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
