<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestoreSession.aspx.cs" Inherits="FrameEnterprise_RestoreSession" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language=javascript>
    function resSession()
    {
	    document.pads.submit();
    }
    window.setInterval("resSession()",30000);
    </script>
</head>
<body>
    <form name=pads action="RestoreSession.aspx" method=post>
    </form>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
