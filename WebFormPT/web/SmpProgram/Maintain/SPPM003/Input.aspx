<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPPM003_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>考核計畫維護</title>

</head>
<body>
    <table width="100%">
        <tr><td>
                <cc1:GlassButton ID="GbSendAssessmentPlan" runat="server" Height="16px" 
                        Text="送出考核計劃" Width="100px" ConfirmText="考評計畫將自動儲存且不能再異動資料, 請確認?" 
                    onclick="GbSendAssessmentPlan_Click" /></td>
            <td><cc1:GlassButton ID="GbReSendAssessment" runat="server" Height="16px" 
                    Text="發送評核通知" Width="100px" Display="False" 
                    onclick="GbReSendAssessment_Click" /></td>
        </tr>
    </table>
    <form id="form1" runat="server">
    <fieldset style="width: 780px">
        <legend>考核計畫維護</legend>
        <table style="margin-left:4px; width: 780px;" border=0 cellspacing=0 cellpadding=1 >  
            <tr><td><cc1:DSCLabel ID="LblCompanyCode" runat="server" Width="70px" Text="公司別" 
                    TextAlign="2" /></td>
                <td><cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="131px" /></td>
                <td><cc1:DSCLabel ID="LblAssessYear" runat="server" Width="70px" Text="考評年度" 
                        TextAlign="2" /></td>
                <td><cc1:SingleField ID="AssessYear" runat="server" Width="100px"/></td>
                <td><cc1:DSCLabel ID="LblStatus" runat="server" Width="70px" Text="狀態" 
                        TextAlign="2" /></td>
                <td><cc1:SingleDropDownList ID="Status" runat="server" Width="100px" ReadOnly="True" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblAssessDate" runat="server" Width="70px" Text="考評期間" 
                    TextAlign="2" /></td>
                <td width="220px"><cc1:SingleDateTimeField ID="AssessStartDate" runat="server" Width="100px"/>~
                    <cc1:SingleDateTimeField ID="AssessEndDate" runat="server" Width="100px"/></td>
                <td><cc1:DSCLabel ID="LblPlanEndDate" runat="server" Width="70px" Text="截止日" 
                        TextAlign="2" /></td>
                <td><cc1:SingleDateTimeField ID="PlanEndDate" runat="server" Width="100px" /></td>
                <td><cc1:DSCLabel ID="LblStartDate" runat="server" Width="70px" Text="開始日" 
                        TextAlign="2" /></td>
                <td><cc1:SingleField ID="StartDate" runat="server" Width="125px" ReadOnly="True" /></td>
            </tr>
            <tr><td>
                <cc1:DSCLabel ID="LblAssessmentName" runat="server" Width="70px" 
                    Text="考評名稱" TextAlign="2" /></td>
                <td colspan=3><cc1:SingleField ID="AssessmentName" runat="server" Width="300px" /></td>
                <td><cc1:DSCLabel ID="LblCloseDate" runat="server" Width="70px" Text="結案日" 
                        TextAlign="2" /></td>
                <td><cc1:SingleField ID="CloseDate" runat="server" Width="125px" ReadOnly="True" /></td>
            </tr>
            <tr><td>
                <cc1:DSCLabel ID="LblEvaluationGUID" runat="server" Width="70px" Text="考評表" 
                    TextAlign="2" /></td>
                <td colspan=3><cc1:SingleDropDownList ID="EvaluationGUID" runat="server" Width="300px" /></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr><td>
                <cc1:DSCLabel ID="LblRemark" runat="server" Width="70px" Text="備註" 
                    TextAlign="2" Height="16px" /></td>
                <td colspan=3><cc1:SingleField ID="Remark" runat="server" Width="300px" /></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <table>
        <tr><td>
            <cc1:DSCTabControl ID="DSCTabDoc" runat="server" Height="300px" Width="780px" 
                PageColor="White">
                <TabPages>
                    <cc1:DSCTabPage runat="server" Title="評核設定" Enabled="True" ImageURL="" Hidden="False" ID="TabAssessmentConfig">
                        <TabBody>
                            <table width="100%">
                                <tr><td>
                                    <cc1:DSCLabel ID="LblSelfEvaluation" runat="server" Text="員工自評" TextAlign="2" 
                                        Width="130px" />
                                    </td>
                                    <td>
                                        <cc1:SingleDropDownList ID="SelfEvaluation" runat="server" Width="60px" />
                                    </td>
                                    <td><cc1:DSCLabel ID="LblSelfEvaluationDays" runat="server" Text="自評天數" TextAlign="2" Width="130px" /></td>
                                    <td><cc1:SingleField ID="SelfEvaluationDays" runat="server" Width="60px"/></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr><td>
                                        <cc1:DSCLabel ID="LblAssessmentCategory" runat="server" Text="評核人員類別" TextAlign="2" Width="130px" />
                                    </td>
                                    <td><cc1:SingleDropDownList ID="AssessmentCategory" runat="server" Width="120px" />
                                        <cc1:SingleDropDownList ID="AssessmentLevel" runat="server" Width="60px" />
                                        <cc1:DSCLabel ID="LblAssessmentLevel" runat="server" Text="階" TextAlign="1" 
                                            Width="24px" /></td>
                                    <td><cc1:DSCLabel ID="LblFirstAssessmentDays" runat="server" Text="一階評核天數" TextAlign="2" Width="130px" /></td>
                                    <td><cc1:SingleField ID="FirstAssessmentDays" runat="server" Width="60px"/></td>
                                    <td><cc1:DSCLabel ID="LblSecondAssessmentDays" runat="server" Text="二階評核天數" TextAlign="2" Width="130px" /></td>
                                    <td><cc1:SingleField ID="SecondAssessmentDays" runat="server" Width="60px"/></td>
                                </tr>
                                <tr><td><cc1:DSCLabel ID="LblAchievementDistWay" runat="server" Text="成積分配主管" TextAlign="2" Width="130px" /></td>
                                    <td><cc1:SingleDropDownList ID="AchievementDistWay" runat="server" Width="120px" /></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage runat="server" Title="考評對象" Enabled="True" ImageURL="" Hidden="False" ID="TabAssessmentMember">
                        <TabBody>
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <cc1:OpenWin ID="OpenWinAssessmentMembers" runat="server" Height="20px" 
                                            onopenwindowbuttonclick="OpenWinAssessmentMembers_OpenWindowButtonClick" 
                                            Width="20px" />
                                    </td>
                                    <td>
                                        <cc1:DSCLabel ID="LblAssessmentMemberGroupGUID" runat="server" Text="部門名稱" 
                                            TextAlign="2" Width="71px" />
                                    </td>
                                    <td>
                                        <cc1:MultiDropDownList ID="OrgUnitOID" runat="server" Height="90px" 
                                            Width="295px" />
                                    </td>
                                    <td valign="top" width="100px">
                                        <cc1:GlassButton ID="GbSelectOrgUnit" runat="server" Height="16px" 
                                            ImageUrl="~/Images/Groups.gif" onclick="GbSelectOrgUnit_Click" Text=" + " 
                                            Width="60px" onbeforeclicks="GbSelectOrgUnit_BeforeClicks" />
                                        <br />
                                        <cc1:GlassButton ID="GbDeleteOrgUnit" runat="server" Height="16px" 
                                            ImageUrl="~/Images/Groups.gif" onclick="GbDeleteOrgUnit_Click" Text=" x " 
                                            Width="60px" />
                                        <br />
                                        <br />
                                    </td>
                                        <td valign="top" width="150px">
                                            <cc1:GlassButton ID="GbAssessmentMemberSearch" runat="server" Height="16px" 
                                                onclick="GbAssessmentMemberSearch_Click" Text="查詢人員" Width="65px" />
                                            <br />
                                            <cc1:GlassButton ID="GbAssessmentMemberRefresh" runat="server" Height="16px" 
                                                onclick="GbAssessmentMemberRefresh_Click" Text="刷新/顯示" Width="65px" />
                                            <br />
                                            <cc1:GlassButton ID="GbAssessmentMemberImport" runat="server" Height="16px" 
                                                onclick="GbAssessmentMemberImport_Click" Text="結果匯入" Width="65px" />
                                            
                                        </td>
                                        <td><cc1:DSCRadioButton ID="filterNewEmployees" runat="server" Text="濾除新進員工" 
                                                Width="200px" Checked="True" />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan=6>
                                            <cc1:DataList ID="AssessmentMembersDraftList" runat="server" height="290px" 
                                                showExcel="True" width="760px" 
                                                showTotalRowCount="True" />
                                        </td>
                                    </tr>
                            </table>
                            
                            <table width="100%">
                                <tr><td><font size="2"><b>匯入結果清單</b></font></td></tr>
                                <tr><td>
                                        <cc1:DataList ID="AssessmentMembersList" runat="server" height="290px" 
                                            width="760px" NoAdd="True" NoModify="True" showExcel="True" 
                                            showTotalRowCount="True" />
                                    </td>
                                </tr>
                            </table>
                        </TabBody>
                    
                    </cc1:DSCTabPage>
                    <cc1:DSCTabPage runat="server" Title="考核人" Enabled="True" ImageURL="" Hidden="False" ID="TabAssessmentManager">
                        <TabBody>
                            <table width="100%">
                                <tr><td>
                                        <cc1:GlassButton ID="GbGenerateAssessmentManager" runat="server" Height="16px" 
                                                Text="依評核設定自動產生考核人" Width="200px" 
                                            onclick="GbGenerateAssessmentManager_Click" /></td>
                                </tr>
                                <tr><td>
                                        <cc1:DataList ID="AssessmentManagerList" runat="server" height="290px" 
                                            width="760px" NoAdd="True" NoModify="True" showExcel="True" 
                                            showTotalRowCount="True" />
                                    </td>
                                </tr>
                            </table>
                        </TabBody>
                    </cc1:DSCTabPage>
                </TabPages>
            </cc1:DSCTabControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
