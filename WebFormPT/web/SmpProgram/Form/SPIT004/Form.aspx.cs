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

public partial class SmpProgram_Form_SPIT004_Form : SmpBasicFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPIT004";
        AgentSchema = "WebServerProject.form.SPIT004.SmpMaintenanceFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPIT";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        //申請人
        OriginatorGUID.Display = false;
        //叫修人員
        CallUserGUID.clientEngineType = engineType;
        CallUserGUID.connectDBString = connectString;
        //叫修單位
        CallOrgUnitGUID.clientEngineType = engineType;
        CallOrgUnitGUID.connectDBString = connectString;
        CallOrgUnitGUID.ReadOnly = true;
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            CallUserGUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
        }

        //改變工具列順序
        base.initUI(engine, objects);
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //顯示單號
        base.showData(engine, objects);
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //申請人
        OriginatorGUID.ValueText = objects.getData("OriginatorGUID");
        //叫修人員
        CallUserGUID.GuidValueText = objects.getData("CallUserGUID");
        CallUserGUID.doGUIDValidate(); 
        //叫修單位
        CallOrgUnitGUID.GuidValueText = objects.getData("CallOrgUnitGUID");
        CallOrgUnitGUID.doGUIDValidate();
        //分機
        CallExtension.ValueText = objects.getData("CallExtension");
        //叫修時間
        CallDateTime.ValueText = objects.getData("CallDateTime");
        //維修時數
        ProcessingHours.ValueText = objects.getData("ProcessingHours");
        //問題描述
        IssueDescription.ValueText = objects.getData("IssueDescription");
        //處理方法
        ProcessingMethod.ValueText = objects.getData("ProcessingMethod");

        string actName = Convert.ToString(getSession("ACTName"));
        if (actName == "" || actName.Equals("填表人"))
        {

        }
        else
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            CallUserGUID.ReadOnly = true;
            CallOrgUnitGUID.ReadOnly = true;
            CallExtension.ReadOnly = true;
            CallDateTime.ReadOnly = true;
            ProcessingHours.ReadOnly = true;
            IssueDescription.ReadOnly = true;
            ProcessingMethod.ReadOnly = true;
        }
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("OriginatorGUID", OriginatorGUID.ValueText);
            objects.setData("CallUserGUID", CallUserGUID.GuidValueText);
            objects.setData("CallOrgUnitGUID", CallOrgUnitGUID.GuidValueText);
            objects.setData("OriginatorGUID", OriginatorGUID.ValueText);
            objects.setData("CallExtension", CallExtension.ValueText);
            objects.setData("CallDateTime", CallDateTime.ValueText);
            objects.setData("ProcessingHours", ProcessingHours.ValueText);
            objects.setData("IssueDescription", IssueDescription.ValueText);
            objects.setData("ProcessingMethod", ProcessingMethod.ValueText);

            //產生單號並儲存至資料物件
            base.saveData(engine, objects);
        }
    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;

        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 設定自動編碼格式所需變數值。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID);
        return hs;
    }

    /// <summary>
    /// 設定流程變數。目前主要是用來傳遞流程所需要的變數值。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        xml += "<SPIT004>";
        xml += "<originator DataType=\"java.lang.String\"></originator>";
        xml += "</SPIT004>";
        param["SPIT004"] = xml;
        return "SPIT004";
    }

    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        base.afterSend(engine, currentObject);
    }

    /// <summary>
    /// 若有加簽，送簽核前呼叫。
    /// 加簽時系統會設定Session("IsAddSign")，所以必需在saveData時執行 setSession("IsAddSign", "AFTER");
    /// AFTER 代表往後簽核
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        return base.beforeSign(engine, isAfter, addSignXml);
    }

    /// <summary>
    /// 按下送簽按鈕後呼叫此方法。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterSign(engine, currentObject, result);
    }

    /// <summary>
    /// 重辦程序
    /// </summary>
    protected override void rejectProcedure()
    {
        //退回填表人終止流程
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
        if (backActID.ToUpper().Equals("CREATOR"))
        {
            try
            {
                base.terminateThisProcess();
            }
            catch (Exception e)
            {
                base.writeLog(e);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }

    /// <summary>
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterApprove(engine, currentObject, result);
    }
    
    /// <summary>
    /// 選擇叫修人員自動帶出叫修單位
    /// </summary>
    /// <param name="values"></param>
    protected void CallUserGUID_SingleOpenWindow(string[,] values)
    {
        AbstractEngine engine = null;
        try
        {
            string userGUID = CallUserGUID.GuidValueText;
            if (!userGUID.Equals(""))
            {
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                string[] deptInfo = base.getDeptInfo(engine, userGUID);
                CallOrgUnitGUID.ValueText = deptInfo[0];
                //CallOrgUnitGUID.GuidValueText = deptInfo[3];
                CallOrgUnitGUID.doValidate();
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }
    }
}