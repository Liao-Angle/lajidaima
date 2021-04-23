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

public partial class SmpProgram_maintain_SPTS002_Detail : BaseWebUI.DataListInlineForm
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
		
		//TTQS
        ids = new string[,]{  
                {"",""},
                {"Y","Y"},
                {"N","N"}       
            };
		
		//預計開課月份       
        ids = new string[,]{ 
                        {"01","1"},
                        {"02","2"},
						{"03","3"},
						{"04","4"},						
						{"05","5"},
						{"06","6"},
						{"07","7"},
						{"08","8"},
						{"09","9"},
						{"10","10"},
						{"11","11"},
						{"12","12"}						
                    };
        EstimateMonth.setListItem(ids);
		
		//內外訓
        ids = new string[,] {
            {"",""},
            {"I",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "I", "內訓")},
            {"O",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "O", "外訓")}
        };
        InOut.setListItem(ids);
        //InOut.ReadOnly = true;

        //課程類別
        ids = new string[,]{ 
            {"",""},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "1", "新人訓練")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "2", "專業職能")},
            {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "3", "管理職能")},
            {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "4", "品質管理")},
            {"5",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "5", "環安衛")} 
        };
        CourseType.setListItem(ids);
        //CourseType.ReadOnly = true;
		
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
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "1", "Level 1 反應評估")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "2", "Level 2  學習評估")},
            {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "3", "Level 3 行為評估")}
        };
        EvaluationLevel.setListItem(ids);
		
		//預算費用
        Fees.ValueText = "0";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
		bool isNew = (bool)getSession("isNew");
		
        EstimateMonth.ValueText = objects.getData("EstimateMonth");
				
		DeptGUID.GuidValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
		
        CourseNo.ValueText = objects.getData("CourseNo");
		CourseName.ValueText = objects.getData("CourseName");
		TrainingHours.ValueText = objects.getData("TrainingHours");
		NumberOfPeople.ValueText = objects.getData("NumberOfPeople");
		TrainingObject.ValueText = objects.getData("TrainingObject");
		CourseType.ValueText = objects.getData("CourseType");
		InOut.ValueText = objects.getData("InOut");
		Fees.ValueText = objects.getData("Fees");
		EvaluationLevel.ValueText = objects.getData("EvaluationLevel");
		TTQS.ValueText = objects.getData("TTQS");
		Quarter.ValueText = objects.getData("Quarter");
		
		string sessionPlanYear = (string)Session["SPTS002_IPlanYear"] ;	
		PlanYear.ValueText = sessionPlanYear;
				
		CourseNo.ReadOnly = true;
		Quarter.ReadOnly = true;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string sql = null;
		
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("PlanFormGUID", "temp");
			string seqNo = getCustomCourseNo(engine, "SMPTSCourseSeqNo", PlanYear.ValueText);
            //Page.Response.Write("alert('seqNO:" + seqNo + "');");
            objects.setData("CourseNo", seqNo);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
		
        objects.setData("EstimateMonth", EstimateMonth.ValueText);
        objects.setData("CourseName", CourseName.ValueText);
        objects.setData("DeptGUID", DeptGUID.GuidValueText);
        objects.setData("id", DeptGUID.ValueText);
        objects.setData("organizationUnitName", DeptGUID.ReadOnlyValueText);
		//Convert.ToString(Math.Round(Convert.ToDouble(TrainingHours.ValueText),1));
		objects.setData("TrainingHours", Convert.ToString(Math.Round(Convert.ToDouble(TrainingHours.ValueText),1))); //可輸入至小數一位
        //objects.setData("TrainingHours", TrainingHours.ValueText);
        objects.setData("NumberOfPeople", NumberOfPeople.ValueText);
        objects.setData("TrainingObject", TrainingObject.ValueText);
        objects.setData("InOut", InOut.ValueText);
        objects.setData("CourseType", CourseType.ValueText);
        objects.setData("Fees", Fees.ValueText);
        objects.setData("EvaluationLevel", EvaluationLevel.ValueText);
        objects.setData("TTQS", TTQS.ValueText);
		
		string strMonth = EstimateMonth.ValueText;
		
		if (strMonth.Equals("01") || strMonth.Equals("02") || strMonth.Equals("03"))
		{
			objects.setData("Quarter", "Q1");
		}
		else if (strMonth.Equals("04") || strMonth.Equals("05") || strMonth.Equals("06"))
		{
			objects.setData("Quarter", "Q2");
		}
		else if (strMonth.Equals("07") || strMonth.Equals("08") || strMonth.Equals("09"))
		{
			objects.setData("Quarter", "Q3");
		}
		else
		{
			objects.setData("Quarter", "Q4");
		}
				
		
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
    protected string getCustomCourseNo(AbstractEngine engine, string code, string strYear)
    {
        string sheetNo = "";
        try
        {                    
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("PlanYear", strYear);
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
		        
        string sql = null;
		int cnt = 0;
        string guid = objects.getData("GUID");
        string companyCode = objects.getData("CompanyCode");
		
		
        //年度+課程名稱不可重覆
		string chkCourseYear = objects.getData("PlanYear");
		string chkCourseName = objects.getData("CourseName");
        //sql = " select count(*) from SmpTSSchDetail d, SmpTSSchForm f where f.CourseYear='" + chkCourseYear + "' and d.CourseName='" + chkCourseName + "'  and f.GUID=d.SchFormGUID ";
		sql = "select count(*) from ( select sf.CourseYear+sd.CourseName checkdata, CompanyCode  from SmpTSSchForm sf, SmpTSSchDetail sd where sf.GUID=sd.SchFormGUID ";
		sql += " union all select pf.PlanYear+pd.CourseName checkdata, CompanyCode from SmpTSPlanForm pf, SmpTSPlanDetail pd where pf.GUID=pd.PlanFormGUID ) a ";
		sql += " where a.checkdata ='" + chkCourseYear+chkCourseName + "'  and CompanyCode='" + companyCode + "'  ";
        cnt = (int)engine.executeScalar(sql);
        if (cnt > 0)
        {
            errMsg += "年度+課程名稱不可重覆!\n";
        }
				
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
		
		        
        return errMsg;
    }
	
	
    
}
