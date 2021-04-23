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

public partial class Program_SCQ_Form_EH010301_Form : SmpBasicFormPage
{
    //責任制
    private string Responsible = "Y";
    private double nianxiujia = 0;
    protected override void init()
    {
        ProcessPageID = "EH010302";
        AgentSchema = "WebServerProject.form.EH010302.EH010302Agent";
        ApplicationID = "SYSTEM";
        ModuleID = "EASYFLOW";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
        try
        {

            string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where EmpNo ='" + si.fillerID + "'";
            DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
            string SQLstr = "";
            if (dtbm.Rows[0][0].ToString() != "")
            {
                SQLstr = "(PartNo = '" + dtbm.Rows[0][0].ToString() + "' ";
                for (int i = 1; i < dtbm.Rows.Count; i++)
                {
                    SQLstr = SQLstr + " or PartNo = '" + dtbm.Rows[i][0].ToString() + "' ";
                } SQLstr = SQLstr + ")  and( EmpTypeName like '%責任制%' or  EmpTypeName like '%Local  Hired%' or  EmpTypeName like '%台派%')";
                EmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


            }
            else
            {
                EmpNo.whereClause = "1=2";
            }

            EmpNo.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "")
            {
                EmpNo.whereClause = "PartNo like '%" + si.fillerOrgID + "%'  and( EmpTypeName like '%責任制%' or  EmpTypeName like '%Local  Hired%' or  EmpTypeName like '%台派%')";
            }
            else
            {
                EmpNo.whereClause = "1=2";
            }
            EmpNo.DoEventWhenNoKeyIn = false;
        }
        

        //設置HR假別設定
            string[,] ids = new string[3, 2]{{"-1","請選擇假別"},{"A01","無薪事假"},{"C13","有薪事假"}};
            LeaveTypeID.setListItem(ids);
            LeaveTypeID.Visible = true;



        string[,] tidss = new string[44, 2];
        int st = 8;
        for (int j = 0; st < 30; )
        {
                tidss[j, 0] = st.ToString().PadLeft(2, '0') + ":00";
                tidss[j, 1] = st.ToString().PadLeft(2, '0') + ":00";

                tidss[j + 1, 0] = st.ToString().PadLeft(2, '0') + ":30";
                tidss[j + 1, 1] = st.ToString().PadLeft(2, '0') + ":30";
                j++;
                j++;
            st++;
        }



        BeginTime.setListItem(tidss);
        BeginTime.ValueText = sp.getParam("DayBegin");
        EndTime.setListItem(tidss);
        EndTime.ValueText = sp.getParam("DayEnd");

        if (base.isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.EH010301.EH010301B");
            dos.setTableName("EH010301B");
            dos.loadFileSchema();
            objects.setChild("EH010301B", dos);
            LeaveDateList.dataSource = dos;
            LeaveDateList.updateTable();
        }

        LeaveDateList.clientEngineType = engineType;
        LeaveDateList.connectDBString = connectString;
        LeaveDateList.HiddenField = new string[] { "GUID", "SheetNo" };

        if (isNew())
        { LeaveTypeID.ReadOnly = true;
            string[,] xgqhzg = new string[,]{ {"0","請選擇"},
                {"1","張華謙"},
                {"2","田軍祥"},	
                {"3","許文坤"},
            };
            sqzszg.setListItem(xgqhzg);
            ///
            if (si.fillerOrgID == "NQM010101")
            {
                showzg.Visible = true;
            }
        }

  string[,] BB = new string[3, 2]{{"0","白班"},{"1","夜班"},{"2","中班"}};
        bb.setListItem(BB);
        bbset.Visible = true;
		
		if (base.isNew())
        {
            PrintButton.Display = false;
        }else{
			PrintButton.Enabled = true;
		}

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
        
