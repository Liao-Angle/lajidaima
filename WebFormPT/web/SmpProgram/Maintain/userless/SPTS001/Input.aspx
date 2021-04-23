<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">        
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>設定教育訓練管理者</title>
    <style type="text/css">
    </style>  
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 600px">
        <legend>設定教育訓練管理者</legend>
        <table style="margin-left:4px; width: 600px;" border=0 cellspacing=0 cellpadding=1 >  
            <tr><td>
                 <cc1:DSCLabel ID="LblCompany" runat="server" Width="60px" 
                        Text="公司別" TextAlign="2" /></td>
                <td>
                    <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="131px" />
                </td>
            </tr>
        </table>
    </fieldset>
    <table>   
         <tr><td align="right">
             <cc1:DSCLabel ID="LblAdmType" runat="server" Width="74px" 
                    Text="對象" TextAlign="2" /></td>
            <td>
                <cc1:SingleDropDownList ID="AdmType" runat="server" 
                    Width="70px" onselectchanged="AdmType_SelectChanged" /></td>
            <td align="right">
                <cc1:DSCLabel ID="LblAdmTypeGUID" runat="server" Width="87px" Text="管理者名稱" 
                    TextAlign="2" /></td>
            <td width="555px">
                <cc1:SingleOpenWindowField ID="AdmTypeGUID" runat="server" 
                    Width="358px" showReadOnlyField="True" guidField="OID" keyField="id" 
                    serialNum="001" tableName="SPKM001" FixReadOnlyValueTextWidth="200px" 
                    FixValueTextWidth="120px" Display="True" IgnoreCase="True" /></td>
        </tr> 
        <tr><td colspan="4">
            <cc1:OutDataList ID="AdmDetailList" runat="server" height="187px" width="600px" 
                CertificateMode="True" onsaverowdata="AdmDetailList_SaveRowData" onshowrowdata="AdmDetailList_ShowRowData"
            />
            </td>                           
        </tr>
        </table> 
    <div>
    </div>
    </form>
</body>
</html>
