<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCAuthService_Maintain_SMSB_Detail" %>

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
        <cc1:DSCLabel id="DSCLabel1" runat="server" text="群組代號" width="71px"></cc1:dsclabel>
        <cc1:singlefield id="SMSAABA002" runat="server" width="140px"></cc1:singlefield>
        &nbsp;<cc1:DSCLabel id="DSCLabel2" runat="server" text="群組名稱" width="71px"></cc1:dsclabel><cc1:singlefield
            id="SMSAABA003" runat="server" width="140px"></cc1:singlefield>
        <cc1:OpenWin ID="openWin1" runat="server" OnOpenWindowButtonClick="openWin1_OpenWindowButtonClick" />
        <br />
        <br />
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="357px" Width="860px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" Title="權限設定">
                    <TabBody>
                        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="權限：" Width="61px" />
                        &nbsp;<cc1:DSCCheckBox ID="AUTH01" runat="server" Text="讀取" />
                        <cc1:DSCCheckBox ID="AUTH02" runat="server" Text="新增" />
                        <cc1:DSCCheckBox ID="AUTH03" runat="server" Text="修改" />
                        <cc1:DSCCheckBox ID="AUTH04" runat="server" Text="刪除" />
                        <cc1:DSCCheckBox ID="AUTH05" runat="server" Text="列印" />
                        <cc1:DSCCheckBox ID="AUTH06" runat="server" Text="報表" />
                        <cc1:DSCCheckBox ID="AUTH07" runat="server" Text="擁有" />
                        <cc1:DSCCheckBox ID="AUTH08" runat="server" Text="權限08" />
                        <cc1:DSCCheckBox ID="AUTH09" runat="server" Text="權限09" />
                        <cc1:DSCCheckBox ID="AUTH10" runat="server" Text="權限10" />
                        <table style="width: 850px; height: 284px">
                            <tr>
                                <td style="width: 31%">
                                    <cc1:MultiDropDownList ID="LeftBox" runat="server" Height="100%" Width="100%" />
                                </td>
                                <td width=50>
                                 <cc1:GlassButton ID="ToRightButton" runat="server" Text=">>" Width="50px" OnClick="ToRightButton_Click" />
                                    <cc1:GlassButton ID="ToLeftButton" runat="server" Text="<<" Width="50px" OnClick="ToLeftButton_Click" />
                                </td>
                                <td style="width: 650px">
                                    <cc1:MultiDropDownList ID="RightBox" runat="server" Height="100%" Width="100%" />
                                </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" Title="成員設定">
                    <TabBody>
                        <cc1:GlassButton ID="SelectButton" runat="server" Text="選擇成員" Width="135px" OnClick="SelectButton_Click" /><br />
                        <cc1:OutDataList ID="BCList" runat="server" Height="274px" />
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
    </div>
    </form>
</body>
</html>
