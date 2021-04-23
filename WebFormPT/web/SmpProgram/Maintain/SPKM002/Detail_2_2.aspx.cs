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

public partial class SmpProgram_Maintain_SPKM001_Detail_2_2 : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        BelongGroupGUID.clientEngineType = (string)Session["engineType"];
        BelongGroupGUID.connectDBString = (string)Session["connectString"];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        BelongGroupGUID.Display = true;
        BelongGroupGUID.GuidValueText = objects.getData("BelongGroupGUID");
        BelongGroupGUID.doGUIDValidate();
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
            objects.setData("SubTypeGUID", "temp");
            objects.setData("MajorTypeGUID", "temp");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }

        objects.setData("BelongGroupGUID", BelongGroupGUID.GuidValueText);
        objects.setData("id", BelongGroupGUID.ValueText);
        objects.setData("Name", BelongGroupGUID.ReadOnlyValueText);

        BelongGroupGUID.valueIndex = 3;
        BelongGroupGUID.doGUIDValidate();
        objects.setData("KindName", BelongGroupGUID.ReadOnlyValueText);
        BelongGroupGUID.valueIndex = 4;
        BelongGroupGUID.doGUIDValidate();
        objects.setData("BelongGroupType", BelongGroupGUID.ReadOnlyValueText);
    }
}