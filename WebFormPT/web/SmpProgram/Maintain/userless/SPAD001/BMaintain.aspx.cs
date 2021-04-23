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
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class SmpProgram_BMaintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick += new ProjectBaseWebUI_BasicMaintain.SendButtonClickEvent(Master_SendButtonClick);
        Master.SaveButtonClick += new ProjectBaseWebUI_BasicMaintain.SaveButtonClickEvent(Master_SaveButtonClick);
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                start();
            }
        }
    }

    public void start()
    {
        Master.maintainIdentity = "SPAD001M";
        Master.ApplicationID = "SMPFORM";
        Master.ModuleID = "SPAD";

        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.maintain.SPAD001.SmpUserFlow");

        Master.HiddenField = new string[] { "GUID", "UserGUID", "FlowGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };

        Master.DialogHeight = 420;
        Master.inputForm = "Input.aspx";
        Master.InitData(obj);
    }

    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPAD001.SmpUserFlowAgent");
        agent.engine = engine;

        bool res = agent.query(whereClause);
        engine.close();

        if (!res)
        {
            throw new Exception(engine.errorString);
        }
        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        Master.basedos = agent.defaultData;
    }

    public void Master_SaveButtonClick()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPAD001.SmpUserFlowAgent");
        agent.engine = engine;

        agent.defaultData = Master.basedos;

        bool res = agent.update();

        engine.close();

        if (!res)
        {
            throw new Exception(engine.errorString);
        }
        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        MessageBox("儲存成功");
    }
}
