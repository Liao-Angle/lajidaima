<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_System_Maintain_SMVO_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>M-Office欄位關聯設定輸入畫面</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" width="510px">
                <tr>
                    <td valign="top" width="120px">
                        <cc1:DSCLabel ID="SMVOAAA002L" runat="server" Text="欄位關聯設定名稱" />
                    </td>
                    <td>
                        <cc1:SingleField ID="SMVOAAA002" runat="server" Width="375px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DSCLabel ID="SMVOAAA003L" runat="server" Text="流程作業畫面關聯名稱" Width="150px" Height="18px" />
                    </td>
                    <td style="white-space:nowrap;">
                        <cc1:SingleOpenWindowField ID="SMVOAAA003"  runat="server"  showReadOnlyField="True" guidField="SMWDAAA001" keyField="SMWDAAA001" serialNum="001" tableName="SMWDAAA" Width="268px" FixReadOnlyValueTextWidth="230px" FixValueTextWidth="0px" valueIndex="1" HiddenClearButton="False" idIndex="0" OnSingleOpenWindowButtonClick="SMVOAAA003_SingleOpenWindowButtonClick" />
                        <cc1:GlassButton ID="GlassButton1" runat="server" Height="17px" Text="解析" Width="101px" OnClick="GlassButton1_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <cc1:DSCCheckBox ID="isAdd" runat="server" OnClick="isAdd_Click" Text="使用快速設定" Width="170px" />
            <cc1:DSCCheckBox ID="IsProhibit" runat="server" Text="是否禁用行動簽核功能" Width="170px" />  
            <br />
            <br />
            <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="361px" Width="662px">
                <TabPages>
                    <cc1:DSCTabPage ID="DSCTabPage15" runat="server" Enabled="True" ImageURL="" Title="快速設定" Hidden="False">
                        <TabBody>
                            <table border="0" cellspacing="3" cellpadding="0">
                                <tr>
                                    <td>
                                        <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="預設欄位" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="顯示欄位" />
                                    </td>
                                    <td style="width: 77px">
                                    </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <cc1:MultiDropDownList ID="MOFrom" runat="server" Height="270px" Width="255px" />
                                    </td>
                                    <td style="width: 51px">
                                        <cc1:GlassButton ID="MORight" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/add_small.gif" OnClick="MORight_Click"  />
                                        <cc1:GlassButton ID="MOLeft" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/remove_small.gif" OnClick="MOLeft_Click"  />
                                    </td>
                                    <td>
                                        <cc1:MultiDropDownList ID="MOTo" runat="server" Height="270px" Width="255px" />
                                    </td>
                                    <td style="width: 77px">
                                        <cc1:GlassButton ID="MOUp" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/up_small.gif" OnClick="MOUp_Click" />
                                        <cc1:GlassButton ID="MODown" runat="server" ImageUrl="~/DSCWebControlRunTime/DSCWebControlImages/down_small.gif" OnClick="MODown_Click" />
                                    </td> 
                                </tr>
                            </table>
                            &nbsp;
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage ID="DSCTabPage1" runat="server" Enabled="True" ImageURL="" Title="預覽編輯" Hidden="False">
                        <TabBody>
                            <cc1:SingleField ID="SMVOAAA004" runat="server" Height="327px" MultiLine="True" Width="653px" OnTextChanged="SMVOAAA004_TextChanged"  />
                        </TabBody>
                    </cc1:DSCTabPage>
                </TabPages>
            </cc1:DSCTabControl>
        </div>
    </form>
</body>
</html>
