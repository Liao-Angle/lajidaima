<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH0114_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>自離申請單</title>
    <style type="text/css">
        .style2
        {
            width: 163px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
        <cc1:SingleField ID="Subject" runat="server" Display="False" />
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" 
            cellpadding="1">
            <tr valign="middle">
                <td align="center" height="40">
                    <font style="font-family: 標楷體; font-size: large;"><b>自離申請單</b></font>
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1"
            class="BasicFormHeadBorder">
            <tr>
                <td width="140px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="工號" Width="84px"></cc1:DSCLabel>
                </td>
                <td width="240px" class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="200px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" />
                </td>
                <td width="110px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBDepartment" runat="server" Text="部門" Width="93px" Style="font-size: x-small">
                    </cc1:DSCLabel>
                </td>
                <td class="style2">
                    <cc1:SingleField ID="Department" runat="server" Width="114px" ReadOnly="True" 
                        Height="24px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBPerType" runat="server" Text="員工分類" Width="84px" Style="font-size: x-small">
                    </cc1:DSCLabel>
                </td>
                <td width="240px" class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="PerType" runat="server" Width="92px" 
                        Height="20px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBKGDate" Width="80px" runat="server" Text="曠工日期一" 
                        Style="font-size: x-small">
                    </cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="first" runat="server" Width="136px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" Width="93px" runat="server" Text="曠工日期二" Style="font-size: x-small">
                    </cc1:DSCLabel>
                </td>
                <td colspan="1" class="style2">
                    <cc1:SingleDateTimeField ID="second" runat="server" Width="114px" 
                        Style="margin-left: 0px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" Width="84px" runat="server" Text="曠工日期三" Style="font-size: x-small">
                    </cc1:DSCLabel>
                </td>
                <td colspan="1" class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="third" runat="server" Width="92px" />
                </td>
            </tr>
            <tr>
                <td align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBLZReason" runat="server" Text="離職原因" Width="84px" 
                        Style="font-size: x-small">
                    </cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" colspan="5">
                    <cc1:SingleField ID="LZReason" runat="server" Width="611px" ReadOnly="false" 
                        Height="30px" />
                </td>
            </tr>
            <tr>
                <td align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBbz" runat="server" Text="其他備註" Width="100%" Style="font-size: x-small">
                    </cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" colspan="5">
                    <cc1:SingleField ID="bz" runat="server" Width="608px" ReadOnly="false" 
                        Height="30px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
