<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrganizationUnitDetail.aspx.cs" Inherits="Program_DSCOrgService_Maintain_OrganizationMaintain_OrganizationUnitDetail" %>

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
        &nbsp;<table style="font-size: 9pt; width: 517px">
            <tr>
                <td style="width: 147px"><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="物件識別碼" Width="100px" />
                    </td>
                <td style="width: 165px">
                    <cc1:singlefield id="OIDF" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
                <td style="width: 136px"><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="物件版本" Width="80px" />
                    </td>
                <td style="width: 176px">
                    <cc1:singlefield id="objectVersionF" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
            </tr>
            <tr>
                <td style="width: 147px"><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="部門代號" Width="80px" />
                    </td>
                <td style="width: 165px">
                    <cc1:singlefield id="idF" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
                <td style="width: 136px"><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="部門名稱" Width="80px" />
                    </td>
                <td style="width: 176px">
                    <cc1:singlefield id="organizationUnitNameF" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
            </tr>
            <tr>
                <td style="width: 147px"><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="主管識別碼" Width="100px" />
                    </td>
                <td style="width: 165px">
                    <cc1:SingleField ID="managerOIDF" runat="server" ReadOnly="true" Width="100%" />
                </td>
                <td style="width: 136px"><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="主管姓名" Width="80px" />
                    </td>
                <td style="width: 176px">
                    <cc1:SingleField ID="userNameF" runat="server" ReadOnly="true" Width="100%" />
                </td>
            </tr>
            <tr>
                <td style="width: 147px"><cc1:DSCLabel ID="DSCLabel7" runat="server" Text="上層部門識別碼" Width="100px" />
                    </td>
                <td style="width: 165px">
                    <cc1:SingleField ID="superUnitOIDF" runat="server" ReadOnly="true" Width="100%" />
                </td>
                <td style="width: 136px"><cc1:DSCLabel ID="DSCLabel8" runat="server" Text="上層部門" Width="100px" />
                    </td>
                <td style="width: 176px">
                    <cc1:SingleField ID="superOrganizationUnitNameF" runat="server" ReadOnly="true" Width="100%" />
                </td>
            </tr>
            <tr>
                <td style="width: 147px"><cc1:DSCLabel ID="DSCLabel9" runat="server" Text="組織單元類型" Width="100px" />
                    </td>
                <td style="width: 165px">
                    <cc1:SingleField ID="organizationUnitTypeF" runat="server" ReadOnly="true" Width="100%" />
                </td>
                <td style="width: 136px">
                </td>
                <td style="width: 176px">
                </td>
            </tr>
            <tr>
                <td style="width: 147px"><cc1:DSCLabel ID="DSCLabel10" runat="server" Text="層級識別碼" Width="100px" />
                    </td>
                <td style="width: 165px">
                    <cc1:SingleField ID="levelOIDF" runat="server" ReadOnly="true" Width="100%" />
                </td>
                <td style="width: 136px"><cc1:DSCLabel ID="DSCLabel11" runat="server" Text="組織單元層級" Width="100px" />
                    </td>
                <td style="width: 176px">
                    <cc1:SingleField ID="organizationUnitLevelNameF" runat="server" ReadOnly="true" Width="100%" />
                </td>
            </tr>
            <tr>
                <td style="width: 147px"><cc1:DSCLabel ID="DSCLabel12" runat="server" Text="公司識別碼" Width="100px" />
                    </td>
                <td style="width: 165px">
                    <cc1:SingleField ID="organizationOIDF" runat="server" ReadOnly="true" Width="100%" />
                </td>
                <td style="width: 136px"><cc1:DSCLabel ID="DSCLabel13" runat="server" Text="公司名稱" Width="100px" />
                    </td>
                <td style="width: 176px">
                    <cc1:SingleField ID="organizationNameF" runat="server" ReadOnly="true" Width="100%" />
                </td>
            </tr>
        </table>
        <br />
    
    </div>
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="221px" Width="515px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage4" runat="server" Enabled="True" Title="部門屬性">
                    <TabBody>
                        <cc1:DataList ID="PropertyList" runat="server" Height="194px" Width="505px" />
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" Title="職務">
                    <TabBody>
                        <cc1:DataList ID="FunctionList" runat="server" Height="194px" Width="505px" />
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" Title="職稱">
                    <TabBody>
                        <cc1:DataList ID="TitleList" runat="server" Height="194px" Width="505px" />
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage3" runat="server" Enabled="True" Title="角色">
                    <TabBody>
                        <cc1:DataList ID="RoleList" runat="server" Height="194px" Width="505px" />
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
    </form>
</body>
</html>
