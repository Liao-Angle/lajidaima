<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_SQIT002_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .style1
        {
            width: 300px;
        }
        .style2
        {
            width: 450px;
        }
        .style3
        {
            font-size: large;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
        <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td align="left" height="30" class="style2">
                    <font style="font-family: 標楷體; font-weight: 700;" class="style1"><span class="style3">
                        服務需求申請單</span></font>
                </td>
                <td>
                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="NO:" Width="60px" Style="margin-left: 0px" />
                </td>
                <td>
                    <cc1:DSCLabel ID="BillNo" runat="server" Width="150px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="客戶公司名稱" Width="109px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="OrgNo" runat="server" Width="266px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="系統名稱" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="SystemNo" runat="server" Width="233px" Height="16px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="需求人員" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="222px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" OnSingleOpenWindowButtonClick="JEmpNo_SingleOpenWindowButtonClick" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="部門" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo" runat="server" Width="135px" Height="16px" ReadOnly="True" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="需求日期" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="Rqdate" runat="server" Width="114px" Height="16px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="需求類別" Width="109px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="RqType" runat="server" Width="114px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="需求項目" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="Rqxm" runat="server" Width="112px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="IT類別" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="OwnerType" runat="server" Width="123px" Height="16px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel19" runat="server" Text="需求說明" Width="696px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="mark" runat="server" Height="209px" Style="margin-right: 71px"
                        MultiLine="True" Width="696px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel20" runat="server" Text="MIS回復" Width="696px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Reason" runat="server" Height="88px" Style="margin-right: 71px"
                        MultiLine="True" Width="696px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="預計系統分析時程" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="fxbg" runat="server" Width="91px" Height="16px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="fxed" runat="server" Width="90px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="預計程式開發時程" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="kfbg" runat="server" Width="96px" Height="16px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="kfed" runat="server" Width="96px" Height="16px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="預計IT測試時程" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="itbg" runat="server" Width="91px" Height="16px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="ited" runat="server" Width="90px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="預計客戶測試時程" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="khbg" runat="server" Width="96px" Height="16px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="khed" runat="server" Width="96px" Height="16px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="預計交付日" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="jfdate" runat="server" Width="96px" Height="16px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="新增程式數量" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="xzcs" runat="server" Width="96px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="新增報表數量" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="xzbb" runat="server" Width="96px" Height="16px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="修改程式數量" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="xgcs" runat="server" Width="96px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="修改報表數量" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="xgbb" runat="server" Width="96px" Height="16px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel25" runat="server" Text="MIS承辦人" Width="96px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="MisOwnerGUID" runat="server" Width="150px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="" Width="142px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCLabel ID="DSCLabel22" runat="server" Text="" Width="142px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
