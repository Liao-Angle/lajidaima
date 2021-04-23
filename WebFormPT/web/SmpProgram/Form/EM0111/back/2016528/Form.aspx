<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0111_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>設備領用單</title>
    <style type="text/css">
        .style2
        {
            width: 280px;
        }
        .BasicFormHeadBorder
        {
            width: 800px;
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
        .style8
        {
            width: 525px;
        }
        .style9
        {
            width: 168px;
        }
        .style10
        {
            width: 172px;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
        <cc1:SingleField id="SheetNo" runat="server" Display="False"></cc1:SingleField>
        <cc1:SingleField id="Subject" runat="server" Display="False"></cc1:SingleField>
        <table style="width: 700px;" border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td align="center" height="30">
            <font style="font-family: 標楷體; font-size: large;"><b>設備領用申請單</b></font></td>
        </tr>
        </table>
    <div>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
         <tr>
            <td colspan=4 align="left" class="BasicFormHeadHead">
            <label style="font-size:12px;font-weight:bold;">申請人信息</label></td>
        </tr>
        <tr>
            <td colspan=4  class="BasicFormHeadHead">
              <table width="100%">
                <tr align="center">
                    <td class="style3">申請人</td>
                    <td><cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="226px" 
                            showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                            tableName="hrusers" valueIndex="1" idIndex="0" /></td>
                    <td class="style7">申請人分機</td>
                    <td>
                     <cc1:SingleField ID="mobileuser" runat="server" Width="102px" 
                            style="margin-left: 12px" />
                    </td>
                     <td class="style5">申請人部門</td>
                    <td>
                     <cc1:SingleField ID="partNouser" runat="server" Width="119px" />
                    </td>
                </tr>
             </table>
        </table>
        <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                    <td colspan=6 align="left" class="BasicFormHeadHead">
                    <label style="font-weight:bold;" width="700px">領用設備</label>
                    </td>
                </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td width=120px align="right" class="BasicFormHeadHead" >
                <cc1:DSCLabel ID="LBEmployeeID" runat="server" Text="主板" Width="102px"></cc1:DSCLabel></td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="zb" runat="server" Width="100px" />塊
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="CPU" Width="102px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="cpu" runat="server" Width="100px" />塊
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="CPU風扇" Width="102px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="cupfs" runat="server" Width="100px" />個
                </td>
                </tr>
                </table>

                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="硬碟" Width="102px"></cc1:DSCLabel></td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="disk" runat="server" Width="100px" />個
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="小電源" Width="102px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="powerx" runat="server" Width="100px" />個
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="大電源" Width="102px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="powerd" runat="server" Width="100px" />個
                </td>
                </tr>
                </table>

                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="鼠標" Width="102px"></cc1:DSCLabel></td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="mouse" runat="server" Width="100px" />個
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="keyboard" runat="server" Text="小鍵盤" Width="102px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="keyboardx" runat="server" Width="100px" />個
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="keyboar" runat="server" Text="大鍵盤" Width="102px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="keyboardd" runat="server" Width="100px" />個
                </td>
                </tr>
                </table>

                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td width=120px align="left" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="百兆有線網卡" Width="102px"></cc1:DSCLabel></td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="bzwk" runat="server" Width="100px" />個
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="千兆有線網卡" Width="102px" 
                        style="text-align: left" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="qzwk" runat="server" Width="100px" />個
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="無線網卡" Width="102px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="wxwk" runat="server" Width="100px" />個
                </td>
                </tr>
                </table>

                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="內存條" Width="102px"></cc1:DSCLabel></td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="memory" runat="server" Width="100px" />個
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="顯卡" Width="102px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="xk" runat="server" Width="100px" />個
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="網線" Width="102px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="wx" runat="server" Width="100px" />米
                </td>
                </tr>
                </table>

                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="電話機" Width="102px"></cc1:DSCLabel></td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="telphone" runat="server" Width="100px" />個
                </td>
                <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="掃描槍" Width="102px" 
                        style="margin-left: 0px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="smq" runat="server" Width="100px" />支
                </td>
                <td width=120px align="right">
                <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="" Width="188px" />
                </td>
                <td width="280px" align="left" class="BasicFormHeadDetail">
                </td>
                </tr>
                </table>

                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td valign="top" align="left" width="102px">
                <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="更換原因" Height="16px" Width="70px" 
                        style="margin-left: 0px" ></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleField ID="reason" runat="server" Width="99%" Height="64px"  
                        MultiLine="True" style="margin-left: 0px" />
                </td>
                </tr>
                </table>

                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td valign="top" align="left" class="BasicFormHeadHead" width="102px">
                <cc1:DSCLabel ID="LBReason" runat="server" Text="其他領用" Height="16px" Width="70px" ></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleField ID="QTLY" runat="server" Width="99%" Height="64px"  
                        MultiLine="True" />
                </td>
                </tr>
                </table>

                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td valign="top" align="left" class="BasicFormHeadHead" width="102px">
                <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="備註說明" Height="16px" 
                        Width="70px" ></cc1:DSCLabel>
                </td>
                <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleField ID="remark" runat="server" Width="99%" Height="64px"  
                        MultiLine="True" />
                </td>
                </tr>
                </table>

                <table border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder" style="width: 700px;">
                <tr>
                <td colspan=4 align="left" class="BasicFormHeadHead">領用人簽字: </td>
                </tr>
                </table>

              </table>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
