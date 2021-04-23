<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCReportService_Maintain_SMRA_Detail" %>

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
    <table border=0 width=510px>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="報表格式定義代號" Width="120px" /></td>
        <td >
            <cc1:SingleField ID="SMRAAAA002" runat="server" Width="370px" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="報表格式定義名稱" Width="120px" /></td>
        <td >
            <cc1:SingleField ID="SMRAAAA003" runat="server" Width="370px" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="報表格式檔案" Width="120px" /></td>
        <td >
            <cc1:FileUpload ID="ReportFile" runat="server" Height="111px" Width="369px" OnDeleteData="ReportFile_DeleteData" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="報表格式定義類型" Width="120px" /></td>
        <td >
            <cc1:SingleField ID="SMRAAAA004" runat="server" Width="370px" ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <td valign=top width=120px><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="定義XML" Width="120px" />
            <cc1:GlassButton ID="AnaButton" runat="server" Height="23px" OnClick="AnaButton_Click"
                Text="解析" Width="114px" />
        </td>
        <td >
            <cc1:SingleField ID="SMRAAAA005" runat="server" Height="280px" MultiLine="True" Width="370px" ReadOnly="True" />
        </td>
    </tr>
    <tr>
        <td colspan=2>
            <cc1:DSCCheckBox ID="SMRAAAA008" runat="server" Text="是否開放其他人使用" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
