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
using WebServerProject.maintain.SMVB;

public partial class SampleProgram_Input : BaseWebUI.DataListInlineForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                LeaderGUID.clientEngineType = (string)Session["engineType"];
                LeaderGUID.connectDBString = (string)Session["connectString"];
            }
        }
    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");

        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.Sample.ProjectDetailTEST");
            detail.setTableName("ProjectDetailTEST");
            detail.loadFileSchema();

            objects.setChild("ProjectDetailTEST", detail);
        }
        else
        {
            detail = objects.getChild("ProjectDetailTEST");
        }

        ProjectID.ValueText = objects.getData("ProjectID");
        ProjectName.ValueText = objects.getData("ProjectName");
        LeaderGUID.GuidValueText = objects.getData("LeaderGUID");
        LeaderGUID.doGUIDValidate();

        ProjectDetailList.dataSource = detail;
        ProjectDetailList.InputForm = "Detail.aspx";
        ProjectDetailList.HiddenField = new string[] { "GUID", "ProjectGUID","IS_LOCK","IS_DISPLAY","DATA_STATUS" };
        ProjectDetailList.updateTable();
    }

    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("ProjectID", ProjectID.ValueText);
        objects.setData("ProjectName", ProjectName.ValueText);
        objects.setData("LeaderGUID", LeaderGUID.GuidValueText);
        objects.setData("id", LeaderGUID.ValueText);
        objects.setData("userName", LeaderGUID.ReadOnlyValueText);

        DataObjectSet detail = ProjectDetailList.dataSource;
        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail.getAvailableDataObject(i);
            dt.setData("ProjectGUID", objects.getData("GUID"));
        }

    }
}
