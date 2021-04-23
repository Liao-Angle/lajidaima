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

public partial class MainFrame_getMainFrameJS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Method = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["Method"]);
            if (Method == null)
            {
                Method = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["Method"]);
            }

            if (Method != null)
            {
                if (Method.Equals("GetProgramID"))
                {
                    string connectString = (string)Session["connectString"];
                    string engineType = (string)Session["engineType"];

                    com.dsc.kernal.factory.IOFactory factory = new com.dsc.kernal.factory.IOFactory();
                    com.dsc.kernal.factory.AbstractEngine engine = factory.getEngine(engineType, connectString);

                    string sql = "select SMVAAAB002 from SMVAAAB where SMVAAAB004 like '%" + com.dsc.kernal.utility.Utility.filter(Request.Form["SV"]) + "'";

                    string res = (string)engine.executeScalar(sql);
                    engine.close();

                    Response.Clear();
                    Response.Write(res);
                    Response.End();
                }
            }
        }
    }
}
