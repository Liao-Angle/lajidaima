<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EG0103_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
   <script type="text/javascript">

       function showt() {
           var odiv = document.getElementById('dds');
           odiv.style.display = "block";
       }
   </script>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <table width=800px border="0" cellspacing="0" cellpadding="1" class="BasicFormHeadBorder">
        <tr>
            <td colspan=4 align="left" class="BasicFormHeadHead">
                <label style="font-size:10px;font-weight:bold;">申請內容</label></td>
        </tr>
        <tr>
            <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBEmployeeID" runat="server" Text="用車人"></cc1:DSCLabel></td>
            <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="CName" runat="server" Width="150px" />
            </td>
            <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="可否拼車"></cc1:DSCLabel>
            </td>
            <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:DSCRadioButton ID="HitchY" runat="server" Text="可以" Checked="True" 
                    Width="59px" GroupName="Hitch" />
                <cc1:DSCRadioButton ID="HitchN" runat="server" Text="不可以" Width="68px" 
                    GroupName="Hitch" />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBDepartment" runat="server" Text="用車部門"></cc1:DSCLabel>
            </td>
            <td  class=BasicFormHeadDetail>
                <cc1:SingleField ID="Department" runat="server" Width="150px" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBDtName" runat="server" Text="手機號碼"></cc1:DSCLabel>
                </td>
            <td  class=BasicFormHeadDetail>
                <cc1:SingleField ID="Mobile" runat="server" Width="119px" />
            </td>
        </tr>
        
           <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="出發地點"></cc1:DSCLabel>
            </td>
            <td  class=BasicFormHeadDetail>
                <cc1:SingleField ID="Departure" runat="server" Width="150px" />
            </td>
            <td align="right"  class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="目的地點"></cc1:DSCLabel>
                </td>
            <td  class=BasicFormHeadDetail>
                <cc1:SingleField ID="Destination" runat="server" Width="119px" />
            </td>
        </tr>
        
           <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="同行人數" ></cc1:DSCLabel>
            </td>
            <td class="BasicFormHeadDetail">
                <cc1:SingleField ID="Peers" runat="server" Width="150px"   />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="去程時間"></cc1:DSCLabel>
                </td>
            <td  class=BasicFormHeadDetail>
                <cc1:SingleField ID="DepartureTime" runat="server" Width="119px"   />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBReason" runat="server" Text="申請原因" ></cc1:DSCLabel>
            </td>
            <td colspan="3" class="BasicFormHeadDetail">
                <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" 
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
        <tr>
            <td colspan="4" align="center" class="BasicFormHeadHead">
                <Panel ID=EditPanel runat=server visible="false">
                    <table  class="BasicFormHeadBorder" width="798px">
                        <tr  >
                                <td colspan=4 align="left" class="BasicFormHeadHead">
                                <label style="font-size:10px;font-weight:bold;">調度室填寫</label></td>
                        </tr>
                        <tr>
                            <td align="right" class="BasicFormHeadHead" width=118px;>
                                <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="司機姓名"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail" width="276px">
                                <cc1:SingleField ID="UserName" runat="server" />
                            </td>
                            <td align="right" class="BasicFormHeadHead" width=118px;>
                                <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="車牌號碼"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail" width="276px">
                                <cc1:SingleField ID="Plate" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="聯繫電話"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:SingleField ID="UserMobile" runat="server"/>
                            </td>
                            <td align="right" class="BasicFormHeadHead">
                                &nbsp;</td>
                            <td class=BasicFormHeadDetail>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="派車方式"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:SingleDropDownList ID="LeaveTypeID" runat="server" onselectchanged="LeaveTypeID_SelectChanged"  />
                            </td>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="用車類型"></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleDropDownList ID="Genre" runat="server" Width="114px" OnSelectChanged="Genre_SelectChanged" />
                            </td>
                        </tr>
                      <panel id="kmasp" runat="server" visible="false">
                        <tr>
                            <td align="right" class="BasicFormHeadHead" >
                                  <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="出發里程數" ></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail">
                                  <cc1:SingleField ID="StartKm" runat="server" />/Km
                            </td>
                            <td align="right" class="BasicFormHeadHead">
                                  <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="出廠保安簽名" ></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadDetail>
                                  <cc1:SingleField ID="goout" runat="server" />
                            </td>
                        </tr>
                
                        <tr>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="回程里程數" ></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail" >
                                <cc1:SingleField ID="EndKm" runat="server" />/Km
                            </td>
                            <td align="right" class="BasicFormHeadHead" >
                                <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="入廠保安簽名" ></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleField ID="goback" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan=4 align="center" class="BasicFormHeadHead">
                                 說明：若為個人車輛，請填寫出發及回程里程數，并保安簽名。
                            </td>
                        </tr>
                        </panel>
                    </table>
                </Panel>
            </td>
        </tr> 
        </table>
    </div>
    </form>
</body>
</html>