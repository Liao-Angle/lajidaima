<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EG0106_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .style1
        {
            width: 189px;
        }
        .style2
        {
            width: 183px;
        }
        .style3
        {
            width: 176px;
        }
        .style4
        {
            width: 136px;
        }
        .style5
        {
            width: 166px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <cc1:SingleField ID="SheetNo" runat="server" Display="False"></cc1:SingleField>
        <cc1:SingleField ID="Subject" runat="server" Display="False"></cc1:SingleField>
        <table style="margin-left: 4px; width: 874px;" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td align="center" height="30" style="width: 800px">
                    <font style="font-family: 標楷體; font-size: large;"><b>門禁權限申請單</b></font>
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 877px;" border="0" cellspacing="0" cellpadding="1"
            class="BasicFormHeadBorder">
            <tr>
                <td valign="middle" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="人臉門禁權限" Width="120px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="djqx" runat="server" Width="200px" Height="16px" />
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBPrivilege" runat="server" Text="門禁權限" Width="123px"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <table width="100%">
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server" Text="一廠二樓MIS主機房進001" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox2" runat="server" Text="一廠二樓MIS主機房出002" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox3" runat="server" Text="一廠二樓茶水間進大辦公室003" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox4" runat="server" Text="一廠二樓茶水間出大辦公室004" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox5" runat="server" Text="一廠大廳玻璃門進005" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox6" runat="server" Text="一廠大廳玻璃門出006" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox7" runat="server" Text="一廠二樓大辦公室玻璃門進007" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox8" runat="server" Text="一廠二樓大辦公室玻璃門出008" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox9" runat="server" Text="MIS大廳機房進009" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox10" runat="server" Text="MIS大廳機房出010" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox11" runat="server" Text="大廳到一樓西側大辦公室進011" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox12" runat="server" Text="大廳到一樓西側大辦公室出012" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox13" runat="server" Text="一廠一樓HR到大廳進013" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox14" runat="server" Text="一廠一樓HR到大廳出014" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox15" runat="server" Text="一廠一樓D100 500半成品倉進015" Width="220px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox16" runat="server" Text="大廳監控室進016" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox17" runat="server" Text="一廠二樓D100 215 倉進017" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox18" runat="server" Text="一廠一樓MIS C機房進018" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox19" runat="server" Text="待啟用019" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox20" runat="server" Text="待啟用020" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox21" runat="server" Text="一廠二樓貴重物品倉出021" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox22" runat="server" Text="一廠二樓貴重物品倉進022" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox23" runat="server" Text="一廠二樓L100電子材料倉進023" Width="220px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox24" runat="server" Text="一廠一樓MIS D機房進024" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox25" runat="server" Text="一廠一樓1號電梯進025" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox26" runat="server" Text="一廠二樓1號電梯進026" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox27" runat="server" Text="一廠一樓2號電梯進027" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox28" runat="server" Text="一廠二樓2號電梯進028" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox29" runat="server" Text="一廠二樓SMT物料房進029" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox30" runat="server" Text="一廠二樓SMT物料房出030" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox31" runat="server" Text="一廠二樓關務資料室031" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox32" runat="server" Text="一廠二樓財務資料室032" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox33" runat="server" Text="一廠二樓TE燒佼保進033" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox34" runat="server" Text="一廠二樓SMT ICT治具室034" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox35" runat="server" Text="一廠二樓量測室035" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox36" runat="server" Text="LAB實驗室036" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox37" runat="server" Text="三廠二樓移印室037" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox38" runat="server" Text="三廠二樓MIS主機房038" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox39" runat="server" Text="三廠二樓MIS維修間039" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox40" runat="server" Text="三廠二樓ME設備間040" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox41" runat="server" Text="一廠二樓SMT備品房041" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox42" runat="server" Text="一廠一樓四號門進042" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox43" runat="server" Text="一廠一樓四號門出043" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox44" runat="server" Text="待啟用044" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox45" runat="server" Text="一廠一樓IQC Rohs測試區045" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox46" runat="server" Text="一廠二樓500倉046" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox47" runat="server" Text="一廠一樓ME教育訓練室047" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox48" runat="server" Text="一廠碼頭進出048" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox49" runat="server" Text="一廠二樓品保樣品室049" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox50" runat="server" Text="待啟用050" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox51" runat="server" Text="待啟用051" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox52" runat="server" Text="待啟用052" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox53" runat="server" Text="三廠二樓東側C機房053" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox54" runat="server" Text="三廠二樓東側D機房054" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox55" runat="server" Text="待啟用055" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox56" runat="server" Text="待啟用056" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox57" runat="server" Text="三廠1電梯057" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox58" runat="server" Text="三廠二樓IE維修間058" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox59" runat="server" Text="三廠二樓受電室059" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox60" runat="server" Text="三廠海關監管倉060" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox61" runat="server" Text="待啟用061" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox62" runat="server" Text="三廠2電梯062" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox63" runat="server" Text="三廠一樓3號門進063" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox64" runat="server" Text="三廠一樓3號門出064" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox65" runat="server" Text="貽百樓頂進065" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox66" runat="server" Text="貽百樓頂出066" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox67" runat="server" Text="貽百二樓大辦公室進067" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox68" runat="server" Text="貽百二樓量測室進068" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox69" runat="server" Text="待啟用069" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox70" runat="server" Text="待啟用070" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox71" runat="server" Text="待啟用071" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox72" runat="server" Text="待啟用072" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox73" runat="server" Text="三廠一樓倉庫東卷閘門進073" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox74" runat="server" Text="三廠一樓倉庫東卷閘門出074" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox75" runat="server" Text="三廠一樓實驗室進075" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox76" runat="server" Text="待啟用076" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox77" runat="server" Text="一廠二樓TE設備間077" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox78" runat="server" Text="一廠二樓TE維修間078" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox79" runat="server" Text="待啟用079" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox80" runat="server" Text="待啟用080" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox81" runat="server" Text="一廠一樓5號門進081" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox82" runat="server" Text="一廠一樓5號門出082" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox83" runat="server" Text="一廠一樓IE治具室083" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox84" runat="server" Text="一廠二樓ME設備間084" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox85" runat="server" Text="待啟用085" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox86" runat="server" Text="待啟用086" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox87" runat="server" Text="待啟用087" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox88" runat="server" Text="待啟用088" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox89" runat="server" Text="待啟用089" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox90" runat="server" Text="待啟用090" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox91" runat="server" Text="待啟用091" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox92" runat="server" Text="待啟用092" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox93" runat="server" Text="一廠二樓參觀通道進093" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox94" runat="server" Text="一廠二樓參觀通道出094" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox95" runat="server" Text="待啟用095" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox96" runat="server" Text="待啟用096" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox97" runat="server" Text="三廠二樓鐳射間097" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox98" runat="server" Text="三廠二樓鐳射間098" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox99" runat="server" Text="三廠二樓鐳射間099" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox100" runat="server" Text="三廠二樓鐳射間100" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox101" runat="server" Text="三廠二樓東側至機房進101" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox102" runat="server" Text="三廠二樓東側至機房出102" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox103" runat="server" Text="三廠大廳至二樓進103" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox104" runat="server" Text="三廠大廳至二樓出104" Width="200px">
                                </cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox105" runat="server" Text="三廠夾層至NPI辦公室105" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox106" runat="server" Text="待啟用106" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox107" runat="server" Text="待啟用107" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox108" runat="server" Text="待啟用108" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox109" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox110" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox111" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox112" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox113" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox114" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox115" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox116" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox117" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox118" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox119" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox120" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox121" runat="server" Text="三廠一樓大廳進(人臉)" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox122" runat="server" Text="三廠一樓車間進出(人臉)" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox123" runat="server" Text="三廠一樓車間進辦公室(人臉)" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox124" runat="server" Text="三廠一樓大廳玻璃門進出(人臉)" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox125" runat="server" Text="一廠二樓董事長辦公室(人臉)" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox126" runat="server" Text="南門三輥閘進出(人臉)" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox127" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:DSCCheckBox ID="DSCCheckBox128" runat="server" Text="待啟用" Width="200px"></cc1:DSCCheckBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" class="BasicFormHeadHead" valign="top" rowspan="2" width="80px">
                    <cc1:DSCLabel ID="LBApplication" runat="server" Text="申請人資料"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <panel id="EditPanel" runat="server">
                <table width="100%">
                    <tr>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead1" runat="server" Text="申請人" Width="70px"></cc1:DSCLabel>
                        </td>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead3" runat="server" Text="部門" Width="71px"></cc1:DSCLabel>
                        </td>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead4" runat="server" Text="職稱" Width="61px"></cc1:DSCLabel>
                        </td>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead5" runat="server" Text="申請事由" Width="93px"></cc1:DSCLabel>
                        </td>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead6" runat="server" Text="物理卡號" Width="98px"></cc1:DSCLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleOpenWindowField ID="EmpNo" runat="server"
                                showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                                tableName="hrusers" 
                                OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" 
                                valueIndex="1" idIndex="0" Width="171px" />
                        </td>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="PartNo" runat="server" Width="100px" />
                        </td>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="DtName" runat="server" Width="60px" />
                        </td>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="Reason" runat="server" Width="100px" />
                        </td>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="Note2" runat="server" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail colspan=5>
                            <cc1:GlassButton ID="AddData" runat="server" BackColor="White"
                                onclick="AddData_Click" ImageUrl="~\Images\lock.png"
                                 />
                        </td>
                    </tr>
                </table>
                </panel>
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadDetail">
                    <cc1:DataList ID="ApplicationList" runat="server" Height="200px" Width="96%" isShowAll="True"
                        NoModify="True" showTotalRowCount="True" NoAllDelete="True" NoAdd="True" IsHideSelectAllButton="True"
                        showExcel="True" IsOpenWinUse="True" NoDelete="True" />
                    <span style="margin-top: 0px; padding-top: 0px">
                        <cc1:FileUpload ID="FileUpload1" runat="server" NoDelete="True" OnAddOutline="FileUpload1_AddOutline"
                            Width="18px" BorderStyle="None" ViewStateMode="Disabled" Height="200px" />
                    </span>
                </td>
            </tr>
            <tr>
                <td valign="top" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBNote" runat="server" Text="備註說明"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Note" runat="server" Width="100%" Height="64px" MultiLine="True" />
                </td>
            </tr>
            <tr runat="server" id="showzg" visible="false">
                <td class="BasicFormHeadHead" style="height: 17px">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="請選擇簽核主管" Width="150px" Height="20px">
                    </cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadHead" style="height: 17px">
                    <cc1:SingleDropDownList ID="sqzszg" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
