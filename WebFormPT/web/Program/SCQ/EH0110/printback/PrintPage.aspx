<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="Program_SCQ_Form_EH0109_PrintPage" %>
<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/SmpWebFormPT.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<style type="text/css">
@media print
{
  .print {display: none;}
}

    .style1
    {
        border-left: 1px none rgb(188,178,147);
        border-right: 1px solid rgb(188,178,147);
        border-top: 1px none rgb(188,178,147);
        border-bottom: 1px solid rgb(188,178,147);
        background-color: white;
        font-size: medium;
        font-family: Arial;
        height: 20px;
    }

</style>
     

<body style="background:#ffffff">
    <form id="form1" runat="server">
    <div>
        <table border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
			<Input Type="Button" Value="列印單據" onClick="javascript:print();" class="print"> 
        <table>
	<table><br>
      <tr><td style=" font-size:8pt; ">表單資訊</td></tr>
        <tr>
     <td>
     <div id="divFormInfo"  style="position:absolute;" runat="server">
    </div>
    <br />
     </td>
     </tr>
      </table>
<br>
<br />
<br />
<br />
<br />
	<table style="margin-left:4px; width: 660px;" border="0" cellspacing="0" cellpadding="1">
     <tr>
      <td align="center" height="30" style="width: 670px">
            <font style="font-family: 標楷體; font-size: large;"><b>新普科技（重慶）有限公司</b></font></td>
     </tr>
     <tr><td align="center" height="30" style="width: 670px">
            <font style="font-family: 標楷體;"><b>人員任用單</b></font></td>
     </tr>
     </table>
     <table style="margin-left:2px; width:660px; " class="BasicFormHeadBorder" border="0" cellpadding="1" cellspacing=0>
         <%--<tr>
            <td colspan=4 align="left" style= "width: 660px" class="BasicFormHeadHead"></td>
        </tr>--%>
        <tr>
        <td>
        <table style="margin-left:4px; width: 660px;" border="0" border-left:1px cellspacing="0" cellpadding="1">
            <tr>
                <td rowspan="2" style="width:50px; text-align: center;" 
                    class="BasicFormHeadHead">任</td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel1" runat="server" Text="姓名" Width="58px" 
                        BorderStyle="Inset"/>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="EmpName" runat="server" Width="100px" ReadOnly="True" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="任用部門" Width="80px"/>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="PartName" runat="server" Width="137px" ReadOnly="True" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel3" runat="server" Text="直屬主管" Width="112px"/>
                </td>
                <td class="BasicFormHeadDetail">
                <cc1:SingleField ID="zszg" runat="server" Width="104px" ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel4" runat="server" Text="職位" Width="58px"/>
                </td>
                <td class="BasicFormHeadDetail">
                <cc1:SingleField ID="DtName" runat="server" Width="100px" ReadOnly="True" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel5" runat="server" Text="職等" Width="80px"/>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="Dt" runat="server" Width="137px" ReadOnly="True" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel6" runat="server" Text="員工類別" Width="112px"/>
                </td>
                <td class="BasicFormHeadDetail">
                <cc1:SingleField ID="EmpType" runat="server" Width="104px" ReadOnly="True" />
                </td>
            </tr>
            </table>
            <table style="margin-left:4px; width: 660px;" border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td rowspan="2" style="width:50px; text-align: center;" 
                    class="BasicFormHeadHead">用</td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel7" runat="server" Text="預計到職日" Width="86px" 
                        style="margin-left: 0px"/>
                </td>
                <td class="BasicFormHeadDetail">
                <cc1:SingleDateTimeField ID="ComeDateY" runat="server" Width="154px" 
                        ReadOnly="True" Font-Bold="False" />
                </td>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel8" runat="server" Text="預計適用期限" Width="117px"/>
                </td>
                <td class="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="TryDateB" runat="server" Width="99px" 
                        InputAreaVisible="True" ReadOnly="True" />
                </td >
                <td class="BasicFormHeadHead" 
                    style="width: 37px; height: 26px; text-align: center;">~
                </td>
                <td class ="BasicFormHeadDetail">
                    <cc1:SingleDateTimeField ID="TryDateE" runat="server" Width="99px" 
                        ReadOnly="True" />
                </td>

                
            </tr>
            <tr>
                <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                    <cc1:DSCLabel ID="DSCLabel9" runat="server" Text="主要職責:" Width="86px" 
                        style="margin-left: 0px"/>
                </td>
            </tr>
            </table>
            <table style="margin-left:4px; width: 660px;" border="0" cellspacing="0" cellpadding="1">
            <tr>
               <td rowspan="1" style="width:60px; text-align: center;" 
                    class="BasicFormHeadHead">內<br />
                   <br />
                   容</td>
               <td class="BasicFormHeadDetail">
                    <cc1:SingleField ID="zhize" runat="server" MultiLine="True" 
                        Width="606px" Height="90px" style="margin-left: 35px" ReadOnly="True" />
               </td> 
            </tr>
            </table>
            <table style="margin-left:4px; width: 660px;" border="0" cellspacing="0" cellpadding="1">
                <tr>
                    <td rowspan="5" style="width:50px;text-align: center;" class="BasicFormHeadHead">用<br />
                        工<br />
                        部<br />
                        門<br />
                        審<br />
                        核</td>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel10" runat="server" Text="（1）本薪" Width="122px"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="bx" runat="server" Width="122px" ReadOnly="True" />
                    </td>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel11" runat="server" Text="（2）主管加給" Width="138px" 
                            Display="True" style="text-align: center"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="zgjg" runat="server" Width="122px" ReadOnly="True"  />
                    </td>
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel12" runat="server" Text="（3）職務加給" Width="122px"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="zwjg" runat="server" Width="122px" ReadOnly="True"  />
                    </td>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel13" runat="server" Text="（4）專業加給" Width="110px" 
                            Height="16px" style="text-align: left"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="zyjg" runat="server" Width="122px" ReadOnly="True"  />
                    </td>
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel14" runat="server" Text="（5）加班補貼" Width="122px"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="jbbt" runat="server" Width="122px" ReadOnly="True" />
                    </td>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel15" runat="server" Text="（1+2+3+4+5）全薪" 
                            Width="142px"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="qx" runat="server" Width="122px" ReadOnly="True" />
                    </td>
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel16" runat="server" Text="加班費計算基數" Width="122px"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="jbfjs" runat="server" Width="122px" 
                            style="margin-bottom: 0px" ReadOnly="True" />
                    </td>
                    <%--<td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:GlassButton ID="xz" runat="server" Text="薪水解密" Width="122px" 
                            onclick="xz_Click"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="password" runat="server" Width="122px" isPassword="True" />
                    </td>--%>
                </tr>
            </table>
            <table style="margin-left:4px; width: 660px;" border="0" cellspacing="0" cellpadding="1">
                <tr>
                    <td rowspan="10" style="width:50px ;text-align: center;" 
                        class="BasicFormHeadHead">約<br />
                        定<br />
                        事<br />
                        項<br />
                    </td>
                    <td class="BasicFormHeadHead" 
                        style="width: 37px; height: 26px; text-align: right;">
                        <cc1:DSCLabel ID="DSCLabel18" runat="server" Text="1、每月10日為公司正常發薪日：
