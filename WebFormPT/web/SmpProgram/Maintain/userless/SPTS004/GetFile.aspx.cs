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

public partial class SmpProgram_Maintain_SPTS004_GetFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=ErrLog.log");

        string fn = Request.QueryString["FID"];
        Response.WriteFile(fn);
        //System.IO.File.Delete(fn);
    }
}
