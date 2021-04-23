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
using WebServerProject.audit.SMXD;

public partial class Program_DSCAuditService_Maintain_SMXD_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMXD";
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


            SMXDAAA obj = (SMXDAAA)objects;
            SMXDAAA002.ValueText = obj.SMXDAAA002;
            SMXDAAA003.ValueText = obj.SMXDAAA003;
            SMXDAAA004.ValueText = obj.SMXDAAA004;

            DataObjectSet detail = obj.getChild("SMXDAAB");
            ABList.dataSource = detail;
            ABList.HiddenField = new string[] { "SMXDAAB001", "SMXDAAB002" };
            ABList.updateTable();
        }
        catch (Exception ze)
        {
            Response.Write("alert('"+ze.Message+"');");
        }
    }

}
