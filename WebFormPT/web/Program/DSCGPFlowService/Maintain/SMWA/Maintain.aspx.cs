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
using WebServerProject.flow.SMWA;

public partial class Program_DSCGPFlowService_Maintain_SMWA_Maintain : BaseWebUI.GeneralWebPage
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
        Master.maintainIdentity = "SMWA";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMWAA";
        SMWAAAA obj = new SMWAAAA();
        Master.HiddenField = new string[] { "SMWAAAA001", "SMWAAAA004", "SMWAAAA007", "SMWAAAA008", "SMWAAAA009", "SMWAAAA010", "SMWAAAA011", "SMWAAAA012", "SMWAAAA013", "SMWAAAA014", "SMWAAAA015", "SMWAAAA016", "SMWAAAA017", "SMWAAAA018", "SMWAAAA019", "SMWAAAA021", "SMWAAAA022", "SMWAAAA023", "SMWAAAA024", "SMWAAAA025", "SMWAAAA026", "SMWAAAA027", "SMWAAAA028", "SMWAAAA029" };

        Master.DialogHeight = 710;
        
        Master.inputForm = "Detail.aspx";
        Master.InitData(obj);

    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMWAAgent agent = new SMWAAgent();
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
        SMWAAgent agent = new SMWAAgent();
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
