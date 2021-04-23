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
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.Xml;
using WebServerProject.auth;

public partial class SmpProgram_Form_SPKM005_ViewDocTypeReader : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                //權限判斷
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                AUTHAgent authagent = new AUTHAgent();
                authagent.engine = engine;
                int auth = authagent.getAuth("SPKM005", (string)Session["UserID"], (string[])Session["usergroup"]);
                engine.close();
                string mstr = "";
                if (auth == 0)
                {
                    Response.Redirect("~/NoAuth.aspx");
                }
                string UserID = (string)Session["UserID"];

                queryData();
            }
        }
    }

    protected void queryData()
    {
        string where = "";
        string docTypeGUID = (string)getSession((string)Session["UserID"], "DocTypeGUID");
        if (Request.QueryString["DocTypeGUID"] != null)
        {
            docTypeGUID = Utility.filter(Request.QueryString["DocTypeGUID"]);
        }
        if (!string.IsNullOrEmpty(docTypeGUID))
        {
            where = " DocTypeGUID='" + docTypeGUID + "'";
            query(where);
        }
    }

    protected void query(string where)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.form.SPKM005.SmpDocTypeReaderAgent");
        agent.engine = engine;
        if (!where.Equals(""))
        {
            agent.query(where);
        }
        else
        {
            agent.query("1=2");
        }
        DataObjectSet dos = agent.defaultData;
        ReaderList.dataSource = dos;

        ReaderList.showSettingPages = new Boolean[] { false, false, false, false, false, false, false, false, false, false };
        ReaderList.HiddenField = new string[] { "GUID", "DocTypeGUID", "BelongGroupType", "BelongGroupGUID", "Kind", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME" };
        ReaderList.updateTable();
        engine.close();
    }
}