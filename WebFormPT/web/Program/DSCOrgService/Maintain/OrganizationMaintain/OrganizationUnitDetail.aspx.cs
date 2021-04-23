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

public partial class Program_DSCOrgService_Maintain_OrganizationMaintain_OrganizationUnitDetail : BaseWebUI.GeneralWebPage
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

                int auth = authagent.getAuth("OrganizationMaintain", (string)Session["UserID"], (string[])Session["usergroup"]);

                engine.close();

                string mstr = "";
                if (auth == 0)
                {
                    Response.Redirect("~/NoAuth.aspx");
                }

                string OID = Request.QueryString["OID"];

                engine = factory.getEngine(engineType, connectString);

                OrganizationUnitAgent agent = new OrganizationUnitAgent();
                agent.engine = engine;
                agent.query("x.OID='" + OID + "'");
                DataObjectSet dos = agent.defaultData;

                engine.close();

                OrganizationUnit o = (OrganizationUnit)dos.getAvailableDataObject(0);

                OIDF.ValueText = o.OID;
                objectVersionF.ValueText = o.objectVersion;
                idF.ValueText = o.id;
                organizationUnitNameF.ValueText = o.organizationUnitName;
                managerOIDF.ValueText = o.managerOID;
                userNameF.ValueText = o.userName;
                superUnitOIDF.ValueText = o.superUnitOID;
                if (o.organizationUnitType.Equals("0"))
                {
                    organizationUnitTypeF.ValueText = com.dsc.locale.LocaleString.getSystemMessageString("program_dscorgservice_maintain_organizationmaintain_organizationunitdetail_aspx.language", "message", "Text1", "部門");
                }
                else
                {
                    organizationUnitTypeF.ValueText = com.dsc.locale.LocaleString.getSystemMessageString("program_dscorgservice_maintain_organizationmaintain_organizationunitdetail_aspx.language", "message", "Text2", "專案");
                }
                levelOIDF.ValueText = o.levelOID;
                organizationUnitLevelNameF.ValueText = o.organizationUnitLevelName;
                organizationOIDF.ValueText = o.organizationOID;
                organizationNameF.ValueText = o.organizationName;

                PropertyList.HiddenField = new string[] { "OID","objectVersion","organizationOID","OrganizationUnitOID" };
                PropertyList.ReadOnly = true;
                PropertyList.InputForm = null;
                PropertyList.dataSource = o.getChild("OrganizationUnitProperty");
                PropertyList.updateTable();

                FunctionList.HiddenField = new string[] { "OID","objectVersion" };
                FunctionList.ReadOnly = true;
                FunctionList.InputForm = null;
                FunctionList.dataSource = o.getChild("FunctionUser");
                FunctionList.updateTable();

                TitleList.HiddenField = new string[] { "OID", "objectVersion" };
                TitleList.ReadOnly = true;
                TitleList.InputForm = null;
                TitleList.dataSource = o.getChild("TitleUser");
                TitleList.updateTable();

                RoleList.HiddenField = new string[] { "OID", "objectVersion" };
                RoleList.ReadOnly = true;
                RoleList.InputForm = null;
                RoleList.dataSource = o.getChild("RoleUser");
                RoleList.updateTable();
            }
        }
    }
}
