<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_System_Form_SPTS001_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>內訓開課申請單</title>
    <style type="text/css">
    </style>
</head>

<body leftmargin=0 topmargin=0>

<form id="form1" runat="server">
<cc1:SingleDateTimeField ID="EndDate" runat="server" Display="False" />      
	<div>	
	<table style="margin-left:4px; width: 760px;" border=0 cellspacing=0 cellpadding=1 >
		<tr height="40"> 
			<td align=center colSpan="5">
				<b><font style="font-family: 標楷體; font-size: large;">內訓開課申請單</font></b> 
            </td>
		</tr>
	</table>
	<table border=0 cellspacing=0 cellpadding=0 class=BasicFormHeadBorder>
		<tr>
			<td colSpan="5" class="BasicFormHeadHead" Width="100%"><b>				
				<cc1:GlassButton ID="PrintButton" runat="server" Height="20px" Width="200" 
                    Text="教育訓練簽到表格" onbeforeclicks="PrintButton_OnClick" Enabled="True" /></b>
			</td>
		</tr>
	</table>
	<br>
	<table style="margin-left:4px; width: 760px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr>
			<td align="right" class="BasicFormHeadHead" title="單號">
				<cc1:DSCLabel ID="lblSheetNo" runat="server" Text="單號" Width="85px" TextAlign="2" />
			</td>
			<td class=BasicFormHeadDetail  colSpan="3">
				<cc1:SingleField ID="SheetNo" runat="server" Width="120px" ReadOnly="True" />
			</td>
		</tr>
		<tr>
			<td align="right" class="BasicFormHeadHead" title="主旨">
				<cc1:DSCLabel ID="lblSubject" runat="server" Text="主旨" Width="85px" IsNecessary="False" TextAlign="2" />
			</td>
			<td class=BasicFormHeadDetail  colSpan="3">
				<cc1:SingleField ID="Subject" runat="server" Width="640px" />
			</td>
		</tr>
		<tr>
		    <td align="right" class="BasicFormHeadHead">
                   <cc1:DSCLabel ID="lblCompanyCode" runat="server" Text="公司別" Width="70px" TextAlign="2" IsNecessary="True" /></td>
			<td  class=BasicFormHeadDetail colSpan="3" >
				<cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="138px"  /></td>
		</tr>
		
		<tr>
			<td align="right" class="BasicFormHeadHead">                
                  <cc1:DSCLabel ID="lblApplyDept" runat="server" Text="申請部門" TextAlign="2" 
                      Width="70px" IsNecessary="True" /> </td>
	           <td  class=BasicFormHeadDetail colSpan="3" >
                   <cc1:SingleOpenWindowField ID="ApplyDeptGUID" runat="server" Width="375px" 
                           showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                           tableName="OrgUnit" FixReadOnlyValueTextWidth="250px" 
                       FixValueTextWidth="95px" IgnoreCase="True" /> 
			</td>   
		</tr>
			<tr>
			 <td align="right" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="LblApplicant" runat="server" Text="申請人員" Width="85px" IsNecessary="True" TextAlign="2" />
			</td>
			<td class=BasicFormHeadDetail colSpan="3">
				<cc1:SingleOpenWindowField ID="ApplicantGUID" runat="server" Width="200px" 
					showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
					tableName="Users" Height="80px" 
					onsingleopenwindowbuttonclick="ApplicantGUID_SingleOpenWindowButtonClick" 
					idIndex="2" valueIndex="3" 
					onbeforeclickbutton="ApplicantGUID_BeforeClickButton" IgnoreCase="true" />
			</td> 
		</tr>
		
		</tr>
			<tr>
			 <td align="right" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lblCheckby1" runat="server" Text="審核人1"  TextAlign="2"></cc1:DSCLabel>
			</td>
			<td class=BasicFormHeadDetail colSpan="3">
				<cc1:SingleOpenWindowField ID="CheckBy1GUID" runat="server" Width="200px" 
					showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
					tableName="Users" Height="80px" 
					idIndex="2" valueIndex="3" 
					onbeforeclickbutton="CheckBy1GUID_BeforeClickButton" IgnoreCase="true" />
			</td> 
			<tr>
			 <td align="right" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lblCheckby2" runat="server" Text="審核人2"  TextAlign="2"></cc1:DSCLabel>
			</td>
			<td class=BasicFormHeadDetail colSpan="3">
				<cc1:SingleOpenWindowField ID="CheckBy2GUID" runat="server" Width="200px" 
					showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
					tableName="Users" Height="80px" 
					idIndex="2" valueIndex="3" 
					onbeforeclickbutton="CheckBy2GUID_BeforeClickButton" IgnoreCase="true" />
			</td> 
		</tr>		
		
		<tr>
			<td align="right" class="BasicFormHeadHead">
                   <cc1:DSCLabel ID="lblCourseYear" runat="server" IsNecessary="True" Text="開課年度" 
                       TextAlign="2" width="70px" /></td>
            <td class="BasicFormHeadDetail"><cc1:SingleField ID="CourseYear" runat="server" Width="113px" /></td>
			<td align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="lblCourseSource" runat="server" Text="課程來源" TextAlign="2" Width="75px" IsNecessary="True" />
            </td>
			<td  class=BasicFormHeadDetail>
                   <cc1:SingleDropDownList ID="SchSource" runat="server" Width="90px" /> </td> 
		</tr>
		<tr>
			<td align="right" class="BasicFormHeadHead">
                  <cc1:DSCLabel ID="lblDateS" runat="server" Text="上課日期" TextAlign="2" 
                      Width="83px" IsNecessary="True" /> 
			</td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="StartDate" runat="server" Width="116px" />				
			</td>
			<td align="right" class="BasicFormHeadHead">
                  <cc1:DSCLabel ID="lblInOut" runat="server" Text="內外訓" TextAlign="2" 
                      Width="66px" IsNecessary="True" /> </td>
			<td  class=BasicFormHeadDetail>
                   <cc1:SingleDropDownList ID="InOut" runat="server" Width="90px" /> </td>
			 
           </tr>
		<tr>
			<td align="right" class="BasicFormHeadHead">
				<cc1:DSCLabel ID="lblStartTime" runat="server" Text="上課時間" Width="75px" IsNecessary="True" TextAlign="2" />
			</td>
			<td class=BasicFormHeadDetail>
				<cc1:SingleDateTimeField ID="StartTime" runat="server" Width="140px" 
					ondatetimeclick="StartTime_DateTimeClick" DateTimeMode="4" />
				<cc1:DSCLabel ID="lblTime" runat="server" Text="~ " Width="10px"></cc1:DSCLabel>&nbsp;&nbsp;&nbsp;	
				<cc1:SingleDateTimeField ID="EndTime" runat="server" Width="140px" 
					ondatetimeclick="EndTime_DateTimeClick" DateTimeMode="4" />
			</td>	
		    <td align="right" class="BasicFormHeadHead">
                  <cc1:DSCLabel ID="lblCourseType" runat="server" Text="課程類別" Width="70px" 
                       TextAlign="2" IsNecessary="True" /> </td>
            <td  class=BasicFormHeadDetail>
                   <cc1:SingleDropDownList ID="SubjectType" runat="server" Width="90px" /> </td> 			
		</tr>
		<tr>					
			<td align="right" class="BasicFormHeadHead">
                 <cc1:DSCLabel ID="lblCourseName" runat="server" Text="課程名稱" Width="70px" 
                        TextAlign="2" IsNecessary="True" /> </td>
            <td class=BasicFormHeadDetail>
                   <cc1:SingleOpenWindowField ID="SchDetailGUID" runat="server" Width="379px" 
                           showReadOnlyField="True" guidField="GUID" keyField="SubjectNo" serialNum="001" 
                           tableName="TSSchForm" FixReadOnlyValueTextWidth="250px" 
                       FixValueTextWidth="95px" 
                       onbeforeclickbutton="SchDetailGUID_BeforeClickButton" 
                        onsingleopenwindowbuttonclick="SchDetailGUID_SingleOpenWindowButtonClick" 
                       IgnoreCase="True" dialogWidth="600" idIndex="3" valueIndex="4" /></td>    
			<td align="right" class="BasicFormHeadHead">
                  <cc1:DSCLabel ID="lblWay" runat="server" Text="教授方式" TextAlign="2" 
                       Width="70px" IsNecessary="True" /> </td>
            <td  class=BasicFormHeadDetail>
                   <cc1:SingleDropDownList ID="Way" runat="server" Width="90px" />  </td>
           </tr>
		<tr>
			<td align="right" class="BasicFormHeadHead">                
                  <cc1:DSCLabel ID="lblDept" runat="server" Text="開課單位" TextAlign="2" 
                      Width="70px" IsNecessary="True" /> </td>
	            <td class=BasicFormHeadDetail>
                   <cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="375px" 
                           showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                           tableName="OrgUnit" FixReadOnlyValueTextWidth="250px" 
                       FixValueTextWidth="95px" IgnoreCase="True" /> 
			</td>              
			<td align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="lblPlace" runat="server" Text="上課地點" TextAlign="2" 
                        Width="70px" IsNecessary="True" /> </td>
            <td class=BasicFormHeadDetail>
                   <cc1:SingleField ID="Place" runat="server" Width="90px" />  </td>
		</tr>			
		<tr>
			<td align="right" class="BasicFormHeadHead">
                  <cc1:DSCLabel ID="lblLecturer" runat="server" Text="講師" TextAlign="2" 
                      Width="70px" IsNecessary="False" />  </td>
            <td class=BasicFormHeadDetail> 
                   <cc1:SingleOpenWindowField ID="LecturerGUID" runat="server" Width="388px" 
                           showReadOnlyField="True" guidField="GUID" keyField="GUID" serialNum="001" 
                           tableName="TSLecturer" FixReadOnlyValueTextWidth="250px" 
                       FixValueTextWidth="95px" 
                       onbeforeclickbutton="LecturerGUID_BeforeClickButton" IgnoreCase="True" />
			</td>  
			<td align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="lblUserQty" runat="server" Text="預訓人數" TextAlign="2" 
                        Width="70px" IsNecessary="True" /> </td>
            <td class=BasicFormHeadDetail>
                   <cc1:SingleField ID="UserQty" runat="server" Width="90px" Fractor="0" />  </td>  
		</tr>
		<tr><td align="right" class="BasicFormHeadHead">
                  <cc1:DSCLabel ID="LblAssessmentI" runat="server" Text="評核方式" Width="94px" 
                      TextAlign="2" />  </td>
            <td class=BasicFormHeadDetail>   
				<cc1:DSCCheckBox ID="CbWrittenTest" runat="server" Text="筆試" Width="49px" />
                   <cc1:SingleField ID="WrittenTest" runat="server" Width="60px" Display="false" />
                   <cc1:DSCCheckBox ID="CbImplement" runat="server" Text="實作" Width="52px" />
                   <cc1:SingleField ID="Implement" runat="server" Width="60px" Display="false" />
                   <cc1:DSCCheckBox ID="CbSatisfaction" runat="server" Text="課程滿意度調查" Width="119px" />
                   <cc1:SingleField ID="Satisfaction" runat="server" Width="60px" Display="false" /> 
                   
               </td>    
			<td align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="lblHours" runat="server" Text="上課時數" TextAlign="2" 
                        Width="70px" IsNecessary="True" /> </td>
            <td class=BasicFormHeadDetail>
                   <cc1:SingleField ID="Hours" runat="server" Width="90px" Fractor="1" 
                       ontextchanged="Hours_TextChanged" />  </td>  
          </tr>
		         
           <tr>
               <td align="right" class="BasicFormHeadHead">
                  <cc1:DSCLabel ID="LblRemark" runat="server" Text="備註" Width="70px" 
                       TextAlign="2" />  </td>
            <td class="BasicFormHeadDetail" colspan="3">
                   <cc1:SingleField ID="Remark" runat="server" Width="640px" /></td> 
           </tr> 			
    </table>
    <table style="margin-left:4px; width: 760px;" border=0 cellspacing=0 cellpadding=1>        
       <tr><td >
            <cc1:DSCTabControl ID="TabList" runat="server" Height="220px" Width="100%" PageColor="White">
            <TabPages>
                <cc1:DSCTabPage runat="server" Title="學員資料" Enabled="True" ImageURL="" Hidden="False" ID="TabTrainee">
                    <TabBody>
                        <table width="100%">
							<tr>
							  <td class="BasicFormHeadDetail" colSpan="4">
								<cc1:DSCGroupBox ID="DSCGroupBox1" runat="server" Text="查詢">
									<table border=0 cellspacing=0 cellpadding=3>
									<tr>
										<td><cc1:DSCLabel ID="lbCheckUser" runat="server" Width="85px"  TextAlign="2" Text="請選擇使用者"/></td>
										<td><cc1:SingleOpenWindowField ID="CheckUser" runat="server" 
												showReadOnlyField="true" guidField="OID" keyField="id" keyFieldType="STRING" serialNum="003" 
											 tableName="Users" Width="150px" idIndex="2" valueIndex="3" 
												onbeforeclickbutton="CheckUser_BeforeClickButton" IgnoreCase="true" 
												FixReadOnlyValueTextWidth="65px" FixValueTextWidth="55px" />
										</td>
										<td><cc1:DSCLabel ID="lbCheckDept" runat="server" Width="80px"  TextAlign="2" Text="請選擇部門"/></td>
										<td><cc1:SingleOpenWindowField ID="CheckDept" runat="server" Width="260px" 
											  showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
											  tableName="OrgUnit" IgnoreCase="true"
											  FixReadOnlyValueTextWidth="145px" FixValueTextWidth="75px"/>
										</td>
										<td><cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" Text="新增" Width="60px" OnClick="SearchButton_Click" showWaitingIcon="True" /></td>
										<td><cc1:GlassButton ID="ClearButton" runat="server" ImageUrl="~/Images/GeneralFileUploadCollapse.gif" Text="清除" Height="25px" Width="60px" OnClick="ClearButton_Click" showWaitingIcon="True" /></td>
									</tr>
									</table>
								</cc1:DSCGroupBox>
							  </td>
							</tr>                           
                            <tr>
								<td>                        
                    			    <cc1:OutDataList ID="DataListTrainee" runat="server" Width="100%"  Height="200px"
								    onsaverowdata="DataListTrainee_SaveRowData" onshowrowdata="DataListTrainee_ShowRowData" 
									OnAddOutline="DataListTrainee_AddOutline" OnDeleteData="DataListTrainee_DeleteData" showExcel="True" />
								</td>
                            </tr>
							
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="TabMaterial" runat="server" Enabled="True" Hidden="False" ImageURL="" Title="課程教材">
                    <TabBody>
                        <table width="100%">
                            <tr><td align="right" Width="70px">
                                    <cc1:DSCLabel ID="lblSource" runat="server" Width="70px" Text="來源" TextAlign="2" /></td>
                                <td><cc1:SingleDropDownList ID="Source" runat="server" Width="70px" /></td>
                                <td align="right" Width="70px">
                                    <cc1:DSCLabel ID="lblMaterialGUID" runat="server" Width="70px" Text="文件號碼" TextAlign="2" /></td>
                                <td><cc1:SingleOpenWindowField ID="MaterialGUID" runat="server" 
                                    width="370px" showReadOnlyField="True" guidField="GUID" keyField="DocNumber" 
                                    serialNum="001" tableName="Document" FixReadOnlyValueTextWidth="200px" FixValueTextWidth="120px" 
                                    onbeforeclickbutton="MaterialGUID_BeforeClickButton" IgnoreCase="True" /></td>
                            </tr>
                            <tr><td colspan="4">
                                    <cc1:OutDataList ID="DataListMaterial" runat="server" height="200px" width="100%" EnableTheming="True" 
                                        OnSaveRowData="DataListMaterial_SaveRowData" OnShowRowData="DataListMaterial_ShowRowData" /></td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
            </TabPages>
            </cc1:DSCTabControl>            
            </td></tr>           
    </table>
   
	</div>
</form>

</body>
</html>
