<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPAD008_Form" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />  
    <style type="text/css">
        .style1
        {
            border-left: 1px none rgb(188,178,147);
            border-right: 1px solid rgb(188,178,147);
            border-top: 1px none rgb(188,178,147);
            border-bottom: 1px solid rgb(188,178,147);
            background-color: white;
            font-size: 9pt;
            font-family: Arial;
            width: 115px;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
    <cc1:SingleField ID="Subject" runat="server" Display="False" /> 
    <cc1:SingleField ID="EngTitle" runat="server" Display="False" />
    <cc1:SingleField ID="EngDeptName" runat="server" Display="False" /> 
     
    <div>
        <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <tr><td align=center>&nbsp;</td>
        </tr>
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>名&nbsp;片&nbsp;申&nbsp;請&nbsp;單</b></font></td>
        </tr>
        </table>

        <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>         
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <asp:Label ID="LblCompanyCode" runat="server" Text="公司別"></asp:Label>
            </td>
	        <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="206px" />
            </td>
        </tr>

        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="申請人員" Width="96px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="199px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    
                    
                    onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton" idIndex="2" 
                    valueIndex="3" IgnoreCase="True" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEngName" runat="server" Text="英文姓名" Width="64px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="EngName" runat="server" Width="200px" />
            </td>
        </tr>

        <tr>
            <td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LblDeptName" runat="server" Text="部門" Width="92px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleOpenWindowField ID="DeptName" runat="server" DisabledCleanValue="False" 
                    guidField="ME001" keyField="ME002" 
                    serialNum="002" showReadOnlyField="True" tableName="OrgUnit" 
                    Width="540px" dialogHeight="450" FixReadOnlyValueTextWidth="300px" 
                    FixValueTextWidth="210px" guidIndex="1" idIndex="2" valueIndex="3" 
                    IgnoreCase="True" />
            </td>			 
        </tr>
         
        <tr>
            <td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LblTitle" runat="server" Text="職稱" Width="85px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleOpenWindowField ID="Title" runat="server" 
                    FixReadOnlyValueTextWidth="300px" FixValueTextWidth="210px" guidField="ME001" 
                    keyField="ME002" serialNum="002" showReadOnlyField="True" tableName="Title" 
                    Width="540px" IgnoreCase="True" />
            </td>			
        </tr>

        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEmail" runat="server" IsNecessary="False" 
                    style="margin-bottom: 0px" Text="E-mail" TextAlign="2" Width="85px" />
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="Email" runat="server" Width="538px" />
                </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblExt" runat="server" IsNecessary="False" Text="分機" 
                    TextAlign="2" Width="85px" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Ext" runat="server" Width="114px" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblPhoneNumber" runat="server" IsNecessary="False" Text="行動電話" 
                    TextAlign="2" Width="85px" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="PhoneNumber" runat="server" Width="114px" />
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblFax" runat="server" IsNecessary="False" Text="傳真號碼" 
                    TextAlign="2" Width="85px" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Fax" runat="server" Width="114px" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblNumberOfApply" runat="server" IsNecessary="False" 
                    Text="申請盒數" TextAlign="2" Width="70px" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="NumberOfApply" runat="server" Width="114px" />
            &nbsp; <font size="2">(一盒300張)</font></td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblAddress" runat="server" IsNecessary="False" Text="廠區地址" 
                    TextAlign="2" Width="85px" />
            </td>
	        <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="Address" runat="server" Width="538px" />
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDeliveryDate" runat="server" Text="名片稿回覆日期" Width="102px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=style1>
                <cc1:SingleDateTimeField ID="DeliveryDate" runat="server" Width="140px" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCompleteDate" runat="server" Text="名片預計送達日期" Width="118px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="CompleteDate" runat="server" Width="140px" />
            </td>
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSpecialRequire" runat="server" IsNecessary="False" 
                    style="margin-bottom: 0px" Text="申請人特殊要求" TextAlign="2" Width="107px" />
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="SpecialRequire" runat="server" Width="538px" 
                    Height="50px" MultiLine="True" ReadOnly="False" />
                </td>
        </tr>
         <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginatorComment" runat="server" IsNecessary="False" 
                    style="margin-bottom: 0px" Text="申請人校稿意見" TextAlign="2" Width="107px" />
            </td>
            <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleField ID="OriginatorComment" runat="server" Width="538px" 
                    Height="50px" MultiLine="True" ReadOnly="False" />
                </td>
        </tr>
                      
         <tr>
	        <td colspan=4 class=BasicFormHeadDetail>
                <font size="2">****註: 申請人校槁發現有誤，請於申請人校稿意見欄說明。</font></td>
        </tr>
                      
         <tr>
	        <td colspan=4 class=BasicFormHeadDetail>
                <font size="2">****註: 名片配送為每週一次。</font><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                人事單位於星期三中午12:00前校稿完成，可於下星期二收到名片；反之，需再加長一週才可收到名片。</td>
        </tr>
                      
        </table>
    </div>
    </form>
</body>
</html>
