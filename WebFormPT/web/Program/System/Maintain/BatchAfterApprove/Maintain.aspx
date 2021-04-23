<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="Program_System_Maintain_BatchAfterApprove_Maintain" %>

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
        <cc1:DSCGroupBox ID="ConditionBox" runat="server" Text="處理條件">
        <table border=0>
            <tr>
                <td>
                    <cc1:dsclabel id="DSCLabel1" runat="server" text="SQL條件:" width="79px"></cc1:dsclabel>
                </td>
                <td colspan=2>
                    <cc1:singlefield id="ConditionField" runat="server" width="549px"></cc1:singlefield>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:glassbutton id="GlassButton1" runat="server" height="24px" onclick="GlassButton1_Click"
                        text="取得資料筆數" width="107px"></cc1:glassbutton>
                </td>
                <td>
                    <cc1:dsclabel id="lblResultText" runat="server" height="19px" width="150px" Text="查詢結果:"></cc1:dsclabel>
                </td>
                <td>
                    <cc1:dsclabel id="lblDataCount" runat="server" height="19px" width="518px"></cc1:dsclabel>
                </td>
            </tr>
        </table>
        
        </cc1:DSCGroupBox>
        <br />
        <cc1:DSCGroupBox ID="ProcessBox" runat="server" Text="處理動作">
        <table border=0>
            <tr>
                <td>
                    <cc1:dsclabel id="DSCLabel2" runat="server" text="處理筆數:" width="79px"></cc1:dsclabel>
                </td>
                <td>
                    <cc1:singlefield id="StepCount" runat="server"></cc1:singlefield><cc1:dsclabel id="lblResult" runat="server" width="150px" Text="(未填寫時處理100筆)"></cc1:dsclabel>

                </td>
                <td>
                    <cc1:glassbutton id="GlassButton2" runat="server" height="22px" onclick="GlassButton2_Click"
                        text="開始處理" width="107px"></cc1:glassbutton>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <br />
        </cc1:DSCGroupBox>
    </div>
    </form>
</body>
</html>
