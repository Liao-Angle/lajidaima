<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Program_DSCOrgService_Maintain_UsersMaintain_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 662px;font-size:9pt">
            <tr>
                <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel1" runat="server" Text="物件識別碼" Width="100px" />
                    </td>
                <td style="width: 180px">
                    <cc1:SingleField ID="OIDF" runat="server" Width="100%" ReadOnly="true" />
                </td>
                <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel2" runat="server" Text="物件版本" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="objectVersionF" runat="server" Width="100%" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel3" runat="server" Text="使用者代號" Width="80px" />
                    </td>
                <td style="width: 180px"><cc1:SingleField ID="idF" runat="server" Width="100%" ReadOnly="true" />
                </td>
                <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel4" runat="server" Text="使用者姓名" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="userNameF" runat="server" Width="100%" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel5" runat="server" Text="密碼" Width="80px" />
                    </td>
                <td style="width: 180px"><cc1:SingleField ID="passwordF" runat="server" Width="100%" ReadOnly="true" />
                </td>
                <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel6" runat="server" Text="離職日期" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="leaveDateF" runat="server" Width="100%" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel7" runat="server" Text="工作時間表物件識別碼" Width="80px" />
                    </td>
                <td style="width: 180px"><cc1:SingleField ID="referCalendarF" runat="server" Width="100%" ReadOnly="true" />
                </td>
                <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel8" runat="server" Text="密碼驗證方式" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="identificationTypeF" runat="server" Width="100%"  ReadOnly="true"/>
                </td>
            </tr>
            <tr>
                <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel9" runat="server" Text="語系" Width="80px" />
                    </td>
                <td style="width: 180px; height: 25px;"><cc1:SingleField ID="languageTypeF" runat="server" Width="100%"  ReadOnly="true"/>
                </td>
                <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel10" runat="server" Text="email信箱" Width="80px" />
                    </td>
                <td style="width: 145px; height: 25px;"><cc1:SingleField ID="mailAddressF" runat="server" Width="100%"  ReadOnly="true"/>
                </td>
            </tr>
            <tr>
                 <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel11" runat="server" Text="電話號碼" Width="80px" />
                    </td>
                <td style="width: 180px"><cc1:SingleField ID="phoneNumberF" runat="server" Width="100%"  ReadOnly="true"/>
                </td>
                 <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel12" runat="server" Text="預設流程系統主機識別碼" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="workflowServerOIDF" runat="server" Width="100%"  ReadOnly="true"/>
                </td>
            </tr>
            <tr>
                 <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel13" runat="server" Text="是否啟用代理人" Width="80px" />
                    </td>
                <td style="width: 180px"><cc1:SingleField ID="enableSubstituteF" runat="server" Width="100%"  ReadOnly="true"/>
                </td>
                 <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel14" runat="server" Text="代理起始時間" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="startSubstituteTimeF" runat="server" Width="100%"  ReadOnly="true"/>
                </td>
            </tr>
            <tr>
                 <td style="width: 123px"><cc1:DSCLabel ID="DSCLabel15" runat="server" Text="代理截止時間" Width="80px" />
                    </td>
                <td style="width: 180px"><cc1:SingleField ID="endSubstituteTimeF" runat="server" Width="100%"  ReadOnly="true"/>
                </td>
                <td style="width: 134px"><cc1:DSCLabel ID="DSCLabel16" runat="server" Text="工時成本單位" Width="80px" />
                    </td>
                <td style="width: 145px"><cc1:SingleField ID="costF" runat="server" Width="100%"  ReadOnly="true"/>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
