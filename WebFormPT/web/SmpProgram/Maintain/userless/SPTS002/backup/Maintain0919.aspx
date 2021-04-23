<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>年度教育訓練計劃查詢1</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=3 cellpadding=0 width=100% style="font-size:9pt">
            <tr>
                <td width=100% >
                    <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">
                    <table border=0 cellspacing=0 cellpadding=5>
	                    <tr>
	                        <td><cc1:DSCLabel ID="lblCompanyCode" runat="server" Width="150px" Text="公司別：" TextAlign="2"/></td>
	                        <td width="100px">
								<cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="90px" TextAlign="1" />	
							</td>
							<td><cc1:DSCLabel ID="lblPlanYear" runat="server" Width="150px" Text="計劃年度："  TextAlign="2"/></td>
	                        <td width="100px">
								<cc1:SingleField ID="PlanYear" runat="server" Width="90px" TextAlign="1" />	
							</td>							
							<td align=right><cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" Text="開始查詢" Width="100px" OnClick="SearchButton_Click" showWaitingIcon="True" /></td>						
	                    </tr>
						<tr>
	                        <td><cc1:DSCLabel ID="lblProduceSch" runat="server" Width="150px" Text="已產生年度開課計劃："  TextAlign="2"/></td>
	                        <td width="100px">
								<cc1:SingleDropDownList ID="ProduceSch" runat="server" Width="90px" TextAlign="1" />	
							</td>
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
