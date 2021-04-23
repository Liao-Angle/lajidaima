Set objFileSystem= CreateObject("Scripting.FileSystemObject")
Set objLogFile = objFileSystem.OpenTextFile("d:\ECP\WebFormPT\Batch\log\SmpForeignTrvlNonPay.Log", 8, True) '8 is for appending

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

strSQL = " select CompanyCode, SMWYAAA012+'-'+SMWYAAA013 UserInfo, SMWYAAA016+'-'+SMWYAAA015 as DeptInfo "
strSql = strSql & " ,SheetNo, Subject , StartTrvlDate, EndTrvlDate "
strSql = strSql & "  , site = CASE WHEN SiteUs = 'Y' THEN '����-' + SiteUsDesc WHEN SiteJp = 'Y' "
strSql = strSql & "                            THEN '�饻-' + SiteJpDesc WHEN SiteKr = 'Y' THEN '����-' + SiteKrDesc  WHEN "
strSql = strSql & "                            SiteSub = 'Y' THEN '�l���q-' + SiteSubDesc WHEN SiteOther = 'Y' THEN '��L-' + "
strSql = strSql & "                            SiteOtherDesc ELSE ' ' END "
strSql = strSql & "  , PrePay = CASE WHEN PrePayTwd = 'Y' THEN '�x��-' + PrePayTwdAmt WHEN PrePayCny = 'Y' "
strSql = strSql & "                            THEN '�H����-' + PrePayCnyAmt WHEN PrePayUsd = 'Y' THEN '����-' + PrePayUsdAmt  WHEN "
strSql = strSql & "                            PrePayOther = 'Y' THEN  PrePayOtherAmt  ELSE ' ' END "
strSql = strSql & " , TrvlDesc, (select mailAddress from Users where OID=sf.OriginatorGUID) TrvlEmp , ya.SMWYAAA004, ya.SMWYAAA005, ya.SMWYAAA002 "
strSql = strSql & " from SmpForeignTrvl sf, SMWYAAA ya "
strSql = strSql & " where sf.GUID=ya.SMWYAAA019 and ya.SMWYAAA020='Y' "
strSql = strSql & "   and sf.GUID not in (select OriForeignForm from SmpForeignTrvlChg sfc,  SMWYAAA yac where sfc.GUID=yac.SMWYAAA019 and yac.SMWYAAA020 in ('Y','I')  ) "
strSql = strSql & "   and sf.GUID not in (select OriForeignForm from SmpForeignTrvlBilling sfb,  SMWYAAA yab where sfb.GUID=yab.SMWYAAA019 and yab.SMWYAAA020  in ('Y','I')  )  "
'strSql = strSql & "   and sf.OriginatorGUID in (select OID from Users where leaveDate ='' or leaveDate is null) "
strSql = strSql & "   and DATEDIFF(day,convert(datetime, sf.EndTrvlDate, 111),getdate() ) > 30 " ' --�W�L30�ѥ��д�
strSql = strSql & "   and DATEDIFF(year,convert(datetime, EndTrvlDate, 111),getdate() ) =0  " '       -- �u��ܷ�~�׸��
strSql = strSql & "   and sf.SheetNo not in ('SPAD00400000490','SPAD00400000322') and sf.FeeCharge IN ('0','4')  " '       -- �ư������� , �u��ܶO�έt�ᬰ�s���Τ���
strSql = strSql & " union all "
strSql = strSql & " select  CompanyCode, SMWYAAA012+'-'+SMWYAAA013 as UserInfo, SMWYAAA016+'-'+SMWYAAA015 as DeptInfo "
strSql = strSql & " , SheetNo, Subject , ChgStartTrvlDate as StartTrvlDate, ChgEndTrvlDate as EndTrvlDate "
strSql = strSql & "  , site = CASE WHEN SiteUs = 'Y' THEN '����-' + SiteUsDesc WHEN SiteJp = 'Y' "
strSql = strSql & "                            THEN '�饻-' + SiteJpDesc WHEN SiteKr = 'Y' THEN '����-' + SiteKrDesc  WHEN "
strSql = strSql & "                            SiteSub = 'Y' THEN '�l���q-' + SiteSubDesc WHEN SiteOther = 'Y' THEN '��L-' + "
strSql = strSql & "                            SiteOtherDesc ELSE ' ' END "
strSql = strSql & "  , PrePay = CASE WHEN ChgPrePayTwd = 'Y' THEN '�x��-' + ChgPrePayTwdAmt WHEN ChgPrePayCny = 'Y' "
strSql = strSql & "                            THEN '�H����-' + ChgPrePayCnyAmt WHEN ChgPrePayUsd = 'Y' THEN '����-' + ChgPrePayUsdAmt  WHEN "
strSql = strSql & "                            ChgPrePayOther = 'Y' THEN  ChgPrePayOtherAmt  ELSE ' ' END "
strSql = strSql & " , TrvlDesc+' - ' +ChgTrvlDesc TrvlDesc , (select mailAddress from Users where OID=sf.OriginatorGUID) TrvlEmp , ya.SMWYAAA004, ya.SMWYAAA005, ya.SMWYAAA002 "
strSql = strSql & " from SmpForeignTrvlChg sf, SMWYAAA ya "
strSql = strSql & " where sf.GUID=ya.SMWYAAA019 and ya.SMWYAAA020='Y'  "
strSql = strSql & "   and ChangeType='0' and sf.FeeCharge IN ('0','4') " ' --����ܤ������, �����X�t����� , �u��ܶO�έt�ᬰ�s���Τ��� 
strSql = strSql & "   and sf.GUID not in (select OriForeignForm from SmpForeignTrvlChg sfc,  SMWYAAA yac where sfc.GUID=yac.SMWYAAA019 and yac.SMWYAAA020 in ('Y','I')  ) "
strSql = strSql & "   and sf.GUID not in (select OriForeignForm from SmpForeignTrvlBilling sfb,  SMWYAAA yab where sfb.GUID=yab.SMWYAAA019 and yab.SMWYAAA020  in ('Y','I')  )  "
'strSql = strSql & "   and sf.OriginatorGUID in (select OID from Users where leaveDate ='' or leaveDate is null) "
strSql = strSql & "   and sf.SheetNo not in ('SPAD00500000205','SPAD00500000132') "
strSql = strSql & "   and DATEDIFF(day,convert(datetime, sf.ChgEndTrvlDate, 111),getdate() ) > 30 "'  --�W�L30�ѥ��д�
strSql = strSql & "   and DATEDIFF(year,convert(datetime, EndTrvlDate, 111),getdate() ) =0 "     '   -- �u��ܷ�~�׸��

