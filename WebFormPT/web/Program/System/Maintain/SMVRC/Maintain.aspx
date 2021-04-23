<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_System_Maintain_SMVRC_Maintain" %>

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
        <cc1:GlassButton ID="SaveButton" runat="server" ImageUrl="~/Images/OK.gif" Text="儲存"
            Width="146px" ConfirmText="你確定要儲存設定嗎?" OnClick="SaveButton_Click" />
        <br />
        <br />
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Height="372px" Text="國定假日設定" Width="630px">
        <table border=0 width=100% cellspacing=5>
            <tr>
                <td style="width: 97px">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="國定假日名稱" />
                </td>
                <td style="width: 116px">
                    <cc1:SingleField ID="SMVRAAC002" runat="server" />
                </td>
                <td>
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="日期" />
                </td>
                <td style="width: 113px">
                    <cc1:SingleDropDownList ID="SMVRAAC0031" runat="server" Width="78px" />
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="月" Width="25px" />
                </td>
                <td>
                    <cc1:SingleDropDownList ID="SMVRAAC0032" runat="server" Width="67px" />
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="日" Width="29px" />
                </td>
            </tr>
        </table>
            <cc1:OutDataList ID="ACTable" runat="server" Height="313px" OnSaveRowData="ACTable_SaveRowData"
                OnShowRowData="ACTable_ShowRowData" showSerial="True" Width="620px" />
        </cc1:DSCGroupBox>
        &nbsp;</div>
    </form>
</body>
</html>
