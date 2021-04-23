<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPKM004_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>文件調閱</title>

</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >       
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>知識文件調閱申請單</b></font></td>
        </tr>
	</table>	

    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=1 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblSubject" runat="server" Text="主旨" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                <cc1:SingleField ID="Subject" runat="server" Width="577px" />
            </td>
        </tr>
		<tr>
            <td align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="lbcompanyCode" runat="server" Text="公司別" TextAlign="2"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail style="height: 12px" colspan="5">
				<cc1:SingleDropDownList ID="CompanyCode" runat="server" Width="138px"  />
            </td>
        </tr>
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblOriginator" runat="server" Text="申請人" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="OriginatorGUID" runat="server" Width="140px" 
                    showReadOnlyField="True" guidField="OID" keyField="id" serialNum="003" 
                    tableName="Users" Height="80px" 
                    idIndex="2" valueIndex="3" FixReadOnlyValueTextWidth="60px" 
                    FixValueTextWidth="50px" 
                    onbeforeclickbutton="OriginatorGUID_BeforeClickButton" />
            </td>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblEndDate" runat="server" Text="調閱截止日" Width="100px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
	        <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="EndDate" runat="server" Width="110px" />
            </td>
        </tr>
		<tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblAccessReason" runat="server" Text="調閱原因" Width="70px" 
                    IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=5>
            <cc1:SingleField ID="AccessReason" runat="server" Width="577px" Height="60px" 
                    MultiLine="True" />
            </td>
        </tr>		
        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblConfidentialLevel" runat="server" Text="最高機密等級" Width="100px" 
                    IsNecessary="False" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=5>
                <cc1:SingleDropDownList ID="ConfidentialLevel" runat="server" Width="107px" />
            </td>
        </tr>

        <tr>
            <td colspan="4" class="BasicFormHeadHead">
                <asp:Label ID="Label2" runat="server" Text=" **點選文件清單中任一文件後，即可在調閱文件欄位中看到文件編號及文件名稱，此時可點選檢視文件按鈕，查看文件內容。" 
                                            style="font-size: 9pt" ForeColor="Red"></asp:Label>
            </td>
        </tr>

        <tr>
            <td align="right" class="BasicFormHeadHead">
                <cc1:DSCLabel ID="LblDocGUID" runat="server" Text="調閱文件" Width="100px" IsNecessary="True" TextAlign="2" />
            </td>
            <td class=BasicFormHeadDetail colspan=3>
                            <cc1:SingleOpenWindowField ID="DocGUID" runat="server" 
                                Width="430px" guidField="GUID" keyField="DocNumber" 
                                serialNum="001" tableName="Document" FixReadOnlyValueTextWidth="200px" 
                                FixValueTextWidth="180px" showReadOnlyField="True" 
                                onsingleopenwindowbuttonclick="DocGUID_SingleOpenWindow" 
                                onbeforeclickbutton="DocGUID_BeforeClickButton" />
                            <cc1:GlassButton ID="SearchButton" runat="server" ImageUrl="~/Images/OK.gif" 
                                OnClick="SearchButton_Click" showWaitingIcon="True" Text="新增" Width="60px" Height="20px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <cc1:GlassButton ID="GlassButtonViewDoc" runat="server" Height="20px" 
                    Text="檢視文件" Width="60px" onclick="GlassButtonViewDoc_Click" />
             </td>
           </tr>
           <tr>
             <td colspan="4">
               <cc1:OutDataList ID="DocAccessList" runat="server" Width="650px" Height="200px"
                                onsaverowdata="DocAccessList_SaveRowData" 
                                onshowrowdata="DocAccessList_ShowRowData" isShowAll="True" />
             </td>
           </tr>
			
		

		</table>


    </div>
    </form>
    <p>
&nbsp;&nbsp;&nbsp;
    </p>
</body>
</html>
