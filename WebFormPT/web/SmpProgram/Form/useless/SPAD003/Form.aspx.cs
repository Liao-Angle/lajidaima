using System;
using System.Data;
using System.Configuration;
using System.Collections;
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

public partial class SmpProgram_Form_SPAD003_Form : SmpAdFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPAD003";
        AgentSchema = "WebServerProject.form.SPAD003.SmpOvertimeFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string userId = (string)Session["UserId"];
        string[,] ids = null;
        string sql = null;

        //公司別
        ids = new string[,]{
                {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad003_form_aspx.language.ini", "message", "smp", "新普科技")},
                {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad003_form_aspx.language.ini", "message", "tp", "中普科技")}
            };
        CompanyCode.setListItem(ids);

        string orgId = "SMP";
        sql = "select orgId from EmployeeInfo where empNumber='" + userId + "'";
        string value = (string)engine.executeScalar(sql);
        if (value != null)
        {
            orgId = value;
        }
        CompanyCode.ValueText = orgId;
        CompanyCode.ReadOnly = true;

        OriginatorGUID.Display = false;
        //申請部門
        OrganizationUnitGUID.clientEngineType = engineType;
        OrganizationUnitGUID.connectDBString = connectString;
        if (OrganizationUnitGUID.ValueText.Equals(""))
        {
            OrganizationUnitGUID.ValueText = si.fillerOrgID; //預設帶出登入者部門
            OrganizationUnitGUID.doValidate(); //帶出人員開窗元件中的部門名稱
        }
        CheckbyGUID.clientEngineType = engineType;
        CheckbyGUID.connectDBString = connectString;

        CreationDateTime.ReadOnly = true;

        UserGUID.clientEngineType = engineType;
        UserGUID.connectDBString = connectString;

        bool isAddNew = base.isNew();
        DataObjectSet detailSet = null;
        if (isAddNew)
        {
            detailSet = new DataObjectSet();
            detailSet.isNameLess = true;
            detailSet.setAssemblyName("WebServerProject");
            detailSet.setChildClassString("WebServerProject.form.SPAD003.SmpOvertimeDetail");
            detailSet.setTableName("SmpOvertimeDetail");
            detailSet.loadFileSchema();
            objects.setChild("SmpOvertimeDetail", detailSet);

            string dateTimeNow = DateTimeUtility.getSystemTime2(null);
            StartDateTime.ValueText = dateTimeNow.Substring(0, 10) + " 17:20:00";
            EndDateTime.ValueText = dateTimeNow.Substring(0, 10) + " 20:20:00";
            Hours.ValueText = "3";

            CheckbyGUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
            UserGUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
        }
        else
        {
            detailSet = objects.getChild("SmpOvertimeDetail");
        }
        OvertimeDetailList.dataSource = detailSet;
        OvertimeDetailList.HiddenField = new string[] { "GUID", "OvertimeFormGUID", "UserGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        OvertimeDetailList.updateTable();

        Hours.ReadOnly = true;
        
        //改變工具列順序
        base.initUI(engine, objects);
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //顯示單號
        base.showData(engine, objects);
        //主旨
        Subject.ValueText = objects.getData("Subject");
        
        //公司別
        CompanyCode.ValueText = objects.getData("CompanyCode");

        //申請人
        OriginatorGUID.ValueText = objects.getData("OriginatorGUID");

        OrganizationUnitGUID.GuidValueText = objects.getData("OrganizationUnitGUID");
        OrganizationUnitGUID.doGUIDValidate();

        CheckbyGUID.GuidValueText = objects.getData("CheckbyGUID");
        if (!CheckbyGUID.GuidValueText.Equals(""))
        {
            CheckbyGUID.doGUIDValidate();
        }
        //填表日期
        CreationDateTime.ValueText = objects.getData("D_INSERTTIME");

        string actName = Convert.ToString(getSession("ACTName"));
        if (actName == "" || actName.Equals("填表人"))
        {

        }
        else
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
            OrganizationUnitGUID.ReadOnly = true;
            CreationDateTime.ReadOnly = true;
            CheckbyGUID.ReadOnly = true;
            UserGUID.ReadOnly = true;
            StartDateTime.ReadOnly = true;
            EndDateTime.ReadOnly = true;
            Hours.ReadOnly = true;
            Reason.ReadOnly = true;
            Remark.ReadOnly = true;
            OvertimeDetailList.ReadOnly = true;
        }

        DataObjectSet detailSet = objects.getChild("SmpOvertimeDetail");
        OvertimeDetailList.dataSource = detailSet;
        OvertimeDetailList.updateTable();
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("CompanyCode", CompanyCode.ValueText);
            objects.setData("OriginatorGUID", OriginatorGUID.ValueText);
            objects.setData("OrganizationUnitGUID", OrganizationUnitGUID.GuidValueText);
            objects.setData("CheckbyGUID", CheckbyGUID.GuidValueText);
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);
        }

        for (int i = 0; i < OvertimeDetailList.dataSource.getAvailableDataObjectCount(); i++)
        {
            OvertimeDetailList.dataSource.getAvailableDataObject(i).setData("OvertimeFormGUID", objects.getData("GUID"));
        }

        //beforeSign 加簽
        string actName = Convert.ToString(getSession("ACTName").ToString());
        if (actName.Equals("人事承辦人員"))
        {
            setSession("IsAddSign", "AFTER");
        }
        //call beforeSetFlow
        setSession("IsSetFlow", "Y");
    }

    protected void OvertimeDetailList_ShowRowData(DataObject objects)
    {
        //bool isAddNew = (bool)getSession("isNew");
        //if (!isAddNew)
        //{
            UserGUID.GuidValueText = objects.getData("UserGUID");
            UserGUID.doGUIDValidate();
            StartDateTime.ValueText = objects.getData("StartDateTime");
            EndDateTime.ValueText = objects.getData("EndDateTime");
            Hours.ValueText = objects.getData("Hours");
            Reason.ValueText = objects.getData("Reason");
            Remark.ValueText = objects.getData("Remark");
        //}
    }

    protected bool OvertimeDetailList_SaveRowData(DataObject objects, bool isNew)
    {
        string strErrMsg = "";
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string hours = Hours.ValueText;
            DateTime restStartDateTime = Convert.ToDateTime(StartDateTime.ValueText.Substring(1, 10) + " 12:00:00");
            DateTime restEndDateTime = Convert.ToDateTime(EndDateTime.ValueText.Substring(1, 10) + " 13:00:00");


            if (!Convert.ToString(StartDateTime.ValueText).Equals(""))
            {
                DateTime startDateTime = Convert.ToDateTime(StartDateTime.ValueText);
                if (startDateTime.CompareTo(restStartDateTime) > 0 && startDateTime.CompareTo(restEndDateTime) < 0)
                {
                    strErrMsg += LblStartDateTime.Text + ": 不可落於休息時間!";
                }

                //AbstractEngine engineErp = base.getErpEngine(CompanyCode.ValueText);
                string id = UserGUID.ValueText;
                string userName = UserGUID.ReadOnlyValueText;
                string startDate = StartDateTime.ValueText.Substring(0, 10);
                //string sql = "select TG001 from PALTG where TG001='" + id + "' and TG002='" + startDate + "'";
                string userGUID = UserGUID.GuidValueText;
                string sql = "select UserGUID from SmpOvertimeDetail a, SMWYAAA b where a.UserGUID='" + userGUID + "' and a.StartDateTime like '" + startDate + "%' and a.OvertimeFormGUID = b.SMWYAAA019 and a.OvertimeFormGUID = b.SMWYAAA019 and SMWYAAA020 in ('I', 'Y') ";
                //Object value = engineErp.executeScalar(sql);
                Object value = engine.executeScalar(sql);
                if (value != null && !value.ToString().Equals(""))
                {
                    strErrMsg += "工號: " + id + ", 日期: " + startDate + ", 此人員加班日期重複";
                }

                for (int i = 0; i < OvertimeDetailList.dataSource.getAvailableDataObjectCount(); i++)
                {
                    DataObject dataObject = OvertimeDetailList.dataSource.getAvailableDataObject(i);
                    string listUserId = dataObject.getData("id");
                    string listStartDate = dataObject.getData("StartDateTime").Substring(0, 10);
                    if (id.Equals(listUserId) && startDate.Equals(listStartDate))
                    {
                        strErrMsg += "工號: " + id + ", 日期: " + startDate + ", 此人員加班日期重複";
                    }
                }
            }

            if (!Convert.ToString(EndDateTime.ValueText).Equals(""))
            {
                DateTime endDateTime = Convert.ToDateTime(EndDateTime.ValueText);
                if (endDateTime.CompareTo(restStartDateTime) > 0 && endDateTime.CompareTo(restEndDateTime) < 0)
                {
                    strErrMsg += LblEndDateTime.Text + ": 不可落於休息時間!";
                }
            }

            if (Convert.ToString(Hours.ValueText).Equals(""))
            {
                strErrMsg += LblHours.Text + ": 未產生加班時數或未達加班計算標準!";
            }
            else
            {
                if (Convert.ToDecimal(hours) <= 0)
                {
                    strErrMsg += LblHours.Text + ": 加班時間選擇錯誤，加班時數為負數!";
                }
            }

            if (!strErrMsg.Equals(""))
            {
                MessageBox(strErrMsg);
                return false;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }
        
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("OvertimeFormGUID", "TEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("UserGUID", UserGUID.GuidValueText);
        objects.setData("id", UserGUID.ValueText);
        objects.setData("userName", UserGUID.ReadOnlyValueText);
        objects.setData("StartDateTime", StartDateTime.ValueText);
        objects.setData("EndDateTime", EndDateTime.ValueText);
        objects.setData("Hours", Hours.ValueText);
        objects.setData("Reason", Reason.ValueText);
        objects.setData("Remark", Remark.ValueText);
        return true;
    }

    /// <summary>
    /// 畫面資料稽核。
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
            if (Convert.ToString(Subject.ValueText).Equals(""))
            {
                strErrMsg += LblSubject.Text + "欄位不得為空!\n";
            }

            if (Convert.ToString(OrganizationUnitGUID.ValueText).Equals(""))
            {
                strErrMsg += LblOrganizationUnit.Text + "欄位不得為空!\n";
            }

            int rowCount = OvertimeDetailList.dataSource.getAvailableDataObjectCount();
            if (rowCount <= 0)
            {
                strErrMsg += "加班申請明細不得為空!\n";
            }

            if (!strErrMsg.Equals(""))
            {
                pushErrorMessage(strErrMsg);
                result = false;
            }
        }
        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);
        string[] deptValues = base.getOrgUnitInfo(engine, OrganizationUnitGUID.GuidValueText); 

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];
        //si.ownerID = OriginatorGUID.ValueText; //表單關系人
        //si.ownerID = deptValues[3];
        si.ownerName = (string)Session["UserName"];
        //si.ownerName = OriginatorGUID.ValueText;
        //si.ownerName = deptValues[4];
        //si.ownerOrgID = depData[0];
        si.ownerOrgID = deptValues[0];
        //si.ownerOrgName = depData[1];
        si.ownerOrgName = deptValues[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定自動編碼格式所需變數值。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID);
        return hs;
    }

    protected override string beforeSetFlow(AbstractEngine engine, string setFlowXml)
    {
        string notifierGUID = ""; //通知人員
        string[] userFunc = getUserRoles(engine, "部門收發", OrganizationUnitGUID.ValueText);
        notifierGUID = userFunc[1]; //部門助理

        string xml = "";
        if (!notifierGUID.Equals(""))
        {
            xml += "<list><list>";
            xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
            xml += "       <performers>";
            xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
            xml += "                <OID>" + notifierGUID + "</OID>";
            xml += "                <participantType><value>HUMAN</value></participantType>";
            xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
            xml += "        </performers>";
            xml += "        <multiUserMode><value>FIREST_GET_FIRST_WIN</value></multiUserMode>";
            xml += "        <name>通知</name>";
            xml += "        <performType><value>NOTICE</value></performType>";
            xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
            xml += "</list></list>";
            setFlowXml = xml;
        }
        return setFlowXml;
    }

    /// <summary>
    /// 設定流程變數。目前主要是用來傳遞流程所需要的變數值。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string creatorId = si.fillerID;
        string notifierId = ""; //通知人員
        string[] userFunc = getUserRoles(engine, "部門收發", OrganizationUnitGUID.ValueText);
        notifierId = userFunc[2]; //部門助理
        string checkbyId = CheckbyGUID.ValueText;
        string orgUnitManagerId = "";
        string[] userInfo = base.getUserGUID(engine, creatorId);
        string[] managerInfo = base.getUserManagerInfo(engine, userInfo[0]);
        if (managerInfo[1].Equals(""))
        {
            orgUnitManagerId = managerInfo[1];
        }
        string[] orgUnitInfo = base.getOrgUnitInfo(engine, OrganizationUnitGUID.GuidValueText);
        if (!orgUnitInfo[3].Equals(""))
        {
            orgUnitManagerId = orgUnitInfo[3];
        }
        xml += "<SPAD003>";
        xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
        xml += "<notifier DataType=\"java.lang.String\">" + notifierId + "</notifier>";
        xml += "<checkby DataType=\"java.lang.String\">" + checkbyId + "</checkby>";
        xml += "<orgUnitManager DataType=\"java.lang.String\">" + orgUnitManagerId + "</orgUnitManager>";
        xml += "</SPAD003>";

        //表單代號
        param["SPAD003"] = xml;
        return "SPAD003";
    }

    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        base.afterSend(engine, currentObject);
    }

    /// <summary>
    /// 若有加簽，送簽核前呼叫。
    /// 加簽時系統會設定Session("IsAddSign")，所以必需在saveData時執行 setSession("IsAddSign", "AFTER");
    /// AFTER 代表往後簽核
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        string actName = Convert.ToString(getSession("ACTName").ToString());
        if (actName.Equals("人事承辦人員"))
        {
            //建立ERP單據
            DataObject currentObject = (DataObject)getSession("currentObject");
            string result = createErpForm(engine, currentObject);
            if (!result.Equals(""))
            {
                throw new Exception(result);
            }
        }
        return base.beforeSign(engine, isAfter, addSignXml);
    }

    /// <summary>
    /// 按下送簽按鈕後呼叫此方法。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
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
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterApprove(engine, currentObject, result);
    }

    protected void DateTimeClick(string values)
    {
        DateTime startDateTime = Convert.ToDateTime(StartDateTime.ValueText);
        DateTime endDateTime = Convert.ToDateTime(EndDateTime.ValueText);
        double hours = (endDateTime - startDateTime).TotalHours;
        double hour = (int)(hours / 0.5) * 0.5;
        string h = "";
        if (hour > 0)
        {
            h = Convert.ToString(hour);
        }

        DateTime startResetDateTime = Convert.ToDateTime(StartDateTime.ValueText.Substring(0, 10) + " 12:00:00");
        DateTime endResetDateTime = Convert.ToDateTime(StartDateTime.ValueText.Substring(0, 10) + " 13:00:00");
        
        if (startResetDateTime.CompareTo(startDateTime) >= 0 && endDateTime.CompareTo(endResetDateTime) >= 0)
        {
            h = Convert.ToString(hour-1);
        }

        Hours.ValueText = h;
    }

    protected string createErpForm(AbstractEngine engine, DataObject currentObject)
    {
        string result = "";
        IOFactory factory = new IOFactory();
        WebServerProject.SysParam sp = null;
        //System.IO.StreamWriter sw = null;
        AbstractEngine erpEngine = null;

        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
            sp = new WebServerProject.SysParam(engine);
            string companyId = CompanyCode.ValueText;
            erpEngine = base.getErpEngine(companyId);
            string creatorGUID = currentObject.getData("D_INSERTUSER");
            string[] values = base.getUserInfo(engine,creatorGUID);
            string creatorId = values[0];
            
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            
            DataObjectSet set = currentObject.getChild("SmpOvertimeDetail");
            for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
            {
                string strTG001 = "", strTG002 = "", strTG003 = "", strTG004 = "", strTG005 = "", strTG006 = "",
	                strTG007 = "", strTG008 = "", strTG009 = "", strTG010 = "", strTG011 = "", strTG012 = "",
	                strTG013 = "", strTG016 = "", strTG017 = "", strTG018 = "", strTG900 = "";
	            string strMB001 = "", strMB002 = "", strMB003 = "", strMB004 = "", strMB005 = "",strMB006 = "",
	                strMB007 = "", strMB008 = "", strMB009 = "", strMB010 = "", strMB011 = "", strMB012 = "", 
                    strMB013 = "", strMB014 = "", strMB015 = "", strMB016 = "", strMB017 = "", strMB018 = "", 
                    strMB019 = "", strMB020 = "", strMB022 = "", strMB023 = "", strMB025 = "";
                string createDate = DateTimeUtility.getSystemTime2(null).Replace("/","").Substring(0,8);
                string flag = "1";
                string userGUID = set.getAvailableDataObject(i).getData("UserGUID");
                string startDateTime = set.getAvailableDataObject(i).getData("StartDateTime");
                string endDateTime = set.getAvailableDataObject(i).getData("EndDateTime");
                string startDate = startDateTime.Replace("/", "").Substring(0, 8); 
                string endDate = endDateTime.Replace("/", "").Substring(0, 8);
                string startTime = startDateTime.Substring(11, 6).Replace(":", "");
                string endTime = endDateTime.Substring(11, 6).Replace(":", "");
                string hours = set.getAvailableDataObject(i).getData("Hours");
                string reason = set.getAvailableDataObject(i).getData("Reason");
                string remark = set.getAvailableDataObject(i).getData("Remark");

                values = base.getUserInfo(engine, userGUID);
                string userId = values[0];
                string sql = "select MV027 from CMSMV where MV001= '" + userId + "'";
                string classCode = (string)erpEngine.executeScalar(sql); //班別
                //sw.WriteLine(sql);
                //找班別
                sql = "select * from AMSMB where MB001= '" + userId + "' and MB002= '" 
                    + startDate.Substring(0, 4) + startDate.Substring(4, 2) + "'";
                //sw.WriteLine(sql);
                DataSet ds = erpEngine.getDataSet(sql, "CLASS");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    string column = "MB0" + String.Format("{0:00}", Convert.ToInt16(startDate.Substring(6, 2)) + 2);
                    //sw.WriteLine(column);
                    string classCodeTemp = ds.Tables[0].Rows[0][column].ToString();
                    if (!Convert.ToString(classCodeTemp).Equals(""))
                    {
                        classCode = classCodeTemp;
                    }
                }
                //sw.WriteLine("classCode=" + classCode);
                //加班時數計算,依班別資料檔計算加班時數
                sql = "select MK025,MK026 from PALMK where MK001= '" + classCode + "'"; //找班別資料檔
                //sw.WriteLine(sql);
                ds = erpEngine.getDataSet(sql, "CLASS");
                string strMK025 = "2";
                string strMK026 = "4";
                if (ds.Tables[0].Rows.Count > 0) {
                    strMK025 = ds.Tables[0].Rows[0]["MK025"].ToString();
                    strMK026 = ds.Tables[0].Rows[0]["MK026"].ToString();
                }

                string hourFormat = "0000";
                if (Convert.ToDouble(hours) - (int)Convert.ToDouble(hours) == 0)
                {
                    hourFormat = String.Format("{0:00}", (int)Convert.ToDouble(hours)) + "00";
                }
                else
                {
                    hourFormat = String.Format("{0:00}", (int)Convert.ToDouble(hours)) + "30";
                }

                //找員工加班單檔
                sql = "select convert(int,isnull(max(TG010),0))+1 from PALTG where TG001= '" + userId + "' and TG002= '" + startDate + "'";
                //sw.WriteLine(sql);
                ds = erpEngine.getDataSet(sql, "TEMP");
                string serialNo = "0001";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string value = ds.Tables[0].Rows[0][0].ToString();
                    serialNo = String.Format("{0:0000}", Convert.ToInt32(value));
                }

                strTG001 = userId; //員工代號
                strTG002 = startDate; //加班日期
                strTG003 = classCode.Trim(); //班別
                strTG004 = startTime; //加班起始時分
                strTG005 = endTime; //加班截止時分  
                strTG006 = hourFormat; //加班時數格式化
                strTG007 = hours; //加班時數
                strTG008 = reason; //加班原因
                strTG009 = "N"; //確認碼
                strTG010 = serialNo; //序號
                
                //加班時數 1類
                if (Convert.ToDouble(strTG007) <=  Convert.ToDouble(strMK025))
                {
    	            strTG011 = strTG007;  //平日加班時數-1類
    	            strTG012 = "0"; //平日加班時數-2類
    	            strTG013 = "0"; //平日加班時數-3類
                }

                //加班時數 2類
                if (Convert.ToDouble(strTG007) > Convert.ToDouble(strMK025)
                        && Convert.ToDouble(strTG007) <= Convert.ToDouble(strMK026))
                {
                    strTG011 = strMK025;
                    strTG012 = (Convert.ToDouble(strTG007) - Convert.ToDouble(strMK025)) + "";
                    strTG013 = "0";
                }

                //加班時數 3類
	            if (Convert.ToDouble(strTG007) > Convert.ToDouble(strMK026) ) 
                {
                    strTG011 = strMK025;
    	            strTG012 = (Convert.ToDouble(strMK026) - Convert.ToDouble(strMK025))+ "";
    	            strTG013 = (Convert.ToDouble(strTG007) - Convert.ToDouble(strMK026)) + "";
                }

                strTG016 = "N"; //轉補休
                strTG017 = "0"; //轉補休時數
                strTG018 = "N"; //不執行電子簽核
                strTG900 = "N"; //不納入工時計算

                //加班單附加檔
                strMB001 = userId; //員工代號
                strMB002 = startDate; //加班日期
                strMB003 = strTG010; //序號
                strMB004 = "0000"; //加班啟始時分
                strMB005 = "0000"; //加班截止時分
                strMB006 = "0";
                strMB007 = "0000"; //new
                strMB008 = "0000"; //new
                strMB009 = "0";
                strMB010 = "0";
                strMB011 = "0";
                strMB012 = "0";
                strMB013 = "0";
                strMB014 = sheetNo + ";" + remark; //備註
                strMB015 = strTG003;
                strMB016 = "N";
                strMB017 = "0"; //new
                strMB018 = "0"; //new    
                strMB019 = "N";
                strMB020 = "N";
                strMB022 = "N";
                strMB023 = "N";
                strMB025 = OrganizationUnitGUID.ValueText; //歸屬部門

                sql = "insert into PALTG (COMPANY,CREATOR,USR_GROUP,CREATE_DATE,FLAG,"
                    + "TG001,TG002,TG003,TG004,TG005,TG006,TG007,TG008,TG009,TG010,TG011,TG012,TG013,TG016,TG017,"
                    + "TG018,TG900) values ("
                    + "'" + companyId + "',"
                    + "'" + creatorId + "',"
                    + "'" + companyId + "',"
                    + "'" + createDate + "',"
                    + "'" + flag + "',"
                    + "'" + strTG001 + "'," + "'" + strTG002 + "'," + "'" + strTG003 + "'," + "'" + strTG004 + "',"
                    + "'" + strTG005 + "'," + "'" + strTG006 + "'," + "'" + strTG007 + "'," + "'" + strTG008 + "',"
                    + "'" + strTG009 + "'," + "'" + strTG010 + "'," + "'" + strTG011 + "'," + "'" + strTG012 + "',"
                    + "'" + strTG013 + "'," + "'" + strTG016 + "'," + "'" + strTG017 + "'," + "'" + strTG018 + "',"
                    + "'" + strTG900 + "')";
                //sw.WriteLine(sql);
                erpEngine.executeSQL(sql);

                sql = "insert into PALSMB (COMPANY,CREATOR,USR_GROUP,CREATE_DATE,FLAG," 
                    + "MB001,MB002,MB003,MB004,MB005,MB006,MB007,MB008,MB009,MB010,MB011,MB012,MB013,MB014,MB015,"
                    + "MB016,MB017,MB018,MB019,MB020,MB022,MB023,MB025) values ("
                    + "'" + companyId + "'," 
	                + "'" + creatorId + "'," 
	                + "'" + companyId + "'," 
	                + "'" + createDate + "'," 
	                + "'" + flag + "'," 
	                + "'" + strMB001 + "'," + "'" + strMB002 + "'," + "'" + strMB003 + "'," + "'" + strMB004 + "'," 
	                + "'" + strMB005 + "'," + "'" + strMB006 + "'," + "'" + strMB007 + "'," + "'" + strMB008 + "'," 
	                + "'" + strMB009 + "'," + "'" + strMB010 + "'," + "'" + strMB011 + "'," + "'" + strMB012 + "'," 
	                + "'" + strMB013 + "'," + "'" + strMB014 + "'," + "'" + strMB015 + "'," + "'" + strMB016 + "'," 
	                + "'" + strMB017 + "'," + "'" + strMB018 + "'," + "'" + strMB019 + "'," + "'" + strMB020 + "'," 
	                + "'" + strMB022 + "'," + "'" + strMB023 + "'," + "'" + strMB025 + "')";
                //sw.WriteLine(sql);
                erpEngine.executeSQL(sql);
                //throw new Exception("");
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(result + " " + e.Message);
        }
        finally
        {
            if (erpEngine != null)
            {
                erpEngine.close();
            }
            //if (sw != null)
            //{
            //    sw.Close();
            //}
        }
        return result;
    }
}