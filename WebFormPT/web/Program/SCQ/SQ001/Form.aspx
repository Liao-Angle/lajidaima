<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_SQ001_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>長途出差申請單</title>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
    <cc1:SingleField ID="Subject" runat="server" Display="False" />
     <table style="width: 700px;" border="0" cellspacing="0" cellpadding="1">
        <tr valign="middle">
            <td align="center" height="40">
                <font style="font-family: 標楷體; font-size: large;"><b>長途出差申請單</b></font>
            </td>
        </tr>
    </table>
    <div>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1"
            class="BasicFormHeadBorder">
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="申請人" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="210px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" />
                </td>
                <td width="100px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBDepartment" runat="server" Text="部門" Width="100%" Style="font-size: x-small"
                        CertificateMode="True"></cc1:DSCLabel>
                </td>
                <td width="164px" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Department" runat="server" Width="155px" ReadOnly="True" Height="24px"
                        CertificateMode="True" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBxz" runat="server" Text="出差性質" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="CCXZ" runat="server" Width="155px" ReadOnly="False" />
                </td>
                <td width="100px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBsxrs" runat="server" Text="隨行人員" Width="100%" Height="16px">
                    </cc1:DSCLabel>
                </td>
                <td width="164px" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="sxrs" runat="server" Width="155px" ReadOnly="False" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBdate" runat="server" Text="出差時間起" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="ccSDate" runat="server" Width="155px" 
                        ondatetimeclick="ccEDate_DateTimeClick" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="出差時間止" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="ccEDate" runat="server" Width="155px" OnDateTimeClick="ccEDate_DateTimeClick" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBts" runat="server" Text="天數" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="ts" runat="server" Width="155px" ReadOnly="True" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBmudi" runat="server" Text="出差目的" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="mudi" runat="server" Width="155px" 
                        ReadOnly="False" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBdlr" runat="server" Text="代理人" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="dlr" runat="server" Width="210px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" OnSingleOpenWindowButtonClick="dlr_SingleOpenWindowButtonClick" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBdanwei" runat="server" Text="單位" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="danwei" runat="server" Width="155px" ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBqtbz" runat="server" Text="備註" Width="100%"></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="qtbz" runat="server" Width="540px" ReadOnly="False" Height="50px"
                        MultiLine="True" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="2"
            class="BasicFormHeadBorder">
        </table>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="2"
            class="BasicFormHeadBorder">
            <tr>
                <td colspan="10" width="40px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LByugu" runat="server" Text="差旅費預估:" Width="375%"></cc1:DSCLabel>
                </td>
            </tr>
            <tr>
                <td width="40px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBjipaio" runat="server" Text="機票" Width="100%"></cc1:DSCLabel>
                </td>
                <td width="60px" align="right" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="jipiao" runat="server" Width="80px" ReadOnly="False" 
                        OnTextChanged="qita_TextChanged" />
                </td>
                <td width="40px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBjiaotong" runat="server" Text="交通" Width="100%"></cc1:DSCLabel>
                </td>
                <td width="60px" align="right" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="jiaotong" runat="server" Width="80px" ReadOnly="False" 
                        OnTextChanged="qita_TextChanged" />
                </td>
                <td width="40px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBshanfei" runat="server" Text="膳費" Width="100%"></cc1:DSCLabel>
                </td>
                <td width="40px" align="right" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="shanfei" runat="server" Width="80px" ReadOnly="False" 
                        OnTextChanged="qita_TextChanged" />
                </td>
                <td width="40px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBqita" runat="server" Text="其他" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="qita" runat="server" Width="80px" ReadOnly="False" 
                        OnTextChanged="qita_TextChanged" />
                </td>
                <td width="40px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBheji" runat="server" Text="合計" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="heji" runat="server" Width="80px" ReadOnly="True" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1"
            class="BasicFormHeadBorder">
            <tr>
                <td colspan="4" width="75px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LByzjine" runat="server" Text="預支金額:" Width="157%"></cc1:DSCLabel>
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBbibie" runat="server" Text="大寫(幣別)" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="bibie" runat="server" Width="200px" ReadOnly="False" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBguishu" runat="server" Text="費用歸屬" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="guishu" runat="server" Width="150px" ReadOnly="False" />
                </td>
            </tr>
            <tr>
                <td width="100px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBqiqidian" runat="server" Text="預訂起點" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="qiqidian" runat="server" Width="120px" />
                </td>
                <td width="100px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="預訂止點" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="zhidian" runat="server" Width="120px" />
                </td>
            </tr>
            <tr>
                <td width="60px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBfromdate" runat="server" Text="出發日" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="fromdate" runat="server" Width="120px" />
                </td>
                <td width="60px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBtodate" runat="server" Text="到達日" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="todate" runat="server" Width="120px" />
                </td>
            </tr>
            <tr>
                <td width="50px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBjtong" runat="server" Text="交通" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="jtong" runat="server" Width="120px" ReadOnly="False" />
                </td>
                <td width="50px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBzhusu" runat="server" Text="住宿" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="zhusu" runat="server" Width="120px" ReadOnly="False" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:OutDataList ID="OutDataList" runat="server" Height="215px" Width="680px" OnSaveRowData="OutDataList_SaveRowData"
                        NoAllDelete="True" NoModify="True" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
