<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0105_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <table style="margin-left: 4px; width: 666px;" border="0" cellspacing="0" cellpadding="1">
            <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
            <cc1:SingleField ID="Subject" runat="server" Display="False" />
            <tr valign="middle">
                <td align="center" height="40">
                    <font style="font-family: 標楷體; font-size: large;"><b>Internet申請單</b></font>
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px" border="0" width="666px" cellspacing="0" cellpadding="1"
            class="BasicFormHeadBorder">
            <tr>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="上網人員類別" Width="107px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" width="60px">
                    <cc1:SingleDropDownList ID="onlietype" runat="server" Width="100px" 
                        onselectchanged="onlietype_SelectChanged" />
                </td>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="客戶姓名" Width="107px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" width="60px">
                    <cc1:SingleField ID="customer" runat="server" Width="122px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="LBHead1" runat="server" Text="申請人" Width="58px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="249px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick"
                        valueIndex="1" idIndex="0" />
                </td>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="LBHead3" runat="server" Text="部門" Width="59px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo" runat="server" Width="122px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBHead4" runat="server" Text="職稱" Width="59px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="DtName" runat="server" Width="122px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="英文名" Width="59px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="EName" runat="server" Width="122px" Height="16px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBIntranet" runat="server" Text="上網方式" Width="100px"></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="Intranet" runat="server" Width="122px" 
                        onselectchanged="onlietype_SelectChanged" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBIP" runat="server" Text="上網起始時段" Width="101px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="SingleDateTimeField1" runat="server" 
                        Width="178px" />
                </td>
                <td colspan="2" class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="SingleDateTimeField2" runat="server" 
                        Width="178px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBMAC" runat="server" Text="MAC"></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="MAC" runat="server" Width="100%" MultiLine="True" 
                        Height="61px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBArea" runat="server" Text="上網地址" Width="92px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" colspan="3">
                    <cc1:SingleField ID="AreaData" runat="server" Width="100%" Height="64px" MultiLine="True" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBReason" runat="server" Text="申請原因" Width="111px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" colspan="3">
                    <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" MultiLine="True" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
