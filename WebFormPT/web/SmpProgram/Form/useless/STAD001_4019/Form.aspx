<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_STAD001_4019_From" %>
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
				<cc1:DSCLabel ID="LblCompanyCode" runat="server" Text="公司別" TextAlign="2" IsNecessary="True" />
            </td>
	        <td class=BasicFormHeadDetail colspan="3">
                <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="206px" />
            </td>
        </tr>

        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="申請人員" Width="96px" 
                    IsNecessary="True" TextAlign="2" />
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
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="EngName" runat="server" Width="200px" />
            </td>
        </tr>

        <tr>
            <td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LblDeptName" runat="server" Text="部門(中)" Width="92px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
				<cc1:SingleField ID="DeptName" runat="server" Width="200px" />              
            </td>
			<td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LblEngDeptName" runat="server" Text="部門(英)" Width="92px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
				<cc1:SingleField ID="EngDeptName" runat="server" Width="200px" />              
            </td>			
        </tr>
         
        <tr>
            <td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LblTitle" runat="server" Text="職稱(中)" Width="85px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
				<cc1:SingleField ID="Title" runat="server" Width="200px" />
            </td>
			<td align="right" class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LblEngTitle" runat="server" Text="職稱(英)" Width="85px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail>
				<cc1:SingleField ID="EngTitle" runat="server" Width="200px" />
            </td>			
        </tr>

        <tr>
			<td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblExt" runat="server" Text="分機" 
                    TextAlign="2" Width="85px" IsNecessary="True"/>
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Ext" runat="server" Width="114px" />
            </td>
			<td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblPhoneNumber" runat="server" Text="行動電話" 
                    TextAlign="2" Width="85px" IsNecessary="True"/>
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="PhoneNumber" runat="server" Width="114px" />
            </td>
            
        </tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEmail" runat="server" IsNecessary="True" 
                    style="margin-bottom: 0px" Text="E-mail" TextAlign="2" Width="85px" />
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Email" runat="server" Width="200px" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblNumberOfApply" runat="server" IsNecessary="True" 
                    Text="申請盒數" TextAlign="2" Width="70px" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="NumberOfApply" runat="server" Width="114px" />
            </td>
        </tr>
		<tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckby" runat="server" IsNecessary="False" 
                    style="margin-bottom: 0px" Text="審核人" TextAlign="2" Width="85px" />
            </td>
            <td class=BasicFormHeadDetail colspan="3">
				<cc1:SingleOpenWindowField ID="CheckbyGUID" runat="server" Width="199px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px"
                    onbeforeclickbutton="CheckbyGUID_BeforeClickButton" idIndex="2" 
                    valueIndex="3" IgnoreCase="True" />                
            </td>
        </tr>
        
        </table>
    </div>
    </form>
</body>
</html>
