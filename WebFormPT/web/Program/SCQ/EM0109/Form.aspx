<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0109_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
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
<body>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left: 4px" border="0" width="666px" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td class="BasicFormHeadHead" width="70px">
                    <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工" Width="105%" Height="16px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" align="left">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="200px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick"
                        valueIndex="1" idIndex="0" />
                </td>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="LBDepartment" runat="server" Text="部門" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo" runat="server" Width="150px" ReadOnly="True" Style="margin-right: 0px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBDtName" runat="server" Text="職稱" Width="96%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="DtName" runat="server" Width="170px" ReadOnly="True" />
                </td>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="LBMobile" runat="server" Text="電話號碼" Width="130%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Mobile" runat="server" Width="149px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="LBReason" runat="server" Text="申請說明" Width="120%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" colspan="3">
                    <cc1:SingleField ID="Reason" runat="server" Width="98%" Height="64px" 
                        MultiLine="True" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" colspan="4" align="center" >
                    <cc1:DSCLabel ID="LBLastMonth" runat="server" Text="上月通話明細(分)" Width="19%" 
                        Height="16px" style="text-align: center">
                    </cc1:DSCLabel>
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCLabel ID="LBShortCall" runat="server" Text="本地市話" Width="98px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCLabel ID="LBLongCall" runat="server" Text="本地長途" Width="97px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCLabel ID="LBGroupCall" runat="server" Text="集團通話" Width="80px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCLabel ID="LBGlobalCall" runat="server" Text="國際漫遊" Width="83px"></cc1:DSCLabel>
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="ShortCall" runat="server" Width="97px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="LongCall" runat="server" Width="100px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="GroupCall" runat="server" Width="100px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="GlobalCall" runat="server" Width="100px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="LBApplyType" runat="server" Text="申請類型" Width="138%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" colspan="3">
                    <cc1:SingleDropDownList ID="ApplyType" runat="server" Width="100px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="LBApplyAmount" runat="server" Text="申請額度" Width="172%" Height="16px">
                    </cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="ApplyAmount" runat="server" Width="100px" ReadOnly="False" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBAgreeAmount" runat="server" Text="核准額度" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="AgreeAmount" runat="server" Width="100px" ReadOnly="False" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="額度超標說明" Width="172%" Height="16px">
                    </cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail" colspan="3">
                    <cc1:SingleField ID="bzcb" runat="server" Width="474px" 
                        ReadOnly="False" MultiLine="True" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBSDate" runat="server" Text="補助開始日" Width="119%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="SDate" runat="server" Width="100px" />
                </td>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="LBEDate" runat="server" Text="補助截止日" Width="145%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="EDate" runat="server" Width="100px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" width="60px">
                    <cc1:DSCLabel ID="LBJoinGroup" runat="server" Text="加入集團網" Width="185%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="JoinGroup" runat="server" Width="100px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBShortNumber" runat="server" Text="短號" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="ShortNumber" runat="server" Width="100px" ReadOnly="False" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail" colspan="4">
                    <table id="table10" border="1" cellpadding="0" class="MsoNormalTable" style="width: 493.5pt"
                        width="658">
                        <tr>
                            <td colspan="8">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span style="font-size: 9pt; font-family: PMingLiU; font-weight: 700; color: blue">新普重慶移動話費補助標準</span></p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <p align="right" class="MsoNormal" style="text-align: right">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">生效日期：</span><span
                                        lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">2011/08/10</span></p>
                            </td>
                        </tr>
                        <tr style="height: 18.75pt">
                            <td width="73">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU">&nbsp;</span></p>
                            </td>
                            <td width="70">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">總經理</span></p>
                            </td>
                            <td width="67">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">副總</span></p>
                            </td>
                            <td width="89">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">協理</span><span
                                        lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">/</span><span
                                            lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">處長</span></p>
                            </td>
                            <td width="98">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">經理</span><span
                                        lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">/</span><span
                                            lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">副理</span></p>
                            </td>
                            <td class="style2">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">主任</span><span
                                        lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">/</span><font
                                            color="#0000ff">副主任</font></p>
                            </td>
                            <td width="73">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span style="font-size: 9pt; font-family: 宋体; color: blue">司機</span></p>
                            </td>
                            <td width="88">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span style="font-size: 9pt; font-family: 宋体; color: blue">其他</span></p>
                            </td>
                        </tr>
                        <tr style="height: 14.25pt">
                            <td width="73">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">需對外</span></p>
                            </td>
                            <td rowspan="3" width="70">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">325</span></p>
                            </td>
                            <td rowspan="3" width="67">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">200</span></p>
                            </td>
                            <td rowspan="3" width="89">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">180</span></p>
                            </td>
                            <td width="98">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">160</span><span
                                        lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">/120</span></p>
                            </td>
                            <td class="style2">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">75</span></p>
                            </td>
                            <td width="73">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">120</span></p>
                            </td>
                            <td width="88">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">25-75</span></p>
                            </td>
                        </tr>
                        <tr style="height: 3pt">
                            <td width="73">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">需對內</span></p>
                            </td>
                            <td width="98">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">120</span><span
                                        lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">/60</span></p>
                            </td>
                            <td class="style2">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">0</span></p>
                            </td>
                            <td>
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">0</span></p>
                            </td>
                            <td width="88">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">0</span></p>
                            </td>
                        </tr>
                        <tr style="height: 3pt">
                            <td width="73">
                                <p class="style1">
                                    <span lang="ZH-TW" style="font-size: 9pt; font-family: PMingLiU; color: blue">平均值/月</span></p>
                            </td>
                            <td width="83">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">141.62</span> <span
                                        lang="EN-US" style="font-size: 11pt; font-family: PMingLiU; color: blue">/139.38</span></p>
                            </td>
                            <td class="style2">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="en-us" style="font-family: PMingLiU; color: #0000ff">131.15</span> <span
                                        lang="EN-US" style="font-size: 11pt; font-family: PMingLiU; color: blue">/116.68</span></p>
                            </td>
                            <td>
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">129.91</span></p>
                            </td>
                            <td width="88">
                                <p align="center" class="MsoNormal" style="text-align: center">
                                    <span lang="EN-US" style="font-size: 9pt; font-family: PMingLiU; color: blue">108.42</span></p>
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