            EmpNo.ValueText = objects.getData("EmpNo");
			SheetNo.ValueText = objects.getData("SheetNo");
          //  EmpNo.doValidate();
            EmpNo.ReadOnlyValueText = objects.getData("EmpName");
            EmpNo.Enabled = false;
            if (EmpNo.ValueText != "")
            {
                Hashtable h = base.getHRUsers(engine, EmpNo.ValueText);
                TryUseDate.Text = DateTime.Parse(h["TryuseDate"].ToString()).ToString("yyyy/MM/dd");
                DtName.Text = h["DtName"].ToString();

                UpdateLeaveData(engine);
            }
            LeaveTypeID.ValueText = objects.getData("LeaveTypeID");
            LeaveTypeID.ReadOnlyText = objects.getData("LeaveTypeName");
            Reason.ValueText = objects.getData("Reason");
            LeaveDateList.dataSource = objects.getChild("EH010301B");
            LeaveDateList.dataSource.sort(new string[,] { { "LeaveDate", DataObjectConstants.ASC } });
            LeaveDateList.updateTable();

            bbset.Visible = false;
            EmpNo.ReadOnly = true;
            Reason.ReadOnly = true;
            EditPanel.Visible = false;
            LeaveDateList.IsShowCheckBox = false;
            LeaveDateList.ReadOnly = true;

            string actName = (string)getSession("ACTName");
            if (actName == "直屬主管")
            {
                LeaveTypeID.ReadOnly = false;
            }
            else
            {
                LeaveTypeID.ReadOnly = true;
            }



        //顯示單號
        base.showData(engine, objects);
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            //產生單號並儲存至資料物件
        if (base.isNew())
        {
            base.saveData(engine, objects);
            double sum = 0;

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);

            objects.setData("Reason", Reason.ValueText);
            objects.setData("Responsible", Responsible);
            objects.setData("ForwardHR", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            //將B表匯進來
            foreach (DataObject obj in LeaveDateList.dataSource.getAllDataObjects())
            {
                double data = 0;
                obj.setData("SheetNo", objects.getData("SheetNo"));
                double.TryParse(obj.getData("NormalTime"), out data);
                sum += data;
            }
            objects.setData("Hours", sum.ToString("0.0"));

            objects.setChild("EH010301B", LeaveDateList.dataSource);


        }
            string actName = (string)getSession("ACTName");
            if (actName == "直屬主管")
            {
                objects.setData("LeaveTypeID", LeaveTypeID.ValueText);
                objects.setData("LeaveTypeName", LeaveTypeID.ReadOnlyText);
            }
    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        if (isNecessary(EmpNo))
        {
            if (EmpNo.GuidValueText.Equals(""))
            {
                pushErrorMessage("必須選擇請假人");
                //pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("Program_SCQ_Form_EH010301_Form_aspx.language", "message", "QueryError1", "必須選擇請假人"));
                result = false;
            }
        }
        if (isNecessary(Reason))
        {
            if (Reason.ValueText.Equals(""))
            {
                pushErrorMessage("必須填寫請假事由");
                //pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("Program_SCQ_Form_EH010301_Form_aspx.language", "message", "QueryError4", "必須填寫請假事由"));
                result = false;
            }
        }


            string actName = (string)getSession("ACTName");
            if (actName == "部門主管")
            {
                if (Reason.ValueText.Equals("請選擇假別"))
                {
                    pushErrorMessage("必須選擇假別");
                    //pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("Program_SCQ_Form_EH010301_Form_aspx.language", "message", "QueryError4", "必須填寫請假事由"));
                    result = false;
                }
            }

        if (LeaveDateList.dataSource.getAvailableDataObjectCount() == 0)
        {
            pushErrorMessage("必須填寫請假日期");
            result = false;
        }
	
	     if (UpdateLeaveData(engine)>0)
            {
                pushErrorMessage("該員尚有年休假剩餘天數,請去普通請假單請假!");
                result = false;
            }

