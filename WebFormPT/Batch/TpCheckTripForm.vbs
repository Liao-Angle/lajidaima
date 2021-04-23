Set objFileSystem= CreateObject("Scripting.FileSystemObject")
Set objLogFile = objFileSystem.OpenTextFile("d:\ECP\WebFormPT\Batch\log\TpCheckTripForm.Log", 8, True) '8 is for appending

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
'objLogFile.Write Now() & " // str_date1: " & str_date & vbCrLf
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

'objLogFile.Write Now() & " // str_lastmonth: " & str_lastmonth & vbCrLf

Set oArgs=WScript.Arguments

'SMP ERP Database
'strsqlCnn = "driver={SQL Server};server=192.168.2.17;AutoTranslate=No;Trusted_Connection=yes;Network=dbnmpntw;"
'strsqlCnn = "driver={SQL Server};server=192.168.2.17;uid=MIS_ERP;pwd=miserp;database=SMP"
strsqlCnn = "driver={SQL Server};server=192.168.2.17;uid=EASYFLOW;pwd=easyflow8;database=TP"
'strsqlCnn = "driver={SQL Server};server=192.168.2.17;uid=EASYFLOW;pwd=easyflow8;database=SMP_TEST"
Set conDSC = WScript.CreateObject ("ADODB.Connection")
conDSC.ConnectionTimeout = 60
conDSC.Open strsqlCnn
'objLogFile.Write Now() & " connect db ERP Database "  & vbCrLf

'SMP New EF Database
strsqlCnn = "driver={SQL Server};server=192.168.2.138;uid=sa;pwd=SMPADMEF2K;database=WebFormPT"
'strsqlCnn = "driver={SQL Server};server=192.168.2.227;uid=sa;pwd=ecp13adm;database=WebFormPT"
Set conEF = WScript.CreateObject ("ADODB.Connection")
conEF.ConnectionTimeout = 60
conEF.Open strsqlCnn

'objLogFile.Write Now() & " connect db  EF Database "  & vbCrLf


'補出差單資料
strsqlecp  = " select  us.id as creator,  replace(CONVERT(char(10), GetDate(),111),'/','') as createdate, us.id as empId, replace(stf.TripDate,'/','') smptripdate, replace(stf.StartTime,':','') as TripSTime, replace(stf.EndTime,':','') as TripETime, replace(stf.TripDate,'/','') as tripdate, '出'+ right(stf.SheetNo, 8) as MC901 "
strsqlecp = strsqlecp + " from SmpTripForm stf, SMWYAAA ya, Users us "
strsqlecp = strsqlecp + " where  stf.GUID = ya.SMWYAAA019 and stf.OriginatorGUID=us.OID  and ya.SMWYAAA020='Y' "
strsqlecp = strsqlecp + " and stf.D_MODIFYTIME >= GetDate()-30 "

