<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Calendar_Calendar" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
    <link href="Calendar.css" rel="stylesheet" type="text/css" />
<script language=javascript>
function clickItem(guid)
{
    parent.openWindowGeneral("<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string001", "行事曆檢視") %>", "Calendar/showDetail.aspx?GUID="+guid, 450, 400, "", true, true);
}
function addNewItem()
{
    parent.openWindowGeneral("<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string002", "新增行事曆項目") %>", "Calendar/newDetail.aspx", 450, 400, "", true, true);
}
function deleteAllItem()
{
    if(confirm('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string003", "你確定要刪除本日所有行事曆項目嗎?") %>')){
        postData="Method=DeleteAllItem&Date="+document.getElementById('TODAY').innerHTML;
        xmlhttp=createXMLHTTP();
        xmlhttp.open('POST', 'CalendarProcess.aspx' , false);
        xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
        xmlhttp.send(postData);

        window.location.reload();
    }
}
function deleteItem(guid)
{
    if(confirm('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string004", "你確定要刪除此行事曆項目嗎?") %>')){
        postData="Method=DeleteItem&GUID="+guid;
        xmlhttp=createXMLHTTP();
        xmlhttp.open('POST', 'CalendarProcess.aspx' , false);
        xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
        xmlhttp.send(postData);
        window.location.reload();
    }
}
function createXMLHTTP() {             
    if (window.ActiveXObject) { 
    　　try {
                    return new ActiveXObject("Msxml2.XMLHTTP");
　　          } catch (e) 
                     {
                        try {
                                return new ActiveXObject("Microsoft.XMLHTTP");
                              } catch (e2) 
                              {
                                return null;            
                              }
                    }
        } else if (window.XMLHttpRequest) 
                     {
                            return new XMLHttpRequest();
　                  }  else { return null; }             
}

</script>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div style="vertical-align:top">
        <table border=0 cellspacing=0 cellpadding=0 width=100%  >
        <tr>
            <td colspan=3>
                <asp:Calendar ID="Calendars" runat="server" DayNameFormat="Shortest" Font-Size="9pt"
                    Width="100%" CssClass="CalendarStyle" OnSelectionChanged="Calendars_SelectionChanged">
                    <DayHeaderStyle CssClass="DayHeaderStyle" />
                    <SelectedDayStyle CssClass="SelectedDayStyle" />
                    <TodayDayStyle CssClass="TodayDayStyle" />
                    <SelectorStyle CssClass="SelectorStyle" />
                    <DayStyle CssClass="DayStyle" />
                    <OtherMonthDayStyle CssClass="OtherMonthDayStyle" Font-Overline="False" />
                    <TitleStyle CssClass="TitleStyle" />
                </asp:Calendar>
            </td>
        </tr>
        <tr class='DayItemTitle'>
            <td width=100% ><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string005", "本日行程")%> (<span id="TODAY"><asp:Literal ID="TodayLiteral" runat="server"></asp:Literal></span>)
            </td>
            <td width=20px><span style="cursor:pointer" onclick='addNewItem()'><img style="cursor:pointer" src="Images/imgNew.gif" border=0 alt="<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string006", "新增本日行程")%>" /></span></td>
            <td width=20px><span style="cursor:pointer" onclick='deleteAllItem()'><img style="cursor:pointer" src="Images/imgDelete.gif" border=0 alt="<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string007", "刪除本日所有行程")%>" /></span></td>
        </tr>
        <tr>
            <td colspan=3 height=2px bgcolor=Black></td>
        </tr>
        <!--進行中-->
        <tr class='DayItemSubTitle' height=15px>
            <td width=100% ><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string008", "進行中")%>
            </td>
            <td width=20px></td>
            <td width=20px></td>
        </tr>
        <tr>
            <td colspan=3 height=2px bgcolor=Black></td>
        </tr>
        <tr>
            <td colspan=3>
        <asp:Panel ID="DayItemProgress" runat="server" Height="100%" Width="100%" ScrollBars="Auto" Wrap="False">
            <asp:Literal ID="ProcessLiteral" runat="server"></asp:Literal></asp:Panel>
            </td>
        </tr>
        <!--未進行-->
        <tr class='DayItemSubTitle' height=15px>
            <td width=100% ><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string009", "未進行")%>
            </td>
            <td width=20px></td>
            <td width=20px></td>
        </tr>
        <tr>
            <td colspan=3 height=2px bgcolor=Black></td>
        </tr>
        <tr>
            <td  colspan=3>
        <asp:Panel ID="DayItemNotYet" runat="server" Height="100%" Width="100%" ScrollBars="Auto" Wrap="False">
            <asp:Literal ID="NotYetLiteral" runat="server"></asp:Literal></asp:Panel>
            </td>
        </tr>
        <!--已進行-->
        <tr class='DayItemSubTitle' height=15px>
            <td width=100% ><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string010", "已進行")%>
            </td>
            <td width=20px></td>
            <td width=20px></td>
        </tr>
        <tr>
            <td colspan=3 height=2px bgcolor=Black></td>
        </tr>
        <tr>
            <td  colspan=3>
        <asp:Panel ID="DayItemYet" runat="server" Height="100%" Width="100%" ScrollBars="Auto" Wrap="False">
            <asp:Literal ID="YetLiteral" runat="server"></asp:Literal></asp:Panel>
            </td>
        </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
