<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDocument.aspx.cs" Inherits="SmpProgram_Maintain_PDFViewer_ViewDocument" %>

<%@ Register Assembly="RadPdf" Namespace="RadPdf.Web.UI" TagPrefix="radPdf" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<style type="text/css">
    @media print
    {
        body
        {
            display: none;
        }
    }
</style>
<head id="Head1" runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>View Document</title>
</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
    <table style="border: 1px solid #C0C0C0;">
        <tr style="border: 0">
            <td style="border: 0">
                View:
            </td>
            <td style="border: 0">
                <button onclick="zoomTo(50);return false;">
                    50%</button><br />
            </td>
            <td style="border: 0">
                <button onclick="zoomTo(100);return false;">
                    100%</button><br />
            </td>
            <td style="border: 0">
                <button onclick="zoomTo(200);return false;">
                    200%</button><br />
            </td>
            <td style="border: 0">
                &nbsp&nbsp&nbsp&nbsp
            </td>
            <td style="border: 0">
                <select id="ZoomSelect">
                    <option value="50">50%</option>
                    <option value="65">65%</option>
                    <option value="75">75%</option>
                    <option value="85">85%</option>
                    <option value="100" selected="selected">100%</option>
                    <option value="110">110%</option>
                    <option value="125">125%</option>
                    <option value="150">150%</option>
                    <option value="175">175%</option>
                    <option value="200">200%</option>
                    <option value="250">250%</option>
                    <option value="300">300%</option>
                </select>
            </td>
            <td style="border: 0">
                <button onclick="zoomToSelect();return false;">
                    Zoom</button><br />
            </td>
        </tr>
    </table>
    <div>
        <radPdf:PdfWebControl ID="PdfWebControl1" runat="server" Height="100%" Width="100%"
            OnClientLoad="initRadPdf();" HideEditMenu="True" HideFileMenu="True" HidePrintButton="True"
            HideRightClickMenu="True" HideSaveButton="True" HideSelectText="True" HideToolsInsertTab="True"
            HideBookmarks="True" HideDownloadButton="True" HideObjectPropertiesBar="True"
            HideThumbnails="True" HideToolsTabs="True" HideTopBar="True" HideToolsMenu="True"
            TabToNextPage="True" TabTopToBottom="True" ViewerPageLayoutDefault="SinglePageContinuous"
            HideFocusOutline="True" HideToggleHighlightsButton="True" />
        <%--<radPdf:PdfWebControl ID="PdfWebControl1" runat="server" Height="100%" Width="100%"
            HideEditMenu="True" HideFileMenu="True" HidePrintButton="True"
            HideRightClickMenu="True" HideSaveButton="True" HideSelectText="True" HideToolsInsertTab="True"
            HideBookmarks="True" HideDownloadButton="True" HideObjectPropertiesBar="True"
            HideThumbnails="True" HideToolsTabs="True" HideTopBar="True" HideToolsMenu="True"
            TabToNextPage="True" TabTopToBottom="True" ViewerPageLayoutDefault="SinglePageContinuous"
            HideFocusOutline="True" HideToggleHighlightsButton="True" />--%>

    </div>
    </form>
    <script type="text/javascript">
        var api = null;

        function initRadPdf() {
            api = new PdfWebControlApi("PdfWebControl1");
            //api.setView({ "zoom": 200 });
        }

        function zoomTo(val) {
            if (api) {
                api.setView({ "zoom": val });
            }
        }
        function zoomToSelect() {
            if (api) {
                var val = document.getElementById("ZoomSelect").value;  
                api.setView({ "zoom": val });
            }
        }
    </script>
</body>
</html>
