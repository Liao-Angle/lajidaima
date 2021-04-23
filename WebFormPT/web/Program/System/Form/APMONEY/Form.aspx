<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_System_Form_APMONEY_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>SMP零用金請款</title>
	<script language="javascript" src="../../../JS/jquery-1.4.1.js"></script>
    <script language="javascript" src="../../../JS/jquery-1.4.1.min.js"></script>
    <style type="text/css">
    </style>
    <script language="javascript">
		function onViewPr(headerId) {
            var url = $("#ViewPrUrl").val();
            var param = { HeaderId: headerId };
            OpenWindowWithPost(url, 'height=680, width=1000, top=100, left=100, toolbar=no, menubar=no, location=no, status=no', 'ViewPr', param);
        }

        function onViewAttach(entityName, pk1Value, modify, title) {
            var url = $("#ViewAttachUrl").val();
            var param = { EntityName: entityName, Pk1Value: pk1Value, Modify: modify, Title: title };
            OpenWindowWithPost(url, 'height=420, width=782, top=100, left=100, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no', 'ViewAttach', param);
        }
		
        function OpenWindowWithPost(url, windowoption, name, params) {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", name);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {
                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }
            window.open("", name, windowoption);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
    </script>
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
            <td width="80px" align=right class=BasicFormHeadHead>按鈕1
            </td>
            <td width="240px" class=BasicFormHeadDetail>&nbsp;&nbsp;
			<cc1:GlassButton ID="GbViewAward" runat="server" Height="20px" 
                    Text=" <<按鈕-畫面上檢視資料>>" Width="200px" onclick="GbViewAward_Click" />
            </td>
        </tr>
		<tr>
            <td colspan=4 width="90%" valign=top align=left class=BasicFormHeadDetail>
                <asp:HyperLink ID="hlSPPOA006" runat="server">利用JavaScript跨系統開啟表單資訊</asp:HyperLink>
				&nbsp;
                <asp:ImageButton ID="ImgButtonAtta" runat="server" ImageUrl="~/Images/attachForSMVE.gif" />
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
		<tr>
            <td width="80px" valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="lblHTMLCODE" runat="server" Text="HTML CODE"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>				
                <cc1:SingleField ID="HTMLCODE" runat="server" Width="100%" Height="64px" MultiLine="True" />
				<p runat="server"  ID="HtmlContentCode"></p>
            </td>
        </tr>
		<tr>
            <td width="80px" valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="lblREMARK1" runat="server" Text="REMARK1"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="REMARK1" runat="server" Width="100%" Height="30px" MultiLine="True" />
            </td>
        </tr>
		<tr>
            <td width="80px" valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="lblREMARK2" runat="server" Text="REMARK2"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="REMARK2" runat="server" Width="100%" Height="30px" MultiLine="True" />
            </td>
        </tr>
		
        </table>
		<BR>
		
		<BR>
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
