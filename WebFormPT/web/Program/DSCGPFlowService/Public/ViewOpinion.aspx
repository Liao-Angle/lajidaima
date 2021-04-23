<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewOpinion.aspx.cs" Inherits="Program_DSCGPFlowService_Public_ViewOpinion" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>簽核流程</title>
</head>
<body style="background-color:white">
    <form id="form1" runat="server">
    <div>
    <table border=0 width=100% cellspacing=5 cellpadding=0>
    <tbody id='oldFlowChart'>
    <tr>
        <td id='flowChartContent'>
            <asp:Literal ID="FlowChartContent" runat="server"></asp:Literal>
        </td>
    </tr>
    </tbody>
    <tr>
        <td align=center>
            <asp:Image ID="FlowImage" runat="server" />
        </td>
    </tr>
    <tbody id='acStatus'>
    <tr>
        <td align=center style="font-size:10pt">
            <cc1:DSCLabel ID="DSCLabel1" runat="server" Width="60px"  />
            <!--Mantis0018609-->
            
             <asp:Image ID="genericactivity" runat="server" ImageUrl="~/Images/FlowStatusImages/genericactivity.gif"  />&nbsp;<cc1:DSCLabel ID="DSCLabel2" runat="server" Width="60px" Text="未傳送"  />
             <asp:Image ID="running" runat="server" ImageUrl="~/Images/FlowStatusImages/running.gif" />&nbsp;<cc1:DSCLabel ID="DSCLabel3" runat="server" Width="60px" Text="處理中" />
             <asp:Image ID="complete" runat="server" ImageUrl="~/Images/FlowStatusImages/complete.gif" />&nbsp;<cc1:DSCLabel ID="DSCLabel4" runat="server" Width="60px" Text="已完成" />
             <asp:Image ID="terminated" runat="server" ImageUrl="~/Images/FlowStatusImages/terminated.gif" />&nbsp;<cc1:DSCLabel ID="DSCLabel5" runat="server" Width="60px" Text="已終止"/>
             <asp:Image ID="suspend" runat="server" ImageUrl="~/Images/FlowStatusImages/suspend.gif" />&nbsp;<cc1:DSCLabel ID="DSCLabel6" runat="server" Width="100px" Text="擱置或未開始"/>
             <!--
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(138,138,138)">&nbsp;</span> 未傳送 
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(0,153,255)">&nbsp;</span> 處理中 
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(0,128,0)">&nbsp;</span> 已完成 
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(255,0,0)">&nbsp;</span> 已終止 
            <span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(255,255,0)">&nbsp;</span> 擱置或未開始-->
        </td>
    </tr>
    </tbody>
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
