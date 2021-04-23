<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVU_Detail" %>

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
        <td valign=top style="width: 165px"><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="使用者(不選擇表示系統預設)" Width="181px" /></td>
        <td >
            <cc1:SingleOpenWindowField ID="SMVUAAA002" runat="server" guidField="OID"
            keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users"
            Width="365px" />        
        </td>
    </tr>
    <tr>
        <td style="width: 165px"><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="帳戶比對長度(0不限制)" Width="164px" /></td>
        <td>
            <cc1:SingleField ID="SMVUAAA003" runat="server" isMoney="True" Width="65px" />
        </td>
    </tr>
    <tr>
        <td style="width: 165px"><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="密碼最小長度(最小4)" Width="164px" /></td>
        <td>
            <cc1:SingleField ID="SMVUAAA004" runat="server" isMoney="True" Width="65px" />
        </td>
    </tr>
    <tr>
        <td style="width: 165px"><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="密碼最大長度(最大20)" Width="164px" /></td>
        <td>
            <cc1:SingleField ID="SMVUAAA005" runat="server" isMoney="True" Width="65px" />
        </td>
    </tr>
    <tr>
        <td colspan=2>
            <cc1:DSCCheckBox ID="SMVUAAA006" runat="server" Text="只能包含大寫英文字母(A-Z)" />
        </td>
    </tr>
    <tr>
        <td colspan=2>
            <cc1:DSCCheckBox ID="SMVUAAA007" runat="server" Text="只能包含小寫英文字母(a-z)" />
        </td>
    </tr>
    <tr>
        <td colspan=2>
            <cc1:DSCCheckBox ID="SMVUAAA008" runat="server" Text="只能包含數字(0-9)" />
        </td>
    </tr>
       <tr>
        <td colspan=2>
            <cc1:DSCCheckBox ID="SMVUAAA010" runat="server" Text="不可與前次密碼相同" />
        </td>
    </tr> 
    <tr>
        <td style="width: 165px"><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="密碼有效天數(0不限制)" Width="164px" /></td>
        <td>
            <cc1:SingleField ID="SMVUAAA009" runat="server" isMoney="True" Width="65px" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
