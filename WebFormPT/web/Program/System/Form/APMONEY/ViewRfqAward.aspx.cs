using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.databean;

public partial class Program_System_Form_APMONEY_ViewRfqAward : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string pageUniueId = Request.QueryString["PageUniqueID"];
        DataObject objects = (DataObject)getSession(pageUniueId, "objects");
        string htmlContent = objects.getData("HTMLCODE");
        if (!htmlContent.Equals(""))
        {
            HtmlContentExtTag.InnerHtml = htmlContent;
        }
    }
}