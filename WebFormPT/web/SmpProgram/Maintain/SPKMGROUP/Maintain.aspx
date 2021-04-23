<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>KM 群組維護作業</title>
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
	                        <td><cc1:DSCLabel ID="lblGroup" runat="server" Width="90px" Text="KM群組代號："/></td>
	                        <td width="400px"><cc1:SingleOpenWindowField ID="CheckGroup" runat="server" showReadOnlyField="true" 
							     guidField="OID" keyField="id" keyFieldType="STRING" serialNum="001" tableName="KMGroup" Width="540px" />
								 <font size="2" color="#ff0000"> (群組名稱包含KM)</font>
							</td>							
							<td align=right><cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" Text="開始查詢" Width="100px" OnClick="SearchButton_Click" showWaitingIcon="True" /></td>						
	                    </tr>
                    
                    </table>
                    </cc1:DSCGroupBox>
                </td>
            </tr>
			<tr>
                <td width="100%" height="100%">
					
                    <cc1:OutDataList ID="DataList" runat="server" Height="315px" Width="100%" showExcel="True" />
                </td>
            </tr>
            
        </table>    
    </div>
    </form>
</body>
</html>
