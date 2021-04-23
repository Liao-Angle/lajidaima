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
        <tr><td align=center><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/smp-logo.jpg"/></td>
        </tr>
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>國內出差旅費核銷單</b></font></td>
        </tr>
    </table>
	
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
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDeptID" runat="server" Text="部門" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="200px" 
				     showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
					 tableName="OrgUnit" />
            </td>
        </tr>
        <tr>			
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="請款人員" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    onsingleopenwindowbuttonclick="OriginatorGUID_SingleOpenWindowButtonClick" 
                    idIndex="2" valueIndex="3" />
            </td>
        </tr>   
		<tr>			
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblCheckBy" runat="server" Text="審核人員" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="CheckByGUID" runat="server" Width="200px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" />
            </td>
        </tr>
		<tr>			
	        <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblStartDate" runat="server" Text="請款日期起" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class="BasicFormHeadDetail">
				<cc1:SingleDateTimeField ID="StartDate" runat="server" Width="140px" />
            </td>
			<td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEndDate" runat="server" Text="請款日期訖" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
				<cc1:SingleDateTimeField ID="EndDate" runat="server" Width="140px" />
            </td>
        </tr>     		
        
		<tr>
			<td class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblTotalAmount" runat="server" Text="請款總額" Width="85px" IsNecessary="True" TextAlign="2" />
            </td>
	        <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="TotalAmount" runat="server" Width="480px" />
            </td>
        </tr>
		
		<tr>
            <td class="BasicFormHeadDetail" colSpan="4">
                <cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">
             
	    			<table border=0 cellspacing=0 cellpadding=3>
	                <tr>
	                    <td><cc1:DSCLabel ID="lbCheckUser" runat="server" Width="100px" Text="請選擇使用者："/></td>
	                    <td><cc1:SingleOpenWindowField ID="CheckUser" runat="server" showReadOnlyField="true" guidField="OID" keyField="id" keyFieldType="STRING" serialNum="001" tableName="Users" Width="218px" /></td>
	                    <td><cc1:DSCLabel ID="lbCheckDept" runat="server" Width="100px" Text="請選擇部門："/></td>
						<td><cc1:SingleOpenWindowField ID="CheckDept" runat="server" Width="150px" 
			                  showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
			                  tableName="OrgUnit" />
						</td>
	                </tr>
	                <tr>
	                    <td colspan=3 align=right><cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" Text="開始查詢" Width="102px" OnClick="SearchButton_Click" showWaitingIcon="True" /></td>
	                </tr>
	                </table>
                </cc1:DSCGroupBox>
              </td>
          </tr>
          <tr>
            <td class="BasicFormHeadDetail" colSpan="4">
            <table border=0 cellspacing=0 cellpadding=3>
            <tr>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblUserGUID" runat="server" Text="工號" Width="100px" IsNecessary="True" /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblTripDate" runat="server" Text="出差日期" Width="80px" IsNecessary="True" /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblTrafficFee" runat="server" Text="車資" Width="50px" IsNecessary="True" /></td>   
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblEatFee" runat="server" Text="繕雜費" Width="50px" IsNecessary="True" /></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblParkingFee" runat="server" Text="停車費" Width="50px" IsNecessary="True" /></td>     
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblOtherFee" runat="server" Text="其他" Width="50px" IsNecessary="True"/></td>
                <td class="BasicFormHeadHead"><cc1:DSCLabel ID="LblOriForm" runat="server" Text="出差單" Width="120px" IsNecessary="True"/></td>
            </tr>
            <tr>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleOpenWindowField ID="UserGUID" runat="server" Width="145px" 
                        showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                        tableName="Users" Height="80px" idIndex="2" valueIndex="3" 
                        EnableTheming="True" ReadOnly="True"/>
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleDateTimeField ID="TripDate" runat="server" Width="140px" ReadOnly="True"/>
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="TrafficFee" runat="server" Width="50px" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="EatFee" runat="server" Width="50px" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="ParkingFee" runat="server" Width="50px" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="OtherFee" runat="server" Width="50px" />
                </td>
                <td class=BasicFormHeadDetail>
                    <cc1:SingleField ID="OriTripFormGUID" runat="server" Width="120px" ReadOnly="True" />
                </td>
            </tr>
			<tr>
				<td class="BasicFormHeadDetail" colSpan="7">
					<cc1:OutDataList ID="BillingDetailList" runat="server" Height="315px" 
                        Width="100%" onsaverowdata="BillingDetailList_SaveRowData" 
                        onshowrowdata="BillingDetailList_ShowRowData" />
				    </cc1:DSCGroupBox>                          
				</td>
			</tr>
		    </table>
        </td></tr>
		
        <tr>
	        <td colspan=4 class=BasicFormHeadDetail>
                <font size="2">****註1: 審核人不須選取部門主管(系統自行判斷)! 若部門有審核人(如: 主任/副理) 請選取!</font></br>
				<font size="2">****註2: 此單據為國內出差單，離廠依據，差旅費核銷請使用國內差旅費核銷單!</font>
			</td>
        </tr>
        </table>
    </div>
    </form>

</body>
</html>
