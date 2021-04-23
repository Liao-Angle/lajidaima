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


public partial class Program_System_Form_STDDOCTEST_STDDOCTEST : BasicFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\xxx\a.txt", true, System.Text.Encoding.Default);
        //sw.WriteLine("xxxx");
        //sw.Close();
    }

    protected override void init()
    {
        ProcessPageID = "STDDOCTEST";
        AgentSchema = "WebServerProject.form.STDDOCTEST.STDDOCTESTAgent";
        ApplicationID = "SYSTEM";
        ModuleID = "SAMPLE";
    }

    protected override void initUI(AbstractEngine engine, DataObject objects)
    {

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        STDDOC001.clientEngineType = engineType;
        STDDOC001.connectDBString = connectString;


        string[,] ids = null;

        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "ids0", "低")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "ids1", "普通")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "ids2", "高")}
        };
        STDDOC006.setListItem(ids);
        STDDOC007.setListItem(ids);

        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "ids3", "普通")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "ids4", "個人")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "ids5", "私密")},
            {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "ids6", "機密")}
        };
        STDDOC008.setListItem(ids);

        //STDDOC013.FixedTime = new string[] { "10:00", "12:00" };
        STDDOC013.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
        STDDOC013.AllowEmpty = true;

        STDDOC001.ValueText = si.fillerID;
        STDDOC001.doValidate();
    }
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //string actName = (string)getSession("ACTName");
        //if (actName != "填表人")
        //{
        //    STDDOC009.ReadOnly = true;
        //    STDDOC009.Visible = false;
        //    STDDOC009.Display = false;
        //}
        STDDOC013.ValueText = objects.getData("STDDOC013");
        STDDOC001.GuidValueText = objects.getData("STDDOC001");
        //STDDOC001.doGUIDValidate();
        STDDOC001.ReadOnlyValueText = objects.getData("STDDOC002");

        STDDOC006.ValueText = objects.getData("STDDOC006");
        STDDOC007.ValueText = objects.getData("STDDOC007");
        STDDOC008.ValueText = objects.getData("STDDOC008");

        STDDOC009.ValueText = objects.getData("STDDOC009");
        STDDOC010.ValueText = objects.getData("STDDOC010");
        STDDOC011.ValueText = objects.getData("STDDOC011");

        if (objects.getData("STDDOC012").Equals("Y"))
        {
            STDDOC012.Checked = true;
        }
        else
        {
            STDDOC012.Checked = false;
        }
    }
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("STDDOC004", si.fillerOrgID);
            objects.setData("STDDOC005", si.fillerOrgName);
            objects.setData("STDDOC013", STDDOC013.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("STDDOC001", STDDOC001.GuidValueText);
        objects.setData("STDDOC002", STDDOC001.ValueText);
        objects.setData("STDDOC003", STDDOC001.ReadOnlyValueText);
        objects.setData("STDDOC006", STDDOC006.ValueText);
        objects.setData("STDDOC007", STDDOC007.ValueText);
        objects.setData("STDDOC008", STDDOC008.ValueText);
        objects.setData("STDDOC009", STDDOC009.ValueText);
        objects.setData("STDDOC010", STDDOC010.ValueText);
        objects.setData("STDDOC011", STDDOC011.ValueText);

        if (STDDOC012.Checked)
        {
            objects.setData("STDDOC012", "Y");
        }
        else
        {
            objects.setData("STDDOC012", "N");
        }
    }
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        if (isNecessary(STDDOC001))
        {
            if (STDDOC001.GuidValueText.Equals(""))
            {
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "QueryError1", "必須填寫發文者"));
                result = false;
            }
        }
        if (isNecessary(STDDOC009))
        {
            if (STDDOC009.ValueText.Equals(""))
            {
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "QueryError2", "必須填寫主旨"));
                result = false;
            }
        }
        if (isNecessary(STDDOC010))
        {
            if (STDDOC010.ValueText.Equals(""))
            {
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "QueryError3", "必須填寫辦法"));
                result = false;
            }
        }
        if (isNecessary(STDDOC011))
        {
            if (STDDOC011.ValueText.Equals(""))
            {
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "QueryError4", "必須填寫說明"));
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

    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        #region 縮起來
        //string actName = (string)getSession("ACTName");
        //if (actName.Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_stddoc_form_aspx.language.ini", "message", "SIR", "直屬主管")))
        //{
        //    if (currentObject.getData("STDDOC012").Equals("Y"))
        //    {
        //        param["IsSecretary"] = "Y";
        //    }
        //    else
        //    {
        //        param["IsSecretary"] = "N";
        //    }
        //    param["Boss"] = "<Boss><BossID dataType=\"java.lang.String\">2902</BossID></Boss>";
        //    return "IsSecretary";
        //}
        //else
        //{
        //    param["IsSecretary"] = "Y";
        //    param["Boss"] = "<Boss><BossID dataType=\"java.lang.String\">2902</BossID></Boss>";

        //    return "Boss";
        //    //return "";
        //}
        #endregion

        param["TEST"] = "<TEST>";
        param["TEST"] += "    <TEST01 dataType=\"java.lang.String\">4020</TEST01>";
        param["TEST"] += "    <TEST02 dataType=\"java.lang.Integer\">12345</TEST02>";
        param["TEST"] += "    <TEST03 dataType=\"java.lang.String\">4020</TEST03>";
        param["TEST"] += "    <TEST04 dataType=\"java.lang.String\">4020</TEST04>";
        param["TEST"] += "    <TEST05 dataType=\"java.lang.String\">3992</TEST05>";
        param["TEST"] +="</TEST>";

        return "TEST";


    }


}
