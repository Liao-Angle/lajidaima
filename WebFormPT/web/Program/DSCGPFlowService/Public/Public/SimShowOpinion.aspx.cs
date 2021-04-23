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

public partial class Program_DSCGPFlowService_Public_SimShowOpinion : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                if (!((bool)Session["signCheckResult"]))
                {
                    Response.Write("<script language=javascript>");
                    Response.Write("window.top.close();");
                    Response.Write("</script>");
                }

            }
        }
    }
    protected void SendButton_Click(object sender, EventArgs e)
    {
        Response.Write("window.top.returnValue='YES';");
        Response.Write("window.top.close();");
    }
    protected void StopButton_Click(object sender, EventArgs e)
    {
        Response.Write("window.top.close();");
    }
}
