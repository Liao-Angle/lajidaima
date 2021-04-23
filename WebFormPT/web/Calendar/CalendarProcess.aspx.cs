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
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class Calendar_CalendarProcess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string method = Request.Form["Method"];
        if (method.Equals("DeleteItem"))
        {
            string guid = Request.Form["GUID"];
            AbstractEngine engine = null;
            try
            {

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                string sql = "";
                sql = "delete from SMVGAAA where SMVGAAA001='" + Utility.filter(guid) + "'";
                engine.executeSQL(sql);

                engine.close();
            }
            catch
            {
                try
                {
                    engine.close();
                }
                catch { };
            }
        }
        else if (method.Equals("DeleteAllItem"))
        {
            string dts = Request.Form["Date"];
            AbstractEngine engine = null;
            try
            {

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                string sql = "";
                sql = "delete from SMVGAAA where SMVGAAA002='" + Utility.filter((string)Session["UserGUID"]) + "' and SMVGAAA006='" + Utility.filter(dts) + "'";
                engine.executeSQL(sql);

                engine.close();
            }
            catch
            {
                try
                {
                    engine.close();
                }
                catch { };
            }
        }
    }
}
