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
using WebServerProject.maintain.SMVD;

public partial class Program_System_Maintain_SMVDU_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick += new ProjectBaseWebUI_ReadOnlyMaintain.SendButtonClickEvent(Master_SendButtonClick);
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
        Master.maintainIdentity = "SMVDU";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMVAA";
        SMVDAAA obj = new SMVDAAA();
        Master.HiddenField = new string[] { "SMVDAAA001"};

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

        SMVDAgent agent = new SMVDAgent();
        agent.engine = engine;

        string mwhereClause = "";
        if (whereClause.Equals(""))
        {
            mwhereClause = " SMVDAAA001 not in (select SMVDAAB002 from SMVDAAB where SMVDAAB003='" + (String)Session["UserID"] + "') or SMVDAAA001 in (select SMVDAAB002 from SMVDAAB where SMVDAAB003='"+(String)Session["UserID"]+"' and SMVDAAB005='N')";
        }
        else
        {
            mwhereClause = whereClause + " and (SMVDAAA001 not in (select SMVDAAB002 from SMVDAAB where SMVDAAB003='" + (String)Session["UserID"] + "') or SMVDAAA001 in (select SMVDAAB002 from SMVDAAB where SMVDAAB003='" + (String)Session["UserID"] + "' and SMVDAAB005='N'))";
        }
        bool res = agent.queryHead(mwhereClause);
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        Master.basedos = agent.defaultData;
    }


}
