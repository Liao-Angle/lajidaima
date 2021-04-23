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

public partial class DSCWebControlRunTime_DSCWebControlUI_DSCRichEdit_DSCRichEditViewSource : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string ParentPID = Request.QueryString["ParentPageUID"];
                string ParentSource = Request.QueryString["ParentSource"];

                string sessionName = ParentPID + "_" + ParentSource + "_OriValueText";
                string oriValueText = (string)Session[sessionName];

                SourceContent.ValueText = oriValueText;
            }
        }
    }
}
