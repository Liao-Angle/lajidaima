<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPKM003_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">        
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>群組人員維護作業</title>
    <style type="text/css">
    </style>  
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 430px">
        <legend>群組人員維護作業</legend>
		<cc1:SingleField ID="OID" runat="server" Display="False" />
		<cc1:SingleField ID="objectVersion" runat="server" Display="False" />
		<cc1:SingleField ID="description" runat="server" Display="False" /> 
		
        <table style="margin-left:4px; width: 330px;" border=0 cellspacing=0 cellpadding=1 >  
            <tr>
				<td><cc1:DSCLabel ID="LblCompanyName" runat="server" Width="100px" Text="公司別" TextAlign="2" /></td>
                <td><cc1:SingleField ID="CompanyName" runat="server" Width="200px" /></td>
            </tr>
			<tr>
				<td><cc1:DSCLabel ID="LblgroupId" runat="server" Width="100px" Text="群組代號" TextAlign="2" /></td>
                <td><cc1:SingleField ID="id" runat="server" Width="200px" /></td>				
            </tr>
			<tr>
				<td><cc1:DSCLabel ID="LblgroupName" runat="server" Width="100px" Text="群組名稱" TextAlign="2" /></td>
                <td><cc1:SingleField ID="groupName" runat="server" Width="200px" /></td>				
            </tr>
        </table>
    </fieldset>
    <table>   
         <tr><td align="right">
             <cc1:DSCLabel ID="LblUserOID" runat="server" Width="60px" Text="群組人員" TextAlign="2" /></td>
            <td>
                <cc1:SingleOpenWindowField ID="UserOID" runat="server" 
                    Width="358px" showReadOnlyField="True" guidField="OID" keyField="id" 
                    serialNum="001" tableName="Users" FixReadOnlyValueTextWidth="200px" 
                    FixValueTextWidth="120px" Display="True" IgnoreCase="True" />
			</td>
        </tr> 
        <tr><td colspan="4">
            <cc1:OutDataList ID="GroupDetailList" runat="server" height="300px" width="430px" 
                CertificateMode="True" onsaverowdata="GroupDetailList_SaveRowData" onshowrowdata="GroupDetailList_ShowRowData" />
            </td>                           
        </tr>
        </table> 
    <div>
    </div>
    </form>
</body>
</html>
