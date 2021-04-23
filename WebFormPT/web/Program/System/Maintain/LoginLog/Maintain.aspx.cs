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
using WebServerProject.maintain.LoginLog;

public partial class Program_System_Maintain_LoginLog_Maintain : BaseWebUI.GeneralWebPage
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
        Master.maintainIdentity = "LoginLog";
        Master.getListTable().showExcel = true;
        LoginData obj = new LoginData();
        Master.HiddenField = new string[] { "GUID", "SESSIONID", "PROCESSID" };

        Master.InitData(obj);

    }
    public void Master_SendButtonClick(string whereClause)
    {
        setSession("whereClause", whereClause);

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        LoginDataAgent agent = new LoginDataAgent();
        agent.engine = engine;
        bool res = agent.query(whereClause);
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        Master.basedos = agent.defaultData;
    }


    protected void ClearLogButton_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "delete from LOGINDATA";
        engine.executeSQL(sql);
        engine.close();

        Master_SendButtonClick("");
        Master.getListTable().updateTable();
        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_loginlog_maintain_aspx.language.ini", "message", "QueryError1", "清除完畢"));
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "update LOGINDATA set LOGOUTTIME='" + DateTimeUtility.getSystemTime2(null) + "' where LOGOUTTIME='' ";
        engine.executeSQL(sql);
        engine.close();

        string whereClause = (string)getSession("whereClause");
        if (whereClause != null)
        {
            Master_SendButtonClick(whereClause);
            Master.getListTable().updateTable();
        }
        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_loginlog_maintain_aspx.language.ini", "message", "QueryError2", "註記完畢"));
    }
}
