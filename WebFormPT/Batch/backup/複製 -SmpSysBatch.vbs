Set objFileSystem= CreateObject("Scripting.FileSystemObject")
Set objLogFile = objFileSystem.OpenTextFile("d:\ECP\WebFormPT\Batch\log\SmpSysBatch1.Log", 8, True) '8 is for appending

sch = "http://schemas.microsoft.com/cdo/configuration/" 
Set cdoConfig = CreateObject("CDO.Configuration") 
With cdoConfig.Fields 
     .Item(sch & "sendusing") = 2 ' cdoSendUsingPort 
	 .Item(sch & "smtpserver") = "sysalert.simplo.com.tw"
     .update 
End With 

Set oArgs=WScript.Arguments

'SMP New EF Database
'strsqlCnn = "driver={SQL Server};server=192.168.2.227;uid=sa;pwd=ecp13adm;database=WebFormPT"
strsqlCnn = "driver={SQL Server};server=192.168.2.138;uid=sa;pwd=SMPADMEF2K;database=NaNa"
Set conn = WScript.CreateObject ("ADODB.Connection")
conn.ConnectionTimeout = 60
conn.Open strsqlCnn

objLogFile.Write Now() & " connect db  EF Database "  & vbCrLf

'start �s�W�N�z�H
sql="select count(*) cnt from  NaNa.dbo.Users  where (leaveDate is null or leaveDate ='') and OID not in (select SMVKBAB003 from WebFormPT.dbo.SMVKBAB) "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
'insert �N�z�H���
While not rs.eof
	cnt = rs("cnt")
	rs.MoveNext
Wend

If cnt > 0 Then	
	strInsert = "insert into WebFormPT.dbo.SMVKBAB "
	strInsert = strInsert + " select NEWID(),(select distinct SMVKBAA001 from WebFormPT.dbo.SMVKBAA, WebFormPT.dbo.SMVKBAB where SMVKBAB002=SMVKBAA001 and SMVKBAA002 in('SMP')), OID, '00000000000000000000000Users0001', convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108), '00000000000000000000000Users0001', convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108) "
	'strInsert = strInsert + " select NEWID(),'d6c6862c-4768-4d98-b798-10c69139fd23', OID, '00000000000000000000000Users0001', convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108), '00000000000000000000000Users0001', convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108) "
	strInsert = strInsert + " from NaNa.dbo.Users "
	strInsert = strInsert + " where (leaveDate is null or leaveDate ='')  and OID not in (select SMVKBAB003 from WebFormPT.dbo.SMVKBAB) "
	strInsert = strInsert + "   and id not in ('4251','3761','4559','4710','4028','2865','4885','4356','4879','4297','4604','4747','4940','4445','2943','4456','4921','4200' "
    strInsert = strInsert + "   ,'4471','4664','4268','4246','4440','4866','4954','4350','4725','4209','4763','4578','4151','4585','4712','2712','4507','4355' "
    strInsert = strInsert + "   ,'4948','4344','4632','1442','2492','4870','4974','4966','3477','4901','4822','4066','4307','4971','4740','4668','4510','4397' "
    strInsert = strInsert + "   ,'4345','4255','2834','4378','4889','4743','4336','4920','4886','4409','4820','4221','4849','3167','4850','2862','4223','4443' "
    strInsert = strInsert + "   ,'4970','4943','3178','4479','4639','4172','1652','4469','4905','4393','4375','4229','4851')  "
	'strInsert = strInsert + "   and id in ('2448','2712','4880') "	
	objLogFile.Write Now() & " strInsert SQL : " & strInsert  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strInsert)
End If
'end �s�W�N�z�H

Set rs = nothing

'start   �s�WSPIT001R1_ALL ��T�ݨD�ӽг�
sql = "select count(*) cnt from WebFormPT.dbo.EmployeeInfo "
sql = sql + " where deptId<>'GSA1100' and (empLeaveDate is null) and deptId not like '6%'  and deptId not like '3%' "
sql = sql + "   and empNumber not in ( SELECT SMSAABC003 FROM WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SPIT001R1_ALL')) "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
'insert SPIT001R1_ALL ��T�ݨD�ӽг�
While not rs.eof
	cnt = rs("cnt")
	rs.MoveNext
Wend

