<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0109_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            width: 79px;
        }
    </style>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工" Width="100%"></cc1:DSCLabel></td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" valueIndex="1" idIndex="0" />
            </td>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBDepartment" runat="server" Text="部門" Width="100%"></cc1:DSCLabel>
            </td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="PartNo" runat="server" Width="150px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBDtName" runat="server" Text="職稱" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="DtName" runat="server" Width="100px" ReadOnly="True" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBMobile" runat="server" Text="電話號碼" Width="100%"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Mobile" runat="server" Width="180px" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBReason" runat="server" Text="申請說明" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBLastMonth" runat="server" Text="上月通話明細 (分)" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <table style="margin-left:4px" border=0 width=100% cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
                <tr>
                    <td align=center class=BasicFormHeadHead width=25%>
                        <cc1:DSCLabel ID="LBShortCall" runat="server" Text="本地市話"></cc1:DSCLabel>
                    </td>
                    <td align=center class=BasicFormHeadHead width=25%>
                        <cc1:DSCLabel ID="LBLongCall" runat="server" Text="本地長途"></cc1:DSCLabel>
                    </td>
                    <td align=center class=BasicFormHeadHead width=25%>
                        <cc1:DSCLabel ID="LBGroupCall" runat="server" Text="集團通話"></cc1:DSCLabel>
                    </td>
                    <td align=center class=BasicFormHeadHead width=25%>
                        <cc1:DSCLabel ID="LBGlobalCall" runat="server" Text="國際漫遊"></cc1:DSCLabel>
                    </td>
                </tr>
                <tr>
                    <td align=center class=BasicFormHeadDetail>
                        <cc1:SingleField ID="ShortCall" runat="server" width=100px />
                    </td>
                    <td align=center class=BasicFormHeadDetail>
                        <cc1:SingleField ID="LongCall" runat="server" width=100px />
                    </td>
                    <td align=center class=BasicFormHeadDetail>
                        <cc1:SingleField ID="GroupCall" runat="server" width=100px />
                    </td>
                    <td align=center class=BasicFormHeadDetail>
                        <cc1:SingleField ID="GlobalCall" runat="server" width=100px />
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBApplyType" runat="server" Text="申請類型" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                                <cc1:SingleDropDownList ID="ApplyType" 
                                    runat="server" Width="120px" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBApplyAmount" runat="server" Text="申請額度" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="ApplyAmount" runat="server" Width="100px" 
                    ReadOnly="False" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBAgreeAmount" runat="server" Text="核准額度" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="AgreeAmount" runat="server" Width="100px" 
                    ReadOnly="False" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBSDate" runat="server" Text="補助開始日" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="SDate" runat="server" 
                                    Width="120px" />  
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBEDate" runat="server" Text="補助截止日" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="EDate" runat="server" 
                                    Width="120px" />  
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBJoinGroup" runat="server" Text="加入集團網" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDropDownList ID="JoinGroup" 
                                    runat="server" Width="80px" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBShortNumber" runat="server" Text="短號" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="ShortNumber" runat="server" Width="100px" 
                    ReadOnly="False" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadDetail colspan=4>
                
                <table id="table10" border="1" cellpadding="0" class="MsoNormalTable" 
                    style="WIDTH: 493.5pt" width="658">
                    <tr>
                        <td colspan="8">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; FONT-WEIGHT: 700; COLOR: blue">
                                新普重慶移動話費補助標準</span></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <p align="right" class="MsoNormal" style="TEXT-ALIGN: right">
                                <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">
                                生效日期：</span><span lang="EN-US" 
                                    style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">2011/08/10</span></p>
                        </td>
                    </tr>
                    <tr style="HEIGHT: 18.75pt">
                        <td width="73">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU">　</span></p>
                        </td>
                        <td width="70">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">
                                總經理</span></p>
                        </td>
                        <td width="67">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">副總</span></p>
                        </td>
                        <td width="89">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">協理</span><span 
                                    lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">/</span><span 
                                    lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">處長</span></p>
                        </td>
                        <td width="98">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">經理</span><span 
                                    lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">/</span><span 
                                    lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">副理</span></p>
                        </td>
                        <td class="style2">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">主任</span><span 
                                    lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">/</span><font 
                                    color="#0000ff">副主任</font></p>
                        </td>
                        <td width="73">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span style="FONT-SIZE: 9pt; FONT-FAMILY: 宋体; COLOR: blue">司機</span></p>
                        </td>
                        <td width="88">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span style="FONT-SIZE: 9pt; FONT-FAMILY: 宋体; COLOR: blue">其他</span></p>
                        </td>
                    </tr>
                    <tr style="HEIGHT: 14.25pt">
                        <td width="73">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">
                                需對外</span></p>
                        </td>
                        <td rowspan="3" width="70">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">325</span></p>
                        </td>
                        <td rowspan="3" width="67">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">200</span></p>
                        </td>
                        <td rowspan="3" width="89">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">180</span></p>
                        </td>
                        <td width="98">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">160</span><span 
                                    lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">/120</span></p>
                        </td>
                        <td class="style2">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">75</span></p>
                        </td>
                        <td width="73">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">120</span></p>
                        </td>
                        <td width="88">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">25-75</span></p>
                        </td>
                    </tr>
                    <tr style="HEIGHT: 3pt">
                        <td width="73">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">
                                需對內</span></p>
                        </td>
                        <td width="98">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">120</span><span 
                                    lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">/60</span></p>
                        </td>
                        <td class="style2">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">0</span></p>
                        </td>
                        <td>
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">0</span></p>
                        </td>
                        <td width="88">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">0</span></p>
                        </td>
                    </tr>

                    <tr style="HEIGHT: 3pt">
                        <td width="73">             
                                    <p class="style1">
                                        <span lang="ZH-TW" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">
                                        平均值/月</span></p>
                        </td>
                        <td width="83">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">141.62</span>
                                <span lang="EN-US" style="FONT-SIZE: 11pt; FONT-FAMILY: PMingLiU; COLOR: blue">/139.38</span></p>
                        </td>
                        <td class="style2">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="en-us" style="FONT-FAMILY: PMingLiU; COLOR: #0000ff">131.15</span>
                                <span lang="EN-US" style="FONT-SIZE: 11pt; FONT-FAMILY: PMingLiU; COLOR: blue">/116.68</span></p>
                        </td>
                        <td>
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">129.91</span></p>
                        </td>
                        <td width="88">
                            <p align="center" class="MsoNormal" style="TEXT-ALIGN: center">
                                <span lang="EN-US" style="FONT-SIZE: 9pt; FONT-FAMILY: PMingLiU; COLOR: blue">108.42</span></p>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
