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

public partial class SmpProgram_Form_SPKM005_MaintainTree : BaseWebUI.GeneralWebPage
{
    private string lsplitter = "#!#!#";
    private string splitter = "#*#*#";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = false;
        string method = Request.Form["Method"];
        if ((method != null) && (!method.Equals("")))
        {
            if (method.Equals("GetMajorType"))  // MajorType
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                string sql = "";
                string data = "";
                sql = "select a.GUID as OID, a.Name as MajorName, a.Enable from SmpMajorType a";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    data += ds.Tables[0].Rows[i][0].ToString() + splitter;
                    data += ds.Tables[0].Rows[i][1].ToString() + splitter;
                    if (ds.Tables[0].Rows[i][2].ToString() == "N")
                        data += "(失效)";
                    else
                        data += "";
                    if (i < (ds.Tables[0].Rows.Count - 1))
                        data += lsplitter;
                }
                engine.close();
                Response.Clear();
                Response.Write(data);
                Response.End();
            }
            else if (method.Equals("GetSubType"))  // SubType
            {
                //string orgID = Request.Form["O"];
                string superID = Request.Form["P"];
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                string sql = "";
                string data = "";
                sql = "select a.GUID as OID, a.Name as SubName, a.Enable from SmpSubType a where ";
                if (superID.Equals(""))
                {
                    sql += " MajorTypeGUID is null";
                }
                else
                {
                    sql += " MajorTypeGUID='" + Utility.filter(superID) + "'";
                }
                DataSet ds = engine.getDataSet(sql, "TEMP");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    data += ds.Tables[0].Rows[i][0].ToString() + splitter;
                    data += ds.Tables[0].Rows[i][1].ToString() + splitter;
                    if (ds.Tables[0].Rows[i][2].ToString() == "N")
                        data += "(失效)";
                    else
                        data += "";
                    if (i < (ds.Tables[0].Rows.Count - 1))
                        data += lsplitter;
                }
                engine.close();
                Response.Clear();
                Response.Write(data);
                Response.End();
            }
            else if (method.Equals("GetDocType"))  // DocType
            {
                //string orgID = Request.Form["O"];
                string superID = Request.Form["P"];
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                string sql = "";
                string data = "";
                sql = "select a.GUID as OID, a.Name as DocName, a.Enable from SmpDocType a where ";
                if (superID.Equals(""))
                {
                    sql += " SubTypeGUID is null";
                }
                else
                {
                    sql += " SubTypeGUID='" + Utility.filter(superID) + "'";
                }
                DataSet ds = engine.getDataSet(sql, "TEMP");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    data += ds.Tables[0].Rows[i][0].ToString() + splitter;
                    data += ds.Tables[0].Rows[i][1].ToString() + splitter;
                    if (ds.Tables[0].Rows[i][2].ToString() == "N")
                        data += "(失效)";
                    else
                        data += "";
                    if (i < (ds.Tables[0].Rows.Count - 1))
                        data += lsplitter;
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
            int auth = authagent.getAuth("SPKM005", (string)Session["UserID"], (string[])Session["usergroup"]);
            engine.close();
            string mstr = "";
            if (auth == 0)
            {
                Response.Redirect("~/NoAuth.aspx");
            }
        }
    }
}