'strSql = strSql & " order by  ya.SMWYAAA008 asc "
objLogFile.Write Now() & " SQL Statement:" & strSql & vbCrLf

Set sqlEC = sqlcnn.Execute(strSql)
str_today = cstr(DatePart("yyyy",now))+"/"+cstr(DatePart("m",now))+"/"+cstr(DatePart("d",now)) 

If not sqlEC.eof Then
	While not sqlEC.eof
		'intCnt = intCnt + 1
		strText = "<font lang=""EN-US"" style=""font-size: 9pt; font-family: Tahoma"">[��~�X�t�O���W�L30�Ѥ����дڸ��]"
		'strText = strText & "<font lang=""EN-US"" style=""font-size: 9pt; font-family: Tahoma"">�Y�w�C�L�e�ܸ��ƪ��ǽЩ��������"
		strText = strText & "<table border=""0"" cellspacing=""1"" cellpadding=""0"" bgcolor=""#C0C0C0"">"
		strText = strText & "<tr style=""font-size: 9pt; font-family: Tahoma""><td>���q�O</td><td>��O</td><td>�渹</td><td>�X�t�H��</td><td>�X�t��_~�W</td><td>�X�t�a�I</td><td>�w��ӽ�</td><td>�D��</td></tr>"		
		strText = strText & "<tr style=""font-size: 9pt; font-family: Tahoma""><td bgcolor=""#FFFFFF"" width=30>" & sqlEC("CompanyCode") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=150>" & sqlEC("SMWYAAA004") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=80><a href=http://"& strAPServerIP &"/ECP?runMethod=showReadOnlyForm&processSerialNumber="& sqlEC("SMWYAAA005") &"> " & sqlEC("SMWYAAA002") & "</a></td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=80>" & sqlEC("UserInfo") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=100>" & sqlEC("StartTrvlDate") &" ~ "& sqlEC("EndTrvlDate") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=90>" & sqlEC("site") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"" width=90>" & sqlEC("PrePay") & "</td>"
		strText = strText & "<td bgcolor=""#FFFFFF"">" & sqlEC("Subject") & "</td></tr>"
		strText = strText & "</table>"
		
		mailUser = sqlEC("TrvlEmp")		
		prePay = sqlEC("PrePay")
				
		Set cdoMessage = CreateObject("CDO.Message") 
		With cdoMessage
			Set .Configuration = cdoConfig 
			.From = "ecp_admin@simplo.com.tw" 
			.To = mailUser
			.Cc = "Yodo_Yang@simplo.com.tw"
			.Bcc = "eva_fan@simplo.com.tw"			
			.Subject = "[New EasyFlow�q��] ��~�X�t�O���W�L30�Ѥ����дڸ��"
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
