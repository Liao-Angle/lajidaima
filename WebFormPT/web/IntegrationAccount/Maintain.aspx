<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="IssueLab_IntegrationAccount_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>    
    <form id="form1" runat="server">
    <div style="text-align:center" align="center">
         <table  style="  margin-left:4px" border=0 width=333px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
          <tr style="display:none">
                <td align="right" class="BasicFormHeadHead" style="width: 167px">
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="LDAP主機"></cc1:DSCLabel>
                </td>
                <td width="240px" class=BasicFormHeadDetail>
                    <cc1:SingleDropDownList ID="sddLDAP" runat="server" Width="150" />
                </td>                
        </tr>
                  <tr>
                <td align="right" class="BasicFormHeadHead" style="width: 167px">
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="廠區"></cc1:DSCLabel>
                </td>
                <td width="240px" class=BasicFormHeadDetail>
                    <cc1:SingleDropDownList ID="sddFactory" runat="server" Width="150" />
                </td>                
        </tr>
            <tr>
                <td align="right" class="BasicFormHeadHead" style="width: 167px">
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="目前站台UserID"></cc1:DSCLabel>
                </td>
                <td width="240px" class=BasicFormHeadDetail>
                    <cc1:SingleField ID="sfLocalAccountID" runat="server"  />
                </td>                
        </tr>
            <tr>
                <td align="right" class="BasicFormHeadHead" style="width: 167px">
                <cc1:DSCLabel ID="LBSTDDOC013" runat="server" Text="目標站台UserID"></cc1:DSCLabel>
                </td>
                <td width="240px" class=BasicFormHeadDetail>
                    <cc1:SingleField ID="sfAccount" runat="server"  />
                </td>                
        </tr>
        <tr style="display:none">
        <td align=right class=BasicFormHeadHead style="width: 167px">
                <cc1:DSCLabel ID="LBSTDDOC001" runat="server" Text="密碼"></cc1:DSCLabel>
                </td>
                <td width="240px" class=BasicFormHeadDetail>                
                <cc1:SingleField ID="sfPassword" runat="server" isPassword="true"  />
                </td>
        </tr>
                <tr style="display:none">
        <td align=right class=BasicFormHeadHead style="width: 167px">
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="Domain"></cc1:DSCLabel>
                </td>
                <td width="240px" class=BasicFormHeadDetail>                                
                       <cc1:SingleDropDownList ID="sddDomain" runat="server" Width="150" />
                </td>
        </tr>
           <tr>
        <td align=right class=BasicFormHeadHead style="width: 167px"> 
                </td>
                <td width="240px" class=BasicFormHeadDetail>                
                    <cc1:GlassButton ID="gBtnCheck" runat="server" Text="確認" Width="100"  showPrepareIcon="true"
                        onclick="gBtnCheck_Click" />
                </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
