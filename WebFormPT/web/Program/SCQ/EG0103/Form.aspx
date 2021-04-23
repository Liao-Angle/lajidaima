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
         <cc1:SingleField id="SheetNo" runat="server" Display="False"></cc1:SingleField>
     <cc1:SingleField id="Subject" runat="server" Display="False"></cc1:SingleField>
     <table style="width: 800px;" border="0" cellspacing="0" 
             cellpadding="1">
     <tr>
      <td align="center" height="30">
            <font style="font-family: 標楷體; font-size: large;"><b>派車申請單</b></font></td>
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
                    OnSingleOpenWindowButtonClick="RequestID_SingleOpenWindowButtonClick" 
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
                <label style="font-size:10px;font-weight:bold;">申請內容</label></td>
        </tr>
        <tr>
            <td width=120px align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LBEmployeeID" runat="server" Text="用車人"></cc1:DSCLabel></td>
            <td width="280px" align="left" class="BasicFormHeadDetail">
                <cc1:SingleField ID="CName" runat="server" Width="150px" />
            </td>
            <td width=120px align="right" class="BasicFormHeadHead">
               <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="可否拼車" />
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
                                <cc1:SingleDropDownList ID="Department" runat="server" 
                                     Width="200px" 
                    Height="16px"  />
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
                <cc1:SingleDateTimeField ID="DepartureTime" runat="server" Width="137px" 
                    DateTimeMode="3" />
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
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="派車方式"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:SingleDropDownList ID="LeaveTypeID" runat="server" 
                                     Width="89px"  />
                            </td>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="用車類型"></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleDropDownList ID="Genre" runat="server" Width="114px" OnSelectChanged="Genre_SelectChanged" />
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
                                <label style="font-size:10px;font-weight:bold;">調度室填寫</label></td>
                        </tr>
                        <tr>
                            <td align="right" class="BasicFormHeadHead" width=118px;>
                                <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="司機姓名"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail" width="276px">
                                <cc1:SingleField ID="UserName" runat="server" Width="144px" />
                            </td>
                            <td align="right" class="BasicFormHeadHead" width=118px;>
                                <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="車牌號碼"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail" width="276px">
                                <cc1:SingleField ID="Plate" runat="server" Width="111px"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="聯繫電話"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail">
                                <cc1:SingleField ID="UserMobile" runat="server" Width="136px"/>
                            </td>
                            <td align="right" class="BasicFormHeadHead">
                                &nbsp;</td>
                            <td class=BasicFormHeadDetail>
                                &nbsp;</td>
                        </tr>
                      <panel id="kmasp" runat="server" visible="false">
                        <tr>
                            <td align="right" class="BasicFormHeadHead" >
                                  <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="出發里程數" ></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail">
                                  <cc1:SingleField ID="StartKm" runat="server" Width="124px" />/Km
                            </td>
                            <td align="right" class="BasicFormHeadHead">
                                  <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="出廠保安簽名" ></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadDetail>
                                  <cc1:SingleField ID="goout" runat="server" Width="111px" />
                            </td>
                        </tr>
                
                        <tr>
                            <td align="right" class="BasicFormHeadHead">
                                <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="回程里程數" ></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadDetail" >
                                <cc1:SingleField ID="EndKm" runat="server" Width="114px" />/Km
                            </td>
                            <td align="right" class="BasicFormHeadHead" >
                                <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="入廠保安簽名" ></cc1:DSCLabel>
                            </td>
                            <td class=BasicFormHeadDetail>
                                <cc1:SingleField ID="goback" runat="server" Width="122px"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan=4 align="center" class="BasicFormHeadHead">
                                 說明：若為個人車輛，請填寫出發及回程里程數，并保安簽名。
                            </td>
                        </tr>
                        </panel>
                    </table>
            </td>
        </tr> 
        </table>
    </div>
    </form>
</body>
</html>