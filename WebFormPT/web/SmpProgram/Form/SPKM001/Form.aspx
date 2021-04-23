<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPKM001_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>文件新增</title>
    <style type="text/css">
        .style4
        {
            width: 166px;
        }
        .style5
        {
            width: 72px;
        }
        .style7
        {
            width: 129px;
        }
    </style>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >       
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>知識文件新增申請單</b></font></td>
        </tr>
    </table>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=5>
                <cc1:SingleField ID="Subject" runat="server" Width="577px" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
            <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="公司別" Width="70px" 
                IsNecessary="False" TextAlign="2"/></td>
           <td class="BasicFormHeadDetail" colspan=5>
              <cc1:SingleDropDownList ID="Site" runat="server" Width="150px" /></td>   
        </tr>   
        <tr>  
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckBy1GUID" runat="server" Text="審核人一" Width="70px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckBy1GUID" runat="server" Width="140px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="60px" 
                    FixValueTextWidth="50px" 
                    onbeforeclickbutton="CheckBy1GUID_BeforeClickButton" IgnoreCase="True" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckBy2GUID" runat="server" Text="審核人二" Width="70px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckBy2GUID" runat="server" Width="141px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="60px" 
                    FixValueTextWidth="50px" 
                    onbeforeclickbutton="CheckBy2GUID_BeforeClickButton" IgnoreCase="True" />
            </td>
        </tr>
        </table>
        <br />
    <table>
        <tr><td>
            <cc1:DSCTabControl ID="DSCTabDoc" runat="server" Height="347px" 
                Width="660px" PageColor="White" EnableTheming="True" 
                style="margin-right: 3px">
                <TabPages>
                    <cc1:DSCTabPage runat="server" Title="文件屬性" Enabled="True" ImageURL="" 
                        Hidden="False" ID="TabIndexCard">
                        <TabBody>
                            <table style="width: 100%">
                                <tr><td align=right>
                                        <cc1:DSCLabel ID="LblDocGUID" runat="server" Text="文件編號" Width="70px" 
                                            IsNecessary="False" TextAlign="2" />
                                    </td>
                                    <td class="style7">                                   
                                    <cc1:SingleOpenWindowField ID="DocGUID" runat="server" 
                                            Width="139px" guidField="GUID" keyField="DocNumber" 
                                            serialNum="001" tableName="Document" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="136px" Enabled="False" ReadOnly="True" Display="True" 
                                            HiddenButton="True"  DisabledCleanValue="False" /> </td>
                                            <td class="style5">                                        
                                             <cc1:GlassButton ID="GlassButtonViewDoc" runat="server" Height="20px" 
                                            Text="檢視文件" Width="60px" onclick="GlassButtonViewDoc_Click" 
                                                    onload="GlassButtonViewDoc_Load" ReadOnly="False" /></td>   
                                            <td></td>                                
                                    <td colspan="2" align=right>
                                        <cc1:DSCLabel ID="LblRevGUID" runat="server" Text="版本" Width="43px" 
                                            IsNecessary="True" TextAlign="2" /></td>                                                                     
                                    <td>
                                        <cc1:SingleDropDownList ID="RevGUID" runat="server" ReadOnly="True" 
                                            Width="77px" />
                                        <cc1:SingleField ID="Released" runat="server" Display="False" ReadOnly="True" 
                                            Width="90px" />
                                    </td>
                                </tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblName" runat="server" Text="文件名稱" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td colspan=6><cc1:SingleField ID="Name" runat="server" Width="547px" /></td>
                                </tr>
                                <tr><td align=right>
                                        <cc1:DSCLabel ID="LblMajorTypeGUID" runat="server" Text="主分類" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td class="style7">
                                        <cc1:SingleOpenWindowField ID="MajorTypeGUID" runat="server" 
                                            Width="120px" guidField="GUID" keyField="Name" 
                                            serialNum="001" tableName="MajorType" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="90px" 
                                            onbeforeclickbutton="MajorTypeGUID_BeforeClickButton" IgnoreCase="True" /></td>
                                    <td align=right class="style5">
                                        <cc1:DSCLabel ID="LblSubTypeGUID" runat="server" Text="子分類" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td colspan="4">
                                        <cc1:SingleOpenWindowField ID="SubTypeGUID" runat="server" 
                                            Width="324px" guidField="GUID" keyField="Name" 
                                            serialNum="001" tableName="SubType" FixReadOnlyValueTextWidth="90px" 
                                            FixValueTextWidth="280px" 
                                            onbeforeclickbutton="SubTypeGUID_BeforeClickButton" 
                                            onsingleopenwindowbuttonclick="SubTypeGUID_SingleOpenWindowButtonClick" IgnoreCase="True" 
                                            /></td>
                                </tr>
                                <tr><td align=right>
                                        <cc1:DSCLabel ID="LblDocTypeGUID" runat="server" Text="文件類別" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                        <td colspan=3>
                                            <cc1:SingleOpenWindowField ID="DocTypeGUID" runat="server" 
                                            Width="362px" guidField="GUID" keyField="Name" 
                                            serialNum="001" tableName="DocType" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="325px"                                                 
                                                onbeforeclickbutton="DocTypeGUID_BeforeClickButton" 
                                                onsingleopenwindowbuttonclick="DocTypeGUID_SingleOpenWindowButtonClick1" /></td>
										<td align=right colspan="2">
                                            <cc1:DSCLabel ID="LblExternal" runat="server" Text="包含外部文件" Width="88px" 
                                                TextAlign="2" />
                                        </td>                                      
                                    <td>
                                        <cc1:SingleDropDownList ID="External" runat="server" ReadOnly="False" 
                                            Width="42px" />
                                    </td>
                                </tr>                               
                                <tr><td align="right"><cc1:DSCLabel ID="LblAuthorOrgUnitGUID" runat="server" Text="製定部門" Width="70px" 
                                            IsNecessary="False" TextAlign="2" /></td>
                                    <td>
                                        <cc1:SingleOpenWindowField ID="AuthorOrgUnitGUID" runat="server" 
                                            Width="136px" guidField="OID" keyField="id" 
                                            serialNum="001" tableName="OrgUnit" FixReadOnlyValueTextWidth="100px" 
                                            FixValueTextWidth="136px" HiddenButton="True" idIndex="2" valueIndex="1" />
                                    </td>                                  
                                    <td align=right>
                                        <cc1:DSCLabel ID="LblDocPropertyGUID" runat="server" Text="文件性質" Width="63px" 
                                            IsNecessary="True" TextAlign="2" ReadOnly="True" /></td>
                                    <td><cc1:SingleDropDownList ID="DocPropertyGUID" runat="server" 
                                            Width="130px"/></td>
                                    <td align=right colspan="2"><cc1:DSCLabel ID="LblConfidentialLevel" runat="server" Text="機密等級" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td>
                                        <cc1:SingleDropDownList ID="ConfidentialLevel" runat="server" Width="77px" />
                                    </td>
                                </tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblAuthorGUID" runat="server" Text="作者/收集者" Width="80px" 
                                            IsNecessary="False" TextAlign="2" /></td>
                                    <td >
                                        <cc1:SingleOpenWindowField ID="AuthorGUID" runat="server" Width="140px" 
                                            showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                                            tableName="Users" Height="80px" 
                                            idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="60px" 
                                            FixValueTextWidth="50px" DisabledCleanValue="False" 
                                            onsingleopenwindowbuttonclick="AuthorGUID_SingleOpenWindowButtonClick" 
                                            onbeforeclickbutton="AuthorGUID_BeforeClickButton" IgnoreCase="True" /></td>
                                    <td align="right" class="style5">
                                        <cc1:DSCLabel ID="LblCreator" runat="server" Text="建立者" Width="70px" 
                                            IsNecessary="False" TextAlign="2" /></td>
                                    <td><cc1:SingleOpenWindowField ID="CreatorGUID" runat="server" Width="90px" 
                                            guidField="OID" keyField="id" serialNum="003" 
                                            tableName="Users" Height="80px" 
                                            idIndex="3" FixReadOnlyValueTextWidth="80px" 
                                            FixValueTextWidth="80px" HiddenButton="True" /></td>
                                    <td colspan=3></td>                                    
                                </tr>                               
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblAbstract" runat="server" Text="文件摘要" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td colspan=6>
                                        <cc1:SingleField ID="Abstract" runat="server" Width="555px" Height="80px" 
                                            MultiLine="True" /></td></tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblKeyWords" runat="server" Text="關鍵字" Width="70px" 
                                            IsNecessary="True" TextAlign="2" /></td>
                                    <td colspan=6>
                                        <cc1:SingleField ID="KeyWords" runat="server" Width="555px" Height="40px" 
                                            MultiLine="True" /></td>
                                </tr>   
                                <tr><td align="right"><cc1:DSCLabel ID="LblEffectiveDate" runat="server" Text="生效日" Width="70px" 
                                            TextAlign="2" /></td>
                                    <td class="style7"><cc1:SingleField ID="EffectiveDate" runat="server" Width="110px" ReadOnly="True"/></td>
                                    <td class="style5">
                                        &nbsp;</td>                                           
                                             <td>&nbsp;</td>                                                           
                                     <td align=right>
                                         &nbsp;</td>
                                    <td class="style4">&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
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
                                        <asp:Label ID="Label1" runat="server" Text="*歸屬群組為文件的製作單位，擁有文件變更/作廢/閱讀各版本附件等權限" 
                                            style="font-size: 9pt" ForeColor="Red"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan=2>
                                        <asp:Label ID="Label2" runat="server" Text="*異動子類別將新重新取得歸屬群組" 
                                            style="font-size: 9pt" ForeColor="Red"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblDocBelongGroup" runat="server" Width="70px" Text="歸屬群組" 
                                            TextAlign="2" /></td>
                                    <td width="555px">
                                        <cc1:SingleOpenWindowField ID="DocBelongGroupGUID" runat="server" 
                                            Width="560px" showReadOnlyField="True" guidField="OID" keyField="id" 
                                            serialNum="001" tableName="SPKM001" FixReadOnlyValueTextWidth="300px" 
                                            FixValueTextWidth="180px" IgnoreCase="True" /></td>
                                </tr>
                                <tr><td colspan=2>
                                        <cc1:OutDataList ID="DataListDocBelongGroup" runat="server" height="290px" 
                                            width="640px" 
                                            onsaverowdata="DataListDocBelongGroup_SaveRowData" 
                                            onshowrowdata="DataListDocBelongGroup_ShowRowData" EnableTheming="True" /></td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="TabAttachment" runat="server" Enabled="True" 
                        Hidden="False" ImageURL="" Title="附件">
                        <TabBody>
                            <table width="100%">
                                <tr>
                                    <td colspan="4" style="text-align: left">
                                        <asp:Label ID="Label3" runat="server" ForeColor="Red" 
                                            style="font-size: 9pt; text-align: left;" Text="*附件若為外部文件，請先選取附件->指定「是否為外部文件」下拉值為「是」-> 點選修改銨鈕" 
                                            ></asp:Label>
                                    </td>
                                </tr>
                                <tr><td><cc1:OpenFileUpload ID="FileUploadAtta" runat="server" onaddoutline="Atta_AddOutline" />
                                        <cc1:GlassButton ID="ButtonUpload" runat="server" Height="20px" 
                                            onclick="Upload_Click" Text="上傳檔案" Width="70px" />
                                    </td>
                                    <td colspan="3" style="text-align: left">
                                        <asp:HyperLink ID="SampleHyperLink" runat="server" Text="文件範本下載" Font-Size="Smaller"                                             
                                            Target="_blank" />  
                                    </td>
                                <tr>
                                    <td>
                                        <cc1:DSCLabel ID="LblAttaFileName" runat="server" style="text-align: left" 
                                            Text="檔案名稱" TextAlign="2" Width="72px" />
                                    </td>
                                    <td>
                                        <cc1:SingleField ID="AttaFileName" runat="server" Width="350px" />
                                    </td>
                                    <td>
                                        <cc1:DSCLabel ID="LblAttaExternal" runat="server" Text="是否為外部文件" TextAlign="2" 
                                            Width="120px" />
                                    </td>
                                    <td>
                                        <cc1:SingleDropDownList ID="AttaExternal" runat="server" Width="40px" />
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <cc1:OutDataList ID="DataListAttachment" runat="server" Height="290px" 
                                            onbeforedeletedata="Atta_BeforeDeleteData" 
                                            onsaverowdata="DataListAttachment_SaveRowData" 
                                            onshowrowdata="DataListAttachment_ShowRowData" Width="640px" />
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
                                            onbeforeclickbutton="ReferenceGUID_BeforeClickButton" IgnoreCase="True" /></td>
                                </tr>
                                <tr><td colspan=4>
                                        <cc1:OutDataList ID="DataListReference" runat="server" height="290px" width="640px"
                                            OnSaveRowData="DataListReference_SaveRowData" 
                                            OnShowRowData="DataListReference_ShowRowData" EnableTheming="True" /></td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="TabReader" runat="server" Enabled="True" Hidden="False" 
                        ImageURL="" Title="讀者">
                        <TabBody>
                            <table width="100%">
                                <tr>
                                   <td colspan="5" style="text-align: left">
                                        <asp:Label ID="Label4" runat="server" ForeColor="Red" 
                                            style="font-size: 9pt; text-align: left;" Text="*分享給新普所有人員閱讀，群組名稱請選SMPKMALL" 
                                           ></asp:Label>                                  
                                </tr>
                                <tr><td align="right">
                                        <cc1:DSCLabel ID="LblReaderBelongGroupType" runat="server" Width="70px" 
                                            Text="對象" TextAlign="2" /></td>
                                    <td>
                                        <cc1:SingleDropDownList ID="ReaderBelongGroupType" runat="server" 
                                            Width="70px" onselectchanged="ReaderBelongGroupType_SelectChanged" /></td>
                                    <td align="right">
                                        <cc1:DSCLabel ID="LblBelongGroupGUID" runat="server" Width="70px" Text="讀者名稱" 
                                            TextAlign="2" /></td>
                                    <td>
                                        <cc1:SingleOpenWindowField ID="ReaderBelongGroupGUID" runat="server" 
                                            Width="350px" showReadOnlyField="True" guidField="OID" keyField="id" 
                                            serialNum="001" tableName="SPKM001" FixReadOnlyValueTextWidth="200px" 
                                            FixValueTextWidth="120px" IgnoreCase="True" /></td>   
                                     <td>
                                        <cc1:GlassButton ID="GbDcoTypeReader" runat="server" Height="16px" Width="64px" 
                                            Text="預設讀者" onclick="GbDcoTypeReader_Click" 
                                             onload="GbDcoTypeReader_Load"  /> </td>
                                </tr>                               
                                <tr><td colspan="5">
                                        <cc1:OutDataList ID="DataListReader" runat="server" height="290px" width="640px"
                                            onsaverowdata="DataListReader_SaveRowData" 
                                            onshowrowdata="DataListReader_ShowRowData" /></td>
                                
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
                                            isShowAll="True" PageSize="50" 
                                        IsHideSelectAllButton="True" IsShowCheckBox="False" IsGeneralUse="True" 
                                        ReadOnly="True" NoAdd="True" NoDelete="True" NoModify="True" /></td>
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
