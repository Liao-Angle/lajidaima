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
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using WebServerProject.system.login;
using com.dsc.kernal.logon;
using com.dsc.kernal.agent;

public partial class SmpProgram_Maintain_SPIT001_Maintain : BaseWebUI.GeneralWebPage
{
	//private string connectString = "";
	//private string engineType = "";
	
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPIT001";
        ApplicationID = "SMPFORM";
        ModuleID = "SPIT";

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
		try
		{
			OnlineUser.OnlineUsers olu = new OnlineUser.OnlineUsers();
			
			lblCurrentUser.Text = olu.print();
		}catch (Exception ze)
		{
		    MessageBox(ze.Message);
		    writeLog(ze);
		}
		
    }
	
    
	
}
