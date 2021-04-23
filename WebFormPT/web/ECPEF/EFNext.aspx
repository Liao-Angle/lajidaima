<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EFNext.aspx.cs" Inherits="ECPEF_Next" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>未命名頁面</title>
	<script language="javascript">
		function redir() {
			var isSMWL = false;
			try {
				var wobj = null;
				//國昌:先不要往上帶, 否則萬一此時有人點其他的功能, 畫面會被蓋掉
				wobj = window.top.parent.getPanelWindowObject('<%=Request.QueryString["ParentPanelID"]%>'); //這行可以取得該panelID的PanelWindow中代表內容的window HTML物件
				if (wobj != null) {
					wobj.refreshDataList('ListTable'); //這行可以呼叫該視窗的refreshDataList方法, 此方法為WebFormBasePage提供
					isSMWL = true;
					wobj.clickDataList('ListTable', 1); //這行可以呼叫該視窗的clickDataList方法, 此方法為WebFormBasePage提供
					window.top.parent.Panel_Close_Silence('<%=Request.QueryString["CurPanelID"]%>'); //這行可以直接關閉目前視窗
				}
			} catch (e) {
				if (wobj != null && isSMWL == true) {
					alert('errs:' + e.description);
				}
				window.top.parent.Panel_Close_Silence('<%=Request.QueryString["CurPanelID"]%>'); //這行可以直接關閉目前視窗
			}
		}
		redir();
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	</div>
	</form>
</body>
</html>