        double sum = 0;
        foreach (DataObject obj in LeaveDateList.dataSource.getAllDataObjects())
        {
            double data = 0;
            double.TryParse(obj.getData("NormalTime"), out data);
            sum += data;
        }
        if (sum == 0)
        {
            pushErrorMessage("請假時數不得為0");
            result = false;
        }

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0")
        {
            pushErrorMessage("MFG-1 部門 請選擇簽核主管");
            result = false;
        }
        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        return getSubmitInfo(engine, objects, si);
    }
    
    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];//填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];//表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
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

    public bool getpartname(string str)
    {
        if (str.Length < 7)
        { return false; }
        str = str.Substring(0, 7);
        if (str == "NQM0301" || str == "NQM0302" || str == "NQM0304" || str == "NQM0305" || str == "NQM0308" || str == "NQM0309" || str == "NQMMM03" || str == "NQM0101" || str == "NQM0501" || str == "NQM0502" || str == "NQM0503" || str == "NQM0504" || str == "NQM0505" || str == "NQM0506")
        {
            return true;
        }
        else
        { return false; }
    }

    /// <summary>
    /// 設定流程變數。目前主要是用來傳遞流程所需要的變數值。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            //填表人
            string creatorId = si.fillerID;

            //主管
			string managerId = "";
			//writeLog("creatorId : " + creatorId);
			string[] userInfo = base.getUserGUID(engine, creatorId);
			//writeLog("userInfo[1] : " + userInfo[1]);
            string[] managerInfo = base.getUserManagerInfo(engine, userInfo[0]);
			if (!managerInfo[1].Equals(""))
            {
                managerId = managerInfo[1];
            }
			//writeLog("managerInfo[1] : " + managerInfo[1]);
            
            //上級主管
            string mrgmrg = "";
            string managerTz = "Q1508126";
            //string[] values = base.getUserManagerInfoID(engine, creatorId);
			
			//writeLog("sqzszg.ValueText : " + sqzszg.ValueText);
			
            if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText != "0")
            {
                if (sqzszg.ValueText == "1")
                {
                    managerId = "Q1100135";
                }
                else if (sqzszg.ValueText == "2")
                {
                    managerId = "QG1200421";
                }
                else if (sqzszg.ValueText == "3")
                {
                    managerId = "Q1210122";
                }
            }
            else
            {

                //managerId = values[1];  //申請人的主管 工號

            }



            //mrgmrg = base.getUserManagerInfoID(engine, managerId)[1];//上一級主管

            

            string isqh = "", istz = "", isfz = "", issg = "";
            Hashtable htb = base.getHRUsers(engine, EmpNo.ValueText);


            if (htb["DutyNo"] == "28" || htb["DutyNo"] == "5" || htb["DutyNo"] == "2" || htb["DutyNo"] == "18")
            {
                isqh = "1";
            }
            string[] str = base.getUserforID(engine, EmpNo.ValueText);
            if (str[0].ToString().Trim() == "LocalHired" || str[0] == "台派")
            {
                istz = "1";
            }

            if ((getpartname(si.fillerOrgID)))
            {

                isfz = "1";
            }
            mrgmrg = base.getUserManagerInfoID(engine, managerId)[1];
            issg = "1";
			//writeLog("managerId : " + managerId);
			//writeLog("mrgmrg : " + mrgmrg);

            xml += "<SQHR002>";
            xml += "<CREATOR DataType=\"java.lang.String\">" + creatorId + "</CREATOR>";//申請人
            xml += "<usermrg DataType=\"java.lang.String\">" + managerId + "</usermrg>";//主管
            xml += "<mrgmrg DataType=\"java.lang.String\">" + mrgmrg + "</mrgmrg>";//上級主管
            xml += "<issg DataType=\"java.lang.String\">" + issg + "</issg>";//上級主管是否簽核
            xml += "<managerTz DataType=\"java.lang.String\">" + managerTz + "</managerTz>";//總經理
            xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";//是否簽核
            xml += "<istz DataType=\"java.lang.String\">" + istz + "</istz>";//是否通知
            xml += "<isfz DataType=\"java.lang.String\">" + isfz + "</isfz>";
            xml += "</SQHR002>";
			//writeLog("xml : " + xml);

        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
        }
        //表單代號
        param["SQHR002"] = xml;
        return "SQHR002";
    }


    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine">WebFormPT</param>
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
    /// <param name="engine">WebFormPT</param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
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
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        if (result == "Y")
        {
            //將ForwardHR改為R
            currentObject.setData("ForwardHR", "R");
            string id = currentObject.getData("SheetNo");
            if (!engine.updateData(currentObject))
            {
                throw new Exception("更新EH010301 ForwardHR資料錯誤. 單號: " + id);
            }
        }
    }

    /// <summary>
    /// 選擇請假人員
    /// </summary>
    /// <param name="values"></param>
    protected void EmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        if (values == null)
            return;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        
        Hashtable h = base.getHRUsers(engine, EmpNo.ValueText);
        TryUseDate.Text = DateTime.Parse(h["TryuseDate"].ToString()).ToString("yyyy/MM/dd");
        DtName.Text = h["DtName"].ToString();

       nianxiujia = UpdateLeaveData(engine);
        
	if(nianxiujia>0)
	{
		 MessageBox("該員工年休假尚有剩餘天數,請去普通請假單請假");
	}
    }

    /// <summary>
    /// 取得即時請假狀況
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    private double UpdateLeaveData(AbstractEngine engine)
    {
        try
        {
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string ndb = sp.getParam("HRDB");
            string ndt = sp.getParam("HRDBType");
            IOFactory factory = new IOFactory();
            AbstractEngine hrEngine = factory.getEngine(ndt, ndb);
            double used = 0;
            double remd = 0;
            string msg = "";
            string sql = @"SELECT [EmpNo],[UsedDays],[RemainDays]
                            FROM [SCQHRDB].[dbo].[AttHolidayYearDataOfDay] 
                            WHERE [EmpNo]='" + EmpNo.ValueText + "'";
            DataSet ds = hrEngine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                double.TryParse(ds.Tables[0].Rows[0]["UsedDays"].ToString(), out used);
                double.TryParse(ds.Tables[0].Rows[0]["RemainDays"].ToString(), out remd);                
            }
            if (used > 0 || remd > 0)
            {
                msg += string.Format("年休假已用{0:0.#}天 (剩餘{1:0.#}天)", used, remd);
            }

            sql = @"SELECT [EmpNo],[LeaveTypeName],SUM([NormalTime]) as [NormalTime]
                      FROM [SCQHRDB].[dbo].[AttLeaveData] a 
                      LEFT JOIN [SCQHRDB].[dbo].[AttLeaveTypeSet] b ON a.[LeaveType] = b.[LeaveTypeID] 
                      WHERE [YYMMDD] > '" + DateTime.Now.ToString("yyyy/01/01") + "' AND [EmpNo] = '" + EmpNo.ValueText + @"' AND [LeaveType]!='C01'
                      GROUP BY [EmpNo], [LeaveTypeName]";
            ds = hrEngine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    used = 0;
                    double.TryParse(dr["NormalTime"].ToString(), out used);
                    if (used > 0)
                    {
                        if (!msg.Equals(""))
                            msg += ", ";
                        msg += dr["LeaveTypeName"].ToString();
                        if((int)(used / 8) > 0)
                            msg += ((int)(used / 8)).ToString("#0") + "天";
                        if(used > (int)(used / 8) * 8)
                            msg += ((int)(used - (int)(used / 8) * 8)).ToString("#0.###") + "小時";
                    }
                }
            }
            YearStatus.Text = msg;
	    return remd;
        }
        catch (Exception ex)
        {
            throw new Exception("查詢HRUSERS錯誤, " + ex.Message.ToString());
        }
    }

    protected void AddData_Click(object sender, EventArgs e)
    {
      
        if (check1.Checked)
        {
            if (LeaveDate.ValueText == "")
            {
                MessageBox("日期必須填寫");
                return;
            }
            bool isExist = false;
            foreach (DataObject obj2 in LeaveDateList.dataSource.getAllDataObjects())
            {
                if (LeaveDate.ValueText == obj2.getData("LeaveDate"))
                {
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                double s = 0;
                string[] sdtm = BeginTime.ValueText.Split(':');
                string[] edtm = EndTime.ValueText.Split(':');


                // DateTime.TryParse(LeaveDate.ValueText + " " + BeginTime.ReadOnlyText, out dt1);
                // DateTime.TryParse(LeaveDate.ValueText + " " + EndTime.ReadOnlyText, out dt2);

                int i =  Convert.ToInt32(edtm[0]) - Convert.ToInt32(sdtm[0]);//時數

                int j = Convert.ToInt32(edtm[1]) - Convert.ToInt32(sdtm[1]);//分鐘數
                if (j < 0)
                {
                    i--;
                    j = 60 + j;
                }
                DateTime ts;
                DateTime.TryParse(i + ":" + j, out ts);
                
                if (i <= 0 && j <= 0)
                {
                    MessageBox("開始時間不得大於結束時間");
                    return;
                }

/*
                if ((int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) <= 12 &&
                    int.Parse(EndTime.ReadOnlyText.Substring(0, 2)) >= 13) || (int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) <= 24 &&
                    int.Parse(EndTime.ReadOnlyText.Substring(0, 2)) >= 25))
                {
                    s = (double)ts.Hour + (double)ts.Minute / 60.0 - 1.0;
                }
                else
                {
                    s = (double)ts.Hour + (double)ts.Minute / 60.0;
                }*/

                if (bb.ValueText == "0")
                {

                    if (int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) < 8)
                    {
                        MessageBox("白班請假,開始時間不得小於8點");
                        return;
                    }
                    if (int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) > 17)
                    {
                        MessageBox("白班請假,開始時間不得大於17點");
                        return;
                    }
                    if (int.Parse(EndTime.ReadOnlyText.Substring(0, 2)) > 17)
                    {
                        MessageBox("白班請假,結束時間不得大於17點");
                        return;
                    }

                    if (int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) <= 12 && int.Parse(EndTime.ReadOnlyText.Substring(0, 2)) >= 13)
                    {
                        s = (double)ts.Hour + (double)ts.Minute / 60.0 - 1.0;
                    }
                    else
                    {
                        s = (double)ts.Hour + (double)ts.Minute / 60.0;
                    }
                }
                else if (bb.ValueText == "1")
                {


                    if (int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) < 20)
                    {
                        MessageBox("夜班請假,開始時間不得小於20點");
                        return;
                    }
                    if (int.Parse(EndTime.ReadOnlyText.Substring(0, 2)) < 20)
                    {
                        MessageBox("夜班請假,結束時間不得大於5點");
                        return;
                    }

                    if ((int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) <= 24 && int.Parse(EndTime.ReadOnlyText.Substring(0, 2)) >= 25))
                    {
                        s = (double)ts.Hour + (double)ts.Minute / 60.0 - 1.0;
                    }
                    else
                    {
                        s = (double)ts.Hour + (double)ts.Minute / 60.0;
                    }
                }
                else if (bb.ValueText == "2")
                {

                    if (int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) < 15)
                    {
                        MessageBox("中班請假,開始時間不得小於15點");
                        return;
                    }
                    if (int.Parse(EndTime.ReadOnlyText.Substring(0, 2)) > 24)
                    {
                        MessageBox("中班請假,結束時間不得大於24點");
                        return;
                    }
                    if (int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) > 24)
                    {
                        MessageBox("中班請假,開始時間不得大於24點");
                        return;
                    }


                    if ((int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) <= 17.5 && int.Parse(EndTime.ReadOnlyText.Substring(0, 2)) >= 18))
                    {
                        s = (double)ts.Hour + (double)ts.Minute / 60.0 - 0.5;
                    }
                    else
                    {
                        s = (double)ts.Hour + (double)ts.Minute / 60.0;
                    }



                    if ((int.Parse(BeginTime.ReadOnlyText.Substring(0, 2)) <= 23.5 && int.Parse(EndTime.ReadOnlyText.Substring(0, 2)) >= 24))
                    {
                        s = (double)ts.Hour + (double)ts.Minute / 60.0 - 0.5;
                    }
                    else
                    {
                        s = (double)ts.Hour + (double)ts.Minute / 60.0;
                    }
                }






                //NormalTime.ValueText = s.ToString("0.0");

                DataObject obj = new DataObject();
                obj.loadFileSchema(LeaveDateList.dataSource.getChildClassString());
                obj.setData("GUID", IDProcessor.getID(""));
                obj.setData("SheetNo", base.PageUniqueID);
                obj.setData("LeaveDate", LeaveDate.ValueText);
                obj.setData("BeginTime", BeginTime.ReadOnlyText);
                obj.setData("EndTime", EndTime.ReadOnlyText);
                obj.setData("NormalTime", s.ToString("0.0"));
                LeaveDateList.dataSource.add(obj);
            }
        }
        else if (check2.Checked)
        {
            if (LeaveSDate.ValueText == "")
            {
                MessageBox("必須選擇起始日期");
                return;
            }
            if (LeaveEDate.ValueText == "")
            {
                MessageBox("必須選擇截止日期");
                return;
            }

            //string[] sdtm = LeaveSDate.ValueText.Split(':');
            //string[] edtm = LeaveEDate.ValueText.Split(':');


            //if (Convert.ToInt32(sdtm[0]) <= 12 && Convert.ToInt32(edtm[0]) >= 13)//--中午減去一個小時
            //{
            //    edtm[0] = (Convert.ToInt32(edtm) - 1).ToString();
            //}
            //if (Convert.ToInt32(sdtm[0]) <= 17 && Convert.ToInt32(edtm[0]) >= 13)//--中午減去一個小時
            //{

            //}







            DateTime dt1;
            DateTime dt2;

            DateTime.TryParse(LeaveSDate.ValueText, out dt1);
            DateTime.TryParse(LeaveEDate.ValueText, out dt2);

            if (dt1 > dt2)
            {
                MessageBox("截止日期必須大於起始日期");
                return;
            }


            string stime = "";
            string etime = "";
            if (bb.ValueText == "0")
            {
                stime = "08:00";
                etime = "17:00";
            }
            else if (bb.ValueText == "1")
            {
                stime = "20:00";
                etime = "29:00";
            }
            else if (bb.ValueText == "2")
            {
                stime = "15:00";
                etime = "24:00";
            }

            for (DateTime dt = dt1; dt <= dt2; dt = dt.AddDays(1))
            {
                bool isExist = false;
                foreach (DataObject tmp in LeaveDateList.dataSource.getAllDataObjects())
                {
                    if (tmp.getData("LeaveDate") == dt.ToString("yyyy/MM/dd"))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    DataObject obj = new DataObject();
                    obj.loadFileSchema(LeaveDateList.dataSource.getChildClassString());
                    obj.setData("GUID", IDProcessor.getID(""));
                    obj.setData("SheetNo", base.PageUniqueID);
                    obj.setData("LeaveDate", dt.ToString("yyyy/MM/dd"));
                    obj.setData("BeginTime", stime);
                    obj.setData("EndTime", etime);
                    obj.setData("NormalTime", "8.0");
                    LeaveDateList.dataSource.add(obj);
                }
            }
        }
        LeaveDateList.dataSource.sort(new string[,] { { "LeaveDate", DataObjectConstants.ASC } });
        LeaveDateList.updateTable();
    }









    protected void LeaveDateList_setClickData(string clickList)
    {
        DataObject[] objs = LeaveDateList.dataSource.getAllDataObjects();
        for (int i = 0; i < objs.Length; i++)
        {
            if(clickList.Substring(i, 1) == "Y")
            {
                LeaveDateList.dataSource.delete(objs[i]);
            }
        }
        LeaveDateList.dataSource.compact();
        LeaveDateList.updateTable();
    }
	

	//列印單據
	protected void PrintButton_OnClick(object sender, EventArgs e)
    {
		//MessageBox("SheetNo : " + SheetNo.ValueText);
		Session["EH010302_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
	    base.showOpenWindow(url, "列印請假單", "", "700", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }	

	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\EH010302.log", true, System.Text.Encoding.Default);
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
}