If cnt > 0 Then	
	strInsert = " insert into WebFormPT.dbo.SMSAABC "
	strInsert = strInsert + " select NEWID(), (select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SPIT001R1_ALL'), empNumber,'ece611bdd36c1004815bdfcbc6e1a37a',convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108), '00000000000000000000000Users0001', convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108) "
	strInsert = strInsert + " from WebFormPT.dbo.EmployeeInfo where  deptId<>'GSA1100' and (empLeaveDate is null) and deptId not like '6%'  and deptId not like '3%' "
    strInsert = strInsert + " and empNumber not in ( SELECT SMSAABC003 FROM WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SPIT001R1_ALL') ) "
	objLogFile.Write Now() & " strInsert SPIT001R1_ALL SQL : " & strInsert  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strInsert)
End If
'end �s�WSPIT001R1_ALL ��T�ݨD�ӽг�
Set rs = nothing

'--�R��GSA1100 SPIT001R1_ALL ��T�ݨD�ӽг��v�� start
'--WebFormPT.dbo.SMVKBAB 
'sql = "select count(*) cnt FROM WebFormPT.dbo.SMVKBAB where SMVKBAB003 in (SELECT OID FROM NaNa.dbo.Users where leaveDate <>'' or leaveDate is not null) "
sql = "SELECT count(*) cnt FROM WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SPIT001R1_ALL') AND SMSAABC003 IN (SELECT empNumber from WebFormPT.dbo.EmployeeInfo where  deptId='GSA1100' ) "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
While not rs.eof
	cnt = rs("cnt")
	objLogFile.Write Now() & " //Delete SPIT001 WebFormPT.dbo.SMVKBAB Count QTY:" & cnt & vbCrLf
	rs.MoveNext
Wend

If cnt > 0 Then	
	strDel = " delete WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SPIT001R1_ALL') AND SMSAABC003 IN (SELECT empNumber from WebFormPT.dbo.EmployeeInfo where  deptId='GSA1100' )  "
	objLogFile.Write Now() & " Delete WebFormPT.dbo.SMSAABC SQL : " & strDel  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strDel)
End If
'--�R��GSA1100 SPIT001R1_ALL ��T�ݨD�ӽг��v�� End


'start   �s�WSMPALL 
sql = "select count(*) cnt from WebFormPT.dbo.EmployeeInfo "
sql = sql + " where (empLeaveDate is null) and deptId not like '6%' and deptId not like '3%' "
sql = sql + "   and empNumber not in ( SELECT SMSAABC003 FROM WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SMPAll')) "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
'insert �s�WSMPALL
While not rs.eof
	cnt = rs("cnt")
	rs.MoveNext
Wend

If cnt > 0 Then	
	strInsert = " insert into WebFormPT.dbo.SMSAABC "
	strInsert = strInsert + " select NEWID(), (select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SMPAll'), empNumber,'ece611bdd36c1004815bdfcbc6e1a37a',convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108), '00000000000000000000000Users0001', convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108)  "
	strInsert = strInsert + " from WebFormPT.dbo.EmployeeInfo where (empLeaveDate is null or empLeaveDate='') and deptId not like '6%'  and deptId not like '3%' "
    strInsert = strInsert + " and empNumber not in ( SELECT SMSAABC003 FROM WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SMPAll') ) "
	objLogFile.Write Now() & " strInsert SMPAll SQL : " & strInsert  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strInsert)
End If
'end �s�WSMPAll
Set rs = nothing


'start   �s�WSYSTEMDEFAULTGROUP 
sql = "select count(*) cnt from WebFormPT.dbo.EmployeeInfo "
sql = sql + " where (empLeaveDate is null) and deptId not like '6%'  and deptId not like '3%' "
sql = sql + "   and empNumber not in ( SELECT SMSAABC003 FROM WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SYSTEMDEFAULTGROUP')) "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
'insert �s�WSYSTEMDEFAULTGROUPL
While not rs.eof
	cnt = rs("cnt")
	rs.MoveNext
Wend

If cnt > 0 Then	
	strInsert = " insert into WebFormPT.dbo.SMSAABC "
	strInsert = strInsert + " select NEWID(), (select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SYSTEMDEFAULTGROUP'), empNumber,'ece611bdd36c1004815bdfcbc6e1a37a',convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108), '00000000000000000000000Users0001', convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108)  "
	strInsert = strInsert + " from WebFormPT.dbo.EmployeeInfo where (empLeaveDate is null or empLeaveDate='') and deptId not like '6%'  and deptId not like '3%' "
    strInsert = strInsert + " and empNumber not in ( SELECT SMSAABC003 FROM WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SYSTEMDEFAULTGROUP') ) "
	objLogFile.Write Now() & " strInsert SYSTEMDEFAULTGROUP SQL : " & strInsert  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strInsert)
