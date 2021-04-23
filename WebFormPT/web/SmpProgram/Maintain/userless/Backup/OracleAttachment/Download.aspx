<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="SmpProgram_Maintain_OracleAttachment_Download" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DownLoad</title>
</head>
<body>
    <form id="form1" runat="server">
    附件下載結果:
    <br />
    <asp:Label ID="Msg_lab" runat="server" Text="Label"></asp:Label>

    <%--<input id="Text0" name="HeaderID" type="text" value="<%=Request["Header_Id"]%>" />
    <input id="Text1" name="FILEID" type="text" value="<%=Request["FileId"]%>" />
    <input id="Text2" name="KEY" type="text" value="<%=Request["Key"]%>" />
    <input id="Text3" name="KIND" type="text" value="<%=Request["Kind"]%>" />
    <input id="Text4" name="USERID" type="text" value="<%=Session["UserID"]%>" />--%>
    </form>
</body>
</html>
