<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPKM003_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>文件作廢</title>

</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <cc1:SingleField ID="ConfidentialLevel" runat="server" Display="False" />
    <cc1:SingleField ID="DocTypeGUID" runat="server" Display="false" />
    <div>
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >       
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>知識文件作廢申請單</b></font></td>
        </tr>
	</table>

    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=5>
                <cc1:SingleField ID="Subject" runat="server" Width="577px" />
            </td>
        </tr>
		<tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="lbcompanyCode" runat="server" Text="公司別" TextAlign="2"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px" colspan="5">
				<cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="138px"  />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="申請人" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="140px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="60px" 
                    FixValueTextWidth="50px" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckBy1GUID" runat="server" Text="審核人一" Width="70px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckBy1GUID" runat="server" Width="140px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="60px" 
                    FixValueTextWidth="50px" 
                    onbeforeclickbutton="CheckBy1GUID_BeforeClickButton" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckBy2GUID" runat="server" Text="審核人二" Width="70px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckBy2GUID" runat="server" Width="141px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="60px" 
                    FixValueTextWidth="50px" 
                    onbeforeclickbutton="CheckBy2GUID_BeforeClickButton" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblVoidDocGUID" runat="server" Text="文件號碼" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=5>
                <cc1:SingleOpenWindowField ID="VoidDocGUID" runat="server" 
                    Width="470px" guidField="GUID" keyField="DocNumber" 
                    serialNum="001" tableName="Document" FixReadOnlyValueTextWidth="300px" 
                    FixValueTextWidth="130px" showReadOnlyField="True" 
                    onsingleopenwindowbuttonclick="VoidDocGUID_SingleOpenWindow" 
                    onbeforeclickbutton="VoidDocGUID_BeforeClickButton" />
                <%--<cc1:GlassButton ID="ViewDocButton" runat="server"  Text="檢視文件" Width="102px" OnClick="ViewDocButton_Click" showWaitingIcon="True" />--%>
                <cc1:GlassButton ID="GlassButtonViewDoc" runat="server" Height="20px" 
                    Text="檢視文件" Width="60px" onclick="GlassButtonViewDoc_Click" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblVoidReason" runat="server" Text="作廢原因" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=5>
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
