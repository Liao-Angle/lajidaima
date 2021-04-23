<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDocTypeReader.aspx.cs" Inherits="SmpProgram_Form_SPKM005_ViewDocTypeReader" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/Enterprise.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <cc1:DSCGroupBox ID="GroupFilter" runat="server" Text="文件類別預設讀者" width="650px">
    <table> 
        <tr>
            <td>
                <cc1:DataList ID="ReaderList" runat="server" height="290px" width="640px" 
                    showExcel="True" IsHideSelectAllButton="True" IsShowCheckBox="False" 
                    NoAdd="True" NoDelete="True" NoModify="True" />
            </td>
        </tr>
    </table>
    </cc1:DSCGroupBox>
    </form>
</body>
</html>
