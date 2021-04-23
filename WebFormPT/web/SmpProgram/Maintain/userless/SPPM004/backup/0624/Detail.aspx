<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_Maintain_SPPM004_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>明細</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td><cc1:DSCLabel ID="LblItemName" runat="server" Width="120px" Text="項目" 
                TextAlign="2" /></td>
            <td colspan=3><cc1:SingleField ID="ItemName" runat="server" Width="600px" 
                    ReadOnly="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblContent" runat="server" Width="120px" Text="考評內容" 
                TextAlign="2" /></td>
            <td colspan=3>
                <cc1:SingleField ID="Content" runat="server" Width="600px" MultiLine="True" 
                    Height="60px" ReadOnly="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblFractionExp" runat="server" Width="120px" Text="分數說明" 
                TextAlign="2" /></td>
            <td><cc1:SingleField ID="FractionExp" runat="server" Width="200" ReadOnly="True" /></td>
            <td><cc1:DSCLabel ID="LblMaxFraction" runat="server" Width="120px" Text="分數範圍" 
                    TextAlign="2" /></td>
            <td><cc1:SingleField ID="MinFraction" runat="server" Width="50px" ReadOnly="True" />~
                <cc1:SingleField ID="MaxFraction" runat="server" Width="50px" ReadOnly="True" /></td>
        </tr>
        <tr><td><cc1:DSCLabel ID="LblSelfScore" runat="server" Width="120px" Text="自評分數" 
                TextAlign="2" /></td>
            <td><cc1:SingleField ID="SelfScore" runat="server" Width="100px" /></td>
            <td><cc1:DSCLabel ID="LblSelfComments" runat="server" Width="120px" Text="自評意見" 
                    TextAlign="2" /></td>
            <td><cc1:SingleField ID="SelfComments" runat="server" Width="270px" 
                    MultiLine="True" Height="40px" /></td>   
        </tr>
        <tr><td><cc1:DSCLabel ID="LblFirstScore" runat="server" Width="120px" Text="一階主管評核分數" Display="False" TextAlign="2" /></td>
            <td><cc1:SingleField ID="FirstScore" runat="server" Width="100px" Display="False" /></td>
            <td><cc1:DSCLabel ID="LblFirstComments" runat="server" Width="120px" Text="一階主管評核意見" Display="False" TextAlign="2" /></td>
            <td><cc1:SingleField ID="FirstComments" runat="server" Width="270px" MultiLine="True" Height="40px" Display="False" /></td>   
        </tr>
        <tr><td><cc1:DSCLabel ID="LblSecondScore" runat="server" Width="120px" Text="二階主管評核分數" Display="False" TextAlign="2" /></td>
            <td><cc1:SingleField ID="SecondScore" runat="server" Width="100px" Display="False" /></td>
            <td><cc1:DSCLabel ID="LblSecondComments" runat="server" Width="120px" Text="二階主管評核意見" Display="False" TextAlign="2" /></td>
            <td><cc1:SingleField ID="SecondComments" runat="server" Width="270px" MultiLine="True" Height="40px" Display="False" /></td>   
        </tr>
    </table>
    </form>
</body>
</html>
