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
using WebServerProject.maintain.SMVC;

public partial class Program_System_Maintain_SMVC_Maintain : BaseWebUI.GeneralWebPage
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
        Master.maintainIdentity = "SMVC";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMVAA";
        SMVCAAA obj = new SMVCAAA();
        Master.HiddenField = new string[] { "SMVCAAA001", "SMVCAAA002"};

        Master.DialogHeight = 470;
        Master.inputForm = "Detail.aspx";
        Master.InitData(obj);

        Master.getListTable().NoAdd = true;
        
    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMVCAgent agent = new SMVCAgent();
        agent.engine = engine;

        string m_whereClause = whereClause;
        if (whereClause.Equals(""))
        {
            m_whereClause = "SMVCAAA002='" + (string)Session["UserID"] + "'";
        }
        else
        {
            m_whereClause = whereClause + " and SMVCAAA002='" + (string)Session["UserID"] + "'";
        }
        bool res = agent.query(m_whereClause, 1000);
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
        SMVCAgent agent = new SMVCAgent();
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
