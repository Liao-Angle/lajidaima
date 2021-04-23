Set objFileSystem= CreateObject("Scripting.FileSystemObject")
Set objLogFile = objFileSystem.OpenTextFile("d:\ECP\WebFormPT\Batch\log\SmpForeignTrvlAlert1.Log", 8, True) '8 is for appending

sch = "http://schemas.microsoft.com/cdo/configuration/" 
Set cdoConfig = CreateObject("CDO.Configuration") 
With cdoConfig.Fields 
     .Item(sch & "sendusing") = 2 ' cdoSendUsingPort 
     '.Item(sch & "smtpserver") = "192.168.2.14" 
	 .Item(sch & "smtpserver") = "sysalert.simplo.com.tw"
     .update 
End With 

objLogFile.Write "smtpserver : sysalert.simplo.com.tw "  & vbCrLf


str_date1 = cstr(DatePart("yyyy",now))+"/"+cstr(DatePart("m",now))+"/"+cstr(DatePart("d",now))
str_year = cstr(DatePart("yyyy",now))
str_month = cstr(DatePart("m",now))
'str_Date = cstr(DatePart("d"-2,now))
str_Date = cstr(DatePart("d",DateAdd("d",+15,now)))
if Len(str_month) < 2 then 
	str_month = "0" & str_month
end if 
if Len(str_Date) < 2 then 
	str_Date = "0" & str_Date
end if

str_date = str_year+"/"+str_month+"/"+str_Date
objLogFile.Write Now() & " // str_date1: " & str_date & vbCrLf
str_date2 = cstr(DateAdd("m",-2,now))

str_date2 = cstr(DatePart("yyyy",DateAdd("m",-2,now)))+"/"+cstr(DatePart("m",DateAdd("m",-2,now)))+"/"+cstr(DatePart("d",DateAdd("m",-2,now)))
str_year = cstr(DatePart("yyyy",DateAdd("m",-2,now)))
str_month = cstr(DatePart("m",DateAdd("m",-2,now)))
str_Date1 = cstr(DatePart("d",DateAdd("m",-2,now)))
if Len(str_month) < 2 then 
	str_month = "0" & str_month
end if 
if Len(str_Date1) < 2 then 
	str_Date1 = "0" & str_Date1
end if
str_lastmonth = str_year+"/"+str_month+"/"+str_Date1

objLogFile.Write Now() & " // str_lastmonth: " & str_lastmonth & vbCrLf


Set oArgs=WScript.Arguments

objLogFile.Write Now() & " oArgs=WScript.Arguments "  & vbCrLf


strDBServerIP = "192.168.2.138"
strAPServerIP = "192.168.2.137"
strDataBase = "WebFormPT"

'strsqlCnn = "driver={SQL Server};server=" & strDBServerIP & ";uid=sa;pwd=SMPADMEF2K;database=" & strDataBase
strsqlCnn = "driver={SQL Server};server=" & strDBServerIP & ";uid=sa;pwd=SMPADMEF2K;database=" & strDataBase
Set sqlcnn = WScript.CreateObject ("ADODB.Connection")
sqlcnn.ConnectionTimeout = 60
sqlcnn.Open strsqlCnn

objLogFile.Write Now() & " connect db "  & vbCrLf

strSQL = "  select orgId, SMWYAAA004, SMWYAAA002, deptInfo, tripUser, StartTrvlDate, EndTrvlDate, site, TrvlDesc from SmpForeignTrvlAlert t where EndTrvlDate >=getdate()  and StartTrvlDate <=getDate() "
strSql = strSql & " order by SMWYAAA004, SMWYAAA002 "
objLogFile.Write Now() & " SQL Statement:" & strSql & vbCrLf

Set sqlEC = sqlcnn.Execute(strSql)
str_today = cstr(DatePart("yyyy",now))+"/"+cstr(DatePart("m",now))+"/"+cstr(DatePart("d",now)) 

If not sqlEC.eof Then
	strText = "<font lang=""EN-US"" style=""font-size: 9pt; font-family: Tahoma"">[至今日 <"&str_today&"> 出差人員清單:]"
	strText = strText & "<table border=""0"" cellspacing=""1"" cellpadding=""0"" bgcolor=""#C0C0C0"">"
	strText = strText & "<tr style=""font-size: 9pt; font-family: Tahoma""><td>公司別</td><td>單別</td><td>單號</td><td>部門</td><td>姓名</td><td>出差日起</td><td>出差日訖</td><td>出差地點</td><td>出差事由</td></tr>"
	intCnt = 0
	While not sqlEC.eof
		intCnt = intCnt + 1
		
		strText = strText & "<tr style=""font-size: 9pt; font-family: Tahoma""><td bgcolor=""#FFFFFF"" width=30>" & sqlEC("orgId") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=150>" & sqlEC("SMWYAAA004") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=80>" & sqlEC("SMWYAAA002") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=80>" & sqlEC("deptInfo") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=80>" & sqlEC("tripUser") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=100>" & sqlEC("StartTrvlDate") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=90>" & sqlEC("EndTrvlDate") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=90>" & sqlEC("site") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"">" & sqlEC("TrvlDesc") & "</td></tr>"
		sqlEC.MoveNext
	Wend
	strText = strText & "</table>"
	
	Set sqlEC = nothing
	'objLogFile.Write Now() & " strText" & strText & vbCrLf
	
	If intCnt > 0 Then
		Set cdoMessage = CreateObject("CDO.Message") 
		With cdoMessage
			Set .Configuration = cdoConfig 
			.From = "ecp_admin@simplo.com.tw" 
			.To = "melinda_ho@simplo.com.tw"
			.Bcc = "eva_fan@simplo.com.tw"
			'.To = "yuyi_chen@mail.simplo.com.tw;eva_chen@mail.simplo.com.tw"
			'.Cc = "Mac_Chen@mail.simplo.com.tw;jerry_chen@mail.simplo.com.tw;Grace_Fan@mail.simplo.com.tw"
			.Subject = "[New EasyFlow通知] <"&str_today&">出差人員清單"
			.htmlbody = strText
			.Send
		End With 
					
		Set cdoMessage = Nothing 
	End If
	
end if




Set sqlcnn = Nothing

objLogFile.Close
Set objFileSystem = nothing
