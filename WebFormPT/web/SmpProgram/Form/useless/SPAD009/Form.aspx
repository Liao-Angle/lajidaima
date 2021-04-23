<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPAD009_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>國內出差單</title>
    <style type="text/css">
    </style>
</head>

<body leftmargin=0 topmargin=0>

<form id="form1" runat="server">
    <cc1:SingleField ID="TempSerialNo" runat="server" />
	<cc1:SingleField ID="Hours" runat="server" />
	<cc1:SingleField ID="ClassCode" runat="server" />
	<cc1:SingleField ID="CategoryCode" runat="server" />
	<cc1:SingleField ID="IsIncludeHoliday" runat="server" />
	<cc1:DSCCheckBox ID="IsHrForm" runat="server" />	
    <cc1:SingleField ID="PayeeFlag" runat="server" />
	<cc1:SingleField ID="RefEtagFee" runat="server" />
   
    
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <%--<tr><td align=center><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/smp-logo.jpg"/></td></tr>--%>
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>國內出差單</b></font></td>
        </tr>
    </table>
	<table border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr>
			<td colSpan="5" class="BasicFormHeadHead" Width="100%"><b>				
				<cc1:GlassButton ID="PrintButton" runat="server" Height="20px" Width="300" 
                    Text="列印國內出差單" onbeforeclicks="PrintButton_OnClick" Enabled="True" /></b>
			</td>
		</tr>
	</table>	<BR>
	
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr>
	        <td align="right" class="BasicFormHeadHead" title="單號">
	            <cc1:DSCLabel ID="LblSheetNo" runat="server" Text="單號" Width="85px" TextAlign="2" />
	        </td>
			<td colspan=5 class=BasicFormHeadDetail>
	            <cc1:SingleField ID="SheetNo" runat="server" Width="100px" ReadOnly="True" />
				<font size="2" color="#ff0000" >
				<cc1:DSCLabel ID="LblOtherDesc" runat="server" Text="" width="480px" /></cc1:DSCLabel>
				</font>
	        </td>
		</tr>	
		<tr>
	        <td align="right" class="BasicFormHeadHead" title="主旨">
	            <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="85px" IsNecessary="False" TextAlign="2" />
	        </td>
			<td colspan=5 class=BasicFormHeadDetail>
	            <cc1:SingleField ID="Subject" runat="server" Width="435px" />
	        </td>
		</tr>
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCompanyCode" runat="server" Text="公司別" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="138px"  />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDeptID" runat="server" Text="部門" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="200px" 
				     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
					 tableName="OrgUnit"  IgnoreCase="true" />
            </td>
        </tr>
        <tr>			
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="出差人員" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
                    idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton" IgnoreCase="true" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbCheckby" runat="server" Text="審核人" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckByGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    onsingleopenwindowbuttonclick="CheckByGUID_SingleOpenWindowButtonClick" 
                    idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="CheckByGUID_BeforeClickButton" IgnoreCase="true" />
            </td>
        </tr>   
		<tr>			
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblTripDate" runat="server" Text="出差日期" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=5 class="BasicFormHeadDetail">
				<cc1:SingleDateTimeField ID="TripDate" runat="server" Width="140px" ondatetimeclick="TripDate_DateTimeClick" />
            </td>
        </tr>     		
        <tr>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblStartTime" runat="server" Text="起始時間" Width="75px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
				<cc1:SingleDateTimeField ID="StartTime" runat="server" Width="140px" 
                    ondatetimeclick="StartTime_DateTimeClick" DateTimeMode="4" />
            </td>
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEndTime" runat="server" Text="截止時間" Width="75px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail colspan=3>
				<cc1:SingleDateTimeField ID="EndTime" runat="server" Width="140px" 
                    ondatetimeclick="EndTime_DateTimeClick" DateTimeMode="4" />
            </td>
        </tr>
        <tr>

	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblIsTripFee" runat="server" Text="是否申請出差費用" TextAlign="2" width="110px" />
            </td>
	        <td class=BasicFormHeadDetail colspan="5">
                <cc1:DSCCheckBox ID="IsTripFee" runat="server" Text="" /> 
            </td>
        </tr>
		<tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblTripSite" runat="server" Text="出差地點" IsNecessary="True" TextAlign="2" /><br>
                
            </td>
	        <td colspan=5 class=BasicFormHeadDetail>
                <cc1:SingleField ID="TripSite" runat="server" Width="400px" /><font size=2 color="ff0000">(ex: 新竹<->台北 )</font>
            </td>
        </tr>
		<tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblNotes" runat="server" Text="擬辦事項" IsNecessary="True" TextAlign="2" /><br>
                <font size=2 color="ff0000">請詳填洽談事項</font>
            </td>
	        <td colspan=5 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Notes" runat="server" Height="50px" Width="500px" MultiLine="True" />
            </td>
        </tr>
        <tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbMeetingMinute" runat="server" Text="會議記錄"  TextAlign="2" /><br>
            </td>
	        <td colspan=5 class=BasicFormHeadDetail>
                <cc1:SingleField ID="MeetingMinute" runat="server" Height="50px" Width="500px" MultiLine="True" />
            </td>
        </tr>
		<tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblTrafficFee" runat="server" Text="車資" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="TrafficFee" runat="server" Width="50px" isMoney="True"  /><font size=2 color="ff0000">(大眾運輸工具車資填此)</font>
            </td>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="油資" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="OilFee" runat="server" Width="50px" ReadOnly="True" isMoney="True"    />
            </td>
        </tr>
		<tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEatFee" runat="server" Text="繕雜資" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="EatFee" runat="server" Width="100px" isMoney="True"   />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbMileage" runat="server" Text="去回公里數" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
				<cc1:SingleField ID="StartMileage" runat="server" Width="50px" Height="10px" isMoney="True"  
                    ontextchanged="StartMileage_TextChanged" />
                <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="~" Width="10px" Height="10px" TextAlign="3" />
                <cc1:SingleField ID="EndMileage" runat="server" Width="50px" isMoney="True"  
                    ontextchanged="EndMileage_TextChanged"  />
            </td>
        </tr>
		<tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblParkingFee" runat="server" Text="停車資" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="ParkingFee" runat="server" Width="100px"  isMoney="True"  />
            </td>
            <td colspan="2" class=BasicFormHeadDetail align="center">
				<font size=2 color="ff0000">( 申請油資請務必填寫 [去回公里數] )</font>
            </td>            
        </tr>
        <tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOtherFee" runat="server" Text="其他費用" TextAlign="2" />
            </td>
	        <td colspan=1 class=BasicFormHeadDetail>
                <cc1:SingleField ID="OtherFee" runat="server" Width="100px" isMoney="True"   />
            </td>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="里程數" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="MileageSum" runat="server" Width="50px" ReadOnly="True" isMoney="True"   />
            </td>
        </tr>
		<tr>			
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="lbEtagFee" runat="server" Text="ETC費用" TextAlign="2"  />				
            </td>
	        <td colspan=5 class=BasicFormHeadDetail>
                <cc1:SingleField ID="EtagFee" runat="server" Width="50px" isMoney="True" TextAlign="1" />
				<asp:HyperLink ID="ETCHyperLink" runat="server" Text="eTag帳戶查詢" Font-Size="Smaller" Target="_blank" />
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Label ID="LBEtcStart" runat="server" Text="交流道起" Width="50px"></asp:Label>
				<cc1:SingleDropDownList ID="EtcStart" runat="server" Width="90px" 
                     onselectchanged="EtcMileage_SelectChanged" TextAlign="2" />
				<asp:Label ID="LBEtcEnd" runat="server" Text="交流道迄" Width="50px"></asp:Label>
				<cc1:SingleDropDownList ID="EtcEnd" runat="server" Width="90px" 
                     onselectchanged="EtcMileageEnd_SelectChanged" TextAlign="1" />	 
            </td>


        </tr>
		
		
        <tr>
	        <td colspan=6 class=BasicFormHeadDetail>                               
				**其他說明<br />
                1.) 此單據為離廠及未來核銷請款依據。<br>
				<font size=2 color="0000ff">2.) 出差起迄時間以出差當日07:00至23:59為限。<br />
                3.) 出差單應於出差前完成申請。如有特殊狀況，最遲應於出差日後5日內(含假日)完成補單，逾時不得申請。<br>
				4.) 出差日期及時間如有異動，請通知人力資源部。<br></font>
                5.) 個人出差核銷→在申請人收到通知後，接著至國內出差核銷單請款，待核銷至財務關卡時列印憑證送至財務部門請款。<br>
                6.) 部門出差費用核銷→請收到通知時將發票及請款文件交給部門請款負責人統一彙總請款。<br>
                7.) 油資：私車公用油資請款，此欄位必需填寫去回公里數，里程數由系統自動計算<br />
                8.) 車資：搭乘大眾運輸工具車資費用請款、公務車油資請款<br />
				<!--<font size="2" color="#ff00ff"><b>*** 嘉普人員至105/12/31止國外出差費請款(發票、收據等)請使用中普統一編號:54542191 ***</b></font> <br/>-->
				**流程說明: 
                <br />
                1.) 個人核銷：填表人 >> 直屬主管 >> 通知出差人員 >> 結案<br />
                2.) 彙總核銷：填表人 >> 直屬主管 >> 出差人員 >> 直屬主管2 >> 通知出差人員 >> 結案 <br>
                </font>
			    <br />
                </td>
        </tr>
        </table>
    </div>
    </form>

</body>
</html>
