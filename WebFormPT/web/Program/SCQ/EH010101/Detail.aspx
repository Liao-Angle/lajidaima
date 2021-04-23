<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SmpProgram_Maintain_SPPM001_Detail" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>明細</title>
</head>
<body>
    <form id="form1" runat="server">

    <asp:Label ID="Label1" runat="server" Text="三辊闸进出记录" Font-Bold="True" 
            Font-Size="Small"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="gvAchievementList" runat="server" Font-Size="X-Small"  
            Font-Bold="False" Width="351px" 
            onrowdatabound="gvAchievementList_RowDataBound">
            <Columns>
            </Columns>
            <FooterStyle Font-Bold="False" Font-Italic="False" />
            <HeaderStyle CssClass="BasicFormHeadHead" Font-Bold="False" Font-Size="X-Small" 
                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                ForeColor="Black" />
        </asp:GridView>
    </form>
</body>
</html>
