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
using System.Xml;
using WebServerProject.org.Organization;
using WebServerProject.auth;

public partial class SmpProgram_Maintain_SPAD005_OrganizationDetail : BaseWebUI.GeneralWebPage
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

                int auth = authagent.getAuth("SPAD005M", (string)Session["UserID"], (string[])Session["usergroup"]);

                engine.close();

                string mstr = "";
                if (auth == 0)
                {
                    Response.Redirect("~/NoAuth.aspx");
                }

                //string OID = com.dsc.kernal.utility.Utility.filter(Request.QueryString["OID"]);
				string OID = Convert.ToString(Request.QueryString["OID"]);

                engine = factory.getEngine(engineType, connectString);

                OrganizationAgent agent = new OrganizationAgent();
                agent.engine = engine;
                agent.query("OID='" + OID + "'");
                DataObjectSet dos = agent.defaultData;

                engine.close();

                Organization o = (Organization)dos.getAvailableDataObject(0);

                companyCode.ValueText = o.id;
                companyName.ValueText = o.organizationName;


            }
        }
    }
}