objLogFile.Write Now() & " // SQL ECP Statement:" & strsqlecp & vbCrLf
Set sqlEcp = conEF.Execute(strsqlecp)
On Error Resume Next
While not sqlEcp.EOF
	strSql = " select COMPANY,CREATOR,USR_GROUP,CREATE_DATE,FLAG,MC001,MC002,MC003,MC005,MC006,MC900,MC901 from AMSMC where MC001='"&RTrim(sqlEcp("empId"))&"' and  MC002='"&RTrim(sqlEcp("smptripdate"))&"' and ( MC003='"&RTrim(sqlEcp("TripSTime"))&"') "
	'objLogFile.Write Now() & " // SQL  Statement:" & strSql & vbCrLf
	Set efEmprs = conDSC.Execute(strSql)
	If efEmprs.eof Then
		'objLogFile.Write Now() & " // in sql "  & vbCrLf
		strCompany = "SMP"
		strCreator = Trim(sqlEcp("creator"))
		strUsrGroup = "SMP"
		strCreateDate = Trim(sqlEcp("createdate"))
		strFlag = "1"
		strMC001 = Trim(sqlEcp("empId"))
		strMC002 = Trim(sqlEcp("smptripdate"))
		strMC003S = Trim(sqlEcp("TripSTime"))
		strMC003E = Trim(sqlEcp("TripETime"))
		strMC005 = "N"
		strMC006 = Trim(sqlEcp("tripdate"))
		strMC900 = "03"
		strMC901 = Trim(sqlEcp("MC901"))
		
		strInsterDSC = "insert into AMSMC (COMPANY,CREATOR,USR_GROUP,CREATE_DATE,FLAG,MC001,MC002,MC003,MC005,MC006,MC900,MC901) values ("
	    strInsterDSC = strInsterDSC + "'" + strCompany + "'," 
	    strInsterDSC = strInsterDSC + "'" + strCreator + "'," 
	    strInsterDSC = strInsterDSC + "'" + strUsrGroup + "'," 
	    strInsterDSC = strInsterDSC + "'" + strCreateDate + "',"
	    strInsterDSC = strInsterDSC + "'" + strFlag + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC001 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC002 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC003S + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC005 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC006 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC900 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC901 + "')"
		objLogFile.Write Now() & " // SQL Statement:" & strInsterDSC & vbCrLf
		Set rs = conDSC.Execute(strInsterDSC)
		Set rs = nothing
	end if 
	
	strSql = " select COMPANY,CREATOR,USR_GROUP,CREATE_DATE,FLAG,MC001,MC002,MC003,MC005,MC006,MC900,MC901 from AMSMC where MC001='"&RTrim(sqlEcp("empId"))&"' and  MC002='"&RTrim(sqlEcp("smptripdate"))&"' and ( MC003='"&RTrim(sqlEcp("TripETime"))&"') "
	'objLogFile.Write Now() & " // SQL  Statement:" & strSql & vbCrLf
	Set efEmprs = conDSC.Execute(strSql)
	If efEmprs.eof Then	
		strCompany = "SMP"
		strCreator = Trim(sqlEcp("creator"))
		strUsrGroup = "SMP"
		strCreateDate = Trim(sqlEcp("createdate"))
		strFlag = "1"
		strMC001 = Trim(sqlEcp("empId"))
		strMC002 = Trim(sqlEcp("smptripdate"))
		strMC003S = Trim(sqlEcp("TripSTime"))
		strMC003E = Trim(sqlEcp("TripETime"))
		strMC005 = "N"
		strMC006 = Trim(sqlEcp("tripdate"))
		strMC900 = "03"
		strMC901 = Trim(sqlEcp("MC901"))
		
		strInsterDSC = "insert into AMSMC (COMPANY,CREATOR,USR_GROUP,CREATE_DATE,FLAG,MC001,MC002,MC003,MC005,MC006,MC900,MC901) values ("
	    strInsterDSC = strInsterDSC + "'" + strCompany + "'," 
	    strInsterDSC = strInsterDSC + "'" + strCreator + "'," 
	    strInsterDSC = strInsterDSC + "'" + strUsrGroup + "'," 
	    strInsterDSC = strInsterDSC + "'" + strCreateDate + "',"
	    strInsterDSC = strInsterDSC + "'" + strFlag + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC001 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC002 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC003E + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC005 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC006 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC900 + "'," 
	    strInsterDSC = strInsterDSC + "'" + strMC901 + "')"
		
		objLogFile.Write Now() & " // SQL Statement:" & strInsterDSC & vbCrLf
		Set rs = conDSC.Execute(strInsterDSC)
		Set rs = nothing
		Set cdoMessage = CreateObject("CDO.Message")   
		With cdoMessage 
			Set .Configuration = cdoConfig 
				.From = "ecp_admin@simplo.com.tw" 
				'.To = "cl_chang@mail.simplo.com.tw;Eva_Fan@mail.simplo.com.tw;Dustin_Cheng@mail.simplo.com.tw"
				.To = "Eva_Fan@simplo.com.tw"
				.Subject ="Insert DSC AMSMC ->  工號: " +strCreator+ " , 出差日期: "+strMC002 + " ,出差時間: " + strMC003S + " ~ " + strMC003E
				.htmlbody = "Insert DSC AMSMC  -> 工號-姓名: " +strCreator+"-"+  + " <br>出差日期: "+strMC002 + " <br>出差時間: " + strMC003S + " ~ " + strMC003E + " <br>寫入出差單備註: " + strMC901 
				'.Send 
		End With 
		Set cdoMessage = Nothing
		
		
		efEmprs.movenext
	End If

	sqlEcp.movenext
Wend



'檢查國外出差單否否有寫入鼎新備註TF010  20140924
strSqlDsc = " SELECT TF001, TF002, TF003 FROM PALTF WHERE TF905='SPAD004' AND (TF010='' OR TF010 IS NULL) and TF004='LI04' AND TF011 <>'V' "
objLogFile.Write Now() & " // SQL ECP Statement:" & strSqlDsc & vbCrLf
Set dscRs = conDSC.Execute(strSqlDsc)
'On Error Resume Next
If not dscRs.eof Then
  While not dscRs.eof
	strEmpNumber = RTrim(dscRs("TF001"))	
	strTripDate = RTrim(dscRs("TF002"))
	strSerial = RTrim(dscRs("TF003"))	
	'objLogFile.Write Now() & " // DSC strEmpNumber:" & strEmpNumber & " - strTripDate :"& strTripDate & " - strSerial : "& strSerial & vbCrLf	
	
	if (strEmpNumber<>"" and strTripDate<>"") then 	
		strSql = "select SheetNo+' - '+Subject as dscTF010  from SmpForeignTrvl sf, SMWYAAA ya "
		strSql = strSql + " where sf.OriginatorGUID=(select OID from Users where id='" + strEmpNumber + "')  "
		strSql = strSql + " and ('" + strTripDate + "' between replace(StartTrvlDate,'/','') and replace(EndTrvlDate,'/','') ) and sf.GUID = ya.SMWYAAA019  "
		
		Set ecpRs = conEF.Execute(strSql)
		If not ecpRs.eof Then
			strDscTF010 = Trim(ecpRs("dscTF010"))
			'objLogFile.Write Now() & " // strDscTF010:" & strDscTF010 & vbCrLf	
		end if 
		Set ecpRs = nothing
	end if 
	
	strUdSql = " UPDATE PALTF set TF010='"+strDscTF010+"' where TF001='" + strEmpNumber + "' AND TF002='" + strTripDate + "' AND (TF010='' OR TF010 IS NULL) AND TF905='SPAD004' AND TF004='LI04' AND TF011 <>'V' "
	Set rs = conDSC.Execute(strUdSql)
	Set rs = nothing
	objLogFile.Write Now() & " // 寫入假單備註 -> 工號:" & strEmpNumber+ " , 日期: " & strTripDate + " 備註內容:" & strDscTF010 & vbCrLf	
    'Set cdoMessage = CreateObject("CDO.Message")   
	'With cdoMessage 
	'	Set .Configuration = cdoConfig 
	'		.From = "ecp_admin@simplo.com.tw" 
	'		.To = "Eva_Fan@simplo.com.tw"		
	'		.Subject ="Update 寫入假單備註 ->  工號: " +strEmpNumber+ " , 日期: " + strTripDate 
	'		.htmlbody = "Update 寫入假單備註 -> 工號: " +strEmpNumber+ " , 日期: " + strTripDate + " <br>備註內容:" + strDscTF010
	'		.Send 
	'End With 
	'Set cdoMessage = Nothing

	dscRs.movenext
  Wend
