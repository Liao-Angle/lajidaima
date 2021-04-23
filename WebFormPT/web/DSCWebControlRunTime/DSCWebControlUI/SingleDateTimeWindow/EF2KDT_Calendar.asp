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
'程式名稱: ActiveX\EF2KDT_Calendar.asp
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
    EasyFlow\:calendar {behavior:url(EF2KDT_Calendar.htc)}
    EasyFlow\:time {behavior:url(EF2KDT_Time.htc)}	
}
</STYLE>
<script language="javascript">
function setFixedTime()
{
    if("<%=Request("datetimemode") %>"!="")
    {
	    //日期
	    if("<%=Request("datetimemode") %>" == "0")
	    {
	    }
	    //日期與時間
	    else if("<%=Request("datetimemode") %>" == "1")
	    {
	        document.all.time1.setFixedTime("<%=Request("fixedtime") %>");
	    }
	    //時間
	    else if("<%=Request("datetimemode") %>" == "2")
	    {
	        document.all.time1.setFixedTime("<%=Request("fixedtime") %>");
    		
	    }
	    //日期與時間(完整)
	    else if("<%=Request("datetimemode") %>" == "3")
	    {
	        document.all.time1.setFixedTime("<%=Request("fixedtime") %>");
    		
	    }
	    //日期與時間(分)
	    else if("<%=Request("datetimemode") %>" == "4") 
	    {
	        document.all.time1.setFixedTime("<%=Request("fixedtime") %>");
	    }
    	else //日期(年月)
    	{
    	}
    }
} 
</script>
</HEAD>
<BODY leftmargin="0" topmargin="0" bgcolor="#FFFFFF" style="border:2px #FFFFFF groove;scrollbar-base-color:#D4D0C8;scrollbar-3dlight-color:#D4D0C8;scrollbar-darkshadow-color:#D4D0C8;scrollbar-shadow-color:#808080;scrollbar-face-color:#FFFFFF;scrollbar-highlight-color:#808080;scrollbar-arrow-color:#808080;">

<script language="javascript">

//if(navigator.userLanguage == "zh-tw") {
	var msgSure = "確定";
	var msgCancel = "取消";
