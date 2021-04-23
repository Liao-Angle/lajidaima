<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCOrgService_Maintain_Users_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 662px;font-size:9pt">
            <tr>
                <td><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="使用者代號" Width="80px" />
                    </td>
                <td><cc1:SingleField ID="idF" runat="server" Width="150px"  />
                </td>
                <td><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="使用者姓名" Width="80px" />
                    </td>
                <td><cc1:SingleField ID="userNameF" runat="server" Width="150px"  />
                </td>
            </tr>
            <tr>
                <td><cc1:DSCLabel ID="DSCLabel16" runat="server" Text="工時成本單位" Width="109px" />
                    </td>
                <td><cc1:SingleField ID="costF" runat="server" Width="150px"  />
                </td>
                <td><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="離職日期" Width="80px" />
                    </td>
                <td>
                    <cc1:SingleDateTimeField ID="leaveDateF" runat="server" Width="150px" DateTimeMode="0"/>
                </td>
            </tr>
            <tr>
                <td><cc1:DSCLabel ID="DSCLabel9" runat="server" Text="語系" Width="80px" />
                    </td>
                <td >
                    <cc1:SingleDropDownList ID="localeStringF" runat="server" Width="150px"/>
                </td>
                 <td><cc1:DSCLabel ID="DSCLabel11" runat="server" Text="電話號碼" Width="80px" />
                    </td>
                <td><cc1:SingleField ID="phoneNumberF" runat="server" Width="150px"  />
                </td>
            </tr>
            <tr>
                <td><cc1:DSCLabel ID="DSCLabel10" runat="server" Text="email信箱" Width="80px" />
                    </td>
                <td colspan=3><cc1:SingleField ID="mailAddressF" runat="server" Width="350px"  />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
