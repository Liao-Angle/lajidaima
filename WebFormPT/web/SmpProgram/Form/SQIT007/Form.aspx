<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_SQIT007_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .BasicFormHeadBorder
        {
            width: 670px;
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
                <font style="font-family: 標楷體; font-size: large;"><b>維修設備領用</b></font>
            </td>
        </tr>
    </table>
      <cc1:DSCTabControl ID="DSCTabAssessment" runat="server" Height="120px" Width="700px"
        PageColor="White">
        <TabPages>
            <cc1:DSCTabPage runat="server" Title="維修信息" Enabled="True" ImageURL="" Hidden="False"
                ID="TabAssessMaintain">
                <TabBody>
    <div>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="單號" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqNO" runat="server" Width="190px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="設備編碼" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="MID" runat="server" Width="190" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="填單人/主管" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Creator" runat="server" Width="190px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="填單人部門" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo" runat="server" Width="190" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="申請日期" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqDate" runat="server" Width="190px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="期望完成日期" Width="100px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqPlanDate" runat="server" Width="190" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="申請人" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqOwner" runat="server" Width="190px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="BU" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="BU" runat="server" Width="190" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBEmployeeID" runat="server" Text="維修項目" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqType" runat="server" Width="190px" />
                </td>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="維修人" Width="99px"></cc1:DSCLabel>
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="wxEmp" runat="server" Width="190px" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="維修原因" Width="100px"></cc1:DSCLabel>
                </td>
                <td colspan="3" width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqConent" runat="server" Width="555px" Height="58px" MultiLine="True"
                        RowsOfMultiLine="0" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="維修說明" Width="100px"></cc1:DSCLabel>
                </td>
                <td colspan="3" width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="wxsm" runat="server" Width="555px" Height="58px" MultiLine="True"
                        RowsOfMultiLine="0" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="left" class="BasicFormHeadHead">
                    <label style="font-weight: bold;" class="style1">
                        配件清單</label>
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="預估費用" Width="99px"></cc1:DSCLabel>
                </td>
                <td colspan="3" width="280px" align="left" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="charge" runat="server" Width="555px" />
                </td>
            </tr>
            <tr>
             <td class=BasicFormHeadDetail colspan=4  valign=top>
                 &nbsp;<cc1:OutDataList 
                     id="wxList" runat="server" Height="153px" 
                     OnSaveRowData="wxList_SaveRowData" ViewStateMode="Disabled" Width="651px" 
                     showExcel="True" NoModify="True" IsExcelWithMultiType="True" 
                     showTotalRowCount="True"></cc1:OutDataList>
            </td>
        </tr> 
        </table>
    </div>
    </TabBody>
            </cc1:DSCTabPage>
             <cc1:DSCTabPage ID="TabAttachment" runat="server" Enabled="True" Hidden="False" ImageURL=""
                Title="附件">
                <TabBody>
                    <table width="700px">
                        <tr>
                            <td>
                                <cc1:DataList ID="AttachmentList" runat="server" Height="300px" Width="663px"
                                    NoAdd="True" IsShowCheckBox="False" NoDelete="True" />
                            </td>
                        </tr>
                    </table>
                </TabBody>
            </cc1:DSCTabPage>
        </TabPages>
    </cc1:DSCTabControl>
    </form>
</body>
</html>
