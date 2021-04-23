<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_System_Form_STAD003_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>人員需求申請單</title>
     <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <cc1:SingleField id="SheetNo" runat="server" Display="False"></cc1:SingleField>
     <cc1:SingleField id="Subject" runat="server" Display="False"></cc1:SingleField><br />
       <cc1:DSCGroupBox ID="DSCGroupBox2" runat="server" Text="申請信息" Width="720px">
        <table border="1">
        <tr>
            <td class="BasicFormHeadDetail" colspan="3" align="center" height="40">
            <font style="font-family: 標楷體; font-size: large;"><b>ST&nbsp;人&nbsp;員&nbsp;需&nbsp;求&nbsp;申&nbsp;請&nbsp;單</b></font></td>
        </tr>
        
        <tr>
        <td class="BasicFormHeadDetail" style="width: 241px; height: 28px;">
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="申請者" Width="52px"/>
            &nbsp;
            
              <cc1:SingleOpenWindowField ID="APMONEY00SQR" runat="server" Width="179px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px"
                    idIndex="2" 
                    valueIndex="3" IgnoreCase="True" OnSingleOpenWindowButtonClick="APMONEY00SQR_SingleOpenWindowButtonClick" />
        <%--    
            <cc1:SingleOpenWindowField ID="APMONEY00SQR" runat="server" guidField="OID" keyField="id"
               serialNum="003" showReadOnlyField="True" tableName="Users" idIndex="2" valueIndex="3"
               IgnoreCase="True" Width="198px" />--%>
        </td>
        <td class="BasicFormHeadDetail" style="width: 168px; height: 28px">
         <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="公司" Width="33px"></cc1:DSCLabel>
        <cc1:SingleDropDownList ID="SingleDropDownListGS" runat="server" Width="130px" />
        </td>
        <td  class="BasicFormHeadDetail" style="height: 28px; width: 274px;">
        <cc1:DSCLabel ID="DSCLabel20" runat="server" Text="部門" Width="31px"></cc1:DSCLabel>
            &nbsp;<cc1:SingleField ID="SingleFieldBMMC" runat="server" Width="201px" />
        </td>
        
        </tr>
        <tr>
        <td class="BasicFormHeadDetail" style="height: 26px; width: 241px;">
            &nbsp;
            <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="申請日期" Width="67px"></cc1:DSCLabel>
             <cc1:DSCLabel ID="DSCLabelDateTime" runat="server" Text="1" Width="162px" Enabled="False"></cc1:DSCLabel>
        </td>
        <td class="BasicFormHeadDetail" style="height: 26px; width: 168px;">
            &nbsp;
         <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="現有編制" Width="67px"></cc1:DSCLabel>
            <cc1:SingleField ID="SingleFieldXyrs" runat="server" Width="62px" />
        </td>
        <td class="BasicFormHeadDetail" style="height: 26px; width: 274px;">
            &nbsp;
          <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="預定錄用日期" Width="90px" Height="27px"></cc1:DSCLabel>
             <cc1:SingleDateTimeField ID="SingleDateTimeFieldYdateTime" runat="server" Width="140px" />
         </td>
         </tr>
         <tr>
         <td class="BasicFormHeadDetail" style="width: 241px; height: 27px">
             &nbsp;&nbsp;
         <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="類別" Width="54px" Height="21px"></cc1:DSCLabel>
         <cc1:SingleDropDownList ID="SingleDropDownListLb" runat="server" Width="167px" />
          
         </td>
         <td class="BasicFormHeadDetail" style="height: 27px; width: 168px;">
             &nbsp;
         <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="需求人數" Width="67px"></cc1:DSCLabel>
         <cc1:SingleField ID="SingleFieldRs" runat="server" Width="64px" />
         </td>
         <td class="BasicFormHeadDetail" style="height: 27px; width: 274px;">
             &nbsp;
         <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="年齡" Width="39px" Height="21px"></cc1:DSCLabel>
             <cc1:SingleField ID="SingleFieldAgeA" runat="server" Width="33px" />
               <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="~" Width="19px"></cc1:DSCLabel>
             <cc1:SingleField ID="SingleFieldAgeB" runat="server" Width="44px" />
         
         </td>
         </tr>
         <tr>
         <td class="BasicFormHeadDetail" colspan="3" style="height: 43px">
            <cc1:DSCLabel ID="DSCLabel10"  Width="72px" Height="44px" runat="server" Text="需求原因" 
       >  </cc1:DSCLabel>
         <cc1:SingleField ID="SingleFieldXqyy" runat="server"
          Width="622px" Height="41px" MultiLine="True" />
            
            
         </td>
    
         </tr>
         <tr>
         <td class="BasicFormHeadDetail" colspan="3" style="height: 27px">
          
            <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="職位需求" 
            Width="72px" Height="40px">  </cc1:DSCLabel>
                <cc1:SingleField ID="SingleFieldZwxq" runat="server" MultiLine="True" Width="624px" />
            
         </td>
         </tr>
         <tr>
         <td class="BasicFormHeadDetail" style="height: 21px; width: 241px;">
          <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="學歷" Width="69px" Height="21px"></cc1:DSCLabel>
             <cc1:SingleDropDownList ID="SingleDropDownListXl" runat="server" Width="161px" />
            
         </td>
         <td class="BasicFormHeadDetail" style="height: 21px; width: 168px;">
         <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="性別" Width="39px"></cc1:DSCLabel>
             <cc1:DSCRadioButton ID="DSCRadioButtonSexMen" runat="server" Text="男" GroupName="sex" />
             <cc1:DSCRadioButton ID="DSCRadioButtonSexWoman" runat="server" Text="女" GroupName="sex" />
               <cc1:DSCRadioButton ID="DSCRadioButtoSexUnlimited" runat="server" Text="不拘" GroupName="sex" Checked="true" />
             
         </td>
         <td class="BasicFormHeadDetail" style="height: 21px; width: 274px;">
         <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="兵役" Width="39px"></cc1:DSCLabel>
          <cc1:DSCRadioButton ID="DSCRadioButton1" runat="server" Text="役畢" GroupName="fy" />
          <cc1:DSCRadioButton ID="DSCRadioButton2" runat="server" Text="不拘" GroupName="fy" Checked="true" />
         </td>
         </tr>
        
         <tr>
         <td class="BasicFormHeadDetail" colspan="3" style="height: 34px">
           <cc1:DSCLabel ID="DSCLabel17" runat="server" Text="科系" Width="66px" Height="37px"></cc1:DSCLabel>
             <cc1:SingleField ID="SingleFieldKx" runat="server" MultiLine="True" Width="631px" />
           
         </td>
         </tr>
         <tr>
         <td class="BasicFormHeadDetail" colspan="3" style="height: 66px">
          <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="具體技能與人格特質" Width="199px"></cc1:DSCLabel>
             <br />
             <cc1:SingleField ID="SingleFieldJtjnrgtz" runat="server" MultiLine="True" Width="700px" />
          
         </td>
         </tr>
         <tr>
         <td class="BasicFormHeadDetail" colspan="3" style="height: 49px">
          <cc1:DSCLabel ID="DSCLabel19" runat="server"
           Text="工作職務說明(技朮人員以上需填寫)" Width="241px"></cc1:DSCLabel>
           <br />
           <cc1:SingleField ID="SingleFieldGzzwsm" runat="server" MultiLine="True" Width="698px" />
         
         
         </td>
         </tr>
        
        
        </table>
       
       </cc1:DSCGroupBox></div>
    </form>
</body>
</html>
