Set objFileSystem= CreateObject("Scripting.FileSystemObject")
Set objLogFile = objFileSystem.OpenTextFile("d:\ECP\WebFormPT\Batch\log\SmpCheckAbsenceFormOverseas.Log", 8, True) '8 is for appending

sch = "http://schemas.microsoft.com/cdo/configuration/" 
Set cdoConfig = CreateObject("CDO.Configuration") 
With cdoConfig.Fields 
     .Item(sch & "sendusing") = 2 ' cdoSendUsingPort 
	 .Item(sch & "smtpserver") = "sysalert.simplo.com.tw"
     .update 
End With 

str_today = cstr(DatePart("yyyy",now))+"/"+cstr(DatePart("m",now))+"/"+cstr(DatePart("d",now)) 
objLogFile.Write Now() & " // now: " & str_today & vbCrLf

Set oArgs=WScript.Arguments
'Connection
strDBServerIP = "192.168.2.138"
strDataBase = "WebFormPT"
strsqlCnn = "driver={SQL Server};server=" & strDBServerIP & ";uid=sa;pwd=SMPADMEF2K;database=" & strDataBase
Set sqlcnn = WScript.CreateObject ("ADODB.Connection")
sqlcnn.ConnectionTimeout = 60
sqlcnn.Open strsqlCnn

objLogFile.Write Now() & " connect db "  & vbCrLf
strSQL = "select count(*) cnt from SmpAbsenceOverseasV"
objLogFile.Write Now() & " SQL Statement:" & strSql & vbCrLf
Set sqlCntV = sqlcnn.Execute(strSql)
str_today = cstr(DatePart("yyyy",now))+"/"+cstr(DatePart("m",now))+"/"+cstr(DatePart("d",now)) 

If not sqlCntV.eof Then
	While not sqlCntV.eof
		strCntV = sqlCntV("cnt")
		sqlCntV.MoveNext
	Wend
	Set sqlCntV = nothing
end if

strSQL = "select count(*) cnt from SmpAbsenceOverseas"
objLogFile.Write Now() & " SQL Statement:" & strSql & vbCrLf
Set sqlCnt = sqlcnn.Execute(strSql)
If not sqlCnt.eof Then
	While not sqlCnt.eof
		strCnt = sqlCnt("cnt")
		sqlCnt.MoveNext
	Wend
	Set sqlCnt = nothing
end if

if strCntV <> strCnt then
	Set cdoMessage = CreateObject("CDO.Message") 
		With cdoMessage 
			Set .Configuration = cdoConfig 
			.From = "ecp_admin@simplo.com.tw" 
			.To = "cl_chang@simplo.com.tw;brian_chang@simplo.com.tw"
    		.Subject = "海外支援部請假單筆數不一致"
			.htmlbody = "All,<br><br>海外支援部請假單資料同步筆數不一致, 請確認.<br>Source View: SmpAbsenceOverseasV, count=" & strCntV & "<br>Destination Table: SmpAbsenceOverseas, count=" & strCnt & "<br>" 
			.Send 
		End With 
	Set cdoMessage = Nothing 
end if

If err.number <> 0 Then
	Set cdoMessage = CreateObject("CDO.Message") 
		With cdoMessage 
			Set .Configuration = cdoConfig 
			.From = "ecp_admin@simplo.com.tw" 
			.To = "cl_chang@simplo.com.tw"
    		.Subject = "SmpCheckAbsenceFormOverseas execute error " &  err.description
			.htmlbody = "SmpCheckAbsenceFormOverseas execute error.  <br>"
			.Send 
		End With 
	Set cdoMessage = Nothing 
End If	


Set sqlcnn = Nothing
objLogFile.Close
Set objFileSystem = nothing
