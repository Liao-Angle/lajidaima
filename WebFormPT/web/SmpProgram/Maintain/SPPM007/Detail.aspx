<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_Maintain_SPPM007_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>考評記錄查詢</title>   
</head>
<body>
   
    <form id="form1" runat="server">
    <cc1:DSCTabControl ID="DSCTabAssessment" runat="server" Height="120px" Width="960px" PageColor="White">
    <TabPages>
        <cc1:DSCTabPage runat="server" Title="評核作業維護" Enabled="True" ImageURL="" Hidden="False" ID="TabAssessMaintain">
        <TabBody>
            <table style="margin-left:4px; width: 920px;" border=0 cellspacing=0 cellpadding=1 > 
                <tr><td><cc1:DSCLabel ID="LblAssessmentName" runat="server" Width="90px" 
                        Text="考評名稱" TextAlign="2" /></td>
                        <td colspan=3>
                            <cc1:SingleField ID="AssessmentName" runat="server" Width="314px" 
                                ReadOnly="True" /></td>
                       <td><cc1:DSCLabel ID="LblStatus" runat="server" Width="90px" Text="評核狀態" 
                            TextAlign="2" /></td>
                       <td><cc1:SingleDropDownList ID="Status" runat="server" Width="100px" 
                            ReadOnly="True" /></td>
                       <td>
                           <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="職稱" TextAlign="2" 
                               Width="40px" />
                    </td>
                       <td>
                           <cc1:SingleField ID="sfdtName" runat="server" Width="100px" ReadOnly="True" />
                    </td>
                </tr>
                <tr><td><cc1:DSCLabel ID="LblEmpName" runat="server" Width="90px" Text="姓名" 
                        TextAlign="2" /></td>
                    <td><cc1:SingleField ID="empName" runat="server" Width="80px" ReadOnly="True" /></td>
                    <td><cc1:DSCLabel ID="LblDeptName" runat="server" Width="80px" Text="部門" 
                            TextAlign="2" Height="16px" /></td>
                    <td><cc1:SingleField ID="deptName" runat="server" Width="273px" ReadOnly="True" /></td>
                    <td><cc1:DSCLabel ID="LblAchievementLevel" runat="server" Width="90px" Text="等級" 
                            TextAlign="2" /></td>
                    <td><cc1:SingleDropDownList ID="AchievementLevel" runat="server" Width="100px" 
                            ReadOnly="True" /></td>
                   <td>
                       <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="職等" TextAlign="2" 
                           Width="40px" />
                    </td>
                   <td>
                       <cc1:SingleField ID="sfZD" runat="server" Width="100px" ReadOnly="True" />
                    </td>
                </tr>
                <tr><td><cc1:DSCLabel ID="LblSelfComments" runat="server" Width="90px" Text="自評意見" 
                        TextAlign="2" /></td>
                    <td colspan=3>
                        <cc1:SingleField ID="SelfComments" runat="server" Width="450px" 
                            MultiLine="True" ReadOnly="True" /></td>
                    <td><cc1:DSCLabel ID="LblSelfScore" runat="server" Width="90px" Text="自評總分" 
                            TextAlign="2" Height="16px" /></td>
                    <td><cc1:SingleField ID="SelfScore" runat="server" Width="80px" ReadOnly="True" 
                            alignRight="True" /></td>
                   <td></td>
                   <td></td>
                </tr>
                <tr><td>
                    <cc1:DSCLabel ID="LblFirstComments" runat="server" Width="90px" 
                        Text="一階評核意見" TextAlign="2"  /></td>
                    <td colspan=3>
                        <cc1:SingleField ID="FirstComments" runat="server" Width="450px" 
                            MultiLine="True" ReadOnly="True"  /></td>
                    <td><cc1:DSCLabel ID="LblFirstScore" runat="server" Width="90px" Text="一階評核總分" 
                            TextAlign="2" /></td>
                    <td><cc1:SingleField ID="FirstScore" runat="server" Width="80px" ReadOnly="True" 
                            alignRight="True"  /></td>
                   <td></td>
                   <td></td>
                </tr>
                <tr><td>
                    <cc1:DSCLabel ID="LblSecondComments" runat="server" Width="90px" 
                        Text="二階評核意見" TextAlign="2" /></td>
                    <td colspan=3>
                        <cc1:SingleField ID="SecondComments" runat="server" Width="450px" 
                            MultiLine="True" ReadOnly="True"  /></td>
                    <td><cc1:DSCLabel ID="LblSecondScore" runat="server" Width="90px" Text="二階評核總分" 
                            TextAlign="2" /></td>
                    <td><cc1:SingleField ID="SecondScore" runat="server" Width="80px" ReadOnly="True" 
                            alignRight="True" /></td>
                   <td></td>
                   <td></td>
                </tr>
                <tr><td>
                    <cc1:DSCLabel ID="LblFinalComments" runat="server" Width="90px" 
                        Text="總詰評語" TextAlign="2" /></td>
                    <td colspan=3>
                        <cc1:SingleField ID="FinalComments" runat="server" Width="450px" 
                            MultiLine="True" ReadOnly="True"  /></td>
                    <td><cc1:DSCLabel ID="LblFinalScore" runat="server" Width="90px" Text="總詰總分" 
                            TextAlign="2" /></td>
                    <td><cc1:SingleField ID="FinalScore" runat="server" Width="80px" ReadOnly="True" 
                            alignRight="True" ontextchanged="FinalScore_TextChanged" /></td>
                   <td></td>
                   <td></td>
                </tr>
            </table>
        </TabBody>
        </cc1:DSCTabPage>
        <cc1:DSCTabPage ID="TabAttachment" runat="server" Enabled="True" Hidden="False" ImageURL="" Title="附件">
        <TabBody>
            <table width="780px">
                <tr><td>
                        <cc1:DataList ID="AttachmentList" runat="server" height="130px" width="780px" NoAdd="True" NoDelete="True" />
                    </td>
                </tr>
            </table>
        </TabBody>
        </cc1:DSCTabPage>
    </TabPages>
    </cc1:DSCTabControl>
    <table width="100%">
        <tr><td width="100%">
                <cc1:DataList ID="AchievementDetailList" runat="server" showExcel="True" ReadOnly="True" NoAdd="True" NoDelete="True" Width="100%" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
