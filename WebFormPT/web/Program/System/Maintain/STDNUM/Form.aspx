<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_System_Maintain_STDNUM_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTDNUM001" runat="server" Text="歸檔號編碼"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="STDNUM001" runat="server" Width="100%" />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
            </td>
        </tr>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTDNUM002" runat="server" Text="有效時間(起)"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="STDNUM002" runat="server" Width="100%" DateTimeMode="0" />            
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSTDNUM003" runat="server" Text="有效時間(迄)"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="STDNUM003" runat="server" Width="100%" DateTimeMode="0" />            
            </td>
        </tr>

        </table>
    </div>
    </form>
</body>
</html>
