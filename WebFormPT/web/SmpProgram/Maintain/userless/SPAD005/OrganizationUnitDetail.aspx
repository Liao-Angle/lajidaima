<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrganizationUnitDetail.aspx.cs" Inherits="SmpProgram_Maintain_SPAD005_OrganizationUnitDetail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>組織資料查詢-Detail</title>
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<table style="font-size: 9pt; width: 517px">
            
            <tr>
                <td style="width: 60px"><cc1:DSCLabel TextAlign="2" ID="DSCLabel3" runat="server" Text="部門代號" Width="60px" />
                    </td>
                <td style="width: 150px">
                    <cc1:singlefield id="deptId" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
                <td style="width: 60px"><cc1:DSCLabel TextAlign="2" ID="DSCLabel4" runat="server" Text="部門名稱" Width="60px" />
                    </td>
                <td style="width: 180px">
                    <cc1:singlefield id="deptName" runat="server" readonly="true" width="100%"></cc1:singlefield>
                </td>
            </tr>
            <tr>
                <td style="width: 60px"><cc1:DSCLabel TextAlign="2" ID="DSCLabel6" runat="server" Text="部門主管" Width="60px" />
                </td>
                <td style="width: 150px">
                    <cc1:SingleField ID="deptMgrName" runat="server" ReadOnly="true" Width="100%" />
                </td>
				<td style="width: 60px"><cc1:DSCLabel TextAlign="2" ID="DSCLabel8" runat="server" Text="E-Mail" Width="60px" />
                    </td>
                <td style="width: 180px">
                    <cc1:SingleField ID="deptMgrEmail" runat="server" ReadOnly="true" Width="100%" />
                </td>                
            </tr>
			<tr>
                <td style="width: 60px"><cc1:DSCLabel TextAlign="2" ID="lblDeptMgrTtl" runat="server" Text="主管職稱" Width="60px" />
                </td>
                <td style="width: 150px">
                    <cc1:SingleField ID="deptMgrTitle" runat="server" ReadOnly="true" Width="100%" />
                </td>
				<td style="width: 60px"><cc1:DSCLabel TextAlign="2" ID="lblNone" runat="server" Text="　" Width="60px" />
                    </td>
                <td style="width: 180px">　　　
                </td>                
            </tr>
        </table>
        <br />
    
    </div>
    <cc1:DSCGroupBox ID="GroupFilter" runat="server" Text="搜尋條件" Width="510px">
    <table width="505px"> 
        <tr>
            <td width="70px">
                <cc1:DSCLabel ID="LblEmpEName" runat="server" Text="英文姓" Width="70px" TextAlign="2" />
            </td>
            <td width="120px">
                <cc1:SingleField ID="empEName" runat="server" Width="100px" Visible="True" IgnoreCase="True" />
            </td>
			<td width="70px">
                <cc1:DSCLabel ID="LblEmpCName" runat="server" Text="中文姓" Width="70px" TextAlign="2" />
            </td>
            <td width="120px">
                <cc1:SingleField ID="empCName" runat="server" Width="100px" Visible="True" IgnoreCase="True" />
            </td>
            <td>
				&nbsp;&nbsp;&nbsp;<cc1:GlassButton ID="ButtonSearch" runat="server" Height="20px" Text="過濾" ImageUrl="~/Images/Search.gif"
                    Width="60px" onclick="ButtonSearch_Click"  />
            </td>
            <td width="100%"></td>
        </tr>
    </table>
    </cc1:DSCGroupBox>
    <div></div>
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="221px" Width="515px">
            <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" Title="部門人員清單" >
                <TabBody>
                    <cc1:DataList ID="FunctionList" runat="server" Height="300px" Width="505px" showExcel="True"  />
                </TabBody>
            </cc1:DSCTabPage>            
        </cc1:DSCTabControl>
    </form>
</body>
</html>
