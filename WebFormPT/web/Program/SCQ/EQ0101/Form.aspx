<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EQ0101_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
             <cc1:SingleField id="SheetNo" runat="server" Display="False"></cc1:SingleField>
     <cc1:SingleField id="Subject" runat="server" Display="False"></cc1:SingleField>
     <table style="width: 800px;" border="0" cellspacing="0" 
             cellpadding="1">
     <tr>
      <td align="center" height="30">
            <font style="font-family: 標楷體; font-size: large;"><b>MES特殊流程申請單</b></font></td>
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
                    tableName="hrusers" 
                    valueIndex="1" idIndex="0" Width="165px" />
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
                    <td style="width:25%" align="left" class="BasicFormHeadHead">申請人職稱</td>
                     <td style="width:25%" align="left" class="BasicFormHeadHead"> 
                         <cc1:SingleField ID="PTitle" runat="server" Width="119px" /></td>
                 </tr>
                </table>
               </td>
             </tr>
              <tr id="ROS1" runat="server">
              <td colspan=4  class="BasicFormHeadHead">
                             <table  width="100%">
                             <tr>
                                 <td colspan=4 align="left" class="BasicFormHeadHead">
                                     <label style="font-size:10px;font-weight:bold;">填寫申請信息</label></td>
                              </tr>
                              <tr>
                                    <td style="width:25%" align="left" class="BasicFormHeadHead">線別</td>
                                    <td style="width:25%" align="left" class="BasicFormHeadHead">
                                      <cc1:SingleField ID="Line" runat="server" Width="119px" />
                                    </td>
                                    <td style="width:25%" align="left" class="BasicFormHeadHead">制令單號</td>
                                    <td style="width:25%" align="left" class="BasicFormHeadHead"> 
                                        <cc1:SingleField ID="Order" runat="server" Width="119px" /></td>
                                </tr>
                                <tr>
                                    <td style="width:25%" align="left" class="BasicFormHeadHead">料號</td>
                                    <td style="width:25%" align="left" class="BasicFormHeadHead">
                                      <cc1:SingleField ID="Feed" runat="server" Width="119px" />
                                    </td>
                                    <td style="width:25%" align="left" class="BasicFormHeadHead">數量</td>
                                    <td style="width:25%" align="left" class="BasicFormHeadHead"> 
                                        <cc1:SingleField ID="Number" runat="server" Width="119px" /></td>
                                </tr>
                                <tr>
                                    <td style="width:25%" align="left" class="BasicFormHeadHead">原因</td>
                                    <td colspan=3 align="center" class="BasicFormHeadHead">
                                      <cc1:SingleField ID="Reason" runat="server" Width="90%" />
                                    </td> 
                                    </tr>
                                    </table>
                     </td>
                </tr>
              <tr>
                 <td colspan=4 align="left" class="BasicFormHeadHead">
                     <label style="font-size:10px;font-weight:bold;">詳細申請信息</label></td>
              </tr>
              <tr>
              <td colspan=4  class="BasicFormHeadHead">

                  <cc1:OutDataList 
                     id="OverTimeList" runat="server" Height="250px" 
                     OnSaveRowData="RequestList_SaveRowData" ViewStateMode="Disabled" Width="100%" 
                     showExcel="True" NoModify="True" IsExcelWithMultiType="True" 
                     showTotalRowCount="True"></cc1:OutDataList>

              </td>
              </tr>
               <tr>
               <td style=" width:25%" class="BasicFormHeadHead">備注說明：</td>
              <td colspan=3  class="BasicFormHeadHead">
                              <cc1:SingleField ID="Note" runat="server" Width="100%" Height="64px" 
                                  MultiLine="True" />
              </td>
              </tr>
              <tr>
              <td  class="BasicFormHeadHead">處理對策：</td>
              <td colspan=3  class="BasicFormHeadHead">

                              <cc1:SingleField ID="Strategy" runat="server" Width="100%" 
                      Height="64px" MultiLine="True" />

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
