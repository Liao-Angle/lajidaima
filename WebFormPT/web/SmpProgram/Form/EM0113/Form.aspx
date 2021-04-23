<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0113_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>筆記本電腦硬領用單</title>
    <style type="text/css">
        .BasicFormHeadBorder
        {
            width: 700px;
        }
        .style5
        {
            width: 197px;
        }
        .style7
        {
            width: 225px;
        }
        .style13
        {
            width: 130px;
        }
        .style14
        {
            width: 278px;
        }
        .style15
        {
            font-size: x-small;
        }
        .style23
        {
            color: #0066FF;
        }
        .style24
        {
            text-align: center;
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
                <font style="font-family: 標楷體; font-size: large;"><b>筆記本電腦硬體設備領用單</b></font>
            </td>
        </tr>
    </table>
    <div>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td colspan="4" align="left" class="BasicFormHeadHead">
                    <label style="font-size: 12px; font-weight: bold;">
                        申請人信息</label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td class="style13">
                    申請人
                </td>
                <td>
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="226px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" />
                </td>
                <td class="style7">
                    申請人分機
                </td>
                <td>
                    <cc1:SingleField ID="mobileuser" runat="server" Width="102px" Style="margin-left: 12px" />
                </td>
                <td class="style5">
                    申請人部門
                </td>
                <td>
                    <cc1:SingleField ID="partNouser" runat="server" Width="119px" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td colspan="4" align="left" class="BasicFormHeadHead">
                    <label style="font-size: 12px; font-weight: bold;">
                        保管人資訊</label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td class="style14">
                    工號
                </td>
                <td>
                    <cc1:SingleField ID="Emp" runat="server" Width="59px" Style="margin-left: 12px" OnTextChanged="Emp_TextChanged" />
                </td>
                <td class="style5">
                    姓名
                </td>
                <td>
                    <cc1:SingleField ID="EmpName" runat="server" Width="82px" OnTextChanged="Emp_TextChanged" />
                </td>
                <td class="style5">
                    部門
                </td>
                <td>
                    <cc1:SingleField ID="PartNo" runat="server" Width="119px" OnTextChanged="Emp_TextChanged" />
                </td>
                <td class="style5">
                    職務
                </td>
                <td>
                    <cc1:SingleField ID="DtName" runat="server" Width="74px" OnTextChanged="Emp_TextChanged" />
                </td>
                <td class="style5">
                    手機
                </td>
                <td>
                    <cc1:SingleField ID="Mobile" runat="server" Width="119px" OnTextChanged="Emp_TextChanged" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label style="font-weight: bold;" width="700px">
                        <span class="style15">申請事宜確認項</span></label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label width="700px">
                        1、筆記本電腦使用性質</label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server" />
                    辦公
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox2" runat="server" />
                    研發
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox3" runat="server" />
                    設備
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox4" runat="server" />
                    實驗
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label width="700px">
                        2、筆記本電腦使用區域</label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" style="width: 700px;">
            <tr>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox5" runat="server" />
                    SCQ內部
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox6" runat="server" />
                    公司外部
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox7" runat="server" />
                    其他
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" style="width: 700px;">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label width="700px">
                        3、工作性質</label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" style="width: 700px;">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label width="700px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3.1、開會說明</label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" style="width: 700px;">
            <tr>
                <td width="100px" class="BasicFormHeadDetail">
                    頻率/每週
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox8" runat="server" />
                    1-2次
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox9" runat="server" />
                    3次以上
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" style="width: 700px;">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label width="700px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3.2、圖面設計</label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" style="width: 700px;">
            <tr>
                <td width="100px" class="BasicFormHeadDetail">
                    頻率/每週
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox10" runat="server" />
                    1-2次
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox11" runat="server" />
                    3次以上
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" style="width: 700px;">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label width="700px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3.3、是否與客戶面對面開會</label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" style="width: 700px;">
            <tr>
                <td width="100px" class="BasicFormHeadDetail">
                    頻率/每週
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox12" runat="server" />
                    1-2次
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox13" runat="server" />
                    3次以上
                </td>
                <td width="100px" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox14" runat="server" />
                    每周7次
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label style="font-weight: bold;" width="700px">
                        <span class="style15">MIS審核建議規格</span></label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td width="50px" class="BasicFormHeadHead  style24">
                    品牌
                </td>
                <td width="100px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="size" runat="server" Text="規格" Width="469px" Style="margin-left: 0px;
                        text-align: center;" Height="16px" TextAlign="1"></cc1:DSCLabel>
                </td>
                <td width="85px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="price" runat="server" Text="參考價格" Width="83px" Height="16px" Style="margin-left: 0px;
                        text-align: center;" TextAlign="1"></cc1:DSCLabel>
                </td>
                <td width="65px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="count" runat="server" Text="數量" Style="margin-left: 0px; text-align: center;"
                        Width="65px" TextAlign="1"></cc1:DSCLabel>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" style="width: 700px;">
            <tr>
                <td width="50px" class="BasicFormHeadHead">
                        <span style="font-family:'Segoe UI';direction:ltr;word-wrap:break-word;color:#000000;font-size:10pt">
                        聯想</span></td>
                <td class="BasicFormHeadDetail" width="469px">
                    <div id="imcontent">
                        <span style="font-family:'Segoe UI';direction:ltr;word-wrap:break-word;color:#000000;font-size:10pt">
                        &nbsp;Lenovo 2019款 小新Air 13英特爾酷睿 i5 13.3英吋超輕薄筆記本 輕奢灰 
                        </span>
                    </div>
                </td>
                <td width="85px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="4799" Width="85px" Height="16px"
                        TextAlign="1" />
                </td>
                <td width="65px" align="center" class="BasicFormHeadHead">
                    <cc1:SingleField ID="PM1" runat="server" Width="40px" Style="text-align: justify" />
                </td>
            </tr>
            <%--<tr>
                <td width="50px" class="BasicFormHeadHead">
                </td>
                <td class="BasicFormHeadDetail" width="469px">
                    &nbsp;</td>
                <td width="85px" align="center" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Width="85px" Height="16px"
                        TextAlign="1" />
                </td>
                <td width="65px" align="center" class="BasicFormHeadHead">
                    <cc1:SingleField ID="PM2" runat="server" Width="40px" />
                </td>
            </tr>
            <tr>
                <td width="50px" class="BasicFormHeadHead">
                </td>
                <td class="BasicFormHeadDetail" width="469px">
                    &nbsp;</td>
                <td width="85px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Width="85px" Height="16px"
                        TextAlign="1" />
                </td>
                <td width="65px" align="center" class="BasicFormHeadHead">
                    <cc1:SingleField ID="PM3" runat="server" Width="40px" />
                </td>
            </tr>
            <tr>
                <td width="50px" class="BasicFormHeadHead">
                </td>
                <td class="BasicFormHeadDetail" width="469px">
                    &nbsp;</td>
                <td width="85px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Width="85px" Height="16px"
                        TextAlign="1" />
                </td>
                <td width="65px" align="center" class="BasicFormHeadHead">
                    <cc1:SingleField ID="PM4" runat="server" Width="40px" />
                </td>
            </tr>--%>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td valign="top" align="left" class="BasicFormHeadHead" width="50px">
                    <cc1:DSCLabel ID="LBReason" runat="server" Text="指定型號" Height="60px" Width="22px"
                        TextAlign="1"></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="zdxh" runat="server" Width="631px" Height="64px" MultiLine="True" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label width="700px">
                        <span class="style15">筆記本電腦申請原因說明</span></label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr>
                <td colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="reason" runat="server" Width="99%" Height="64px" MultiLine="True"
                        Style="margin-left: 0px" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td colspan="6" align="left" class="BasicFormHeadHead">
                    <label width="700px">
                        <span class="style15">注：MIS筆記本管理辦法說明</span></label>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <tr>
                <td class="BasicFormHeadDetail style23" width="700px">
                    1、處級單位以上主管始得申請筆記型電腦。此外，其餘辦公用途之筆記型電腦之需求，非經常性使用者請於各部門內部先行調配使用，經常使用者 (長期固定每週須攜出使用二次以上者)，須由處級單位以上主管核准始得申請。
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail style23" width="700px">
                    2、非一般辦公用途之筆記型電腦申請，如: 研發、實驗、與工程需求，需於”電腦軟硬體設備申請表”上註明使用之目的。
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail style23" width="700px">
                    3、筆記型電腦將定期由資訊部門配合財務部門盤點，若上述 1 與2使用之目的消失後，將由資訊部門收回統一調配。
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
            <tr runat="server" id="showzg" visible="false">
                <td class="BasicFormHeadHead" colspan="3" style="height: 17px">
                    <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="請選擇簽核主管" Width="150px" Height="20px">
                    </cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadHead" colspan="3" style="height: 17px">
                    <cc1:SingleDropDownList ID="sqzszg" runat="server" Width="111px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
