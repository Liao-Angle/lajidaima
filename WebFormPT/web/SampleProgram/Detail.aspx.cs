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

public partial class SampleProgram_Detail : BaseWebUI.DataListInlineForm
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        StartDate.ValueText = objects.getData("StartDate");
        EndDate.ValueText = objects.getData("EndDate");

    }
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("ProjectGUID","temp");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("StartDate", StartDate.ValueText);
        objects.setData("EndDate", EndDate.ValueText);
    }
}
