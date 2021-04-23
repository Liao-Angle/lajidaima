<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH020101_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .style1
        {
            width: 171px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
    <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
    <table style="width: 700px;" border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td align="center" height="30">
                <font style="font-family: 標楷體; font-size: large;"><b>獎懲申請單</b></font>
            </td>
        </tr>
    </table>
    <div>
        <%--<table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">--%>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td colspan="1" align="left" class="BasicFormHeadHead">
                    <label style="font-size: 12px; font-weight: bold;">
                        申請人信息</label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr align="center">
                <td class="BasicFormHeadHead" width="width:50px">
                    申請人
                </td>
                <td class="style1">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="109%" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" />
                </td>
                <td class="BasicFormHeadHead">
                    申請人分機
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="mobileuser" runat="server" Width="102px" Style="margin-left: 12px"
                        ReadOnly="True" />
                </td>
                <td class="BasicFormHeadHead">
                    申請人部門
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="partNo" runat="server" Width="110px" CertificateMode="True"
                        ReadOnly="False" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td class="BasicFormHeadHead" rowspan="3" style="width:50px"; align="center">
                    獎<br />
                    懲<br />
                    內<br />
                    容<br />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="工號" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="JEmpNo" runat="server" Width="198px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" 
                        onsingleopenwindowbuttonclick="JEmpNo_SingleOpenWindowButtonClick" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="部門" Width="116px" Display="True"
                        Style="text-align: center" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="JPartNo" runat="server" Width="133px" 
                        ReadOnly="False" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="獎懲日期" Width="72px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="Date" runat="server" Width="168px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="獎懲類別" Width="75px" Height="16px"
                        Style="text-align: left" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="JNo" runat="server" Width="132px" 
                        onselectchanged="JNo_SelectChanged" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="獎懲次數" Width="102px" 
                        Height="16px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="CS" runat="server" Width="166px" Height="16px" 
                        onselectchanged="JNo_SelectChanged" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="獎懲金額(元)" Width="102px" 
                        Height="16px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Money" runat="server" Width="166px" Height="16px" 
                        ReadOnly="True" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td class="BasicFormHeadHead" style="width: 50px; " align="center">
                    獎<br />
                    懲<br />
                    原<br />
                    因</td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Reson" runat="server" MultiLine="True" Width="641px" Height="82px"
                        Style="margin-left: 35px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px" 
                    width="width:50px">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="備註" Width="46px" Height="16px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Mark" runat="server" MultiLine="True" Width="641px" Height="82px"
                        Style="margin-left: 35px" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:OutDataList ID="OutDataListJC" runat="server" Height="144px" Width="670px" 
                        onsaverowdata="OutDataListJC_SaveRowData" NoAllDelete="True" 
                        NoModify="True"></cc1:OutDataList>
                        
                         <span style="margin-top:0px; padding-top:0px"> 
                 <cc1:FileUpload ID="FileUpload1" runat="server" NoDelete="True" 
                             onaddoutline="FileUpload1_AddOutline" Width="18px" 
                     BorderStyle="None" ViewStateMode="Disabled" Height="144px" /></span>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
