<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_System_Maintain_SMVKC_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>代理人查詢作業</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="279px" Width="690px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="通用代理人" Hidden="False">
                <TabBody>
                    <table border="0" cellspacing="0" cellpadding="2" width="650px" class="BasicFormHeadBorder">
                    <tr>
                        <td style="width:15%" class="BasicFormHeadHead">
                            <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="被代理人：" TextAlign="2"/>
                        </td>
                        <td style="width:35%" class="BasicFormHeadDetail">
                            <cc1:SingleOpenWindowField ID="SMVKAAB003" runat="server" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users" Width="100%" />
                        </td>
                        <td style="width:15%" class="BasicFormHeadHead">
                            <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="日期：" TextAlign="2"/>
                        </td>
                        <td style="width:35%" class="BasicFormHeadDetail">
                            <cc1:SingleDateTimeField ID="SubsDateTime" runat="server" Width="150px" DateTimeMode="1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="BasicFormHeadHead">
                            <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="代理人：" TextAlign="2" />
                        </td>
                        <td class="BasicFormHeadDetail">
                            <cc1:SingleOpenWindowField ID="SMVKAAB005" runat="server" showReadOnlyField="True" Width="100%" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
                        </td>
                        <td class="BasicFormHeadDetail" align="right" colspan="2" valign="bottom">
                            <cc1:GlassButton ID="SubsCancel" runat="server" Text="清除條件" Width="100px" OnClick="SubsCancel_Click" ImageUrl="../../../../Images/NO.gif" />
                            <cc1:GlassButton ID="SubsQuery" runat="server" Text="重新查詢" Width="100px" OnClick="SubsQuery_Click" ImageUrl="../../../../Images/OK.gif" />
                        </td>
                    </tr>
                    </table>
                    <cc1:DataList ID="SubsList" runat="server" Height="325px" Width="680px" />
                </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="流程代理人" Hidden="False">
                <TabBody>
                    <table border="0" cellspacing="0" cellpadding="2" width="650px" class="BasicFormHeadBorder">
                    <tr>
                        <td style="width:15%" class="BasicFormHeadHead">
                            <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="被代理人：" TextAlign="2"/>
                        </td>
                        <td style="width:35%" class="BasicFormHeadDetail">
                            <cc1:SingleOpenWindowField ID="SMVKAAC003" runat="server" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users" Width="225px" />
                        </td>
                        <td style="width:15%" class="BasicFormHeadHead">
                            <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="日期：" TextAlign="2"/>
                        </td>
                        <td style="width:35%" class="BasicFormHeadDetail">
                            <cc1:SingleDateTimeField ID="ProcessSubDateTime" runat="server" Width="150px" DateTimeMode="1"  />
                        </td>
                    </tr>
                    <tr>
                        <td class="BasicFormHeadHead">
                            <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="代理人：" TextAlign="2"/>
                        </td>
                        <td class="BasicFormHeadDetail">
                            <cc1:SingleOpenWindowField ID="SMVKAAC004" runat="server" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users" Width="225px" />
                        </td>
                         <td class="BasicFormHeadHead">
                            <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="發起流程：" TextAlign="2"/>
                        </td>
                        <td class="BasicFormHeadDetail">
                            <cc1:SingleOpenWindowField ID="FlowID" runat="server" guidField="SMWBAAA001" keyField="SMWBAAA003" serialNum="001" showReadOnlyField="True" tableName="SMWBAAA" Width="225px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="BasicFormHeadHead">
                            <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="發起部門：" TextAlign="2"/>
                        </td>
                        <td class="BasicFormHeadDetail">
                            <cc1:SingleOpenWindowField ID="SMVKAAC005" runat="server" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="OrgUnit" Width="225px" />
                        </td>
                        <td class="BasicFormHeadDetail" align="right" colspan="2" valign="bottom">
                            <cc1:GlassButton ID="ProcessSubCancel" runat="server" Text="清除條件" Width="100px" OnClick="ProcessSubCancel_Click" ImageUrl="../../../../Images/NO.gif" />
                            <cc1:GlassButton ID="ProcessSubQuery" runat="server" Text="重新查詢" Width="100px" OnClick="ProcessSubQuery_Click" ImageUrl="../../../../Images/OK.gif" />
                        </td>
                    </tr>
                    </table>
                    <cc1:DataList ID="ProcessSubList" runat="server" Height="300px" Width="680px" />
                </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
    </div>
    </form>
</body>
</html>
