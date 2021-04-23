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

public partial class ProjectBaseWebUI_WebiReportNormal : System.Web.UI.Page
{
    public string reportID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            reportID = Request.QueryString["ReportID"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;

            try
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                engine = factory.getEngine(engineType, connectString);

                string url = WebiURL.composeWebiURL(engine, (string)Session["UserID"], (string)Session["UserPWD"], (string[])Session["UserGroup"], reportID, "");

                engine.close();

                if (Session["isLogonWebi"] == null)
                {
                    url += "&sWindow=New";
                    Session["IsLogonWebi"] = true;
                }
                else
                {
                    url += "&sWindow=Same";
                }
                //Response.Write("<script language=javascript>");
                //Response.Write("window.open('" + url + "','_blank','height=600,width=800,status=no,toolbar=no,menubar=no,location=no,top=0,left=0,resizable=1');");
                //Response.Write("try{");
                //Response.Write("window.parent.Panel_Close_Silence('" + Request.QueryString["CurPanelID"] + "');"); //這行可以直接關閉目前視窗
                //Response.Write("}catch(e){};");
                //Response.Write("</script>");
                Response.Redirect(url);
            }
            catch (Exception te)
            {
                try
                {
                    engine.close();
                }
                catch { };
                Response.Write(te.StackTrace + "<br>" + te.Message);
            }
        }
    }
}
