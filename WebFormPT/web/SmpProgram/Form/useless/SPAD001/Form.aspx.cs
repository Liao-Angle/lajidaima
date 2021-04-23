using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;

public partial class SmpProgram_System_Form_SPAD001_Form : SmpAdFormPage
{
	//public com.dsc.kernal.webservice.WSDLClient WC = null;
	//public com.dsc.kernal.factory.AbstractEngine sp7engine = null;
	
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初使化
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPAD001"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPAD001.SmpAbsenceFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";
    }

    /// <summary>
    /// 初使表單資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        //MessageBox(approveErpFormTest());
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo"); //填表人的一些基本資訊
        string userId = (string)Session["UserID"];
        bool isAddNew = base.isNew();
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
		
		//20180111 HRM Test
		try
		{
			//取得系統參數
			String result = "";
			WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
			string strhrmWs = sp.getParam("SmpHrmWebService"); //系統參數
			writeLog("strhrmWs : " + strhrmWs);

			com.dsc.kernal.webservice.WSDLClient hrmWs = new com.dsc.kernal.webservice.WSDLClient(strhrmWs);
            hrmWs.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
            hrmWs.build(true);	
			
			//init ECP Web Service 
            WebServerProject.SysParam param = new WebServerProject.SysParam(engine);
            string ws = param.getParam("SmpHrmWebService");  //Get System Configuration                       
            com.dsc.kernal.webservice.WSDLClient service = new com.dsc.kernal.webservice.WSDLClient(ws);
            service.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
            service.build(true);
			
			 //Prepare XML
            //string xml = @"<Request><Access><Authentication password="""" user=""gp""/></Access><RequestContent><ServiceType>Dcms.HR.Services.IAttendanceParameterService,Dcms.HR.Business.AttendanceParameter</ServiceType><Method>GetEmpOTHours</Method><Parameters><Parameter type=""System.String"">B7A52998-ED73-4187-BA0B-2587632D13B3</Parameter><Parameter type=""System.DateTime"">2018/01/12</Parameter><Parameter type=""System.Boolean"">True</Parameter><Parameter type=""System.Boolean"">False</Parameter><Parameter type=""System.Boolean"">False</Parameter><Parameter type=""System.String""/></Parameters></RequestContent></Request>";
			
			string xml = GetEmpInfoXML("4020");
                        
            //Call HR WebService
            result = Convert.ToString(service.callSync("IntegrationByXmlWithLanguageEx", xml,"0"));
            writeLog("XML result : " + result);  
			
			writeLog("ws : " + ws);			

		}
		catch(Exception ex)
		{
			writeLog("Exception : " + ex);
		}

        string[,] ids = null;
        //AbstractEngine engineErp = null;
        string sql = null;
        DataSet ds = null;
        int count = 0;
        try
        {
            //公司別
            ids = new string[,]{
                {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "smp", "新普科技")},
				{"TE",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "smp", "嘉普科技")},
                {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "tp", "中普科技")}
            };
            CompanyCode.setListItem(ids);
            string orgId = "SMP";
            if (!string.IsNullOrEmpty(objects.getData("CompanyCode")))
            {
                orgId = objects.getData("CompanyCode");
            }
            else
            {
                sql = "select orgId from EmployeeInfo where empNumber='" + userId + "'";
                string value = (string)engine.executeScalar(sql);

                if (value != null)
                {
                    orgId = value;
                }
            }

            CompanyCode.ValueText = orgId;
            CompanyCode.ReadOnly = true;

            //班別 (來自人事)
            string year = DateTimeUtility.getSystemTime2(null).Substring(0, 4);
            //sql = "select p.MK001, p.MK002 from CMSMI c, PALMK p where c.MI002='" + year + "' and c.COMPANY='" + orgId + "' and c.MI003 = p.MK001 and c.COMPANY = p.COMPANY";
			sql = "select p.MK001, p.MK002 from CMSMI c, PALMK p where c.MI002='2016' and c.COMPANY='" + orgId + "' and c.MI003 = p.MK001 and c.COMPANY = p.COMPANY";
            ds = engine.getDataSet(sql, "TEMP");
            count = ds.Tables[0].Rows.Count;
            ids = new string[1 + count, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";

            for (int i = 0; i < count; i++)
            {
                string classCodeId = ds.Tables[0].Rows[i][0].ToString();
                string classCodeName = ds.Tables[0].Rows[i][1].ToString();
                ids[1 + i, 0] = classCodeId;
                ids[1 + i, 1] = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", classCodeId.ToLower(), classCodeName);
            }
            ClassCode.setListItem(ids);
            ClassCode.ReadOnly = true;

            //請假人員部門
            OrganizationUnitGUID.clientEngineType = engineType;
            OrganizationUnitGUID.connectDBString = connectString;
            if (OrganizationUnitGUID.ValueText.Equals(""))
            {
                OrganizationUnitGUID.ValueText = si.fillerOrgID;
                OrganizationUnitGUID.doValidate();
            }
            OrganizationUnitGUID.ReadOnly = true;

            //請假人員
            OriginatorGUID.clientEngineType = engineType;
            OriginatorGUID.connectDBString = connectString;
            if (OriginatorGUID.ValueText.Equals(""))
            {
                OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
                OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
            }

            //代理人
            DeputyGUID.clientEngineType = engineType;
            DeputyGUID.connectDBString = connectString;
            if (DeputyGUID.ValueText.Equals(""))
            {
                string[] substituteValues = base.getSubstituteUserInfo(engine, OriginatorGUID.GuidValueText);
                if (substituteValues[0] != "")
                {
                    DeputyGUID.GuidValueText = substituteValues[0];
                    DeputyGUID.doGUIDValidate();
                }
            }
            //流程類別
            ids = new string[,]{
                {"",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "ids0", "")},
                {"N",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "ids1", "標準")},
                {"Y",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "ids2", "自定")}
            };
            IsCustomFlow.setListItem(ids);
            IsCustomFlow.ReadOnly = true;

            if (IsCustomFlow.ValueText.Equals(""))
            {
                IsCustomFlow.ValueText = getIsCustomFlow(engine);
            }

            //假別 (來自人事)
            sql = "select MC001, MC002 from PALMC_V where COMPANY='" + orgId + "' order by MC001";
            ds = engine.getDataSet(sql, "TEMP");
            count = ds.Tables[0].Rows.Count;
            ids = new string[1+count, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            for (int i = 0; i <count  ; i++)
            {
                string categoryId = ds.Tables[0].Rows[i][0].ToString();
                string categoryName = ds.Tables[0].Rows[i][1].ToString();
                ids[1+i, 0] = categoryId;
                ids[1+i, 1] = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", categoryId.ToLower(), categoryName);
            }
            
            CategoryCode.setListItem(ids);
            //假別, 預設為特休假
            if (CategoryCode.ValueText.Equals(""))
            {
                CategoryCode.ValueText = "LD01";
            }

            //請假含假日
            ids = new string[,]{
                {"N",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "ids3", "否")},
                {"Y",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "ids4", "是")}
            };
            IsIncludeHoliday.setListItem(ids);
            //請假含假日, 預設為N
            if (IsIncludeHoliday.ValueText.Equals(""))
            {
                IsIncludeHoliday.ValueText = "N";
            }

            //查詢特休明細按鈕
            //ButtonSearch.Display = false;

            //審核人一
            Checkby1GUID.clientEngineType = engineType;
            Checkby1GUID.connectDBString = connectString;

            //審核人二
            Checkby2GUID.clientEngineType = engineType;
            Checkby2GUID.connectDBString = connectString;

            //設定請假人員預設資料
            //setOriginatorDefaultValue();

            if (!OriginatorGUID.ValueText.Equals(userId))
            {
                ButtonSearch.Enabled = false;
            }

            //時數
            //Hours.ValueText = getHours();
            if (Hours.ValueText.Equals(""))
            {
                //Hours.ValueText = "8";
                Hours.ValueText = getHours();
            }
            Hours.ReadOnly = true;

            //隱藏欄位
            IsIncludeDateEve.Display = false;
            TempSerialNo.Display = false;
            OriginatorTitle.Display = false;

            if (isAddNew)
            {
                OriginatorGUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
                Checkby1GUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
                Checkby2GUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
            }
            //改變工具列順序
            base.initUI(engine, objects);
            if (isNew() == true)
            {
                witeLogSMP(Convert.ToString(Session["UserID"]), OriginatorGUID.ValueText, OriginatorGUID.ReadOnlyValueText);
            }
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {

        }
    }

    private void witeLogSMP(string UserID, string fillerID, string fillerName)
    {
        try
        {
            writeLog(new Exception(ProcessPageID + " " + DateTime.Now.ToString("yyyy/mm/dd HH:mm:ss.ff") + " Session[\"UserID\"]=" + UserID + " fillerID=" + fillerID + " fillerName=" + fillerName), false);
        }
        catch { }
    }

    /// <summary>
    /// 顯示表單資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        System.IO.StreamWriter sw = null;
				
        try
        {
            //表單欄位
            //主旨
            Subject.ValueText = objects.getData("Subject");
            //顯示單號
            base.showData(engine, objects);
            //公司別
            CompanyCode.ValueText = objects.getData("CompanyCode");
            //請假人員部門
            OrganizationUnitGUID.GuidValueText = objects.getData("OrganizationUnitGUID");
            OrganizationUnitGUID.doGUIDValidate();
            //請假人員
            OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
            OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
            //流程類別
            IsCustomFlow.ValueText = objects.getData("IsCustomFlow");
            //代理人員
            DeputyGUID.GuidValueText = objects.getData("DeputyGUID");
            DeputyGUID.doGUIDValidate();
            //班別
            ClassCode.ValueText = objects.getData("ClassCode");
            //假別
            CategoryCode.ValueText = objects.getData("CategoryCode");
            //請假含假日
            IsIncludeHoliday.ValueText = objects.getData("IsIncludeHoliday");
            //起始日期
            StartDate.ValueText = objects.getData("StartDate");
            //截止日期
            EndDate.ValueText = objects.getData("EndDate");
            //起始時間
            StartTime.ValueText = objects.getData("StartTime");
            //截止時間
            EndTime.ValueText = objects.getData("EndTime");
            //時數
            Hours.ValueText = objects.getData("Hours");
            showHourDesc(Hours.ValueText);
            //說明
            Description.ValueText = objects.getData("Description");
            //審核人一
            Checkby1GUID.GuidValueText = objects.getData("Checkby1GUID");
            Checkby1GUID.doGUIDValidate();
            //審核人二
            Checkby2GUID.GuidValueText = objects.getData("Checkby2GUID");
            Checkby2GUID.doGUIDValidate();
            //回傳序號
            TempSerialNo.ValueText = objects.getData("TempSerialNo");

            //ButtonSearch.Display = true;

            string actName = Convert.ToString(getSession("ACTName"));
            if (actName == "" || actName.Equals("填表人"))
            {
                if (CategoryCode.ValueText.Equals("LF01") || CategoryCode.ValueText.Equals("LF02") 
                    || CategoryCode.ValueText.Equals("LI03"))
                {
                    IsIncludeHoliday.ValueText = "Y";
                    IsIncludeHoliday.ReadOnly = true;
                }
            }
            else
            {
                //表單發起後不允許修改
                Subject.ReadOnly = true;
                CategoryCode.ReadOnly = true;
                IsIncludeHoliday.ReadOnly = true;
                CompanyCode.ReadOnly = true;
                OriginatorGUID.ReadOnly = true;
                DeputyGUID.ReadOnly = true;
                StartDate.ReadOnly = true;
                StartTime.ReadOnly = true;
                EndDate.ReadOnly = true;
                EndTime.ReadOnly = true;
                Checkby1GUID.ReadOnly = true;
                Checkby2GUID.ReadOnly = true;
                ButtonSearch.Enabled = true;
                OriginatorTitle.ReadOnly = true;
            }

            //唯讀欄位
            Hours.ReadOnly = true;
            IsCustomFlow.ReadOnly = true;
            ClassCode.ReadOnly = true;
			
			string strLoginID = (string)Session["UserID"];
			//if (strLoginID.Equals("3992") || strLoginID.Equals("3787"))
			//{
				ButtonSearch.Enabled = false;
			//}

            if (actName.Equals("直屬主管"))
            {
                AddSignButton.Display = true;
            }
            else if (actName.Equals("SPAD001_HRADM") || actName.Equals("差勤負責人"))
            {
                Hours.ReadOnly = false;
            }

            string userId = (string)Session["UserID"];
            if (userId.Equals("1356"))
            {
                OriginatorTitle.Display = true;
				OriginatorTitle.ValueText = objects.getData("OriginatorTitle");
                string deptId = OrganizationUnitGUID.ValueText;
                if (deptId.Equals("GSA1100") || deptId.Equals("GZA1100"))
                {
                    string chinaTitle = base.getDispatchedTitle(CompanyCode.ValueText, OriginatorGUID.ValueText);
                    if (!string.IsNullOrEmpty(chinaTitle))
                    {
                        OriginatorTitle.ValueText = chinaTitle;
                    }
                }
            }
            else
            {
                LblOriginatorTitle.Text = "";
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }

    /// <summary>
    /// 儲存表單資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            bool isAddNew = base.isNew(); //base 父類別
            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
                objects.setData("CompanyCode", CompanyCode.ValueText);
                objects.setData("OrganizationUnitGUID", OrganizationUnitGUID.GuidValueText);
                objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
                objects.setData("DeputyGUID", DeputyGUID.GuidValueText);
                objects.setData("IsCustomFlow", IsCustomFlow.ValueText);
                objects.setData("ClassCode", ClassCode.ValueText);
                objects.setData("IsIncludeHoliday", IsIncludeHoliday.ValueText);
                objects.setData("IsIncludeDateEve", IsIncludeDateEve.ValueText);
                objects.setData("StartDate", StartDate.ValueText);
                objects.setData("StartTime", StartTime.ValueText);
                objects.setData("EndDate", EndDate.ValueText);
                objects.setData("EndTime", EndTime.ValueText);
                //objects.setData("Hours", Hours.ValueText);
                //回傳序號
                objects.setData("TempSerialNo", TempSerialNo.ValueText);
                objects.setData("Checkby1GUID", Checkby1GUID.GuidValueText);
                objects.setData("Checkby2GUID", Checkby2GUID.GuidValueText);
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //for MCloud
                string[] values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
                OriginatorTitle.ValueText = values[4];
                objects.setData("CompanyCodeValue", CompanyCode.ReadOnlyText);
                //objects.setData("OriginatorUserName", OriginatorGUID.ReadOnlyValueText);
                //objects.setData("OriginatorDeptName", OrganizationUnitGUID.ReadOnlyValueText);
                objects.setData("OriginatorTitle", OriginatorTitle.ValueText);
                //objects.setData("DeputyUserName", DeputyGUID.ReadOnlyValueText);
                objects.setData("ClassCodeValue", ClassCode.ReadOnlyText);
                objects.setData("CategoryCodeValue", CategoryCode.ReadOnlyText);
                objects.setData("HoursDesc", LblHoursDesc.Text);
                //objects.setData("Checkby1UserName", Checkby1GUID.ReadOnlyValueText);
                //objects.setData("Checkby2UserName", Checkby2GUID.ReadOnlyValueText);

                base.saveData(engine, objects);
            }
            objects.setData("Hours", Hours.ValueText);
            objects.setData("CategoryCode", CategoryCode.ValueText);
            objects.setData("Description", Description.ValueText);
            objects.setData("TempSerialNo", TempSerialNo.ValueText);

            //beforeSetFlow
            setSession("IsSetFlow", "Y");

            //beforeSign 加簽
            string actName = Convert.ToString(getSession("ACTName").ToString());
            if (actName.Equals("SPAD001_HRADM") || actName.Equals("差勤負責人"))
            {
                setSession("IsAddSign", "AFTER");
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {

        }
    }

    /// <summary>
    /// 檢查送單資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));

        if (actName.Equals(""))
        {
            //審核人一二不可為申請人
            if (OriginatorGUID.ValueText.Equals(Checkby1GUID.ValueText)
                || OriginatorGUID.ValueText.Equals(Checkby2GUID.ValueText))
            {
                strErrMsg += "審核人不可為申請人!\n";
            }

            //審核人不可請假人的直屬主管
            string originatorGUID = OriginatorGUID.GuidValueText;
            string[] values = base.getUserManagerInfo(engine, originatorGUID);
            string managerGUID = values[0];
            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];
            if (managerId.Equals(Checkby1GUID.ValueText) || managerId.Equals(Checkby2GUID.ValueText))
            {
                strErrMsg += "審核人不需填寫直屬主管.系統會自動判斷!\n";
            }

            //代理人員不可設為請假人員
            if (OriginatorGUID.ValueText.Equals(DeputyGUID.ValueText))
            {
                strErrMsg += "代理人員不可設為請假人員!\n";
            }

            decimal hours = 0;
            bool isDecimal = false;
            //請假時數不可為零
            if (!Hours.ValueText.Equals(""))
            {
                isDecimal = decimal.TryParse(Hours.ValueText, out hours);
                if (!isDecimal)
                {
                    strErrMsg += Hours.ValueText + "\n";
                }
                //hours = Convert.ToDecimal(Hours.ValueText);
            }
            if (hours <= 0)
            {
                strErrMsg += "請假時數必需大於零!\n";
            }

            //截止請假日期不可小於起始請假日期
            string strStartDateTime = StartDate.ValueText + " " + StartTime.ValueText + ":00";
            string strEndDateTime = EndDate.ValueText + " " + EndTime.ValueText + ":00";
            DateTime startDateTime = Convert.ToDateTime(strStartDateTime);
            DateTime endDateTime = Convert.ToDateTime(strEndDateTime);
            if (startDateTime.CompareTo(endDateTime) > 0)
            {
                strErrMsg += "截止請假日期不可小於起始請假日期!\n";
            }

            //檢查請假時數必須為整點
            if (!Hours.ValueText.Equals(""))
            {
                if (isDecimal)
                {
                    if (Convert.ToDecimal(Hours.ValueText) % 1 != 0)
                    {
                        strErrMsg += "請假時數必須以小時為單位!\n";
                    }
                }
            }

            //請假日期跨天, 請假時間必須為整點
            if (IsIncludeDateEve.ValueText.Equals("Y"))
            {
                if (!StartTime.ValueText.EndsWith("00") || EndTime.ValueText.Equals("00"))
                {
                    //strErrMsg += "請假日期跨天, 請假時間必須為整點!\n";
                }
            }

            //R1300, R1700, 除PALEF12CHK群組人員(1907, 3969, 4410, 4321)外必需選擇審核人一
            string[] userDeptInfo = base.getDeptInfo(engine, originatorGUID);
            if (userDeptInfo[0].Equals("R1300") || userDeptInfo[0].Equals("R1700"))
            {
                if (Checkby1GUID.ValueText.Equals("") && Checkby2GUID.ValueText.Equals(""))
                {
                    //strErrMsg += "AME/ME 需選擇審核人!\n";
                }
            }

            //請假時間必須為整點 00/30
            if (!StartTime.ValueText.EndsWith("00") && !StartTime.ValueText.EndsWith("30"))
            {
                strErrMsg += "請假起始時間必須為整點!/n";
            }
            if (!EndTime.ValueText.EndsWith("00") && !EndTime.ValueText.EndsWith("30"))
            {
                strErrMsg += "請假截止時間必須為整點!/n";
            }

            //請假日期為假日時, 顯示警示訊息
            if (getIsIncludeHoliday().Equals("Y") && IsIncludeHoliday.ValueText.Equals("N"))
            {
                MessageBox("【請假日期起~迄含休假日】，請確認請假日期無誤!");
            }

            //請假日期時間有重覆!
            string strTemp = checkDataTime();
            if (!strTemp.Equals(""))
            {
                strErrMsg += strTemp;
            }

            //檢查是否特休超出可休天數
            if (CategoryCode.ValueText.Equals("LD01"))
            {
                decimal erpDays = getERPDays();
                if (hours > erpDays)
                {
                    strErrMsg += "本次特休請假時數(" + hours + "), 已超出特休可休時數(" + erpDays + ")!\n";
                }
            }

            //檢查起始日期時間、截止日期時間是否合理

            //設定主旨
            if (!Subject.ValueText.StartsWith("請假日期"))
            {
                values = base.getUserInfo(engine, originatorGUID);
                string subject = "請假日期：" + StartDate.ValueText + " 申請人員：" + OriginatorGUID.ValueText + " " + values[1];
                Subject.ValueText = subject + Subject.ValueText;
            }

            //Event: 請假起始/截止日期時間，重新計算請假時數
            //Event: 請假是否含假日，重新計算請假時數

            if (!strErrMsg.Equals(""))
            {
                pushErrorMessage(strErrMsg);
                result = false;
            }
        }

        return result;
    }

    /// <summary>
    /// 初使送單資訊
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人(登入者)
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"]; //表單關系人
        //si.ownerID = OriginatorGUID.ValueText;
        si.ownerName = (string)Session["UserName"];
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        //depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
        //si.ownerOrgID = depData[0];
        //si.ownerOrgName = depData[1];
        //si.submitOrgID = depData[0];
        return si;
    }

    /// <summary>
    /// 取得送單資訊
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = OriginatorGUID.ValueText;
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
        //si.ownerOrgID = depData[0];
        //si.ownerOrgName = depData[1];
        //si.submitOrgID = depData[0];
        depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");
        return si;
    }

    /// <summary>
    /// 取得單號編碼定義
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID); //自動編號設定作業
        return hs;
    }

    /// <summary>
    /// 設定流程變數
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        string[] values = null;
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        //填表人
        string creatorId = si.fillerID;
        
        //請假人員
        string originatorGUID = currentObject.getData("OriginatorGUID");
        values = base.getUserInfo(engine, originatorGUID);
        string originatorId = values[0];
        values = base.getUserInfo(engine, currentObject.getData("DeputyGUID"));
        string deputyId = values[0];
        string notifierId = deputyId + ";"; //通知人員
        string deptAssistantId = ""; //部門助理
        string[] deptInfo = base.getDeptInfo(engine, currentObject.getData("OriginatorGUID"));
        if (!deptInfo[0].Equals(""))
        {
            string[] userFunc = getUserRoles(engine, "部門收發", deptInfo[0]);
            deptAssistantId = userFunc[2];
        }

        //填表人不等於請假人員則通知
        //if (!creatorId.Equals(originatorId))
        //{
        //    notifierId += originatorId + (";");
        //}
        //通知部門助理
        if (!deptAssistantId.Equals(""))
        {
            notifierId += deptAssistantId + (";");
        }
       
        //審核人一
        values = base.getUserInfo(engine, currentObject.getData("Checkby1GUID"));
        string checkby1Id = values[0];
        
        //審核人二
        values = base.getUserInfo(engine, currentObject.getData("Checkby2GUID"));
        string checkby2Id = values[0];

        //主管
        values = base.getUserManagerInfo(engine, originatorGUID);
        string managerGUID = values[0];
        values = base.getUserInfo(engine, managerGUID);
        string managerId = values[0];

        //請假時數
        string hours = currentObject.getData("Hours");

        //人事承辦人員
        //string[][] hrUsers = base.getGroupdUser(engine, "SMP-HRADM");
        //string hrUndertaker = "";
        //for (int i = 0; i < hrUsers.Length; i++)
        //{
        //    hrUndertaker += hrUsers[i][0] + ";";
        //}

        //string[][] backTwUsers = base.getGroupdUser(engine, "SPAD001-BACKTW");
        //string backTwNotifier = "";
        //for (int i = 0; i < backTwUsers.Length; i++)
        //{
        //    backTwNotifier += backTwUsers[i][0] + ";";
        //}

        string decisionCode = "";
        values = base.getUserInfo(engine, originatorGUID);
        if (!values[3].Equals(""))
        {
            int titleId = Convert.ToInt32(values[3]);
            int hour = Convert.ToInt32(hours);
            if (titleId <= 25)
            {
                decisionCode = "CHAIRMAN";
            }
            else if (titleId <= 28)
            {
                if (hour >= 8)
                {
                    decisionCode = "CHAIRMAN";
                }
            }
            else if (titleId <= 31)
            {
                if (hour >= 8)
                {
                    decisionCode = "CHAIRMAN";
                }
            }
        }

        //是否返台/自定流程
        string isCustomerFlow = currentObject.getData("IsCustomFlow");
        if (isCustomerFlow.Equals("Y"))
        {
            decisionCode = "BACK";
        }

        //是否資深副總簽核
        string isNeedAVP = getIsNeedAVP(engine, originatorGUID);
        
        xml += "<PALEF12>";
        xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
        xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
        xml += "<notifier DataType=\"java.lang.String\">" + notifierId + "</notifier>";
        xml += "<verify1 DataType=\"java.lang.String\">" + checkby1Id + "</verify1>";
        xml += "<verify2 DataType=\"java.lang.String\">" + checkby2Id + "</verify2>";
        xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
        xml += "<hours DataType=\"java.lang.Integer\">" + hours + "</hours>";
        xml += "<hrUndertaker DataType=\"java.lang.String\">SMP-HRADM</hrUndertaker>";
        xml += "<isBackTW DataType=\"java.lang.String\">" + isCustomerFlow + "</isBackTW>";
        xml += "<backTwNotifier DataType=\"java.lang.String\">SPAD001-BACKTW</backTwNotifier>";
        xml += "<isNeedAVP DataType=\"java.lang.String\">" + isNeedAVP + "</isNeedAVP>";
        xml += "<decisionCode DataType=\"java.lang.String\">" + decisionCode + "</decisionCode>";
        xml += "</PALEF12>";

        //表單代號
        param["PALEF12"] = xml;

        return "PALEF12";
    }

    /// <summary>
    /// 流程設定前
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="setFlowXml"></param>
    /// <returns></returns>
    protected override string beforeSetFlow(AbstractEngine engine, string setFlowXml)
    {
        return setFlowXml;
    }

    /// <summary>
    /// 表單送出後
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        try
        {
            string strErrMsg = "";
            //新增WFERP請假單
            object[] aryTemp = callDataCenter("6");
            if (Convert.ToString(aryTemp[0]).Equals("Y"))
            {
                string tempSerialNo = Convert.ToString(aryTemp[2]);
                currentObject.setData("TempSerialNo", tempSerialNo);
                TempSerialNo.ValueText = tempSerialNo;
                engine.updateData(currentObject);
                //寫字串到ERP備註欄位
                updateErpForm(currentObject);
            }
            else
            {
                strErrMsg += "送簽後續處理作業發生錯誤, 錯誤原因: " + Convert.ToString(aryTemp[1]);
            }

            //寫字串到ERP備註欄位
            //aryTemp = callDataCenter("7");
            //if (aryTemp != null)
            //{
            //    if (aryTemp.Length > 0)
            //    {
            //        if (!Convert.ToString(aryTemp[0]).Equals("Y"))
            //        {
            //            strErrMsg += "送簽後續處理作業發生錯誤, 錯誤原因: " + Convert.ToString(aryTemp[1]);
            //        }
            //    }
            //    else
            //    {
            //        strErrMsg += "送簽後續處理作業發生錯誤, 錯誤原因: 無法取得傳回值!";
            //    }
            //}
            //else
            //{
            //    strErrMsg += "送簽後續處理作業發生錯誤, 錯誤原因: 傳回值為空!";
            //}

            if (!strErrMsg.Equals(""))
            {
                pushErrorMessage(strErrMsg);
                //throw new Exception(strErrMsg);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        base.afterSend(engine, currentObject);
    }

    /// <summary>
    /// 簽核前
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        string xml = "";
        string actName = Convert.ToString(getSession("ACTName").ToString());
        string hradm = "SPAD001_HRADM";
        bool isAddSignActivity = false;

        if (actName.Equals(hradm))
        {
            isAddSignActivity = true;
        }

        ArrayList newPerformDetailArray = (ArrayList)getSession("PerformDetailArray");
        for (int i = 0; i < newPerformDetailArray.Count; i++)
        {
            PerformDetail pd = (PerformDetail)newPerformDetailArray[i];
            string activityName = fixNbspS(Page.Server.HtmlEncode(pd.activityName));
            if (activityName.Equals(hradm))
            {
                isAddSignActivity = false;
                break; //加簽過不再加簽
            }
        }

        if (isAddSignActivity)
        {
            string isCustomerFlow = IsCustomFlow.ValueText;
            //自定簽核
            if (isCustomerFlow.Equals("Y"))
            {
                string originatorGUID = OriginatorGUID.GuidValueText;
                string sql = "select StateValueGUID, StateValueId, StateValueName,SignType,StateType,StateNo from SmpUserFlow uf, SmpUserFlowDetail ufd where uf.UserGUID = '" + originatorGUID + "' and uf.Active='Y' and uf.GUID = ufd.UserFlowGUID order by StateNo";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                int rows = ds.Tables[0].Rows.Count;

                string hrDept = "PI01";
                AbstractEngine engineErp = getErpEngine(CompanyCode.ValueText);
                sql = "SELECT MA006 FROM CMSSMA where MA001='" + OriginatorGUID.ValueText + "'";
                hrDept = (string)engineErp.executeScalar(sql);
                //組xml
                xml += "<list>";

                //台幹自定流程需在SMP HR簽核後即先通知, STCS訂票人員
                if (hrDept.Equals("PI04")) //一般間接/長派新世電子(常熟)有限公司
                {
                    sql = "select OID from Groups where id='SPAD001-STCS-TICKET'";
                    string oid = (string)engine.executeScalar(sql);
                    if (oid != null)
                    {
                        xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                        xml += "        <performers>";
                        xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                        xml += "                <OID>" + oid + "</OID>";
                        xml += "                <participantType><value>GROUP</value></participantType>";
                        xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                        xml += "        </performers>";
                        xml += "        <multiUserMode><value>FOR_EACH</value></multiUserMode>";
                        xml += "        <name>通知訂票人員</name>";
                        xml += "        <performType><value>NOTICE</value></performType>";
                        xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                    }
                }

                //自定流程簽核人員
                for (int i = 0; i < rows; i++)
                {
                    string oid = ds.Tables[0].Rows[i][0].ToString();
                    string participantType = "";
                    string performType = "NORMAL";
                    string stateValueName = ds.Tables[0].Rows[i][2].ToString();
                    string signType = ds.Tables[0].Rows[i][3].ToString();
                    string stateType = ds.Tables[0].Rows[i][4].ToString();
                    if (signType.Equals("1"))
                    {
                        participantType = "HUMAN";
                    }
                    else if (signType.Equals("21"))
                    {
                        participantType = "GROUP";
                    }

                    if (stateType.Equals("2")) //簽核
                    {
                        performType = "NORMAL";
                        stateValueName = "加簽[" + stateValueName + "]";
                    }
                    else if (stateType.Equals("4")) //通知
                    {
                        performType = "NOTICE";
                        stateValueName = "通知[" + stateValueName + "]";
                    }

                    xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                    xml += "        <performers>";
                    xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    xml += "                <OID>" + oid + "</OID>";
                    xml += "                <participantType><value>" + participantType + "</value></participantType>";
                    xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    xml += "        </performers>";
                    xml += "        <multiUserMode><value>FIREST_GET_FIRST_WIN</value></multiUserMode>";
                    xml += "        <name>" + stateValueName + "</name>";
                    xml += "        <performType><value>" + performType + "</value></performType>";
                    xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                }

                //自定流程, 非返台假LI03, 時數 >= 24 則加簽董事長 1356
                string hours = Hours.ValueText;
                string category = CategoryCode.ValueText;
                if (!category.Equals("LI03") && Convert.ToInt16(hours) >= 24)
                {
                    string[] groupInfo = base.getGroupGUID(engine, "SMP-CHAIRMAN");
                    string chairmanGroupOID = groupInfo[0];

                    xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                    xml += "        <performers>";
                    xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    xml += "                <OID>" + chairmanGroupOID + "</OID>";
                    xml += "                <participantType><value>GROUP</value></participantType>";
                    xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    xml += "        </performers>";
                    xml += "        <multiUserMode><value>FIREST_GET_FIRST_WIN</value></multiUserMode>";
                    xml += "        <name>加簽董事長</name>";
                    xml += "        <performType><value>NORMAL</value></performType>";
                    xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                }
                
                xml += "</list>";
            }
            //addCustomActivity(engine, Convert.ToString(getSession("WorkItemOID")), true, xml, "", "");
            return xml;
        }
        else if (actName.Equals("差勤負責人"))
        {
            //確認ERP單據
            string result = approveErpForm();
            if (!result.Equals(""))
            {
                throw new Exception(result);
            }
        }

        return addSignXml;
    }

    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        //string actName = Convert.ToString(getSession("ACTName").ToString());
        //if (actName.Equals("差勤負責人"))
        //{
        //    //確認ERP單據
        //    string approveResult = approveErpForm();
        //    if (!approveResult.Equals(""))
        //    {
        //        throw new Exception(approveResult);
        //    }
        //}
        
        base.afterSign(engine, currentObject, result);
    }

    /// <summary>
    /// 重辦程序
    /// </summary>
    protected override void rejectProcedure()
    {
        //退回填表人終止流程
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
        if (backActID.ToUpper().Equals("CREATOR"))
        {
            try
            {
                //widhDrawErpForm();
                //base.rejectProcedure();
                base.terminateThisProcess();
            }
            catch (Exception e)
            {
                base.writeLog(e);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        if (result.Equals("Y"))
        {

        }
        else
        {
            widhDrawErpForm(engine, currentObject);
        }
        base.afterApprove(engine, currentObject, result);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
    }

    /// <summary>
    /// 假別變更
    /// </summary>
    /// <param name="value"></param>
    protected void CategoryCode_SelectChanged(string value)
    {
        //提示
        string strMessage = "";
        string category = CategoryCode.ValueText;
        IsIncludeHoliday.ReadOnly = false;

        //LA03家庭照顧假
        if (category.Equals("LA03"))
        {
            strMessage += "請檢附家庭成員與本假別相關事由之醫療證明或其他證明文件!\n";
        }
        //LB01病假:
        else if (category.Equals("LB01"))
        {
            strMessage += "1日(含)內請檢附當日醫療費用收據!\n";
            strMessage += "1日以上請檢附診斷證明書，並註明宜休養天數!\n";
        }
        //LE01喪假:
        else if (category.Equals("LE01"))
        {
            strMessage += "1.請檢附訃文或死亡證明書!\n";
            strMessage += "2.須於6個月內請完!\n";
        }
        //LF01產假(一):
        else if (category.Equals("LF01"))
        {
            strMessage += "1.到職滿6個月以上分娩，給假56天(全薪)!\n";
            strMessage += "2.請假期間包含例假日及國定假日!\n";
            strMessage += "3.請檢附出生證明書!\n";
            //產假，請假是否含假日預設為是
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        //LF02產假(二):
        else if (category.Equals("LF02"))
        {
            strMessage += "1.到職未滿6個月分娩，給假56天(半薪)!\n";
            strMessage += "2.請假期間包含例假日及國定假日!\n";
            strMessage += "3.請檢附出生證明書!\n";
            //產假，請假是否含假日預設為是
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        //LF03陪產假:
        else if (category.Equals("LF03"))
        {
            strMessage += "請檢附出生證明書!\n";
        }
        //LF04流產假(三):
        else if (category.Equals("LF04"))
        {
            strMessage += "1.妊娠3個月以上流產，且到職滿6個月，給假28天(全薪)!\n";
            strMessage += "2.請假期間包含例假日及國定假日!\n";
            strMessage += "3.請檢附診斷證明書!\n";
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        //LF05產假(四):
        else if (category.Equals("LF05"))
        {
            strMessage += "1.妊娠3個月以上流產，到職未滿6個月，給假28天(半薪)!\n";
            strMessage += "2.請假期間包含例假日及國定假日!\n";
            strMessage += "3.請檢附診斷證明書!\n";
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        //LF06流產假(五):
        else if (category.Equals("LF06"))
        {
            strMessage += "1.妊娠2個月以上未滿3個月流產，且到職滿6個月，給假7天(全薪)!\n";
            strMessage += "2.請假期間包含例假日及國定假日!\n";
            strMessage += "3.請檢附診斷證明書!\n";
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        //LF07流產假(六):
        else if (category.Equals("LF07"))
        {
            strMessage += "1.妊娠2個月以上未滿3個月流產，到職未滿6個月，給假7天(半薪)!\n";
            strMessage += "2.請假期間包含例假日及國定假日!\n";
            strMessage += "3.請檢附診斷證明書!\n";
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        //LF07流產假(七):
        else if (category.Equals("LF08"))
        {
            strMessage += "1.妊娠未滿2個月流產，且到職滿6個月，給假5天(全薪)!\n";
            strMessage += "2.請假期間包含例假日及國定假日!\n";
            strMessage += "3.請檢附診斷證明書!\n";
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        //LF07流產假(八):
        else if (category.Equals("LF09"))
        {
            strMessage += "1.妊娠未滿2個月流產，到職未滿6個月，給假5天(半薪)!\n";
            strMessage += "2.請假期間包含例假日及國定假日!\n";
            strMessage += "3.請檢附診斷證明書!\n";
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        //LF10產檢假:
        else if (category.Equals("LF10"))
        {
            strMessage += "請檢附產前檢查紀錄表影本及就醫收據!\n";
        }
        //LG01婚假:
        else if (category.Equals("LG01"))
        {
            strMessage += "1.請檢附結婚登記證明文件!\n";
            strMessage += "2.請假起始日不得小於結婚登記日!\n";
            strMessage += "3.須於結婚登記完成日起6個月內請完!\n";
        }
        //LG03原住民慶典假:
        else if (category.Equals("LG03"))
        {
            strMessage += "請檢附戶口名簿或身分證!\n";
        }
        //LH01公傷假, LJ01無薪假
        else if (category.Equals("LH01") || category.Equals("LJ01"))
        {
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        else if (category.Equals("LI01"))
        {
            //strMessage += "1.說明中請詳述出差之地點及原因!\n";
            //strMessage += "2.若為參加外訓,請檢附外訓申請單及上課資料!\n";
            //strMessage += "出差必須填寫國內出差單，而非請假單!\n";
            string deptId = OrganizationUnitGUID.ValueText;
            if (!deptId.ToUpper().Equals("A1310") && !deptId.ToUpper().Equals("GSA1100") && !deptId.ToUpper().Equals("GZA1100"))
            {
                strMessage += "非海外支援部出差必須填寫國內出差單，而非請假單!";
                CategoryCode.ValueText = "";
            }
        }
        //LI03返台假:
        else if (category.Equals("LI03"))
        {
            strMessage += "返台假僅提供長派大陸地區人員申請返台休假使用,非長派人員不得申請!\n";
            //返台假，請假是否含假日預設為是
            IsIncludeHoliday.ValueText = "Y";
            IsIncludeHoliday.ReadOnly = true;
        }
        else
        {
            IsIncludeHoliday.ValueText = "N";
            //IsIncludeHoliday.ReadOnly = false;
        }

        //重算時數
        string hours = getHours();
        Hours.ValueText = Convert.ToString(hours);

        if (!strMessage.Equals(""))
        {
            MessageBox(strMessage);
        }
    }

    /// <summary>
    /// 班別變更重算時數
    /// </summary>
    /// <param name="value"></param>
    protected void ClassCode_SelectChanged(string value)
    {
        //重算時數
        string hours = getHours();
        Hours.ValueText = Convert.ToString(hours);
    }
    
    /// <summary>
    /// 是否含假日變更重算時數
    /// </summary>
    /// <param name="value"></param>
    protected void IsIncludeHoliday_SelectChanged(string value)
    {
        //重算時數
        string hours = getHours();
        Hours.ValueText = Convert.ToString(hours);
    }

    /// <summary>
    /// 請假日期變更重算時數
    /// </summary>
    /// <param name="values"></param>
    protected void StartDate_DateTimeClick(string values)
    {
        //重新確認請假日期是否跨天
        IsIncludeDateEve.ValueText = getIsIncludeDateEve();

        //重算時數
        string hours = getHours();
        Hours.ValueText = Convert.ToString(hours);

        //重新取得班別
        object[] aryTemp = callDataCenter("9");
        setSession(base.PageUniqueID, "UserWorkInfo", aryTemp);
        //給班別預設值
        ClassCode.ValueText = aryTemp[0] + "";
    }

    /// <summary>
    /// 截止日期變更重算時數
    /// </summary>
    /// <param name="values"></param>
    protected void EndDate_DateTimeClick(string values)
    {
        //重新確認請假日期是否跨天
        IsIncludeDateEve.ValueText = getIsIncludeDateEve();
        
        //重算時數
        string hours = getHours();
        Hours.ValueText = Convert.ToString(hours);
    }

    /// <summary>
    /// 請假時間變更重算時數
    /// </summary>
    /// <param name="values"></param>
    protected void StartTime_DateTimeClick(string values)
    {
        //請假時間 00~29, 則顯示00; 30~59, 則顯示30
        string strStartDateTime = StartDate.ValueText + " " + StartTime.ValueText + ":00";
        DateTime startDateTime = Convert.ToDateTime(strStartDateTime);
        if (startDateTime.Minute < 30)
        {
            StartTime.ValueText = startDateTime.Hour + ":00";
        }
        else
        {
            StartTime.ValueText = startDateTime.Hour + ":30";
        }

        //重算時數
        string hours = getHours();
        Hours.ValueText = Convert.ToString(hours);
    }

    /// <summary>
    /// 截止時間變更重算時數
    /// </summary>
    /// <param name="values"></param>
    protected void EndTime_DateTimeClick(string values)
    {
        //請假時間 00~29, 則顯示00; 30~59, 則顯示30
        string strEndDateTime = EndDate.ValueText + " " + EndTime.ValueText + ":00";
        DateTime endDateTime = Convert.ToDateTime(strEndDateTime);
        if (endDateTime.Minute < 30)
        {
            EndTime.ValueText = endDateTime.Hour + ":00";
        }
        else
        {
            EndTime.ValueText = endDateTime.Hour + ":30";
        }

        //重算時數
        string hours = getHours();
        Hours.ValueText = Convert.ToString(hours);
    }

    /// <summary>
    /// 重新選擇代理人員
    /// </summary>
    /// <param name="values"></param>
    protected void DeputyGUID_SingleOpenWindowButtonClick(string[,] values)
    {

    }

    /// <summary>
    /// 重新選擇請假人員
    /// </summary>
    /// <param name="values"></param>
    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        
        //設定請假人員預設資料
        setOriginatorDefaultValue();

        //更新部門
        string[] deptInfoValues = base.getDeptInfo(engine, OriginatorGUID.GuidValueText);
        if (!deptInfoValues[0].Equals(""))
        {
            OrganizationUnitGUID.ValueText = deptInfoValues[0];
            OrganizationUnitGUID.doValidate();
        }

        //更新流程類別
        IsCustomFlow.ValueText = getIsCustomFlow(engine);

        //更新代理人
        string[] substituteValues = base.getSubstituteUserInfo(engine, OriginatorGUID.GuidValueText);
        if (substituteValues[0] != "")
        {
            DeputyGUID.GuidValueText = substituteValues[0];
            DeputyGUID.ValueText = substituteValues[1];
            DeputyGUID.ReadOnlyValueText = substituteValues[2];
        }
        else
        {
            DeputyGUID.GuidValueText = "";
            DeputyGUID.ValueText = "";
            DeputyGUID.ReadOnlyValueText = "";
        }

        //重算時數
        string hours = getHours();
        Hours.ValueText = Convert.ToString(hours);

        if (OriginatorGUID.ValueText.Equals((string)Session["UserID"]))
        {
            ButtonSearch.Enabled = true;
        }
        else
        {
            ButtonSearch.Enabled = false;
        }
    }

    protected void OriginatorGUID_BeforeClickButton()
    {
        
    }

    /// <summary>
    /// 請假明細查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSearch_BeforeClicks(object sender, EventArgs e)
    {
        Session["SPAD001_OriginatorId"] = OriginatorGUID.ValueText;
        Session["SPAD001_CompanyCode"] = CompanyCode.ValueText;
        Session["SPAD001_OriginatorGUID"] = OriginatorGUID.GuidValueText;
        string url = "LeaveSummary.aspx";
        bool openWindow = !ButtonSearch.ReadOnly;
        //if (OriginatorGUID.ValueText.Equals((string)Session["UserID"]))
        //{
        //    openWindow = true;
        //}

        if (openWindow)
        {
            base.showOpenWindow(url, "查詢特休明細", "", "500", "", "", "", "1", "1", "", "", "", "", "800", "", true);
        }
    }

    /// <summary>
    /// 設定請假人員班別、請假日期時間、截止日期時間、是否跨天
    /// </summary>
    private void setOriginatorDefaultValue()
    {
        string actName = Convert.ToString(getSession("ACTName"));
        if (actName.Equals(""))
        {
            AbstractEngine engine = null;
            try
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                
                //公司別
                string sql = "select orgId from EmployeeInfo where empNumber='" + OriginatorGUID.ValueText + "'";
                string value = (string)engine.executeScalar(sql);

                if (value != null)
                {
                    CompanyCode.ValueText = value;
                }

                //流程類別
                IsCustomFlow.ValueText = getIsCustomFlow(engine);

                //取回員工基本班別,與上下班/休息時間等 (作業類別9) 
                object[] aryTemp = callDataCenter("9");
                setSession(base.PageUniqueID, "UserWorkInfo", aryTemp);
                //給班別預設值
                ClassCode.ValueText = aryTemp[0] + "";

                //起始時間
                string workingTime = aryTemp[2].ToString();
                workingTime = workingTime.Substring(0, 2) + ":" + workingTime.Substring(2, 2);
                StartTime.DateTimeMode = 4;
                StartTime.AllowEmpty = false;
                StartTime.ValueText = workingTime;

                //截止時間
                workingTime = aryTemp[3].ToString();
                workingTime = workingTime.Substring(0, 2) + ":" + workingTime.Substring(2, 2);
                EndTime.DateTimeMode = 4;
                EndTime.AllowEmpty = false;
                EndTime.ValueText = workingTime;

                //是否跨天
                IsIncludeDateEve.ValueText = getIsIncludeDateEve();
                //MessageBox(IsIncludeDateEve.ValueText);

                //取得是否跨天
                string today = DateTimeUtility.convertDateTimeToString(DateTime.Now).Substring(0, 10);
                string tomorrow = DateTimeUtility.convertDateTimeToString(DateTime.Now.AddDays(1)).Substring(0, 10);
                if (IsIncludeDateEve.ValueText.Equals("Y"))
                {
                    //起始日期
                    StartDate.DateTimeMode = 0;
                    StartDate.AllowEmpty = false;
                    //StartDate.ValueText = today;

                    //截止日期
                    EndDate.DateTimeMode = 0;
                    EndDate.AllowEmpty = false;
                    //EndDate.ValueText = tomorrow;
                }
                else
                {
                    //起始日期
                    StartDate.DateTimeMode = 0;
                    StartDate.AllowEmpty = false;
                    //StartDate.ValueText = tomorrow;

                    //截止日期
                    EndDate.DateTimeMode = 0;
                    EndDate.AllowEmpty = false;
                    //EndDate.ValueText = tomorrow;
                }
            }
            catch (Exception e)
            {
                base.writeLog(e);
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }
        }
    }

    /// <summary>
    /// 是否為自定流程
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    private string getIsCustomFlow(AbstractEngine engine)
    {
        string isCustomFlow = "N";
        string originatorGUID = OriginatorGUID.GuidValueText;
        string sql = "select StateValueId,SignType from SmpUserFlow uf, SmpUserFlowDetail ufd where uf.UserGUID = '" + originatorGUID + "' and uf.Active='Y' and uf.GUID = ufd.UserFlowGUID ";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        if (rows > 0)
        {
            isCustomFlow = "Y";
        }
        return isCustomFlow;
    }

    /// <summary>
    /// 是否需要簽到執行副總
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    private string getIsNeedAVP(AbstractEngine engine, string userGUID)
    {
        string isNeedAVP = "N";
        string[] depts = base.getDeptInfo(engine, userGUID);
        string[] values = base.getUserInfo(engine, userGUID);
        
        if (!values[3].Equals(""))
        {
            int titleId = Convert.ToInt32(values[3]);
            //RD部門副理以上需簽核至副總
            if (depts[0].StartsWith("NSR1") || depts[0].StartsWith("RD1"))
            {
                if (titleId <= 33)
                {
                    isNeedAVP = "Y";
                }
            }
            else if (depts[0].StartsWith("NSS11"))
            {
                if (titleId <= 41)
                {
                    isNeedAVP = "Y";
                }
            }
        }
        
        return isNeedAVP;
    }

    /// <summary>
    /// 取得請假時數
    /// </summary>
    /// <returns></returns>
    private string getHours()
    {
        string hours = Hours.ValueText;
        if (!StartDate.ValueText.Equals("") && !EndDate.ValueText.Equals("")
            && !StartTime.ValueText.Equals("") && !EndTime.ValueText.Equals(""))
        {
            string actName = Convert.ToString(getSession("ACTName"));
            if (actName.Equals(""))
            {
                //取得請假時數
                object[] aryTemp = callDataCenter("5");
                hours = Convert.ToString(aryTemp[1]);
                int i = 0;
                bool isNumber = int.TryParse(hours, out i);
                if (isNumber)
                {
                    //MessageBox(hours);
                    if (hours.StartsWith("0"))
                    {
                        string endWithHrCode = "LE01,LF01,LF02,LG02,LH01,LI01,LI02,LI03,LJ01,LJ02";
                        string categoryCode = CategoryCode.ValueText;
                        if (endWithHrCode.IndexOf(categoryCode) == -1)
                        {
                            string hour = hours.Substring(0, hours.Length - 2);
                            string minute = hours.Substring(hours.Length - 2);
                            hours = Convert.ToString(Convert.ToInt16(hour) + Convert.ToDecimal(minute) / 60);
                        }
                    } 
                    else if (hours.EndsWith("00") || hours.EndsWith("30"))
                    {
                        string endWithHrCode = "LE01,LF01,LF02,LG02,LH01,LI01,LI02,LJ01,LJ02";
                        string categoryCode = CategoryCode.ValueText;
                        if (endWithHrCode.IndexOf(categoryCode) == -1)
                        {
                            string hour = hours.Substring(0, hours.Length - 2);
                            string minute = hours.Substring(hours.Length - 2);
                            hours = Convert.ToString(Convert.ToInt16(hour) + Convert.ToDecimal(minute) / 60);
                        }
                    }

                    string classCode = ClassCode.ValueText;
                    int startDate = Convert.ToInt32(StartDate.ValueText.Replace("/", ""));
                    int endDate = Convert.ToInt32(EndDate.ValueText.Replace("/", ""));
                    int startTime = Convert.ToInt32(StartTime.ValueText.Replace(":", ""));
                    int endTime = Convert.ToInt32(EndTime.ValueText.Replace(":", ""));


                    if (endDate > startDate && startTime > endTime)
                    {
                        aryTemp = (object[])getSession(base.PageUniqueID, "UserWorkInfo");
                        int startWorkingTime = Convert.ToInt32(aryTemp[2]);
                        int endWorkingTime = Convert.ToInt32(aryTemp[3]);
                        int startRestTime = Convert.ToInt32(aryTemp[5]);
                        int endRestTime = Convert.ToInt32(aryTemp[6]);

                        if (classCode.StartsWith("SD") || classCode.Equals("SM5"))
                        {
                            if (startTime >= endRestTime && endTime <= startRestTime)
                            {
                                hours = Convert.ToString((int)((Convert.ToDouble(hours) / 0.5) * 0.5) - 23);
                            }
                        }
                        else if (classCode.Equals("SM2"))
                        {
                            if (startTime >= endRestTime && endTime <= endRestTime && endTime >= startWorkingTime)
                            {
                                hours = Convert.ToString((int)((Convert.ToDouble(hours) / 0.5) * 0.5) - 23);
                            }
                        }
                        else if (classCode.Equals("SN5"))
                        {

                        }
                    }
                }
            }
            else
            {
                MessageBox(hours);
            }
        }

        showHourDesc(hours);

        return hours;
    }

    /// <summary>
    /// 請假日期是否包含假日
    /// </summary>
    /// <returns></returns>
    private string getIsIncludeHoliday()
    {
        string isInclude = "N";
        AbstractEngine engineErp = null;
        try
        {
            engineErp = getErpEngine(CompanyCode.ValueText);
            string startDate = StartDate.ValueText.Replace("/", "");
            string endDate = EndDate.ValueText.Replace("/", "");
            string classCode = ClassCode.ValueText;
            string sql = "select MP001 from CMSMP as CMSMP WHERE MP003='" + classCode + "' and (MP004 = '" + startDate + "' OR MP004 = '" + endDate + "')";
            DataSet ds = engineErp.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                isInclude = "Y";
            }
            //object[] aryTemp = callDataCenter("4");
            //if (Convert.ToString(aryTemp[2]).Equals("1"))
            //{
            //    isInclude = "Y";
            //}
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (engineErp != null) engineErp.close();
        }

        return isInclude;
    }

    /// <summary>
    /// 上班時間是否跨天
    /// </summary>
    /// <returns></returns>
    private string getIsIncludeDateEve()
    {
        string includeDateEve = "N";
        if (!Convert.ToString(StartDate.ValueText).Equals("") && !Convert.ToString(EndDate.ValueText).Equals(""))
        {
            if (!StartDate.ValueText.Equals(EndDate.ValueText))
            {
                includeDateEve = "Y";
            }
        }
        else
        {
            object[] aryTemp = (object[])getSession(base.PageUniqueID, "UserWorkInfo");
            string startWorkingTime = Convert.ToString(aryTemp[2]);
            string endWorkingTime = Convert.ToString(aryTemp[3]);
            if (string.Compare(startWorkingTime, endWorkingTime) > 0) //上班時間大於下班時間
            {
                includeDateEve = "Y";
            }
        }

        return includeDateEve;
    }

    /// <summary>
    /// 請假日期時間是否有重覆
    /// </summary>
    /// <returns></returns>
    private string checkDataTime()
    {
        string strMessage = "";
        AbstractEngine engineErp = null;
        try
        {
            //檢查請假日期時間是否有重覆
            bool isErr = false;
            engineErp = getErpEngine(CompanyCode.ValueText);
            string originatorId = OriginatorGUID.ValueText;
            string startDate = StartDate.ValueText.Replace("/", "");
            string endDate = EndDate.ValueText.Replace("/", "");
            string startTime = StartTime.ValueText.Replace(":", "");
            string endTime = EndTime.ValueText.Replace(":", "");
            string startDateTime = startDate + startTime;
            string endDateTime = endDate + endTime;
            string sql = "";
            if (StartDate.ValueText.Equals(EndDate.ValueText))
            {
                sql = "select TF001 FROM PALTF WHERE TF001= '" + originatorId + "' AND TF011 IN ('Y','N') AND TF002='" + startDate + "' AND ((TF005 >= '" + startTime + "' AND TF006 <= '" + endTime + "') OR (TF005 < '" + startTime + "' AND TF006 > '" + startTime + "' AND TF006 <= '" + endTime + "') OR (TF005 >= '" + startTime + "' AND TF005 < '" + endTime + "' AND TF006 > '" + endTime + "'))";
                DataSet ds = engineErp.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    isErr = true;
                }
            }
            else
            {
                sql = "select TF001 FROM PALTF WHERE TF001= '" + originatorId + "' AND ((TF002+TF005 >= '" + startDateTime + "' AND TF002+TF006 <= '" + endDateTime + "') OR (TF002+TF005 < '" + startDateTime + "' AND TF002+TF006 > '" + startDateTime + "' AND TF002+TF006 <= '" + endDateTime + "') OR (TF002+TF005 >= '" + startDateTime + "' AND TF002+TF005 < '" + endDateTime + "' AND TF002+TF006 > '" + endDateTime + "')) AND TF011 IN ('Y','N')";
                DataSet ds = engineErp.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    isErr = true;
                }
            }
            if(isErr) 
            {
                strMessage += "請假日期時間有重覆, 請確認日期與時間, 若有問題請聯絡人事部門處理!\n";
            }

            //檢查請假日期時間截止日期時間是否合理
            object[] aryTemp = (object[])getSession(base.PageUniqueID, "UserWorkInfo");
            string startWorkingTime = Convert.ToString(aryTemp[2]);
            string endWorkingTime = Convert.ToString(aryTemp[3]);
            string startRestTime = Convert.ToString(aryTemp[5]);
            string endRestTime = Convert.ToString(aryTemp[6]);
            int intStartTime = Convert.ToInt32(startTime);
            int intEndTime = Convert.ToInt32(endTime);
            int intStartWorkingTime = Convert.ToInt32(startWorkingTime);
            int intEndWorkingTime = Convert.ToInt32(endWorkingTime);
            int intStartRestTime = Convert.ToInt32(startRestTime);
            int intEndRestTime = Convert.ToInt32(endRestTime);

            if (intStartTime == intStartRestTime)
            {
                strMessage += "請假時間不可等於休息時間(起)!\n";
            }

            if (intEndTime == intEndRestTime)
            {
                strMessage += "截止時間不可等於休息時間區(迄)!\n";
            }

            string startWorkingDateTime = startDate + startWorkingTime;
            string endWorkingDateTime = endDate + endWorkingTime;
            long lonStartWorkingDateTime = Convert.ToInt64(startWorkingDateTime);
            long lonEndWorkingDateTime = Convert.ToInt64(endWorkingDateTime);
            long lonStartDateTime = Convert.ToInt64(startDateTime);
            long lonEndDateTime = Convert.ToInt64(endDateTime);

            if (intStartWorkingTime < intEndWorkingTime) //沒有跨天
            {
                //請假時間
                if (intStartTime < intStartWorkingTime)
                {
                    strMessage += "請假時間必需大於上班時間!\n";
                }
                if (intStartTime > intEndWorkingTime)
                {
                    strMessage += "請假時間必需小於下班時間!\n";
                }
                //截止時間
                if (intEndTime < intStartWorkingTime)
                {
                    strMessage += "截止時間必需大於上班時間!\n";
                }
                if (intEndTime > intEndWorkingTime)
                {
                    strMessage += "截止時間必需小於下班時間!\n";
                }
            }
            else //夜班
            {
                
            }

            //休息時間若為半小時，請假起始時間在休息前起訖相減需為整點休息後需為半點
            if ((intStartRestTime - intEndRestTime) % 100 != 0) //休息時間不是整點
            {
                if (intEndTime > intStartTime)
                {
                    if (intStartTime <= intStartRestTime)
                    {
                        if (intStartTime % 100 != 0)
                        {
                            strMessage += "休息前起始時間必需為整點!\n";
                        }
                    }

                    if (intStartTime >= intEndRestTime)
                    {
                        if (intStartTime % 100 == 0)
                        {
                            strMessage += "休息後起始時間必需為半點!\n";
                        }
                    }

                    if (intEndTime <= intStartRestTime)
                    {
                        if (intEndTime % 100 != 0)
                        {
                            strMessage += "休息前截止時間必需為整點!\n";
                        }
                    }

                    if (intEndTime >= intEndRestTime)
                    {
                        if (intEndTime % 100 == 0)
                        {
                            strMessage += "休息後截止時間必需為半點!\n";
                        }
                    }
                }
            }

            int intStartDate = Convert.ToInt32(startDate);
            int intEndDate = Convert.ToInt32(endDate);

            DateTime dateStart = Convert.ToDateTime(endDate);
            DateTime dateEnd = Convert.ToDateTime(startDate);

            decimal hour = dateEnd.CompareTo(dateStart) * 8;
            //MessageBox(hour+"");
            if (Convert.ToInt16(Hours.ValueText) > hour)
            {
                strMessage += "時數計算有誤, 請確認請假起始日期時間/截止日期時間是否正確!\n";
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (engineErp != null) engineErp.close();
        }

        return strMessage;
    }

    /// <summary>
    /// 取得特休假時數
    /// </summary>
    /// <returns></returns>
    private decimal getERPDays()
    {
        AbstractEngine engineErp = null;
        decimal hour = 0;
        try
        {
            engineErp = getErpEngine(CompanyCode.ValueText);
            string strYear = DateTimeUtility.getSystemTime2(null).Substring(0, 4);
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strUserId = OriginatorGUID.ValueText;

            //string sql = "select TK900,TK902,TK007,TK008,MV021 from PALTK,CMSMV where TK001='" + OriginatorGUID.ValueText + "' and TK002 ='" + year + "' and TK001=MV001";
            //DataSet ds = engineErp.getDataSet(sql, "TEMP");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    string dateNow = DateTimeUtility.getSystemTime2(null);
            //    if (Convert.ToDecimal(ds.Tables[0].Rows[0][4].ToString().Replace("/","").Substring(4, 4)) > Convert.ToDecimal(dateNow.Replace("/","").Substring(4, 4)))
            //    {
            //        hour = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString());
            //    } 
            //    else
            //    {
            //        hour = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString())
            //            + Convert.ToDecimal(ds.Tables[0].Rows[0][2].ToString()) - Convert.ToDecimal(ds.Tables[0].Rows[0][3].ToString());
            //    }
            //}


            //顯示特休時數資訊
            int intNotUsedHours = 0;
            string sql = "select PALTK.*,MV021 from PALTK,CMSMV where TK001='" + strUserId + "' and TK002 ='" + strYear + "' and TK001=MV001";
            DataSet ds = engineErp.getDataSet(sql, "TEMP");
            //TK900: 上年度未休, TK902: 上年度已休, TK007: 本年度特休, TK008: 本年度已休
            if (ds.Tables[0].Rows.Count > 0)
            {
                string yearHours = "";
                string usedHours = "";
                string notUsedHours = "";
                usedHours = ds.Tables[0].Rows[0]["TK902"].ToString();
                notUsedHours = ds.Tables[0].Rows[0]["TK900"].ToString();
                decimal reserveHours = 0;
                if (!notUsedHours.Equals("") && !usedHours.Equals(""))
                {
                    reserveHours = Convert.ToDecimal(notUsedHours) - Convert.ToDecimal(usedHours); //上年度未休時數-上年度已休時數【此值若大於56小時，最多只能計算56小時】
                    if (reserveHours > 7 * 8)
                    {
                        reserveHours = 7 * 8;
                    }
                }

                string arriveDate = ds.Tables[0].Rows[0]["MV021"].ToString().Substring(4);
                string strNowDate = strNow.Replace("/", "").Substring(4, 4);
                if (Convert.ToDecimal(arriveDate) > Convert.ToDecimal(strNowDate))
                {

                    //今天的日期小於到職日
                    if (!notUsedHours.Equals("") && !usedHours.Equals(""))
                    {
                        decimal hours = Convert.ToDecimal(notUsedHours) - Convert.ToDecimal(usedHours);
                        intNotUsedHours = (int)hours / 1;
                    }
                }
                else
                {
                    //今天的日期大於等於到職日
                    usedHours = ds.Tables[0].Rows[0]["TK008"].ToString();
                    yearHours = ds.Tables[0].Rows[0]["TK007"].ToString();

                    decimal hours = Convert.ToDecimal(yearHours) + Convert.ToDecimal(reserveHours) - Convert.ToDecimal(usedHours);
                    intNotUsedHours = (int)hours / 1;

                }

                if (ds.Tables[0].Rows[0]["TK904"].ToString().Equals("Y"))
                {
                    intNotUsedHours = 0;
                }
            }
            hour = intNotUsedHours;
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (engineErp != null) engineErp.close();
        }
        
        return hour;
    }

    /// <summary>
    /// 透過元件資料相關資料
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private object[] callDataCenter(string id)
    {
        object[] aryTemp = null; //回傳值, object 陣列
        object[] aryCompanyData = new object[2];
        object[] aryData = null;
        System.IO.StreamWriter sw = null;
        string connectString = (string)Session["connectString"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        try
        {
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            sp = new WebServerProject.SysParam(engine);
            string companyId = sp.getParam(CompanyCode.ValueText + "SPADCompanyID");
            string companyDBName = sp.getParam(CompanyCode.ValueText + "SPADCompanyDBName");
            object strERPServerIP = sp.getParam("SPADComServerIP");
            object strERPVersion = "3";
            object erpVersion = "lngERPVersion";
            object erpServerIP = "strERPServerIP";
            object progId = "strProgID";
            object pali15 = "PALI15";
            object palef12 = "PALEF12";
            object aryPara = "aryPara";
            object aryCompany = "aryCompanyData";

            aryCompanyData[0] = companyId;
            aryCompanyData[1] = companyDBName;
            object aryObjCompanyData = aryCompanyData;

            switch (id)
            {
                case "4": //把日期傳到ERP查詢當天是否為假日 等於 getIsHoliday()
                    {
                        aryData = new object[9];
                        aryData[0] = id;
                        aryData[1] = OriginatorGUID.ValueText; //工號
                        aryData[2] = StartDate.ValueText.Replace("/", ""); //請假日期
                        aryData[3] = ClassCode.ValueText; //班別 (SD4:常日班)
                        aryData[4] = ""; //
                        aryData[5] = ""; //
                        aryData[6] = ""; //
                        aryData[7] = ""; //
                        aryData[8] = ""; //
                        object aryObjData = aryData;
                        

                        Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion); //ERPVersion
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP); //ERPServerIP
                        dicERPFormListTemp.Add(ref progId, ref pali15); //ERPProgID
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData); //資料參數
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);//Company ID & DBName

                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp);
                        //回傳值 [0]: 班別代號; [1]: 班別名稱; [2]: 是否為假日; 0: 非假日, 1: 假日
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                case "5": //ERP計算請假時數, 等於 Calculate_LeaveTime
                    {
                        //MessageBox("callDataCenter(" + id + ")");
                        aryData = new object[8];
                        aryData[0] = id;
                        aryData[1] = OriginatorGUID.ValueText; //工號
                        aryData[2] = StartDate.ValueText.Replace("/", ""); //請假日期
                        aryData[3] = EndDate.ValueText.Replace("/", ""); //請假日期迄
                        aryData[4] = CategoryCode.ValueText; //假別
                        aryData[5] = IsIncludeHoliday.ValueText; //請假含假日
                        aryData[6] = StartTime.ValueText.Replace(":", ""); //請假時間起
                        aryData[7] = EndTime.ValueText.Replace(":", ""); //請假時間迄
                        object aryObjData = aryData;

                        Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion); //ERPVersion
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP); //ERPServerIP
                        dicERPFormListTemp.Add(ref progId, ref palef12); //ERPProgID
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData); //資料參數
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);//Company ID & DBName
                        
                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp);
                        //回傳值 [0]: 狀況代碼; [1]: 休假時數 (會因前端資料而計算不正確); [2]: 訊息
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                case "6": //6.新增WFERP請假單
                    {
                        aryData = new object[8];
                        aryData[0] = id;
                        aryData[1] = OriginatorGUID.ValueText; //員工代號
                        aryData[2] = StartDate.ValueText.Replace("/", ""); //起始請假日期
                        aryData[3] = EndDate.ValueText.Replace("/", ""); //截止請假日期
                        aryData[4] = CategoryCode.ValueText; //假別
                        aryData[5] = IsIncludeHoliday.ValueText; //請假是否含假日
                        aryData[6] = StartTime.ValueText.Replace(":", ""); //請假起始時分
                        aryData[7] = EndTime.ValueText.Replace(":", ""); //請假截止時分
                        object aryObjData = aryData;

                        Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion);
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP);
                        dicERPFormListTemp.Add(ref progId, ref palef12);
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData);
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);
                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp); //送資料給ERP測試可不可以寫入
                        //回傳值 [0]: Y 成功; [1]: 訊息; [2]: 此次寫入ERP 回傳的資料(假單單號)
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                case "7": //7:寫字串到ERP備註欄位
                    {
                        aryData = new object[11];
                        string strUserID = (string)Session["UserID"];
                        string[] tempSerialNos = TempSerialNo.ValueText.Split(';');
                        object[] aryTempSerialNo = new object[tempSerialNos.Length];
                        for (int i = 0; i < aryTempSerialNo.Length; i++)
                        {
                            aryTemp = tempSerialNos[i].Split('-');
                            aryTempSerialNo[i] = aryTemp;
                        }

                        aryData[0] = "7";
                        aryData[1] = strUserID; //員工代號
                        aryData[2] = "";
                        aryData[3] = "";
                        aryData[4] = "";
                        aryData[5] = "";
                        aryData[6] = "";
                        aryData[7] = "";
                        aryData[8] = getSession(this.PageUniqueID, "SheetNo");
                        aryData[9] = Subject.ValueText.Substring(0, 100);
                        aryData[10] = aryTempSerialNo;
                        object aryObjData = aryData;

                        Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion);
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP);
                        dicERPFormListTemp.Add(ref progId, ref palef12);
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData);
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);
                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp);
                        //回傳值 [0]: Y 成功; [1]: 訊息
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                case "9": //取回員工基本班別,與上下班/休息時間等 (作業類別9) 等於 CheckFieldData->SetUserWorkInfo
                    {
                        string startDate = StartDate.ValueText;
                        if (startDate.Equals(""))
                        {
                            startDate = DateTimeUtility.convertDateTimeToString(DateTime.Now.AddDays(1)).Substring(0, 10);
                            startDate = startDate.Replace("/", "");
                        }

                        aryData = new object[3];
                        aryData[0] = id;
                        aryData[1] = OriginatorGUID.ValueText; //工號
                        aryData[2] = startDate.Replace("/", ""); //請假日期
                        object aryObjData = aryData;

                        Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion); //ERPVersion
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP); //ERPServerIP
                        dicERPFormListTemp.Add(ref progId, ref palef12); //ERPProgID
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData); //資料參數,本例: 作業類別/工號/請假明期
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);//Company ID & DBName

                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP(); //元件初始值方法
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp); //元件呼叫方法, dicERPFormListTemp 呼叫之參數
                        //回傳值 [0]: 班別代號; [1]: 班別名稱; [2]: 上班時間; [3]: 下班時間; [4]: 正常工作時數; [5]: 中間休息開始時間; [6]: 中間休息結束時間;
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                default:
                    aryTemp = new object[1];
                    break;
            }
        }
        catch (Exception e)
        {
            aryTemp = new object[1];
            base.writeLog(e);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            if (erpEngine != null)
            {
                erpEngine.close();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }
        return aryTemp;
    }

    /// <summary>
    /// 更新ERP假單
    /// </summary>
    private void updateErpForm(DataObject currentObject)
    {
        IOFactory factory = new IOFactory(); 
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        System.IO.StreamWriter sw = null;
        string sql = null;
        try
        {
            string connectString = (string)Session["connectString"];
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            erpEngine = base.getErpEngine(CompanyCode.ValueText);
            sp = new WebServerProject.SysParam(engine);
            string companyCode = currentObject.getData("CompanyCode");
            string companyId = sp.getParam(companyCode + "SPADCompanyID");
            string tempSerialNo = currentObject.getData("TempSerialNo");
            //sw.WriteLine(tempSerialNo);
            string[] aryTempSerialNo = tempSerialNo.Split(';');
            string strUserID = (string)Session["UserID"];
            string strDateNow = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
            string strSheetNo = currentObject.getData("SheetNo");
            //sw.WriteLine(strSheetNo);
            string strComment = strSheetNo + " - " + currentObject.getData("Description");
            if (strComment.Length > 100)
            {
                strComment = strComment.Substring(0, 100);
            }
            strComment = strComment.Replace("'","''");

            erpEngine = base.getErpEngine(currentObject.getData("CompanyCode"));
            for (int i = 0; i < aryTempSerialNo.Length; i++)
            {
                string[] aryWfFormIDs = aryTempSerialNo[i].Split('-');
                for (int j = 0; j < aryWfFormIDs.Length; j++)
                {
                    string strWfFormID = aryWfFormIDs[0];
                    string strWfSerialNo = aryWfFormIDs[1];
                    sql = "UPDATE PALTF SET MODIFIER='" + strUserID + "', MODI_DATE='" + strDateNow + "', FLAG = FLAG+1, TF010='" + strComment + "', TF904='" + strSheetNo + "' WHERE COMPANY='" + companyId + "' AND TF001='" + OriginatorGUID.ValueText + "' AND TF002='" + strWfFormID + "' AND TF003='" + strWfSerialNo + "'";
                    //sw.WriteLine(sql);
                    erpEngine.executeSQL(sql);
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            if (erpEngine != null)
            {
                erpEngine.close();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }
    }

    /// <summary>
    /// 核准ERP單據
    /// </summary>
    private string approveErpForm()
    {
        string strResult = "";
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        EFGateWayOfERP.Engine erpGateWayEngine = null;
        EFGateWayOfERP.CallForERP callForERP = null;
        System.IO.StreamWriter sw = null;
        try
        {
            string comExeResult = null;
            bool isError = false;
            string connectString = (string)Session["connectString"];
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            erpEngine = base.getErpEngine(CompanyCode.ValueText);
            sp = new WebServerProject.SysParam(engine);

            string companyCode = CompanyCode.ValueText;
            string companyId = sp.getParam(companyCode + "SPADCompanyID");
            object strERPServerIP = sp.getParam("SPADComServerIP");
            string strEF2KWebSite = sp.getParam("EF2KWebSite");

            object strERPVersion = "3"; //ERP版本
            object strReturnType = "2"; //回寫狀態
            object strWfFormID = OriginatorGUID.ValueText; //表單單別（員工代號）
            object strAction = "0"; //確認狀態
            object strProgID = "PALI12"; //程式代號
            object strCompID = companyId; //公司別代號
            object strUserID = (string)Session["UserID"]; //審核人員工代號
            object strParameter4 = ""; //參數四	
            object strKeyNumber = "3"; //Key值個數
            object strComfirmObject = "TransManager.TxnManager"; //確認元
            object strComPRID = "PALI12"; //確認元程式代號
            object strComDate = ""; // DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //確認日期
            object strUserNTID = "LISA_LAI";
            string sql = "select left(ldapid, charindex('@', ldapid)-1) from Users where id = '" + Utility.filter(strUserID.ToString()) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                strUserNTID = ds.Tables[0].Rows[0][0].ToString();
            }
            string strSiteName = "EF2KWeb";
            int idx = strEF2KWebSite.LastIndexOf("/");
            if (idx > 0)
            {
                strSiteName = strEF2KWebSite.Substring(idx + 1);
            }

            object erpVersion = "lngERPVersion";
            object returnType = "lngReturnType";
            object wfFormID = "strWfFormID";
            object wfSheetNo = "strWfSheetNo";
            object action = "lngAction";
            object progID = "strProgID";
            object compID = "strCompID";
            object userID = "strUserID";
            object parameter3 = "strParameter3";
            object parameter4 = "strParameter4";
            object keyNumber = "lngKeyNumber";
            object comfirmObject = "strComfirmObject";
            object comPRID = "strComPRID";
            object comDate = "strComDate";
            object userNTID = "strUserNTID";
            object erpServerIP = "strERPServerIP";
            object debugMod = "strDebugMod";
            object y = "Y";


            string strDateNow = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");

            callForERP = new EFGateWayOfERP.CallForERP();
            erpGateWayEngine = new EFGateWayOfERP.Engine();
            string[] tempSerialNos = TempSerialNo.ValueText.Split(';');

            for (int i = 0; i < tempSerialNos.Length; i++)
            {
                string[] arySerialNos = tempSerialNos[i].Split('-');
                object strWfSheetNo = arySerialNos[0]; //表單單號（請假日期）
                object strParameter3 = arySerialNos[1]; //流水號

                Scripting.Dictionary dicERPInfoSet = new Scripting.Dictionary();
                dicERPInfoSet.Add(ref erpVersion, ref strERPVersion); //ERP版本
                dicERPInfoSet.Add(ref returnType, ref strReturnType); //回寫狀態
                dicERPInfoSet.Add(ref wfFormID, ref strWfFormID); //ERP程式代號
                dicERPInfoSet.Add(ref wfSheetNo, ref strWfSheetNo); //ERP單號
                dicERPInfoSet.Add(ref action, ref strAction); //Action
                dicERPInfoSet.Add(ref progID, ref strProgID); //EF表單代號
                dicERPInfoSet.Add(ref compID, ref strCompID); //ERP公司別代號
                dicERPInfoSet.Add(ref userID, ref strUserID); //EF員工代號
                dicERPInfoSet.Add(ref parameter3, ref strParameter3); //ERP 參數3
                dicERPInfoSet.Add(ref parameter4, ref strParameter4); //ERP 參數4
                dicERPInfoSet.Add(ref keyNumber, ref strKeyNumber); //Key值個數
                dicERPInfoSet.Add(ref comfirmObject, ref strComfirmObject); //ERP確認元代號
                dicERPInfoSet.Add(ref comPRID, ref strComPRID); //ERP確認元程式代號
                dicERPInfoSet.Add(ref comDate, ref strComDate); //確認日期
                dicERPInfoSet.Add(ref userNTID, ref strUserNTID); //EF使用者NT登入帳號
                dicERPInfoSet.Add(ref erpServerIP, ref strERPServerIP); //ERPServerIP
                dicERPInfoSet.Add(ref debugMod, ref y);

                object[] aryTemp = (object[])callForERP.ChkERP_BeforeApprove(dicERPInfoSet);
                if (Convert.ToString(aryTemp[0]).Equals("Y"))
                {
                    comExeResult = erpGateWayEngine.saveERPInfoToTemp(dicERPInfoSet, strSiteName); //確認ERP是否可回寫
                    aryTemp = (object[])callForERP.SetERP_AfterApprove(dicERPInfoSet); //寫回審核結果到ERP
                    if (Convert.ToString(aryTemp[0]).Equals("N")) //執行有誤
                    {
                        isError = true;
                    }
                    else
                    {
                        sql = "UPDATE PALTF SET MODIFIER='" + strUserID + "', MODI_DATE='" + strDateNow + "', TF905='SPAD001' WHERE COMPANY='" + companyId + "' AND TF001='" + OriginatorGUID.ValueText + "' AND TF002='" + strWfSheetNo + "' AND TF003='" + strParameter3 + "'";
                        //erpEngine.executeSQL(sql);
                        if (!erpEngine.executeSQL(sql))
                        {
                            strResult += erpEngine.errorString;
                        }
                    }
                }
                else //執行有誤
                {
                    isError = true;
                }
                if (isError)
                {
                    string strErrReason = "執行表單簽核之前續處理作業時發生錯誤. ";
                    string strErrSource = "請假單 [確認過程] 程式代號=" + strProgID + " 單別=" + strWfFormID + " 單號=" + strWfSheetNo + " ";
                    string strErrDesc = "錯誤訊息=" + Convert.ToString(aryTemp[1]);
                    strResult += strErrReason + strErrSource + strErrDesc;
                }
                dicERPInfoSet = null;
            }
            callForERP = null;
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            if (erpEngine != null)
            {
                erpEngine.close();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }
        return strResult;
    }

    /// <summary>
    /// 作廢ERP單據
    /// </summary>
    private string widhDrawErpForm(AbstractEngine engine, DataObject currentObject)
    {
        string strResult = "";
        IOFactory factory = new IOFactory();
        AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        string sql = null;
        EFGateWayOfERP.Engine erpGateWayEngine = new EFGateWayOfERP.Engine();
        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
        System.IO.StreamWriter sw = null;
        try
        {
            sw = new System.IO.StreamWriter(@"d:\ECP\WebFormPT\web\LogFolder\SPAD001.log", true, System.Text.Encoding.Default);
            string companyCode = currentObject.getData("CompanyCode");
            erpEngine = base.getErpEngine(companyCode);
            sp = new WebServerProject.SysParam(engine);
            string companyId = sp.getParam(companyCode + "SPADCompanyID");
            string strERPServerIP = sp.getParam("SPADComServerIP");
            string strEF2KWebSite = sp.getParam("EF2KWebSite");
            string strSheetNo = currentObject.getData("SheetNo");
            
            string strERPVersion = "3"; //ERP版本
            string strReturnType = "2"; //回寫狀態
            string[] values = base.getUserInfo(engine, currentObject.getData("OriginatorGUID"));
            string strWfFormID = values[0]; //表單單別（員工代號）
            string strAction = "2"; //確認狀態
            string strProgID = "PALI12"; //程式代號
            string strCompID = companyId; //公司別代號
            string strUserID = (string)Session["UserID"]; //審核人員工代號
            if (string.IsNullOrEmpty(strUserID))
            {
                string[][] users = base.getGroupdUser(engine, "SMP-HRADM");
                strUserID = users[0][0];
            }
			string strParameter4 = ""; //參數四	
            string strKeyNumber = "3"; //Key值個數
            string strComfirmObject = "TransManager.TxnManager"; //確認元
            string strComPRID = "PALI12"; //確認元程式代號
            string strComDate = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //確認日期
            string strSiteName = "EF2KWeb";
            int idx = strEF2KWebSite.LastIndexOf("/");
            if (idx > 0)
            {
                strSiteName = strEF2KWebSite.Substring(idx + 1);
            }

            string strUserNTID = "LISA_LAI";
            sql = "select left(ldapid, charindex('@', ldapid)-1) from Users where id = '" + Utility.filter(strUserID) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                strUserNTID = ds.Tables[0].Rows[0][0].ToString();
            }
            
            string[] tempSerialNos = currentObject.getData("TempSerialNo").Split(';');
            for(int i=0; i<tempSerialNos.Length; i++) {
                string[] arySerialNos = tempSerialNos[i].Split('-');
                string strWfSheetNo = arySerialNos[0]; //表單單號（請假日期）
                string strParameter3 = arySerialNos[1]; //流水號

                //Scripting.Dictionary dicERPInfoSet = new Scripting.Dictionary();
                //dicERPInfoSet.Add("lngERPVersion", Convert.ToInt32(strERPVersion)); //ERP版本
                //dicERPInfoSet.Add("lngReturnType", Convert.ToInt32(strReturnType)); //回寫狀態
                //dicERPInfoSet.Add("strWfFormID", strWfFormID); //ERP程式代號
                //dicERPInfoSet.Add("strWfSheetNo", strWfSheetNo); //ERP單號
                //dicERPInfoSet.Add("lngAction", Convert.ToInt32(strAction)); //Action
                //dicERPInfoSet.Add("strProgID", strProgID); //'EF表單代號
                //dicERPInfoSet.Add("strCompID", strCompID); //ERP公司別代號
                //dicERPInfoSet.Add("strUserID", strUserID); //EF員工代號
                //dicERPInfoSet.Add("strParameter3", strParameter3); //ERP 參數3
                //dicERPInfoSet.Add("strParameter4", strParameter4); //ERP 參數4
                //dicERPInfoSet.Add("lngKeyNumber", Convert.ToInt32(strKeyNumber)); //Key值個數
                //dicERPInfoSet.Add("strComfirmObject", strComfirmObject); //ERP確認元代號
                //dicERPInfoSet.Add("strComPRID", strComPRID); //ERP確認元程式代號
                //dicERPInfoSet.Add("strComDate", strComDate); //確認日期
                //dicERPInfoSet.Add("strUserNTID", strUserNTID); //EF使用者NT登入帳號
                //dicERPInfoSet.Add("strERPServerIP", strERPServerIP); //ERP Server IP
                //dicERPInfoSet.Add("strDebugMod", "Y");
                //string result = erpGateWayEngine.saveERPInfoToTemp(dicERPInfoSet, strSiteName);
                //object[] aryTemp = (object[])callForERP.SetERP_AfterApprove(dicERPInfoSet);

                //更新ERP該假單
                //MODIFYIER --最後修改者
                //MODI_DATE --最後修改日
                //FLAG --目前值加1
                //TF010 --備註欄
                //TF011 --Y 已確認, N 未確認, V 已作廢
                //TF012 --確認日期
                //TF013 --確認人員
                //TF904  --單號
                sql = "UPDATE PALTF SET MODIFIER='" + strUserID + "', MODI_DATE='" + strComDate + "', FLAG = FLAG+1, TF011='V', TF012='" + strComDate + "', TF013='" + strUserID + "' WHERE COMPANY='" + strCompID + "' AND TF001='" + strWfFormID + "' AND TF002='" + strWfSheetNo + "' AND TF003='" + strParameter3 + "'";
                sw.WriteLine(sql);
                erpEngine.executeSQL(sql);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            if (erpEngine != null)
            {
                erpEngine.close();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }
        return strResult;
    }

    /// <summary>
    /// 核准ERP單據測試
    /// </summary>
    /// <returns></returns>
    private string approveErpFormTest()
    {
        string strResult = "";
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        EFGateWayOfERP.Engine erpGateWayEngine = null;
        EFGateWayOfERP.CallForERP callForERP = null;

        try
        {
            string connectString = (string)Session["connectString"];
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            erpEngine = base.getErpEngine(CompanyCode.ValueText);
            sp = new WebServerProject.SysParam(engine);
            object strERPServerIP = sp.getParam("SPADComServerIP");
            string strEF2KWebSite = sp.getParam("EF2KWebSite");
            object strERPVersion = "3"; //ERP版本
            object strReturnType = "2"; //回寫狀態
            object strWfFormID = "00010"; //OriginatorGUID.ValueText; //表單單別（員工代號）
            object strAction = "0"; //確認狀態
            object strProgID = "PALI12"; //程式代號
            object strCompID = "TP_TEST"; //companyId; //公司別代號
            object strUserID = "2854"; //(string)Session["UserID"]; //審核人員工代號
            object strParameter4 = ""; //參數四          
            object strKeyNumber = "3"; //Key值個數
            object strComfirmObject = "TransManager.TxnManager"; //確認元
            object strComPRID = "PALI12"; //確認元程式代號
            object strComDate = ""; // DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //確認日期
            object strUserNTID = "LISA_LAI";
            string sql = "select left(ldapid, charindex('@', ldapid)-1) from Users where id = '" + Utility.filter(strUserID.ToString()) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                strUserNTID = ds.Tables[0].Rows[0][0].ToString();
            }
            string strSiteName = "EF2KWeb";
            int idx = strEF2KWebSite.LastIndexOf("/");

            if (idx > 0)
            {
                strSiteName = strEF2KWebSite.Substring(idx + 1);
            }

            strUserNTID = "LISA_LAI";
            strSiteName = "EF2KPilot";

            object erpVersion = "lngERPVersion";
            object returnType = "lngReturnType";
            object wfFormID = "strWfFormID";
            object wfSheetNo = "strWfSheetNo";
            object action = "lngAction";
            object progID = "strProgID";
            object compID = "strCompID";
            object userID = "strUserID";
            object parameter3 = "strParameter3";
            object parameter4 = "strParameter4";
            object keyNumber = "lngKeyNumber";
            object comfirmObject = "strComfirmObject";
            object comPRID = "strComPRID";
            object comDate = "strComDate";
            object userNTID = "strUserNTID";
            object erpServerIP = "strERPServerIP";
            object debugMod = "strDebugMod";
            object y = "Y";

            callForERP = new EFGateWayOfERP.CallForERP();
            erpGateWayEngine = new EFGateWayOfERP.Engine();
            string[] tempSerialNos = "20140711-0001".Split(';');
            for (int i = 0; i < tempSerialNos.Length; i++)
            {
                string[] arySerialNos = tempSerialNos[i].Split('-');
                object strWfSheetNo = arySerialNos[0]; //表單單號（請假日期）
                object strParameter3 = arySerialNos[1]; //流水號
                Scripting.Dictionary dicERPInfoSet = new Scripting.Dictionary();
                dicERPInfoSet.Add(ref erpVersion, ref strERPVersion); //ERP版本
                dicERPInfoSet.Add(ref returnType, ref strReturnType); //回寫狀態
                dicERPInfoSet.Add(ref wfFormID, ref strWfFormID); //ERP程式代號
                dicERPInfoSet.Add(ref wfSheetNo, ref strWfSheetNo); //ERP單號
                dicERPInfoSet.Add(ref action, ref strAction); //Action
                dicERPInfoSet.Add(ref progID, ref strProgID); //EF表單代號
                dicERPInfoSet.Add(ref compID, ref strCompID); //ERP公司別代號
                dicERPInfoSet.Add(ref userID, ref strUserID); //EF員工代號
                dicERPInfoSet.Add(ref parameter3, ref strParameter3); //ERP 參數3
                dicERPInfoSet.Add(ref parameter4, ref strParameter4); //ERP 參數4
                dicERPInfoSet.Add(ref keyNumber, ref strKeyNumber); //Key值個數
                dicERPInfoSet.Add(ref comfirmObject, ref strComfirmObject); //ERP確認元代號
                dicERPInfoSet.Add(ref comPRID, ref strComPRID); //ERP確認元程式代號
                dicERPInfoSet.Add(ref comDate, ref strComDate); //確認日期
                dicERPInfoSet.Add(ref userNTID, ref strUserNTID); //EF使用者NT登入帳號
                dicERPInfoSet.Add(ref erpServerIP, ref strERPServerIP); //ERPServerIP
                dicERPInfoSet.Add(ref debugMod, ref y);

                object[] aryTemp = (object[])callForERP.ChkERP_BeforeApprove(dicERPInfoSet);
                if (Convert.ToString(aryTemp[0]).Equals("Y"))
                {
                    object result = erpGateWayEngine.saveERPInfoToTemp(dicERPInfoSet, strSiteName);
                    if (!Convert.ToString(result).Equals(""))
                    {
                        strResult += result;
                    }
                }
                else
                {
                    //執行有誤
                    string strErrReason = "執行表單簽核之前續處理作業時發生錯誤. ";
                    string strErrSource = "請假單 [確認過程] 程式代號=" + strProgID + " 單別=" + strWfFormID + " 單號=" + strWfSheetNo + " ";
                    string strErrDesc = "錯誤訊息=" + Convert.ToString(aryTemp[1]);
                    strResult += strErrReason + strErrSource + strErrDesc;
                }

                dicERPInfoSet = null;
                callForERP = null;
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            if (erpEngine != null)
            {
                erpEngine.close();
            }
        }
        return strResult;
    }

    private void showHourDesc(string hours)
    {
        int j = 0;
        bool isGotHour = int.TryParse(hours, out j);
        if (isGotHour)
        {
            int hour = Convert.ToInt16(hours);
            int day = hour / 8;
            int remainder = hour % 8;
            LblHoursDesc.Text = "(合計: " + day + "天" + remainder + "小時)";
        }
    }

    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPAD001.log", true, System.Text.Encoding.Default);
            sw.WriteLine(line);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }
	
	private static string GetEmpInfoXML(string pEmpCode)
	{
		//JobId : 1A3A9972-BFAF-4B81-838B-F337A778204F
		string str = string.Format(@"<Request><Access><Authentication user=""gp"" password=""""/></Access><RequestContent><ServiceType>Dcms.HR.Services.IAttendanceCollectService,Dcms.HR.Business.AttendanceCollect</ServiceType><Method>GetCollectByEmpId</Method><Parameters><Parameter type=""System.String"">1A3A9972-BFAF-4B81-838B-F337A778204F</Parameter><Parameter type=""System.DateTime"">2017/10/20</Parameter><Parameter type=""System.DateTime"">2017/10/25</Parameter></Parameters></RequestContent></Request>");
		//string str = @"<Request><Access><Authentication password="""" user=""gp""/></Access><RequestContent><ServiceType>Dcms.HR.Services.IAttendanceParameterService,Dcms.HR.Business.AttendanceParameter</ServiceType><Method>GetEmpOTHours</Method><Parameters><Parameter type=""System.String"">B7A52998-ED73-4187-BA0B-2587632D13B3</Parameter><Parameter type=""System.DateTime"">2018/01/12</Parameter><Parameter type=""System.Boolean"">True</Parameter><Parameter type=""System.Boolean"">False</Parameter><Parameter type=""System.Boolean"">False</Parameter><Parameter type=""System.String""/></Parameters></RequestContent></Request>";
		return str;
	}
}