<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">        
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>直屬主管維護作業</title>
    <style type="text/css">
    </style>  
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 600px">
        <legend>直屬主管維護作業</legend>
        <table style="margin-left:4px; width: 600px;" border=0 cellspacing=0 cellpadding=1 >  
			<tr><td colspan="4" align="right">
				<cc1:GlassButton ID="TSSchCreateButton" runat="server" Text="修改直屬主管" Width="180px" OnClick="CreateButton_Click" ConfirmText="你確定要修改直屬主管嗎?" />
				　　　　　
			</td></tr>
            <tr><td>
                 <cc1:DSCLabel ID="LblEmployee" runat="server" Width="60px" 
                        Text="員工資料" TextAlign="2" /></td>
                <td>
				    <cc1:SingleOpenWindowField ID="EMPOID" runat="server" 
                    showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                    serialNum="003" tableName="Users" IgnoreCase="True" />			
                    
                </td>
            </tr>
			<tr><td>
                 <cc1:DSCLabel ID="LblManager" runat="server" Width="60px" 
                        Text="直屬主管" TextAlign="2" /></td>
                <td>
				    <cc1:SingleOpenWindowField ID="MANAGEROID" runat="server" 
                    showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
                    serialNum="003" tableName="Users" IgnoreCase="True" />				
                    
                </td>
            </tr>
        </table>
    </fieldset>
   
    </form>
</body>
</html>
