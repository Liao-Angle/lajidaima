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
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using WebServerProject.system.login;
using com.dsc.kernal.logon;
using com.dsc.kernal.agent;
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM007_Input : BaseWebUI.GeneralWebPage
{
    string[] encodeFields = new string[] { "SelfScore", "FirstScore", "SecondScore", "FinalScore" };
	
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPPM007M";
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
                string funcId = Request.QueryString["FuncId"];                
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                string[,] ids = null;
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                //設定考核計畫
                ids = SmpPmMaintainUtil.getAssessment(engine);
                AssessmentPlanGUID.setListItem(ids);
                
                //設定狀態
                ids = SmpPmMaintainUtil.getAchievementLevel(engine);
                AchievementLevel.setListItem(ids);

                //部門
                CheckDept.clientEngineType = engineType;
                CheckDept.connectDBString = connectString;

                //是否顯示部門
                if (funcId.Equals("0")) //個人
                {
                    lblCheckDept.Visible = false;
                    CheckDept.Display = false;
                }
                else
                {
                    lblCheckDept.Visible = true;
                    CheckDept.Display = true;
                }

                string userGUID = (string)Session["UserGUID"];
                string whereClause = "1=2";                
                queryData(whereClause);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSearch_Click(object sender, EventArgs e)
    {
        string funcId = Request.QueryString["FuncId"];
        string userGUID = (string)Session["UserGUID"];
        string whereClause = "";

        if (funcId.Equals("0")) //個人，只可查有自核且狀態為COMPLETE或CLOSE
        {
            whereClause += " (UserGUID='" + userGUID + "' and (Status = '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE + "' OR  " +
                "Status ='" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CLOSE + "') and SelfEvaluation ='Y' ) ";
        }
        else
        {
            whereClause += " Status <>'" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CANCEL + "' ";
        }

        if (!string.IsNullOrEmpty(AssessmentPlanGUID.ValueText))
        {
            whereClause += " and AssessmentPlanGUID='" + AssessmentPlanGUID.ValueText + "'";
        }

        if (!string.IsNullOrEmpty(AchievementLevel.ValueText))
        {
            whereClause += " and AchievementLevel='" + AchievementLevel.ValueText + "' ";
        }

        if (!string.IsNullOrEmpty(CheckDept.ValueText))
        {
            whereClause += " and deptId='" + CheckDept.ValueText + "'";
        }            
        
        queryData(whereClause);
    }

    /// <summary>
    /// 
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
            agent.loadSchema("WebServerProject.maintain.SPPM007.SmpPmAssessmentUserScoreAgent");
            agent.engine = engine;
            agent.query(whereClause);
            DataObjectSet set = agent.defaultData;
           
            AchievementList.InputForm = "Detail.aspx";
            AchievementList.DialogHeight = 600;
            AchievementList.DialogWidth = 1000;
            AchievementList.HiddenField = new string[] { "GUID", "UserGUID", "AssessmentPlanGUID", "AssessmentManagerGUID", "EvaluationGUID", "EvaluationName", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            AchievementList.dataSource = set;
            SmpPmMaintainUtil.getDecodeValue(set, encodeFields);
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

     
}
