<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPAD002_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">        
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>代理人維護作業</title>
    <style type="text/css">
    </style>  
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 430px">
        <legend>被代理人維護作業</legend>
		<cc1:SingleField ID="SMVKAAA005" runat="server" Display="False" /> 
		<cc1:SingleField ID="SMVKAAA006" runat="server" Display="False" />
        <table style="margin-left:4px; width: 430px;" border=0 cellspacing=0 cellpadding=1 >  
            <tr><td>
                 <cc1:DSCLabel ID="LblSMVKAAA002" runat="server" Width="100px" 
                        Text="被代理人" TextAlign="2" /></td>
                <td>
					<cc1:SingleOpenWindowField ID="SMVKAAA002" runat="server" 
                    showReadOnlyField="True" Width="340px" guidField="OID" keyField="id" 
                    serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                        EnableTheming="True" IgnoreCase="True"/>
                </td>
            </tr>
			<tr><td>
                 <cc1:DSCLabel ID="LblAgentDate" runat="server" Width="100px" 
                        Text="代理期間起~訖" TextAlign="2" /></td>
                <td>
					<cc1:SingleDateTimeField ID="SMVKAAA003" runat="server" width="155px" DateTimeMode="1" />
					<cc1:SingleDateTimeField ID="SMVKAAA004" runat="server" width="155px" DateTimeMode="1" />
                </td>
            </tr>
        </table>
    </fieldset>
    <table>   
         <tr><td align="right">
             <cc1:DSCLabel ID="LblSMVKAAB003" runat="server" Width="74px" 
                    Text="代理人" TextAlign="2" /></td>
            <td>
                <cc1:SingleOpenWindowField ID="SMVKAAB005" runat="server" 
                    Width="358px" showReadOnlyField="True" guidField="OID" keyField="id" 
                    serialNum="001" tableName="Users" FixReadOnlyValueTextWidth="200px" 
                    FixValueTextWidth="120px" Display="True" IgnoreCase="True" />
			</td>
        </tr> 
        <tr><td colspan="4">
            <cc1:OutDataList ID="AgentDetailList" runat="server" height="160px" width="430px" 
                CertificateMode="True" onsaverowdata="AgentDetailList_SaveRowData" onshowrowdata="AgentDetailList_ShowRowData"
            />
            </td>                           
        </tr>
        </table> 
    <div>
    </div>
    </form>
</body>
</html>
