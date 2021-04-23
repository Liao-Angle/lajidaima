<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPAD001_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>My Default</title>
    <style type="text/css">
        .DSCLabel
        {}
    </style>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <cc1:SingleField ID="IsIncludeDateEve" runat="server" />
    <cc1:SingleField ID="TempSerialNo" runat="server" />
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <tr><td align=center>&nbsp;</td>
        </tr>
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>請&nbsp;假&nbsp;單</b></font></td>
        </tr>
    </table>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
<%--        <tr bgcolor="#3399CC">
            <td colspan=4 align="center">
                <img src="../../../Images/hospital.gif" border=0 alt="" align="ABSMIDDLE">&nbsp;
                <font COLOR="white">請&nbsp;&nbsp;&nbsp;&nbsp;假&nbsp;&nbsp;&nbsp;&nbsp;單</font>
            </td>
        </tr>--%>
        <tr>
            <td align="right" class="BasicFormHeadHead" title="主旨">
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="100px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Subject" runat="server" Width="512px" />
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCompanyCode" runat="server" Text="公司別" Width="100px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="182px" 
                    Font-Strikeout="False" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOrganizationUnit" runat="server" Text="請假人員部門" 
                    Width="100px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OrganizationUnitGUID" runat="server" 
                    Width="220px" showReadOnlyField="True" guidField="OID" keyField="id" 
                    serialNum="001" tableName="OrgUnit" IgnoreCase="True" />
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="請假人員" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="220px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
                    idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton" IgnoreCase="True" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDeputy" runat="server" Text="代理人員" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="DeputyGUID" runat="server" Width="220px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" 
                    onsingleopenwindowbuttonclick="DeputyGUID_SingleOpenWindowButtonClick" 
                    idIndex="2" valueIndex="3" IgnoreCase="True" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblIsCustomFlow" runat="server" Text="流程類別" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="IsCustomFlow" runat="server" Width="182px" />
            </td>
			<td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblClassCode" runat="server" Text="班別" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="ClassCode" runat="server" Width="182px" 
                    onselectchanged="ClassCode_SelectChanged" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCategory" runat="server" Text="假別" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="CategoryCode" runat="server" Width="182px" 
                    onselectchanged="CategoryCode_SelectChanged" />
            </td>
			<td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblIsIncludeHoliday" runat="server" Text="請假含假日" Width="85px" CssClass="DSCLabel" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="IsIncludeHoliday" runat="server" Width="182px" 
                    onselectchanged="IsIncludeHoliday_SelectChanged" />
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblStartDate" runat="server" Text="起始日期" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="StartDate" runat="server" Width="185px" 
                    ondatetimeclick="StartDate_DateTimeClick" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEndDate" runat="server" Text="截止日期" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="EndDate" runat="server" Width="185px" 
                    ondatetimeclick="EndDate_DateTimeClick" />
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblStartTime" runat="server" Text="起始時間" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="StartTime" runat="server" Width="185px" 
                    ondatetimeclick="StartTime_DateTimeClick" DateTimeMode="4" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEndTime" runat="server" Text="截止時間" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="EndTime" runat="server" Width="185px" 
                    ondatetimeclick="EndTime_DateTimeClick" DateTimeMode="4" />
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblHours" runat="server" Text="請假時數" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Hours" runat="server" Width="70px" />&nbsp;
                 <cc1:DSCLabel ID="LblHoursDesc" runat="server" Text="" Width="140px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginatorTitle" runat="server" Text="請假人員職稱" 
                    Width="100px" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="OriginatorTitle" runat="server" Width="160px" />
            </td>
        </tr>
        <tr><td rowspan=2 class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDescription" runat="server" Text="說明" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:GlassButton ID="ButtonSearch" runat="server" Height="20px" Width="512px" 
                    Text="查詢休假明細" Display="True" onbeforeclicks="ButtonSearch_BeforeClicks" />
            </td></tr>
        <tr>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Description" runat="server" Height="70px" Width="512px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckby1" runat="server" Text="審核人一" Width="85px" IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Checkby1GUID" runat="server" Width="220px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                    tableName="Users" IgnoreCase="True" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckby2" runat="server" Text="審核人二" Width="85px" IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Checkby2GUID" runat="server" Width="220px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                    tableName="Users" IgnoreCase="True" />
            </td>
        </tr>
        <tr>
	        <td colspan=4 class=BasicFormHeadDetail>
                <font size="2">****註: 審核人不須選取部門主管(系統自行判斷)! 若部門有審核人(如: 主任/副理) 請選取!</font></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
