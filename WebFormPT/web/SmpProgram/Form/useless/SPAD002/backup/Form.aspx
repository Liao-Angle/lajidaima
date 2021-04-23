<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Simplo_Form_SPAD002_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>    
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
        .style2
        {
            font-size: large;
            font-family: 標楷體;
        }
    </style>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
	<table height="500px" border="0" cellpadding="0" cellspacing="0" a><tr><td valign="top">
    <div>
        <div class="style1">
            <span class="style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 出 勤 修 正 單</span>
        </div>
        
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr>
            <td width="70px" height="30px" class=BasicFormHeadHead align="right">
                <cc1:DSCLabel ID="LBCOMPANY" runat="server" Text="公 司 別" />
            </td>        
            <td colspan=5 width="500px" align="left" class=BasicFormHeadDetail>
                <cc1:SingleDropDownList ID="COMPANY" runat="server" Width="182px" Font-Strikeout="False" />
            </td>
         
        </tr>
        <tr>
            <td width="70px" height="30px" alight=right class=BasicFormHeadHead align="right">
                <cc1:DSCLabel ID="LBUSERDEPT" runat="server" Text="部       門" />
            </td>            
            <td width="180px" class=BasicFormHeadDetail align="left">               
                <cc1:SingleOpenWindowField ID="DEPTID" TextAlign="1" runat="server" Width="260px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="OrgUnit" FixReadOnlyValueTextWidth="170px" FixValueTextWidth="60px"/>     								
            </td>
            <td width="70px" alight=right class=BasicFormHeadHead align="right">
                <cc1:DSCLabel ID="LBUSERGUID" runat="server" Text="申 請 人"></cc1:DSCLabel>
            </td>
            <td width="150px" class=BasicFormHeadDetail align="left" colspan=3>
				<cc1:SingleOpenWindowField ID="USERGUID" runat="server" Width="150px" 
	                 showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    idIndex="2" valueIndex="3" 
	                 tableName="Users" 
                    onsingleopenwindowbuttonclick="USERGUID_SingleOpenWindowButtonClick" 
                    onbeforeclickbutton="USERGUID_BeforeClickButton" TextAlign="2" />
			</td>
			
        </tr>
        <tr>
            <td width="70px" height="30px" class=BasicFormHeadHead align="right">
                <cc1:DSCLabel ID="LBREASON" runat="server" Text="更正理由" />
            </td>        
            <td colspan=5 width="500px" align="left" class=BasicFormHeadDetail>
                <cc1:DSCRadioButton ID="REASON1" runat="server" Text="未帶卡片" />
                <cc1:DSCRadioButton ID="REASON2" runat="server" Text="忘記刷卡" Checked="True" />
                <cc1:DSCRadioButton ID="REASON3" runat="server" Text="其他(請說明)" />
                &nbsp;&nbsp;<cc1:SingleField ID="REASONDESC" runat="server" Width="180px" />
            </td>
         
        </tr>
        <tr>
            <td colspan=6 class=BulletinContent align="center">修 正 上 下 班 時 間</td>
        </tr>
		<tr>
            <td colspan=6 align="center" class=BasicFormHeadDetail>
            <cc1:GlassButton ID="ButtonSearch" runat="server" Height="20px" Width="320px" 
                    Text="查詢計薪週期出勤明細" Display="True" onbeforeclicks="ButtonSearch_OnClick" />&nbsp; 
            <cc1:GlassButton ID="ButtonSearchByDate" runat="server" Height="20px" Width="320px" 
                    Text="依上/下班日期查詢出勤時間" Display="True" onbeforeclicks="ButtonSearchByDate_OnClick" />
            </td>
        </tr>
        <tr>
            <td width="70px" height="30px" class=BasicFormHeadHead align="right">
                <cc1:DSCLabel ID="LBSTART_TIME" runat="server" Text="上班時間" />
            </td>
            <td colspan=5 width="240px" class=BasicFormHeadDetail align="left">
               <cc1:SingleDateTimeField ID="STARTTIME" runat="server" width="155px" DateTimeMode="3" ondatetimeclick="StartDate_DateTimeClick"/>
			   &nbsp;&nbsp;<cc1:GlassButton ID="ClearStartDateButton" runat="server" onclick="ClearStartDateButton_Click" text="清除" width="50px" />
            </td>
        </tr>
        <tr>
            <td width="70px" height="30px" class=BasicFormHeadHead align="right">
                <cc1:DSCLabel ID="LBEND_TIME" runat="server" Text="下班時間" />
            </td>
            <td colspan=5 width="240px" class=BasicFormHeadDetail align="left">
                <cc1:SingleDateTimeField ID="ENDTIME" runat="server" width="155px" DateTimeMode="3" ondatetimeclick="EndDate_DateTimeClick"/>
				&nbsp;&nbsp;<cc1:GlassButton ID="ClearEndDateButton" runat="server" onclick="ClearEndDateButton_Click" text="清除" width="50px" />
            </td>
        </tr>

        <tr>
            <td width="70px" height="30px" class=BasicFormHeadHead align="right">
                <cc1:DSCLabel ID="LBREVIEWER" runat="server" Text="審 核 人" />
            </td>
            <td colspan=5 width="550px" class=BasicFormHeadDetail align="left">
                <cc1:SingleOpenWindowField ID="REVIEWER" runat="server" Width="180px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="REVIEWER_BeforeClickButton" IgnoreCase="true" />                                                
            </td>           
        </tr>

        </table>
    </div>
	</td></tr></table>
    </form>
</body>
</html>
