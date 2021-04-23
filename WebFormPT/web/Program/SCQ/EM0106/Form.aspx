<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0106_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
                 <cc1:SingleField id="SheetNo" runat="server" Display="False"></cc1:SingleField>
     <cc1:SingleField id="Subject" runat="server" Display="False"></cc1:SingleField>
     <table style="width: 800px;" border="0" cellspacing="0" 
             cellpadding="1">
     <tr>
      <td align="center" height="30">
            <font style="font-family: 標楷體; font-size: large;"><b>電話傳真掃描複印申請單</b></font></td>
     </tr>
     </table>
        
        <table style="margin-left:4px" border=0 width=800px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="使用者"></cc1:DSCLabel></td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" valueIndex="1" idIndex="0" />
            </td>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBeMail" runat="server" Text="使用者電子郵箱"></cc1:DSCLabel>
            </td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="Email" runat="server" Width="180px" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBDepartment" runat="server" Text="使用者部門"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="PartNo" runat="server" Width="150px" ReadOnly="True" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBDtName" runat="server" Text="使用者職稱"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="DtName" runat="server" Width="100px" ReadOnly="True" />
            </td>
        </tr>
                <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="填單人聯繫電話"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="tbrMobile" runat="server" Width="150px"/>
            </td>
                        <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="使用者聯繫電話"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="syrMobile" runat="server" Width="150px" />
            </td>
            
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBChecked" runat="server" Text="申請內容"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <Panel ID="CheckPanel" runat="server">
                    <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server" Text="傳真"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox2" runat="server" Text="掃描"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox3" runat="server" Text="複印"></cc1:DSCCheckBox></br>
                    <cc1:DSCCheckBox ID="DSCCheckBox4" runat="server" Text="內線"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox5" runat="server" Text="市話"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox6" runat="server" Text="國內長途"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox7" runat="server" Text="國際長途"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox8" runat="server" Text="其他"></cc1:DSCCheckBox>
                </Panel>
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBReason" runat="server" Text="申請原因"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Reason" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
                                <tr runat="server" id="showzg" visible="false">
                            <td class="BasicFormHeadHead"  style="height: 17px">
                                  <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="請選擇簽核主管" Width="150px" Height="20px"></cc1:DSCLabel>
                            </td>
                            <td class="BasicFormHeadHead" style="height: 17px">
                                  <cc1:SingleDropDownList ID="sqzszg" runat="server" Width="111px" />
                            </td>
                        </tr> 
        </table>
    </div>
    </form>
</body>
</html>
