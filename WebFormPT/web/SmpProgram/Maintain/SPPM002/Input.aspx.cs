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
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM002_Input : BaseWebUI.DataListSaveForm
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

                UserGUID.connectDBString = connectString;
                UserGUID.clientEngineType = engineType;

                FirstAssessUserGUID.connectDBString = connectString;
                FirstAssessUserGUID.clientEngineType = engineType;

                SecondAssessUserGUID.connectDBString = connectString;
                SecondAssessUserGUID.clientEngineType = engineType;

                AchievementUserGUID.connectDBString = connectString;
                AchievementUserGUID.clientEngineType = engineType;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        //公司別
        orgName.ValueText = objects.getData("orgName");
        //工號/姓名
        UserGUID.ValueText = objects.getData("UserGUID");
        UserGUID.doValidate();
        //職稱
        titleName.ValueText = objects.getData("titleName");
        //部門代號/名稱
        string deptId = objects.getData("deptId");
        string deptName = objects.getData("deptName");
        deptOID.ValueText = deptId;
        deptOID.ReadOnlyValueText = deptName;

        //一階評核人員
        FirstAssessUserGUID.ValueText = objects.getData("FirstAssessUserGUID");
        FirstAssessUserGUID.doValidate();
        //二階評核人員
        SecondAssessUserGUID.ValueText = objects.getData("SecondAssessUserGUID");
        SecondAssessUserGUID.doValidate();

        string achievementUserGUID = objects.getData("AchievementUserGUID");
        AchievementUserGUID.ValueText = achievementUserGUID;
        if (!string.IsNullOrEmpty(achievementUserGUID))
        {
            AchievementUserGUID.doGUIDValidate();
        }

        if (isNew)
        {
            
        }
        else
        {
            
        }

        engine.close();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("orgName", orgName.ValueText);
        objects.setData("UserGUID", UserGUID.ValueText);
        objects.setData("empNumber", UserGUID.ValueText);
        objects.setData("empName", UserGUID.ReadOnlyValueText);
        objects.setData("deptId", deptOID.ValueText);
        objects.setData("deptName", deptOID.ReadOnlyValueText);
        objects.setData("titleName", titleName.ValueText);
        objects.setData("FirstAssessUserGUID", FirstAssessUserGUID.ValueText);
        objects.setData("FirstAssessUserId", FirstAssessUserGUID.ValueText);
        objects.setData("FirstAssessUserName", FirstAssessUserGUID.ReadOnlyValueText);
        objects.setData("SecondAssessUserGUID", SecondAssessUserGUID.ValueText);
        objects.setData("SecondAssessUserId", SecondAssessUserGUID.ValueText);
        objects.setData("SecondAssessUserName", SecondAssessUserGUID.ReadOnlyValueText);
        objects.setData("AchievementUserGUID", AchievementUserGUID.ValueText);
        objects.setData("AchievementUserId", AchievementUserGUID.ValueText);
        objects.setData("AchievementUserName", AchievementUserGUID.ReadOnlyValueText);

        //檢查欄位資料
        string errMsg = checkFieldData(objects, engine);
        engine.close();
        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
    }

    public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
        
        return errMsg;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPPM002.SmpPmUserAssessmentAgent");
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

    protected void UserGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string[] result = SmpPmMaintainUtil.getUserInfoById(engine, values[0, 2]);
        orgName.ValueText = result[8];
        titleName.ValueText = result[3];
        deptOID.ValueText = result[6];
        deptOID.ReadOnlyValueText = result[7];
        engine.close();
    }
}
