<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_EG0109_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>常駐/支援人員申請單</title>
    </head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1">
            <cc1:SingleField ID="SheetNo" runat="server" Display="False" />
            <cc1:SingleField ID="Subject" runat="server" Display="False" />
            <tr valign="middle">
                <td align="center" height="40">
                    <font style="font-family: 標楷體; font-size: large;"><b>常駐/支援人員申請單</b></font>
                </td>
            </tr>
        </table>
        <table style="margin-left: 4px; width: 700px;" border="0" cellspacing="0" cellpadding="1"
            class="BasicFormHeadBorder">
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="申請類型" Width="100%"></cc1:DSCLabel>
                </td>
                <td width="164px" class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="Rqtype" runat="server" Width="155px" 
                        ReadOnly="True" Height="24px"
                        CertificateMode="True" onselectchanged="Rqtype_SelectChanged" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="歸屬公司" Width="100%"></cc1:DSCLabel>
                </td>
                <td width="164px" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Company" runat="server" Width="155px" ReadOnly="True" Height="24px"
                        CertificateMode="True" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBEmpNo" runat="server" Text="申請人" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleOpenWindowField ID="EmpNo" runat="server" Width="210px" showReadOnlyField="True"
                        guidField="EmpNo" keyField="EmpNo" serialNum="001" tableName="TravleUser" valueIndex="1"
                        idIndex="0" 
                        OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" />
                    <cc1:SingleField ID="sfEmpName" runat="server" Width="10px" />
                </td>
                <td width="100px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBDepartment" runat="server" Text="英文名" Width="100%" Style="font-size: small"
                        CertificateMode="True"></cc1:DSCLabel>
                </td>
                <td width="164px" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Ename" runat="server" Width="155px" ReadOnly="True" Height="24px"
                        CertificateMode="True" />
                </td>
            </tr>
             <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBxz" runat="server" Text="性別" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="Sex" runat="server" Width="155px" ReadOnly="True" />
                </td>
                <td width="100px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBsxrs" runat="server" Text="常駐支援部門" Width="100%" Height="16px"></cc1:DSCLabel>
                </td>
                <td  width="164px" class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="PartNo" runat="server" Width="155px" ReadOnly="True" />
                </td>
            </tr>
            
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBdate" runat="server" Text="時間起" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="ccSDate" runat="server" Width="155px" 
                        InputAreaVisible="True" ondatetimeclick="ccSDate_DateTimeClick" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="時間止" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="ccEDate" runat="server" 
                        Width="155px" ondatetimeclick="ccSDate_DateTimeClick" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBts" runat="server" Text="天數" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="ts" runat="server" Width="155px" ReadOnly="True" />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBmudi" runat="server" Text="職務" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="DtName" runat="server" Width="155px" 
                        ReadOnly="True" />
                </td>
            </tr>
            <tr>
                
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBdlr" runat="server" Text="類別" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="EmpType" runat="server" Width="149px"  />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBdanwei" runat="server" Text="靜電帽" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="Jdm" runat="server" Width="155px" ReadOnly="True" />
                </td>
            </tr>
             <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="工衣" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="Gyi" runat="server" Width="149px"  />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="鞋" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="Jdx" runat="server" Width="155px" ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="住宿" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="ZS" runat="server" Width="149px"  />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="用餐" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDropDownList ID="YC" runat="server" Width="155px" ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="物理卡號" Width="100%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PID" runat="server" Width="149px"  />
                </td>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="台胞證或身份證" Width="145%"></cc1:DSCLabel>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="CardID" runat="server" Width="155px" ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td width="80px" align="right" class="BasicFormHeadHead">
                    <cc1:DSCLabel ID="LBqtbz" runat="server" Text="備註" Width="100%"></cc1:DSCLabel>
                </td>
                <td  colspan="3" class="BasicFormHeadDetail">
                    <cc1:SingleField ID="qtbz" runat="server" Width="558px" ReadOnly="True" 
                        Height="79px" MultiLine="True" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>