End If
'end �s�WSYSTEMDEFAULTGROUP
Set rs = nothing


'start   �s�WSMPAllTwnStaffGrp �s������v��(���t�~��)
sql = "select count(*) cnt from WebFormPT.dbo.EmployeeInfo "
sql = sql + " where deptId<>'GSA1100' and (empLeaveDate is null) and deptId not like '6%'  and deptId not like '3%' "
sql = sql + "   and empNumber not in ( SELECT SMSAABC003 FROM WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SMPAllTwnStaffGrp')) "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
'insert SMPAllTwnStaffGrp �s������v��(���t�~��)
While not rs.eof
	cnt = rs("cnt")
	rs.MoveNext
Wend

If cnt > 0 Then	
	strInsert = " insert into WebFormPT.dbo.SMSAABC "
	strInsert = strInsert + " select NEWID(), (select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SMPAllTwnStaffGrp'), empNumber,'ece611bdd36c1004815bdfcbc6e1a37a',convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108), '00000000000000000000000Users0001', convert(varchar, getdate(), 111) + ' ' + convert(varchar, getdate(), 108)  "
	strInsert = strInsert + " from WebFormPT.dbo.EmployeeInfo where  deptId<>'GSA1100' and (empLeaveDate is null) and deptId not like '6%'  and deptId not like '3%' "
    strInsert = strInsert + " and empNumber not in ( SELECT SMSAABC003 FROM WebFormPT.dbo.SMSAABC where SMSAABC002=(select SMSAABA001 From WebFormPT.dbo.SMSAABA where SMSAABA002='SMPAllTwnStaffGrp') ) "
	objLogFile.Write Now() & " strInsert SPIT001R1_ALL SQL : " & strInsert  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strInsert)
End If
'end �s�WSMPAllTwnStaffGrp �s������v��(���t�~��)
Set rs = nothing



'start   �s�WNaNa.dbo.Group_User SMPKMAll
sql = "select count(*) cnt FROM  WebFormPT.dbo.EmployeeInfo where empGUID not in (select UserOID FROM NaNa.dbo.Group_User where GroupOID= (select OID from NaNa.dbo.Groups where id='SMPKMALL')  ) and (empLeaveDate is null or empLeaveDate ='') and deptId<>'GSA1100' and deptId not like '3%' and deptId not like '6%' "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
'insert NaNa.dbo.Group_User SMPKMAll
While not rs.eof
	cnt = rs("cnt")
	rs.MoveNext
Wend

If cnt > 0 Then	
	strInsert = " insert into NaNa.dbo.Group_User "
	strInsert = strInsert + " select  (select OID from NaNa.dbo.Groups where id='SMPKMALL'), empGUID FROM  WebFormPT.dbo.EmployeeInfo where empGUID not in (select UserOID FROM NaNa.dbo.Group_User  where GroupOID= (select OID from NaNa.dbo.Groups where id='SMPKMALL') ) and (empLeaveDate is null or empLeaveDate ='') and deptId<>'GSA1100' and deptId not like '3%' and deptId not like '6%' "
	objLogFile.Write Now() & " strInsert  NaNa.dbo.Group_User SQL : " & strInsert  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strInsert)
End If
'end �s�W NaNa.dbo.Group_User SMPKMAll



'--�R����¾�H�� Start
'--WebFormPT.dbo.SMVKBAB 
sql = "select count(*) cnt FROM WebFormPT.dbo.SMVKBAB where SMVKBAB003 in (SELECT OID FROM NaNa.dbo.Users where leaveDate <>'' or leaveDate is not null) "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
While not rs.eof
	cnt = rs("cnt")
	objLogFile.Write Now() & " //Delete WebFormPT.dbo.SMVKBAB Count QTY:" & cnt & vbCrLf
	rs.MoveNext
Wend

If cnt > 0 Then	
	strDel = " delete WebFormPT.dbo.SMVKBAB where SMVKBAB003 in (SELECT OID FROM NaNa.dbo.Users where leaveDate <>'' or leaveDate is not null) "
	objLogFile.Write Now() & " Delete WebFormPT.dbo.SMVKBAB SQL : " & strDel  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strDel)
End If

'--WebFormPT.dbo.SMSAABC 
sql = "select count(*) cnt FROM WebFormPT.dbo.SMSAABC where SMSAABC003 in (SELECT id FROM NaNa.dbo.Users where leaveDate <>'' or leaveDate is not null)  "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
While not rs.eof
	cnt = rs("cnt")
	objLogFile.Write Now() & " //Delete WebFormPT.dbo.SMSAABC Count QTY:" & cnt & vbCrLf
	rs.MoveNext
