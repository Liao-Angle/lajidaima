<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Help_Default" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("SysInfo.aspx.language.ini", "global", "string006", "線上說明") %></title>
</head>
<frameset cols="250,*" border=1>
<frame src='Outlook.aspx?ShowSys=1' name='MenuList' id='MenuList'></frame>
<frame src='about:blank' name='Content' id='Content'></frame>
</frameset>
</html>
