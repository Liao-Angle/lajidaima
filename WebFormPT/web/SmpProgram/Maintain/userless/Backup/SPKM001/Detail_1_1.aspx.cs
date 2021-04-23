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

public partial class SmpProgram_Maintain_SPKM001_Detail_1_1 : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        MajorTypeAdmUserGUID.clientEngineType = (string)Session["engineType"];
        MajorTypeAdmUserGUID.connectDBString = (string)Session["connectString"];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        //MajorTypeAdmUserGUID.ValueText = objects.getData("id");
        //userName.ValueText = objects.getData("userName");

        MajorTypeAdmUserGUID.Display = true;
        MajorTypeAdmUserGUID.GuidValueText = objects.getData("MajorTypeAdmUserGUID");
        MajorTypeAdmUserGUID.doGUIDValidate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("MajorTypeGUID", "temp");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("MajorTypeAdmUserGUID", MajorTypeAdmUserGUID.GuidValueText);
        objects.setData("id", MajorTypeAdmUserGUID.ValueText);
        objects.setData("userName", MajorTypeAdmUserGUID.ReadOnlyValueText);

        //objects.setData("userName", userName.ValueText);
    }


}