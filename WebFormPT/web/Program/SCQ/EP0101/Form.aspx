<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EP0101_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
             <cc1:SingleField id="SheetNo" runat="server" ></cc1:SingleField>
   	     <cc1:SingleField id="Subject" runat="server" ></cc1:SingleField>
     <table style="width: 800px;" border="0" cellspacing="0" 
             cellpadding="1">
     <tr>
      <td align="center" height="30">
            <font style="font-family: 標楷體; font-size: large;"><b>Layout申請單</b></font></td>
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
                    <td align="left" class="BasicFormHeadHead">申請人</td>
                    <td align="left" class="BasicFormHeadHead">
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server"
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" valueIndex="1" idIndex="0" Width="165px" />
                    </td>
                     <td align="left" class="BasicFormHeadHead">申請人分機</td>
                    <td align="left" class="BasicFormHeadHead">

            <cc1:SingleField ID="EmpMobile" runat="server" Width="119px" />

                    </td>

                </tr>
                <tr>
                    <td style="width:25%" align="left" class="BasicFormHeadHead">申請人部門</td>
                    <td style="width:25%" align="left" class="BasicFormHeadHead">
                      <cc1:SingleField ID="Depart" runat="server" Width="119px" />
                    </td>
                    <td style="width:25%" align="left" class="BasicFormHeadHead">申請日期</td>
                     <td style="width:25%" align="left" class="BasicFormHeadHead"> 

            <cc1:SingleField ID="SDate" runat="server" Width="119px" />

                         </td>
                 </tr>
                </table>
               </td>
             </tr>
             <tr>
               <td colspan=4 align="left" class="BasicFormHeadHead">
                <label style="font-size:10px;font-weight:bold;">申請具體內容</label></td>
            </tr>
            <tr>
                    <td style="width:20%" align="left" class="BasicFormHeadHead">需求主題   </td>
                    <td style="width:30%" align="left" colspan=3 class="BasicFormHeadHead">
                      <cc1:SingleField ID="Title" runat="server" Width="100%" />
                    </td>
           </tr>
                       <tr>
                    <td style="width:20%" align="left" class="BasicFormHeadHead">需求目的   </td>
                    <td style="width:30%" align="left" colspan=3 class="BasicFormHeadHead">
                <cc1:SingleField ID="aim" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
                    </td>
           </tr>
                       <tr>
                    <td  align="left" class="BasicFormHeadHead">擬定規劃區域   </td>
                    <td align="left" colspan=3 class="BasicFormHeadHead">
                      <cc1:SingleField ID="Area" runat="server" Width="100%" />
                    </td>
           </tr>
                       <tr>
                    <td  align="left" class="BasicFormHeadHead">規劃具體要求   </td>
                    <td align="left" colspan=3 class="BasicFormHeadHead">
                <cc1:SingleField ID="ask" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
                    </td>
           </tr>
           <tr id="TR1" runat=server>
               <td colspan=4 align="left" class="BasicFormHeadHead">
                <label style="font-size:10px;font-weight:bold;">設備清單</label></td>
            </tr>
               <tr id="TR2" runat=server>
                    <td  align="left" class="BasicFormHeadHead">名稱</td>
                    <td  align="left" class="BasicFormHeadHead">
                      <cc1:SingleField ID="Name" runat="server" Width="119px" />
                    </td>
                    <td align="left" class="BasicFormHeadHead">數量</td>
                     <td  align="left" class="BasicFormHeadHead"> 
                         <cc1:SingleField ID="Number" runat="server" Width="119px" /></td>
                 </tr>
                                 <tr id="TR3" runat=server>
                    <td  align="left" class="BasicFormHeadHead">規格</td>
                    <td  align="left" class="BasicFormHeadHead">
                      <cc1:SingleField ID="version" runat="server" Width="119px" />
                    </td>
                    <td align="left" class="BasicFormHeadHead">備註</td>
                     <td  align="left" class="BasicFormHeadHead"> 
                         <cc1:SingleField ID="Note" runat="server" Width="119px" /></td>
                 </tr>
                 <tr>
              <td colspan=4  class="BasicFormHeadHead">
                  <cc1:OutDataList 
                     id="OverTimeList" runat="server" Height="250px" 
                     OnSaveRowData="RequestList_SaveRowData" ViewStateMode="Disabled" Width="100%" 
                     NoModify="True" IsExcelWithMultiType="True" 
                     showTotalRowCount="True"></cc1:OutDataList>
              </td>
              </tr>
               <tr>
                    <td  align="left" class="BasicFormHeadHead">預計完工日期</td>
                    <td  align="left" class="BasicFormHeadHead">
                        <label style="font-size:10px;font-weight:bold;">
                                <cc1:SingleDateTimeField ID="YWorkDate" runat="server" 
                                    Width="120px" />  
                        （IE單位填寫）</label>
                    </td>
                    <td align="left" class="BasicFormHeadHead">實際完工日期 </td>
                     <td style="width:30%" align="left" class="BasicFormHeadHead"> 
                         <label style="font-size:10px;font-weight:bold;">
                                <cc1:SingleDateTimeField ID="SWorkDate" runat="server" 
                                    Width="120px" />  
                         （施工單位填寫）</label>
                         </td>
                 </tr>
                                <tr>
                    <td  align="left" class="BasicFormHeadHead">方案預估 (RMB)</td>
                    <td  align="left" class="BasicFormHeadHead">
                      <cc1:SingleField ID="Money" runat="server" Width="119px" />元
                    </td>
                    <td align="left" class="BasicFormHeadHead">施工單位 </td>
                     <td  align="left" class="BasicFormHeadHead"> 
                  <cc1:SingleDropDownList ID="sgdw" runat="server" Width="111px" />
                                    </td>
                 </tr>
                                <tr runat="server" id="showzg" visible="false">
            <td class="BasicFormHeadHead" colspan="3" style="height: 17px">
          <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="請選擇簽核主管" Width="150px" Height="20px"></cc1:DSCLabel>
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
