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
using WebServerProject.org.OrganizationUnitLevel;

public partial class Program_DSCOrgService_Maintain_OrganizationUnitLevelMaintain_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick+=new ProjectBaseWebUI_ReadOnlyMaintain.SendButtonClickEvent(Master_SendButtonClick);
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
        Master.maintainIdentity = "OrganizationUnitLevelMaintain";
        OrganizationUnitLevel obj = new OrganizationUnitLevel();
        Master.HiddenField = new string[] {
                            "OID", "objectVersion", "organizationOID"};

        Master.inputForm = "Detail.aspx";
        Master.DialogWidth = 750;
        Master.InitData(obj);

        Master.getQueryPage().HiddenField = new string[] { "OID", "objectVersion", "organizationOID", "description" };

    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        OrganizationUnitLevelAgent agent = new OrganizationUnitLevelAgent();
        agent.engine = engine;
        bool res = agent.query(whereClause);
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString.Replace("\n","").Replace("\r",""));
        }
        Master.basedos = agent.defaultData;
    }


}
