<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVA_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
<script language=javascript>
var splitter='$*$*$';
function readData()
{
    var data=window.dialogArguments;
    var list=data.split(splitter);
    document.getElementById("SMVAAAA003").value = list[0];
    document.getElementById("SMVAAAA004").value = list[1];
    document.getElementById("SMVAAAA005").value = list[2];
}
function confirmsave()
{
    if (document.getElementById("SMVAAAA003").value == '') {
        alert('<%=msg1 %>');
        return false;
    }
    if (document.getElementById("SMVAAAA003").value.length > 50) {
        alert('<%=msg2 %>');
        return false;
    }
    if (document.getElementById("SMVAAAA004").value == '') {
        alert('<%=msg3 %>');
        return false;
    }
    if (document.getElementById("SMVAAAA004").value.length > 50) {
        alert('<%=msg4 %>');
        return false;
    }else{
        document.getElementById("SMVAAAA004").value = document.getElementById("SMVAAAA004").value.replace(/'/g, '-');
    }
    var rets = document.getElementById("SMVAAAA003").value + splitter + document.getElementById("SMVAAAA004").value + splitter + document.getElementById("SMVAAAA005").value;
    window.returnValue=rets;
    window.close();
    return true;
}
function closeWin()
{
    window.returnValue='';
    window.close();
}
</script>
</head>
<body onload='readData()'>
    <form id="form1" runat="server">
    <div style="padding-top:10px;padding-left:10px">
    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="模組代碼:" Width=70px/><input type=text id='SMVAAAA003' class='SingleField_Normal' width=200px /><br />
    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="模組名稱:" Width=70px/><input type=text id='SMVAAAA004' class='SingleField_Normal' width=200px /><br />
    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="圖形網址:" Width=70px/><input type=text id='SMVAAAA005' class='SingleField_Normal' width=400px style="width: 316px" /><br />
    <br />
        &nbsp;&nbsp;<cc1:GlassButton ID="GlassButton1" runat="server" Text="確定" Width=50px Height=20px BeforeClick='confirmsave'/>
        <!--<input type=button class='GlassButton_Normal' value='確定' onclick='confirmsave()' />&nbsp;-->
        &nbsp;&nbsp;<cc1:GlassButton ID="GlassButton2" runat="server" Text="取消" Width=50px Height=20px BeforeClick='closeWin'/>
        <!--<input type=button class='GlassButton_Normal' value='取消' onclick='closeWin()' />&nbsp;-->
    </div>
    </form>
</body>
</html>
