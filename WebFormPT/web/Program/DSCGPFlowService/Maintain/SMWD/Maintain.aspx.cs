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
using WebServerProject.flow.SMWD;

public partial class Program_DSCGPFlowService_Maintain_SMWD_Maintain : BaseWebUI.GeneralWebPage
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
        Master.maintainIdentity = "SMWD";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMWAA";
        SMWDAAA obj = new SMWDAAA();
        Master.HiddenField = new string[] { "SMWDAAA001", "SMWDAAA005", "SMWDAAA006", "SMWDAAA007", "SMWDAAA008", "SMWDAAA009", "SMWDAAA010", "SMWDAAA011", "SMWDAAA012", "SMWDAAA013", "SMWDAAA014","SMWDAAA015","SMWDAAA016","SMWDAAA017","SMWDAAA018","SMWDAAA019","SMWDAAA020","SMWDAAA021","SMWDAAA022","SMWDAAA023","SMWDAAA024","SMWDAAA025","SMWDAAA026","SMWDAAA027","SMWDAAA028","SMWDAAA029","SMWDAAA030","SMWDAAA031","SMWDAAA032","SMWDAAA033","SMWDAAA034","SMWDAAA051","SMWDAAA052","SMWDAAA053","SMWDAAA054","SMWDAAA055","SMWDAAA056","SMWDAAA101", "SMWDAAA102", "SMWDAAA103", "SMWDAAA104", "SMWDAAA105", "SMWDAAA106", "SMWDAAA107", "SMWDAAA108", "SMWDAAA109", "SMWDAAA110", "SMWDAAA111", "SMWDAAA112", "SMWDAAA113", "SMWDAAA114", "SMWDAAA115", "SMWDAAA116", "SMWDAAA117", "SMWDAAA118", "SMWDAAA119", "SMWDAAA120", "SMWDAAA121", "SMWDAAA122", "SMWDAAA151","SMWDAAA152","SMWDAAA153","SMWDAAA154","SMWDAAA155","SMWDAAA156","SMWDAAA201", "SMWDAAA202", "SMWDAAA203", "SMWDAAA204", "SMWDAAA205", "SMWDAAA206", "SMWDAAA207", "SMWDAAA208", "SMWDAAA209", "SMWDAAA210", "SMWDAAA211", "SMWDAAA212", "SMWDAAA213", "SMWDAAA214", "SMWDAAA215", "SMWDAAA216", "SMWDAAA217", "SMWDAAA218", "SMWDAAA219", "SMWDAAA220", "SMWDAAA221", "SMWDAAA222","SMWDAAA251","SMWDAAA252","SMWDAAA253","SMWDAAA254","SMWDAAA255","SMWDAAA256",
        "SMWDAAA300","SMWDAAA301","SMWDAAA302","SMWDAAA303","SMWDAAA304","SMWDAAA305","SMWDAAA306","SMWDAAA307","SMWDAAA308","SMWDAAA309","SMWDAAA310","SMWDAAA311","SMWDAAA312","SMWDAAA313","SMWDAAA314","SMWDAAA315","SMWDAAA316","SMWDAAA321","SMWDAAA322","SMWDAAA323","SMWDAAA324","SMWDAAA325","SMWDAAA326","SMWDAAA327","SMWDAAA328","SMWDAAA329","SMWDAAA330","SMWDAAA331","SMWDAAA332","SMWDAAA333","SMWDAAA334","SMWDAAA335","SMWDAAA451","SMWDAAA452","SMWDAAA453","SMWDAAA454","SMWDAAA455","SMWDAAA551","SMWDAAA552","SMWDAAA553","SMWDAAA554","SMWDAAA555", "SMWDAAA057"};

        Master.DialogHeight = 700;
        Master.DialogWidth = 800;
        
        Master.inputForm = "Detail.aspx";
        Master.InitData(obj);

        
    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMWDAgent agent = new SMWDAgent();
        agent.engine = engine;

        //bool res = agent.queryHead(whereClause);
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
        SMWDAgent agent = new SMWDAgent();
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
