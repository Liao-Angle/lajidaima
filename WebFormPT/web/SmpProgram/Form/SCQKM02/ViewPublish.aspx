<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPublish.aspx.cs" Inherits="SmpProgram_Form_SPKM005_ViewPublish" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        資料處理中, 請稍後.....</div>
    </form>
<script language=javascript>
    var url = '/ECP/SmpProgram/Maintain/PDFViewer/ViewDocument.aspx';
    var wx = 1024;
    var x = (screen.width - wx) / 2;
    window.open(url, "", "left="+x+", width="+wx+" resizable=yes, status=yes,toolbar=no, menubar=no, location=no");
    window.close();
</script>
</body>
</html>
