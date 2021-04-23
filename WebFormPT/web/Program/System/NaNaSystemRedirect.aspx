<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NaNaSystemRedirect.aspx.cs" Inherits="Program_System_NaNaSystemRedirect" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
<%
    com.dsc.kernal.factory.IOFactory factory = new com.dsc.kernal.factory.IOFactory();
    string connectString = (string)Session["connectString"];
    string engineType = (string)Session["engineType"];

    com.dsc.kernal.factory.AbstractEngine engine = factory.getEngine(engineType, connectString);

    WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
    string url = sp.getParam("FlowAdminURL");
    string pwd = sp.getParam("FlowAdminPassword");
    string tool = Request.QueryString["tool"];
    engine.close();
    url += "aspNetFrameLogin.jsp";
%>
<script language=javascript>
function startRedirect()
{
    document.all.password.value='<%=pwd %>';
    document.all.tool.value='<%=tool %>';
    document.all.form1.action='<%=url %>';
    document.all.curpanelid.value='<%=(string)Request.QueryString["CurPanelID"] %>';
    document.all.form1.submit();
    
}
</script>    
</head>
<body onload='startRedirect()'>
    <form id="form1" action='' method=post runat=server>
    <div>
        <input type=password id=password name=password value='' style="width: 0px; height: 0px" /> 
        <input type=text name=userid value='administrator' style="width: 0px; height: 0px" />
        <input type=text name=tool id=tool value='' style="visibility: hidden" />
        <input type=text name=curpanelid id=curpanelid value='' style="visibility: hidden" />
    </div>
    </form>
</body>
</html>