上月26日-當月15日離職，薪資於當月20日發放，當月15日-當月25日離職，薪資於次月10日發放。" Width="605px" style="font-size: x-small"/>
                    </td>
                   
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel20" runat="server" Text="2、同意合理範圍內工作重新調派或輪崗。" 
                            Width="605px"/>
                    </td>
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel22" runat="server" Text="3、自願遵守公司獎懲規範及其它管理辦法之規定。" Width="605px"/>
                    </td>
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel24" runat="server" Text="4、請假:依照請假規則及工作相關規則辦理。" Width="605px"/>
                    </td>
                    
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">&nbsp;5、
                        <cc1:DSCCheckBox ID="DSCLabel19" runat="server" 
                            Text="符合(責任制),全薪中已足額支付加班費（平時及雙休）,除法定節假日外不再另計加班費。" Width="530px" 
                            style="text-align: left"/>
                    </td> 
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCCheckBox ID="DSCLabel21" runat="server" 
                            Text="符合(加班制),因工作需要延長工作時間或休假日出勤以約定基數計發加班費。" Width="605px" Checked="True"/>
                    </td>
                    
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel23" runat="server" Text="6、員工薪資屬保密事項,不外泄﹑不窺探他人薪資。" Width="605px"/>
                    </td>
                    
                </tr>
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel25" runat="server" Text="本人自願同意公司核薪標准及上述約定事項。" Width="605px"/>
                    </td>
                    
                </tr>
                </table>
                <table style="margin-left:4px; width: 660px;" border="0" cellspacing="0" cellpadding="1">
                <tr>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel26" runat="server" Text="員工簽名確認:" Width="122px"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="qm" runat="server" Width="122px" />
                    </td>
                    <td class="BasicFormHeadHead" style="width: 37px; height: 26px">
                        <cc1:DSCLabel ID="DSCLabel27" runat="server" Text="日期:" Width="122px"/>
                    </td>
                    <td class="BasicFormHeadDetail">
                        <cc1:SingleField ID="qmrq" runat="server" Width="122px" />
                    </td>
                    
                </tr>
                </td>
                </tr>
                </table>
     </table>
     <table>
       <tr><td style=" font-size:8pt; ">簽核意見</td></tr>
        <tr>
     <td>
     <div id="div2"  style="position:absolute;" runat="server">
    </div>
    <br />
     </td>
     </tr>
      </table>
      <br />
       <br />
	</div>
    </form>
</body>
</html>
