<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWL_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>原稿資料夾</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DSCGroupBox ID="GroupSignBox" runat="server" Text="群簽" >
            <table>
            <tr>
                <td nowrap=true>
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="簽核意見" />
                </td>
                <td>
                    <cc1:SingleDropDownList ID="signResult" Width="100px" runat="server" />
                </td>
                <td>
                    <cc1:SingleField ID="signOpinion" Width="200px" runat="server" />
                </td>
                <td><cc1:glassbutton id="GroupSign" runat="server" onclick="GroupSign_Click"
            width="111px" Text="群簽" showWaitingIcon="true"></cc1:glassbutton></td>
            </tr>
            </table>
        </cc1:DSCGroupBox>
        <br />
        <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="搜尋條件" >
        <table>
            <tr>
                <td>
                    <cc1:DSCLabel id="DSCLabel2" runat="server" text="工作性質" width="95px"></cc1:dsclabel>
                </td>
                <td>
                    <cc1:singledropdownlist id="AssignType" runat="server" width="150px"></cc1:singledropdownlist>
                </td>
                <td>
                    <cc1:DSCLabel id="DSCLabel6" runat="server" text="列出此流程" width="95px"></cc1:dsclabel>
                </td>
                <td colspan=3>
                    <cc1:singledropdownlist id="ProcessIDList" runat="server" width="268px"></cc1:singledropdownlist>
                </td colspan=1>
                <td colspan=2>
                     <cc1:singledropdownlist id="ViewTimes" runat="server" width="115px"></cc1:singledropdownlist>               
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="主旨包含" Width="100%" />
                </td>
                <td>
                    <cc1:SingleField ID="Subject" runat="server" Width="100%" />
                </td>
                <td>
                    <cc1:DSCLabel ID="Dsclabel3" runat="server" Text="工作建立時間" Width="93px" />
               </td>
                <td>
                    <cc1:SingleDateTimeField ID="StartTime" runat="server" Width="133px" DateTimeMode="1" />
                </td>
                <td>
                    <cc1:DSCLabel ID="Dsclabel4" runat="server" Text="～" Width="20px" />
                </td>
                <td>
                    <cc1:SingleDateTimeField ID="EndTime" runat="server" Width="133px" DateTimeMode="1" />
                </td>
                <td>
                    <cc1:GlassButton ID="FilterButton" runat="server" Height="20px" OnClick="FilterButton_Click" Text="搜尋" Width="43px" showWaitingIcon="True" />
                </td>
                <td>    
                    <cc1:GlassButton id="ToggleButton" runat="server" onclick="ToggleButton_Click" 
                        text="保留/復原" width="70px" />
                </td>
            </tr>
        </table>
        </cc1:DSCGroupBox>
        <br />
        <cc1:datalist id="ListTable" runat="server" height="305px" width="100%" 
            OnCustomDisplayTitle="ListTable_CustomDisplayTitle" IsPanelWindow="True" 
            OnGetFlowStatusString="ListTable_GetFlowStatusString" 
            OnRefreshButtonClick="ListTable_RefreshButtonClick" NoAdd="True" 
            NoDelete="True" OnsetEnhancedRow="ListTable_setEnhancedRow"></cc1:datalist>
    
    </div>
    </form>
</body>
</html>
