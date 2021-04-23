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

public partial class Program_System_Form_STD002_Form : BasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "STD002";
        AgentSchema = "WebServerProject.form.STD002.STD002Agent";
        ApplicationID = "SYSTEM";
        ModuleID = "SAMPLE";
    }
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        STD002001.clientEngineType = engineType;
        STD002001.connectDBString = connectString;
        STD002004.clientEngineType = engineType;
        STD002004.connectDBString = connectString;
        STD002004.DoEventWhenNoKeyIn = false;

        string[,] ids = null;

        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_std002_form_aspx.language", "message", "ids0", "特休")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_std002_form_aspx.language", "message", "ids1", "病假")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_std002_form_aspx.language", "message", "ids2", "出差")}
        };
        STD002005.setListItem(ids);

        STD002001.ValueText = si.fillerID;
        STD002001.doValidate();

    }
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        STD002001.GuidValueText = objects.getData("STD002001");
        STD002001.doGUIDValidate();
        STD002004.GuidValueText = objects.getData("STD002004");
        STD002004.doGUIDValidate();
        STD002005.ValueText = objects.getData("STD002005");
        STD002006.ValueText = objects.getData("STD002006");
        STD002007.ValueText = objects.getData("STD002007");
        STD002008.ValueText = objects.getData("STD002008");
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
        objects.setData("STD002001", STD002001.GuidValueText);
        objects.setData("STD002002", STD002001.ValueText);
        objects.setData("STD002003", STD002001.ReadOnlyValueText);
        objects.setData("STD002004", STD002004.GuidValueText);
        objects.setData("STD002005", STD002005.ValueText);
        objects.setData("STD002006", STD002006.ValueText);
        objects.setData("STD002007", STD002007.ValueText);
        objects.setData("STD002008", STD002008.ValueText);

    }
    
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result=true;
        if (isNecessary(STD002004))
        {
            if (STD002004.GuidValueText.Equals(""))
            {
                //pushErrorMessage("必須選擇代理人");
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_std002_form_aspx.language", "message", "QueryError1", "必須選擇代理人"));
                result = false;
            }
        }
        if (isNecessary(STD002006))
        {
            if (STD002006.ValueText.Equals(""))
            {
                //pushErrorMessage("必須選擇請假起始");
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_std002_form_aspx.language", "message", "QueryError2", "必須選擇請假起始"));
                result = false;
            }
        }
        if (isNecessary(STD002007))
        {
            if (STD002007.ValueText.Equals(""))
            {
                //pushErrorMessage("必須選擇請假截止");
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_std002_form_aspx.language", "message", "QueryError3", "必須選擇請假截止"));
                result = false;
            }
        }
        if (isNecessary(STD002008))
        {
            if (STD002008.ValueText.Equals(""))
            {
                //pushErrorMessage("必須填寫說明");
                pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("program_system_form_std002_form_aspx.language", "message", "QueryError4", "必須填寫說明"));
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
        //取得BG直屬主管
        string str = "<Boss>";
        str += "<BossID dataType=\"java.lang.String\">0278</BossID>";
        str += "</Boss>";

        string actName = (string)getSession("ACTName");
        if (actName.Equals(""))
        {
            param["Boss"] = str;
            return "Boss";
        }
        else
        {
            return "";
        }
    }

    protected void STD002004_SingleOpenWindowButtonClick(string[,] values)
    {
        if (values == null)
            MessageBox("test");
    }

}
