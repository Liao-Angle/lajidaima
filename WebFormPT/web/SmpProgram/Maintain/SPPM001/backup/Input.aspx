<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPPM001_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>考核表維護</title>
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 780px">
        <legend>考核表維護</legend>
        <table style="margin-left:4px; width: 780px;" border=0 cellspacing=0 cellpadding=1 >  
            <tr><td><cc1:DSCLabel ID="LblName" runat="server" Width="100px" Text="名稱" TextAlign="2" /></td>
                <td><cc1:SingleField ID="Name" runat="server" Width="200px" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblDescription" runat="server" Width="100px" Text="說明" TextAlign="2" /></td>
                <td><cc1:SingleField ID="Description" runat="server" Width="500px" /></td>
            </tr>
        </table>
    </fieldset>
    <table style="margin-left:4px; width: 780px;" border=0 cellspacing=0 cellpadding=1 >  
         <tr>
            <td>
                <cc1:DataList ID="SmpPmEvaluationDetailList" runat="server" Width="780px" 
                    DialogHeight="520" showExcel="True" />
            </td>
        </tr>
    </table> 
    </form>
</body>
</html>
