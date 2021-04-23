<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EM0101_Form" %>

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


        <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>  
        <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
        <cc1:SingleField ID="Subject" runat="server" Display="False" />          

        <tr valign=middle > 
		    <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>EMail需求申請單</b></font></td>
	    </tr>
	</table>
        <table style="margin-left:4px" border=0 width=800px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工" Width="100%"></cc1:DSCLabel></td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="hrusers" OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" valueIndex="1" idIndex="0" />
            </td>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBDepartment" runat="server" Text="部門" Width="100%"></cc1:DSCLabel>
            </td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="Department" runat="server" Width="150px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBDtName" runat="server" Text="職稱" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="DtName" runat="server" Width="100px" ReadOnly="True" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBExtension" runat="server" Text="分機" Width="100%"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Extension" runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBeMail" runat="server" Text="電子郵箱" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="eMail1" runat="server" Width="58px" />
                 &nbsp;_ <cc1:SingleField ID="eMail2" runat="server" Width="58px" />
                @<cc1:SingleField ID="eMail3" runat="server" Width="120px" ReadOnly="True" 
                    ValueText="simplo.com.cn" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBTypeID" runat="server" Text="申請理由" Width="100%"></cc1:DSCLabel>
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="TypeID" 
                    runat="server" Width="110px" /> 
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBChecked" runat="server" Text="郵件群組" Width="100%"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <Panel ID="CheckPanel" runat="server">
                    <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server" Text="助理組"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox11" runat="server" Text="主任組"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox2" runat="server" Text="部級主管組"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox3" runat="server" Text="處級主管組"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox4" runat="server" Text="PushMail"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox5" runat="server" Text="WebMail"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox6" runat="server" Text="Cost Center Head"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox7" runat="server" Text="經副理級以上主管"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox8" runat="server" Text="陸籍副理級以上幹部"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox9" runat="server" Text="台籍幹部"></cc1:DSCCheckBox>
                    <cc1:DSCCheckBox ID="DSCCheckBox10" runat="server" Text="對外權限" 
                    ForeColor="Red"></cc1:DSCCheckBox>
                </Panel>
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBNote" runat="server" Text="備註" Width="100%"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Note" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
