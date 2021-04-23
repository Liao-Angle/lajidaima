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
using WebServerProject.audit.SMXC;

public partial class Program_DSCAuditService_Maintain_SMXC_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMXC";
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


            SMXCAAA obj = (SMXCAAA)objects;
            SMXCAAA002.ValueText = obj.SMXCAAA002;
            SMXCAAA003.ValueText = obj.SMXCAAA003;
            SMXCAAA004.ValueText = obj.SMXCAAA004;
            SMXCAAA005.ValueText = obj.SMXCAAA005;
            SMXCAAA006.ValueText = obj.SMXCAAA006;
            SMXCAAA007.ValueText = obj.SMXCAAA007;
            SMXCAAA008.ValueText = obj.SMXCAAA008;
            SMXCAAA009.ValueText = obj.SMXCAAA009;
            SMXCAAA010.ValueText = obj.SMXCAAA010;
            SMXCAAA011.ValueText = obj.SMXCAAA011;
            SMXCAAA012.ValueText = obj.SMXCAAA012;
            SMXCAAA013.ValueText = obj.SMXCAAA013;
            SMXCAAA014.ValueText = obj.SMXCAAA014;
            SMXCAAA015.ValueText = obj.SMXCAAA015;

        }
        catch (Exception ze)
        {
            Response.Write("alert('"+ze.Message+"');");
        }
    }

}
