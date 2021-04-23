<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0111_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>設備領用單</title>
    <style type="text/css">
        .BasicFormHeadBorder
        {
            width: 700px;
        }
        .style3
        {
            width: 115px;
        }
        .style5
        {
            width: 197px;
        }
        .style7
        {
            width: 225px;
        }
        .style11
        {
            font-size: medium;
        }
        .style12
        {
            font-size: x-small;
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
                <font style="font-family: 標楷體; font-size: large;"><b>設備領用申請單</b></font>
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
            <tr>
                <td colspan="4" class="BasicFormHeadHead">
                    <table width="100%">
                        <tr align="center">
                            <td class="style3">
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
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
            <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
                <tr>
                    <td>
                        <%--<cc1:DSCTabControl ID="DSCTabDoc" runat="server" Height="50px" Width="698px" 
                            SelectedTab="0" TabIndex="1">
                            <TabPages>--%>
                                <%--<cc1:DSCTabPage ID="lysb" runat="server" Enabled="True" Hidden="False" ImageURL=""
                                    Title="設備領用">--%>
                                    <%--<TabBody >--%>
                                    <tr>
                                        <td colspan=6 align="left" class="BasicFormHeadHead">
                                        <label style="font-weight:bold;" width="700px">領用設備</label>
                                        </td>
                                    </tr>
                                        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" 
                                            style="width: 680px;" ID="a1">
                                            <tr>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="LBEmployeeID" runat="server" Text="主板" Width="102px"></cc1:DSCLabel>
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="zb" runat="server" Width="100px" Style="text-align: center" />
                                                    塊
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="CPU" Width="102px" />
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="cpu" runat="server" Width="100px" Style="text-align: center" />
                                                    塊
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="CPU風扇" Width="102px" />
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="cupfs" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" 
                                            style="width: 680px;" ID="a2">
                                            <tr>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="硬碟" Width="102px"></cc1:DSCLabel>
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="disk" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="小電源" Width="102px" />
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="powerx" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="大電源" Width="102px" />
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="powerd" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" 
                                            style="width: 680px;" ID="a3">
                                            <tr>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="鼠標" Width="102px"></cc1:DSCLabel>
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="mouse" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="keyboard" runat="server" Text="小鍵盤" Width="102px" />
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="keyboardx" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="keyboar" runat="server" Text="大鍵盤" Width="102px" />
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="keyboardd" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" 
                                            style="width: 680px;" ID="a4">
                                            <tr>
                                                <td width="120px" align="left" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="百兆有線網卡" Width="102px"></cc1:DSCLabel>
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="bzwk" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="網線(5米)" Width="102px" Style="text-align: left" />
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="qzwk" runat="server" Width="100px" Style="text-align: center" />
                                                    根
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="電話機" Width="102px"></cc1:DSCLabel>
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="telphone" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" 
                                            style="width: 680px;" ID="a">
                                            <tr>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="內存條" Width="102px"></cc1:DSCLabel>
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="memory" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="網線(3米)" Width="102px" />
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="wx" runat="server" Width="100px" Style="text-align: center" />
                                                    根
                                                </td>
                                                <td width="120px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="顯卡" Width="102px" />
                                                </td>
                                                <td width="280px" align="left" class="BasicFormHeadDetail">
                                                    <cc1:SingleField ID="xk" runat="server" Width="100px" Style="text-align: center" />
                                                    個
                                                </td>
                                                
                                            </tr>
                                        </table>
                                   <%-- </TabBody>
                                </cc1:DSCTabPage>--%>
                                <%--<%--<cc1:DSCTabPage runat="server" Title="整機領用" Enabled="True" ImageURL="" Hidden="False"
                                    ID="zjly">
                                    <%--<TabBody>--%>
                                       <%-- <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" 
                                            style="width: 650px;">
                                            <tr>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="mc" runat="server" Text="名稱" Width="136px" 
                                                        Style="margin-top: 0px" TextAlign="1">
                                                    </cc1:DSCLabel>
                                                </td>
                                                <td align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="size" runat="server" Text="規格" Width="427px" Style="margin-left: 0px; text-align: center;"
                                                        Height="16px" TextAlign="1"></cc1:DSCLabel>
                                                </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="price" runat="server" Text="參考價格" Width="68px" 
                                                        Height="16px" />
                                                </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="count" runat="server" Text="數量" Width="40px" />
                                                </td>
                                            </tr>--%>
                                            <%--</table>--%>
                                            <%--<table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadDetail" 
                                            style="width: 650px;">--%>
                                            <%--<tr>
                                                <td width="100px" class="BasicFormHeadHead">
                                                    <span class="style11">產線電腦</span><br />（小機箱）</td>
                                                <td class="BasicFormHeadHead">
                                                    <span class="style12">1.主板:GIGABYTE H61M-S2PH(4個PCI界面,集成顯卡)</span><br class="style12" /> <span 
                                                        class="style12">2.CPU: Intel 酷睿G3260</span><br class="style12" /> <span 
                                                        class="style12">3.硬盤:希捷500G 7200轉 
                                                    <br />
                                                    4.記憶體: Kingston DDR3 4G<br />5.顯示器: Dell 17寸標屏<br />6.機箱電源: 東方城 金河田電源300W<br />7.鍵鼠: 
                                                    聯繫L-100/羅技鼠標MK100</span></td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="3550" Width="68px" 
                                                        Height="16px" TextAlign="1" />
                                                </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:SingleField ID="CXDN" runat="server" Width="40px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100px" class="BasicFormHeadHead">
                                                    <span class="style11">辦公電腦</span><br />（大機箱）</td>
                                                <td class="BasicFormHeadHead">
                                                    <span class="style12">1.主板:華碩（ASUS）H81M主板</span><br class="style12" /> <span 
                                                        class="style12">2.CPU: Intel 酷睿I3 4170</span><br class="style12" /> <span 
                                                        class="style12">3.硬盤:金勝維/金典 60G 固態硬盤 希捷500G 7200轉 硬盤 
                                                    <br />
                                                    4.記憶體: Kingston DDR3 4G<br />5.顯示器: AOC 19.5寸寬屏<br />6.機箱電源: SAMA 
                                                    工匠3+領航550電源300W<br />7.鍵鼠: 羅技MK100 二代鍵鼠套裝</span></td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="3550" Width="68px" 
                                                        Height="16px" TextAlign="1" />
                                                </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:SingleField ID="BGDN" runat="server" Width="40px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100px" class="BasicFormHeadHead">
                                                    <span class="style11">工程電腦</span><br />（品牌機）</td>
                                                <td class="BasicFormHeadHead">
                                                    <span class="style12">1.ACER 19寸寬屏 LCD顯示器</span><br class="style12" /> <span 
                                                        class="style12">2.ACER D430主機: I5-6100/4G/500G/1G獨顯/鍵鼠套裝</span><br class="style12" /> </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="4800" Width="68px" 
                                                        Height="16px" TextAlign="1" />
                                                </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:SingleField ID="GCDNP" runat="server" Width="40px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100px" class="BasicFormHeadHead">
                                                    <span class="style11">工程電腦</span><br />（組裝機）</td>
                                                <td class="BasicFormHeadHead">
                                                    <span class="style12">1.主板:GIGABYTE H110(4個PCI界面,集成顯卡)</span><br class="style12" /> <span 
                                                        class="style12">2.CPU: Intel 酷睿 I5 6400</span><br class="style12" /> <span 
                                                        class="style12">3.硬盤:固態硬盤120G,希捷500G 7200轉 
                                                    <br />
                                                    4.記憶體: Kingston DDR3 4G<br />5.顯示器: AOC19寸寬屏<br />6.機箱電源: 大水牛機箱+金河田電源400W<br />7.鍵鼠:羅技鼠標MK100 
                                                    二代鍵鼠套裝<br />8.2G獨顯</span></td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel19" runat="server" Text="4500" Width="68px" 
                                                        Height="16px" TextAlign="1" />
                                                </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:SingleField ID="GCDNZ" runat="server" Width="40px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100px" class="BasicFormHeadHead">
                                                    <span class="style11">工業電腦<br />(主機)</span></td>
                                                <td class="BasicFormHeadHead">
                                                    <span class="style12">1.鍵鼠套裝MK100<br />2.顯示器ACER V173 or ACER AOC 712S<br />3.RK-607B主機殼/（雙核E5300-DualCore 
                                                    2.6GHzLGA-775/4G記憶體/A301主機板/500G硬碟/460W電源）</span></td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel20" runat="server" Text="6070" Width="68px" 
                                                        Height="16px" TextAlign="1" />
                                                </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:SingleField ID="GYDN6" runat="server" Width="40px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100px" class="BasicFormHeadHead">
                                                    <span class="style11">工業電腦<br />(主機)</span></td>
                                                <td class="BasicFormHeadHead">
                                                    <span class="style12">1.鍵鼠套裝MK100</span><br class="style12" /> <span 
                                                        class="style12">2.顯示器ACER V173 or ACER AOC 712S</span><br class="style12" /> <span 
                                                        class="style12">3.凌華RK610A主機殼/i7-860/8G/E340主機板/500G HDD/（i7處理器集成Inter HD 
                                                    Graphics顯卡）</span></td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="9200" Width="68px" 
                                                        Height="16px" TextAlign="1" />
                                                </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:SingleField ID="GYDN9" runat="server" Width="40px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100px" class="BasicFormHeadHead">
                                                    <span class="style11">工業電腦<br />主機+顯示器</span></td>
                                                <td class="BasicFormHeadHead">
                                                    <span class="style12">1.顯示器AOC 21寸顯示器</span><br class="style12" /> <span 
                                                        class="style12">2.研華IPC-7132主機殼/i7-2600/內存8G/500G 
                                                    HDD/主機板AIMB-701VG（i7處理器集成，獨顯2G）</span><br class="style12" /> </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:DSCLabel ID="DSCLabel22" runat="server" Text="8900" Width="68px" 
                                                        Height="16px" TextAlign="1" />
                                                </td>
                                                <td width="100px" align="right" class="BasicFormHeadHead">
                                                    <cc1:SingleField ID="GYDN8" runat="server" Width="40px" />
                                                </td>
                                            </tr>
                                        </table>--%>
                                    <%--</TabBody>--%>
                                <%--</cc1:DSCTabPage>--%>
                            <%--</TabPages>
                        </cc1:DSCTabControl>--%>
                    </td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                    <td valign="top" align="left" class="BasicFormHeadHead" width="102px">
                        <cc1:DSCLabel ID="LBReason" runat="server" Text="其他領用" Height="16px" Width="70px">
                        </cc1:DSCLabel>
                    </td>
                    <td colspan="3" class="BasicFormHeadDetail">
                        <cc1:SingleField ID="QTLY" runat="server" Width="99%" Height="64px" MultiLine="True" />
                    </td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                    <td valign="top" align="left" width="102px">
                        <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="更換原因" Height="16px" Width="70px"
                            Style="margin-left: 0px"></cc1:DSCLabel>
                    </td>
                    <td colspan="3" class="BasicFormHeadDetail">
                        <cc1:SingleField ID="reason" runat="server" Width="99%" Height="64px" MultiLine="True"
                            Style="margin-left: 0px" />
                    </td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                    <td valign="top" align="left" class="BasicFormHeadHead" width="102px">
                        <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="備註說明" Height="16px" Width="70px">
                        </cc1:DSCLabel>
                    </td>
                    <td colspan="3" class="BasicFormHeadDetail">
                        <cc1:SingleField ID="remark" runat="server" Width="99%" Height="64px" MultiLine="True" />
                    </td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;
                height: 41px;">
                <tr>
                    <td colspan="4" align="left" class="BasicFormHeadHead">
                        <strong>領用人簽字: </strong>
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
        </table>
    </div>
    </form>
</body>
</html>
