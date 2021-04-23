<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH0107_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .style2
        {
            width: 135px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left: 4px; width: 680px;" border="0" cellspacing="0" cellpadding="1">
            <cc1:SingleField ID="SheetNo" runat="server" />
            <cc1:SingleField ID="Subject" runat="server" />
            <tr>
                <td align="center" height="40">
                    <font style="font-family: 標楷體; font-size: large;"><b>薪資加扣項申請單</b></font>
                </td>
            </tr>
        </table>
        <table width="680px" border="1" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="填單人" Width="63px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="Label2" runat="server" Text="申請人" Width="63px" 
                        style="text-align: left" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="User" runat="server" showReadOnlyField="True" guidField="EmpNo"
                        keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1" idIndex="0"
                        Width="190px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="Label1" runat="server" Text="部門" Width="48px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="partNouser" runat="server" Width="119px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="Label3" runat="server" Text="申請日期" Width="103px"></cc1:DSCLabel>
                </td>
                <td class="style2">
                    <cc1:SingleField ID="Cdate" runat="server" Width="121px" />
                </td>
            </tr>
            <tr id="User1S" runat="server">
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="資料" Width="103px"></cc1:DSCLabel>
                </td>
            </tr>
            <tr id="User2S" runat="server">
                <td align="right" colspan="6" class="BasicFormHeadHead">
                    <cc1:DSCTabControl ID="DSCTabControl1" runat="server" Height="70px" Width="100%">
                        <TabPages>
                            <cc1:DSCTabPage runat="server" Title="个人" Enabled="True" ImageURL="" Hidden="False"
                                ID="DSCTabPage1">
                                <TabBody>
                                    <table class="BasicFormHeadBorder" width="100%" cellspacing="0" cellpadding="1" border="0">
                                        <tbody>
                                            <tr>
                                                <td align="center" class="BasicFormHeadDetail">
                                                    員工&nbsp;<cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="200px" idIndex="0"
                                                        valueIndex="1" tableName="hrusers" serialNum="001" keyField="EmpNo" guidField="EmpNo"
                                                        showReadOnlyField="True" wfdid="w1"></cc1:SingleOpenWindowField>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </TabBody>
                            </cc1:DSCTabPage>
                            <cc1:DSCTabPage runat="server" Title="部门" Enabled="True" ImageURL="" Hidden="False"
                                ID="DSCTabPage2">
                                <TabBody>
                                    <div style="text-align: center">
                                        <cc1:SingleDropDownList ID="SingleDropDownList1" runat="server" />
                                    </div>
                                </TabBody>
                            </cc1:DSCTabPage>
                            <cc1:DSCTabPage runat="server" Title="线别" Enabled="True" ImageURL="" Hidden="False"
                                ID="DSCTabPage3">
                                <TabBody>
                                    <div style="text-align: center">
                                        <cc1:SingleDropDownList ID="SingleDropDownList2" runat="server" />
                                    </div>
                                </TabBody>
                            </cc1:DSCTabPage>
                        </TabPages>
                    </cc1:DSCTabControl>
                </td>
            </tr>
            <tr id="User6S" runat="server">
                <td align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="日 期" Width="103px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:SingleDateTimeField ID="WorkDate" runat="server" Width="120px" DateTimeMode="5" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="選擇金額" Width="103px"></cc1:DSCLabel></nobr>
                </td>
                <td colspan="3" align="right" class="BasicFormHeadHead">
                    <cc1:SingleDropDownList ID="sMoney" runat="server" Width="110px" OnSelectChanged="sMoney_SelectChanged" />
                </td>
            </tr>
            <tr id="User3S" runat="server">
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="科目類型" Width="103px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:SingleDropDownList ID="Account" runat="server" Width="110px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="金額" Width="103px"></cc1:DSCLabel>
                </td>
                <td colspan="3" align="right" class="BasicFormHeadHead">
                    <cc1:SingleField ID="Money" runat="server" Width="110px" />
                </td>
            </tr>
            <tr id="User4S" runat="server">
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="申請原因" Width="103px" 
                        Height="16px"></cc1:DSCLabel>
                </td>
                <td colspan="5">
                    <cc1:SingleField ID="Note" runat="server" Height="92px" Width="680px" MultiLine="True" />
                </td>
            </tr>
            <tr id="User5S" runat="server">
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="備注說明" Width="103px"></cc1:DSCLabel>
                </td>
                <td colspan="5">
                    <cc1:SingleField ID="Remark" runat="server" Height="92px" Width="680px" MultiLine="True" />
                </td>
            </tr>
            <tr>
                <td colspan="1" align="left" class="BasicFormHeadHead">
                        <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="注意" Width="103px" 
                            ForeColor="Blue"></cc1:DSCLabel>
                </td>
                <td colspan="5" class="BasicFormHeadDetail" >
                    <cc1:DSCLabel ID="DSCLabel10" runat="server" 
                        Text="高溫補貼<=465;崗位津貼,特殊/重點<=140 多能工/儲備<=200;崗位補貼<=200" Width="680px" 
                        ForeColor="#0000CC"></cc1:DSCLabel>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <cc1:OutDataList ID="LeaveDateList" runat="server" Height="250px" ViewStateMode="Disabled"
                        Width="96%" NoModify="True" IsExcelWithMultiType="True" showTotalRowCount="True"
                        NoAllDelete="True" OnSaveRowData="LeaveDateList_SaveRowData" 
                        showExcel="True"></cc1:OutDataList>
                    <span style="margin-top: 0px; padding-top: 0px">
                        <cc1:FileUpload ID="FileUpload1" runat="server" NoDelete="True" OnAddOutline="FileUpload1_AddOutline"
                            Width="18px" BorderStyle="None" ViewStateMode="Disabled" Height="234px" />
                    </span>
                </td>
            </tr>
            <tr runat="server" id="showzg" visible="false">
                <td colspan="2" class="BasicFormHeadHead" style="height: 17px">
                    請選擇簽核主管：
                </td>
                <td colspan="4" class="BasicFormHeadHead">
                    <cc1:SingleDropDownList ID="sqzszg" runat="server" Width="111px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
