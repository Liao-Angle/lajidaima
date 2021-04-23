Set objFileSystem= CreateObject("Scripting.FileSystemObject")
Set objLogFile = objFileSystem.OpenTextFile("d:\ECP\WebFormPT\Batch\log\SmpExpiredAccount1.Log", 8, True) '8 is for appending

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

'SMP ERP Database
'strsqlCnn = "driver={SQL Server};server=192.168.2.17;AutoTranslate=No;Trusted_Connection=yes;Network=dbnmpntw;"
'strsqlCnn = "driver={SQL Server};server=192.168.2.17;uid=MIS_ERP;pwd=miserp;database=SMP"
strsqlCnn = "driver={SQL Server};server=192.168.2.17;uid=EASYFLOW;pwd=easyflow8;database=SMP"
Set conDSC = WScript.CreateObject ("ADODB.Connection")
conDSC.ConnectionTimeout = 60
conDSC.Open strsqlCnn
objLogFile.Write Now() & " connect db ERP Database "  & vbCrLf

'SMP New EF Database
strsqlCnn = "driver={SQL Server};server=192.168.2.138;uid=sa;pwd=SMPADMEF2K;database=WebFormPT"
Set conEF = WScript.CreateObject ("ADODB.Connection")
conEF.ConnectionTimeout = 60
conEF.Open strsqlCnn

objLogFile.Write Now() & " connect db  EF Database "  & vbCrLf

strsqlquitemp="select MV001,MV022,MV002,MV004 from CMSMV where MV022 > (getdate()-180) and MV022<=getdate() and MV001 not in ('4960','4251','4632','4747','4988','4409') "
objLogFile.Write Now() & " // SQL Statement:" & strsqlquitemp & vbCrLf
Set sqlempquitrs = conDSC.Execute(strsqlquitemp)
On Error Resume Next
While not sqlempquitrs.EOF
	strSql="Select id, substring(mailAddress, 1 ,(charindex('@', mailAddress)-1)) adAccount,userName from NaNa.dbo.Users where id='"&RTrim(sqlempquitrs("MV001"))&"' and leaveDate is null and id not in (select distinct signUserId from SmpNonSignList where creationDate >=  (getdate()-30)) "
	'objLogFile.Write Now() & " // SQL Statement:" & strSql & vbCrLf
	Set efEmprs = conEF.Execute(strSql)
	If not efEmprs.eof Then
		'objLogFile.Write Now() & " // in sql "  & vbCrLf
		If RTrim(sqlempquitrs("MV001"))=RTrim(efEmprs("id")) Then
			strDate = Trim(sqlempquitrs("MV022"))
			strExpireUserId = Trim(efEmprs("id"))
			strExpireUserAd = Trim(efEmprs("adAccount"))
			strExpireUserName = Trim(efEmprs("userName"))
			strDate = Mid(strDate, 1, 4) & "/" & Mid(strDate, 5, 2) & "/" & Mid(strDate, 7, 2)
			strUdUsr="update NaNa.dbo.Users set leaveDate='"+strDate+"' where id='" + RTrim(sqlempquitrs("MV001"))+"'"
					
			objLogFile.Write Now() & " // SQL Statement:" & strUdUsr & vbCrLf
			Set rs = conEF.Execute(strUdUsr)
			Set rs = nothing
		    Set cdoMessage = CreateObject("CDO.Message")   
			With cdoMessage 
				Set .Configuration = cdoConfig 
					.From = "ecp_admin@simplo.com.tw" 
					'.To = "cl_chang@mail.simplo.com.tw;Eva_Fan@mail.simplo.com.tw;Dustin_Cheng@mail.simplo.com.tw"
					.To = "Eva_Fan@simplo.com.tw;cl_chang@simplo.com.tw"
					.Cc = "ted_chen@simplo.com.tw"
					.Subject ="ECP employee expired account ->  工號: " +strExpireUserId+ " , 姓名: "+strExpireUserName + " ,英文名: " + strExpireUserAd
					.htmlbody = "ECP employee expired account -> 工號: " +strExpireUserId+ " , 姓名: "+strExpireUserName + " ,英文名:" + strExpireUserAd +"<br>Exit day is "+strDate 
					.Send 
			End With 
			Set cdoMessage = Nothing
		End If
		efEmprs.movenext
	End If

	sqlempquitrs.movenext
Wend

	
objLogFile.Write Now() & " End........ " &  vbCrLf

Set conDSC = nothing
Set conEF = nothing

objLogFile.Write vbCrLf & vbCrLf
objLogFile.Close     
Set objFileSystem = nothing
