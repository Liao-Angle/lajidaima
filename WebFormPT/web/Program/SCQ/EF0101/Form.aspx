<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EF0101_Form" %>

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
        <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1>  
        <cc1:SingleField ID="SheetNo" runat="server" />
        <cc1:SingleField ID="Subject" runat="server"  /><tr valign=middle > 
		    <td align=center style="height: 40px"><font style="font-family: 標楷體; font-size: large;"><b>廠務需求申請單</b></font></td>
	    </tr>
	</table>
        <table style="margin-left:4px" border=0 width=666px cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
                <tr>
            <td width="120px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="員工" Width="100%"></cc1:DSCLabel></td>
            <td width="200px" class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="EmpNo" runat="server" 
                    showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                    tableName="hrusers" 
                    OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" 
                    valueIndex="1" idIndex="0"  />
            </td>
            <td width="100px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBMobile" runat="server" Text="電話號碼" Width="100%"></cc1:DSCLabel>
            </td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:SingleField ID="Mobile" runat="server" Width="180px" />
            </td>
        </tr>
        <tr>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBRequestDate" runat="server" Text="申請日期"></cc1:DSCLabel></td>
            <td width="240px" class=BasicFormHeadDetail>
                <cc1:DSCLabel ID="RequestDate" runat="server" Text="表單發起日"></cc1:DSCLabel>
            </td>
            <td width="80px" align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBExpectDate" runat="server" Text="需完成日期"></cc1:DSCLabel></td>
            <td width="240px" class=BasicFormHeadDetail>
                                <cc1:SingleDateTimeField ID="ExpectDate" runat="server" 
                                    Width="120px" />  
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBLocation" runat="server" Text="地點"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="Location" runat="server" Width="500px" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBCTitle" runat="server" Text="申請說明"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="Explanation" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBResult" runat="server" Text="施工結果"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="Result" runat="server" Width="100%" Height="64px" 
                    MultiLine="True" />
            </td>
        </tr>
        <tr>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBExecuter" runat="server" Text="施工者"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Executer" runat="server" Width="160px" />
            </td>
            <td align=right class=BasicFormHeadHead style="height: 24px">
                <cc1:DSCLabel ID="LBUseTime" runat="server" Text="維修用時" />
                </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="UseTime" runat="server" Width="54px" />
                小時</td>
        </tr>

                <tr runat="server" id="showzg" visible="false">
            <td class="BasicFormHeadHead" style="height: 17px">
          <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="請選擇簽核主管" Width="150px" Height="20px"></cc1:DSCLabel>
             </td>
              <td class="BasicFormHeadHead"  style="height: 17px">
                  <cc1:SingleDropDownList ID="sqzszg" runat="server" Width="111px" />
             </td>
        </tr>
         </table>
    </div>
    </form>
</body>
</html>
