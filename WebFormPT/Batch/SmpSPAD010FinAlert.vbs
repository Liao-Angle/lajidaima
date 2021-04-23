Set objFileSystem= CreateObject("Scripting.FileSystemObject")
Set objLogFile = objFileSystem.OpenTextFile("d:\ECP\WebFormPT\Batch\log\SmpSPAD010FinAlert.Log", 8, True) '8 is for appending

sch = "http://schemas.microsoft.com/cdo/configuration/" 
Set cdoConfig = CreateObject("CDO.Configuration") 
With cdoConfig.Fields 
     .Item(sch & "sendusing") = 2 ' cdoSendUsingPort 
     '.Item(sch & "smtpserver") = "192.168.2.14" 
	 .Item(sch & "smtpserver") = "sysalert.simplo.com.tw"
     .update 
End With 

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

strDBServerIP = "192.168.2.138"
strAPServerIP = "192.168.2.137"
strDataBase = "WebFormPT"

'strsqlCnn = "driver={SQL Server};server=" & strDBServerIP & ";uid=sa;pwd=SMPADMEF2K;database=" & strDataBase
strsqlCnn = "driver={SQL Server};server=" & strDBServerIP & ";uid=sa;pwd=SMPADMEF2K;database=" & strDataBase
Set sqlcnn = WScript.CreateObject ("ADODB.Connection")
sqlcnn.ConnectionTimeout = 60
sqlcnn.Open strsqlCnn

objLogFile.Write Now() & " connect db "  & vbCrLf

strSQL = " select ei.orgId, ya.SMWYAAA002 , ya.SMWYAAA004 , ya.SMWYAAA006  "
strSql = strSql & " , ya.SMWYAAA008+'-'+ ya.SMWYAAA009 writeMember, uw.mailAddress writeEmail   "
strSql = strSql & " , ur.id+'-'+ ur.userName relationShipMember , ur.mailAddress relaEmail ,ya.SMWYAAA017  "
strSql = strSql & " , wi.workItemName,u.userName,wi.createdTime, ya.SMWYAAA005 "
strSql = strSql & " from  [NaNa].dbo.WorkItem wi, [NaNa].dbo.ProcessInstance pi, [WebFormPT].[dbo].SMWYAAA ya , [NaNa].dbo.WorkAssignment wa , [NaNa].dbo.Users u, [NaNa].dbo.Users uw, [NaNa].dbo.Users ur, [WebFormPT].dbo.SmpTripBilling eb, [WebFormPT].dbo.EmployeeInfo ei "
strSql = strSql & " where wi.contextOID=pi.contextOID and  pi.serialNumber=ya.SMWYAAA005  and wa.workItemOID=wi.OID  and u.id='4864' and wi.completedTime is null and ya.SMWYAAA020='I' "
strSql = strSql & "  and u.OID=wa.assigneeOID and uw.id= ya.SMWYAAA008 and ur.OID=eb.OriginatorGUID and  ya.SMWYAAA004 ='國內出差旅費核銷單' and eb.SheetNo = ya.SMWYAAA002 "
strSql = strSql & " and ya.SMWYAAA017   < getdate()-10 and  ei.empGUID = ur.OID "
'strSql = strSql & " order by  ya.SMWYAAA008 asc "
objLogFile.Write Now() & " SQL Statement:" & strSql & vbCrLf

Set sqlEC = sqlcnn.Execute(strSql)
str_today = cstr(DatePart("yyyy",now))+"/"+cstr(DatePart("m",now))+"/"+cstr(DatePart("d",now)) 

If not sqlEC.eof Then
	While not sqlEC.eof
		'intCnt = intCnt + 1
		strText = "<font lang=""EN-US"" style=""font-size: 9pt; font-family: Tahoma"">[列印國內出差旅費核銷單後送至財務部]"
		'strText = strText & "<font lang=""EN-US"" style=""font-size: 9pt; font-family: Tahoma"">若已列印送至董事長室請忽略此單據"
		strText = strText & "<table border=""0"" cellspacing=""1"" cellpadding=""0"" bgcolor=""#C0C0C0"">"
		strText = strText & "<tr style=""font-size: 9pt; font-family: Tahoma""><td>公司別</td><td>單別</td><td>單號</td><td>填表人</td><td>關係人</td><td>填表日期</td><td>財務收件日</td><td>主旨</td></tr>"		
		strText = strText & "<tr style=""font-size: 9pt; font-family: Tahoma""><td bgcolor=""#FFFFFF"" width=30>" & sqlEC("orgId") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=150>" & sqlEC("SMWYAAA004") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=80><a href=http://"& strAPServerIP &"/ECP?runMethod=showReadOnlyForm&processSerialNumber="& sqlEC("SMWYAAA005") &"> " & sqlEC("SMWYAAA002") & "</a></td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=80>" & sqlEC("writeMember") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=100>" & sqlEC("relationShipMember") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=90>" & sqlEC("SMWYAAA017") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=90>" & sqlEC("createdTime") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"">" & sqlEC("SMWYAAA006") & "</td></tr>"
		strText = strText & "</table>"
		
		writeEmail = sqlEC("writeEmail")
		relaEmail = sqlEC("relaEmail")
		
		mailUser = writeEmail
		If (mailUser <> relaEmail) then
			mailUser = mailUser & ";" & relaEmail
		End if 
		
		Set cdoMessage = CreateObject("CDO.Message") 
		With cdoMessage
			Set .Configuration = cdoConfig 
			.From = "ecp_admin@simplo.com.tw" 
			.To = mailUser
			'.To = "eva_fan@simplo.com.tw"
			.Bcc = "eva_fan@simplo.com.tw"
			.Cc = "Yodo_Yang@simplo.com.tw"
			.Subject = "[New EasyFlow通知] 列印國內出差旅費核銷單後送至財務部"
			.htmlbody = strText
			.Send
		End With 
					
		Set cdoMessage = Nothing 
		objLogFile.Write Now() & " strText" & strText & vbCrLf		
		sqlEC.MoveNext
	Wend
	
	
	Set sqlEC = nothing

	
end if

Set sqlcnn = Nothing
objLogFile.Close
Set objFileSystem = nothing
