<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPERP002_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>費用請款單</title>
    <style type="text/css">
        .styleHeader
        {
	        font-family:Arial;
	        font-size:16pt;
	        border-style:solid;
	        border-width:1px;
	        border-top-style:none;
	        border-left-style:solid
        }
        
        .style1
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
            background-color: white;
            font-size: 9pt;
            font-family: Arial;
        }
        .style2
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
            background-color: white;
            font-size: 9pt;
            font-family: Arial;
        }
        
        .style3
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
            background-color: rgb(225,217,196);
            font-size: 9pt;
            font-family: Arial;
            width: 82px;
        }
        
        .style4
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
            background-color: white;
            font-size: 9pt;
            font-family: Arial;
        }
        .style5
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
            background-color: white;
            font-size: 9pt;
            font-family: Arial;
            height: 12px;
        }
        .style6
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
	        border-color:rgb(188,178,147);
	        background-color:rgb(225,217,196);
            font-size: 9pt;
            font-family: Arial;
            height: 12px;
        }
    </style>

<script language="javascript" src="../../../JS/jquery-1.4.1.js"></script>
    <script language="javascript" src="../../../JS/jquery-1.4.1.min.js"></script>

    <script language="javascript">
        function onViewPo(headerId) {
            var url = $("#ViewPoUrl").val();
            var param = { PoNumber: headerId };
            OpenWindowWithPost(url, 'height=680, width=1000, top=100, left=100, toolbar=no, menubar=no, location=no, status=no', 'ViewPr', param);
        }

        function onViewAttach(entityName, pk1Value, modify, title) {
            var url = $("#ViewAttachUrl").val();
            var param = { EntityName: entityName, Pk1Value: pk1Value, Modify: modify, Title: title };
            OpenWindowWithPost(url, 'height=420, width=782, top=100, left=100, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no', 'ViewAttach', param);
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

<body>
<table border="0" width="666px" id="table1" bgcolor="#ffffff" cellspacing="1" bordercolor="#000000" cellpadding="1">
<tr>
<td style="border-style: solid; border-width: 1px; width: 655px; word-wrap: break-word"  align="center">

<form id="form1" runat="server">
    <asp:HiddenField ID="SourceId" runat="server" />
    <cc1:SingleField ID="SheetNo" runat="server" Display="False" ReadOnly="True" />
    <cc1:SingleField ID="Subject" runat="server" Display="False" />
	<cc1:SingleField ID="CheckBy" runat="server" Display="False" />
    <asp:HiddenField ID="ViewAttachUrl" runat="server" />
    <asp:HiddenField ID="ViewPoUrl" runat="server" />
    
        <%--<table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>--%>
        <table>
        <tr>
            <td align=center colspan="6" align="center" class=styleHeader>
                <!--<img src="../../../Images/smp-logo.jpg" border=0 alt="" width="312" height="57"><br>-->                
				<b>費用請款單<b>
            </td>
        </tr>      
		<tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="lbcompanyCode" runat="server" Text="公司別"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px" colspan="5">
                <cc1:SingleField ID="CompanyCode" runat="server" Height="20px" width="330px" />
            </td>
        </tr>

        <tr>
            <td class=BasicFormHeadHead>
                <cc1:DSCLabel ID="PoNumberlb" runat="server" Text="採購單號" Width="70px"></cc1:DSCLabel>                
            </td>
            <td class=style2>
                <table border=0 cellspacing="0" cellpadding="0">
                    <tr>
                        <td class=style2>
                            <%--<cc1:SingleField ID="PoNumber" runat="server" Width="80px" Height="20px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" /> --%>
                            <cc1:SingleField ID="PoNumber" runat="server" Height="20px" />
                            <%--<asp:HiddenField ID="PoNumber" runat="server" />--%>
                            <asp:HyperLink ID="hlPoNumber" runat="server">PO Number</asp:HyperLink>
                            
                        </td>
                        <td align=right class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="VersionLb" runat="server" Text="版次" Width="30px"></cc1:DSCLabel>
                        </td>
                        <td>
                            <cc1:SingleField ID="PoVersion" runat="server" Width="20px" Height="20px"  /> 
                        </td>
                    </tr>
                </table>                
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="Currencylb" runat="server" Text="交易幣別" Width="70px"></cc1:DSCLabel>
            </td>
            <td class=style1 >
                <table border=0 cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <cc1:SingleField ID="Currency" runat="server" Width="40px" Height="20px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" /> 
                        </td>
                        <td align=right class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="匯率" Width="30px"></cc1:DSCLabel>
                        </td>
                        <td>
                            <cc1:SingleField ID="Rate" runat="server" Width="50px" Height="20px"  /> 
                        </td>
                    </tr>
                </table> 
            </td>
            <td align=left class=BasicFormHeadHead>
                <cc1:DSCLabel ID="TaxCodelb" runat="server" Text="課稅別" Width="70px"></cc1:DSCLabel>
            </td>
            <td class=style4 >
                <cc1:SingleField ID="TaxCode" runat="server" Height="20px" width="100px" />                
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="PoCreateDatelb" runat="server" Text="採購日期"></cc1:DSCLabel>
            </td>
            <td class=style2>
                <cc1:SingleDateTimeField ID="PoCreateDate" runat="server" />
                <%--<cc1:SingleField ID="PoCreateDate" runat="server" Height="20px" Width="100px" />--%>
            </td>		
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="PoMemberlb" runat="server" Text="採購人員"></cc1:DSCLabel> 
            </td>
            <td class=style1>
                <cc1:SingleOpenWindowField ID="PurMemberGUID" runat="server" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                    tableName="Users" IgnoreCase="True"/>
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="PaymentTermlb" runat="server" Text="付款條件"></cc1:DSCLabel>  
            </td>
            <td class=style5>
                <cc1:SingleField ID="PaymentTerm" runat="server" Height="20px" width="100px" />  
            </td> 
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="SupplierNumlb" runat="server" Text="廠商代號"></cc1:DSCLabel>
            </td>
            <td class=style2>
                <cc1:SingleField ID="SupplierNum" runat="server" Height="20px" width="100px" />
            </td>
            <td align=right class=BasicFormHeadHead >
                <cc1:DSCLabel ID="Organizationlb" runat="server" Text="廠別"></cc1:DSCLabel> 
            </td>
            <td class=style1 colspan="3">
                <cc1:SingleField ID="Organization" runat="server" Height="20px" width="100px" /> 
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="SupplierNamelb" runat="server" Text="廠商全名"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px" colspan="3">
                <cc1:SingleField ID="SupplierName" runat="server" Height="20px" width="330px" />
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="申請人"></cc1:DSCLabel>
            </td>
            <td class=style5>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="Users" IgnoreCase="True"/>
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="審核人1"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px">
                <cc1:SingleOpenWindowField ID="CheckBy1" runat="server" 
                    showReadOnlyField="True" guidField="OID" keyField="id" 
                     serialNum="003" idIndex="2" valueIndex="3" tableName="Users" 
                    onbeforeclickbutton="CheckBy1_BeforeClickButton" IgnoreCase="True"/>
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="審核人2"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px" colspan="3">
                <cc1:SingleOpenWindowField ID="CheckBy2" runat="server" 
                    showReadOnlyField="True" guidField="OID" keyField="id" 
                     serialNum="003" idIndex="2" valueIndex="3" tableName="Users" 
                    onbeforeclickbutton="CheckBy2_BeforeClickButton" IgnoreCase="True"/>
            </td>
            <%--<td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="原幣總額"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px" >
                <cc1:SingleField ID="SingleField3" runat="server" Height="20px" width="100px" alignRight="true" Fractor="2" isMoney="True"  />
            </td>--%>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="原幣未稅金額"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px">
                <cc1:SingleField ID="EnterNonTaxAmount" runat="server" Height="20px" width="100px" alignRight="true" Fractor="2" isMoney="True"  />
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="原幣稅額"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px">
                <cc1:SingleField ID="EnterTaxAmount" runat="server" Height="20px" width="100px" alignRight="true" Fractor="2" isMoney="True"  />
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="原幣總額"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px" >
                <cc1:SingleField ID="EnterAmount" runat="server" Height="20px" width="100px" alignRight="true" Fractor="2" isMoney="True"  />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="本幣未稅金額"></cc1:DSCLabel>
            </td>
            <td class=style5>
                <cc1:SingleField ID="FunctionNoTaxAmount" runat="server" Height="20px" width="100px" alignRight="true" isMoney="True" />
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="FunctionTaxAmountlb" runat="server" Text="本幣稅額"></cc1:DSCLabel>
            </td>
            <td class=style5>
                <cc1:SingleField ID="FunctionTaxAmount" runat="server" Height="20px" width="100px" alignRight="true" isMoney="True" />
            </td>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="本幣總額"></cc1:DSCLabel>
            </td>
            <td class=style5>
                <cc1:SingleField ID="FunctionAmount" runat="server" Height="20px" width="100px" alignRight="true" isMoney="True" />
            </td>
        </tr>
        <tr>
           <td align=right class=BasicFormHeadHead  style="height: 12px">
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="備註"></cc1:DSCLabel>  
            </td>
            <td class=style5 colspan="5">
                <cc1:SingleField ID="Remark" runat="server" Height="20px" Width="500px" />  
            </td>         
        </tr>

        </table>

    <table style="margin-left:4px" border=0 width=655px cellspacing=0 cellpadding=1 >
        <tr>
            <td class=styleHeader>
                   <cc1:DataList ID="DetailList" runat="server" Height="242px" Width="650px"/>
                    </cc1:DSCTabControl>
            </td>
        </tr>
    </table>
    <br>

</form>
</td></tr></table>
</body>
</html>
