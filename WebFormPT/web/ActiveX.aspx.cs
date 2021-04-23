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
using System.Text;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class ActiveX : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            renderDownload();
        }
    }

    protected void renderDownload()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        WebServerProject.auth.AUTHAgent agent = new WebServerProject.auth.AUTHAgent();
        agent.engine = engine;

        StringBuilder sb = new StringBuilder();
        string sql = "select * from SMVSAAA where SMVSAAA004='Y'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            int auth=agent.getAuthFromAuthItem(ds.Tables[0].Rows[i]["SMVSAAA005"].ToString(), (string)Session["UserID"], (string[])Session["usergroup"]);
            if (agent.parse(auth, WebServerProject.auth.AUTHAgent.READ))
            {
                sb.Append("<li>" + ds.Tables[0].Rows[i]["SMVSAAA002"].ToString());
                sb.Append("<br>");
                sb.Append(ds.Tables[0].Rows[i]["SMVSAAA003"].ToString());
                sb.Append("<br>");
            }
        }

        engine.close();
        Downloads.Text = sb.ToString();
    }
 }
