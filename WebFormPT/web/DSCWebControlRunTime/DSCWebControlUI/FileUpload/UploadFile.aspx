<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFile.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_FileUpload_UploadFile" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <script src="../../../JS/ShareScript.js" language="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border=0 cellpadding=0 cellspacing=2>
    <tr>
        <td><cc1:dsclabel id="DSCLabel1" runat="server" text="選擇上傳檔案" width="100px"></cc1:dsclabel></td>
        <td><asp:FileUpload ID="FUP" runat="server" CssClass="SingleField" Width="296px" /></td>
    </tr>
    <tr>
        <td><cc1:dsclabel id="DSCLabel2" runat="server" text="檔案說明" width="100px"></cc1:dsclabel></td>
        <td><cc1:SingleField ID="Description" runat="server" Width="296px" /></td>
    </tr>
    <tr>
        <td colspan=2 align=center>
            <asp:Button ID="ConfirmUpload" runat="server" CssClass="GlassButton" Text="確認上傳" Height="28px" OnClick="ConfirmUpload_Click" /></td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
