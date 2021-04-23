<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPAD007_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title>國外出差差旅費報銷申請單</title>
    <style type="text/css">
    </style>
</head>

<body>
<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1><tr><td>
<form id="form1" runat="server">

    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>
	    <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
	    <cc1:SingleField ID="Subject" runat="server" Display="False" /> 

        <tr height="40" align="center"> 
			<td><font style="font-family: 標楷體; font-size: large;"><b>國外出差差旅費報銷單</b></font></td>
	    </tr>
	</table>

    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr>
			<td colSpan="5" align="center" Width="100%" class="BasicFormHeadHead"><b>				
				<cc1:GlassButton ID="PrintButton1" runat="server" Height="20px" Width="100%" 
                    Text="列印國外出差差旅費報銷單" onbeforeclicks="PrintButton_OnClick" Enabled="True" /></b>
			</td>
		</tr>
	    <tr height="40">
		    <td class="BasicFormHeadHead" colSpan="5">
				<font size="2" face="Arial"><b>基本資料</b></font>
			</td>
		</tr>
		<tr height="30">
			<td align="left" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="LblCompanyCode" runat="server" Text="公司別" Width="85px" />
	        </td>
			<td colspan=4 class=BasicFormHeadDetail>
				<cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="138px"  />
	        </td>
		</tr>
		<tr height="30" align="center" class="BasicFormHeadHead">
			<td class="BasicFormHeadHead"><cc1:DSCLabel ID="lbDeptName" runat="server" Text="申請單位" IsNecessary="true" width="60px"></cc1:DSCLabel></td>
			<td class="BasicFormHeadHead"><cc1:DSCLabel ID="lbOriginator" runat="server" Text="出差人員" IsNecessary="true" width="60px"></cc1:DSCLabel></td>
			<td class="BasicFormHeadHead"><cc1:DSCLabel ID="lbTitle" runat="server" Text="職稱" IsNecessary="true" width="35px"></cc1:DSCLabel></td>
			<td class="BasicFormHeadHead"><cc1:DSCLabel ID="lbAgent" runat="server" Text="職務代理人" IsNecessary="true" width="75px"></cc1:DSCLabel></td>
			<td class="BasicFormHeadHead"><cc1:DSCLabel ID="lbCheckBy" runat="server" Text="審核人"  width="45px"></cc1:DSCLabel></td>
		</tr>
		<tr height="30" align="center">
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="150px" 
				     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="OrgUnit" />
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="150px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003"  
                     tableName="Users" 
					 onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
					 idIndex="2" valueIndex="3" onbeforeclickbutton="OriginatorGUID_BeforeClickButton" IgnoreCase="True"/>
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleField ID="Title" runat="server" Height="20px" width="80px" />
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="AgentGUID" runat="server" Width="140px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                     tableName="Users" 
					 idIndex="2" valueIndex="3" onbeforeclickbutton="AgentGUID_BeforeClickButton"  IgnoreCase="True"/>
			</td>
            <td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="CheckByGUID" runat="server" Width="140px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                     tableName="Users" 
					 idIndex="2" valueIndex="3" onbeforeclickbutton="CheckByGUID_BeforeClickButton" IgnoreCase="True" />
			</td>
		</tr>
		<tr height="30">
			<td align="left" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbOriForeignForm" runat="server" Text="勾稽出差申請單號" Width="130px" IsNecessary="true"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4" >
				<cc1:SingleOpenWindowField ID="OriForeignForm" runat="server" Width="500px" 
				     showReadOnlyField="True" guidField="OID" keyField="SheetNo" serialNum="001" 
                     tableName="SPAD007" 
					 onbeforeclickbutton="OriForeignForm_BeforeClickButton"
                    onsingleopenwindowbuttonclick="OriForeignForm_SingleOpenWindowButtonClick"  />                
			</td>			
		</tr>
		<tr height="30">
			<td align="left" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbTrvlDate" runat="server" Text="預計出差日期" Width="100px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4" >
				<cc1:SingleDateTimeField ID="StartTrvlDate" runat="server" Width="140px" />&nbsp;&nbsp;
                <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
				<cc1:SingleDateTimeField ID="EndTrvlDate" runat="server" Width="140px" />				
			</td>
		</tr>
		<tr height="30">
		    <td align="left" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbChgTrvlDate" runat="server" Text="異動後出差日期" Width="120px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4"> 	
				<cc1:SingleDateTimeField ID="ChgStartTrvlDate" runat="server" Width="140px" />&nbsp;&nbsp;
                <cc1:DSCLabel ID="DSCLabel018" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
				<cc1:SingleDateTimeField ID="ChgEndTrvlDate" runat="server" Width="140px" />
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadHead">
		        <cc1:DSCLabel ID="lbTrvlSite" runat="server" Text="出差地點" Width="70px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:DSCCheckBox ID="SiteUs" runat="server" Height="20px" Width="50px" Text="美國"/>
				<cc1:SingleField ID="SiteUsDesc" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="SiteJp" runat="server" Height="20px" Width="50px" Text="日本"/>
				<cc1:SingleField ID="SiteJpDesc" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="SiteKr" runat="server" Height="20px" Width="50px" Text="韓國"/>
				<cc1:SingleField ID="SiteKrDesc" runat="server" Height="20px" Width="50px" />&nbsp;<br>
				<cc1:DSCCheckBox ID="SiteSub" runat="server" Height="20px" Width="65px" Text="子公司"/>
				<cc1:SingleField ID="SiteSubDesc" runat="server" Height="20px" Width="50px" />&nbsp;
				<cc1:DSCCheckBox ID="SiteOther" runat="server" Height="20px" Width="50px" Text="其他"/>
				<cc1:SingleField ID="SiteOtherDesc" runat="server" Height="20px" Width="50px" />
			</td>
		</tr>						
	    <tr height="30">				
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbFeeCharge1" runat="server" Text="費用負擔" Width="100px" />
	        </td>        
	        <td class="BasicFormHeadDetail" colSpan="4">
	            <cc1:DSCRadioButton ID="FeeCharge1" runat="server" Text="新普" Checked="true" />&nbsp;
	            <cc1:DSCRadioButton ID="FeeCharge2" runat="server" Text="新世" />&nbsp;
	            <cc1:DSCRadioButton ID="FeeCharge3" runat="server" Text="新普(重慶)" />&nbsp;
				<cc1:DSCRadioButton ID="FeeCharge5" runat="server" Text="中普" />&nbsp;
				<cc1:DSCRadioButton ID="FeeCharge6" runat="server" Text="太普" />
	        </td>				
	    </tr>
	    <tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5">
		        <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="出差（預辦）事由：（說明：如費用由子公司負擔，需經子公司核准）" Width="500px"></cc1:DSCLabel>
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadDetail" colSpan="5">
                <cc1:SingleField ID="TrvlDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
		    </td>
	    </tr>
        <tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5">
				<cc1:DSCLabel ID="DSCLabel20" runat="server" Text="異動或取消說明" Width="500px"></cc1:DSCLabel>
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadDetail" colSpan="5">
                <cc1:SingleField ID="ChgTrvlDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
		    </td>
	    </tr>	    
		
        <tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5">
				<cc1:DSCLabel ID="DSCLabel10" runat="server" Text="C 預支申請" Width="100px"></cc1:DSCLabel>
            </td>
	    </tr>
	    <tr height="30">
            <td  class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel22" runat="server" Text="原預支申請" Width="80px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:DSCCheckBox ID="PrePayTwd" runat="server" Height="20px" Width="70px" Text="新台幣"/>
				<cc1:SingleField ID="PrePayTwdAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;
				<cc1:DSCCheckBox ID="PrePayCny" runat="server" Height="20px" Width="70px" Text="人民幣"/>
				<cc1:SingleField ID="PrePayCnyAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;
				<cc1:DSCCheckBox ID="PrePayUsd" runat="server" Height="20px" Width="70px" Text="美金"/>
				<cc1:SingleField ID="PrePayUsdAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;
				<cc1:DSCCheckBox ID="PrePayJpy" runat="server" Height="20px" Width="70px" Text="日圓"/>
				<cc1:SingleField ID="PrePayJpyAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;
				<cc1:DSCCheckBox ID="PrePayEur" runat="server" Height="20px" Width="70px" Text="歐元"/>
				<cc1:SingleField ID="PrePayEurAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;	
				<cc1:DSCCheckBox ID="PrePayOther" runat="server" Height="20px" Width="70px" Text="其他"/>
				<cc1:SingleField ID="PrePayOtherAmt" runat="server" Height="20px" Width="50px"  />
		    </td>
		</tr>
        <tr>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbChgPrePay" runat="server" Text="異動後預支申請" Width="120px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:DSCCheckBox ID="ChgPrePayTwd" runat="server" Height="20px" Width="70px" Text="新台幣"/>
				<cc1:SingleField ID="ChgPrePayTwdAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;
				<cc1:DSCCheckBox ID="ChgPrePayCny" runat="server" Height="20px" Width="70px" Text="人民幣"/>
				<cc1:SingleField ID="ChgPrePayCnyAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;
				<cc1:DSCCheckBox ID="ChgPrePayUsd" runat="server" Height="20px" Width="70px" Text="美金"/>
				<cc1:SingleField ID="ChgPrePayUsdAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;
				<cc1:DSCCheckBox ID="ChgPrePayJpy" runat="server" Height="20px" Width="70px" Text="日圓"/>
				<cc1:SingleField ID="ChgPrePayJpyAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;
				<cc1:DSCCheckBox ID="ChgPrePayEur" runat="server" Height="20px" Width="70px" Text="歐元"/>
				<cc1:SingleField ID="ChgPrePayEurAmt" runat="server" Height="20px" Width="50px" isMoney="True" />&nbsp;
				<cc1:DSCCheckBox ID="ChgPrePayOther" runat="server" Height="20px" Width="70px" Text="其他"/>
				<cc1:SingleField ID="ChgPrePayOtherAmt" runat="server" Height="20px" Width="50px" />
		    </td>
		</tr>
		<tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5">
				<cc1:DSCLabel ID="lbBillingList" runat="server" Text="差旅費報銷明細列表" Width="200px"></cc1:DSCLabel>
            </td>
	    </tr>
		<tr height="30">
		    <td align="left" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lbActualTrvlDate" runat="server" Text="實際出差日期" Width="100px" IsNecessary="true"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail" colSpan="4"> 	
				<cc1:SingleDateTimeField ID="ActualStartTrvlDate" runat="server" Width="140px" />&nbsp;&nbsp;
                <cc1:DSCLabel ID="lbActrualTrvlDate11" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;
				<cc1:SingleDateTimeField ID="ActualEndTrvlDate" runat="server" Width="140px" />
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadDetail" colSpan="5">
				<cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="差旅費報銷明細清單" Width="660px" DialogHeight="460" IsNecessary="true">
				&nbsp;&nbsp;
				<asp:Label ID="LBDate" runat="server" Text="日期" Width="60px"></asp:Label>
				<cc1:SingleDateTimeField ID="OccurDate" runat="server" Width="128px"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LBType" runat="server" Text="類別" Width="26px"></asp:Label>
                <cc1:SingleDropDownList ID="PayClass" runat="server" Width="77px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Label ID="LBAmount" runat="server" Text="金額" Width="36px"></asp:Label>
                <cc1:SingleField ID="OccurAmt" runat="server" Width="78px" alignRight />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Label ID="LBCurrency" runat="server" Text="幣別" Width="28px" Height="16px"></asp:Label>
                <cc1:SingleDropDownList ID="OccurCurrency" runat="server" Width="60px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
            	&nbsp;&nbsp;
				<asp:Label ID="LBDesc" runat="server" Text="摘要" Width="60px"></asp:Label>
                <cc1:SingleField ID="OccurDesc" runat="server" align="right" style="margin-right: 0px" Width="400px" />
                <br />
                <cc1:OutDataList ID="RequestList" runat="server" Height="227px" 
                     OnSaveRowData="RequestList_SaveRowData" OnShowRowData="RequestList_ShowRowData" 
                     ViewStateMode="Disabled" Width="650px" showExcel="True" showSerial="True" 
                        isShowAll="True" />
                </cc1:DSCGroupBox>                          
			</td>
		</tr>

        <tr height="30">
            <td colspan=5>
                <table style="margin-left:4px; width: 675px;" border=0 cellspacing=0 
                    cellpadding=0  align="center">
			        <tr class="BasicFormHeadHead" align="center">
                        <td align="center"><asp:Label ID="Label6" runat="server" Text="總計" width="30"></asp:Label></td>
				        <!--<td align="center"><asp:Label ID="LabelSum" runat="server" Text="總計" width="30"></asp:Label></td>-->
				        <td align="center"><asp:Label ID="LabelTWD" runat="server" Text="台幣" width="30"></asp:Label></td>
				        <td align="center"><asp:Label ID="LabelUSD" runat="server" Text="美金" width="30"></asp:Label></td>
				        <td align="center"><asp:Label ID="LabelJPY" runat="server" Text="日幣" width="30"></asp:Label></td>
				        <td align="center"><asp:Label ID="LabelKRW" runat="server" Text="韓幣" width="30"></asp:Label></td>
				        <td align="center"><asp:Label ID="LabelCNY" runat="server" Text="人民幣" width="45"></asp:Label></td>
						<td align="center"><asp:Label ID="LabelMR" runat="server" Text="馬幣" width="30"></asp:Label></td>
				        <td align="center"><asp:Label ID="LabelOu" runat="server" Text="歐元" width="30"></asp:Label></td>
				        <td align="center"><asp:Label ID="LabelOTH" runat="server" Text="其他" width="30"></asp:Label></td>
			        </tr>
			        <tr>
				        <td class="BasicFormHeadHead" align="center"><asp:Label ID="Label1EnterAmt" runat="server" Text="金額"></asp:Label></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayTw" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayUs" runat="server" Height="20px" Width="50px" 
                                isMoney="True" Fractor="2" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayJp" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayKr" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayCn" runat="server" Height="20px" Width="50px" 
                                isMoney="True" Fractor="2" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayMa" runat="server" Height="20px" Width="50px" 
                                isMoney="True" Fractor="2" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayOu" runat="server" Height="20px" Width="50px" 
                                isMoney="True" Fractor="2" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayOther" runat="server" Height="20px" Width="50px" /></td>
			        </tr>
			        <tr>
				        <td align="center" class="BasicFormHeadHead"><asp:Label ID="Label1" runat="server" Text="換算NT"></asp:Label></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayTwTw" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayUsTw" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayJpTw" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayKrTw" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayCnTw" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayMaTw" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayOuTw" runat="server" Height="20px" Width="50px" 
                                isMoney="True" /></td>
				        <td class="BasicFormHeadDetail">
			            <cc1:SingleField ID="ActualPayOtherTw" runat="server" Height="20px" Width="50px" /></td>
			        </tr>
		        </table>

            </td>
        </tr>
        <tr height="30">
            <td colspan="5">
                <table style="margin-left:4px; width: 667px;" border=0 cellspacing=0 
                    cellpadding=0>
                    <tr>
		                <td class="BasicFormHeadHead"><asp:Label ID="Label2" runat="server" Text="總金額"></asp:Label></td>
                        <td class="BasicFormHeadDetail"><cc1:SingleField ID="ActualAmount" runat="server" 
                                Height="20px" Width="60px" isMoney="True" /></td>
                        <td class="BasicFormHeadHead"><asp:Label ID="Label3" runat="server" Text="應付金額"></asp:Label></td>
                        <td class="BasicFormHeadDetail"><cc1:SingleField ID="ActualApAmt" runat="server" 
                                Height="20px" Width="60px" isMoney="True" /></td>
                        <td class="BasicFormHeadHead"><asp:Label ID="Label4" runat="server" Text="預支還回"></asp:Label></td>
                        <td class="BasicFormHeadDetail"><cc1:SingleField ID="ActualRtnApAmt" runat="server" 
                                Height="20px" Width="60px" /></td>
                        <td class="BasicFormHeadHead"><asp:Label ID="Label5" runat="server" Text="還回日期"></asp:Label></td>
                        <td class="BasicFormHeadDetail"><cc1:SingleDateTimeField ID="ActualRtnApDate" runat="server" Height="20px" Width="100px" /></td>
	                </tr>
                </table>
            </td>
        </tr>

	    <tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5">
				<cc1:DSCLabel ID="DSCLabel1" runat="server" Text="財務註記" Width="500px"></cc1:DSCLabel>
			</td>
		</tr>
      	<tr height="30">
			<td class="BasicFormHeadDetail" colSpan="5">
                <cc1:SingleField ID="FinDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
		    </td>
	    </tr>
        <tr height="30">
			<td class="BasicFormHeadDetail" colSpan="5"><b><font color="#FF0000">
                ** 依照『營利事業所得稅查核準則』規定,國外出差費報銷,應保留飛機票票根或登機證票根及電子機票 **</font></b>
		    </td>
	    </tr>
        <%--</tr>
	    <tr height="30">
		    <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbActrualGetDate" runat="server" Text="實際領取日" Width="80px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail">
                <cc1:SingleDateTimeField ID="ActualGetDate" runat="server" DateTimeMode="0" Height="20px" Width="120px" />
			</td>
			<td class="BasicFormHeadHead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="領取人" Width="80px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadDetail">
                <cc1:SingleOpenWindowField ID="GetMemberGUID" runat="server" Width="140px" 
                        showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                        tableName="Users"  IgnoreCase="True"/>                    
			</td>
	    </tr>--%>			
    </table>

</form>
</td></tr></table>
</body>
</html>
