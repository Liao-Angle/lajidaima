<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPKM005_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/Enterprise.css" rel="stylesheet" type="text/css" />
    <title>文件進階查詢</title>
    <style type="text/css">
        .style1
        {
            width: 487px;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr><td>
            <cc1:GlassButton ID="GlassButtonChange" runat="server" Height="20px" Text="變更" 
                Width="60px" />
            </td><td>
                <cc1:GlassButton ID="GlassButtonAccess" runat="server" Height="20px" Text="調閱" 
                    Width="60px" />
            </td>
            <td>
                <cc1:GlassButton ID="GlassButtonVoid" runat="server" Height="20px" Text="作廢" 
                    Width="60px" />
            </td>
        </tr>
    </table>
    <br>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >       
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>知識文件</b></font></td>
        </tr>
    </table>
    <table>
        <tr><td>
            <cc1:DSCTabControl ID="DSCTabDoc" runat="server" Height="347px" 
                Width="670" PageColor="White">
                <TabPages>
                    <cc1:DSCTabPage runat="server" Title="文件屬性" Enabled="True" ImageURL="" 
                        Hidden="False" ID="TabIndexCard">
                        <TabBody>
                            <table width="100%">
                                <tr><td align=right><cc1:DSCLabel ID="LblSite" runat="server" Text="公司別" Width="70px" 
                                                IsNecessary="False" TextAlign="2" ReadOnly="True" /></td>
                                    <td><cc1:SingleDropDownList ID="Site" runat="server" Width="90px" ReadOnly="True" /></td>
                                    <td align=right></td>
                                    <td colspan=3><asp:Label ID="LblTip" ForeColor="Red" style="font-size: 9pt; text-align: left;" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr><td align=right>
                                        <cc1:DSCLabel ID="LblDocGUID" runat="server" Text="文件編號" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td><cc1:SingleOpenWindowField ID="DocGUID" runat="server" 
                                            Width="120px" guidField="GUID" keyField="DocNumber" 
                                            serialNum="001" tableName="Document" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="130px" IgnoreCase="True" /></td>
                                    <td align=right>
                                        <cc1:DSCLabel ID="LblRevGUID" runat="server" Text="版本" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td><cc1:SingleDropDownList ID="RevGUID" runat="server" 
                                            Width="158px" ReadOnly="True" onselectchanged="RevGUID_SelectChanged" /></td>
                                    <td align=right>
                                        <cc1:DSCLabel ID="LBLStatus" runat="server" Text="狀態" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td><cc1:SingleDropDownList ID="Status" runat="server" 
                                            Width="90px" ReadOnly="True" /></td>
                                </tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblName" runat="server" Text="文件名稱" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td colspan=5><cc1:SingleField ID="Name" runat="server" Width="565px" /></td>
                                </tr>
                                <tr><td align=right>
                                        <cc1:DSCLabel ID="LblMajorTypeGUID" runat="server" Text="主分類" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td><cc1:SingleOpenWindowField ID="MajorTypeGUID" runat="server" 
                                            Width="120px" guidField="GUID" keyField="Name" 
                                            serialNum="001" tableName="MajorType" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="90px" 
                                            onbeforeclickbutton="MajorTypeGUID_BeforeClickButton" IgnoreCase="True" /></td>
                                    <td align=right>
                                        <cc1:DSCLabel ID="LblSubTypeGUID" runat="server" Text="子分類" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td colspan="3">
                                        <cc1:SingleOpenWindowField ID="SubTypeGUID" runat="server" 
                                            Width="355px" guidField="GUID" keyField="Name" 
                                            serialNum="001" tableName="SubType" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="293px" 
                                            onbeforeclickbutton="SubTypeGUID_BeforeClickButton" IgnoreCase="True" /></td>
                                </tr>
                                <tr><td align=right>
                                        <cc1:DSCLabel ID="LblDocTypeGUID" runat="server" Text="文件類別" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                        <td colspan=3>
                                            <cc1:SingleOpenWindowField ID="DocTypeGUID" runat="server" 
                                            Width="360px" guidField="GUID" keyField="Name" 
                                            serialNum="001" tableName="DocType" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="330px" 
                                                onbeforeclickbutton="DocTypeGUID_BeforeClickButton" IgnoreCase="True" /></td>
                                        <td><cc1:DSCLabel ID="LblExternal" runat="server" Text="外部文件" Width="70px" TextAlign="2" />
                                        </td>
                                        <td>
                                            <cc1:SingleDropDownList ID="External" runat="server" Width="40px" ReadOnly="True" />
                                        </td>
                                </tr>
                                <tr><td align=right><cc1:DSCLabel ID="LblAuthorOrgUnitGUID" runat="server" Text="製定部門" Width="70px" 
                                            IsNecessary="False" TextAlign="2" /></td>
                                    <td><cc1:SingleOpenWindowField ID="AuthorOrgUnitGUID" runat="server" 
                                            Width="115px" guidField="OID" keyField="id" 
                                            serialNum="001" tableName="OrgUnit" FixReadOnlyValueTextWidth="100px" 
                                            FixValueTextWidth="107px" HiddenButton="True" idIndex="2" valueIndex="1" /></td>
                                    <td align=right>
                                        <cc1:DSCLabel ID="LblDocPropertyGUID" runat="server" Text="文件性質" Width="70px" 
                                            IsNecessary="True" TextAlign="2" ReadOnly="True" /></td>
                                    <td><cc1:SingleDropDownList ID="DocPropertyGUID" runat="server" 
                                            Width="155px"/></td>
                                    <td align=right><cc1:DSCLabel ID="LblConfidentialLevel" runat="server" Text="機密等級" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td><cc1:SingleDropDownList ID="ConfidentialLevel" runat="server" 
                                            Width="107px" /></td>
                                </tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblAuthorGUID" runat="server" Text="作者/收集者" Width="80px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td><cc1:SingleOpenWindowField ID="AuthorGUID" runat="server" Width="90px" 
                                            guidField="OID" keyField="id" serialNum="003" 
                                            tableName="Users" Height="80px" 
                                            idIndex="3" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="80px" DisabledCleanValue="False" HiddenButton="True" /></td>
                                    <td align="right">
                                        <cc1:DSCLabel ID="LblCreator" runat="server" Text="建立者" Width="70px" 
                                            IsNecessary="False" TextAlign="2" /></td>
                                    <td><cc1:SingleOpenWindowField ID="CreatorGUID" runat="server" Width="90px" 
                                            guidField="OID" keyField="id" serialNum="003" 
                                            tableName="Users" Height="80px" 
                                            idIndex="3" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="80px" HiddenButton="True" /></td>
                                    <td align="right"></td>
                                    <td></td>
                                </tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblAbstract" runat="server" Text="文件摘要" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td colspan=5>
                                        <cc1:SingleField ID="Abstract" runat="server" Width="566px" Height="80px" 
                                            MultiLine="True" /></td></tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblKeyWords" runat="server" Text="關鍵字" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td colspan=5>
                                        <cc1:SingleField ID="KeyWords" runat="server" Width="564px" Height="40px" 
                                            MultiLine="True" /></td>
                                </tr>
                                <tr><td><cc1:DSCLabel ID="LblEffectiveDate" runat="server" Text="生效日" Width="70px" 
                                            TextAlign="2" /></td>
                                    <td><cc1:SingleField ID="EffectiveDate" runat="server" Width="70px" ReadOnly="True" /></td>
                                    <td><cc1:DSCLabel ID="LblExpiryDate" runat="server" Text="到期日" Width="70px" 
                                            TextAlign="2" /></td>
                                    <td><cc1:SingleDateTimeField ID="ExpiryDate" runat="server" Width="110px" /></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="TabBelongGroup" runat="server" Enabled="True" Hidden="False" 
                        ImageURL="" Title="歸屬群組">
                        <TabBody>
                            <table width="100%">
                                <tr>
                                    <td colspan=2>
                                        <cc1:DSCLabel ID="LblDocBelongGroupTip" runat="server" 
                                            Text="*歸屬群組為文件的製作單位，擁有文件變更/作廢/閱讀各版本附件等權限" Width="580px" />
                                    </td>
                                </tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblDocBelongGroup" runat="server" Width="70px" Text="歸屬群組" 
                                            TextAlign="2" /></td>
                                    <td width="555px">
                                        <cc1:SingleOpenWindowField ID="DocBelongGroupGUID" runat="server" 
                                            Width="500px" showReadOnlyField="True" guidField="OID" keyField="id" 
                                            serialNum="001" tableName="SPKM001" FixReadOnlyValueTextWidth="400px" 
                                            FixValueTextWidth="60px" IgnoreCase="True" /></td>
                                </tr>
                                <tr><td colspan=2>
                                        <cc1:OutDataList ID="DataListDocBelongGroup" runat="server" height="290px" 
                                            width="640px" showExcel="True" 
                                            onsaverowdata="DataListDocBelongGroup_SaveRowData" 
                                            onshowrowdata="DataListDocBelongGroup_ShowRowData" 
                                            IsHideSelectAllButton="True" IsShowCheckBox="False" /></td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="TabAttachment" runat="server" Enabled="True" 
                        Hidden="False" ImageURL="" Title="附件">
                        <TabBody>
                            <table width="100%">
                                <tr><td><cc1:OpenFileUpload ID="FileUploadAtta" runat="server" onaddoutline="Atta_AddOutline" />
                                        <cc1:GlassButton ID="ButtonUpload" runat="server" Height="20px" 
                                            onclick="Upload_Click" Text="上傳檔案" Width="70px" /></td>
                                <tr>
                                    <td>
                                        <cc1:DataList ID="DataListAttachment" runat="server" CertificateMode="False" 
                                            height="290px" IsHideSelectAllButton="True" IsShowCheckBox="False" 
                                            onbeforedeletedata="Atta_BeforeDeleteData" 
                                            onbeforeopenwindow="Atta_BeforeOpenWindows" showExcel="True" width="640px" />
                                    </td>
                                </tr>
                                 </table></TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="TabReference" runat="server" Enabled="True" 
                        Hidden="False" ImageURL="" Title="關聯文件">
                        <TabBody>
                            <table width="100%">
                                <tr><td align="right" Width="70px">
                                        <cc1:DSCLabel ID="LblSource" runat="server" Width="70px" Text="來源" 
                                            TextAlign="2" /></td>
                                    <td>
                                        <cc1:SingleDropDownList ID="Source" runat="server" 
                                            Width="70px" /></td>
                                    <td align="right" Width="70px">
                                        <cc1:DSCLabel ID="LblReferenceGUID" runat="server" Width="70px" Text="文件號碼" 
                                            TextAlign="2" /></td>
                                    <td>
                                        <cc1:SingleOpenWindowField ID="ReferenceGUID" runat="server" 
                                            Width="420px" showReadOnlyField="True" guidField="GUID" keyField="DocNumber" 
                                            serialNum="001" tableName="Document" FixReadOnlyValueTextWidth="300px" 
                                            FixValueTextWidth="90px" 
                                            onbeforeclickbutton="ReferenceGUID_BeforeClickButton" IgnoreCase="True"/></td>
                                </tr>
                                <tr><td colspan=4>
                                        <cc1:OutDataList ID="DataListReference" runat="server" height="290px" width="640px" 
                                            showExcel="True"
                                            OnSaveRowData="DataListReference_SaveRowData" 
                                            OnShowRowData="DataListReference_ShowRowData" 
                                            onbeforeopenwindow="Reference_BeforeOpenWindow" 
                                            IsHideSelectAllButton="True" IsShowCheckBox="False" /></td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="TabReader" runat="server" Enabled="True" Hidden="False" 
                        ImageURL="" Title="讀者">
                        <TabBody>
                            <table width="100%">
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblReaderBelongGroupType" runat="server" Width="70px" 
                                            Text="對象" TextAlign="2" /></td>
                                    <td>
                                        <cc1:SingleDropDownList ID="ReaderBelongGroupType" runat="server" 
                                            Width="70px" onselectchanged="ReaderBelongGroupType_SelectChanged" /></td>
                                    <td align="right">
                                        <cc1:DSCLabel ID="LblBelongGroup" runat="server" Width="70px" Text="讀者名稱" 
                                            TextAlign="2" /></td>
                                    <td>
                                        <cc1:SingleOpenWindowField ID="ReaderBelongGroupGUID" runat="server" 
                                            Width="350px" showReadOnlyField="True" guidField="OID" keyField="id" 
                                            serialNum="001" tableName="SPKM001" FixReadOnlyValueTextWidth="200px" 
                                            FixValueTextWidth="120px" IgnoreCase="True" />
                                    </td>
                                    <td>
                                        <cc1:GlassButton ID="GbDcoTypeReader" runat="server" Height="16px" Width="64px" 
                                            Text="預設讀者" />
                                    </td>
                                </tr>
                                <tr><td colspan="5">
                                        <cc1:OutDataList ID="DataListReader" runat="server" height="290px" width="640px" 
                                            showExcel="True"
                                            onsaverowdata="DataListReader_SaveRowData" 
                                            onshowrowdata="DataListReader_ShowRowData" IsHideSelectAllButton="True" 
                                            IsShowCheckBox="False" /></td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="TabAccess" runat="server" Enabled="True" Hidden="False" 
                        ImageURL="" Title="調閱記錄">
                        <TabBody>
                            <table width="100%">
                               <tr><td>
                                        <cc1:OutDataList ID="DataListAccess" runat="server" height="290px" width="640px" 
                                            showExcel="True"
                                            IsShowCheckBox="False" /></td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="TabHistory" runat="server" Enabled="True" Hidden="False" 
                        ImageURL="" Title="文件歷史記錄">
                        <TabBody>
                            <table width="100%">
                                <tr><td>
                                    <cc1:DataList ID="DataListHistory" runat="server" height="290px" width="640px" 
                                            isShowAll="True" PageSize="50" showExcel="True" 
                                        IsShowCheckBox="False" NoAdd="True" NoDelete="True" NoModify="True" 
                                        IsHideSelectAllButton="True" /></td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                </TabPages>
            </cc1:DSCTabControl>
            </td></tr>
    </table>
    </div>
    </form>
</body>
</html>
