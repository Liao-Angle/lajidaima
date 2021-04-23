<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPERP004_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/Enterprise.css" rel="stylesheet" type="text/css" />
    <title>My Default</title>
    <style type="text/css">

        .style1
        {
            width: 655px;
        }

    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <cc1:SingleField ID="ShipmentHeaderId" runat="server" Width="1px" />
    <cc1:SingleField ID="FlowId" runat="server" Width="1px" />
    <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="230px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="110px" 
                    FixValueTextWidth="80px" />
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <tr><td align=center>&nbsp;</td>
        </tr>
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>庶&nbsp;務&nbsp;收&nbsp;料&nbsp;單</b></font></td>
        </tr>
    </table>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr><td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="85px"  TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=5>
                <cc1:SingleField ID="Subject" runat="server" Width="560px" />
            </td>
        </tr>
        <tr>
            <td class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LblSetOfBookName" runat="server" Text="公司別" Width="80px" TextAlign="2"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail colspan="5">
                <cc1:SingleField ID="SetOfBookName" 
                    runat="server" Height="20px" Width="120px" /></td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblReceptNum" runat="server" Text="進貨單號" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="ReceptNum" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblVatCode" runat="server" Text="課稅別" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="VatCode" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblVatRegistrationNum" runat="server" Text="統一編號" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="VatRegistrationNum" runat="server" Width="120px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDueDate" runat="server" Text="進貨日期" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="DueDate" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblPaymentName" runat="server" Text="付款條件" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="PaymentName" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblPackingSlip" runat="server" Text="發票號碼" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="PackingSlip" runat="server" Width="120px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblVendorName" runat="server" Text="供應商" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="VendorName" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblRate" runat="server" Text="匯率" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Rate" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblShippedDate" runat="server" Text="發票日期" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="ShippedDate" runat="server" Width="120px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblComments" runat="server" Text="備註" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Comments" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblWaybillAirbillNum" runat="server" Text="進口報單" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="WaybillAirbillNum" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCurrencyCode" runat="server" Text="幣別" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="CurrencyCode" runat="server" Width="120px" />
            </td>
        </tr>
    </table>
    <br />
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lblAccepterId" runat="server" Text="驗收人員" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="AccepterId" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    FixReadOnlyValueTextWidth="50px" IgnoreCase="True" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lblAccepter1GUID" runat="server" Text="審核人一" Width="85px"  
                    TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Accepter1GUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    FixReadOnlyValueTextWidth="50px" IgnoreCase="True" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lblAccepter2GUID" runat="server" Text="審核人二" Width="85px"  
                    TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Accepter2GUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    FixReadOnlyValueTextWidth="50px" IgnoreCase="True" />
            </td>
        </tr>
        <br>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lblAccepter3GUID" runat="server" Text="審核人三" Width="85px"  
                    TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Accepter3GUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    FixReadOnlyValueTextWidth="50px" IgnoreCase="True" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lblAccepter4GUID" runat="server" Text="審核人四" Width="85px"  
                    TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Accepter4GUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    FixReadOnlyValueTextWidth="50px" IgnoreCase="True" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lblAccepter5GUID" runat="server" Text="審核人五" Width="85px"  
                    TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Accepter5GUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    FixReadOnlyValueTextWidth="50px" IgnoreCase="True" />
            </td>
        </tr>
        <tr>
	        <td colspan=6 class=BasicFormHeadDetail>
                <font size="2">註: 驗收流程只需需求者簽核驗收，若要增加驗收審核人員 (如：主任/副理/經理) 請選取，流程將依序進行簽核!</font></td>
        </tr>
    </table>
    <br>
    <cc1:OutDataList ID="DataListLine" runat="server" Height="172px" Width="663px" style="margin-right: 0px" />
    <br>
    <table width="660px">
        <tr><td align=right><font size="2">P07-001-H</font></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
