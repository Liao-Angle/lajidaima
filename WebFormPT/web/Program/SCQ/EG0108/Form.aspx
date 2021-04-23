<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EG0108_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>外出申請單</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
    <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
    <div>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1">
        <panel runat="server" id="Panel1" visible="false">
        <tr>
            <td align="center" height="30" style="width: 700px">
                <font style="font-family: 標楷體; font-size: large;"><b>新普科技（重慶）有限公司</b></font>
            </td>
        </tr>
        </panel>
        <panel runat="server" id="Panel2" visible="false">
        <tr>
            <td align="center" height="30" style="width: 700px">
                <font style="font-family: 標楷體; font-size: large;"><b>重慶貽百電子有限公司</b></font>
            </td>
        </tr>
        </panel>
        <tr>
            <td align="center" height="30" style="width: 670px">
                <font style="font-family: 標楷體; font-weight: 700;" class="style1">外出申請單</font>
            </td>
        </tr>
    </table>
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
                    <cc1:SingleField ID="PartNo" runat="server" Width="155px" ReadOnly="True" Height="24px"
                        CertificateMode="True" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBxz" runat="server" Text="職務" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="DtName" runat="server" Width="155px" ReadOnly="True" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="外出類型" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="OutType" runat="server" Width="155px" 
                        ReadOnly="True" onselectchanged="OutType_SelectChanged" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBdate" runat="server" Text="外出時間起" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="OutB" runat="server" Width="155px" 
                        DateTimeMode="3" ondatetimeclick="ccEDate_DateTimeClick" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="外出時間止" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="OutE" runat="server" Width="155px" 
                        OnDateTimeClick="ccEDate_DateTimeClick" DateTimeMode="3" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBts" runat="server" Text="外出時數" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="OutTime" runat="server" Width="155px" ReadOnly="True" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBmudi" runat="server" Text="請假類別" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="LeaveType" runat="server" Width="155px" ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="外出地點" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="OutPlace" runat="server" Width="155px" ReadOnly="True" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBdlr" runat="server" Text="代簽主管" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="dlr" runat="server" Width="210px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="ecpusers" valueIndex="1"
                        idIndex="0" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="外出原因" Width="100%"></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="OutReason" runat="server" Width="540px" ReadOnly="True" Height="50px"
                        MultiLine="True" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
