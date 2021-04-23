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
using com.dsc.kernal.global;
using com.dsc.kernal.security;
using com.dsc.kernal.utility;
using WebServerProject.system.login;
using com.dsc.kernal.logon;
using com.dsc.kernal.agent;
using com.dsc.kernal.utility;
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM006_Input : BaseWebUI.GeneralWebPage
{
    string[] encodeFields = new string[] { "SelfScore_W", "FirstScore_W", "SecondScore_W", "FinalScore" };

    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPPM006M";
        ApplicationID = "SYSTEM";
        ModuleID = "SPPM";

        base.OnInit(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {		
		if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {                
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                string[,] ids = null;
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                //設定考核計畫
                ids = SmpPmMaintainUtil.getAssessmentName(engine);               
                AssessmentPlanGUID.setListItem(ids);
                //設定等級
                ids = SmpPmMaintainUtil.getAchievementLevel(engine);
                AchievementLevel.setListItem(ids);                 
                //設定狀態
                ids = SmpPmMaintainUtil.getAssessmentStatusIds(engine);
                AchievementStatus.setListItem(ids);
                //AchievementStatus.ValueText = SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN;                
                string userGUID = (string)Session["UserId"];
                //string whereClause = " 1=2 ";
                string[] values = SmpPmMaintainUtil.getLastAchievementAssessmentPlan(engine, userGUID);
                string assessmentPlanGUID = values[0];
                AssessmentPlanGUID.ValueText = assessmentPlanGUID;
                //queryData(whereClause);
                if (!string.IsNullOrEmpty(assessmentPlanGUID))
                {
                    GbSearch_Click(sender, e);
                }
                AchievementLevel.Visible = true;
                //部門
                CheckDept.clientEngineType = engineType;
                CheckDept.connectDBString = connectString;
            }
        }
    }
	
	/// <summary>
    /// 部門開窗前先取得公司名
    /// </summary>
    protected void CheckDept_BeforeClickButton()
    {
        string connectString = (string)Session["connectString"];
      
        string companyCode = "";
        string companyName = "";
        string[,] ids = null;      
       
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        string sql = "select CompanyCode from SmpPmAssessmentPlan Where GUID in (" + SmpPmMaintainUtil.GetfilterNameplanGUID(AssessmentPlanGUID.ValueText) + ")";
        DataSet ds = engine.getDataSet(sql, "TEMP");       
        if (ds.Tables[0].Rows.Count > 0)
        {
          companyCode= ds.Tables[0].Rows[0][0].ToString();          

          //依companyCode取得公司名稱
          ids = SmpPmMaintainUtil.getCompanyIds(engine);
          for (int i = 0; i < ids.Length; i++)
          {
              if (ids[i, 0].Equals(companyCode))
                  companyName = ids[i, 1];
          }         
          CheckDept.whereClause = " (organizationName ='" + companyName + "' )";
        }
                
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(AssessmentPlanGUID.ValueText))
        {      

            //query ,不查Cancel            
            string userGUID = (string)Session["UserId"];

            ///判斷是否可以提交
            //if (SmpPmMaintainUtil.GetSubmitStatus(AssessmentPlanGUID.ValueText, userGUID))
            //{
            //    GbSubmit.Enabled = true;
            //}
            //else
            //{
            //    GbSubmit.Enabled = false;
            //}
            GbSubmit.Enabled = true;

            string whereClause = " AssessUserGUID='" + userGUID + "' and AssessmentPlanGUID in(" + SmpPmMaintainUtil.GetfilterNameplanGUID(AssessmentPlanGUID.ValueText) + ") "+
                                 " and Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CANCEL + "' ";
           
            //grid detail data
            if (!string.IsNullOrEmpty(AchievementStatus.ValueText) && !AchievementStatus.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_STATUS_CANCEL))
            {
                whereClause +=  " and Status='" + AchievementStatus.ValueText + "'";
            }   
            
            if (!string.IsNullOrEmpty(AchievementLevel.ValueText))
            {
                if (AchievementLevel.ValueText.Equals("NA"))
                    whereClause += " and AchievementLevel='' ";                 
                else
                    whereClause += " and AchievementLevel='" + AchievementLevel.ValueText + "' ";
            }

            if (!string.IsNullOrEmpty(CheckDept.ValueText))
            {
                whereClause += " and deptId='" + CheckDept.ValueText + "'";
            }
            queryData(whereClause);            
        }
        else
        {
            MessageBox("請先選擇考核名稱!");
        }
    }
	
	
	/// <summary>
    /// 查詢資料
    /// </summary>
    /// <param name="whereClause"></param>
    protected void queryData(string whereClause)
    {
	    AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM006.SmpPmUserAchievementAgent");
            agent.engine = engine;
            agent.query(whereClause);
            DataObjectSet set = agent.defaultData;

            AchievementList.InputForm = "Input.aspx";
            AchievementList.DialogHeight = 600;
            AchievementList.DialogWidth = 840;
            AchievementList.HiddenField = new string[] { "GUID", "deptId", "UserGUID", "AssessmentPlanGUID", "AssessmentName", "AssessUserGUID", "SelfComments", "SelfScore_W", "FirstComments", "SecondComments", "Status", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            SmpPmMaintainUtil.getDecodeValue(set, encodeFields);
            AchievementList.dataSource = set;
            AchievementList.updateTable();
        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
            writeLog(ex);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }
    }
	
	       
    /// <summary>
    /// 成績分佈統計
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbAnalysis_Click(object sender, EventArgs e)
    {
        DataObjectSet dos = AchievementList.dataSource;          
        if (!string.IsNullOrEmpty(AssessmentPlanGUID.ValueText) && dos.getAvailableDataObjectCount() >0 )
        {
            //Session["SPPM006_UserGUID"] = (string)Session["UserGUID"];
            Session["SPPM006_AssessmentPlanGUID"] = AssessmentPlanGUID.ValueText;
            string url = "AnalysisNew.aspx";
            base.showOpenWindow(url, "查詢成績分佈統計", "", "500", "500", "500", "", "1", "1", "", "", "", "", "600", "", true);
        }
        else
        {
            MessageBox("請先選擇考核名稱，並執行查詢!");
        }
    }
	
	 
    /// <summary>
    /// 提交成績
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSubmit_Click(object sender, EventArgs e)
    {
        DataObjectSet dos = AchievementList.dataSource;
        if (!string.IsNullOrEmpty(AssessmentPlanGUID.ValueText) && dos.getAvailableDataObjectCount() > 0)        
        {
            //check Status
            string userGUID = (string)Session["UserId"];       
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            string whereClause = " AssessmentPlanGUID in(" + SmpPmMaintainUtil.GetfilterNameplanGUID(AssessmentPlanGUID.ValueText) + ") and AssessUserGUID ='" + userGUID + "' ";
            string now = DateTimeUtility.getSystemTime2(null);            

            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            string sql = "select count(*) from SmpPmUserAchievement Where " + whereClause + " and ( AchievementLevel = '' or " +
                         "(Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN + "' and Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CANCEL + "')) ";   
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                int count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                if (count > 0)
                {
                    MessageBox("無法提交成績!請確認:\n1.所有員工已輸入總詰分數\n2.不可重覆提交成績");                 
                }
                else
                {                   
                    sql = "update SmpPmUserAchievement set Status ='" + SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE + "', " +
                    "D_MODIFYUSER = '" + (string)Session["UserGUID"] + "', D_MODIFYTIME='" + now + "'  where " +
                    whereClause + "and Status ='" + SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN + "' ";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                    else
                    {
                        MessageBox("已成功提交!");
                        //重新查詢!
                        AchievementStatus.ValueText ="";
                        queryData(whereClause);    
                    }                    
                }
            }

        }
        else
        {
            MessageBox("請先選擇考核名稱，並執行查詢!");
        }    
    }
    
}
