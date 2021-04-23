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
using WebServerProject.auth;

public partial class Program_DSCOrgService_Maintain_OrganizationMaintain_Maintain : BaseWebUI.GeneralWebPage
{
    private string lsplitter = "#!#!#";
    private string splitter = "#*#*#";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = false;

        string method = Request.Form["Method"];
        if ((method != null) && (!method.Equals("")))
        {
            if (method.Equals("GetOrganization"))
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "";

                string data = "";

                sql = "select OID, id, organizationName from Organization";
                DataSet ds = engine.getDataSet(sql, "TEMP");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    data += ds.Tables[0].Rows[i][0].ToString() + splitter;
                    data += ds.Tables[0].Rows[i][1].ToString() + splitter;
                    data += ds.Tables[0].Rows[i][2].ToString();
                    if (i < (ds.Tables[0].Rows.Count - 1))
                    {
                        data += lsplitter;
                    }
                }


                engine.close();

                Response.Clear();
                Response.Write(data);
                Response.End();
            }
            else if (method.Equals("GetOrganizationUnit"))
            {
                string orgID = Request.Form["O"];
                string superID = Request.Form["P"];

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "";

                string data = "";

                sql = "select OID, id, organizationUnitName from OrganizationUnit where organizationOID='" + Utility.filter(orgID) + "' and ";
                if (superID.Equals(""))
                {
                    sql += " superUnitOID is null";
                }
                else
                {
                    sql += " superUnitOID='" + Utility.filter(superID) + "'";
                }
                DataSet ds = engine.getDataSet(sql, "TEMP");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    data += ds.Tables[0].Rows[i][0].ToString() + splitter;
                    data += ds.Tables[0].Rows[i][1].ToString() + splitter;
                    data += ds.Tables[0].Rows[i][2].ToString();
                    if (i < (ds.Tables[0].Rows.Count - 1))
                    {
                        data += lsplitter;
                    }
                }


                engine.close();

                Response.Clear();
                Response.Write(data);
                Response.End();

            }
        }
        else
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
        }
    }
}
