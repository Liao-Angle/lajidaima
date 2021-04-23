<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoSession.aspx.cs" Inherits="NoSession" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
<script language=javascript>
function redir()
{
    top.window.location.href='default.aspx';
}
window.setInterval('redir()', 1000);
</script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 width=100% height=100% >
        <tr>
            <td width=100% height=100% align=center valign=middle style="font-size:10pt">
            <%=com.dsc.locale.LocaleString.getMainFrameLocaleString("NoSession.aspx.language.ini", "global", "string001", "系統已經版本更新. 請您重新登入")%>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
