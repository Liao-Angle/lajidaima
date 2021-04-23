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

public partial class Program_System_Form_SPPO_Form : BasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "SPPO";
        AgentSchema = "WebServerProject.form.SPPO.SPPOAgent";
        ApplicationID = "SYSTEM";
        ModuleID = "SAMPLE";
    }
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        SPPO002.clientEngineType = engineType;
        SPPO002.connectDBString = connectString;
        SPPO003.clientEngineType = engineType;
        SPPO003.connectDBString = connectString;
        SPPO008.clientEngineType = engineType;
        SPPO008.connectDBString = connectString;
        SPPO009.clientEngineType = engineType;
        SPPO009.connectDBString = connectString;

        DSCRadioButton2.Checked = true;
        DSCRadioButton1.Checked = false;
        
        //外部資料初始化??

        SPPO001.ValueText = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
        SPPO002.ValueText = si.fillerID;
        SPPO002.doValidate();
        SPPO003.ValueText = si.fillerOrgID;
        SPPO003.doValidate();

    }
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        SPPO001.ValueText = objects.getData("SPPO001");
        SPPO002.ValueText = objects.getData("SPPO002");
        SPPO002.doValidate();
        SPPO003.ValueText = objects.getData("SPPO003");
        SPPO003.doValidate();
        SPPO004.ValueText = objects.getData("SPPO004");
        SPPO005.ValueText = objects.getData("SPPO005");
        SPPO006.ValueText = objects.getData("SPPO006");
        SPPO007.ValueText = objects.getData("SPPO007");
        SPPO008.ValueText = objects.getData("SPPO008");
        SPPO008.doValidate();
        SPPO009.ValueText = objects.getData("SPPO009");
        SPPO009.doValidate();
        if (objects.getData("SPPO010").Equals("1"))
        {
            DSCRadioButton2.Checked = true;
            DSCRadioButton1.Checked = false;
        }
        else
        {
            DSCRadioButton2.Checked = false;
            DSCRadioButton1.Checked = true;
        }
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
        objects.setData("SPPO001", SPPO001.ValueText);
        objects.setData("SPPO002", SPPO002.ValueText);
        objects.setData("SPPO003", SPPO003.ValueText);
        objects.setData("SPPO004", SPPO004.ValueText);
        objects.setData("SPPO005", SPPO005.ValueText);
        objects.setData("SPPO006", SPPO006.ValueText);
        objects.setData("SPPO007", SPPO007.ValueText);
        objects.setData("SPPO008", SPPO008.ValueText);
        objects.setData("SPPO009", SPPO009.ValueText);
        if (DSCRadioButton2.Checked && !DSCRadioButton1.Checked)
        {
            objects.setData("SPPO010", "1");
        }
        else
        {
            objects.setData("SPPO010", "0");
        }

    }
    
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result=true;
        try
        {
            if (SPPO001.ValueText.Equals(""))
            {
                pushErrorMessage(SPPO001lb.Text + "欄位不可為空");
                result = false;
            }
            if (DSCRadioButton1.Checked && !DSCRadioButton2.Checked)
            {
                if (SPPO009.ValueText.Equals(""))
                {
                    pushErrorMessage(SPPO009lb.Text + "欄位不可為空");
                    result = false;
                }
            }
        }
        catch (Exception ex)
        {
            pushErrorMessage(ex.Message);
            result = false;
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
        string str = "<SPPO>";
        str += "<APP dataType=\"java.lang.String\">" + SPPO002.ValueText + "</APP>";
        str += "<DEM dataType=\"java.lang.String\">" + SPPO002.ValueText + "</DEM>";
        str += "<COU dataType=\"java.lang.String\">" + SPPO009.ValueText + "</COU>";
        str += "<PST dataType=\"java.lang.String\">2902</PST>";//採購人員是???
        if (DSCRadioButton1.Checked && !DSCRadioButton2.Checked)
        {
            str += "<isCOU dataType=\"java.lang.String\">Y</isCOU>";
        }
        else
        {
            str += "<isCOU dataType=\"java.lang.String\">N</isCOU>";
        }
        str += "</SPPO>";

        string actName = (string)getSession("ACTName");

            param["SPPO"] = str;
            return "SPPO";

    }



}
