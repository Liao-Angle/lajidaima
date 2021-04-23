<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail_1_2.aspx.cs" Inherits="SmpProgram_Maintain_SPKM001_Detail_1_2" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>輸入</title>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
            <legend>文件分類設定作業-子分類</legend>
            <table>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="lblName" runat="server" Text="子分類名稱" Width="90px"></cc1:DSCLabel>
                    </td>
                    <td>
                        <cc1:SingleField ID="Name" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="lblDesc" runat="server" Text="子分類描述" Width="90px"></cc1:DSCLabel>
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
            </table>
        </fieldset>
    </form>
</body>
</html>
