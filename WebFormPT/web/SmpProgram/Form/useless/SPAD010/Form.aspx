<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPAD010_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>國內出差旅費核銷單</title>
    <style type="text/css">
    </style>
</head>

<body leftmargin=0 topmargin=0>

<form id="form1" runat="server">
    
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
		<cc1:SingleField ID="SheetNo" runat="server" Display="False" />
        <%--<tr><td align=center><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/smp-logo.jpg"/></td></tr>--%>
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>國內出差旅費核銷單</b></font></td>
        </tr>
    </table>
	<table style="margin-left:4px; width: 180px;" border=0 cellspacing=0 cellpadding=1 >
		<tr>
			<td align="left" Width="100%" class="BasicFormHeadHead"><b>				
				<cc1:GlassButton ID="PrintButton1" runat="server" Height="20px" Width="100%" 
                    Text="列印國內出差旅費核銷單" onbeforeclicks="PrintButton_OnClick" Enabled="True" /></b>
			</td>
		</tr>
	</table><br>
	
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr>
	        <td align="right" class="BasicFormHeadHead" title="主旨">
	            <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="85px" IsNecessary="True" TextAlign="2" />
	        </td>
			<td colspan=3 class=BasicFormHeadDetail>
	            <cc1:SingleField ID="Subject" runat="server" Width="435px" />
	        </td>
		</tr>		
		<tr>
	        <td align="right" class="BasicFormHeadHead" title="公司別">
				<cc1:DSCLabel ID="LblCompanyCode" runat="server" Text="公司別" Width="85px" IsNecessary="True" TextAlign="2" />
	        </td>
			<td colspan=3 class=BasicFormHeadDetail>
				<cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="138px"  />
	        </td>
		</tr>
        <tr>			
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="請款人員" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
                    idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton"  IgnoreCase="true" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDeptID" runat="server" Text="部門" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="200px" 
				     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
					 tableName="OrgUnit" IgnoreCase="true"/>
            </td>
        </tr>   
		<tr>			
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckBy" runat="server" Text="審核人員" Width="85px" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckByGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="CheckByGUID_BeforeClickButton"  IgnoreCase="true" />
            </td>
        </tr>
		<tr>			
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblStartDate" runat="server" Text="請款日期起" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class="BasicFormHeadDetail">
				<cc1:SingleDateTimeField ID="StartDate" runat="server" Width="140px" />
            </td>
			<td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEndDate" runat="server" Text="請款日期訖" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
				<cc1:SingleDateTimeField ID="EndDate" runat="server" Width="140px" />
            </td>
        </tr>     		
        
		<tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblTotalAmount" runat="server" Text="請款總額" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="TotalAmount" runat="server" Width="120px" />
            </td>
            <td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblPayeeGUID" runat="server" Text="領款人" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="PayeeGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" idIndex="2" valueIndex="3"  IgnoreCase="true" />
            </td>
        </tr>
        <tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblFinDesc" runat="server" Text="其他說明" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="FinDesc" runat="server" Width="480px" />
            </td>
        </tr>
		
		<tr>
            <td class="BasicFormHeadDetail" colSpan="4">
                <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">
             
	    			<table border=0 cellspacing=0 cellpadding=3>
	                <tr>
	                    <td><cc1:DSCLabel ID="lbCheckUser" runat="server" Width="100px" Text="請選擇使用者："/></td>
	                    <td><cc1:SingleOpenWindowField ID="CheckUser" runat="server" 
                                showReadOnlyField="true" guidField="OID" keyField="id" keyFieldType="STRING" serialNum="003" 
                             tableName="Users" Width="218px" idIndex="2" valueIndex="3" 
                                onbeforeclickbutton="CheckUser_BeforeClickButton" IgnoreCase="true" /></td>
	                    <td><cc1:DSCLabel ID="lbCheckDept" runat="server" Width="100px" Text="請選擇部門："/></td>
						<td><cc1:SingleOpenWindowField ID="CheckDept" runat="server" Width="150px" 
			                  showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
			                  tableName="OrgUnit" IgnoreCase="true"/>
						</td>
                        <td><cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" Text="開始查詢" Width="102px" OnClick="SearchButton_Click" showWaitingIcon="True" /></td>
	                </tr>
	                </table>
                </cc1:DSCGroupBox>
              </td>
          </tr>
          <tr>
            <td class="BasicFormHeadDetail" colSpan="4">
            <table border=0 cellspacing=0 cellpadding=3>
            <tr>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblUserGUID" runat="server" Text="工號" Width="100px" ReadOnly="True" /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblTripDate" runat="server" Text="出差日期" Width="80px" ReadOnly="True" /></td>
				<td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblStartTime" runat="server" Text="起始時間" Width="40px" /></td>
				<td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblEndTime" runat="server" Text="截止時間" Width="40px" /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblStartMileage" runat="server" Text="去公里數" Width="40px" /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblEndMileage" runat="server" Text="回公里數" Width="40px"/></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblMileageSum" runat="server" Text="里程數" Width="40px" /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblOilFee" runat="server" Text="油資" Width="40px" /></td>   
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblTrafficFee" runat="server" Text="車資" Width="40px" /></td>   
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblEatFee" runat="server" Text="繕雜費" Width="40px"  /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblParkingFee" runat="server" Text="停車費" Width="40px" /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblEtagFee" runat="server" Text="ETC費用" Width="40px"  /></td> 
				<td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblEtcStart" runat="server" Text="交流道起" Width="40px"  /></td> 
				<td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblEtcEnd" runat="server" Text="交流道訖" Width="40px"  /></td> 
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblOtherFee" runat="server" Text="其他" Width="40px" /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblOriForm" runat="server" Text="出差單" Width="120px" ReadOnly="True" /></td>
				<td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblTripSite" runat="server" Text="出差地點" Width="100px" ReadOnly="True" /></td>
                
            </tr>
            <tr>
				<cc1:SingleField ID="RefEtagFee" runat="server" />
                <td class=BasicFormHeadDetail>
                    <cc1:SingleOpenWindowField ID="UserGUID" runat="server" Width="100px" 
                        showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                        tableName="Users" Height="80px" idIndex="2" valueIndex="3" 
                        EnableTheming="True" ReadOnly="True" isShowAll="True" 
                        onbeforeclickbutton="UserGUID_BeforeClickButton" IgnoreCase="true" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleDateTimeField ID="TripDate" runat="server" Width="100px" ReadOnly="True"/>
                </td>
				<td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="StartTime" runat="server" Width="40px" ReadOnly="True"/>
                </td>
				<td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="EndTime" runat="server" Width="40px" ReadOnly="True"/>
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="StartMileage" runat="server" Width="40px" isMoney="True"
                        ontextchanged="StartMileage_TextChanged" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="EndMileage" runat="server" Width="40px" isMoney="True"
                        ontextchanged="EndMileage_TextChanged" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="MileageSum" runat="server" Width="40px" ReadOnly="True" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="OilFee" runat="server" Width="40px" ReadOnly="True" isMoney="True"/>
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="TrafficFee" runat="server" Width="40px" isMoney="True"/>
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="EatFee" runat="server" Width="40px" isMoney="True"/>
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="ParkingFee" runat="server" Width="40px" isMoney="True"/>
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="EtagFee" runat="server" Width="40px" isMoney="True"/>
                </td>
				<td class=BasicFormHeadDetail>                    
					<cc1:SingleDropDownList ID="EtcStart" runat="server" Width="90px" 
                     onselectchanged="EtcMileage_SelectChanged" TextAlign="2" />
                </td>
				<td class=BasicFormHeadDetail>
                    <cc1:SingleDropDownList ID="EtcEnd" runat="server" Width="90px" 
                     onselectchanged="EtcMileageEnd_SelectChanged" TextAlign="1" />	
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="OtherFee" runat="server" Width="40px" isMoney="True"/>
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="OriTripFormGUID" runat="server" Width="120px" ReadOnly="True" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="TripSite" runat="server" Width="120px" ReadOnly="True" />
                </td>
            </tr>
			<tr>
				<td class="BasicFormHeadDetail" colSpan="18">
					<cc1:OutDataList ID="BillingDetailList" runat="server" 
                        Width="100%" onsaverowdata="BillingDetailList_SaveRowData" 
                        onshowrowdata="BillingDetailList_ShowRowData"  OnAddOutline="BillingDetailList_AddOutline" OnDeleteData="BillingDetailList_DeleteData" showExcel="True" />
				    </cc1:DSCGroupBox>                          
				</td>
			</tr>
		    </table>
        </td></tr>
		<!--
        <tr>
	        <td colspan=4 class=BasicFormHeadDetail>
                <font size="2">****註1: 審核人不須選取部門主管(系統自行判斷)! 若部門有審核人(如: 主任/副理) 請選取!</font></br>
				<font size="2">****註: 此單據為國內出差單，離廠依據，差旅費核銷請使用國內差旅費核銷單!</font>
			</td>
        </tr>-->
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
                8.) 車資：搭乘大眾運輸工具車資費用請款、公務車油資請款	
                </font>
			    <br />
                </td>
        </tr>
        </table>
    </div>
    </form>

</body>
</html>
