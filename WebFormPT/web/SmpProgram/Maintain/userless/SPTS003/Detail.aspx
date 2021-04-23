<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_maintain_SPTS003_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>年度開課計劃維護畫面-2</title>
</head>
<body>
	<cc1:SingleField ID="SingleField1" runat="server" Display="False" /> 
    <cc1:SingleField ID="SingleField2" runat="server" Display="False" />
    <form id="form1" runat="server">
    <table>
    <!--    <tr>
            <td><cc1:DSCLabel ID="lblSchYear" runat="server" Width="100px" Text="計劃年度" 
                    TextAlign="2" IsNecessary="True"/></td>
            <td><cc1:SingleField ID="SchYear" runat="server" Width="100px" ReadOnly="True" /></td>
            <td><cc1:DSCLabel ID="lblCompanyCode" runat="server" Text="公司別" TextAlign="2" IsNecessary="true" Width="200px"/></td>
            <td><cc1:SingleField ID="CompanyCode" runat="server" Width="100px" ReadOnly="True" /></td>
        </tr>-->
        <tr>
            <td><cc1:DSCLabel ID="lblSchNo" runat="server" Width="100px" Text="課程代號" 
                    TextAlign="2" IsNecessary="True"/></td>
            <td><cc1:SingleField ID="SchNo" runat="server" Width="100px" ReadOnly="True" /></td>
            <td><cc1:DSCLabel ID="lblSchMonth" runat="server" Text="預計開課月份" TextAlign="2" IsNecessary="true" Width="200px"/></td>
            <td>
				<cc1:SingleDropDownList ID="SchMonth" runat="server" Width="100px"  onselectchanged="SchMonth_SelectChanged"/>
				<cc1:SingleField ID="Quarter" runat="server" Width="50px" ReadOnly="True" />
			</td>
        </tr>
        <tr>
			<td><cc1:DSCLabel ID="lblSubjectGUID" runat="server" Width="100px" Text="課程名稱" TextAlign="2" IsNecessary="true"/></td>
            <td colspan="3" class="left">
                    <cc1:SingleOpenWindowField ID="SubjectDetailGUID" runat="server" Width="450px" 
                            showReadOnlyField="True" guidField="GUID" keyField="SubjectNo" serialNum="001" 
                            tableName="TSSubject" FixReadOnlyValueTextWidth="300px" 
                        FixValueTextWidth="100px" IgnoreCase="True" 
                        onbeforeclickbutton="SubjectDetailGUID_BeforeClickButton" 
                        onsingleopenwindowbuttonclick="SubjectDetailGUID_SingleOpenWindowButtonClick" /></td>		
        </tr>
        <tr>
			
			<td><cc1:DSCLabel ID="lblDeptGUID" runat="server" Width="100px" Text="部門代號" TextAlign="2" IsNecessary="true"/></td>
            <td colspan="3" align="left">
                <cc1:SingleOpenWindowField ID="DeptGUID" 
                    runat="server" Width="450px" 
                            showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                            tableName="OrgUnit" FixReadOnlyValueTextWidth="300px" 
                        FixValueTextWidth="100px" IgnoreCase="True" 
                    onbeforeclickbutton="DeptGUID_BeforeClickButton" />                    
            </td>
        </tr>
        
        <tr>
			<td><cc1:DSCLabel ID="lblInOut" runat="server" Width="100px" Text="內/外訓" TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="InOut" runat="server" Width="100px" 
                    ReadOnly="True"  />
            </td>
			<td><cc1:DSCLabel ID="lblSubjectType" runat="server" Width="200px" Text="課程類別" TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="SubjectType" runat="server" Width="100px" 
                    ReadOnly="True" />
            </td>
        </tr>
		<tr>
			<td><cc1:DSCLabel ID="lblTrainingHours" runat="server" Width="100px" Text="訓練時數" TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleField ID="TrainingHours" runat="server" Width="100px" isMoney="True"	Fractor="1" /></td>
			<td><cc1:DSCLabel ID="lblNumberOfPeople" runat="server" Width="200px" Text="預訓人數" TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleField ID="NumberOfPeople" runat="server" Width="100px" isMoney="True" /></td>
        </tr>
		<tr>
			<td><cc1:DSCLabel ID="lblTrainingObject" runat="server" Width="100px" Text="預訓對象" TextAlign="2" IsNecessary="true"/></td>
            <td colspan="3"><cc1:SingleField ID="TrainingObject" runat="server" Width="400px" /></td>			
        </tr>
		
		<tr>
			<td><cc1:DSCLabel ID="lblFees" runat="server" Width="100px" Text="預算費用" TextAlign="2" IsNecessary="true"/></td>
            <td colspan="3"><cc1:SingleField ID="Fees" runat="server" Width="100px" isMoney="True" /></td>
		</tr>
        <tr>
			<td><cc1:DSCLabel ID="lblTTQS" runat="server" Width="100px" Text="TTQS課程" TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="TTQS" runat="server" Width="90px" /></td>
			<td><cc1:DSCLabel ID="lblSchSource" runat="server" Width="110px" Text="計劃來源" TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="SchSource" runat="server" Width="90px" /></td>
        </tr>
		<tr>
			<td><cc1:DSCLabel ID="lblCancel" runat="server" Width="100px" Text="取消課程" TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="Cancel" runat="server" Width="90px" 
                    onselectchanged="Cancel_SelectChanged" /></td>
            <td><cc1:DSCLabel ID="lblClosed" runat="server" Width="110px" Text="開班授課" TextAlign="2" IsNecessary="true"/></td>
            <td><cc1:SingleDropDownList ID="Closed" runat="server" Width="90px" 
                    ReadOnly="True" /></td>
        </tr>
		<tr>
			<td><cc1:DSCLabel ID="lblEvaluationLevel" runat="server" Width="100px" Text="評估等級" TextAlign="2" IsNecessary="true"/></td>
            <td colspan="3"><cc1:SingleDropDownList ID="EvaluationLevel" runat="server" Width="450px" />
            </td>
        </tr>		


    </table>
    </form>
</body>
</html>
