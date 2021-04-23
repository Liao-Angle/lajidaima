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
using com.dsc.kernal.utility;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        if (connectString != null && !connectString.Equals(""))
        {
            com.dsc.kernal.factory.IOFactory factory = new com.dsc.kernal.factory.IOFactory();
            com.dsc.kernal.factory.AbstractEngine engine = factory.getEngine(engineType, connectString);

            string sql = "select LOGOUTTIME from LOGINDATA where SESSIONID='" + Utility.filter(Request.QueryString["SessionID"]) + "'";
            System.Data.DataSet ds = engine.getDataSet(sql, "TEMP");
            if ((ds.Tables[0].Rows.Count > 0) && (ds.Tables[0].Rows[0][0].ToString().Equals("")))
            {
                sql = "update LOGINDATA set LOGOUTTIME='" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null) + "' where SESSIONID='" + Utility.filter(Session.SessionID) + "'";
                engine.executeSQL(sql);
            }
            engine.close();
        }
        Session.RemoveAll();
        Session.Clear();
        Session.Abandon();
    }
}
