<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DSCWebControlUI_SingleDateTimeWindow_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <script language=javascript>
    function adjustFrame(){
        //document.all.FRAMETAG.style.width="225";
        //document.all.FRAMETAG.style.height="250";
    }
    </script>
</head>
<body style="margin-left:0px;margin-top:0px" onload='adjustFrame()'>
    <form id="form1" runat="server">
    <div>
        <iframe id="FRAMETAG" src='<%=targetURL%>' style="width:100%;height:100%"></iframe>
    </div>
    </form>
</body>
</html>