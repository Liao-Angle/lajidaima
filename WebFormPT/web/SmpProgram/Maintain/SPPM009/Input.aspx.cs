using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebServerProject;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.kernal.mail;
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM003_Input : BaseWebUI.DataListSaveForm
{

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
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                //設定公司別
                string[,] ids = SmpPmMaintainUtil.getCompanyIds(engine);
                CompanyCode.setListItem(ids);
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
            bool isNew = (bool)getSession("isNew");

            CompanyCode.ValueText = objects.getData("CompanyCode");
            AssessYear.ValueText = objects.getData("AssessYear");
            AssessStartDate.ValueText = objects.getData("AssessStartDate");
            AssessEndDate.ValueText = objects.getData("AssessEndDate");
            PlanEndDate.ValueText = objects.getData("PlanEndDate");
            AssessmentName.ValueText = objects.getData("AssessmentName");
            Status.ValueText = objects.getData("Status");
            StartDate.ValueText = objects.getData("StartDate");
            CloseDate.ValueText = objects.getData("CloseDate");

            //考核對象階段狀態
            DataObjectSet userStageSet = objects.getChild("SmpPmUserAssessmentStage");
            UserAssessmentStageList.dataSource = userStageSet;
            //UserAssessmentStageList.InputForm = "Detail.aspx";
            UserAssessmentStageList.DialogHeight = 600;
            UserAssessmentStageList.DialogWidth = 840;
            UserAssessmentStageList.HiddenField = new string[] { "GUID", "UserGUID", "AssessmentPlanGUID", "AssessmentManagerGUID", "AssessUserGUID", "AssessUserId", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            //UserAssessmentStageList.reSortCondition("部門", DataObjectConstants.ASC);
            UserAssessmentStageList.addSortCondition("部門", DataObjectConstants.ASC);
            UserAssessmentStageList.addSortCondition("工號", DataObjectConstants.ASC);
            UserAssessmentStageList.addSortCondition("評核階段", DataObjectConstants.ASC);
            UserAssessmentStageList.updateTable();
            
            disableAssessmentPlan();
            setSession("objects", objects);
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

    /// <summary>
    /// 儲存資料
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        string errMsg = "";
        IOFactory factory = null;
        AbstractEngine engine = null;

        try
        {
            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

            //objects.setData("CompanyCode", CompanyCode.ValueText);
            //objects.setData("AssessYear", AssessYear.ValueText);
            //objects.setData("AssessStartDate", AssessStartDate.ValueText);
            //objects.setData("AssessEndDate", AssessEndDate.ValueText);
            //objects.setData("PlanEndDate", PlanEndDate.ValueText);
            //objects.setData("AssessmentName", AssessmentName.ValueText);
            //objects.setData("Status", Status.ValueText);
            //objects.setData("StartDate", StartDate.ValueText);
            //objects.setData("CloseDate", CloseDate.ValueText);

            //檢查欄位資料
            errMsg = checkFieldData(objects, engine);

            if (!errMsg.Equals(""))
            {
                errMsg = errMsg.Replace("\n", "; ");
            }

            setSession("objects", objects);
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

    /// <summary>
    /// 檢查資料
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="engine"></param>
    /// <returns></returns>
    public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
        
        return errMsg;
    }

    /// <summary>
    /// 儲存資料至資料表
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPPM009.SmpPmAssessmentPlanAgent");
        agent.engine = engine;
        agent.query("1=2");

        bool result = agent.defaultData.add(objects);
        if (!result)
        {
            engine.close();
            throw new Exception(agent.defaultData.errorString);
        }
        else
        {
            result = agent.update();
            engine.close();
            if (!result)
            {
                throw new Exception(engine.errorString);
            }
        }
    }

    /// <summary>
    /// 考核計畫唯讀
    /// </summary>
    protected void disableAssessmentPlan()
    {
        //計劃表單
        CompanyCode.ReadOnly = true;
        AssessYear.ReadOnly = true;
        AssessStartDate.ReadOnly = true;
        AssessEndDate.ReadOnly = true;
        PlanEndDate.ReadOnly = true;
        AssessmentName.ReadOnly = true;
    }
}
