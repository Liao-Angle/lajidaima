<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0111_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>設備領用申請單</title>
    <style type="text/css">
        .style1
        {
            font-size: medium;
        }
        .style2
        {
            text-align: left;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
         <cc1:SingleField id="SheetNo" runat="server" Display="False"></cc1:SingleField>
     <cc1:SingleField id="Subject" runat="server" Display="False"></cc1:SingleField>
     <table style="width: 800px;" border="0" cellspacing="0" 
             cellpadding="1">
     <tr>
      <td align="center" height="30">
            <font style="font-family: 標楷體; font-size: large;"><b>設備領用申請單</b></font></td>
     </tr>
     </table>
    <div>
        <table width=800px border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
         <tr>
            <td colspan=4 align="left" class="BasicFormHeadHead">
                <label style="font-size:10px;font-weight:bold;">申請人信息</label></td>
        </tr>

         <tr>
            <td colspan=4  class="BasicFormHeadHead">
              <table width="100%">
                <tr align="center">
                    <td>申請人</td>
                    <td>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server"
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" 
                    valueIndex="1" idIndex="0" Width="165px" />
                    </td>
                     <td>申請人分機</td>
                    <td>

            <cc1:SingleField ID="mobileuser" runat="server" Width="119px" />

                    </td>
                     <td>申請人部門</td>
                    <td>
     
            <cc1:SingleField ID="partNouser" runat="server" Width="119px" />
     
                    </td>
                </tr>
              </table>
            </td>
        </tr>


        <tr>
            <td colspan=4 align="left" class="BasicFormHeadHead">
                <label style="font-weight:bold;" class="style1">領用設備</label></td>
        </tr>
        <tr>
            <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBEmployeeID" runat="server" Text="辦公電腦"></cc1:DSCLabel></td>
            <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="BGDN" runat="server" Width="150px" />
                套</td>
            <td width=120px align="right" class="BasicFormHeadHead">
               <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="產線電腦" />
            </td>
            <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="CXDN" runat="server" Width="150px" />
                套</td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="XSQ" runat="server" Text="顯示器"></cc1:DSCLabel>
            </td>
            <td  class=BasicFormHeadDetail>
                    <cc1:DSCRadioButton ID="HitchY" runat="server" Text="17寸" Checked="True" 
                    Width="59px" GroupName="Hitch" />
                    <cc1:DSCRadioButton ID="HitchN" runat="server" Text="19寸" Width="68px" 
                    GroupName="Hitch" />
                    <cc1:DSCRadioButton ID="HitchM" runat="server" Text="LCD" Width="68px" 
                    GroupName="Hitch" />
                    <cc1:SingleField ID="LXSQ" runat="server" Width="43px" />
                    臺</td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="JP" runat="server" Text="鍵盤"></cc1:DSCLabel>
                </td>
            <td  class=BasicFormHeadDetail>
                    <cc1:DSCRadioButton ID="JPD" runat="server" Text="大" Checked="True" 
                    Width="59px" GroupName="JP" />
                    <cc1:DSCRadioButton ID="JPX" runat="server" Text="小" Width="68px" 
                    GroupName="JP" />
                <cc1:SingleField ID="LJP" runat="server" Width="63px" 
                    style="margin-left: 49px" />
                個</td>
        </tr>
        
           <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="鼠標"></cc1:DSCLabel>
            </td>
            <td  class=BasicFormHeadDetail>
                <cc1:SingleField ID="SHUBIAO" runat="server" Width="106px" />
                個</td>
            <td align="right"  class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="電話機"></cc1:DSCLabel>
                </td>
            <td  class=BasicFormHeadDetail>
                <cc1:SingleField ID="DIANHUA" runat="server" Width="119px" />
                部</td>
        </tr>
        
        <tr>
            <td valign="top" align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBReason" runat="server" Text="其他領用" ></cc1:DSCLabel>
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleField ID="QTLY" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        
        
        <tr>
            <td valign="top" align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="備註欄位" ></cc1:DSCLabel>
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleField ID="Remark" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
         
        <tr runat="server" id="showzg" visible="false">
            <td class="BasicFormHeadHead" colspan="3" style="height: 17px">
                <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="請選擇簽核主管" Width="150px" Height="20px"></cc1:DSCLabel>
            </td>
            <td class="BasicFormHeadHead" colspan="3" style="height: 17px">
                <cc1:SingleDropDownList ID="sqzszg" runat="server" Width="111px" />
            </td>
        </tr> 

        <tr>
            <td colspan="4" align="center" class="BasicFormHeadHead">
                    <table  class="BasicFormHeadBorder" width="798px">
                        <tr  >
                                <td colspan=4 align="left" class="BasicFormHeadHead">
                                <label style="font-weight:bold;" class="style1">更換(維修)設備</label></td>
                        </tr>
                        <tr>
                            <td align="right" class="BasicFormHeadHead" width=118px;>
                                <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="更換主機零件明細"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail" width="276px">
                                <cc1:SingleField ID="ZHUJI" runat="server" Width="261px" MultiLine="True" 
                                    Height="25px" style="margin-right: 5px; margin-top: 0px" />
                            </td>
                            <td align="right" class="BasicFormHeadHead" width=118px;>
                                <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="更換原因"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail" width="276px">
                                <cc1:SingleField ID="ZJR" runat="server" Width="273px" MultiLine="True" 
                                    Height="25px" style="margin-right: 5px; margin-top: 0px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="顯示器"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail">
                    <cc1:DSCRadioButton ID="WXY" runat="server" Text="17寸" Checked="True" 
                    Width="59px" GroupName="WX" />
                    <cc1:DSCRadioButton ID="WXN" runat="server" Text="19寸" Width="68px" 
                    GroupName="WX" />
                                <cc1:SingleField ID="WXS" runat="server" Width="51px"/>
                                臺</td>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="更換原因"></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleField ID="XSQR" runat="server" Width="272px" MultiLine="True" 
                                    Height="25px" style="margin-right: 5px; margin-top: 0px" />
                            </td>
                        </tr>
                      <%--<panel id="kmasp" runat="server" visible="false">--%>
                        <tr>
                            <td align="right" class="BasicFormHeadHead" >
                                  <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="掃描槍" ></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail">
                    <cc1:DSCRadioButton ID="SMQA" runat="server" Text="一維" Checked="True" 
                    Width="59px" GroupName="SMQ" />
                    <cc1:DSCRadioButton ID="SMQB" runat="server" Text="二維" Width="68px" 
                    GroupName="SMQ" />
                    <cc1:DSCRadioButton ID="SMQC" runat="server" Text="無線" Width="68px" 
                    GroupName="SMQ" />
                                  <cc1:SingleField ID="SMQW" runat="server" Width="41px" />支</td>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel19" runat="server" Text="更換原因"></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleField ID="SMQR" runat="server" Width="274px" MultiLine="True" 
                                      Height="25px" style="margin-right: 5px; margin-top: 0px" />
                            </td>
                        </tr>
                            <td valign="top" align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="備註欄位" ></cc1:DSCLabel>
                            </td>
                            <td colspan="3" class="BasicFormHeadDetail">
                                <cc1:SingleField ID="WBZ" runat="server" Width="100%" Height="64px" 
                                    MultiLine="True" />
                            </td>
                        <tr>
                            <td colspan=4 class="style2">
                                 領用人簽字:
                            </td>
                        </tr>
                        <%--</panel>--%>
                    </table>
            </td>
        </tr> 
        </table>
    </div>
    </form>
</body>
</html>
