<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail_2_3_1.aspx.cs" Inherits="SmpProgram_Maintain_SPKM001_Detail_2_3_1" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>輸入</title>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset>
       <legend>文件分類設定作業-子分類-預設讀者</legend>
    <table>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblBelongGroupGUID" runat="server" Text="預設讀者" Width="90px"></cc1:DSCLabel>
            </td>
            <td>
                <cc1:SingleOpenWindowField ID="BelongGroupGUID" runat="server" showReadOnlyField="True"
                    Width="254px" guidField="OID" keyField="id" serialNum="002" tableName="SPKM001" />
            </td>
        </tr>
    </table>
    </fieldset>
    </form>
</body>
</html>
