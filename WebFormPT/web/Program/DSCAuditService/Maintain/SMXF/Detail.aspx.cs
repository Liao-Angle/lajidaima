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
using WebServerProject.audit.SMXF;

public partial class Program_DSCAuditService_Maintain_SMXF_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMXF";
        ApplicationID = "SYSTEM";
        ModuleID = "SMXAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        SaveButton.Visible = false;
        ResetButton.Visible = false;
        try
        {
            connectString = (string)Session["connectString"];
            engineType = (string)Session["engineType"];


            SMXFAAA obj = (SMXFAAA)objects;
            SMXDAAB003.ValueText = obj.SMXDAAB003;
            SMXDAAB004.ValueText = obj.SMXDAAB004;
            SMXDAAB005.ValueText = obj.SMXDAAB005;
            SMXDAAB006.ValueText = obj.SMXDAAB006;
            SMXDAAB007.ValueText = obj.SMXDAAB007;
            SMXDAAA002.ValueText = obj.SMXDAAA002;
            SMXDAAA003.ValueText = obj.SMXDAAA003;

        }
        catch (Exception ze)
        {
            Response.Write("alert('"+ze.Message+"');");
        }
    }

}
