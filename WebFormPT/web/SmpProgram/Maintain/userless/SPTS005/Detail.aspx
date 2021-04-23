<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_maintain_SPTS005_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>上課學員</title>
    <style type="text/css">       
        #form1
        {
            width: 544px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 540px">
        <legend>上課學員</legend> 
        <table style="margin-left:4px; width: 525px;" border=0 cellspacing=0 
            cellpadding=1 >
            <tr><td align="right">                    
                   <cc1:DSCLabel ID="LblEmployeeGUID" runat="server" Text="學員姓名" TextAlign="2" Width="75px" /> </td>
	            <td colspan="3">
                    <cc1:SingleOpenWindowField ID="EmployeeGUID" runat="server" Width="411px"                  
                        showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                        tableName="Users" idIndex="2" valueIndex="3" 
                        FixReadOnlyValueTextWidth="280px" FixValueTextWidth="90px" 
                        onsingleopenwindowbuttonclick="EmployeeGUID_SingleOpenWindowButtonClick" 
                        IgnoreCase="True" /></td>
             </tr>
             <tr>
                <td align="right">
                   <cc1:DSCLabel ID="LblDept" runat="server" Text="部門" TextAlign="2" Width="75px" /> </td>
	            <td colspan="3">
                    <cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="413px" 
                            showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                            tableName="OrgUnit" FixReadOnlyValueTextWidth="280px" 
                        FixValueTextWidth="90px" IgnoreCase="True" /></td>                     
            </tr>       
            <tr>
                <td align="right">
                     <cc1:DSCLabel ID="LblApplyWay" runat="server" Text="報名方式" TextAlign="2" Width="75px" /> </td>
	            <td>
                    <cc1:SingleDropDownList ID="ApplyWay" runat="server" Width="90px" /> </td> 
                <td align="right">
                     <cc1:DSCLabel ID="LblAttendance" runat="server" Text="是否出席" TextAlign="2" 
                         Width="77px" /> </td>
	            <td>
                    <cc1:SingleDropDownList ID="Attendance" runat="server" Width="90px" /> </td>               
            </tr>
            <tr>
                <td align="right">
                     <cc1:DSCLabel ID="LblGetCertificate" runat="server" Text="取得證書" TextAlign="2" Width="75px" /> </td>
	            <td>
                    <cc1:SingleDropDownList ID="GetCertificate" runat="server" Width="90px" /> </td> 
                <td align="right">
                     <cc1:DSCLabel ID="LblCertificateNo" runat="server" Text="證書號碼" TextAlign="2" Width="75px" /> </td>
	            <td>
                    <cc1:SingleField ID="CertificateNo" runat="server" Width="90px" /></td>               
            </tr>  
            <tr><td align="right">
                     <cc1:DSCLabel ID="LblFee" runat="server" Text="費用" TextAlign="2" Width="75px" /> </td>
	            <td>
                    <cc1:SingleField ID="Fee" runat="server" Width="90px" Fractor="2" 
                        isMoney="True" ValueText="0" /></td>     
                <td align="right">
                     <cc1:DSCLabel ID="LblSign" runat="server" Text="簽訂訓練合約" TextAlign="2" 
                         Width="90px" /> </td>
	            <td>
                    <cc1:SingleDropDownList ID="Sign" runat="server" Width="90px" /> </td> 
            </tr>
            <tr>
                <td align="right">
                     <cc1:DSCLabel ID="LblExpire" runat="server" Text="合約到期日" TextAlign="2" Width="75px" /> </td>
	            <td>
                    <cc1:SingleDateTimeField ID="Expire" runat="server" Width="117px" /></td>
                <td align="right">
                     <cc1:DSCLabel ID="LblPass" runat="server" Text="通過狀態" TextAlign="2" Width="75px" /> </td>
	            <td>
                    <cc1:SingleDropDownList ID="Pass" runat="server" Width="90px" /> </td>      
            </tr>             
           
        </table>
    </fieldset>  
    </form>
</body>
</html>
