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

public partial class SmpProgram_Maintain_SPPM004_Input : BaseWebUI.GeneralWebPage
{
	private string connectString = "";
	private string engineType = "";
	
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPPM004M";
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
                string objectGUID = Request.QueryString["ObjectGUID"];
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                string[,] ids = null;
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                //設定考核計畫
                ids = SmpPmMaintainUtil.getAssessment(engine);
                AssessmentPlanGUID.setListItem(ids);
                
                //設定狀態
                ids = SmpPmMaintainUtil.getAssessmentStatusIds(engine);
                Status.setListItem(ids);
                Status.ValueText = SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN;
                
                string userGUID = (string)Session["UserGUID"];
                string whereClause = "AssessUserGUID='" + userGUID + "'";
                if (funcId.Equals("0"))
                {
                    whereClause += " and Stage='0'";
                }
                else
                {
                    whereClause += " and Stage <>'0'";
                }

                //whereClause += " and Status='" + SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN + "'";
                //queryData(whereClause);
				//20171213 SMP HR Emily要求調整抓取最後一次評核計劃帶入晝面中
				string[] values = SmpPmMaintainUtil.getLastAchievementAssessmentPlan(engine, userGUID);
                string assessmentPlanGUID = values[0];
                AssessmentPlanGUID.ValueText = assessmentPlanGUID;
                
                if (!string.IsNullOrEmpty(assessmentPlanGUID))
                {
					queryData(whereClause);
                    GbSearch_Click(sender, e);
                }
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
        string whereClause = "AssessUserGUID='" + userGUID + "'";

        if (!string.IsNullOrEmpty(AssessmentPlanGUID.ValueText))
        {
            whereClause += " and AssessmentPlanGUID='" + AssessmentPlanGUID.ValueText + "'";
        }
        if (!string.IsNullOrEmpty(Status.ValueText))
        {
            whereClause += " and Status='" + Status.ValueText + "'";
        }
        if (funcId.Equals("0"))
        {
            whereClause += " and Stage='" + SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION + "'";
        }
        else
        {
            whereClause += " and Stage <>'" + SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION + "'";
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
            agent.loadSchema("WebServerProject.maintain.SPPM004.SmpPmAssessmentUserScoreAgent");
            agent.engine = engine;
            agent.query(whereClause);
            DataObjectSet set = agent.defaultData;

            AssessmentStageList.InputForm = "Input.aspx";
            AssessmentStageList.DialogHeight = 600;
            AssessmentStageList.DialogWidth = 1000;
            AssessmentStageList.HiddenField = new string[] { "GUID", "UserGUID", "AssessmentPlanGUID", "AssessmentDays", "AssessmentManagerGUID", "EvaluationGUID", "EvaluationName", "SelfComments", "FirstComments", "SecondComments", "FinalScore", "FinalComments", "AchievementLevel", "AssessUserGUID", "UserAssessmentStageGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            AssessmentStageList.dataSource = set;
            AssessmentStageList.updateTable();

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
