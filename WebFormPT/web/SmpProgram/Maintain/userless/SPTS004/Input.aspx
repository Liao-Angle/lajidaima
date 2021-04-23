<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>講師管理維護</title>
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 600px">
        <legend>講師管理維護畫面</legend>
        <table style="margin-left:4px; width: 621px;" border=0 cellspacing=0 
            cellpadding=1 >  
            <tr><td>
                <cc1:DSCLabel ID="LblCompany" runat="server" Width="100px" Text="公司別" 
                    TextAlign="2" IsNecessary="True" /></td>
                <td>
                    <cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="131px" /></td>
                <td><cc1:DSCLabel ID="LblLecturerSource" runat="server" Width="100px" Text="講師來源" 
                        TextAlign="2" IsNecessary="True" /></td>
                <td><cc1:SingleDropDownList ID="LecturerSource" runat="server" Width="131px" 
                        onselectchanged="LecturerSource_SelectChanged" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblInLecturerGUID" runat="server" Width="100px" 
                    Text="內部講師" TextAlign="2" /></td>
                <td><cc1:SingleOpenWindowField ID="InLecturerGUID" runat="server" Width="200px" 
                        showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                        tableName="Users" idIndex="2" valueIndex="3" 
                        FixReadOnlyValueTextWidth="100px" 
                        onsingleopenwindowbuttonclick="InLecturerGUID_SingleOpenWindowButtonClick" 
                        IgnoreCase="True" />
                <td><cc1:DSCLabel ID="LblInLecturerDeptGUID" runat="server" Width="100px" Text="部門" TextAlign="2" /></td>
                <td><cc1:SingleOpenWindowField ID="InLecturerDeptGUID" runat="server" Width="200px" 
                        showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                        tableName="OrgUnit" FixReadOnlyValueTextWidth="100px" IgnoreCase="True" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblExtLecturer" runat="server" Width="100px" Text="外部講師" 
                    TextAlign="2" /></td>
                <td><cc1:SingleField ID="ExtLecturer" runat="server" Width="185px" /></td>
                <td><cc1:DSCLabel ID="LblExtCompany" runat="server" Width="100px" Text="公司/機構" 
                        TextAlign="2" /></td>
                <td><cc1:SingleField ID="ExtCompany" runat="server" Width="185px" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblEmail" runat="server" Width="100px" Text="信箱" 
                    TextAlign="2" /></td>
                <td><cc1:SingleField ID="Email" runat="server" Width="185px"/></td>
                <td><cc1:DSCLabel ID="LblStartEndDate" runat="server" Width="100px" Text="有效時間" 
                        TextAlign="2" /></td>
                <td><cc1:SingleDateTimeField ID="StartDate" runat="server" Width="100px" />~
                    <cc1:SingleDateTimeField ID="EndDate" runat="server" Width="100px" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblTelephone" runat="server" Width="100px" Text="電話" 
                    TextAlign="2" /></td>
                <td><cc1:SingleField ID="Telephone" runat="server" Width="185px" /></td>
                <td></td>
                <td></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblSpecialSkill" runat="server" Width="100px" Text="專長" 
                    TextAlign="2" /></td>
                <td colspan="3"><cc1:SingleField ID="SpecialSkill" runat="server" Width="512" /></td>
            </tr>
            <tr><td><cc1:DSCLabel ID="LblExperience" runat="server" Width="100px" Text="經歷" 
                    TextAlign="2" /></td>
                <td colspan=3>
                    <cc1:SingleField ID="Experience" runat="server" Height="70px" Width="512px" 
                        MultiLine="True" /></td>
            </tr>
            <tr><td>
                <cc1:DSCLabel ID="LblCalssType" runat="server" Width="100px" Text="教授課程類別" 
                    TextAlign="2" IsNecessary="True" /></td>
                <td colspan=3>
                    <cc1:DSCCheckBox ID="CbxOrientation" runat="server" Text="新人訓練" Width="80px" />
                    <cc1:SingleField ID="Orientation" runat="server" Width="60px" Display="false" />
                    &nbsp;
                    <cc1:DSCCheckBox ID="CbxProfessional" runat="server" Text="專業職能" Width="80px" />
                    <cc1:SingleField ID="Professional" runat="server" Width="60px" Display="false" />
                    &nbsp;
                    <cc1:DSCCheckBox ID="CbxManagement" runat="server" Text="管理職能" Width="80px" />
                    <cc1:SingleField ID="Management" runat="server" Width="60px" Display="false" />
                    &nbsp;
                    <cc1:DSCCheckBox ID="CbxQuality" runat="server" Text="品質管理" Width="80px" />
                    <cc1:SingleField ID="Quality" runat="server" Width="60px"  Display="false" />
                    &nbsp;
                    <cc1:DSCCheckBox ID="CbxEHS" runat="server" Text="安全衛生" Width="80px" />
                    <cc1:SingleField ID="EHS" runat="server" Width="60px" Display="false" />
                </td>
            </tr>
        </table>
    </fieldset>
    <table>   
         
    </table> 
    <div>
    </div>
    </form>
</body>
</html>
