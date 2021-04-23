<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0107_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=800px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead width=120px>
                <cc1:DSCLabel ID="LBSubject" runat="server" Text="需求主題"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="Subject" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBType" runat="server" Text="需求類型"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="Action" 
                    runat="server" Width="110px" onselectchanged="Action_SelectChanged" /> 
                <cc1:SingleDropDownList ID="Requirement1" 
                    runat="server" Width="110px" /> 
                            <cc1:SingleField ID="Requirement2" runat="server" Width="200px" 
                    ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBMESModules" runat="server" Text="模組類型"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <table width="100%">
                    <tr>
                        <td class=BasicFormHeadDetail>                    
                            <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server" Text="資料收集模組"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>                    
                            <cc1:DSCCheckBox ID="DSCCheckBox2" runat="server" Text="WIP資料維護模組"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox3" runat="server" Text="生產制程管理規劃設定"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>  
                            <cc1:DSCCheckBox ID="DSCCheckBox4" runat="server" Text="出貨管理模組"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox5" runat="server" Text="維修資料收集模組"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox6" runat="server" Text="重要物料收集管理模組"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox7" runat="server" Text="業務訂單管理模組"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox8" runat="server" Text="生產資料多維分析模組"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>  
                            <cc1:DSCCheckBox ID="DSCCheckBox9" runat="server" Text="警示系統控制設定模組"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBReason" runat="server" Text="需求說明"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="Reason" runat="server" Width="100%" 
                    Height="60px" MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBBenefit" runat="server" Text="效益分析"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Benefit" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