Wend

If cnt > 0 Then	
	strDel = " delete WebFormPT.dbo.SMSAABC where SMSAABC003 in (SELECT id FROM NaNa.dbo.Users where leaveDate <>'' or leaveDate is not null) "
	objLogFile.Write Now() & " Delete WebFormPT.dbo.SMSAABC SQL : " & strDel  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strDel)
End If

'--NaNa.dbo.Group_User
sql = "select count(*) cnt FROM NaNa.dbo.Group_User where UserOID in (SELECT OID FROM NaNa.dbo.Users where leaveDate <>'' or leaveDate is not null)  "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
While not rs.eof
	cnt = rs("cnt")
	objLogFile.Write Now() & " //Delete NaNa.dbo.Group_User Count QTY:" & cnt & vbCrLf
	rs.MoveNext
Wend

If cnt > 0 Then	
	strDel = " delete NaNa.dbo.Group_User where UserOID in (SELECT OID FROM NaNa.dbo.Users where leaveDate <>'' or leaveDate is not null) "
	objLogFile.Write Now() & " Delete NaNa.dbo.Group_User SQL : " & strDel  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strDel)
End If

'--�R����¾�H�� End



'start Update USERSETTING, RECEIVEMAIL='Y', �]�s�W�H���]�w��, ����쬰N
sql = "select count(*) cnt from  WebFormPT.dbo.USERSETTING where RECEIVEMAIL='N'  "
objLogFile.Write Now() & " // SQL Statement:" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
'insert NaNa.dbo.Group_User SMPKMAll
While not rs.eof
	cnt = rs("cnt")
	rs.MoveNext
Wend

If cnt > 0 Then	
	strUpdate = " update  WebFormPT.dbo.USERSETTING set RECEIVEMAIL='Y' where RECEIVEMAIL='N'   "
	objLogFile.Write Now() & " strUpdate WebFormPT.dbo.USERSETTING  SQL : " & strUpdate  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strUpdate)
End If
'end Update USERSETTING, RECEIVEMAIL='Y', �]�s�W�H���]�w��, ����쬰N



'start Update NaNa.dbo.Users referCalendarOID='6f361ac4d3e9100482ffb551efacc31c' ,  ldapid='engName@LDAPDN', localeString='zh_TW'  --
'sql = "select count(*) cnt from  WebFormPT.dbo.USERSETTING where RECEIVEMAIL='N'  "
sql = "select count(*) cnt From NaNa.dbo.Users where ldapid not like '%@%' and localeString<>'zh_TW' "
objLogFile.Write Now() & " // SQL Statement Update NaNa.dbo.Users :" & sql & vbCrLf
Set rs = conn.Execute(sql)

'On Error Resume Next
'Start Update NaNa.dbo.Users referCalendarOID='6f361ac4d3e9100482ffb551efacc31c' ,  ldapid='engName@LDAPDN', localeString='zh_TW' 
While not rs.eof
	cnt = rs("cnt")
	rs.MoveNext
Wend

If cnt > 0 Then	
	strUpdate = " update NaNa.dbo.Users set referCalendarOID='6f361ac4d3e9100482ffb551efacc31c', localeString='zh_TW', ldapid=substring(mailAddress, 1 ,( charindex('@', mailAddress)-1))+'@LDAPDN' where ldapid not like '%@%' and localeString<>'zh_TW' and mailAddress like '%tw%' "
	objLogFile.Write Now() & " strUpdate WebFormPT.dbo.USERSETTING  SQL : " & strUpdate  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strUpdate)
	
	strUpdate = " update NaNa.dbo.Users set referCalendarOID='6f361ac4d3e9100482ffb551efacc31c', localeString='zh_TW', ldapid=substring(mailAddress, 1 ,( charindex('@', mailAddress)-1))+'@OLDAPDN' where ldapid not like '%@%' and localeString<>'zh_TW' and mailAddress like '%cn%' "
	objLogFile.Write Now() & " strUpdate WebFormPT.dbo.USERSETTING  SQL : " & strUpdate  & vbCrLf & vbCrLf
	Set rs=conn.Execute(strUpdate)
End If
'end Update NaNa.dbo.Users referCalendarOID='6f361ac4d3e9100482ffb551efacc31c' ,  ldapid='engName@LDAPDN', localeString='zh_TW'




Set rs = nothing
Set conn = nothing

objLogFile.Close
Set objFileSystem = nothing



