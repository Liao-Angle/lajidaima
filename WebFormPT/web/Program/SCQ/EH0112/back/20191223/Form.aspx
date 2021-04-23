<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EH0112_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>離職申請單</title>
    <style type="text/css">
        .style1
        {
            width: 327px;
        }
        .style2
        {
            width: 37px;
            height: 26px;
        }
        .style3
        {
            height: 26px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
        <tr>
            <td colspan="5" class="BasicFormHeadHead" width="100%">
                <b>
                    <cc1:GlassButton ID="PrintButton2" runat="server" Height="20px" Width="300" Text="列印離職單"
                        OnBeforeClicks="PrintButton_OnClick" Enabled="True" />
                </b>
            </td>
        </tr>
    </table>
    <div>
        <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
        <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
        <cc1:SingleField ID="line" runat="server" Display="true"></cc1:SingleField>
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
                        idIndex="0" OnSingleOpenWindowButtonClick="JEmpNo_SingleOpenWindowButtonClick" />
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
                    <cc1:SingleField ID="RqDate" runat="server" Width="132px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="申請離職日期" Width="102px" Style="margin-left: 0px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="LeaveDate" runat="server" Width="143px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="職位" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="DtName" runat="server" Width="155px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="崗位" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Gwei" runat="server" Width="132px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="聯繫電話" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Phone" runat="server" Width="145px" Height="16px" />
                </td>
            </tr>
            <tr>
              <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="崗位類別" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="GwType" runat="server" Width="155px" Height="16px" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="崗位編號" Width="68px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="GwNo" runat="server" Width="132px" Height="16px" />
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
                    <cc1:DSCCheckBox ID="DSCCheckBox15" runat="server" ReadOnly="False" />
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
                <td colspan="2" class="BasicFormHeadDetail">
                    <cc1:DSCCheckBox ID="DSCCheckBox35" runat="server" />
                    是否已開通ERP、PLM賬號 &nbsp;
                </td>
                <td class="BasicFormHeadDetail">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadDetail">
                    經主管核准離職后,請依下列各項內容辦理交接：
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" class="BasicFormHeadBorder" border="0"
            cellspacing="0" cellpadding="1">
            <tr>
                <td class="BasicFormHeadDetail">
                    序號
                </td>
                <td class="BasicFormHeadDetail">
                    單位
                </td>
                <td class="BasicFormHeadDetail">
                    項目
                </td>
                <td class="BasicFormHeadDetail">
                    費用
                </td>
                <td class="BasicFormHeadDetail">
                    備註
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    1
                </td>
                <td class="BasicFormHeadDetail">
                    本部門
                </td>
                <td class="BasicFormHeadDetail">
                    工作進度說明,工具儀器,相關數據等移交
                </td>
                <td class="BasicFormHeadDetail">
                    &nbsp;
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note1" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    2
                </td>
                <td class="BasicFormHeadDetail">
                    信息
                </td>
                <td class="BasicFormHeadDetail">
                    取消mail及各項系統賬號
                </td>
                <td class="BasicFormHeadDetail">
                    &nbsp;
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note2" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    3
                </td>
                <td class="BasicFormHeadDetail">
                    財務
                </td>
                <td class="BasicFormHeadDetail">
                    預借款項及其他款項是否結清
                </td>
                <td class="BasicFormHeadDetail">
                    &nbsp;
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note3" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td rowspan="6" class="BasicFormHeadDetail">
                    4
                </td>
                <td rowspan="6" class="BasicFormHeadDetail">
                    人事
                </td>
                <td class="BasicFormHeadDetail">
                    社會保險，住房公積金轉出
                </td>
                <td class="BasicFormHeadDetail">
                    &nbsp;
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note4" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    欠班年休
                    <cc1:GlassButton ID="GlassButton1" runat="server" Height="16px" OnClick="GlassButton1_Click"
                        Text="查詢" Width="42px" />
                </td>
                <td class="BasicFormHeadDetail">
                    &nbsp;
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note5" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    卡片，員工手冊
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PayNo35" runat="server" Width="75px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note6" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    上崗證管理
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="sgz" runat="server" Width="75px" Height="16px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note12" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    培訓協議
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PayNo29" runat="server" Width="75px" Height="16px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note7" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    識別證回收(廠牌，易拉釦)
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PayNo34" runat="server" Width="75px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note8" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td rowspan="3" class="BasicFormHeadDetail">
                    5
                </td>
                <td rowspan="3" class="BasicFormHeadDetail">
                    總務
                </td>
                <td class="BasicFormHeadDetail">
                    物品歸還（工作服/鞋、靜電環、內務柜鑰匙）
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PayNo38" runat="server" Width="75px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note9" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    退宿辦理（宿舍物品確認歸還，無損壞）
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PayNo67" runat="server" Width="75px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note10" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    住宿扣款
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PayNo78" runat="server" Width="75px" />
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="note11" runat="server" Height="25px" Style="margin-right: 71px"
                        MultiLine="True" Width="150px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
