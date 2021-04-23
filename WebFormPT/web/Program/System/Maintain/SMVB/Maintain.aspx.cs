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
using WebServerProject.maintain.SMVB;

public partial class Program_System_Maintain_SMVB_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick+=new ProjectBaseWebUI_ListMaintain.SendButtonClickEvent(Master_SendButtonClick);
        Master.DeleteButtonClick+=new ProjectBaseWebUI_ListMaintain.DeleteButtonClickEvent(Master_DeleteButtonClick);
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
        Master.maintainIdentity = "SMVB";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMVAA";
        SMVAAAB obj = new SMVAAAB();
        Master.HiddenField = new string[] { "SMVAAAB001", "SMVAAAB005", "SMVAAAB006", "SMVAAAB007", "SMVAAAB008", "SMVAAAB009", "SMVAAAB010","SMVAAAB011","SMVAAAB012","SMVAAAB013","SMVAAAB014" };
        //Master.getListTable().reOrderField(new string[] { "SMVAAAB001", "SMVAAAB003", "SMVAAAB002", "SMVAAAB004", "SMVAAAB005", "SMVAAAB006", "SMVAAAB007", "SMVAAAB008", "SMVAAAB009", "SMVAAAB010", "SMVAAAB011", "SMVAAAB012", "SMVAAAB013", "SMVAAAB014", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME" });
        Master.DialogHeight = 600;
        Master.DialogWidth = 750;
        Master.inputForm = "Detail.aspx";
        Master.getListTable().showSetup = true;
        Master.getListTable().showExcel = true;
       
        //Master.getListTable().isShowAll = true;
        //Master.getListTable().PageSize = 20;
        Master.InitData(obj);

    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMVBAgent agent = new SMVBAgent();
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
        SMVBAgent agent = new SMVBAgent();
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
