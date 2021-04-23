<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPERP005_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/Enterprise.css" rel="stylesheet" type="text/css" />
    <title>My Default</title>
    <style type="text/css">

    </style>
    <script language="javascript" src="../../../JS/jquery-1.4.1.min.js"></script>

    <script language="javascript">
        function onViewInvMmt(headerId) {
            var url = $("#ViewInvMmtUrl").val();
            var param = { ImHeaderId: headerId };
            OpenWindowWithPost(url, 'height=680, width=1000, top=100, left=100, toolbar=no, menubar=no, location=no, status=no,scrollbars=yes', 'ViewInvMmt', param);
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
    <cc1:SingleField ID="SheetNo" runat="server" Display="False" ReadOnly="True" />    
    <cc1:SingleField ID="OrgId" runat="server" Display="False" />
    <cc1:SingleField ID="EcpTrxTypeId" runat="server" Width="1px" />        
	<cc1:SingleField ID="HeaderId" runat="server" Display="False" />
    <cc1:SingleField ID="RequestorNum" runat="server" Display="False" />
    <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="120px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="110px" 
                    FixValueTextWidth="80px" />
    <cc1:SingleOpenWindowField ID="CreatorGUID" runat="server" Width="120px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="110px" 
                    FixValueTextWidth="80px" />    
    <asp:HiddenField ID="ViewInvMmtUrl" runat="server" />
    
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <tr><td align=center><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/smp-logo.jpg"/></td>
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
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbOrgName" runat="server" Text="組織" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="OrgName" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbRequestorName" runat="server" Text="需求者" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="RequestorName" runat="server" Width="340px" />
            </td>
            
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbTrxTypeName" runat="server" Text="單別" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="TrxTypeName" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbRequestorDept" runat="server" Text="需求者部門" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="RequestorDept" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbLineQuantity" runat="server" Text="筆數" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="LineQuantity" runat="server" Width="120px" />
            </td>
            
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbInvMmtNum" runat="server" Text="單號" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="InvMmtNum" runat="server" Width="120px" Display="False" />
                <asp:HyperLink ID="hlInvMmtNum" runat="server">INV Number</asp:HyperLink>
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbVersion" runat="server" Text="版本" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Version" runat="server" Width="120px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbSumQuantity" runat="server" Text="合計數量" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="SumQuantity" runat="server" Width="120px" />
            </td>
            
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbComments" runat="server" Text="備註" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail colspan="5" >
                <cc1:SingleField ID="Comments" runat="server" Width="560px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbCheckby1GUID" runat="server" Text="審核人1" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Checkby1GUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="Checkby1GUID_BeforeClickButton" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbCheckby2GUID" runat="server" Text="審核人2" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Checkby2GUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="Checkby2GUID_BeforeClickButton" />  
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbRdMemberGUID" runat="server" Text="RD" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="RdMemberGUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="RdMemberGUID_BeforeClickButton" /> 
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbMeMemberGUID" runat="server" Text="AME/ME" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="MeMemberGUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="MeMemberGUID_BeforeClickButton" /> 

            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbPmMemberGUID" runat="server" Text="PM" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="PmMemberGUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="PmMemberGUID_BeforeClickButton" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbSaleMemberGUID" runat="server" Text="業務人員" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="SaleMemberGUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="SaleMemberGUID_BeforeClickButton" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbMcMemberGUID" runat="server" Text="物管" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="McMemberGUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="McMemberGUID_BeforeClickButton" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbQaMemberGUID" runat="server" Text="品管" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="QaMemberGUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="QaMemberGUID_BeforeClickButton" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbSaleManagerGUID" runat="server" Text="業務主管" Width="85px"  TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="SaleManagerGUID" 
                    runat="server" Width="120px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="SaleManagerGUID_BeforeClickButton" />
            </td>
        </tr>
    </table>
    <br>
    <cc1:OutDataList ID="DataListLine" runat="server" Height="172px" Width="663px" 
            style="margin-right: 0px" isShowAll="True" IsShowCheckBox="False" 
            showExcel="True" showSerial="True" />
    </div>
    </form>
</body>
</html>
