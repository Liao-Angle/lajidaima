<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain_SPPM010_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div>
<table border=0 cellpadding=1 cellspacing=0>
    <tr><td>
            <cc1:dscgroupbox id="DSCGroupBox1" runat="server" text="步驟一: 請選擇要修改的資料表"
                width="323px" height="20px"><cc1:DSCLabel id="DSCLabel1" runat="server" Width="79px" Text="資料表名稱"></cc1:DSCLabel> 
                <cc1:SingleDropDownList id="SelectTableName" runat="server" Width="192px" OnSelectChanged="SelectTableName_SelectChanged"></cc1:SingleDropDownList>
            </cc1:dscgroupbox>
        </td>
        <td>
            <cc1:dscgroupbox id="DSCGroupBox3" runat="server" readonly="False" text="步驟二: 請選擇要修改的欄位"
                width="375px" height="20px">
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="欄位名稱" Width="83px" />
                <cc1:SingleDropDownList ID="SelectFieldName" runat="server" Width="230px" />
            </cc1:dscgroupbox>
        </td>
    </tr>
    <tr><td><br /></td></tr> 
    <tr><td colspan=2>
        <cc1:dscgroupbox id="DSCGroupBox2" runat="server"  text="步驟三: 請勾選要修改的資料"
            width="810px">
            <cc1:OutDataList id="DataGrid" runat="server" Width="800px" 
                Height="300px" ReadOnly="True" NoAdd="true" NoDelete="true" NoModify="true" 
                isShowAll="True" showExcel="True" showTotalRowCount="True"></cc1:OutDataList></cc1:dscgroupbox>
            </td>
    </tr>
    <tr><td><br /></td></tr> 
    <tr>
        <td colspan=2>
            <cc1:DSCGroupBox ID="DSCGroupBox4" runat="server" Text="步驟四: 請先按下資料加密, 更新內容後按下確認鈕" Width="711px">
            <table border="0" cellpadding="0">
                <tr>
                    <td rowspan="2" valign="top">
                        <cc1:GlassButton ID="EncodeField" runat="server" Height="22px" Text="資料加密" Width="97px" OnClick="EncodeField_Click" />
                        <cc1:GlassButton ID="ConfirmButton" runat="server" Height="22px" Text="確認修改" Width="97px" ConfirmText="你確定要修改資料嗎?" OnClick="ConfirmButton_Click" />
                    </td>
                    <td rowspan="2">&nbsp;</td>
                </tr>
            </table>
            </cc1:DSCGroupBox>
        </td>
    </tr>
</table>
    
</div>
</form>
</body>
</html>
