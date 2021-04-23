<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_maintain_SPTS007_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">   
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>專業人員名冊</title>
    <style type="text/css">
        .style1
        {
            width: 122px;
        }
    </style>  
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 617px">
        <legend>專業人員名冊</legend> 
        <table style="margin-left:4px; width: 593px;" border=0 cellspacing=0 
            cellpadding=1 >
            <tr><td align="right">                  
                   <cc1:DSCLabel ID="LblCompanyCode" runat="server" Width="90px" Text="公司別" 
                    TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="114px" />
                  </td>
             </tr>
            <tr><td align="right">
                   <cc1:DSCLabel ID="LblEmployeeGUID" runat="server"  Width="90px" Text="員工姓名" 
                       TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleOpenWindowField ID="EmployeeGUID" runat="server" Width="450px"                  
                        showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                        tableName="Users" idIndex="2" valueIndex="3" 
                        FixReadOnlyValueTextWidth="300px" FixValueTextWidth="90px" 
                        onsingleopenwindowbuttonclick="EmployeeGUID_SingleOpenWindowButtonClick" 
                        IgnoreCase="True" /></td>
             </tr>
             <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblDept" runat="server"  Width="90px" Text="部門" TextAlign="2" 
                        IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="450px" 
                            showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                            tableName="OrgUnit" FixReadOnlyValueTextWidth="300px" 
                        FixValueTextWidth="90px" IgnoreCase="True" /></td>                     
            </tr> 
            <tr><td align="right">
                    <cc1:DSCLabel ID="LblOnBoard" runat="server"  Width="90px" Text="到職日" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleDateTimeField ID="OnBoard" runat="server" Width="116px" 
                        style="margin-left: 15px" /></td>  
              
            </tr>  
            <tr><td align="right">
                    <cc1:DSCLabel ID="LblJobTitle" runat="server"  Width="90px" Text="工作職稱" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="JobTitle" runat="server"  Width="422px" /></td>     
              
            </tr>
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblSpecialty" runat="server" Width="90px" Text="專業技能項目" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="Specialty" runat="server" Width="423px" Height="100px" 
                        MultiLine="True" /></td> 
            </tr> 
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblStartYear" runat="server" Width="90px" Text="有效年度(起)" 
                        TextAlign="2" Display="True" IsNecessary="True" /></td> 
	            <td class="style1">
                    <cc1:SingleField ID="StartYear" runat="server" Width="60px" 
                        ontextchanged="StartYear_TextChanged"  />                    
                    <cc1:DSCLabel ID="LblS1" runat="server" Width="51px" Text="(YYYY)" 
                        Height="16px" /></td> 
                 <td align="right">
                    <cc1:DSCLabel ID="LblEndYear" runat="server" Width="84px" Text="有效年度(迄)" 
                         TextAlign="2" style="margin-left: 0px" /></td>                     
	            <td>
                    <cc1:SingleField ID="EndYear" runat="server" Width="60px" 
                        ontextchanged="EndYear_TextChanged"  />
                    <cc1:DSCLabel ID="LblS2" runat="server" Width="51px" Text="(YYYY)" /></td>
            </tr>  
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblRemark" runat="server" Width="90px" Text="備註" 
                        TextAlign="2" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="Remark" runat="server" Width="424px"  /></td> 
            </tr>      
           
        </table>
    </fieldset>  
   
    </form>
</body>
</html>