End If 
Set dscRs = nothing

'檢查國外出差單否否有寫入鼎新備註TF010  end



'檢查國外出差異動單否否有寫入鼎新備註TF010  20141224
strSqlDsc = " SELECT TF001, TF002, TF003 FROM PALTF WHERE TF905='SPAD005' AND (TF010='' OR TF010 IS NULL) and TF004='LI04' AND TF011 <>'V' "
objLogFile.Write Now() & " // SQL ECP Statement:" & strSqlDsc & vbCrLf
Set dscRs = conDSC.Execute(strSqlDsc)
'On Error Resume Next
If not dscRs.eof Then
  While not dscRs.eof
	strEmpNumber = RTrim(dscRs("TF001"))	
	strTripDate = RTrim(dscRs("TF002"))
	strSerial = RTrim(dscRs("TF003"))	
	'objLogFile.Write Now() & " // DSC strEmpNumber:" & strEmpNumber & " - strTripDate :"& strTripDate & " - strSerial : "& strSerial & vbCrLf	
	
	if (strEmpNumber<>"" and strTripDate<>"") then 	
		strSql = "select SheetNo+' - '+Subject as dscTF010  from SmpForeignTrvlChg sf, SMWYAAA ya "
		strSql = strSql + " where sf.OriginatorGUID=(select OID from Users where id='" + strEmpNumber + "')  "
		strSql = strSql + " and ('" + strTripDate + "' between replace(ChgStartTrvlDate,'/','') and replace(ChgEndTrvlDate,'/','') ) and sf.GUID = ya.SMWYAAA019  "
		
		Set ecpRs = conEF.Execute(strSql)
		If not ecpRs.eof Then
			strDscTF010 = Trim(ecpRs("dscTF010"))
			'objLogFile.Write Now() & " // strDscTF010:" & strDscTF010 & vbCrLf	
		end if 
		Set ecpRs = nothing
	end if 
	
	strUdSql = " UPDATE PALTF set TF010='"+strDscTF010+"' where TF001='" + strEmpNumber + "' AND TF002='" + strTripDate + "' AND (TF010='' OR TF010 IS NULL) AND TF905='SPAD005' AND TF004='LI04' AND TF011 <>'V' "
	Set rs = conDSC.Execute(strUdSql)
	Set rs = nothing
	objLogFile.Write Now() & " // 寫入假單備註 -> 工號:" & strEmpNumber+ " , 日期: " & strTripDate + " 備註內容:" & strDscTF010 & vbCrLf	
    'Set cdoMessage = CreateObject("CDO.Message")   
	'With cdoMessage 
	'	Set .Configuration = cdoConfig 
	'		.From = "ecp_admin@simplo.com.tw" 
	'		.To = "Eva_Fan@simplo.com.tw"		
	'		.Subject ="Update 寫入假單備註 ->  工號: " +strEmpNumber+ " , 日期: " + strTripDate 
	'		.htmlbody = "Update 寫入假單備註 -> 工號: " +strEmpNumber+ " , 日期: " + strTripDate + " <br>備註內容:" + strDscTF010
	'		.Send 
	'End With 
	'Set cdoMessage = Nothing

	dscRs.movenext
  Wend
End If 
Set dscRs = nothing

'檢查國外出差單否否有寫入鼎新備註TF010  end


objLogFile.Write Now() & " End........ " &  vbCrLf

Set conDSC = nothing
Set conEF = nothing

'objLogFile.Write vbCrLf & vbCrLf
objLogFile.Close     
Set objFileSystem = nothing
