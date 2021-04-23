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

public partial class Program_System_Report_Webi2423 : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.maintainIdentity = "Webi2423";
        Master.ModuleID = "SYSTEM";
        Master.ApplicationID = "SYSTEM";
        Master.reportID = "2423";

        Master.composeSearchString+=new ProjectBaseWebUI_WebiReportParameter.composeSearchStringEvent(Master_composeSearchString);
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                start();
            }
        }
    }

    protected void start()
    {
        Master.InitData();
    }

    protected string Master_composeSearchString()
    {
        string strs = "&lsSCKey=" + CKey.ValueText;
        return strs;
    }
}
