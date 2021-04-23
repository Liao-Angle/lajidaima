<%@ Language=VBScript Codepage=65001 %>
<%
Response.Buffer=true
Response.CacheControl = "no-cache"
Response.Expires = -1                             ' works with IE 4.0 browsers. 
Response.AddHeader "Pragma","no-cache"            ' works with Proxy Servers. 
Response.AddHeader "cache-control", "no-store"    ' works with IE 5.0 browsers. 
%>
<%
'==================================================================================
'==================================================================================
'專案名稱: EasyFlow Fassade
'程式名稱: ActiveX\EF2KDT_Time.asp
'原始版本: 10.01.0001
'  撰寫者: 余英蘭
'撰寫日期: 2004/01/01
'
'版權聲明: Copyright(c) 1999-2001, 鼎新電腦股份有限公司  版權所有 (02)8667-2776
'          本電腦程式受著作權法及國際公約保護。
'          凡未經授權擅自複製或散佈本程式的全部或部分，將遭受最嚴厲的民、刑事處分。
'
'修正摘要:
'
'==================================================================================
'==================================================================================
%>
<HTML xmlns:EasyFlow>
<HEAD>
	<TITLE>EasyFlow</TITLE>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<STYLE>
@media all{
    EasyFlow\:time {behavior:url(EF2KDT_Time.htc)}
}
</STYLE>
<SCRIPT LANGUAGE=javascript>
function showHTML()
{
	//alert(TextBox.id);
	//TextBox.setFocus();
	//TextBox.setDfValue("20");
	//alert(TextBox.innerHTML);
}

function changeHTML()
{
	//TextBox.setOptionSource("menuform","itemlist");
	//var rule=" strType:CMDate;isEmpty:Y;strFieldName:test;strKeyFilter:Int;"
	//TextBox.setRule(rule);
	//TextBox.sayHello();
	
}
</SCRIPT>
</HEAD>
<BODY leftmargin="0" topmargin="20" bgcolor="#FFFFFF" style="border:2px #FFFFFF groove;scrollbar-base-color:#D4D0C8;scrollbar-3dlight-color:#D4D0C8;scrollbar-darkshadow-color:#D4D0C8;scrollbar-shadow-color:#808080;scrollbar-face-color:#FFFFFF;scrollbar-highlight-color:#808080;scrollbar-arrow-color:#808080;">
	<EasyFlow:time id="time1"  value="Date" initTime="12:00:00"/>
</BODY>
</HTML>