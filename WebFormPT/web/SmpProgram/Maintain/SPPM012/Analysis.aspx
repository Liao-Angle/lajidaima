<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Analysis.aspx.cs" Inherits="SmpProgram_Maintain_SPPM011_Analysis" %>

<%@ Register assembly="DSCWebControl" namespace="DSCWebControl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .style2
        {
            height: 40px;
        }
    </style>
    </head>
<body >
    <form id="form1" runat="server">
    <div>  
    <table border="0" cellpadding="1" cellspacing="0" style=" width:400px; height :100px;">
    <tr>
    <td style=" width:100px;height:30px;">
    
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="評核人數:" TextAlign="2" 
            Width="100%" />
    
    </td>
    <td>
    
        <cc1:SingleField ID="sfyphNum" runat="server" Width="120px" />
    
    </td>
   
    </tr>
        <tr>
    <td style=" height:30px;">
    
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="已完成人數:" TextAlign="2" 
            Width="100%" />
    
    </td>
    <td>
    
        <cc1:SingleField ID="sfCompleteNum" runat="server" Width="120px" />
    
    </td>
  
    </tr>
        <tr>
    <td style=" height:30px;">
    
        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="未完成人數:" TextAlign="2" 
            Width="100%" />
    
    </td>
    <td>
    
        <cc1:SingleField ID="sfNoCompleteNum" runat="server" Width="120px" />
    
    </td>
  
    </tr>
        <tr>
    <td style=" height:30px;">
    
        <cc1:DSCLabel ID="lblplanGUID" runat="server" />
        <cc1:DSCLabel ID="lblAssessUserGUID" runat="server" />
            </td>
    <td>
    
        <cc1:DSCCheckBox ID="cbSubmitStatus" runat="server" Text="是否同意主管提交成績" 
            Width="100%" />
            </td>
  
    </tr>
    </table>
    <hr />  
   <asp:Label ID="Label1" runat="server" Text="成績分佈統計" Font-Bold="True" 
            Font-Size="Small"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="gvAchievementScore" runat="server">
    </asp:GridView>
    <br />
    <hr />  
    <br />
  <table border="0" cellpadding="0" cellspacing="0" style=" width :400px; height:40;">
  <tr>
  <td class="style2">
  <cc1:GlassButton ID="gbSendNotice" runat="server" Text="通知主管Review" 
          Width="120px" onclick="gbSendNotice_Click" />
  </td>
  </tr>
    </table>
    </div>

    </form>
</body>
</html>
