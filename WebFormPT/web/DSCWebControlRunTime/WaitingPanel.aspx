<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WaitingPanel.aspx.cs" Inherits="DSCWebControlRunTime_WaitingPanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>    
</head>

<body topmargin=0 leftmargin=0 marginwidth=0 marginheight=0 onclick='parent.hideWaitingIcon();' onload="try{parent.WaitingIconLoadComplete();}catch(e){}">
    <form id="form1" runat="server">
    <div style="border-width:1px;border-style:solid;border-color:Black;background-color:white;width:248px;height:40px">
            <table border=0 width=100% height=100% style={font-size:10pt;z-index:100000}>
            <tr>
            	<td>                
            		<img style="z-index:100000" src='<%=Page.ResolveUrl("~/DSCWebControlRunTime/DSCWebControlImages/waiting.gif") %>'>
            	</td>
            	<td valign=middle style="z-index:100000"><%=com.dsc.locale.LocaleString.getSystemMessageString("web_general.language.ini", "global", "WAITINGMESSAGE", "資料處理中, 請稍待.....<br>點擊此視窗關閉訊息")%></td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
