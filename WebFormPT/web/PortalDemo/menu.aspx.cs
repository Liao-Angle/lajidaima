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

public partial class PortalDemo_menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string SSOToken = com.dsc.kernal.utility.IDProcessor.getID("");
            string folder = Server.MapPath("~/tempFolder/" + SSOToken);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(folder, false);
            sw.WriteLine("OK");
            sw.Close();

            HyperLink1.NavigateUrl = "http://127.0.0.1/webformpt/?runMethod=executeProgram&programID=DefaultInbox";
            HyperLink2.NavigateUrl = "http://127.0.0.1/webformpt/?runMethod=executeProgram&programID=DefaultInbox&SSOToken=" + SSOToken + "&UserID=2902&VerifySite=PortalDemo";
            HyperLink3.NavigateUrl = "http://127.0.0.1/webformpt/?runMethod=executeProgram&programID=DefaultInbox&CloseTitle=1&CloseToolBar=1&CloseSetting=1";
            HyperLink4.NavigateUrl = "http://127.0.0.1/webformpt/?runMethod=executeProgram&programID=DefaultInbox&CloseTitle=1&CloseToolBar=1&CloseSetting=1&SSOToken=" + SSOToken + "&UserID=2902&VerifySite=PortalDemo";
        }
    }
}
