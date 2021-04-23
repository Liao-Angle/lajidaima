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
using WebServerProject.maintain.SMVL;

public partial class Program_System_Maintain_SMVL_Maintain : BaseWebUI.GeneralWebPage
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
                Master.SendButton_Click(sender, e); //2009/08/10 hjlin add for mantis 13337
                //Master.getListTable().ReadOnly = true;
            }
        }
    }

    public void start()
    {
        Master.maintainIdentity = "SMVL";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMVAA";
        SMVLAAA obj = new SMVLAAA();
        Master.HiddenField = new string[] { "SMVLAAA001", "SMVLAAA002"};

        Master.DialogHeight = 470;
        Master.inputForm = "Detail.aspx";
        Master.InitData(obj);

    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMVLAgent agent = new SMVLAgent();
        agent.engine = engine;

        if (!whereClause.Equals(""))
        {
            whereClause += " and SMVLAAA002='" + (string)Session["UserID"] + "'";
        }
        else
        {
            whereClause = " SMVLAAA002='" + (string)Session["UserID"] + "'";
        }
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
        SMVLAgent agent = new SMVLAgent();
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
