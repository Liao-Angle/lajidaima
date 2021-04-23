<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_maintain_SPTS002_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">        
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>課程主檔</title>
    <style type="text/css">
    </style>  
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 600px">
        <legend>課程主檔</legend>
        <table style="margin-left:4px; width: 600px;" border=0 cellspacing=0 cellpadding=1 >  
            <tr><td width="220px">
                 <cc1:DSCLabel ID="LblCompany" runat="server" Width="200px" 
                        Text="公司別" TextAlign="2" IsNecessary="true"/></td>
                <td width="380px">
                    <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="131px" onselectchanged="CompanyCode_SelectChanged" />
                </td>				
            </tr>			
        </table>
    </fieldset>

	<table>  
		<tr><td  width="100%">
            <cc1:DSCTabControl ID="TabPlan" runat="server" Height="120px"   
                Width="620px" PageColor="White">
            <TabPages>
                <cc1:DSCTabPage runat="server" Title="課程主檔" Enabled="True" ImageURL="" Hidden="False" ID="DSCTabPage1">
                    <TabBody>
                        <table width="100%">                            
                            <tr>
								<td>
									<cc1:DataList ID="DataListSubject" runat="server" Height="220px" 
                                    Width="600px" showExcel="True" 
                                        onbeforeopenwindow="DataListSubject_BeforeOpenWindow" 
                                        onbeforedeletedata="DataListSubject_BeforeDeleteData"  />  
								</td>
                            </tr>							
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
			</TabPages>
            </cc1:DSCTabControl>            
            </td></tr> 
		</table>
    </form>
</body>
</html>
