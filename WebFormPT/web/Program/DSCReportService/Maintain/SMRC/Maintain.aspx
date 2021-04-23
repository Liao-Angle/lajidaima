<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCReportService_Maintain_SMRC_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border=0 cellpadding=0 cellspacing=3>
    <tr>
        <td>
            <cc1:GlassButton ID="SaveButton" Text="儲存" ImageUrl="~/Images/OK.gif" runat="server" Width="110px" OnClick="SaveButton_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="Webi報表整合設定" Width="700px">
            <table border=0 cellspacing=1 cellpadding=0>
            <tr>
                <td><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="權限/帳號對應模式:" Width="136px" /></td>
                <td><cc1:SingleDropDownList ID="SMRCAAA002" runat="server" Width="216px" />
                    <cc1:DSCCheckBox ID="SMRCAAA007" runat="server" Text="使用Trusted Authentication" />
                </td>
            </tr>
            <tr>
                <td><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="OpenDocument網址:" Width="136px" /></td>
                <td><cc1:SingleField ID="SMRCAAA003" runat="server" Width="542px" /></td>
            </tr>
            <tr>
                <td><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="整合驗證網址:" Width="136px" /></td>
                <td><cc1:SingleField ID="SMRCAAA004" runat="server" Width="542px" /></td>
            </tr>
            <tr>
                <td><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="InfoView網址:" Width="136px" /></td>
                <td><cc1:SingleField ID="SMRCAAA005" runat="server" Width="542px" /></td>
            </tr>
            <tr>
                <td><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="CMC網址:" Width="136px" /></td>
                <td><cc1:SingleField ID="SMRCAAA006" runat="server" Width="542px" /></td>
            </tr>
            </table>
            </cc1:DSCGroupBox>        
        </td>
    </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
