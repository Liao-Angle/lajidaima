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

public partial class Program_System_Form_STDDOC4019_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick += new ProjectBaseWebUI_GeneralMaintain.SendButtonClickEvent(Master_SendButtonClick);
        //Master.DeleteButtonClick += new ProjectBaseWebUI_GeneralMaintain.DeleteButtonClickEvent(Master_DeleteButtonClick);
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
        Master.maintainIdentity = "STDDOC4019M"; // =ProcessPageID
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SAMPLE";
        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.form.STDDOC4019.STDDOC4019");

        Master.HiddenField = new string[] {"GUID","STDDOC001","IS_LOCK","IS_DISPLAY","DATA_STATUS" };

        Master.getListTable().IsGeneralUse = true;
        Master.DialogHeight = 470;
        Master.inputForm = "Default.aspx";
        Master.InitData(obj);

        Master.getListTable().NoAdd = true;
        Master.getListTable().FormTitle = com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc4019_maintain_aspx.language.ini", "message", "Title", "簽呈檢視");
    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.form.STDDOC4019.STDDOC4019Agent");
        agent.engine = engine;

        bool res = agent.query(whereClause);
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        Master.basedos = agent.defaultData;
    }
    public void Master_DeleteButtonClick()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.form.STDDOC4019.STDDOC4019Agent");
        agent.engine = engine;
        agent.defaultData = Master.basedos;

        bool result = agent.update();
        engine.close();
        if (!result)
        {
            throw new Exception(engine.errorString);
        }
    }
}
