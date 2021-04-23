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

public partial class Program_DSCAuthService_Maintain_SMSD_GetFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=Data.xml");

        string fn = Request.QueryString["FID"];
        Response.WriteFile(fn);
        //System.IO.File.Delete(fn);
    }
}
