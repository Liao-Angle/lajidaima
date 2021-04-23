<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SampleProgram_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:dscgroupbox id="DSCGroupBox1" runat="server" height="62px" text="清單輸入區" width="663px"><cc1:DSCLabel id="DSCLabel1" runat="server" Text="專案代號" Width="94px"></cc1:DSCLabel> <cc1:SingleField id="ProjectID" runat="server" Width="185px"></cc1:SingleField><BR /><cc1:DSCLabel id="DSCLabel2" runat="server" Text="專案名稱" Width="94px"></cc1:DSCLabel>
            &nbsp;<cc1:SingleField id="ProjectName" runat="server" Width="185px"></cc1:SingleField><BR /><cc1:DSCLabel id="DSCLabel3" runat="server" Text="主持人" Width="94px"></cc1:DSCLabel>
            <cc1:SingleOpenWindowField ID="LeaderGUID" runat="server" showReadOnlyField="True"
                Width="264px" guidField="OID" keyField="id" serialNum="001" tableName="Users" />
        </cc1:dscgroupbox>    
        <cc1:DataList ID="ProjectDetailList" runat="server" Height="284px" Width="660px" />
    </div>
    </form>
</body>
</html>
