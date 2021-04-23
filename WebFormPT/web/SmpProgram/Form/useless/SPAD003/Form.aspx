<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPAD003_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>My Default</title>
    <style type="text/css">
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <cc1:SingleField ID="OriginatorGUID" runat="server" Display="False" Width="169px" />
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <tr><td align=center>&nbsp;</td>
        </tr>
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>加&nbsp;班&nbsp;單</b></font></td>
        </tr>
    </table>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr><td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td colspan="3" class=BasicFormHeadDetail>
            <cc1:SingleField ID="Subject" runat="server" Width="550px" />
            </td>
        </tr>
        <tr><td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCompanyCode" runat="server" Text="公司別" Width="100px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="182px" 
                    Font-Strikeout="False" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCreationDateTime" runat="server" Text="申請日期" Width="85px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="CreationDateTime" runat="server" Width="170px" 
                    ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOrganizationUnit" runat="server" Text="申請單位" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OrganizationUnitGUID" runat="server" 
                    Width="200px" showReadOnlyField="True" guidField="OID" keyField="id" 
                    serialNum="001" tableName="OrgUnit" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckby" runat="server" Text="審核人" Width="85px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckbyGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" idIndex="2" valueIndex="3" />
            </td>
        </tr>
        </table>
    <br>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblUserGUID" runat="server" Text="工號" Width="110px" 
                    IsNecessary="True" /></td>
            <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblStartDateTime" runat="server" Text="起始時間" Width="100px" 
                    IsNecessary="True" /></td>
            <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblEndDateTime" runat="server" Text="截止時間" Width="100px" 
                    IsNecessary="True" /></td>   
            <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblHours" runat="server" Text="時數" Width="35px" 
                    IsNecessary="True" /></td>
            <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblReason" runat="server" Text="加班原因" Width="89px" 
                    IsNecessary="True" /></td>     
            <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblRemark" runat="server" Text="備註" Width="66px" /></td>
        </tr>
        <tr>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="UserGUID" runat="server" Width="145px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" idIndex="2" valueIndex="3" 
                    EnableTheming="True" IgnoreCase="True" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="StartDateTime" runat="server" 
                    Width="145px" DateTimeMode="1" ondatetimeclick="DateTimeClick" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="EndDateTime" runat="server" 
                    Width="145px" DateTimeMode="1" ondatetimeclick="DateTimeClick" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Hours" runat="server" Width="33px" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Reason" runat="server" Width="99px" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Remark" runat="server" Width="67px" />
            </td>
        </tr>
        <tr>
            <td colspan=7 class=BasicFormHeadDetail>
                <cc1:OutDataList ID="OvertimeDetailList" runat="server" Width="650px" 
                    OnSaveRowData="OvertimeDetailList_SaveRowData" 
                    OnShowRowData="OvertimeDetailList_ShowRowData"
                    DialogHeight="460" />
            </td>
        </tr>
    </table>
    </div>
    
    </form>
</body>
</html>
