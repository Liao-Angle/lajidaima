<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EG0104_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
        <table style="margin-left:4px" border=0 width=800px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工"></cc1:DSCLabel></td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="254px" 
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" 
                    OnSingleOpenWindowButtonClick="RequestID_SingleOpenWindowButtonClick" 
                    valueIndex="1" idIndex="0" />
            </td>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBTelephone" runat="server" Text="聯絡電話" />
                </td>
            <td width="280px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="Telephone" runat="server" Width="80%" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBRoomNo" runat="server" Text="會議室"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleDropDownList ID="RoomNo" 
                    runat="server" Width="87px" /> 
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBSTime" runat="server" Text="起始時間"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="SDateTime" runat="server" 
                                    Width="200px" DateTimeMode="3" />  
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBETime" runat="server" Text="終止時間" />
                </td>
            <td class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="EDateTime" runat="server" 
                                    Width="200px" DateTimeMode="3" />  
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBSubject" runat="server" Text="會議主題"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Subject" runat="server" Width="80%" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBParticipant" runat="server" Text="參與人員" 
                    style="margin-left: 0px"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                <cc1:SingleField ID="Participant" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBMachine" runat="server" Text="設備使用"></cc1:DSCLabel>
            </td>
            <td colspan=3 class=BasicFormHeadDetail>
                 <cc1:DSCCheckBox ID="Projector" runat="server" Text="投影機"></cc1:DSCCheckBox>
                 <cc1:DSCCheckBox ID="Video" runat="server" Text="視訊會議"></cc1:DSCCheckBox>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
