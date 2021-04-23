<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocTypeDetail.aspx.cs" Inherits="SmpProgram_Maintain_SPKM002_DocTypeDetail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>輸入</title>
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table id="ToolBar" border="0" cellspacing="0" cellpadding="0" width="100%" height="25px">
            <tr>
                <td>
                    <cc1:GlassButton ID="SaveButton" runat="server" ImageUrl="~/Images/OK.gif" Text="儲存"
                        Width="80px" ConfirmText="你確定要儲存設定嗎?" OnClick="SaveButton_Click" />
                </td>
            </tr>
        </table>

        <hr />
        <fieldset>
            <legend>文件分類設定作業-子分類-文件類別</legend>
            <table>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="lblName" runat="server" Text="文件類別名稱" Width="90px"></cc1:DSCLabel>
                    </td>
                    <td>
                        <cc1:SingleField ID="Name" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="lblDesc" runat="server" Text="文件類別描述" Width="90px"></cc1:DSCLabel>
                    </td>
                    <td>
                        <cc1:SingleField ID="Desc" runat="server" Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="lblEnable" runat="server" Text="生失效" Width="90px"></cc1:DSCLabel>
                    </td>
                    <td>
                        <cc1:SingleDropDownList ID="Enable" runat="server" Width="50px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="lblExternal" runat="server" Text="是否包含外部文件" Width="90px"></cc1:DSCLabel>
                    </td>
                    <td>
                        <cc1:SingleDropDownList ID="External" runat="server" Width="50px" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </form>
</body>
</html>
