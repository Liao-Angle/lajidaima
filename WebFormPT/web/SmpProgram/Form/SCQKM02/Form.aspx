<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_SCQKM01_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>客訴文件</title>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
    <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
    <table style="width: 700px;" border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td align="center" height="30">
                <font style="font-family: 標楷體; font-size: large;"><b>客訴文件申請單</b></font>
            </td>
        </tr>
    </table>
    <div>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td colspan="4" align="left" class="BasicFormHeadHead">
                    <label style="font-size: 12px; font-weight: bold;">
                        申請人信息</label>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="BasicFormHeadHead">
                    <table width="100%">
                        <tr align="center">
                            <td class="style3">
                                申請人
                            </td>
                            <td>
                                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="226px" showReadOnlyField="True"
                                    guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                                    idIndex="0" />
                            </td>
                            <td class="style7">
                                申請人分機
                            </td>
                            <td>
                                <cc1:SingleField ID="mobileuser" runat="server" Width="102px" Style="margin-left: 12px" />
                            </td>
                            <td class="style5">
                                申請人部門
                            </td>
                            <td>
                                <cc1:SingleField ID="partNouser" runat="server" Width="119px" />
                            </td>
                        </tr>
                    </table>
        </table>
        <table style="width: 700px;" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td>
                    <cc1:DSCTabControl ID="DSCTabDoc" runat="server" Height="347px" Width="690px" PageColor="White"
                        EnableTheming="True" Style="margin-right: 3px">
                        <TabPages>
                            <cc1:DSCTabPage runat="server" Title="文件屬性" Enabled="True" ImageURL="" Hidden="False"
                                ID="DSCTabPage1">
                                <TabBody>
                                    <table width="100%">
                                        <tr>
                                            <td width="120px" align="right" class="BasicFormHeadHead">
                                                <cc1:DSCLabel ID="kehu" runat="server" Text="客戶" Width="53px"></cc1:DSCLabel>
                                            </td>
                                            <td width="280px" align="left" class="BasicFormHeadDetail">
                                                <cc1:SingleField ID="kh" runat="server" Width="120px" Style="text-align: center" />
                                            </td>
                                            <td width="120px" align="right" class="BasicFormHeadHead">
                                                <cc1:DSCLabel ID="jizhong" runat="server" Text="機種" Width="63px"></cc1:DSCLabel>
                                            </td>
                                            <td width="280px" align="left" class="BasicFormHeadDetail">
                                                <cc1:SingleField ID="jz" runat="server" Width="100px" Style="text-align: center" />
                                            </td>
                                            <td width="120px" align="right" class="BasicFormHeadHead">
                                                <cc1:DSCLabel ID="biaoti" runat="server" Text="標題" Width="46px"></cc1:DSCLabel>
                                            </td>
                                            <td width="280px" align="left" class="BasicFormHeadDetail">
                                                <cc1:SingleField ID="bt" runat="server" Width="200px" Style="text-align: center" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="120px" align="right" class="BasicFormHeadHead">
                                                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="階段" Width="53px"></cc1:DSCLabel>
                                            </td>
                                            <td width="280px" align="left" class="BasicFormHeadDetail">
                                                <cc1:SingleDropDownList ID="jd" runat="server" Width="120px" Style="text-align: center" />
                                            </td>
                                            <td width="120px" align="right" class="BasicFormHeadHead">
                                                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="類別" Width="63px"></cc1:DSCLabel>
                                            </td>
                                            <td width="280px" align="left" class="BasicFormHeadDetail">
                                                <cc1:SingleDropDownList ID="lb" runat="server" Width="100px" Style="text-align: center" />
                                            </td>
                                            <td width="120px" align="right" class="BasicFormHeadHead">
                                                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="區域" Width="46px"></cc1:DSCLabel>
                                            </td>
                                            <td width="280px" align="left" class="BasicFormHeadDetail">
                                                <cc1:SingleDropDownList ID="qy" runat="server" Width="200px" Style="text-align: center" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="120px" align="right" class="BasicFormHeadHead">
                                                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="狀態" Width="53px"></cc1:DSCLabel>
                                            </td>
                                            <td width="280px" align="left" class="BasicFormHeadDetail">
                                                <cc1:SingleDropDownList ID="zt" runat="server" Width="120px" Style="text-align: center" />
                                            </td>
                                            <td width="120px" align="right" class="BasicFormHeadHead">
                                                <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="ID" Width="63px"></cc1:DSCLabel>
                                            </td>
                                            <td width="280px" align="left" class="BasicFormHeadDetail">
                                                 <cc1:SingleOpenWindowField ID="DocGUID" runat="server" 
                                            Width="139px" guidField="GUID" keyField="DocNumber" 
                                            serialNum="001" tableName="Document" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="136px" Enabled="False" ReadOnly="True" Display="True" 
                                            HiddenButton="True"  DisabledCleanValue="False" />
                                            </td>
                                            <td width="120px" align="right" class="BasicFormHeadHead">
                                                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="部門" Width="46px"></cc1:DSCLabel>
                                            </td>
                                            <td width="280px" align="left" class="BasicFormHeadDetail">
                                                <cc1:SingleField ID="bm" runat="server" Width="200px" 
                                                    Style="text-align: center" />
                                            </td>
                                        </tr>
                                    </table>
                                </TabBody>
                            </cc1:DSCTabPage>
                            <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" Hidden="False" ImageURL=""
                                Title="附件">
                                <TabBody>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <cc1:OpenFileUpload ID="FileUploadAtta" runat="server" OnAddOutline="Atta_AddOutline" />
                                                <cc1:GlassButton ID="ButtonUpload" runat="server" Height="20px" OnClick="Upload_Click"
                                                    Text="上傳檔案" Width="70px" />
                                            </td>
                                            <tr>
                                                <td>
                                                    <cc1:DSCLabel ID="LblAttaFileName" runat="server" Style="text-align: left" Text="檔案名稱"
                                                        TextAlign="2" Width="72px" />
                                                </td>
                                                <td>
                                                    <cc1:SingleField ID="AttaFileName" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <cc1:OutDataList ID="DataListAttachment" runat="server" Height="290px" OnBeforeDeleteData="Atta_BeforeDeleteData"
                                                        OnSaveRowData="DataListAttachment_SaveRowData" OnShowRowData="DataListAttachment_ShowRowData"
                                                        Width="640px" />
                                                </td>
                                            </tr>
                                    </table>
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
