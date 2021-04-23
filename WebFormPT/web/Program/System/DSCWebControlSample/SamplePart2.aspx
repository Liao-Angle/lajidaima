<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SamplePart2.aspx.cs" Inherits="Program_System_DSCWebControlSample_SamplePart2" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <script language="javascript">
    function GlassButtonSampleBeforeClick()
    {
        alert('aspx js Before Click~!');
        return true;
    }
    function GlassButtonSampleAfterClick()
    {
        alert('aspx js After Click~!');
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:dsctabcontrol id="DSCTabControl1" runat="server" height="343px" width="711px"><TabPages>
<cc1:DSCTabPage runat="server" ID="DSCTabPage7" Enabled="True" ImageURL="" Hidden="False" Title="FileUpload"><TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top">
        <!--工具列區-->
        <cc1:GlassButton ID="orderByUploadTimeDescButton" runat="server" Text="orderByUploadTimeDesc" Width="200px" OnClick="orderByUploadTimeDescButton_Click" />
        <cc1:GlassButton ID="FUNoAddButton" runat="server" Text="NoAdd" Width="200px" OnClick="FUNoAddButton_Click" />
        <cc1:GlassButton ID="FNoDeleteButton" runat="server" Text="NoDelete" Width="200px" OnClick="FNoDeleteButton_Click" />
        <cc1:GlassButton ID="FReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="FReadOnlyButton_Click" />
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:FileUpload ID="FileUploadSample" Width="450px" Height="300px" runat="server" />
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" ID="DSCTabPage8" Enabled="True" ImageURL="" Hidden="False" Title="GraphFileUpload"><TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top">
        <!--工具列區-->
        <cc1:GlassButton ID="GorderByUploadTimeDescButton" runat="server" Text="orderByUploadTimeDesc" Width="200px" OnClick="GorderByUploadTimeDescButton_Click" />
        <cc1:GlassButton ID="GUNoAddButton" runat="server" Text="NoAdd" Width="200px" OnClick="GUNoAddButton_Click" />
        <cc1:GlassButton ID="GNoDeleteButton" runat="server" Text="NoDelete" Width="200px" OnClick="GNoDeleteButton_Click" />
        <cc1:GlassButton ID="GReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="GReadOnlyButton_Click" />
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:GraphFileUpload ID="GraphFileUploadSample" Width="450px" Height="300px" runat="server" />
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>        
<cc1:DSCTabPage runat="server" ID="DSCTabPage1" Enabled="True" ImageURL="" Hidden="False" Title="GlassButton"><TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top">
        <!--工具列區-->
        <cc1:GlassButton ID="EnabledButton" runat="server" Text="Enabled" Width="200px" OnClick="EnabledButton_Click" />
        <cc1:GlassButton ID="GlassButtonTextButton" runat="server" Text="Text" Width="200px" OnClick="GlassButtonTextButton_Click" />
        <cc1:GlassButton ID="GlassButtonReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="GlassButtonReadOnlyButton_Click" />
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:GlassButton ID="GlassButtonSample" runat="server" Width="400px" Text="GlassButton" BeforeClick="GlassButtonSampleBeforeClick" AfterClick="GlassButtonSampleAfterClick" OnBeforeClicks="GlassButtonSample_BeforeClick" OnClick="GlassButtonSample_Click"/>
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" ID="DSCTabPage2" Enabled="True" ImageURL="" Hidden="False" Title="IPField">
<TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top" width=205>
        <!--工具列區-->
        <cc1:GlassButton ID="IPFValueTextButton" runat="server" Text="ValueText" Width="200px" OnClick="IPFValueTextButton_Click" />
        <cc1:GlassButton ID="IPFCustomCSSButton" runat="server" Text="CustomCSS" Width="200px" OnClick="IPFCustomCSSButton_Click" />
        <cc1:GlassButton ID="IPFReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="IPFReadOnlyButton_Click" />
        <p/>
        <cc1:GlassButton ID="IPFgetIPPartButton" runat="server" Text="getIPPart" Width="200px" OnClick="IPFgetIPPartButton_Click" />
        
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:IPField ID="IPFieldSample" runat="server" /><BR />
        <cc1:DSCLabel id="DSCLabel2" runat="server" Width="88px" Text="結果："></cc1:DSCLabel> <cc1:SingleField id="IPFieldResult" runat="server" Width="389px" MultiLine="true" Height="100px"></cc1:SingleField>
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" ID="DSCTabPage3" Enabled="True" ImageURL="" Hidden="False" Title="MultiDropDownList"><TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top" width=205>
        <!--工具列區-->
        <cc1:GlassButton ID="MDDLValueTextButton" runat="server" Text="ValueText" Width="200px" OnClick="MDDLValueTextButton_Click" />
        <cc1:GlassButton ID="MDDLReadOnlyTextButton" runat="server" Text="ReadOnlyText" Width="200px" OnClick="MDDLReadOnlyTextButton_Click" />
        <cc1:GlassButton ID="MDDLReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="MDDLReadOnlyButton_Click" />

    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:MultiDropDownList ID="MDDLSample" runat="server" Width="300px" Height="250px" OnSelectChanged="MDDLSampleChange"/>
        <BR /><cc1:DSCLabel id="DSCLabel1" runat="server" Width="88px" Text="結果："></cc1:DSCLabel> <cc1:SingleField id="MDDLResult" runat="server" Width="389px" MultiLine="true" Height="50px"></cc1:SingleField>
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" ID="DSCTabPage4" Enabled="True" ImageURL="" Hidden="False" Title="OpenWin"><TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top" width=205>
        <!--工具列區-->

    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:GlassButton ID="DoOpenWin" runat="server" Width="100px" Text="單選開窗" OnClick="DoOpenWinClick"/>
        <cc1:GlassButton ID="DoMultiWin" runat="server" Width="100px" Text="多選開窗" OnClick="DoMultiWinClick"/>
        <cc1:OpenWin ID="OpenWinSample" runat="server" OnOpenWindowButtonClick="OpenWinSampleClick"/>
        <BR /><cc1:DSCLabel id="DSCLabel3" runat="server" Width="88px" Text="結果："></cc1:DSCLabel> <cc1:SingleField id="OPResult" runat="server" Width="389px" MultiLine="true" Height="250px"></cc1:SingleField>
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" ID="DSCTabPage5" Enabled="True" ImageURL="" Hidden="False" Title="OutDataList">
<TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top">
        <!--工具列區-->
        <cc1:GlassButton ID="DataListHiddenFieldButton" runat="server" Text="HiddenField" Width="200px" OnClick="DataListHiddenFieldButton_Click" />
        <cc1:GlassButton ID="ShowSerialButton" runat="server" Text="ShowSerial" Width="200px" OnClick="ShowSerialButton_Click" />
        <cc1:GlassButton ID="IsShowCheckBoxButton" runat="server" Text="IsShowCheckBox" Width="200px" OnClick="IsShowCheckBoxButton_Click" />
        <cc1:GlassButton ID="showTotalRowCountButton" runat="server" Text="showTotalRowCount" Width="200px" OnClick="showTotalRowCountButton_Click" />
        <cc1:GlassButton ID="showExcelButton" runat="server" Text="showExcel" Width="200px" OnClick="showExcelButton_Click" />
        <cc1:GlassButton ID="showPrintButton" runat="server" Text="showPrint" Width="200px" OnClick="showPrintButton_Click" />
        <cc1:GlassButton ID="showSetupButton" runat="server" Text="showSetup" Width="200px" OnClick="showSetupButton_Click" />
        <cc1:GlassButton ID="MultiLineButton" runat="server" Text="MultiLine" Width="200px" OnClick="MultiLineButton_Click" />
        <cc1:GlassButton ID="IsHideSelectAllButton" runat="server" Text="IsHideSelectAllButton" Width="200px" OnClick="IsHideSelectAllButton_Click" />
        <cc1:GlassButton ID="showAdminColumnButton" runat="server" Text="showAdminColumn" Width="200px" OnClick="showAdminColumnButton_Click" />
        <p />
        <cc1:GlassButton ID="CheckDataButton" runat="server" Text="CheckData" Width="200px" OnClick="CheckDataButton_Click" />
        <cc1:GlassButton ID="UnCheckDataButton" runat="server" Text="UnCheckData" Width="200px" OnClick="UnCheckDataButton_Click" />
        <cc1:GlassButton ID="UnCheckAllDataButton" runat="server" Text="UnCheckAllData" Width="200px" OnClick="UnCheckAllDataButton_Click" />
        <cc1:GlassButton ID="getSelectedItemButton" runat="server" Text="getSelectedItem" Width="200px" OnClick="getSelectedItemButton_Click" />
        <cc1:GlassButton ID="reOrderFieldButton" runat="server" Text="reOrderField" Width="200px" OnClick="reOrderFieldButton_Click" />
        <p />
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:OutDataList ID="DataListSample" runat="server" Width="454px" Height="248px" />
        <BR /><cc1:DSCLabel id="DSCLabel4" runat="server" Width="88px" Text="結果："></cc1:DSCLabel> <cc1:SingleField id="DataListOutput" runat="server" Width="389px" MultiLine="true" Height="50px"></cc1:SingleField>
    </td>
</tr>
</table></TabBody>
</cc1:DSCTabPage>
</TabPages>
</cc1:dsctabcontrol>
    
    </div>
    </form>
</body>
</html>
