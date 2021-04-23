<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_STIP001_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>資訊需求申請單</title>
    <style type="text/css">
    </style>
</head>

<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>  
        <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
        <cc1:SingleField ID="Subject" runat="server" Display="False" />          

        <tr valign=middle > 
		    <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>資訊需求申請單</b></font></td>
	    </tr>
	</table>
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr height="30">
			<td class="BasicFormHeadHead" colspan="4">
				<font size="2" face="Arial"><b>申請人資料 </b>(請填寫實際申請人基本資料)</font>
			</td>
		</tr>
        <tr valign="middle" height="30">
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel1" runat="server" Text="公司" Width="35px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel3" runat="server" Text="工號" Width="35px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel8" runat="server" Text="申請人(中)" Width="65px" IsNecessary="true"></cc1:DSCLabel>
            </td>
		    <td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel9" runat="server" Text="申請人(英)" Width="65px"  IsNecessary="true"></cc1:DSCLabel>
            </td>
		</tr>
        <tr valign="middle" height="30" align="center">
			<td class=BasicFormHeadDetail>
				<cc1:SingleDropDownList ID="Company" runat="server" Width="150px" Font-Strikeout="False" />
            </td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleField ID="OriginatorNumber" runat="server" Height="20px" 
                            width="150px" ontextchanged="OriginatorNumber_SingleFieldButtonClick" />
            </td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleField ID="OriginatorCName" runat="server" Height="20px" width="150px"/>
            </td>
			<td class=BasicFormHeadDetail>
                <cc1:SingleField ID="OriginatorEName" runat="server" Height="20px" width="150px" />
            </td>
		</tr>
		<tr valign="middle" height="30">
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel2" runat="server" Text="部門" Width="35px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel10" runat="server" Text="職稱" Width="35px" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel11" runat="server" Text="審核人" Width="45px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel121" runat="server" Text="申請人分機" Width="75px" IsNecessary="true"></cc1:DSCLabel>
            </td>				        
		</tr>
		<tr valign="middle" height="30" align="center">
			<td class=BasicFormHeadDetail>
				<cc1:SingleOpenWindowField ID="OriginatorDeptGUID" runat="server" Width="150px" 
                  showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                  tableName="OrgUnit" />
			</td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleField ID="Title" runat="server" Height="20px" width="150px" />
            </td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleOpenWindowField ID="CheckByGUID" runat="server" Width="150px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                     tableName="Users"  />
            </td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleField ID="Extension" runat="server" Height="20px" width="150px" />
			</td>				        
		</tr>
		<tr>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="資訊需求內容清單" Width="660px" DialogHeight="460" IsNecessary="true">
				&nbsp;&nbsp;
				<asp:Label ID="LBRequestType" runat="server" Text="需求類別" Width="60px"></asp:Label>
				<cc1:SingleDropDownList ID="RequestType" runat="server" Width="150px" 
                     onselectchanged="RequestType_SelectChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LBRequestItem" runat="server" Text="需求項目" Width="60px"></asp:Label>
                <cc1:SingleDropDownList ID="RequestItem" runat="server" Width="200px" 
                     onselectchanged="RequestItem_SelectChanged"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
                &nbsp;&nbsp;&nbsp;&nbsp;<font color="red"><cc1:DSCLabel ID="lbRequestTips" runat="server" Text="" Width="550px"></cc1:DSCLabel></font><br>
            	&nbsp;&nbsp;<asp:Label ID="LBRequestDesc" runat="server" Text="需求說明" Width="60px"></asp:Label>
                <cc1:SingleField ID="RequestDesc" runat="server" align="right" style="margin-right: 0px" Width="400px" />
                <br />
                <cc1:OutDataList ID="RequestList" runat="server" Height="227px" 
                     OnSaveRowData="RequestList_SaveRowData" OnShowRowData="RequestList_ShowRowData" 
                     ViewStateMode="Disabled" Width="650px" />
                </cc1:DSCGroupBox>                          
			</td>
		</tr>
        <tr height="30">
			<td class="BasicFormHeadHead" colSpan="4"><b><font size="2">
				<asp:Label ID="Label1" runat="server" Text="使用者需求說明" Width="120px" IsNecessary="true"></asp:Label></font></b>
			</td>
		</tr>
        <tr>
			<td class="BasicFormHeadDetail" colspan="4">
				<cc1:SingleField ID="UserDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
			</td>
		</tr>
        <tr height="30">
			<td class="BasicFormHeadHead" colSpan="4"><font size="2">
				<b><asp:Label ID="Label2" runat="server" Text="MIS處理說明" Width="120px"></asp:Label></b></font>
                <br />
            </td>
		</tr>
        <tr height="30">
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:SingleField ID="MisDesc" runat="server" Height="92px" Width="650px" MultiLine="True" />
            </td>
		</tr>
        <tr valign="middle" height="30" align="center" >
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel22" runat="server" Text="會簽人員1" Width="80px"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="DSCLabel23" runat="server" Text="會簽人員2" Width="80px"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="DSCLabel25" runat="server" Text="MIS承辦人" Width="80px"></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadHead">
				<cc1:DSCLabel ID="DSCLabel27" runat="server" Text="預計完成日" Width="80px"></cc1:DSCLabel>
            </td>
		</tr>
		<tr valign="middle" align="center" Height="30px">
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="Countersign1GUID" runat="server" Width="150px"
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                     tableName="Users"  />
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="Countersign2GUID" 
                     runat="server" Width="150px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                     tableName="Users"  /> 
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleOpenWindowField ID="MisOwnerGUID" runat="server" Width="150px" 
                     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                     tableName="Users"  /> 
			</td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleDateTimeField ID="EstimateCompleteDate" runat="server" Width="150px" />
			</td>
		</tr>   
	</table>

    </div>       
</form>

</body>
</html>
