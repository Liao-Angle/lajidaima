<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_System_Form_APMONEY_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBAPMONEY001" runat="server" Text="申請人"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="width: 240px">
                <cc1:SingleOpenWindowField ID="APMONEY001" runat="server" Width="254px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBAPMONEY004" runat="server" Text="申請單位"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="APMONEY004" runat="server" Width="254px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="OrgUnit" />
            </td>
        </tr>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBAPMONEY006" runat="server" Text="申請總金額"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="width: 240px">
                <cc1:SingleField ID="APMONEY007" runat="server" Width="251px" alignRight="True" isAccount="True" isMoney="True" ReadOnly="True" />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
            </td>
        </tr>
        <tr>
            <td width="80px" valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBAPMONEY007" runat="server" Text="說明"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="APMONEY006" runat="server" Width="100%" Height="64px" MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
        &nbsp;<cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="299px" Width="666px">
            <TabPages>
                <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="申請明細">
                    <TabBody>
                        <cc1:DataList ID="DetailList" runat="server" Height="242px" Width="655px" OnAddOutline="DetailList_AddOutline" OnDeleteData="DetailList_DeleteData" />
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="出帳單位">
                    <TabBody>
                        <asp:Label ID="LBAPLOCATION002" runat="server" Text="出帳單位" Width="61px"></asp:Label>
                        <cc1:SingleField ID="APLOCATION002" runat="server" Width="289px" />
                        <cc1:GlassButton ID="GlassButtonDownload" runat="server" Height="20px" 
                            Text="下載" Width="40px" />
                        <br />
                        <cc1:OutDataList ID="LocationList" runat="server" Height="227px" OnSaveRowData="LocationList_SaveRowData"
                            OnShowRowData="LocationList_ShowRowData" Width="653px" />
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
        </cc1:DSCTabControl>
    </form>
</body>
</html>
