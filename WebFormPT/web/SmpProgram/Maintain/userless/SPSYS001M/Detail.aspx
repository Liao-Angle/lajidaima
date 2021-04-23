<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_Maintain_SPSYS001M_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>表單簽核歷程</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<table border=0 width=100% cellspacing=5 cellpadding=0>
			<tr>
				<td align=center>
					<asp:Image ID="FlowImage" runat="server" />
				</td>
			</tr>
			<tr>
				<td align=center style="font-size:10pt">
					<cc1:DSCLabel ID="DSCLabel8" runat="server" Text="活動狀態:" Width="70px" />
					<span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(138,138,138)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="未傳送" Width="50px" /> 
					<span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(0,153,255)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="處理中" Width="50px" /> 
					<span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(0,128,0)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="已完成" Width="50px" /> 
					<span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(255,0,0)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="已終止" Width="50px" /> 
					<span style="width:16px;height:16px;border-style:solid;border-width:1px;border-color:Black;background-color:rgb(255,255,0)">&nbsp;</span> <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="擱置或未開始" Width="90px" />
				</td>
			</tr>
			<tr>
				<td align=center>
					<asp:Literal ID="OpinionList" runat="server"></asp:Literal>
				</td>
			</tr>
		</table>
    </div>
    </form>
</body>
</html>
