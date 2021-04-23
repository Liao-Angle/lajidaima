<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPAD006_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>國外出差預辦證件申請</title>
    <style type="text/css">
    </style>
</head>

<body leftmargin=0 topmargin=0>

<form id="form1" runat="server">
	<div>
	<cc1:SingleField ID="SheetNo" runat="server" Display="False" />
	<cc1:SingleField ID="Subject" runat="server" Display="False" /> 
	
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
		<tr height="40"> 
			<td align=center colSpan="5">
				<font style="font-family: 標楷體; font-size: large;">國外出差預辦證件申請單</font> 
            </td>
		</tr>
	</table>
	
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr height="30">
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
		<tr height="30" align="center">
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="DSCLabel4" runat="server" Text="申請單位" width="60px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel5" runat="server" Text="出差人員" width="60px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel6" runat="server" Text="職稱" width="35px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel7" runat="server" Text="職務代理人" width="75px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel12" runat="server" Text="審核人" width="45px" IsNecessary="true"></cc1:DSCLabel>
            </td>
		</tr>
		<tr height="30" align="center">
			<td class="BasicFormHeadDetail" width="96" >
				<cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="150px" 
					showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
					tableName="OrgUnit" />
			</td>
			<td class="BasicFormHeadDetail"  width="180">
				<cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="150px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3"
                     tableName="Users" 
                    onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton" IgnoreCase="True"/>
			</td>
			<td class="BasicFormHeadDetail" width="100">
				<cc1:SingleField ID="Title" runat="server" Height="20px" width="80px" />
			</td>
			<td class="BasicFormHeadDetail" width="180">
                <cc1:SingleOpenWindowField ID="AgentGUID" runat="server" Width="140px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3" 
                     tableName="Users" onbeforeclickbutton="AgentGUID_BeforeClickButton" IgnoreCase="True" />
			</td>
            <td class="BasicFormHeadDetail" width="180">
				<cc1:SingleOpenWindowField ID="CheckByGUID" runat="server" Width="140px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3" 
                     tableName="Users" onbeforeclickbutton="CheckByGUID_BeforeClickButton"  IgnoreCase="True" />
			</td>
		</tr>
        <tr height="30">
			<td class="BasicFormHeadHead" vAlign="top" width="660" colSpan="5" align="left" >
		        <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="出差（預辦）事由：（說明：如費用由子公司負擔，需經子公司核准）" IsNecessary="true"></cc1:DSCLabel>
			</td>
		</tr>
		<tr height="30">
			<td class="BasicFormHeadDetail" vAlign="top" width="660" colSpan="5" >
                <cc1:SingleField ID="Comment" runat="server" Height="92px" Width="650px" MultiLine="True" />
		    </td>
	    </tr>
	    <tr height="30">
		    <td class="BasicFormHeadHead" colSpan="5"><b>
				<cc1:DSCLabel ID="DSCLabel15" runat="server" Text="A 簽證申請 / 護照申請" Width="400px"></cc1:DSCLabel></b>
            </td>
	    </tr>
	    <tr height="30">
			<td class="BasicFormHeadDetail">
		        <cc1:DSCCheckBox ID="Passport" runat="server" Height="20px" Width="104px" Text="護照"/>
   		    </td>
		    <td class="BasicFormHeadHead" colSpan="4">
				<cc1:DSCLabel ID="DSCLabel17" runat="server" Text="身分證正本，2吋白底彩色相片2張(半年內依護照規格照的相片)，退伍令 (36歲以下之男性)，需7個工作天" Width="500px"></cc1:DSCLabel>
				<br>
				<font size="2" color="#ff0000"> (第一次申辦護照請到戶政單位，先作人別確認，憑證再透過旅行社代辦)</font>
            </td>
	    </tr>
	    <tr height="30">
		    <td class="BasicFormHeadDetail">
                <cc1:DSCCheckBox ID="MTPs" runat="server" Height="20px" Width="100px"  Text="台胞證" />                
		    </td>
		    <td class="BasicFormHeadHead" colSpan="4">
		        <cc1:DSCLabel ID="DSCLabel19" runat="server" Text="護照影本(效期3個月以上)、身份證影本、彩色白底２吋相片１張(比照護照規格 3.2- 3.6 cm 大頭照，  不可穿著白色上衣)、尚未過期台胞證；需5個工作天" Width="500px"></cc1:DSCLabel>
            </td>
	    </tr>
	    <!--<tr height="30">
		    <td class="BasicFormHeadDetail">
                <cc1:DSCCheckBox ID="MtpsPlus" runat="server" Height="20px" Width="100px"  Text="台胞加簽"/>
		    </td>
		    <td class="BasicFormHeadHead" colSpan="4">
                <cc1:DSCLabel ID="DSCLabel21" runat="server" Text="台胞證正本；需4個工作天" Width="500px"></cc1:DSCLabel>
            </td>
	    </tr>-->
	    <tr height="30">
		    <td class="BasicFormHeadDetail">
		        <cc1:DSCCheckBox ID="USvisa" runat="server" Height="20px" Width="120px" Text="美國簽證" />
		    </td>
		    <td class="BasicFormHeadHead" colSpan="4">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="台灣設籍的中華民國國民，持新版晶片護照赴美從事90天以下的商務或觀光旅行，事先透過「旅行許可電子系統」(ESTA)取得授權許可，且沒有其他特殊限制而無法適用者，可免申請B1/B2簽證，直接赴美。「旅行許可」申請網頁：https://esta.cbp.dhs.gov/esta/；需1個工作天" width="500"></cc1:DSCLabel>                                       
		        </td>
	        </tr>
	        <tr>
		        <td class="BasicFormHeadDetail">
					<cc1:DSCCheckBox ID="Other" runat="server" Height="20px" Width="100px" MultiLine="True" Text="其他" />
		        </td>
		        <td class="BasicFormHeadDetail" colSpan="4">
					<cc1:SingleField ID="OtherComment" runat="server" Height="30px" Width="480px" MultiLine="True" />
		        </td>
	        </tr>
	        <tr>
		        <td class="BasicFormHeadHead">
					<cc1:DSCLabel ID="DSCLabel24" runat="server" Text="送件日期：" Width="80px"></cc1:DSCLabel>
				</td>
				<td class="BasicFormHeadDetail">
					<cc1:SingleDateTimeField ID="DeliveryDate" runat="server" DateTimeMode="0"  Height="20px" Width="120px" />
				</td>
				<td class="BasicFormHeadHead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
				<td class="BasicFormHeadHead"> 
					<cc1:DSCLabel ID="DSCLabel26" runat="server" Text="完成日期：" Width="80px"></cc1:DSCLabel>
				</td>
				<td class="BasicFormHeadDetail">
					<cc1:SingleDateTimeField ID="CompleteDate" runat="server" DateTimeMode="0" Height="20px" Width="120px" />
		        </td>
	        </tr>
        </table>
	</div>
</form>

</body>
</html>
