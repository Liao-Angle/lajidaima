<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EFIN01_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>財產轉移申請單</title>
    <style type="text/css">
        .style1
        {
            font-size: x-large;
        }
        .style2
        {
            font-size: large;
        }
        .style3
        {
            font-size: large;
            font-weight: bold;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
        <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1">
            <panel runat="server" id="Panel1" visible="false">
        <tr>
            <td align="center" height="30" style="width: 700px"> 
                <span class="style3">新普科技（重慶）有限公司</span>
            </td>
        </tr>
        </panel>
            <panel runat="server" id="Panel2" visible="false">
        <tr>
            <td align="center" height="30" style="width: 700px" class="style3"> 
                重慶貽百電子有限公司
            </td>
        </tr>
        </panel>
            <tr>
                <td align="center" height="30" style="width: 670px">
                    <font style="font-family: 標楷體; font-weight: 700;" class="style1">
                    <span class="style2">財產轉移單</span></font>
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1"class="BasicFormHeadBorder">
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBDepartment" runat="server" Text="轉出單位" Width="73px" ></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo" runat="server" Width="116px" ReadOnly="True" Height="24px" />
                </td>
                <td  class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="保管人" Width="74px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="205px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" 
                        OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" />
                </td>
                <td  class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBdate" runat="server" Text="申請日期" Width="77px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="SQDate" runat="server" Width="110px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="轉入單位" Width="100%" ></cc1:DSCLabel>
                </td>
                <td  class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo1" runat="server" Width="116px" ReadOnly="True" Height="24px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="保管人" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNoIn" runat="server" Width="205px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" 
                        OnSingleOpenWindowButtonClick="EmpNoIn_SingleOpenWindowButtonClick" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="預計轉入日" Width="92px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="ZRDate" runat="server" Width="110px" />
                </td>
            </tr>
             <tr>
                <td  class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBxz" runat="server" Text="財產編號" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="CCBH" runat="server" Width="116px" ReadOnly="True" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBsxrs" runat="server" Text="財產名稱" Width="100%" Height="16px"></cc1:DSCLabel>
                </td>
                <td  class="BasicFormHeadDetail">
                    <cc1:SingleField ID="CCMC" runat="server" Width="170px" ReadOnly="True" />
                </td>
                <td  class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="規格" Width="100%" Height="16px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="GGE" runat="server" Width="110px" ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="單位" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="DW" runat="server" Width="116px" ReadOnly="True" />
                </td>
                <td  class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="數量" Width="100%" Height="16px"></cc1:DSCLabel>
                </td>
                <td   class="BasicFormHeadDetail">
                    <cc1:SingleField ID="NUM" runat="server" Width="170px" ReadOnly="True" />
                </td>
                <td  class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="原因說明" Width="100%" Height="16px"></cc1:DSCLabel>
                </td>
                <td   class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Reason" runat="server" Width="110px" ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td  class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBqtbz" runat="server" Text="備註" Width="100%"></cc1:DSCLabel>
                </td>
                <td  colspan="5" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="bz" runat="server" Width="621px" ReadOnly="True" 
                        Height="50px" MultiLine="True" />
                </td>
            </tr>
            <panel runat="server" id="Panel3" visible="false">
            <tr>
                <td  class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="轉入單位承辦人" Width="89%"></cc1:DSCLabel>
                </td>
                <td colspan="5" class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="owner" runat="server" Width="210px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="ecpusers" valueIndex="1"
                        idIndex="0" />
                </td>
            </tr>
            </panel>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:OutDataList ID="OutDataList" runat="server" Height="215px" Width="700px" OnSaveRowData="OutDataList_SaveRowData"
                        NoAllDelete="True" NoModify="True" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
