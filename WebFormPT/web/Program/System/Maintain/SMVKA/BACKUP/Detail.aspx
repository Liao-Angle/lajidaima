<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVKA_Detail" %>

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
    <table border="0" cellpadding="2" cellspacing="0" width="700px" class="BasicFormHeadBorder">
    <tr>
        <td style="height: 26px; width: 110px;" class="BasicFormHeadHead">
            <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="代理期間" Width="73px"/>
            <cc1:GlassButton ID="ClearButton" runat="server" Text="清除" Height="25px" Width="32px" OnClick="ClearButton_Click"/>
        </td>
        <td style="height: 26px" class="BasicFormHeadDetail" valign="bottom">
            <cc1:SingleDateTimeField ID="StartTime" runat="server" Width="150px" DateTimeMode="1" />～<cc1:SingleDateTimeField ID="EndTime" runat="server" Width="150px" DateTimeMode="1"  />
        </td>
    </tr>
    <tr>
        <td colspan="2" class="BasicFormHeadDetail">
            <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="279px" Width="690px">
                <TabPages>
                    <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="通用代理人">
                    <TabBody>
                        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="代理人：" Width="72px" />
                        <cc1:SingleOpenWindowField ID="SUBS" runat="server" showReadOnlyField="True"
                            Width="248px" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
                        <cc1:OutDataList ID="SubsList" runat="server" Height="250px" Width="680px" OnSaveRowData="SubsList_SaveRowData" OnShowRowData="SubsList_ShowRowData" />
                    </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="流程代理人">
                    <TabBody>
                        <table border="0" cellspacing="1" cellpadding="0">
                        <tr>
                            <td style="height: 22px">
                                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="選擇流程"/>
                            </td>
                            <td style="height: 22px">
                                <cc1:SingleOpenWindowField ID="FlowID" runat="server" guidField="SMWBAAA001" keyField="SMWBAAA003" serialNum="001" showReadOnlyField="True" tableName="SMWBAAA" Width="252px" />
                            </td>
                            <td style="height: 22px">
                                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="代理人"/>
                            </td>
                            <td style="height: 22px">
                                <cc1:SingleOpenWindowField ID="SMVKAAC004" runat="server" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users" Width="252px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="發起部門"/>
                            </td>
                            <td>
                                <cc1:SingleOpenWindowField ID="SMVKAAC005" runat="server" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="OrgUnit" Width="252px" />
                            </td>
                            <td>
                                <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="永久代理" Display="false"/>
                            </td>
                            <td>
                                <cc1:DSCCheckBox ID="SMVKAAC008" runat="server" Display="false"/>
                            </td>
                        </tr>
                        </table><cc1:OutDataList ID="ProcessSubList" runat="server" Height="220px" Width="680px" OnSaveRowData="ProcessSubList_SaveRowData" OnShowRowData="ProcessSubList_ShowRowData" />
                    </TabBody>
                    </cc1:DSCTabPage>
                </TabPages>
            </cc1:DSCTabControl>
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
