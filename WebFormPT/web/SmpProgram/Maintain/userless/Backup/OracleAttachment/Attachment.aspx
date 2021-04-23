<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attachment.aspx.cs" Inherits="SmpProgram_Maintain_OracleAttachment_Attachment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Attachment</title>
</head>
<body>
    附件下載完成後請關閉此頁面。
    <form action="DownLoad.aspx" id="form1" method="post">
    <input id="Header_Id" name="Header_Id" type="hidden" value="<%=Request["Header_Id"]%>" />
    <input id="FileId" name="FileId" type="hidden" value="<%=Request["FileId"]%>" />
    <input id="Key" name="Key" type="hidden" value="<%=Request["Key"]%>" />
    <input id="Kind" name="Kind" type="hidden" value="<%=Request["Kind"]%>" />
<%--    <input id="Header_Id2" name="Header_Id2" type="text" value="<%=Request["Header_Id"]%>" />
    <input id="FileId2" name="FileId2" type="text" value="<%=Request["FileId"]%>" />
    <input id="Key2" name="Key2" type="text" value="<%=Request["Key"]%>" />
    <input id="Kind2" name="Kind2" type="text" value="<%=Request["Kind"]%>" />--%>
    </form>
    <script type="text/javascript">
        window.onload = onPost;
        function onPost() {
            var frm = document.forms["form1"];
            frm.action = "DownLoad.aspx";
            frm.submit();
        }
    </script>
</body>
</html>
