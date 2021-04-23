<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SamplePart3.aspx.cs" Inherits="Program_System_DSCWebControlSample_SamplePart3" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:dsctabcontrol id="DSCTabControl1" runat="server" height="343px" width="711px"><TabPages>
<cc1:DSCTabPage runat="server" ID="DSCTabPage1" Enabled="True" ImageURL="" Hidden="False" Title="RoutineWizard"><TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top">
        <!--工具列區-->
        <cc1:GlassButton ID="RWValueTextButton" runat="server" Text="ValueText" Width="200px" OnClick="RWValueTextButton_Click" />
        <cc1:GlassButton ID="RWReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="RWReadOnlyButton_Click" />
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:RoutineWizard ID="RWSample" runat="server" Width="400px"/>
        <BR /><cc1:DSCLabel id="DSCLabel1" runat="server" Width="88px" Text="結果："></cc1:DSCLabel> <cc1:SingleField id="RWResult" runat="server" Width="389px" MultiLine="true" Height="250px"></cc1:SingleField>
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" ID="DSCTabPage2" Enabled="True" ImageURL="" Hidden="False" Title="SingleDateTimeField"><TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top">
        <!--工具列區-->
        <cc1:GlassButton ID="DateTimeModeButton" runat="server" Text="DateTimeMode" Width="200px" OnClick="DateTimeModeButton_Click" />
        <cc1:GlassButton ID="ReadOnlyValueTextButton" runat="server" Text="ReadOnlyValueText" Width="200px" OnClick="ReadOnlyValueTextButton_Click" />
        <cc1:GlassButton ID="SDTReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="SDTReadOnlyButton_Click" />
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:SingleDateTimeField ID="SDTSample" runat="server" Width="200px" DateTimeMode="0" OnDateTimeClick="SDTSampleClick" />
        <BR /><cc1:DSCLabel id="DSCLabel2" runat="server" Width="88px" Text="結果："></cc1:DSCLabel> <cc1:SingleField id="SDTResult" runat="server" Width="389px" MultiLine="true" Height="250px"></cc1:SingleField>
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" ID="DSCTabPage3" Enabled="True" ImageURL="" Hidden="False" Title="SingleDropDownList"><TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top">
        <!--工具列區-->
        <cc1:GlassButton ID="SDDLValueTextButton" runat="server" Text="ValueText" Width="200px" OnClick="SDDLValueTextButton_Click" />
        <cc1:GlassButton ID="SDDLReadOnlyTextButton" runat="server" Text="ReadOnlyText" Width="200px" OnClick="SDDLReadOnlyTextButton_Click" />
        <cc1:GlassButton ID="SDDLReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="SDDLReadOnlyButton_Click" />
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:SingleDropDownList ID="SDDLSample" runat="server" OnSelectChanged="SDDLChange"/>
        <BR /><cc1:DSCLabel id="DSCLabel3" runat="server" Width="88px" Text="結果："></cc1:DSCLabel> <cc1:SingleField id="SDDLResult" runat="server" Width="389px" MultiLine="true" Height="250px"></cc1:SingleField>
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" ID="DSCTabPage4" Enabled="True" ImageURL="" Hidden="False" Title="SingleField"><TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top">
        <!--工具列區-->
        <cc1:GlassButton ID="SFValueTextButton" runat="server" Text="ValueText" Width="200px" OnClick="SFValueTextButton_Click" />
        <cc1:GlassButton ID="SFCustomCSSButton" runat="server" Text="CustonCSS" Width="200px" OnClick="SFCustomCSSButton_Click" />
        <cc1:GlassButton ID="SFReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="SFReadOnlyButton_Click" />
        <cc1:GlassButton ID="SFisAccountButton" runat="server" Text="isAccount" Width="200px" OnClick="SFisAccountButton_Click" />
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:SingleField ID="SFSample" runat="server" OnTextChanged="SFSampleChange"/>
        <br />
        <cc1:SingleField ID="SFAccount" runat="server" isMoney="true" isAccount="false" Fractor="2" alignRight="true" OnTextChanged="SFAccountChange"/>
        <cc1:DSCLabel id="SFAccountDSCR" runat="server" Text="isAccount = false"></cc1:DSCLabel>
        <cc1:DSCLabel id="DSCLabel4" runat="server" Width="88px" Text="結果："></cc1:DSCLabel> <cc1:SingleField id="SFResult" runat="server" Width="389px" MultiLine="true" Height="250px"></cc1:SingleField>
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
<cc1:DSCTabPage runat="server" ID="DSCTabPage5" Enabled="True" ImageURL="" Hidden="False" Title="SingleOpenWindowField">
<TabBody>
<table border=0 cellspacing=0 cellpadding=2>
<tr>
    <td valign="top">
        <!--工具列區-->
        <cc1:GlassButton ID="SOWPartialReadOnlyButton" runat="server" Text="PartialReadOnly" Width="200px" OnClick="SOWPartialReadOnlyButton_Click" />
        <cc1:GlassButton ID="SOWHiddenTextButton" runat="server" Text="HiddenText" Width="200px" OnClick="SOWHiddenTextButton_Click" />
        <cc1:GlassButton ID="SOWHiddenButtonButton" runat="server" Text="HiddenButton" Width="200px" OnClick="SOWHiddenButtonButton_Click" />
        <cc1:GlassButton ID="SOWshowReadOnlyFieldButton" runat="server" Text="showReadOnlyField" Width="200px" OnClick="SOWshowReadOnlyFieldButton_Click" />
        <cc1:GlassButton ID="SOWHiddenClearButtonButton" runat="server" Text="HiddenClearButton" Width="200px" OnClick="SOWHiddenClearButtonButton_Click" />
        <cc1:GlassButton ID="SOWReadOnlyButton" runat="server" Text="ReadOnly" Width="200px" OnClick="SOWReadOnlyButton_Click" />
        <p/>
        <cc1:DSCCheckBox ID="SOWTestBeforeButton" runat="server" Text="勾選時測試BeforeClick中加入條件"/>
        <p />
        <cc1:GlassButton ID="SOWGuidValueTextButton" runat="server" Text="GuidValueText" Width="200px" OnClick="SOWGuidValueTextButton_Click" />
        <cc1:GlassButton ID="SOWValueTextButton" runat="server" Text="ValueText" Width="200px" OnClick="SOWValueTextButton_Click" />
        <cc1:GlassButton ID="SOWReadOnlyValueTextButton" runat="server" Text="ReadOnlyValueText" Width="200px" OnClick="SOWReadOnlyValueTextButton_Click" />
    </td>
    <td valign="top">
        <!--測試元件區-->
        <cc1:SingleOpenWindowField ID="SOWSample" runat="server" OnBeforeClickButton="SOWChangeWhere" OnSingleOpenWindowButtonClick="SOWClick" guidField="OID" keyField="id" keyFieldType="STRING" serialNum="001" tableName="Users" />
        <BR /><cc1:DSCLabel id="DSCLabel5" runat="server" Width="88px" Text="結果："></cc1:DSCLabel> <cc1:SingleField id="SOWResult" runat="server" Width="389px" MultiLine="true" Height="250px"></cc1:SingleField>
    </td>
</tr>
</table>
</TabBody>
</cc1:DSCTabPage>
</TabPages>
</cc1:dsctabcontrol>
    
    </div>
    </form>
</body>
</html>
