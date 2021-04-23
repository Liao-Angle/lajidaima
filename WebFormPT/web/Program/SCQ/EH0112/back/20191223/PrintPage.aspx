<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="Program_SCQ_Form_EH0112_PrintPage" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<style type="text/css">
    @media print
    {
        .print
        {
            display: none;
        }
    }
    
    .style1
    {
        border-left: 1px none rgb(188,178,147);
        border-right: 1px solid rgb(188,178,147);
        border-top: 1px none rgb(188,178,147);
        border-bottom: 1px solid rgb(188,178,147);
        background-color: white;
        font-size: medium;
        font-family: Arial;
        height: 20px;
    }
</style>
<body style="background: #ffffff">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <input type="Button" value="列印單據" onclick="javascript:print();" class="print">
        </table>
        <table>
            <br>
            <tr>
                <td style="font-size: 8pt;">
                    表單資訊
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divFormInfo" style="position: absolute;" runat="server">
                    </div>
                    <br />
                </td>
            </tr>
        </table>
        <br>
        <br />
        <br />
        <br />
        <br />
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
                    <font style="font-family: 標楷體; font-weight: 700;" class="style1">離職申請單</font>
                </td>
            </tr>
            <tr>
                <td align="left" height="30" style="width: 670px">
                    <font style="font-family: 標楷體; font-weight: 700;" class="style1">人事編碼：<cc1:DSCLabel
                        ID="HRCODE" runat="server" Width="165px" />
                    </font>
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="工號" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="357px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" valueIndex="1"
                        idIndex="0" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    部門
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartNo" runat="server" Width="180px" Height="16px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="入職日期" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="ComeDate" runat="server" Width="155px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="申請日期" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="RqDate" runat="server" Width="145px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="離職日期" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="LeaveDate" runat="server" Width="155px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="職位" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="DtName" runat="server" Width="155px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="崗位" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Gwei" runat="server" Width="145px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="聯繫電話" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Phone" runat="server" Width="145px" Height="16px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="離職性質：" Width="86px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server" Checked="True" />
                    正常離職
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox2" runat="server" />
                    其他
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td rowspan="11" class="BasicFormHeadDetail">
                    離<br />
                    職<br />
                    原<br />
                    因<br />
                    類<br />
                    別
                </td>
                <td class="BasicFormHeadDetail">
                    A、組織管理
                </td>
                <td class="BasicFormHeadDetail">
                    B、工作因素
                </td>
                <td class="BasicFormHeadDetail">
                    C、薪資福利
                </td>
                <td class="BasicFormHeadDetail">
                    D、個人原因
                </td>
                <td class="BasicFormHeadDetail">
                    E、發展空間
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox3" runat="server" />
                    不適應管理風格
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox4" runat="server" />
                    日常加班少
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox5" runat="server" />
                    薪資偏低
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox6" runat="server" />
                    結婚或生子
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox7" runat="server" />
                    晉升機會太少
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox8" runat="server" />
                    公司制度太嚴
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox9" runat="server" />
                    加班時間太長
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox10" runat="server" />
                    與承諾薪資不符
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox11" runat="server" />
                    健康不佳
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox12" runat="server" />
                    能力未獲肯定
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox13" runat="server" />
                    工作氣氛不佳
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox14" runat="server" />
                    工作量太大
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox15" runat="server" />
                    食宿不佳
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox16" runat="server" />
                    創業
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox17" runat="server" />
                    缺乏輪調機會
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox18" runat="server" />
                    主管不公平
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox19" runat="server" />
                    不適應倒班
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox20" runat="server" />
                    交通不便
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox21" runat="server" />
                    升學
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox22" runat="server" />
                    學習機會太少
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox23" runat="server" />
                    班組長管理太嚴
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox24" runat="server" />
                    工作環境不佳
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox25" runat="server" />
                    福利太低
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox26" runat="server" />
                    個人或家庭因素
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox27" runat="server" />
                    專業不對口
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox28" runat="server" />
                    其他
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox29" runat="server" />
                    崗位不適應
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox30" runat="server" />
                    其他
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox31" runat="server" />
                    其他
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox32" runat="server" />
                    其他
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    &nbsp;
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox33" runat="server" />
                    其他
                </td>
                <td class="BasicFormHeadDetail">
                    &nbsp;
                </td>
                <td colspan="2" class="BasicFormHeadDetail">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5" class="BasicFormHeadDetail">
                    <strong>是否需要做離職體檢：
                    <cc1:DSCCheckBox ID="DSCCheckBox36" runat="server" />
                    是
                    <cc1:DSCCheckBox ID="DSCCheckBox37" runat="server" />
                    否(本人自願放棄公司組織的離崗體檢,由此產生的一切後果,由我本人承擔.)
                </strong>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="BasicFormHeadDetail">
                    本人簽字確認：
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td rowspan="6" class="BasicFormHeadDetail">
                    法<br />
                    律<br />
                    責<br />
                    任<br />
                    提<br />
                    醒
                </td>
            </tr>
            <tr>
                <td>
                    <strong>「說明」</strong>
                </td>
            </tr>
            <tr>
                <td>
                    按照國家法律和合同約定及本公司要求提前告知公司離職，是員工必須履行的義務。離職日期確定後，需要辦理完工作交接後，才能離開，是員工必須履行的義務。對於應交接的事項，若因您的原因未交接清楚，對此員工負有民事責任，造成損失的公司可追償。如您與本公司存在培訓等其他服務協定或因個人原因給公司造成損失的，可能存在賠償責任。員工意願提交公司以後，員工本人無權更改或撤回意願，以上事項您清楚嗎？
                </td>
            </tr>
            <tr>
                <td>
                    <strong>「本人確認」</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>本人清楚上述法律責任，並基於上述原因自願解除與公司的勞動合同。</strong>
                </td>
            </tr>
            <tr>
                <td>
                    ★申請者簽名：__________________ 日期：___________ 年_______ 月_______日
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="font-size: 8pt;">
                    簽核意見
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div2" style="position: absolute;" runat="server">
                    </div>
                    <br />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
