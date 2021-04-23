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

public partial class Program_System_Form_APMONEY_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "APMONEY";
        ApplicationID = "SYSTEM";
        ModuleID = "SAMPLE";
    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        APDETAIL002.ValueText = objects.getData("APDETAIL002");
        APDETAIL003.ValueText = objects.getData("APDETAIL003");
        GlassButtonDownload.ReadOnly = false;
    }

    protected override void saveData(DataObject objects)
    {
        
        try
        {
            decimal test = decimal.Parse(APDETAIL003.ValueText);
            if (test <= 0)
            {
                throw new Exception("");
            }
        }
        catch
        {
            throw new Exception("金額必須大於0");
        }
        
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("APDETAIL001", "TEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("APDETAIL002", APDETAIL002.ValueText);
        objects.setData("APDETAIL003", APDETAIL003.ValueText);
    }

    protected override void afterSetReadOnlyForm(DataObject objects)
    {
        //APDETAIL002.ReadOnly = false;
        GlassButtonDownload.ReadOnly = false;
    }
}
