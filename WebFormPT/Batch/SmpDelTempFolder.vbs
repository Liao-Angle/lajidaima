
Set args = WScript.Arguments
sitePath = "c:\ECPSite\WebFormPT\web\"
If args.Length > 0 Then
	sitePath = args.Item(0)
End If

Set fso = CreateObject("Scripting.FileSystemObject")
logFolder = sitePath & "LogFolder\SmpDelTempFolder.log"
tempFolder = sitePath & "tempFolder\"

Set logFile = fso.OpenTextFile(logFolder, 8, True) '8 is for appending

sch = "http://schemas.microsoft.com/cdo/configuration/" 
Set cdoConfig = CreateObject("CDO.Configuration") 
With cdoConfig.Fields 
     .Item(sch & "sendusing") = 2 ' cdoSendUsingPort 
	 .Item(sch & "smtpserver") = "sysalert.simplo.com.tw"
     .update 
End With 

'dateNow = cstr(DatePart("yyyy",now))+"/"+cstr(DatePart("m",now))+"/"+cstr(DatePart("d",now))+" "+cstr(DatePart("h",now))+":"+cstr(DatePart("n",now))+":"+cstr(DatePart("s",now))

'SMP New EF Database
'strsqlCnn = "driver={SQL Server};server=192.168.2.227;uid=sa;pwd=ecp13adm;database=WebFormPT"
strsqlCnn = "driver={SQL Server};server=192.168.2.138;uid=sa;pwd=SMPADMEF2K;database=WebFormPT"
Set conn = WScript.CreateObject ("ADODB.Connection")
conn.ConnectionTimeout = 60
conn.Open strsqlCnn
 
If sitePath = "" Then      
	Wscript.Echo "No Folder parameter was passed"      
	Wscript.Quit
Else
	Set folder = fso.GetFolder(tempFolder)
	Set files = folder.Files    
	For each folderIdx In files    
		fileName = folderIdx.Name
		guid = Mid(fileName, 1, InStrRev(fileName, ".")-1)
		sql = "select count('x') cnt from FILEITEM a WHERE FILEPATH ='" & fileName & "' AND LEVEL1='' AND LEVEL2=''"
		logFile.Write Now() & " // SQL Statement: " & sql & vbCrLf
		Set rs = conn.Execute(sql)
		While not rs.eof
			cnt = rs("cnt")
			rs.MoveNext
		Wend
		If cnt = 0 Then
			'§R°£ÀÉ®×
			logFile.Write Now() & " // delete fileName: " & fileName & vbCrLf
			folderIdx.Delete
		End If
	Next
End If

Set conn = nothing
logFile.Write vbCrLf & vbCrLf
logFile.Close     
Set fso = nothing

