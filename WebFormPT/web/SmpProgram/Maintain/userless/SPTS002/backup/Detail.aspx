<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_maintain_SPTS002_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>年度教育訓練計劃-明細</title>
</head>
<body>
	<cc1:SingleField ID="PlanYear" runat="server" Display="False" /> 
	
    <form id="form1" runat="server">
    <table>
        <tr>
			<td><cc1:DSCLabel ID="lblEstimateMonth" runat="server" Width="90px" Text="預計開課月份"  TextAlign="2" IsNecessary="true"/></td>
            <td>
				<cc1:SingleDropDownList ID="EstimateMonth" runat="server" Width="100px" />
				<cc1:SingleField ID="Quarter" runat="server" Width="50px" />
			</td>
			<td><cc1:DSCLabel ID="lblDeptGUID" runat="server" Width="90px" Text="部門代號"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="300px" 
                            showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                            tableName="OrgUnit" FixReadOnlyValueTextWidth="180px" 
                        FixValueTextWidth="70px" />
			</td>
        </tr>
        <tr>
			<td><cc1:DSCLabel ID="lblCourseNo" runat="server" Width="90px" Text="課程名稱"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleField ID="CourseName" runat="server" Width="200px" /></td>			
			<td><cc1:DSCLabel ID="lblCourseName" runat="server" Width="90px" Text="課程代號"  TextAlign="2" /></td>	
			<td><cc1:SingleField ID="CourseNo" runat="server" Width="100px" /></td>
        </tr>
		<tr>
			<td><cc1:DSCLabel ID="lblTrainingHours" runat="server" Width="90px" Text="訓練時數"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleField ID="TrainingHours" runat="server" Width="100px" isMoney="True"	Fractor="1" /></td>
			<td><cc1:DSCLabel ID="lblNumberOfPeople" runat="server" Width="90px" Text="預訓人數"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleField ID="NumberOfPeople" runat="server" Width="100px" isMoney="True" /></td>
        </tr>
		<tr>
			<td><cc1:DSCLabel ID="lblTrainingObject" runat="server" Width="90px" Text="預訓對象"  TextAlign="2" IsNecessary="true"/></td>
            <td colspan="3"><cc1:SingleField ID="TrainingObject" runat="server" Width="400px" /></td>			
        </tr>
		<tr>
			<td><cc1:DSCLabel ID="lblInOut" runat="server" Width="90px" Text="內/外訓"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="InOut" runat="server" Width="100px"  />
            </td>
			<td><cc1:DSCLabel ID="lblCourseType" runat="server" Width="90px" Text="課程類別"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="CourseType" runat="server" Width="100px" />
            </td>
        </tr>
		<tr>
			<td><cc1:DSCLabel ID="lblFees" runat="server" Width="90px" Text="預算費用"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleField ID="Fees" runat="server" Width="100px" isMoney="True" /></td>
			<td><cc1:DSCLabel ID="lblEvaluationLevel" runat="server" Width="90px" Text="評估等級"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="EvaluationLevel" runat="server" Width="200px" />
            </td>
        </tr>		
        <tr>
			<td><cc1:DSCLabel ID="lblTTQS" runat="server" Width="90px" Text="TTQS課程"  TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="TTQS" runat="server" Width="90px" /></td>
        </tr>

    </table>
    </form>
</body>
</html>
