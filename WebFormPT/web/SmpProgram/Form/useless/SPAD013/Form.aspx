<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPAD013_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title>文具用品申請單</title>
    <style type="text/css">
    </style>
</head>

<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>  
        <tr valign=middle > 
		    <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>文具用品申請單</b></font></td>
	    </tr>
	</table>
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>		
	    <tr>
	        <td align="right" class="BasicFormHeadHead" title="單號">
	            <cc1:DSCLabel ID="LblSheetNo" runat="server" Text="單號" Width="85px" TextAlign="2" />
	        </td>
			<td colspan=3 class=BasicFormHeadDetail>
	            <cc1:SingleField ID="SheetNo" runat="server" Width="120px" ReadOnly="True" />
	        </td>
		</tr>	
		<tr>
	        <td align="right" class="BasicFormHeadHead" title="主旨">
	            <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="85px" IsNecessary="False" TextAlign="2" />
	        </td>
			<td colspan=3 class=BasicFormHeadDetail>
	            <cc1:SingleField ID="Subject" runat="server" Width="300px" />
	        </td>
		</tr>
        <tr valign="middle">
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="lblCompany" runat="server" Text="公司" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="lblDept" runat="server" Text="部門" IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="lblOriginator" runat="server" Text="申請人"  IsNecessary="true"></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="lblCheckby" runat="server" Text="審核人"></cc1:DSCLabel>
            </td>
		</tr>
        <tr valign="middle" align="center">
			<td class=BasicFormHeadDetail>
				<cc1:SingleDropDownList ID="Company" runat="server" Width="150px" Font-Strikeout="False" />
            </td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="150px" 
                  showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                  tableName="OrgUnit" />				  
			</td>
			<td class=BasicFormHeadDetail>					
				<cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
                    idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton" IgnoreCase="True"/>	
            </td>	
			<td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckByGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    onsingleopenwindowbuttonclick="CheckByGUID_SingleOpenWindowButtonClick" 
                    idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="CheckByGUID_BeforeClickButton" IgnoreCase="True"/>
            </td>			
		</tr>
		<tr valign="middle">			
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="lblTtlAmount" runat="server" Text="預計金額" ></cc1:DSCLabel>
            </td>
			<td class="BasicFormHeadHead" align="center">
				<cc1:DSCLabel ID="lblDueDate" runat="server" Text="預計購入日期" ></cc1:DSCLabel>
			</td>
			<td class="BasicFormHeadHead" align="center"><cc1:DSCLabel ID="DSCLabelText01" runat="server" Text="  " ></cc1:DSCLabel></td>
			<td class="BasicFormHeadHead" align="center"><cc1:DSCLabel ID="DSCLabelText02" runat="server" Text="  " ></cc1:DSCLabel></td>
		</tr>
		<tr valign="middle" align="center">			
			<td class=BasicFormHeadDetail>
				<cc1:SingleField ID="TotalAmount" runat="server" Height="20px" width="120px" />
            </td>
			<td class="BasicFormHeadDetail">
				<cc1:SingleDateTimeField ID="DueDate" runat="server" Width="120px" />
			</td>
			<td class=BasicFormHeadDetail><cc1:DSCLabel ID="DSCLabel01" runat="server" Text="  " ></cc1:DSCLabel></td>
			<td class=BasicFormHeadDetail><cc1:DSCLabel ID="DSCLabel02" runat="server" Text="  " ></cc1:DSCLabel></td>
		</tr>
		<tr>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="文具申請清單" Width="700px" DialogHeight="460" IsNecessary="true">
				<table style="margin-left:4px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
					<tr valign="middle" align="center">
						<td class="BasicFormHeadHead"><asp:Label ID="lblStationeryGUID" runat="server" Text="品名規格" Width="120px"></asp:Label></td>
						<td class="BasicFormHeadHead"><asp:Label ID="LBQuantity" runat="server" Text="數量" Width="80px"  ></asp:Label></td>
						<td class="BasicFormHeadHead"><asp:Label ID="lblUserDesc" runat="server" Text="用途(使用人)" Width="80px"></asp:Label></td>
						<td class="BasicFormHeadHead"><asp:Label ID="lblOtherDesc" runat="server" Text="備註" Width="80px"></asp:Label></td>
					</tr>
					<tr valign="middle" align="center">
						<td class="BasicFormHeadDetail">
							<cc1:SingleOpenWindowField ID="StationeryGUID" runat="server" 
							Width="300px" showReadOnlyField="True" guidField="GUID" keyField="ProdDesc" 
		                    serialNum="001" tableName="Stationery" 
							FixValueTextWidth="120px" FixReadOnlyValueTextWidth="20px" 
							onbeforeclickbutton="StationeryGUID_BeforeClickButton" />
						</td>							
						<td class="BasicFormHeadDetail">
							<cc1:SingleField ID="Quantity" runat="server" align="right" style="margin-right: 0px" Width="60px" isAccount="True" isMoney="true"/>
						</td>							
						<td class="BasicFormHeadDetail">
							<cc1:SingleField ID="UserDesc" runat="server" align="left" style="margin-right: 0px" Width="150px" />							 
						</td>
						<td class="BasicFormHeadDetail">
							<cc1:SingleField ID="OtherDesc" runat="server" align="left" Width="150px" />
						</td>
					</tr>
				</table>
				<font color="red"><cc1:DSCLabel ID="lbRequestTips" runat="server" Text="" Width="550px"></cc1:DSCLabel></font>
				<br>
				<cc1:OutDataList ID="RequestList" runat="server" Height="150px" 
                     OnSaveRowData="RequestList_SaveRowData" OnShowRowData="RequestList_ShowRowData" 
                     ViewStateMode="Disabled" Width="700px" EnableTheming="True"  
					 onaddoutline="RequestList_AddOutline" ondeletedata="RequestList_DeleteData"  />
                </cc1:DSCGroupBox> 
			</td>
		</tr>
        <tr >
			<td class="BasicFormHeadHead" colSpan="4"><b><font size="2">
				<asp:Label ID="lblReqDesc" runat="server" Text="使用者需求說明" Width="120px" IsNecessary="true"></asp:Label></font></b>
			</td>
		</tr>
        <tr>
			<td class="BasicFormHeadDetail" colspan="4">
				<cc1:SingleField ID="ReqDesc" runat="server" Height="50px" Width="700px" MultiLine="True" />
			</td>
		</tr>
        <tr>
			<td class="BasicFormHeadHead" colSpan="4"><font size="2">
				<b><asp:Label ID="lblHrDesc" runat="server" Text="管理部處理說明" Width="120px"></asp:Label></b></font>
                <br />
            </td>
		</tr>
        <tr>
			<td class="BasicFormHeadDetail" colSpan="4">
				<cc1:SingleField ID="HrDesc" runat="server" Height="50px" Width="700px" MultiLine="True" />
            </td>
		</tr>
          
	</table>

    </div>       
</form>

</body>
</html>
