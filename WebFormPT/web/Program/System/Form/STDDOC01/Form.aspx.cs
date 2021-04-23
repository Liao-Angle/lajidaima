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
using com.dsc.flow.data;

public partial class Program_System_Form_STDDOC01_Form : BasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "STDDOC01";
        AgentSchema = "WebServerProject.form.STDDOC01.STDDOC01Agent";
        ApplicationID = "SYSTEM";
        ModuleID = "SAMPLE";
    }
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        //MessageBox((string)getSession("UIStatus"));

    }
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        STDDOC01001.ValueText = objects.getData("STDDOC01001");
    }
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("STDDOC01001", STDDOC01001.ValueText);
    }
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result=true;
        if (isNecessary(STDDOC01001))
        {
            if (STDDOC01001.ValueText.Equals(""))
            {
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc01_form_aspx.language.ini", "message", "QueryError", "必須填寫會辦意見"));
                result = false;
            }
        }


        return result;
    }
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID);
        return hs;
    }

    //protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    //{
    //    string actName = (string)getSession("ACTName");

    //    if (actName.Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc01_form_aspx.language.ini", "message", "SIR", "直屬主管")))
    //    {
    //        if (STDDOC01001.ValueText.Equals("N"))
    //        {
    //            param["OpinionStr"] = "N";
    //        }
    //        else
    //        {
    //            param["OpinionStr"] = "Y";
    //        }
    //        return "OpinionStr";
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}

}
