<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCGPFlowService_Maintain_SMWM_Detail" %>

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
        <cc1:DSCGroupBox id="DSCGroupBox1" runat="server" text="流程改派" Width="100%">
        <table border=0 width=100% cellspacing=5 cellpadding=0>
        <tr>
            <td>
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="要改派的關卡" Width="85px" TextAlign="2" />
            </td>
            <td>
                <cc1:SingleDropDownList ID="WorkItemOIDs" runat="server" Width="172px" OnSelectChanged="WorkItemOIDs_SelectChanged"/>
            </td>
            <td>
                <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="要改派的對象" Width="85px" TextAlign="2" />
            </td>
            <td>
                <cc1:SingleDropDownList ID="WorkAssignmentOIDs" runat="server" Width="155px"/>
            </td>
            <td>
                <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="改派方式" Width="85px" TextAlign="2" />
            </td>
            <td>
                <cc1:SingleDropDownList ID="ReassignmentType" runat="server" Width="155px"/>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="新處理者" Width="85px" TextAlign="2" />
            </td>
            <td>
                <cc1:SingleOpenWindowField ID="acceptorId" runat="server" Width="176px" guidField="OID" keyField="id" serialNum="001" showReadOnlyField="True" tableName="Users" FixReadOnlyValueTextWidth="90px" FixValueTextWidth="55px" />
            </td>
            <td>
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="改派意見" Width="85px" TextAlign="2" />
            </td>
            <td width=100% colspan=2>
                <cc1:SingleField ID="SignOpinion" runat="server" Width="100%" />
            </td>
            <td>
                <cc1:GlassButton ID="Reassign" runat="server" Text="改派" Width="30px" OnClick="Reassign_Click" showWaitingIcon="True" />
            </td>
        </tr>
        </table>
        </cc1:DSCGroupBox>
        <cc1:DSCGroupBox id="DSCGroupBox2" runat="server" text="重新執行AfterApprove" Width="100%">
        <table border=0 width=100% cellspacing=5 cellpadding=0>
        <tr>
            <td style="width: 231px">
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="執行結果" Width="85px" TextAlign="2" />
                <cc1:SingleDropDownList ID="SignResult" runat="server" Width="135px"/>
            </td>
            <td>
                <cc1:GlassButton ID="AfterApproveButton" runat="server" Text="重新執行" Width="130px" OnClick="AfterApproveButton_Click" showWaitingIcon="True" />
            </td>
        </tr>
        </table>
        </cc1:DSCGroupBox>
        <cc1:DSCGroupBox ID="DSCGroupBox3" runat="server" Text="強制ByPass" Width="100%">
            <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="要ByPass的關卡" Width="117px" TextAlign="2" />
            <cc1:SingleDropDownList ID="ByPassActivity" runat="server" Width="100px"/><cc1:GlassButton ID="ByPassButton" runat="server" Text="ByPass" Width="130px" OnClick="ByPassButton_Click" showWaitingIcon="True" />
        </cc1:DSCGroupBox>
        <br />
    
    <table border=0 width=100% cellspacing=5 cellpadding=0>
    <tr>
        <td align=center>
            <asp:Image ID="FlowImage" runat="server" />
        </td>
    </tr>
    <tr>
        <td align=center style="font-size:10pt">
            <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="活動狀態:" Width="70px" />
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(138,138,138)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="未傳送" Width="50px" /> 
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(0,153,255)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="處理中" Width="50px" /> 
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(0,128,0)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="已完成" Width="50px" /> 
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(255,0,0)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="已終止" Width="50px" /> 
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(255,255,0)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="擱置或未開始" Width="90px" />
        </td>
    </tr>
    <tr>
        <td align=center>
            <asp:Literal ID="OpinionList" runat="server"></asp:Literal>
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
