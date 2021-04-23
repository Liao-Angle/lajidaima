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
using System.Collections.Specialized;
using System.EnterpriseServices;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class SmpProgram_Maintain_SPIT005_Maintain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        OnlineUser.OnlineUsers olu = new OnlineUser.OnlineUsers();
        Response.Write(olu.print());
    }
}
