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

public partial class SmpProgram_Maintain_SPPM003_AssessmentManagerDetail : BaseWebUI.DataListInlineForm
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

        SelfEvaluateUserGUID.connectDBString = connectString;
        SelfEvaluateUserGUID.clientEngineType = engineType;

        FirstAssessUserGUID.connectDBString = connectString;
        FirstAssessUserGUID.clientEngineType = engineType;

        SecondAssessUserGUID.connectDBString = connectString;
        SecondAssessUserGUID.clientEngineType = engineType;
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

        SelfEvaluateUserGUID.GuidValueText = objects.getData("SelfEvaluateUserGUID");
        SelfEvaluateUserGUID.ReadOnlyValueText = objects.getData("selfEvaluateUserName");
        if (!string.IsNullOrEmpty(SelfEvaluateUserGUID.GuidValueText))
        {
            SelfEvaluateUserGUID.doGUIDValidate();
        }

        FirstAssessUserGUID.GuidValueText = objects.getData("FirstAssessUserGUID");
        if (!string.IsNullOrEmpty(FirstAssessUserGUID.GuidValueText))
        {
            FirstAssessUserGUID.doGUIDValidate();
        }
        SecondAssessUserGUID.GuidValueText = objects.getData("SecondAssessUserGUID");
        if (!string.IsNullOrEmpty(SecondAssessUserGUID.GuidValueText))
        {
            SecondAssessUserGUID.doGUIDValidate();
        }
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
        objects.setData("UserGUID", UserGUID.GuidValueText);
        objects.setData("orgName", orgName.ValueText);
        objects.setData("empNumber", UserGUID.ValueText);
        objects.setData("empName", UserGUID.ReadOnlyValueText);
        objects.setData("deptOID", deptOID.GuidValueText);
        objects.setData("deptId", deptOID.ValueText);
        objects.setData("deptName", deptOID.ReadOnlyValueText);
        objects.setData("titleName", titleName.ValueText);
        objects.setData("functionName", functionName.ValueText);
        objects.setData("SelfEvaluateUserGUID", SelfEvaluateUserGUID.GuidValueText);
        objects.setData("selfEvaluateUserName", SelfEvaluateUserGUID.ReadOnlyValueText);
        objects.setData("FirstAssessUserGUID", FirstAssessUserGUID.GuidValueText);
        objects.setData("firstAssessManagerName", FirstAssessUserGUID.ReadOnlyValueText);
        objects.setData("SecondAssessUserGUID", SecondAssessUserGUID.GuidValueText);
        objects.setData("secondAssessManagerName", SecondAssessUserGUID.ReadOnlyValueText);
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
        engine.close();
    }
}
