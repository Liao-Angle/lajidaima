<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPIT002_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/Enterprise.css" rel="stylesheet" type="text/css" />
    <title>My Default</title>
    <style type="text/css">
        img {  border: 0px} 
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <cc1:SingleField ID="OriginatorUserName" runat="server" Visible="False"/>
    <cc1:SingleField ID="Qa1UserName" runat="server" Visible="False"/>
    <cc1:SingleField ID="Qa2UserName" runat="server" Visible="False"/>
    <cc1:SingleField ID="SystemNameValue" runat="server" Visible="False"/>
    <cc1:SingleField ID="CategoryNameValue" runat="server" Visible="False"/>
    <cc1:SingleField ID="RequesterOrgUnitName" runat="server" Visible="False"/>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>資訊系統上線申請單</b></font></td>
        </tr>
    </table>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr><td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="Subject" runat="server" Width="522px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead" rowspan="2">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="申請人" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail rowspan="2">
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="210px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3" 
                    tableName="Users" Height="80px" FixReadOnlyValueTextWidth="100px" 
                    FixValueTextWidth="80px" IgnoreCase="True" />
            </td>
        
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblQa1" runat="server" Text="QA人員1" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Qa1GUID" runat="server" Width="210px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3" 
                    tableName="Users" Height="80px" FixReadOnlyValueTextWidth="100px" 
                    FixValueTextWidth="80px" IgnoreCase="True" />
            </td>
        </tr>
        <tr>
            <td class=BasicFormHeadHead align="right">
                <cc1:DSCLabel ID="LblQa2" runat="server" Text="QA人員2" Width="85px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="Qa2GUID" runat="server" Width="210px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3" 
                    tableName="Users" Height="80px" FixReadOnlyValueTextWidth="100px" 
                    FixValueTextWidth="80px" IgnoreCase="True" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSystemName" runat="server" Text="系統名稱" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="SystemName" runat="server" Width="200px" 
                    onselectchanged="SystemName_SelectChanged" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCategoryName" runat="server" Text="模組名稱" Width="85px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail style="margin-left: 40px">
                <cc1:SingleDropDownList ID="CategoryName" runat="server" Width="192px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblRequesterOrgUnitGUID" runat="server" Text="需求部門" 
                    Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="RequesterOrgUnitGUID" runat="server" 
                    Width="210px" showReadOnlyField="True" guidField="OID" keyField="id" 
                    serialNum="001" tableName="OrgUnit" FixReadOnlyValueTextWidth="120px" 
                    FixValueTextWidth="60px" IgnoreCase="True" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblInfoDemandGUID" runat="server" Text="需求單單號" 
                    Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail valign=middle>
                <table>
                <tr>
                <td>
                <cc1:SingleOpenWindowField ID="InfoDemandGUID" runat="server" Width="150px" 
                    guidField="GUID" keyField="id" serialNum="001" 
                    tableName="InfoDemand" Height="18px" FixReadOnlyValueTextWidth="0px" 
                    FixValueTextWidth="120px" 
                        onsingleopenwindowbuttonclick="InfoDemandGUID_SingleOpenWindowButtonClick" 
                        IgnoreCase="True" />
                </td>
                <td>
                <cc1:GlassButton ID="GlassButtonViewInfoDemand" runat="server" Height="20px"
                    Text="檢視" Width="43px" onBeforeClick ="GlassButtonViewInfoDemand_BeforeClick" 
                        onbeforeclicks="GlassButtonViewInfoDemand_BeforeClicks" />
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblExpectReleaseDateTime" runat="server" Text="預計上線日期" 
                    Width="90px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="ExpectReleaseDateTime" runat="server" 
                    Width="200px" DateTimeMode="1" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblActualReleaseDateTime" runat="server" Text="實際上線日期" 
                    Width="110px" IsNecessary="True" TextAlign="2" Height="16px" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="ActualReleaseDateTime" runat="server" 
                    Width="195px" DateTimeMode="1" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblProgramName" runat="server" Text="程式/報表名稱" 
                    Width="110px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="ProgramName" runat="server" Height="40px" Width="522px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblIncludeKmDoc" runat="server" Text="是否包含KM文件" 
                    Width="110px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="IncludeKmDoc" runat="server" Width="50" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDocGUID" runat="server" Text="文件編號" 
                    Width="85px" IsNecessary="False" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="DocGUID" runat="server" Width="200px" 
                    guidField="GUID" keyField="DocNumber" serialNum="001" 
                    tableName="Document" FixReadOnlyValueTextWidth="0px" 
                    FixValueTextWidth="160px" Height="16px"
                    onbeforeclickbutton="DocGUID_BeforeClickButton" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead" rowspan=3>
                <cc1:DSCLabel ID="LblReleaseDocuments" runat="server" Text="上線文件" 
                    Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <asp:HyperLink ID="HyperLinkSd" runat="server" ImageUrl="~/Images/Get.gif" BorderWidth="0px"></asp:HyperLink>
                <cc1:DSCLabel ID="LblSdDocFilePath" runat="server" Text="1.SA/SD Document " 
                    Width="184px" IsNecessary="True" TextAlign="2" />&nbsp;&nbsp;
            </td>
            <td class=BasicFormHeadDetail colspan=2>
                <cc1:SingleField ID="SdDocFilePath" runat="server" Width="307px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <asp:HyperLink ID="HyperLinkUt" runat="server" ImageUrl="~/Images/Get.gif"></asp:HyperLink>
                <cc1:DSCLabel ID="LblUtDocFilePath" runat="server" Text="2.Unit Test Report" 
                    Width="184px" IsNecessary="True" TextAlign="2" />&nbsp;&nbsp;
            </td>
            <td class=BasicFormHeadDetail colspan=2>
                <cc1:SingleField ID="UtDocFilePath" runat="server" Width="307px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <asp:HyperLink ID="HyperLinkQa" runat="server" ImageUrl="~/Images/Get.gif"></asp:HyperLink>
                <cc1:DSCLabel ID="LblQaDocFilePath" runat="server" Text="3.QA Report" 
                    Width="184px" IsNecessary="True" TextAlign="2" />&nbsp;&nbsp;
            </td>
            <td class=BasicFormHeadDetail colspan=2>
                <cc1:SingleField ID="QaDocFilePath" runat="server" Width="307px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDescription" runat="server" Text="說明" 
                    Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Description" runat="server" Height="70px" Width="522px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblQaDescription" runat="server" Text="QA說明" 
                    Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="QaDescription" runat="server" Height="70px" Width="522px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblVssDescription" runat="server" Text="VSS說明" 
                    Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="VssDescription" runat="server" Height="70px" Width="522px" 
                    MultiLine="True" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
