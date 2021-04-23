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
using WebServerProject;
using com.dsc.kernal.mail;
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM007_Detail : BaseWebUI.DataListSaveForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                SaveButton.Visible = false;
                
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                //設定等級
                string[,] ids = SmpPmMaintainUtil.getAchievementLevel(engine);
                AchievementLevel.setListItem(ids);
                //設定狀態
                ids = SmpPmMaintainUtil.getAssessmentStatusIds(engine);
                Status.setListItem(ids);

                engine.close();
            }
        }
    }

    /// <summary>
    /// 顯示資料
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        string errMsg = "";

        try
        {
            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

            string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
            string userGUID = objects.getData("UserGUID");
            string whereClause = "AssessmentManagerGUID='" + assessmentManagerGUID + "' and UserGUID='" + userGUID + "'";          
            AssessmentName.ValueText = objects.getData("AssessmentName");           
            empName.ValueText = objects.getData("empName");
            deptName.ValueText = objects.getData("deptName");            
            Status.ValueText = objects.getData("Status");
            SelfScore.ValueText = objects.getData("SelfScore");
            SelfComments.ValueText = objects.getData("SelfComments");
            FirstScore.ValueText = objects.getData("FirstScore");
            FirstComments.ValueText = objects.getData("FirstComments");
            SecondScore.ValueText = objects.getData("SecondScore");
            SecondComments.ValueText = objects.getData("SecondComments");
            FinalScore.ValueText = objects.getData("FinalScore");
            FinalComments.ValueText = objects.getData("FinalComments");
            AchievementLevel.ValueText = objects.getData("AchievementLevel");

            //考核表分數明細            
            DataObjectSet scoreDetailSet = objects.getChild("SmpPmAssessmentScoreDetail");
            AchievementDetailList.dataSource = scoreDetailSet;
            AchievementDetailList.setColumnStyle("Content", 360, DSCWebControl.GridColumnStyle.LEFT);          
            //AssessmentScoreDetailList.InputForm = "DetailSummary.aspx";
            //AssessmentScoreDetailList.DialogHeight = 600;
            //AssessmentScoreDetailList.DialogWidth = 840;           
            AchievementDetailList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "EvaluationDetailGUID", "UserGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };            
            AchievementDetailList.reSortCondition("編號", DataObjectConstants.ASC);
            AchievementDetailList.updateTable();
        }
        catch (Exception e)
        {
            errMsg += e.Message;
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }

        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
    }


    
 
}