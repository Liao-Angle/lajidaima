<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="SmpProgram_Maintain_SPTS006_Import" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>人員資格鑑定說明書匯入</title>
    <style type="text/css">

.GridPageSelect
{
	border-style:solid;
	border-width:1pt;
	font-size:10pt;
	font-family:Arial;
}
.GridPageText
{
	border-style:solid;
	border-width:1pt;
	font-size:10pt;
	font-family:Arial;
}
.GridList
{
	font-size:10pt;
	background-color:transparent;
	font-family:Arial;
}
.GridBorder
{
	overflow:scroll;
	scrollbar-face-color:white;
	scrollbar-highlight-color:#CCCCFF;
	border-Style:inset;
	border-Width:1px;
	POSITION:relative;
	background-color:#FFFFFF;
	font-family:Arial;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=3 cellpadding=0 width=100% style="font-size:9pt">
            <tr>
                <td width=100% >
                    <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="匯入說明">
                        <cc1:DSCLabel runat="server" ID="ExplainText1" 
                            Text="1. 匯入所使用的Excel檔案, 可由下方連結下載標準範本." Width="660px" />
                    </cc1:DSCGroupBox>
                </td>
            </tr>
            <tr>
                <td width=100% height=100%>
                    <cc1:DSCGroupBox ID="DSCGroupBox2" runat="server" Text="範例與格式下載">
                        <li><a href='#' onclick='window.open("人員資格鑑定說明書匯入說明.xls");return false;'><%=TEMPLATEEXP1 %></a></li>
                        <li><a href='#' onclick='window.open("人員資格鑑定說明書匯入範例.xls");return false;'><%=TEMPLATEEXP2 %></a></li>
                    </cc1:DSCGroupBox>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:DSCGroupBox ID="DSCGroupBox3" runat="server" Text="匯入">
                        <asp:FileUpload ID="XmlFilePath" runat="server" Width="396px" />
                        <br />
                        <asp:Button ID="ImportButton" runat="server" Text="<%=TEMPLATEEXP5 %>" OnClick="ImportButton_Click" Width="86px" />
                    </cc1:DSCGroupBox>
                </td>
            </tr>
        </table>
        <table>
            <tr><td>
                <cc1:DataList ID="DataListExpertiseRep" runat="server" height="220px" width="640px" 
                                            showExcel="True"
                                            IsHideSelectAllButton="True" 
                                            IsShowCheckBox="False" />
                </td>    
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
