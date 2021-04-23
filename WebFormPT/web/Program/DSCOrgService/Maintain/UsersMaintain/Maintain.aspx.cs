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
using WebServerProject.org.Users;

public partial class Program_DSCOrgService_Maintain_UsersMaintain_Maintain : BaseWebUI.GeneralWebPage
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
        Master.maintainIdentity = "UsersMaintain";
        Users obj = new Users();
        Master.HiddenField = new string[] {
                            "OID", "objectVersion", "password", "referCalendarOID", 
                "workflowServerOID", "mailingFrequencyType"};

        Master.inputForm = "Detail.aspx";
        Master.DialogWidth = 750;
        Master.InitData(obj);

        
    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        UsersAgent agent = new UsersAgent();
        agent.engine = engine;
        bool res = agent.query(whereClause,100);
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString.Replace("\n","").Replace("\r",""));
        }
        Master.basedos = agent.defaultData;
    }


}
