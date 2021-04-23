<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPSP001_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>My Default</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr><td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="85px" 
                    IsNecessary="True" TextAlign="2" ForeColor="Red"/>
            </td>
            <td class=BasicFormHeadDetail>
            <cc1:SingleField ID="Subject" runat="server" Width="305px" ToolTip="subject" />
                <asp:TextBox ID="TextBox1" runat="server" ToolTip="textbox1"></asp:TextBox>
            </td></tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="申請人" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="300px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                    tableName="Users" Height="80px" />
            </td>
        </tr>
    </table>
    <table>
        <tr><td>
            <cc1:DSCTabControl ID="DSCTabDoc" runat="server" Height="347px" 
                Width="660px" onload="Attachment_DataBinding" PageColor="White">
                <TabPages>
                    <cc1:DSCTabPage runat="server" Title="文件屬性" Enabled="True" ImageURL="" 
                        Hidden="False" ID="DSCTabConverPage">
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="DSCTabAttachment" runat="server" Enabled="True" 
                        Hidden="False" ImageURL="" Title="文件檔案">
                        <TabBody>
                            <cc1:OpenFileUpload ID="FileUploadAtta" runat="server" onaddoutline="Atta_AddOutline" />
                            <cc1:GlassButton ID="ButtonUpload" runat="server" Height="20px" 
                                onclick="Upload_Click" Text="上傳檔案" Width="60px" />
                            <br />
                            <cc1:DataList ID="DataListAttachment" runat="server" height="355px" width="100%"
                                onbeforedeletedata="Atta_BeforeDeleteData" onbeforeopenwindow="Atta_BeforeOpenWindows"
                                />
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="DSCTabOrgUnit" runat="server" Enabled="True" Hidden="False" 
                        ImageURL="" Title="相關單位">
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="DSCTabRelationship" runat="server" Enabled="True" 
                        Hidden="False" ImageURL="" Title="參考文件">
                    </cc1:DSCTabPage>
                </TabPages>
            </cc1:DSCTabControl>
            </td></tr>
    </table>
    </div>
    </form>
    <br>
    <br>
    <br>
    <br>
</body>
</html>
