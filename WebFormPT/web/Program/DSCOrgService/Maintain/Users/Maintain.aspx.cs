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

public partial class Program_DSCOrgService_Maintain_Users_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick += new ProjectBaseWebUI_ListMaintain.SendButtonClickEvent(Master_SendButtonClick);
        Master.getListTable().RefreshButtonClick += new DSCWebControl.DataList.RefreshButtonClickEvent(Program_System_Maintain_Users_Maintain_RefreshButtonClick);
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
        Master.maintainIdentity = "Users";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "Organization";
        Users obj = new Users();
        Master.HiddenField = new string[] {
                            "OID", "objectVersion", "password", "referCalendarOID", 
                "workflowServerOID", "mailingFrequencyType","identificationType", "localeString","ldapid","enableSubstitute","cost"};

        Master.inputForm = "Detail.aspx";
        Master.getListTable().NoDelete = true;

        Master.getListTable().FormTitle = com.dsc.locale.LocaleString.getSystemMessageString("program_dscorgservice_maintain_users_maintain_aspx.language.ini", "message", "Title", "使用者資料維護");
        Master.DialogWidth = 750;
        Master.DialogHeight = 500;
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
    public void Program_System_Maintain_Users_Maintain_RefreshButtonClick()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        UsersAgent agent = new UsersAgent();

        agent.engine = engine;
        agent.query((string)getSession("whereClause"));


        engine.close();

        Master.basedos = agent.defaultData;
        Master.getListTable().updateTable();
    }


}
