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

public partial class Program_System_Maintain_STDNUM_Form : BasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "STDNUM";
        AgentSchema = "WebServerProject.maintain.STDNUM.STDNUMAgent";
        ApplicationID = "SYSTEM";
        ModuleID = "SAMPLE";
    }
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];



        STDNUM002.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
        STDNUM003.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);


    }
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        STDNUM001.ValueText = objects.getData("STDNUM001");
        STDNUM002.ValueText = objects.getData("STDNUM002");
        STDNUM003.ValueText = objects.getData("STDNUM003");

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
        objects.setData("STDNUM001", STDNUM001.ValueText);
        objects.setData("STDNUM002", STDNUM002.ValueText);
        objects.setData("STDNUM003", STDNUM003.ValueText);
    }
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result=true;
        if (isNecessary(STDNUM001))
        {
            if (STDNUM001.ValueText.Equals(""))
            {
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_stdnum_form_aspx.language.ini", "message", "QueryError1", "必須填寫歸檔號編號"));
                result = false;
            }
        }
        if (isNecessary(STDNUM002))
        {
            if (STDNUM002.ValueText.Equals(""))
            {
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_stdnum_form_aspx.language.ini", "message", "QueryError2", "必須填寫有效期間(起)"));
                result = false;
            }
        }
        if (isNecessary(STDNUM003))
        {
            if (STDNUM003.ValueText.Equals(""))
            {
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_stdnum_form_aspx.language.ini", "message", "QueryError3", "必須填寫有效期間(迄)"));
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

}
