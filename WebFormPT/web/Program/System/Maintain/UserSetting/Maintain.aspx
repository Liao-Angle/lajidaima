<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_System_Maintain_UserSetting_Maintain" %>

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
        <table border=0 cellpadding=2 cellspacing=0 width=750px class=BasicFormHeadBorder>
        <tr>
            <td colspan=4 class=BasicFormHeadDetail>
                <cc1:GlassButton ID="SaveButton" runat="server" Text="儲存設定" Height="25px" Width="91px" OnClick="SaveButton_Click" ConfirmText="你確定要儲存設定？"/>
            </td>
        </tr>
        <tr>
            <td class=BasicFormHeadHead style="width: 136px">
                <cc1:DSCLabel ID="VRGUIDTEXT" runat="server" Text="班別設定"/>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="VRGUID" runat="server" Width="187px" />
            </td>
            <td style="height: 26px; width: 136px;" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="工作處理"/>
            </td>
            <td style="height: 26px" class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="FlowMethod" runat="server" Width="187px"  />
            </td>
        </tr>
        <tr>
            <td style="height: 26px; width: 136px;" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="重設密碼"/>
            </td>
            <td style="height: 26px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="USERPWD" runat="server" Width="185px" />
            </td>
            <td style="height: 26px; width: 136px;" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="使用語系"/>
            </td>
            <td style="height: 26px" class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="USERLANGUAGE" runat="server" Width="187px"  />
            </td>
        </tr>
        <tr>
            <td colspan=2 class=BasicFormHeadDetail>
                <cc1:DSCCheckBox ID="RECEIVEMAIL" runat="server" Text="是否收取Email" ReadOnly="True" />
                <cc1:DSCCheckBox ID="RECEIVEMSG" runat="server" Text="是否線上即時通知"/>
            </td>
            <td style="height: 26px; width: 136px;" class=BasicFormHeadHead>
            <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="簽名圖檔"/>
            </td>
            <td style="height: 26px; margin-left: 40px;" class=BasicFormHeadDetail>
            <div>
                <cc1:GlassButton ID="gtnUploadPersonalImage" runat="server" Text="上傳" 
                    onclick="gtnUploadPersonalImage_Click" Width="100px" />                
                <img id="imgPreview" alt="" src="" style="vertical-align:middle;display:none; width:150;height:40" />
            </div>            
                <cc1:OpenFileUpload ID="ofuSignImage" runat="server" 
                    onaddoutline="ofuSignImage_AddOutline" />
            </td>
        </tr>
        <tr>
            <td style="height: 26px; width: 136px;" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="代理期間" Width="73px"/><cc1:GlassButton ID="GlassButton1" runat="server" Text="清除" Height="25px" Width="32px" OnClick="ClearButton_Click"/>
            </td>
            <td style="height: 26px" class=BasicFormHeadDetail valign=bottom colspan=3>
                <cc1:SingleDateTimeField ID="StartTime" runat="server" Width="132px" DateTimeMode="1" />～<cc1:SingleDateTimeField ID="EndTime" runat="server" Width="132px" DateTimeMode="1"  />
            </td>
        </tr>
        <tr>
            <td colspan=4 class=BasicFormHeadDetail>
                <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="279px" Width="731px">
                    <TabPages>
                        <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="通用代理人">
                        <TabBody>
                            <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="代理人：" Width="72px" />
                            <cc1:SingleOpenWindowField ID="SUBS" runat="server" showReadOnlyField="True"
                                Width="248px" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
                            <cc1:OutDataList ID="SubsList" runat="server" Height="210px" Width="682px" OnSaveRowData="SubsList_SaveRowData" OnShowRowData="SubsList_ShowRowData" />
                        </TabBody>
                        </cc1:DSCTabPage>
                        <cc1:DSCTabPage ID="DSCTabPage2" runat="server" Enabled="True" ImageURL="" Title="流程代理人">
                        <TabBody>
                            <table border=0 cellspacing=1 cellpadding=0>
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
                                    <cc1:SingleOpenWindowField ID="SubUser" runat="server" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users" Width="252px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="發起部門"/>
                                </td>
                                <td>
                                    <cc1:SingleOpenWindowField ID="InvokeDept" runat="server" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="OrgUnit" Width="252px" />
                                </td>
                                <td>
                                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="永久代理"/>
                                </td>
                                <td>
                                    <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server"/>
                                </td>
                            </tr>
                            </table><cc1:OutDataList ID="ProcessSubList" runat="server" Height="187px" Width="682px" OnSaveRowData="ProcessSubList_SaveRowData" OnShowRowData="ProcessSubList_ShowRowData" />
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
