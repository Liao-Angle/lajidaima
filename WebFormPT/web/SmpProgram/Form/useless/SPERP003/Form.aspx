<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_System_Form_SPPO_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>庶務性請購單</title>
    <script language="javascript" src="../../../JS/jquery-1.4.1.js"></script>
    <script language="javascript" src="../../../JS/jquery-1.4.1.min.js"></script>

    <script language="javascript">
        function onViewPr(headerId) {
            var url = $("#ViewPrUrl").val();
            var param = { HeaderId: headerId };
            OpenWindowWithPost(url, 'height=680, width=1000, top=100, left=100, toolbar=no, menubar=no, location=no, status=no', 'ViewPr', param);
        }

        function onViewAttach(entityName, pk1Value, modify, title) {
            var url = $("#ViewAttachUrl").val();
            var param = { EntityName: entityName, Pk1Value: pk1Value, Modify: modify, Title: title };
            OpenWindowWithPost(url, 'height=420, width=782, top=100, left=100, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no', 'ViewAttach', param);
        }

        function OpenWindowWithPost(url, windowoption, name, params) {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", name);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {
                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }
            window.open("", name, windowoption);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
    </script>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <asp:HiddenField ID="SPPOA007" runat="server" />
    <asp:HiddenField ID="SPPOA011" runat="server" />
    <asp:HiddenField ID="SPPOA014" runat="server" />
    <asp:HiddenField ID="SPPOA016" runat="server" />
    <asp:HiddenField ID="SPPOA017" runat="server" />
    <asp:HiddenField ID="ViewAttachUrl" runat="server" />
    <asp:HiddenField ID="ViewPrUrl" runat="server" />
    <div>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 >
        <tr><td align=center>&nbsp;</td>
        </tr>
        <tr>
            <td align=center height="40"><font style="font-family: 標楷體; font-size: large;"><b>庶&nbsp;務&nbsp;性&nbsp;請&nbsp;購&nbsp;單</b></font></td>
        </tr>
    </table>
    <table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td class=BasicFormHeadHead align=right colspan="3" class="style1">
                <cc1:DSCLabel ID="lblSPPO0003" runat="server" Text="申請日期" Width="80px"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleDateTimeField ID="SPPOA003" runat="server" Width="140px" /></td>
        </tr> 
        <tr>
            <td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSubject" runat="server" Text="主旨" Width="80px"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail colspan="3"><cc1:SingleField ID="Subject" runat="server" Height="20px" Width="564px" /></td>
        </tr>
        <tr>
            <td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSetOfBookName" runat="server" Text="公司別" Width="80px"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail colspan="3"><cc1:SingleField ID="SetOfBookName" runat="server" Height="20px" Width="120px" /></td>
        </tr>
        <tr>
            <td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSPPOA004" runat="server" Text="請購人員" Width="80px"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="SPPOA004" 
                    runat="server" Width="220px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="001" tableName="Users" IgnoreCase="True" /></td>
            <td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSPPO003" runat="server" Text="部門代號" Width="80px"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail><cc1:SingleOpenWindowField ID="SPPOA005" 
                    runat="server" Width="229px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="001" tableName="OrgUnit" IgnoreCase="True" /></td>
        </tr>
        <tr><td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSPPO004" runat="server" Text="請購單單號" Width="80px"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail><cc1:SingleField ID="SPPOA006" runat="server" Height="20px" />
                <asp:HyperLink ID="hlSPPOA006" runat="server">PR Number</asp:HyperLink>
                &nbsp;
                <asp:ImageButton ID="ImgButtonAtta" runat="server" ImageUrl="~/Images/attachForSMVE.gif" />
            </td>
            <td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSPPO005" runat="server" Text="幣別" Width="80px"></cc1:DSCLabel></td>
            <td class=BasicFormHeadHead>
                <cc1:SingleField ID="SPPOA008" runat="server" Height="20px" Width="80px" />
                <cc1:DSCLabel ID="lblSPPO009" runat="server" Text="預估金額" Width="70px"></cc1:DSCLabel>
                <cc1:SingleField ID="SPPOA009" runat="server" Height="20px" Width="80px" />
            </td> 
        </tr>
        <tr><td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSPPO010" runat="server" Text="說明" Width="80px"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail colspan=3><cc1:SingleField ID="SPPOA010" runat="server" Width="562px" Height="61px" MultiLine=true /></td>
        </tr>
        <tr><td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSPPOA018" runat="server" Text="請購類別" Width="80px"></cc1:DSCLabel></td>
            <td class=BasicFormHeadDetail class=BasicFormHeadDetail>
                <cc1:DSCRadioButton ID="SPPOA018A" runat="server" Text="資訊類" Width="80px" />
                <cc1:DSCRadioButton ID="SPPOA018C" runat="server" Text="工安類" Width="80px" />
                <cc1:DSCRadioButton ID="SPPOA018B" runat="server" Text="其他類" Width="80px" />
            </td>
            <td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSPPOA012" runat="server" Text="審核人員" Width="80px" /></td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="SPPOA012" 
                    runat="server" Width="229px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="SPPOA012_BeforeClickButton" IgnoreCase="True" /></td>
        </tr>
        <tr><td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSPPOA013" runat="server" Text="會簽人員" Width="80px" /></td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="SPPOA013" 
                    runat="server" Width="231px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3" 
                    onbeforeclickbutton="SPPOA013_BeforeClickButton" IgnoreCase="True" /></td>
            <td class=BasicFormHeadHead><cc1:DSCLabel ID="lblSPPOA019" runat="server" Text="通知人員" Width="80px" /></td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleOpenWindowField ID="SPPOA019" 
                    runat="server" Width="231px" showReadOnlyField="True" guidField="OID" 
                    keyField="id" serialNum="003" tableName="Users" idIndex="2" valueIndex="3"
                    onbeforeclickbutton="SPPOA019_BeforeClickButton" IgnoreCase="True" /></td>
        </tr> 
    </table>
    <div>
    <cc1:DSCLabel ID="lblRemark1" runat="server" Text="(註1：點選請購單單號可以開啟ERPPortal此請購單明細資料)" Width="666px"></cc1:DSCLabel>
    <br>
    <cc1:DSCLabel ID="lblRemark2" runat="server" Text="(註2：點選附件圖示可開啟ERPPortal此請購單附件)" Width="666px"></cc1:DSCLabel> 
    </div>
    <br>
    </div>
        <cc1:OutDataList ID="DataListLine" runat="server" Height="172px" Width="663px" style="margin-right: 0px" />
        <br />
        <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="會簽人員意見" Width="666px" />
        <br />
        <cc1:SingleField ID="SPPOA015" runat="server" Width="666px" Height="61px" MultiLine=true/>
        <br /> 
        <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="流程說明：請購人員 >> 需求者 >> 審核人員(自行選取) >> 部門主管 >> 會簽人員 >> 通知" Width="666px"></cc1:DSCLabel> 
        <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="會簽說明：資訊類系統加簽資訊部負責人員；系統自動加簽料號預設採購員" Width="666px"></cc1:DSCLabel> 
    </form>
</body>
</html>
