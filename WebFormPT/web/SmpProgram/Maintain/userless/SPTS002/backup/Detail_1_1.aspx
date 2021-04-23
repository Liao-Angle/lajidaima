<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail_1_1.aspx.cs" Inherits="SmpProgram_Maintain_SPKM001_Detail_1_1" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>明細</title>
</head>
<body>
    <form id="form1" runat="server">
   <fieldset>
       <legend>文件分類設定作業-管理者</legend>
    <table>
        <tr>
            <td>
                <cc1:DSCLabel ID="lblid" runat="server" Width="90px" Text="管理者" />
            </td>
            <td>
                <cc1:SingleOpenWindowField ID="MajorTypeAdmUserGUID" runat="server" showReadOnlyField="True"
                    Width="254px" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
            </td>
        </tr>
    </table>
    </fieldset>
    </form>
</body>
</html>
