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
using WebServerProject.flow.SMWR;

public partial class Program_DSCGPFlowService_Maintain_SMWR_Maintain : BaseWebUI.GeneralWebPage
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
        Master.maintainIdentity = "SMWR";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMWAA";
        SMWRAAA obj = new SMWRAAA();
        Master.HiddenField = new string[] { "SMWRAAA001", "SMWRAAA002", "SMWRAAA003", "SMWRAAA004", "SMWRAAA005", "SMWRAAA006", "SMWRAAA007", "SMWRAAA008", "SMWRAAA009", "SMWRAAA010", "SMWRAAA011", "SMWRAAA012", "SMWRAAA013", "SMWRAAA014", "SMWRAAA015","SMWRAAA016","SMWRAAA017","SMWRAAA018","SMWRAAA019","SMWRAAA020","SMWRAAA021","SMWRAAA024","SMWRAAA200" };

        Master.DialogHeight = 530;
        
        Master.inputForm = "Detail.aspx";
        Master.InitData(obj);

        
    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMWRAgent agent = new SMWRAgent();
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
        SMWRAgent agent = new SMWRAgent();
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
