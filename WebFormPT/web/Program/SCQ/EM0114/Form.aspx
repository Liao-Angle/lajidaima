<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0114_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
    <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
    <table style="width: 800px;" border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td align="center" height="30">
                <font style="font-family: 標楷體; font-size: large;"><b>閒置資產發佈單</b></font>
            </td>
        </tr>
    </table>
    <div>
        <table width="800px" border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td colspan="4" align="left" class="BasicFormHeadHead">
                    <label style="font-weight: bold;" class="style1">
                        資產信息</label>
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBEmployeeID" runat="server" Text="資產編號" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="AssetsNo" runat="server" Width="175px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="資產名稱" Width="91px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="AssetsName" runat="server" Width="150px" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="購入日期" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="YYMMDD" runat="server" Width="175px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="保管人" Width="91px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Owner" runat="server" Width="150px" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="歸屬部門" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo" runat="server" Width="175px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="是否固資" Width="91px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="IsFixed" runat="server" Width="150px" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="備註欄位" Width="109px"></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Remark" runat="server" Width="100%" Height="64px" MultiLine="True" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
