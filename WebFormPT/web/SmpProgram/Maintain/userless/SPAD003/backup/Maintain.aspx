<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>直屬主管維護作業</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=3 cellpadding=0 width=100% style="font-size:9pt">
            <tr>
                <td width=100% >
                    <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">
                    <table border=0 cellspacing=0 cellpadding=3>
	                    <tr>
	                        <td><cc1:DSCLabel ID="lblEmployee" runat="server" Width="90px" Text="員工："/></td>
	                        <td width="400px">
								<cc1:SingleOpenWindowField ID="EmployeeOID" runat="server" 
			                    showReadOnlyField="True" Width="480px" guidField="OID" keyField="id" 
			                    serialNum="001" tableName="Users" IgnoreCase="True" />	
								
								 <font size="2" color="#ff0000"> (變更直屬主管人員)</font>
							</td>							
							<td align=right><cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" Text="開始查詢" Width="100px" OnClick="SearchButton_Click" showWaitingIcon="True" /></td>						
	                    </tr>
                    
                    </table>
                    </cc1:DSCGroupBox>
                </td>
            </tr>
			<tr>
                <td width="100%" height="100%">					
                    <cc1:DataList ID="DataList" runat="server" Height="315px" Width="100%" showExcel="True" />
                </td>
            </tr>
            
        </table>    
    </div>
    </form>
</body>
</html>
