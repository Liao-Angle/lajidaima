<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVE_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>程式代碼輸入畫面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table style="margin-left:4px" border="0" width="550px" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
        <tr>
            <td width="115px" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="公告發布日期" Width="100%" TextAlign="2" />
            </td>
            <td Width="160px" class="BasicFormHeadDetail">
                <cc1:SingleDateTimeField ID="SMVEAAA002" runat="server" Width="100%" />
            </td>
            <td width="115px" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="公告有效期限" Width="100%" TextAlign="2" />
            </td>
            <td Width="160px" class="BasicFormHeadDetail">
                <cc1:SingleDateTimeField ID="SMVEAAA003" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="是否上線" Width="100%" TextAlign="2" />
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleDropDownList ID="SMVEAAA004" runat="server" ReadOnly="False" Width="150px" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="公告標題" Width="100%" TextAlign="2" />
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleField ID="SMVEAAA005" runat="server" Width="100%" ReadOnly="False" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead" colspan="4">
                <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="(公告內容於預設公告顯示區為純文字格式，使用者點選進入全文後才以格式文字顯示)" Width="100%" TextAlign="0" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="公告內容" Width="100%" TextAlign="2" />
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:DSCRichEdit ID="SMVEAAA006" runat="server" Width="435px" Height="310px" ReadOnly="False" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="公告部門" Width="100%" TextAlign="2" />
            </td>
            <td colspan="2" class="BasicFormHeadDetail">
                <cc1:SingleOpenWindowField ID="SMVEAAA007" runat="server" showReadOnlyField="True" Width="100%" guidField="OID" keyField="id" serialNum="001" tableName="OrgUnit" />
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:GlassButton ID="ToAllButton" runat="server" Text="公佈至所有人" Width="100px" OnClick="ToAllButton_Click" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="是否包含子部門" Width="100%" TextAlign="2" />
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleDropDownList ID="SMVEAAA008" runat="server" ReadOnly="False" Width="150px" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="公告附件" Width="100%" TextAlign="2" />
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:FileUpload ID="attachFile" runat="server" Height="130px" Width="100%"/>
            </td>
        </tr>
      </table>
    </div>
    </form>
</body>
</html>
