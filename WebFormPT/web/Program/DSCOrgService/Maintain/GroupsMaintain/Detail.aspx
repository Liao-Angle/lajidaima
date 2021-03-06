<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCOrgService_Maintain_GroupsMaintain_Detail" %>

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
                <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="物件識別碼" Width="100px" />
                    </td>
                <td style="width: 180px">
                    <cc1:SingleField ID="OIDF" runat="server" Width="100%" ReadOnly="true" />
                </td>
                <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="物件版本" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="objectVersionF" runat="server" Width="100%" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="群組代號" Width="80px" />
                    </td>
                <td style="width: 180px"><cc1:SingleField ID="idF" runat="server" Width="100%" ReadOnly="true" />
                </td>
                <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="群組名稱" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="groupNameF" runat="server" Width="100%" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="公司識別碼" Width="100px" />
                    </td>
                <td style="width: 180px"><cc1:SingleField ID="organizationOIDF" runat="server" Width="100%" ReadOnly="true" />
                </td>
                <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="公司名稱" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="organizationNameF" runat="server" Width="100%" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel7" runat="server" Text="說明" Width="80px" />
                    </td>
                <td colspan="3"><cc1:SingleField ID="descriptionF" runat="server" Width="100%" ReadOnly="true" />
                </td>
            </tr>
        </table>
        <br />
    
    </div>
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Height="295px" Text="使用者清單" Width="660px">
            <cc1:DataList ID="UserList" runat="server" Height="242px" Width="650px" />
        </cc1:DSCGroupBox>
    </form>
</body>
</html>
