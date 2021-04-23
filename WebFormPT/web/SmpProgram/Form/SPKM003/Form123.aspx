<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPKM003_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>文件作廢</title>
    <style type="text/css">
        .style1
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
            background-color: white;
            font-size: 9pt;
            font-family: Arial;
            width: 379px;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbSubject" runat="server" Text="主旨" Width="70px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="Subject" runat="server" Width="577px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbCheckBy1" runat="server" Text="審核人一" Width="70px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckBy1GUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="80px" 
                    FixValueTextWidth="50px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbCheckBy2" runat="server" Text="審核人二" Width="70px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=style1>
                <cc1:SingleOpenWindowField ID="CheckBy2GUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="80px" 
                    FixValueTextWidth="50px" />
            </td>
            
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbOriginator" runat="server" Text="申請人" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="80px" 
                    FixValueTextWidth="50px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbDocGUID" runat="server" Text="作廢文件" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=style1>
                <cc1:SingleOpenWindowField ID="DocGUID" runat="server" 
                    Width="200px" guidField="GUID" keyField="DocNumber" 
                    serialNum="001" tableName="Document" FixReadOnlyValueTextWidth="100px" 
                    FixValueTextWidth="90px" showReadOnlyField="True" 
                    onsingleopenwindowbuttonclick="VoidDocGUID_SingleOpenWindow" />
            </td>

        </tr>
        
        <tr>
            
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbStatus" runat="server" Text="狀態" Width="70px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="Status" runat="server" Width="90px" ReadOnly="True" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbRevGUID" runat="server" Text="版本" Width="70px" IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="RevGUID" runat="server" Width="90px" ReadOnly="True" />
            </td>
            
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbVoidReason" runat="server" Text="作廢原因" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=3>
            <cc1:SingleField ID="VoidReason" runat="server" Width="577px" Height="60px" 
                    MultiLine="True" />
            </td>
        </tr>
    </table>

    </div>
    </form>
    <p>
&nbsp;&nbsp;&nbsp;
    </p>
</body>
</html>
