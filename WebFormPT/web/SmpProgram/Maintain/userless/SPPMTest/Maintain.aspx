<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain_SPPMTest_Maintain" %>

<%@ Register assembly="DSCWebControl" namespace="DSCWebControl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server"  name="TextBox1"></asp:TextBox>
&nbsp;&nbsp;
        <cc1:GlassButton ID="GlassButton1" runat="server" Height="16px" 
            onclick="GlassButton1_Click" Text="添加" Width="100px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <cc1:SingleField ID="SingleField1" runat="server" Width="100px" />
    
    </div>
    </form>
</body>
</html>
