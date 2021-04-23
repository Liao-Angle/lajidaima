<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_maintain_SPTS002_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">        
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>年度教育訓練計劃</title>
    <style type="text/css">
    </style>  
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 600px">
        <legend>年度教育訓練年度計劃</legend>
        <table style="margin-left:4px; width: 600px;" border=0 cellspacing=0 cellpadding=1 >  
			<tr><td colspan="4" align="right">
				<cc1:GlassButton ID="TSSchCreateButton" runat="server" Text="產生年度開課計劃" Width="180px" OnClick="CreateButton_Click" ConfirmText="你確定要產生年度開課計劃嗎?" />
				　　　　　
			</td></tr>
            <tr><td width="220px">
                 <cc1:DSCLabel ID="LblCompany" runat="server" Width="200px" 
                        Text="公司別" TextAlign="2" IsNecessary="true"/></td>
                <td width="380px">
                    <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="131px" />
                </td>
				
            </tr>
			<tr><td width="220px">
                 <cc1:DSCLabel ID="LblPlanYear" runat="server" Width="200px" 
                        Text="計劃年度" TextAlign="2" IsNecessary="true"/></td>
                <td width="380px">
                    <cc1:SingleField ID="PlanYear" runat="server" Width="131px" ontextchanged="PlanYear_TextChanged"  />
                    <cc1:DSCLabel ID="LblS1" runat="server" Width="51px" Text="(YYYY)" />
                </td>
            </tr>
			<tr><td width="220px">
                 <cc1:DSCLabel ID="LblProduceSch" runat="server" Width="200px" 
                        Text="已產生年度開課計劃" TextAlign="2" IsNecessary="true"/></td>
                <td width="380px">
                    <cc1:SingleDropDownList ID="ProduceSch" runat="server" Width="131px"  />
                </td>
            </tr>
			<tr><td width="220px">
                 <cc1:DSCLabel ID="LblRemark" runat="server" Width="200px" 
                        Text="備註" TextAlign="2" /></td>
                <td width="380px">
                    <cc1:SingleField ID="Remark" runat="server" Width="180px" />
                </td>
            </tr>
        </table>
    </fieldset>

	<table>  
		<tr><td  width="100%">
            <cc1:DSCTabControl ID="TabPlan" runat="server" Height="120px"   
                Width="620px" PageColor="White">
            <TabPages>
                <cc1:DSCTabPage runat="server" Title="年度教育訓練計劃" Enabled="True" ImageURL="" Hidden="False" ID="DSCTabPage1">
                    <TabBody>
                        <table width="100%">
                            <tr>                                                               
								<td>
						            <%--<asp:FileUpload ID="FuPath" runat="server" Width="396px" />--%>
						            &nbsp;
						            <cc1:DSCLabel ID="lblImportFile" runat="server" style="text-align: left" 
						              Text="檔案名稱" TextAlign="2" Width="72px" />                                     
						            <cc1:SingleField ID="PlanFileName" runat="server" Width="350px" />
						            <cc1:OpenFileUpload ID="FileUploadPlan" runat="server" onaddoutline="Plan_AddOutline" Display="True" />
						            <cc1:GlassButton ID="ButtonImport" runat="server" Height="20px" 
						               onclick="PlanImport_Click" Text="匯入" Width="70px" />
						            <asp:HyperLink ID="HyperLink1" runat="server" 
						              NavigateUrl="/ECP/SmpProgram/Maintain/SPTS002/年度教育訓練計劃匯入範例.xls">取得匯入格式</asp:HyperLink>
					            </td>                                                      
					        </tr>
                            <tr>
								<td>
									<cc1:DataList ID="DataListPlan" runat="server" Height="220px" 
                                    Width="600px" showExcel="True"  />  
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
