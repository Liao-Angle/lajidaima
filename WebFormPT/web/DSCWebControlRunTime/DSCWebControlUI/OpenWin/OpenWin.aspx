<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpenWin.aspx.cs" Inherits="DSCWebControlRunTime_DSCWebControlUI_OpenWin_OpenWin" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>請選擇資料</title>
    <script>
        function search(event)
        {            
            if (event.keyCode == 13) {                
                document.getElementById('S_ValueText').onblur();
                document.getElementById('S_sfSearchButtonProxy').onblur();
                //GlassButton_Click('SearchButton');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"  >
    <div>
        <table border=0 cellspacing=1 cellpadding=0>
            <tr>
                <td style="width: 359px" nowrap=true>
                    <cc1:SingleDropDownList ID="FieldSelect" runat="server" Width="119px" />
                    <cc1:SingleDropDownList ID="OperationList" runat="server" Width="76px" />
                    <cc1:SingleField ID="ValueText" runat="server" Width="127px"  onKeyUpClientScript="search(event)" />
                </td>
                <td style="width: 161px">                    
                    <cc1:GlassButton ID="SearchButton" runat="server" Text="重新搜尋" Width="66px" OnClick="SearchButton_Click" /><cc1:GlassButton ID="SendButton" runat="server" Text="確定選擇" Width="66px" OnClick="SendButton_Click" />
                    <cc1:SingleField ID="sfSearchButtonProxy" runat="server" OnTextChanged="sfSearchButtonProxy_TextChanged" Display="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="2" style="height: 339px">
                    &nbsp;<cc1:DataList ID="DataGrids" runat="server" Height="323px" IsOpenWinUse="True"
                        Width="510px" OnShowRowData="DataGrids_ShowRowData" OnChangePageSizeClick="DataGrids_ChangePageSizeClick" OnFirstPageClick="DataGrids_FirstPageClick" OnNextPageClick="DataGrids_NextPageClick" OnPrevPageClick="DataGrids_PrevPageClick" OnsetClickData="DataGrids_setClickData" />
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
