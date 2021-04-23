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

public partial class SmpProgram_Maintain_SPPM011_Maintain : BaseWebUI.GeneralWebPage
{
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
                sddlAssessmentPlanGUID.setListItem(ids);

                string whereClause = "";
                if (!sddlAssessmentPlanGUID.ValueText.Equals(""))
                {
                    whereClause = "AssessmentName='" + sddlAssessmentPlanGUID.ValueText + "' and Status in('" + SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN + "','" + SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE + "','" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CLOSE + "')";
                }

                if (whereClause.Equals(""))
                {
                    queryData("1=2");
                }
                else
                {
                    queryData(whereClause);
                }

                
            }
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
            agent.loadSchema("WebServerProject.maintain.SPPM011.SmpPmUserAnalysisAgent");
            agent.engine = engine;
            agent.query(whereClause);
            DataObjectSet set = agent.defaultData;

            dlAchievementList.InputForm = "AnalysisNew.aspx";
            dlAchievementList.DialogHeight = 600;
            dlAchievementList.DialogWidth = 840;
            dlAchievementList.HiddenField = new string[] {"AssessUserGUID"};
            dlAchievementList.dataSource = set;
            dlAchievementList.updateTable();
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

    protected void GbFind_Click(object sender, EventArgs e)
    {
        string whereClause = "";
        if (!sddlAssessmentPlanGUID.ValueText.Equals(""))
        {
            whereClause = "AssessmentName='" + sddlAssessmentPlanGUID.ValueText + "' and Status in('" + SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE + "','" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CLOSE + "') and SubmitStatus in('A','Y')";
        }

   
        if (whereClause.Equals(""))
        {
            queryData("1=2");
        }
        else
        {
            queryData(whereClause);
        }
    }

    protected void gbAchievementFind_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(sddlAssessmentPlanGUID.ValueText))
        {
            Session["SPPM011_sddlAssessmentPlanGUID"] = sddlAssessmentPlanGUID.ValueText;   
            string url = "AnalysisAll.aspx";
            base.showOpenWindow(url, "成績分佈統計匯總查詢", "", "1000", "500", "500", "", "1", "1", "", "", "", "", "800", "", true);
        }
        else
        {
            MessageBox("請先選擇考核名稱，並執行查詢!");
        }
    }
    private void AllApprove()
    {

        if (!string.IsNullOrEmpty(sddlAssessmentPlanGUID.ValueText))
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            string Submit = "Y";
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            string sqltxt = "update dbo.SmpPmUserAchievement set SubmitStatus='" + Submit + "',Status ='" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CLOSE + "' where AssessmentPlanGUID  in (" + SmpPmMaintainUtil.GetfilterNameplanGUID(sddlAssessmentPlanGUID.ValueText) + ") ";
            bool result = engine.executeSQL(sqltxt);
            if (!result)
            {
                throw new Exception(engine.errorString);
            }
            else
            {
                MessageBox("一鍵核准成功!");
                string whereClause = "";
                if (!sddlAssessmentPlanGUID.ValueText.Equals(""))
                {
                    whereClause = "AssessmentName='" + sddlAssessmentPlanGUID.ValueText + "' and Status in('" + SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE + "','" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CLOSE + "')";
                }

                if (whereClause.Equals(""))
                {
                    queryData("1=2");
                }
                else
                {
                    queryData(whereClause);
                }
            }

            engine.close();
        }
        else
        {
            MessageBox("請先選擇考核名稱，並執行查詢!");
        }

    }
    protected void gbAprove_Click(object sender, EventArgs e)
    {
        AllApprove();
    }
}