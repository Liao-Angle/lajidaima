using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using WebServerProject;

public partial class smpEasyFlow_system_ptsEFcreate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //string dfgs = Utility.CheckCrossSiteScripting(Request.QueryString["ObjectGUID"].ToString());
        //string PPUID = Utility.CheckCrossSiteScripting(Request.QueryString["ParentPageUID"].ToString());
        string progid = Utility.CheckCrossSiteScripting(Convert.ToString(Request.QueryString["progid"]));
        string sql = "";

        string url = "";
        string ef2kwebsite = "";
        string ef2kwebdomain = "";
        string rrid = "";
        string sp7str = "";

        SysParam sp = new SysParam(engine);
        ef2kwebsite = sp.getParam("EF2KWebSite");
        ef2kwebdomain = sp.getParam("EF2KWebDomain");
        sp7str = sp.getParam("EF2KWebDB");
        AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7str);
        rrid = mappingUserID(engine, sp7engine);
        string efprogid ="";
        switch (progid)
        {
            case "ruku" :
                efprogid = progid + "/" + progid + "_Create_Init.asp";
                break;

            /*在這邊補上其他需要在ECP裡面開啟EasyFlow的 開單URL*/

            default :
                break;
        }

        string url2 = ef2kwebsite + "CHT/Forms/" + efprogid + "?ParentPanelID=" + Utility.CheckCrossSiteScripting(Request.QueryString["ParentPanelID"]) + "&CurPanelID=" + Utility.CheckCrossSiteScripting(Request.QueryString["CurPanelID"]) + "&rrid=" + ef2kwebdomain + "\\" + rrid + "&FromECP=1";

        //showPanelWindow("TEST", url2, 0, 0, "", true, false);
        Response.Redirect(url2);




    }

    private string mappingUserID(AbstractEngine engine, AbstractEngine sp7engine)
    {
        string ret = "";
        ret = Convert.ToString(sp7engine.executeScalar("select resak004 from resak with(nolock) where resak001='" + Convert.ToString(Session["UserID"]) + "'"));
        if (ret != "")
        {
            return ret;
        }
        else
        {
            return Convert.ToString(Session["UserID"]);
        }
    }


}
