<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_maintain_SPTS003_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">        
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>年度開課計劃維護畫面-1</title>
    <style type="text/css">
    </style>  
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 600px">
        <legend>年度開課計劃</legend>
        <table style="margin-left:4px; width: 600px;" border=0 cellspacing=0 cellpadding=1 >  
			<cc1:SingleField ID="Closed" runat="server" Display="False"/>
            <tr><td width="220px">
                 <cc1:DSCLabel ID="LblCompanyCode" runat="server" Width="220px" 
                        Text="公司別" TextAlign="2" IsNecessary="true"/></td>
                <td width="380px">
                    <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="131px" />
                </td>
            </tr>
			<tr><td width="220px">
                 <cc1:DSCLabel ID="LblSchYear" runat="server" Width="220px" 
                        Text="計劃年度" TextAlign="2" IsNecessary="true" /></td>
                <td width="380px">
                    <cc1:SingleField ID="SchYear" runat="server" Width="131px" ontextchanged="SchYear_TextChanged" Visible="True" />
                    <cc1:DSCLabel ID="LblYear1" runat="server" Width="51px" Text="(YYYY)" />
                </td>
            </tr>		
        </table>
    </fieldset>

	<table>        
        <tr><td  width="100%">
            <cc1:DSCTabControl ID="TabPlan" runat="server" Height="120px"   
                Width="620px" PageColor="White">
            <TabPages>
                <cc1:DSCTabPage runat="server" Title="年度開課計劃" Enabled="True" ImageURL="" Hidden="False" ID="DSCTabPage1">
                    <TabBody>
                        <table width="100%">
                            <tr>                                                               
                                <td>
                                    <%--<asp:FileUpload ID="FuPath" runat="server" Width="396px" />--%>
                                    &nbsp;
                                   <cc1:DSCLabel ID="lblImportFile" runat="server" style="text-align: left" 
                                       Text="檔案名稱" TextAlign="2" Width="67px" />                                     
                                   <cc1:SingleField ID="SchFileName" runat="server" Width="350px" />
                                   <cc1:OpenFileUpload ID="FileUploadSch" runat="server" onaddoutline="Sch_AddOutline" Display="True" />
                                   <cc1:GlassButton ID="ButtonImport" runat="server" Height="20px" 
                                        onclick="SchImport_Click" Text="匯入" Width="50px" />
                                        <asp:HyperLink ID="HyperLink1" runat="server" 
                                        NavigateUrl="/ECP/SmpProgram/Maintain/SPTS003/年度開課計劃匯入範例.xls" 
                                        Font-Size="X-Small">取得匯入格式</asp:HyperLink>
                                </td>
                                                              
                            </tr>
                            <tr><td>                        
                                <cc1:DataList ID="SchDetailList" runat="server" Height="220px" 
                                    Width="600px" DialogWidth="600" showExcel="True" 
                                    onbeforeopenwindow="SchDetailList_BeforeOpenWindow" 
                                    onbeforedeletedata="SchDetailList_BeforeDeleteData" />  </td>
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
