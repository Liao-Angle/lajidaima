<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SingleDateTimeWindow.aspx.cs" Inherits="DSCWebControlUI_SingleDateTimeWindow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
</head>
<body style="margin-top:0px;margin-left:0px;margin-bottom:0px;margin-right:0px">
    <form id="form1" runat="server">
    <div>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
            CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
            ForeColor="Black" Height="180px" Width="200px">
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <SelectorStyle BackColor="#CCCCCC" />
            <WeekendDayStyle BackColor="#FFFFCC" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
        </asp:Calendar>
        </div>
        <asp:Table ID="Table1" runat="server" Font-Size="9pt" Width="198px" BorderWidth="0px" CellPadding="0" CellSpacing="0">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" RowSpan="2" Width="33%">
                    <asp:TextBox ID="HH" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="100%" OnTextChanged="HH_TextChanged" AutoPostBack="True"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server" Width="5px"><asp:Button ID="HU" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" OnClick="HU_Click" 
            Height="10px" Text="^" Width="13px" /></asp:TableCell>
                <asp:TableCell runat="server" RowSpan="2" Width="33%"><asp:TextBox ID="MM" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="100%" OnTextChanged="MM_TextChanged" AutoPostBack="True"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server" Width="5px"><asp:Button ID="MU" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" OnClick="MU_Click" 
            Height="10px" Text="^" Width="13px" /></asp:TableCell>
                <asp:TableCell runat="server" RowSpan="2" Width="33%"><asp:TextBox ID="SS" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="100%" OnTextChanged="SS_TextChanged" AutoPostBack="True"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server" Width="5px"><asp:Button ID="SU" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" OnClick="SU_Click" 
            Height="10px" Text="^" Width="13px" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Button ID="HD" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" OnClick="HD_Click" 
            Height="10px" Text="v" Width="13px" /></asp:TableCell>
                <asp:TableCell runat="server"><asp:Button ID="MD" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" OnClick="MD_Click" 
            Height="10px" Text="v" Width="13px" /></asp:TableCell>
                <asp:TableCell runat="server"><asp:Button ID="SD" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" OnClick="SD_Click" 
            Height="10px" Text="v" Width="13px" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="ConfirmButton" runat="server" Text="確定選擇" Height="27px" OnClick="ConfirmButton_Click" Width="198px" /><br />
    </form>
</body>
</html>
