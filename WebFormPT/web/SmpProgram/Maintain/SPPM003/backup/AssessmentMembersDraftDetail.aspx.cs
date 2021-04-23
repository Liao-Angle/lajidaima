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
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM003_AssessmentMembersDraftDetail : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        UserGUID.connectDBString = connectString;
        UserGUID.clientEngineType = engineType;

        deptOID.connectDBString = connectString;
        deptOID.clientEngineType = engineType;

        deptManagerNameGUID.connectDBString = connectString;
        deptManagerNameGUID.clientEngineType = engineType;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        orgName.ValueText = objects.getData("orgName");
        UserGUID.GuidValueText = objects.getData("UserGUID");
        UserGUID.ValueText = objects.getData("empNumber");
        UserGUID.ReadOnlyValueText = objects.getData("empName");
        deptOID.ValueText = objects.getData("deptId");
        deptOID.ReadOnlyValueText = objects.getData("deptName");
        titleName.ValueText = objects.getData("titleName");
        functionName.ValueText = objects.getData("functionName");
        deptManagerNameGUID.ValueText = objects.getData("deptManagerId");
        deptManagerNameGUID.ReadOnlyValueText = objects.getData("deptManagerName");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        //string assessmentPlanGUID = (string)getSession("AssessmentPlanGUID", "AssessmentPlanGUID");
        string assessmentPlanGUID = "AssessmentPlanGUID";
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("GUID", IDProcessor.getID(""));
        objects.setData("AssessmentPlanGUID", assessmentPlanGUID);
        objects.setData("orgName", orgName.ValueText);
        objects.setData("UserGUID", UserGUID.GuidValueText);
        objects.setData("empNumber", UserGUID.ValueText);
        objects.setData("empName", UserGUID.ReadOnlyValueText);
        objects.setData("deptOID", deptOID.GuidValueText);
        objects.setData("deptId", deptOID.ValueText);
        objects.setData("deptName", deptOID.ReadOnlyValueText);
        objects.setData("titleName", titleName.ValueText);
        objects.setData("functionName", functionName.ValueText);
        objects.setData("deptManagerId", deptManagerNameGUID.ValueText);
        objects.setData("deptManagerName", deptManagerNameGUID.ReadOnlyValueText);

    }

    protected void UserGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string[] result = SmpPmMaintainUtil.getUserInfoById(engine, values[0, 1]);
        orgName.ValueText = result[8];
        titleName.ValueText = result[3];
        deptOID.ValueText = result[6];
        deptOID.ReadOnlyValueText = result[7];
        functionName.ValueText = result[11];
        deptManagerNameGUID.ValueText = result[9];
        deptManagerNameGUID.ReadOnlyValueText = result[10];
        engine.close();
    }
}
