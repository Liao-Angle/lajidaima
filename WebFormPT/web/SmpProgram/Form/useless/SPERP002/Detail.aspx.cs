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
using com.dsc.kernal.agent;


public partial class SmpProgram_System_Form_SPERP002_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SmpExpenseBill";
        ApplicationID = "SMPFORM";
        ModuleID = "SPERP";
    }


    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];
        
        PoType.ValueText = objects.getData("PoType");
        PoCategory.ValueText = objects.getData("PoCategory");
        OriginatorGUID.ValueText = objects.getData("OriginatorGUID");
        //OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        //OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID

        ItemSpec.ValueText = objects.getData("ItemSpec");
        Remark.ValueText = objects.getData("Remark");
        Quantity.ValueText = objects.getData("Quantity");
        PriceUnit.ValueText = objects.getData("PriceUnit");
        Amount.ValueText = objects.getData("Amount");
        DueDate.ValueText = objects.getData("DueDate");
        SourceId.Value = objects.getData("SourceId");
        OriginatorGUID.ReadOnly = true;
    }

    protected override void saveData(DataObject objects)
    {               
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("PoNumberGUID", "TEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }

        objects.setData("PoType", PoType.ValueText);
        objects.setData("PoCategory", PoCategory.ValueText);
        objects.setData("OriginatorGUID", OriginatorGUID.ValueText);
        //objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
        objects.setData("ItemSpec", ItemSpec.ValueText);
        objects.setData("Remark", Remark.ValueText);
        objects.setData("Quantity", Quantity.ValueText);
        objects.setData("PriceUnit", PriceUnit.ValueText);
        objects.setData("Amount", Amount.ValueText);
        objects.setData("DueDate", DueDate.ValueText);
        objects.setData("SourceId", SourceId.Value);
        
    }
    protected override void afterSetReadOnlyForm(DataObject objects)
    {
        //OriginatorGUID.ReadOnly = false;
    }


}
