<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCAuthService_Maintain_SMSC_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <link href="SMSC.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=3 cellpadding=0 width=100% style="font-size:9pt">
            <tr>
                <td width=100% >
                    <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="匯入說明" style="font-size:7pt">
                        <cc1:DSCLabel runat="server" ID="ExplainText1" Text="1. 匯入所使用的Excel檔案, 可由下方連結下載標準範本. 不論是匯入異動檔或是完整設定檔, 所使用的格式皆相同" />
                        <cc1:DSCLabel runat="server" ID="ExplainText2" Text="2. 選擇匯入異動檔時, 系統會根據Excel內容設定做資料異動. 選擇匯入完整設定檔時, 系統會將所指定的資料庫設定全部刪除後再匯入" />
                        <cc1:DSCLabel runat="server" ID="ExplainText3" Text="3. 當系統有設定多個資料庫時, 可在選項後方選擇要異動是目前資料庫或者所有資料庫" />
                        <cc1:DSCLabel runat="server" ID="ExplainText4" Text="4. 當匯入完成後, 如果處理成功, 系統會顯示『匯入成功』訊息. 若失敗, 則會下載檔案（根據瀏覽器設定決定）, 此時另存該檔案即可" />
                        <cc1:DSCLabel runat="server" ID="ExplainText5" Text="5. 若選擇匯入完整設定檔時, 請先利用『權限匯出作業』將資料庫設定值備份出來. 匯出的Excel檔案格式相同於匯入檔案" />
                        <cc1:DSCLabel runat="server" ID="ExplainText6" Text="6. 選擇匯入完整檔前, 系統清除所有設定為單一Transaction作業; 而處理各資料庫異動時, 各資料庫為獨立Transaction" />
                        <cc1:DSCLabel runat="server" ID="ExplainText7" Text="7. 不論處理異動檔或是完整設定檔, 各資料庫皆為獨立Transaction. 一筆資料失敗後, 該資料庫會rollback" />
                        <cc1:DSCLabel runat="server" ID="ExplainText8" Text="8. 權限項目請由權限項目維護作業設定, 無法透過此匯入作業設定." />
                    </cc1:DSCGroupBox>
                </td>
            </tr>
            <tr>
                <td width=100% height=100%>
                    <cc1:DSCGroupBox ID="DSCGroupBox2" runat="server" Text="範例與格式下載">
                        <li><a href='#' onclick='window.open("權限設定匯入作業格式說明.xls");return false;'><%=TEMPLATEEXP1 %></a></li>
                        <li><a href='#' onclick='window.open("權限設定匯入作業範例.xls");return false;'><%=TEMPLATEEXP2 %></a></li>
                    </cc1:DSCGroupBox>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:DSCGroupBox ID="DSCGroupBox3" runat="server" Text="匯入">
                        <asp:RadioButton ID="IncreRadio" runat="server" Checked="True" GroupName="ModeGroup"
                            Text="<%=TEMPLATEEXP3 %>" />
                        <asp:RadioButton ID="TotalRadio" runat="server" GroupName="ModeGroup" Text="<%=TEMPLATEEXP4 %>" />
                        <asp:DropDownList ID="DBSetting" runat="server">
                            <asp:ListItem Value="0">目前資料庫設定</asp:ListItem>
                            <asp:ListItem Value="1">所有資料庫設定</asp:ListItem>
                        </asp:DropDownList>
                        <asp:FileUpload ID="XmlFilePath" runat="server" Width="396px" />
                        <br />
                        <asp:Button ID="ImportButton" runat="server" Text="<%=TEMPLATEEXP5 %>" OnClick="ImportButton_Click" Width="86px" />
                    </cc1:DSCGroupBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
