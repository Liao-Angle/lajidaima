<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCAuthService_Maintain_SMSF_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=3 cellpadding=0 width=100% style="font-size:9pt">
            <tr>
                <td width=100% >
                    <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">
                    <table border=0 cellspacing=0 cellpadding=3>
                    <tr>
                        <td style="width: 107px">
                            <cc1:DSCLabel ID="DSCLabel1" runat="server" Width="150px" Text="請選擇程式名稱："/>
                        </td>
                        <td>
                            <cc1:SingleOpenWindowField ID="CheckProgram" runat="server" guidField="SMVAAAB001" keyField="SMVAAAB002" keyFieldType="STRING" serialNum="001" showReadOnlyField="true" Style="z-index: 75; left: 0px; position: absolute; top: 0px" tableName="SMVAAAB" Width="500px" />
                        </td>
                  </tr>
                    <tr>
                        <td colspan="2">
                            <cc1:DSCCheckBox ID="ListHave" runat="server" Text="僅列出擁有任一權限的使用者"/>
                        </td>
                        <td colspan="2" align="right">
                            <cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" Text="開始查詢" Width="102px" OnClick="SearchButton_Click" showWaitingIcon="True" />
                        </td>
                    </tr>
                    </table>
                    </cc1:DSCGroupBox>
                </td>
            </tr>
            <tr>
                <td width="100%" height="100%">
                    <cc1:OutDataList ID="UserAuthList" runat="server" Height="315px" Width="100%" />
                </td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
