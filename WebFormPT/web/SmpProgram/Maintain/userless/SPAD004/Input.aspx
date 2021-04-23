<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_maintain_SPAD004_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">        
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>國內出差彙總核銷維護畫面-1</title>    
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 1000px; margin-bottom: 0px;">
        <legend>國內出差彙總核銷</legend>
        <table style="margin-left:4px; width: 1000px;" border=0 cellspacing=0 cellpadding=1 >  
			<cc1:SingleField ID="Closed" runat="server" Display="False"/>
            <tr><td width="120px">
                 <cc1:DSCLabel ID="LblCompanyCode" runat="server" Width="120px" 
                        Text="公司別" TextAlign="2" IsNecessary="true"/></td>
                <td width="240px" colspan="2">
                    <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="131px" />
                </td>
                <td width="120px">
                 <cc1:DSCLabel ID="LblOriginatorGUID" runat="server" Width="120px" 
                        Text="承辦人員" TextAlign="2" IsNecessary="true" /></td>
                <td width="380px">
                    <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" 
                    showReadOnlyField="True" Width="280px" guidField="OID" keyField="id" 
                    serialNum="001" tableName="Users" IgnoreCase="True" />
                </td>
                <td width="120px">
                 <cc1:DSCLabel ID="LblSheetNo" runat="server" Width="120px" 
                        Text="單號" TextAlign="2" IsNecessary="False" /></td>
                <td width="380px">
                   <cc1:SingleField ID="SheetNo" runat="server" Width="131px" Visible="True" />
                </td>
            </tr>
            <tr><td width="120px">
                 <cc1:DSCLabel ID="LblBillingDate" runat="server" Width="120px" 
                        Text="彙總請款年月" TextAlign="2" IsNecessary="true"/></td>
                <td width="100px" >
                   <cc1:SingleField ID="BillingDate" runat="server" Width="60px" Visible="True" /></td>
                <td><cc1:DSCLabel ID="LblYearMonth" runat="server" Width="96px" Text="(YYYY-MM)" />
                </td>
                <td width="120px">
                 <cc1:DSCLabel ID="LblDeptGUID" runat="server" Width="120px" 
                        Text="申請部門" TextAlign="2" IsNecessary="True" /></td>
                <td width="380px" >
                   <cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="280px" 
                   showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                   tableName="OrgUnit" IgnoreCase="true"/>
                </td>
                <td width="120px">
                 <cc1:DSCLabel ID="LblTotalAmount" runat="server" Width="120px" 
                        Text="請款總額" TextAlign="2" /></td>
                <td width="380px" >
                   <cc1:SingleField ID="TotalAmount" runat="server" Width="131px" Visible="True" />
                </td>
                
            </tr>
            <tr><td width="120px">
                 <cc1:DSCLabel ID="LblDesc" runat="server" Width="120px" 
                        Text="其他說明" TextAlign="2"/></td>
                <td width="100px" colspan="4">
                   <cc1:SingleField ID="Description" runat="server" Width="650px" Visible="True" />
                </td>
                <td colspan="2" align="center">
                    <cc1:GlassButton ID="SummaryPrint" runat="server" ImageUrl="~/Images/GeneralPrintCertificate.gif" 
                         onbeforeclicks="PrintButton_OnClick" Text="列印國內出差彙總表" Width="160px" 
                        Enabled="True"  />
                </td>
            </tr>
            </table>
    </fieldset>

	<table>        
        <tr><td  width="100%">
            <cc1:DSCTabControl ID="TabPlan" runat="server" Height="120px"   
                Width="100%" PageColor="White">
            <TabPages>
                <cc1:DSCTabPage runat="server" Title="國內出差彙總核銷明細" Enabled="True" ImageURL="" Hidden="False" ID="DSCTabPage1">
                    <TabBody>
                        <table width="100%">
                            <tr>
            <td class="BasicFormHeadDetail">
                <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">
             
	    			<table border=0 cellspacing=0 cellpadding=3>
	                <tr>
	                    <td><cc1:DSCLabel ID="lbCheckUser" runat="server" Width="120px" Text="請選擇使用者" TextAlign="2"/></td>
	                    <td><cc1:SingleOpenWindowField ID="CheckUser" runat="server" 
                                showReadOnlyField="true" guidField="OID" keyField="id" keyFieldType="STRING" serialNum="003" 
                             tableName="Users" Width="300px" idIndex="2" valueIndex="3" IgnoreCase="true" /></td>
                        <td>
                            <cc1:DSCLabel ID="LblCheckDept" runat="server" Text="請選擇部門" Width="120px" TextAlign="2" />
                        </td>
	                    <td>
                            <cc1:SingleOpenWindowField ID="CheckDept" runat="server" guidField="OID" 
                                IgnoreCase="true" keyField="id" serialNum="001" showReadOnlyField="True" 
                                tableName="OrgUnit" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:DSCLabel ID="lbCheckDept" runat="server" Text="出差日起~訖" Width="120px" TextAlign="2" />
                        </td>
                        <td>
                            <cc1:SingleDateTimeField ID="StartDate" runat="server" Width="120px" />
                            &nbsp;<cc1:DSCLabel ID="DSCLabel1" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;
                            <cc1:SingleDateTimeField ID="EndDate" runat="server" Width="120px" />
                        </td>
                        <td colspan="2" align="center">
                            <cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" 
                                OnClick="SearchButton_Click" showWaitingIcon="True" Text="開始查詢" Width="102px" />
                        </td>
                    </tr>

	                </table>
                </cc1:DSCGroupBox>
              </td>
          </tr>
                            <tr><td>                        
                                <cc1:OutDataList ID="DetailList" runat="server" Height="300px" 
                                    Width="1020px" DialogWidth="300" showExcel="True" 
                                    onbeforeopenwindow="DetailList_BeforeOpenWindow" 
                                    onbeforedeletedata="DetailList_BeforeDeleteData" 
                                    onaddoutline="DetailList_AddOutline" ondeletedata="DetailList_DeleteData" />  
                                </cc1:DSCGroupBox> </td>
                                    
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
