<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_System_Form_SPPO_Form" %>

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
        <asp:Image ID="Image1" runat="server" Height="100px" ImageUrl="~/Program/System/Form/SPPO/143823.gif"
            Width="667px" /><br />
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
         <tr>
            <td align=right colspan="4">
            </td>
            <td align=right class=BasicFormHeadHead style="width: 147px">
                <cc1:DSCLabel ID="SPPO001lb" runat="server" Text="申請日期"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="width: 186px">
                <cc1:SingleDateTimeField ID="SPPO001" runat="server" />
            </td>
        </tr> 
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="SPPO002lb" runat="server" Text="請購人員" Width="73px"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="width: 157px">
                <cc1:SingleOpenWindowField ID="SPPO002" runat="server" Width="172px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="SPPO003lb" runat="server" Text="部門代號" Width="75px"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail colspan="3">
                <cc1:SingleOpenWindowField ID="SPPO003" runat="server" Width="325px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="OrgUnit" />
            </td>
        </tr>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead style="height: 12px">
                <cc1:DSCLabel ID="SPPO004lb" runat="server" Text="請購單單號"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="width: 157px; height: 12px">
                <cc1:SingleField ID="SPPO004" runat="server" Width="97%" Height="20px"  />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead style="height: 12px">
                <cc1:DSCLabel ID="SPPO005lb" runat="server" Text="幣別"></cc1:DSCLabel> 
            </td>
            <td class=BasicFormHeadDetail style="width: 168px; height: 12px">
                <cc1:SingleField ID="SPPO005" runat="server" Width="93%" Height="20px"  /> 
            </td>
            <td align=right class=BasicFormHeadHead style="width: 147px; height: 12px">
                <cc1:DSCLabel ID="SPPO006lb" runat="server" Text="預估金額"></cc1:DSCLabel>  
            </td>
            <td class=BasicFormHeadDetail style="width: 186px; height: 12px;">
                <cc1:SingleField ID="SPPO006" runat="server" Width="99%" Height="20px"  />  
            </td> 
        </tr>
        <tr>
            <td width="80px" valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="SPPO007lb" runat="server" Text="說明"></cc1:DSCLabel> 
            </td>
            <td colspan=5 class=BasicFormHeadDetail>
                <cc1:SingleField ID="SPPO007" runat="server" Width="99%" Height="61px" MultiLine=true/>   
            </td>
        </tr>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="SPPO008lb" runat="server" Text="審核人員" Width="73px"/>
            </td>
            <td class=BasicFormHeadDetail style="width: 157px">
                <cc1:SingleOpenWindowField ID="SPPO008" runat="server" Width="171px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="SPPO009lb" runat="server" Text="會簽人員" Width="73px"/>
            </td>
            <td class=BasicFormHeadDetail style="width: 168px" colspan=3>
                <cc1:SingleOpenWindowField ID="SPPO009" runat="server" Width="325px" showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
            </td>
        </tr> 
        <tr>
            <td width="80px" valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="SPPO010lb" runat="server" Text="請購類別"></cc1:DSCLabel>
            </td>
            <td colspan=5 class=BasicFormHeadDetail>
                <cc1:DSCRadioButton ID="DSCRadioButton1" runat="server" Text="資訊類" Width="96px" />
                <cc1:DSCRadioButton ID="DSCRadioButton2" runat="server" Text="非資訊類" Width="96px" />
                
            </td>
        </tr>
        
        </table>
    </div>
        &nbsp;<cc1:OutDataList ID="OutDataList1" runat="server" Height="172px" Width="664px" />
       <br /> 
       &nbsp;<cc1:DSCLabel ID="DSCLabel1" runat="server" Text="流程說明：請購人員簽核 >> 需求者簽核 >> 審核人員簽核(自行選取) >> 部門主管簽核 >> 會簽人員簽核 >> 通知採購承辦人員" Width="666px"></cc1:DSCLabel> 
    </form>
</body>
</html>
