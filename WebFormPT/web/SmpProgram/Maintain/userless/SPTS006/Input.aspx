<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_maintain_SPTS006_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">   
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>人員資格鑑定說明書</title>    
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 702px">
        <legend>人員資格鑑定說明書</legend> 
        <table style="margin-left:4px; width: 680px;" border=0 cellspacing=0 
            cellpadding=1 >
            <tr><td align="right">                  
                   <cc1:DSCLabel ID="LblCompanyCode" runat="server" Width="90px" Text="公司別" 
                    TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="114px" /> </td>
            </tr>
            <tr><td align="right">                  
                   <cc1:DSCLabel ID="LblEducational" runat="server" Width="90px" Text="學歷" 
                    TextAlign="2" IsNecessary="True" /></td> 
	             <td class="style2">
                    <cc1:SingleDropDownList ID="Educational" runat="server" Width="114px" /> </td>
                 <td align="right">                  
                     &nbsp;</td> 
	             <td>&nbsp;</td>                       
            </tr>
            <tr><td align="right">
                    <cc1:DSCLabel ID="LblJobFunction" runat="server"  Width="90px" Text="工作職務" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="JobFunction" runat="server"  Width="201px" /></td>  
            </tr>   
            <tr><td align="right">
                    <cc1:DSCLabel ID="LblJobItem" runat="server"  Width="90px" Text="工作項目" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="JobItem" runat="server"  Width="525px" Height="16px" /></td>     
              
            </tr>
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblExperience" runat="server" Width="90px" Text="經歷" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="Experience" runat="server" Width="525px" Height="57px" 
                        MultiLine="True" /></td> 
            </tr> 
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblCourse" runat="server" Width="90px" Text="訓練課程" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="Course" runat="server" Width="525px" Height="57px" 
                        MultiLine="True" /></td> 
            </tr> 
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblSkill" runat="server" Width="90px" Text="技能" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="Skill" runat="server" Width="525px" Height="57px" 
                        MultiLine="True" /></td> 
            </tr>
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblEvaluation" runat="server" Width="90px" Text="評核方式" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="Evaluation" runat="server" Width="525px" Height="57px" 
                        MultiLine="True" /></td> 
            </tr>  
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblStartYear" runat="server" Width="90px" Text="有效年度(起)" 
                        TextAlign="2" IsNecessary="True" /></td> 
	            <td class="style2">
                    <cc1:SingleField ID="StartYear" runat="server" Width="60px" 
                        ontextchanged="StartYear_TextChanged"  />                    
                    <cc1:DSCLabel ID="LblS1" runat="server" Width="51px" Text="(YYYY)" /></td> 
                 <td align="right">
                    <cc1:DSCLabel ID="LblEndYear" runat="server" Width="84px" Text="有效年度(迄)" 
                         TextAlign="2" style="margin-left: 0px" /></td>                     
	            <td>
                    <cc1:SingleField ID="EndYear" runat="server" Width="60px" 
                        ontextchanged="EndYear_TextChanged"  />
                    <cc1:DSCLabel ID="LblS2" runat="server" Width="51px" Text="(YYYY)" /></td>
            </tr>  
            <tr>
                <td align="right">
                    <cc1:DSCLabel ID="LblRemark" runat="server" Width="90px" Text="備註" 
                        TextAlign="2" /></td> 
	            <td colspan="3">
                    <cc1:SingleField ID="Remark" runat="server" Width="525px"  /></td> 
            </tr>      
           
        </table>
    </fieldset>  
   
    </form>
</body>
</html>
