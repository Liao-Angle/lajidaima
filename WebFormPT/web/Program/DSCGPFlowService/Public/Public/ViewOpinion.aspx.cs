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
using com.dsc.kernal.databean;
using com.dsc.kernal.agent;
using com.dsc.kernal.utility;
using com.dsc.flow.data;
using com.dsc.flow.server;
using System.Data;
using WebServerProject;
using WebServerProject.auth;

public partial class Program_DSCGPFlowService_Public_ViewOpinion : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string imageURL = Request.QueryString["ImageURL"];
            string processSerialNumber = Request.QueryString["processSerialNumber"];
            string opinionType = Request.QueryString["opinionType"];
            string sourceURL = Request.QueryString["SourceURL"];
            string PGID = Request.QueryString["PGID"];
            string isUseHTML = Request.QueryString["isUseHTML"];

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            
            FlowImage.ImageUrl = imageURL;

            if (imageURL.Equals(""))
            {
                FlowImage.Visible = false;
                string sss = "<script language=javascript>";
                sss += "document.getElementById('acStatus').style.display='none';";
                sss += "</script>";

                Type ctype = this.GetType();
                ClientScriptManager cm = Page.ClientScript;

                if (!cm.IsStartupScriptRegistered(ctype, "GeneralWebFormScript"))
                {
                    cm.RegisterStartupScript(ctype, "GeneralWebFormScript", sss);
                }
            }
            if ((isUseHTML != null) && (isUseHTML.Equals("Y")))
            {
                string htmlg = "";
                htmlg = (string)base.getSession(PGID, "GraphHTML");
                FlowChartContent.Text = htmlg;
            }
            /*
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            SysParam sp = new SysParam(engine);
            string siteName = sp.getParam("SiteName");
            engine.close();

            string param = "GetSignOpinion=1&FlowGUID=" + processSerialNumber + "&OpinionType=" + opinionType+"&EFLogonID="+(string)Session["UserID"];
            string html = HTTPProcessor.sendGet(com.dsc.kernal.utility.GlobalProperty.getProperty("global", "httpHead") + "://" + siteName + sourceURL, param);
            */
            string html = "";
            html = (string)base.getSession(PGID, "OpinionHTML");

            OpinionList.Text = html;

            //*********************************************************************
            //註冊切換轉派意見的Script
            //*********************************************************************
            string sstr = "<script language=javascript>";
            sstr += "function toogleOpinionDetail(workitempanel){";
            sstr += "   imgobj=event.srcElement;";
            //sstr += "   workitemobj=eval('document.all.'+workitempanel);";
            sstr += "     workitemobj=eval(\"document.getElementById(\''+workitempanel + '\')\" );";
            sstr += "   if(workitemobj.style.display=='none'){";
            sstr += "       workitemobj.style.display='';";
            sstr += "       imgobj.src='" + Page.ResolveClientUrl("~/DSCWebControlRunTime/DSCWebControlImages/o.gif") + "';";
            sstr += "   }else{";
            sstr += "       workitemobj.style.display='none';";
            sstr += "       imgobj.src='" + Page.ResolveClientUrl("~/DSCWebControlRunTime/DSCWebControlImages/c.gif") + "';";
            sstr += "   }";
            sstr += "}";
            sstr += "</script>";

            ClientScriptManager cm2 = Page.ClientScript;
            Type ctype2 = Page.GetType();
            cm2.RegisterStartupScript(ctype2, "SignOpinionScript", sstr);                        
            
        }   
    }

    protected override void  OnLoadComplete(EventArgs e)
    {
 	    base.OnLoadComplete(e);
        genericactivity.ToolTip = DSCLabel2.Text;
        running.ToolTip = DSCLabel3.Text;
        complete.ToolTip = DSCLabel4.Text;
        terminated.ToolTip = DSCLabel5.Text;
        suspend.ToolTip = DSCLabel6.Text;
    }     
}           
