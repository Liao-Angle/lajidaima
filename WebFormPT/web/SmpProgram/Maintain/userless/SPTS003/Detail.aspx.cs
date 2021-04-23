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

public partial class SmpProgram_maintain_SPTS003_Detail : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
		string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string[,] ids = null;
		
		DeptGUID.clientEngineType = (string)Session["engineType"];
        DeptGUID.connectDBString = (string)Session["connectString"];

        SubjectDetailGUID.clientEngineType = (string)Session["engineType"];
        SubjectDetailGUID.connectDBString = (string)Session["connectString"];
		
		//預計開課月份       
        ids = new string[,]{ 
                        {"01","01"},
                        {"02","02"},
						{"03","03"},
						{"04","04"},						
						{"05","05"},
						{"06","06"},
						{"07","07"},
						{"08","08"},
						{"09","09"},
						{"10","10"},
						{"11","11"},
						{"12","12"}						
                    };
        SchMonth.setListItem(ids);
		
		//內外訓
        ids = new string[,] {
            {"",""},
            {"I",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "I", "內訓")},
            {"O",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "O", "外訓")}
        };
        InOut.setListItem(ids);
        //InOut.ReadOnly = true;

        //課程類別
        ids = new string[,]{ 
            {"",""},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "1", "新人訓練")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "2", "專業職能")},
            {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "3", "管理職能")},
            {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "4", "品質管理")},
            {"5",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "5", "安全衛生")} 
        };
        SubjectType.setListItem(ids);
        SubjectType.ReadOnly = true;
		
		//TTQS
        ids = new string[,]{  
                {"",""},
                {"Y","Y"},
                {"N","N"}       
            };
        TTQS.setListItem(ids);

        //評估等級
        ids = new string[,]{ 
            {"",""},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "1", "Level 1 反應評估 - 教育訓練滿意度調整")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "2", "Level 2 學習評估 - 考試、心得報告、取得證書或證照")},
            {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "3", "Level 3 行為評估 - 開課、課後行動計劃並執行、工作改善計劃提出及執行")}

        };
        EvaluationLevel.setListItem(ids);
		
		//課程來源
        ids = new string[,]{ 
            {"",""},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "1", "年度計劃")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts003_input_aspx.language.ini", "message", "2", "新增申請")}
        };
        SchSource.setListItem(ids);
		
		//預算費用
        Fees.ValueText = "0";
		
		//取消課程
		ids = new string[,]{ 
                    {"Y","Y"},
                    {"N","N"}       
              };
		Cancel.setListItem(ids);
		Cancel.ValueText = "N";

        //開班授課
        ids = new string[,]{ 
                    {"Y","Y"},
                    {"N","N"}       
              };
        Closed.setListItem(ids);
        Closed.ValueText = "N";		
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
		bool isNew = (bool)getSession("isNew");

        SchNo.ValueText = objects.getData("SchNo");
        SchMonth.ValueText = objects.getData("SchMonth");
        Quarter.ValueText = objects.getData("Quarter");
        DeptGUID.GuidValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
        SubjectDetailGUID.GuidValueText = objects.getData("SubjectDetailGUID");
        SubjectDetailGUID.doGUIDValidate();
        InOut.ValueText = objects.getData("InOut");
        SubjectType.ValueText = objects.getData("SubjectType");
        TrainingHours.ValueText = objects.getData("TrainingHours");
        NumberOfPeople.ValueText = objects.getData("NumberOfPeople");
        TrainingObject.ValueText = objects.getData("TrainingObject");
        Fees.ValueText = objects.getData("Fees");
        EvaluationLevel.ValueText = objects.getData("EvaluationLevel");
        TTQS.ValueText = objects.getData("TTQS");
        SchSource.ValueText = objects.getData("SchSource");
        Cancel.ValueText = objects.getData("Cancel");
        Closed.ValueText = objects.getData("Closed");

        SchYear.ValueText = objects.getData("SchYear");
        CompanyCode.ValueText = objects.getData("CompanyCode");

        if (isNew)
        {
			//SchSource.ValueText = "2"; //新增時 課程來源只可選取 [新增申請]
			//SchSource.ReadOnly = true;
			Cancel.ValueText = "N"; //新增時, 預設為N
            Closed.ValueText = "N"; //新增時, 預設為N
            TTQS.ValueText = "N";
			SchNo.ReadOnly = true;			
			Fees.ValueText = "0"; //預算費用	
            Quarter.ValueText = "Q1";
            SchSource.ValueText = "2";

            string strISchYear = (string)Session["SPTS003_ISchYear"];
            string strICompany = (string)Session["SPTS003_ICompanyCode"];
            SchYear.ValueText = strISchYear;
            CompanyCode.ValueText = strICompany;
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
		IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

		string strSchYear = SchYear.ValueText;
        string strCompanyCode = CompanyCode.ValueText;             
		string seqNo = "";
		
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SchFormGUID", "temp");
			//string seqNo = getCustomCourseNo(engine, "SMPTSCourseSeqNo");
            seqNo = getCustomSchNo(engine, "SMPTSSchNo", strCompanyCode + strSchYear);
            //Page.Response.Write("alert('seqNO:" + seqNo + "');");
            objects.setData("SchNo", seqNo);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }

        objects.setData("SchMonth", SchMonth.ValueText);
        objects.setData("Quarter", Quarter.ValueText);
        objects.setData("DeptGUID", DeptGUID.GuidValueText);
        objects.setData("SubjectDetailGUID", SubjectDetailGUID.GuidValueText);        
        objects.setData("InOut", InOut.ValueText);
        objects.setData("SubjectType", SubjectType.ValueText);
        objects.setData("TrainingHours", TrainingHours.ValueText);
        objects.setData("NumberOfPeople", NumberOfPeople.ValueText);
        objects.setData("TrainingObject", TrainingObject.ValueText);
        objects.setData("Fees", Fees.ValueText);
        objects.setData("EvaluationLevel", EvaluationLevel.ValueText);
        objects.setData("TTQS", TTQS.ValueText);
        objects.setData("SchSource", SchSource.ValueText);
        objects.setData("Cancel", Cancel.ValueText);
        objects.setData("Closed", Closed.ValueText);        
        objects.setData("DeptId", DeptGUID.ValueText);
        objects.setData("DeptName", DeptGUID.ReadOnlyValueText);
        objects.setData("SubjectNo", SubjectDetailGUID.ValueText);
        objects.setData("SubjectName", SubjectDetailGUID.ReadOnlyValueText);
		
		//檢查欄位資料
        string errMsg = checkFieldData(objects, engine);
        engine.close();
        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }

    }
	
	/// <summary>
    /// 取得編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getCustomSchNo(AbstractEngine engine, string code, string strYear)
    {
        string sheetNo = "";
        try
        {                    
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
			hs.Add("CompanyIDYear", strYear);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return sheetNo;
    }
	
	 public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
		        
        string guid = objects.getData("GUID");
        string companyCode = objects.getData("CompanyCode");
		        
		decimal hours = 0;
		string chkTrainingHours = objects.getData("TrainingHours");
        //訓練時數不可為零
        if (!chkTrainingHours.Equals(""))
        {
            hours = Convert.ToDecimal(chkTrainingHours);
        }
        if (hours <= 0)
        {
            errMsg += "訓練時數必需大於零!\n";
        }
		
		string chkNumberOfPeople = objects.getData("NumberOfPeople");
        //預訓人數不可為零
        if (!chkNumberOfPeople.Equals(""))
        {
            hours = Convert.ToDecimal(chkNumberOfPeople);
        }
        if (hours <= 0)
        {
            errMsg += "預訓人數必需大於零!\n";
        }
		
		string chkFees = objects.getData("Fees");
        //預算費用不可為零
        if (!chkFees.Equals(""))
        {
            hours = Convert.ToDecimal(chkFees);
        }
        if (hours < 0)
        {
            errMsg += "預算費用必需輸入! \n";
        }
		
        return errMsg;
    }


    /// <summary>
    /// 月份變更重算季
    /// </summary>
    /// <param name="value"></param>
    protected void SchMonth_SelectChanged(string value)
    {
        //重算季
        string strMonth = SchMonth.ValueText;

        if (strMonth.Equals("01") || strMonth.Equals("02") || strMonth.Equals("03"))
        {
            Quarter.ValueText = Convert.ToString("Q1");
        }
        else if (strMonth.Equals("04") || strMonth.Equals("05") || strMonth.Equals("06"))
        {
            Quarter.ValueText = Convert.ToString("Q2");
        }
        else if (strMonth.Equals("07") || strMonth.Equals("08") || strMonth.Equals("09"))
        {
            Quarter.ValueText = Convert.ToString("Q3");
        }
        else
        {
            Quarter.ValueText = Convert.ToString("Q4");
        }	
    }

    protected void SubjectDetailGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string strSubjectGUID = SubjectDetailGUID.GuidValueText;

        try
        {
            string sql = "select InOut, SubjectType, TrainingHours, DeptGUID from SmpTSSubjectDetail where GUID='" + strSubjectGUID + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");

            if (ds.Tables[0].Rows.Count > 0)
            {
                InOut.ValueText = ds.Tables[0].Rows[0][0].ToString();
                SubjectType.ValueText = ds.Tables[0].Rows[0][1].ToString();
                TrainingHours.ValueText = ds.Tables[0].Rows[0][2].ToString();
                DeptGUID.GuidValueText = ds.Tables[0].Rows[0][3].ToString();
                DeptGUID.doGUIDValidate();
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
    }

    protected void SubjectDetailGUID_BeforeClickButton()
    {
        string companyCode = CompanyCode.ValueText;
        if (!companyCode.Equals(""))
        {
            SubjectDetailGUID.whereClause = " (CompanyCode='" + companyCode + "') ";
        }
    }

    protected void DeptGUID_BeforeClickButton()
    {
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            string companyCode = CompanyCode.ValueText;
            string companyName = "";
            if (companyCode.Equals("SMP"))
            {
                companyName = "新普科技";
            }
            else if (companyCode.Equals("TP"))
            {
                companyName = "中普科技";
            }
            DeptGUID.whereClause = " ( organizationName='" + companyName + "' ) ";
        }

    }
    protected void Cancel_SelectChanged(string value)
    {
        //已有開課記錄的課程，則 [取消課程]不可為Y
        string strMessage = "";

        if (Closed.ValueText.Equals("Y")) 
        {
            if (value.Equals("Y"))
            {
                strMessage += "已有開課記錄的課程，則 [取消課程]不可為Y!\n";
                Cancel.ValueText = "N";
            }
        }
        
        if (!strMessage.Equals(""))
        {
            MessageBox(strMessage);
        }        
    }
}
