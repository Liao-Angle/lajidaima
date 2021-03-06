<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrganizationDetail.aspx.cs" Inherits="Program_DSCOrgService_Maintain_OrganizationMaintain_OrganizationDetail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<table style="font-size: 9pt; width: 462px">
            <tr>
                <td style="width: 120px"><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="物件識別碼" Width="100px" />
                    </td>
                <td style="width: 165px">
                    <cc1:singlefield id="OIDF" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
                <td style="width: 101px"><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="物件版本" Width="80px" />
                    </td>
                <td style="width: 176px">
                    <cc1:singlefield id="objectVersionF" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
            </tr>
            <tr>
                <td style="width: 101px"><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="公司代號" Width="80px" />
                    </td>
                <td style="width: 165px">
                    <cc1:singlefield id="idF" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
                <td style="width: 101px"><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="公司名稱" Width="80px" />
                    </td>
                <td style="width: 176px">
                    <cc1:singlefield id="organizationNameF" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