//} else {
//	var msgSure = "Sure";
//	var msgCancel = "Cancel";
//}
if("<%=Request("datetimemode") %>"!="")
{
	//日期
	if("<%=Request("datetimemode") %>" == "0")
	{
		if("<%=Request("datetimevalue") %>" != "    /  /  ")
		{
			document.write("<EasyFlow:calendar id='calendar1' serverDate='<%=Date()%>' initDate='"  + "<%=Request("datetimevalue") %>" + "' />"  + "<hr width='95%'><center><input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toSure1()' value='" + msgSure + "' > &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
		else
		{
			document.write("<EasyFlow:calendar id='calendar1' serverDate='<%=Date()%>' />" + "<hr width='95%'> <center><input style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} type=button onclick='toSure1()' value='" + msgSure + "' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}	
	}
	//日期與時間
	else if("<%=Request("datetimemode") %>" == "1")
	{
		if("<%=Request("datetimevalue") %>" != "    /  /     :  :")
		{
		    ddt="<%=Request("datetimevalue") %>";
	        datetag=ddt.split(" ");
			document.write("<EasyFlow:calendar id='calendar1'  serverDate='<%=Date()%>' initDate='"  + datetag[0] + "' />"  + "<EasyFlow:time id='time1' dateMode='2' serverTime='<%=Date() & " " & Hour(Time()) & ":" & Minute(Time()) & ":" & Second(Time())%>'  initTime='" + datetag[1] + "'   fixTimeTag='"  + "<%=Request("fixedtime") %>" + "'/>" + "<hr width='95%'><center><input type=button onclick='toSure0()' value='" + msgSure + "' > &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
		else
		{
			document.write("<EasyFlow:calendar id='calendar1' serverDate='<%=Date()%>' />" + "<EasyFlow:time id='time1' dateMode='2' serverTime='<%=Date() & " " & Hour(Time()) & ":" & Minute(Time()) & ":" & Second(Time())%>'   fixTimeTag='"  + "<%=Request("fixedtime") %>" + "'/>"  + "<hr width='95%'> <center><input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toSure0()' value='" + msgSure + "' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} type=button onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
	}	
	//時間
	else if("<%=Request("datetimemode") %>" == "2")
	{
		if("<%=Request("datetimevalue") %>" != "  :  :  ")
		{
			document.write("<EasyFlow:time id='time1' dateMode='1' serverTime='<%=Date() & " " & Hour(Time()) & ":" & Minute(Time()) & ":" & Second(Time())%>' initTime='" + "<%=Request("datetimevalue") %>" + "'  fixTimeTag='"  + "<%=Request("fixedtime") %>" + "' />" + "<hr width='95%'> <center><input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toSure2()' value='" + msgSure + "' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
		else
		{
			document.write("<EasyFlow:time id='time1' dateMode='1'  serverTime='<%=Date() & " " & Hour(Time()) & ":" & Minute(Time()) & ":" & Second(Time())%>'   fixTimeTag='"  + "<%=Request("fixedtime") %>" + "'/>" + "<hr width='95%'> <center><input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toSure2()' value='" + msgSure + "' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toCancel()' value='" + msgCancel + "' id='cancelt'></center>");
			
		}
		
	}
	//日期與時間(分)
	else if("<%=Request("datetimemode") %>" == "3")
	{
		if("<%=Request("datetimevalue") %>" != "    /  /     :  ")
		{
		    ddt="<%=Request("datetimevalue") %>";
		     //alert('ASP Page datetimevalue : '+ "<%=Request("datetimevalue") %>");
		    //alert('ASP Page fixedtime : '+ "<%=Request("fixedtime") %>");
	        datetag=ddt.split(" ");
	        //alert('datetag[1]'+datetag[1]);
			document.write("<EasyFlow:calendar id='calendar1'  serverDate='<%=Date()%>' initDate='"  + datetag[0] + "' />"  + "<EasyFlow:time id='time1' dateMode=3 serverTime='<%=Date() & " " & Hour(Time()) & ":" & Minute(Time()) & ":" & Second(Time())%>'  initTime='" + datetag[1] + "'   fixTimeTag='"  + "<%=Request("fixedtime") %>" + "'/>" + "<hr width='95%'><center><input type=button onclick='toSure3()' value='" + msgSure + "' > &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
		else
		{
			document.write("<EasyFlow:calendar id='calendar1' serverDate='<%=Date()%>' />" + "<EasyFlow:time id='time1' dateMode=3 serverTime='<%=Date() & " " & Hour(Time()) & ":" & Minute(Time()) %>'    fixTimeTag='"  + "<%=Request("fixedtime") %>" + "'/>"  + "<hr width='95%'> <center><input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toSure3()' value='" + msgSure + "' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} type=button onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
	}
	//時間(分:秒)
	else if("<%=Request("datetimemode") %>" == "4")
	{
		if("<%=Request("datetimevalue") %>" != "  :  ")//Eric
		{
		    ddt="<%=Request("datetimevalue") %>";
		    //alert('ASP Page datetimevalue : '+ "<%=Request("datetimevalue") %>");
		    //alert('ASP Page fixedtime : '+ "<%=Request("fixedtime") %>");
	        datetag=ddt.split(" ");
	         //alert('datetag[1]'+datetag[1]);
			document.write("<EasyFlow:time id='time1' dateMode='4' serverTime='<%=Date() & " " & Hour(Time()) & ":" & Minute(Time()) & ":" & Second(Time())%>'  initTime='" + datetag[0] + "'   fixTimeTag='"  + "<%=Request("fixedtime") %>" + "'/>" + "<hr width='95%'><center><input type=button onclick='toSure2()' value='" + msgSure + "' > &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
		else
		{
			 //alert('ASP Page datetimevalue : '+ "<%=Request("datetimevalue") %>");
		    //alert('ASP Page fixedtime : '+ "<%=Request("fixedtime") %>");
			document.write("<EasyFlow:time id='time1' dateMode='4' serverTime='<%=Date() & " " & Hour(Time()) & ":" & Minute(Time()) & ":" & Second(Time())%>'   fixTimeTag='"  + "<%=Request("fixedtime") %>" + "'/>"  + "<hr width='95%'> <center><input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toSure2()' value='" + msgSure + "' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} type=button onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
	}
    //日期
    else
	{
		if("<%=Request("datetimevalue") %>" != "    /  ")
		{
			document.write("<EasyFlow:calendar id='calendar1' serverDate='<%=Date()%>' initDate='"  + "<%=Request("datetimevalue") %>" + "/01' />"  + "<hr width='95%'><center><input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toSure4()' value='" + msgSure + "' > &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
		else
		{
			document.write("<EasyFlow:calendar id='calendar1' serverDate='<%=Date()%>' />" + "<hr width='95%'> <center><input style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} type=button onclick='toSure4()' value='" + msgSure + "' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button style={padding-top:2px;padding-left:4px;font-size:9pt;width:50px;} onclick='toCancel()' value='" + msgCancel + "' ></center>");
		}
	}
}


function toSure3()
{
	returnValue = calendar1.getValue() + " " + time1.getValue().substring(0, 5);
	window.close();
}
function toSure0()
{
	returnValue = calendar1.getValue() + " " + time1.getValue();
	window.close();
}
function toSure1()
{
	returnValue = calendar1.getValue();
	window.close();
}
function toSure2()
{
	returnValue =  time1.getValue();
	window.close();
}
function toSure4()
{
	returnValue = calendar1.getValue().substring(0, 7);
	window.close();
}
function toCancel()
{
	returnValue=null;
	window.close();
}
</script>
</BODY>
</HTML>
