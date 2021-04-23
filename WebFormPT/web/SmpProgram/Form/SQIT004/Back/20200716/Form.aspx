<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_SQIT004_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .BasicFormHeadBorder
        {
            width: 670px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
    <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
    <table style="width: 670px;" border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td align="center" height="30">
                <font style="font-family: 標楷體; font-size: large;"><b>資訊需求申請單</b></font>
            </td>
        </tr>
    </table>
    <div>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label style="font-weight: bold;" class="style1">
                        需求資訊</label>
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="單號" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqNO" runat="server" Width="190px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="版本" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="version" runat="server" Width="190" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="填單人/主管" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Creator" runat="server" Width="190px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="填單人部門" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo" runat="server" Width="190" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="申請日期" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqDate" runat="server" Width="190px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="期望完成日期" Width="100px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqPlanDate" runat="server" Width="190" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="申請人" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqOwner" runat="server" Width="190px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="BU" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="BU" runat="server" Width="190" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBEmployeeID" runat="server" Text="需求項目" Width="99px"></cc1:DSCLabel>
                </td>
                <td colspan="5" width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqType" runat="server" Width="555px" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="主旨" Width="99px"></cc1:DSCLabel>
                </td>
                <td colspan="5" width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Subject1" runat="server" Width="555px" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="需求內容" Width="100px"></cc1:DSCLabel>
                </td>
                <td colspan="5" width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqConent" runat="server" Width="555px" Height="71px" MultiLine="True"
                        RowsOfMultiLine="0" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label style="font-weight: bold;" class="style1">
                        需求分析</label>
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="需求等級" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqLv" runat="server" Width="100px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="開發人員" Width="100px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="DevlopOwner" runat="server" Width="100px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="預計完成日期" Width="100px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqMISDate" runat="server" Width="100px" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="計劃SA時數" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PlanHour" runat="server" Width="100px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="開發時數" Width="100px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="DevlopHour" runat="server" Width="100px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="預計費用" Width="100px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PlanCost" runat="server" Width="100px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
