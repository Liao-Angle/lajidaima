<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVEU_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>系統公告檢視畫面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table style="margin-left:4px" border="0" width="550px" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
        <tr>
            <td width="105px" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="公告發布日期：" Width="100%" TextAlign="2" />
            </td>
            <td Width="155px" class="BasicFormHeadDetail">
                <cc1:SingleField ID="SMVEAAA002" runat="server" Width="100%" ReadOnly="True" />
            </td>
            <td width="105px" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="公告有效期限：" Width="100%" TextAlign="2" />
            </td>
            <td Width="155px" class="BasicFormHeadDetail">
                <cc1:SingleField ID="SMVEAAA003" runat="server" Width="100%" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="公告標題：" Width="100%" TextAlign="2" />
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleField ID="SMVEAAA005" runat="server" Width="100%" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="公告內容：" Width="100%" TextAlign="2" />
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:DSCRichEdit ID="SMVEAAA006" runat="server" Width="425px" Height="310px" MultiLine="True" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="公告附件：" Width="100%" Display="false" TextAlign="2" />
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:FileUpload ID="attachFile" runat="server" Height="160px" Width="100%" Display="false"/>
            </td>
        </tr>
      </table>
    </div>
    </form>
</body>
</html>
