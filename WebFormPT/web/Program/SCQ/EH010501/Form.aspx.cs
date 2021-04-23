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

public partial class Program_SCQ_Form_EH010501_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EH010501";
        AgentSchema = "WebServerProject.form.EH010501.EH010501Agent";
        ApplicationID = "SYSTEM";
        ModuleID = "EASYFLOW";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string[,] rids = new string[4, 2] { { "REPLACE_DL", "遞補直接員工" }, { "REPLACE_IDL", "遞補間接員工" }, { "NEW_DL", "新增直接員工" }, { "NEW_IDL", "新增間接員工" } };
        IssueType.setListItem(rids);

        string[,] mids = new string[2, 2] { { "役畢", "役畢" }, { "不拘", "不拘" } };
        Military.setListItem(mids);

        string[,] gids = new string[3, 2] { { "男", "男" }, { "女", "女" }, { "不拘", "不拘" }};
        Gender.setListItem(gids);

        string[,] eids = new string[6, 2] { { "中專", "中專" }, { "大專", "大專" }, { "本科", "本科" }, { "碩士", "碩士" }, { "博士", "博士" }, { "其他", "其他" } };
        Education.setListItem(eids);

        ApplyDate.ReadOnly = true;
        Department.ValueText = si.fillerOrgName;

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

        ApplyDate.ValueText = DateTime.Parse(objects.getData("D_INSERTTIME")).ToString("yyyy/MM/dd");
        IssueDate.ValueText = objects.getData("IssueDate");
        Department.ValueText = objects.getData("Department");
        Quantity.ValueText = objects.getData("Quantity");
        IssueType.ValueText = objects.getData("IssueType") + "_" + objects.getData("EmpType");
        IssueQuantity.ValueText = objects.getData("IssueQuantity");
        Reason.ValueText = objects.getData("Reason");
        IssuePosition.ValueText = objects.getData("IssuePosition");
        Gender.ValueText = objects.getData("Gender");
        Military.ValueText = objects.getData("Military");
        AgeMax.ValueText = objects.getData("AgeMax");
        AgeMin.ValueText = objects.getData("AgeMin");
        Education.ValueText = objects.getData("Education");
        Subject.ValueText = objects.getData("Subject");
        Technical.ValueText = objects.getData("Technical");
        JobDetail.ValueText = objects.getData("JobDetail");

        ApplyDate.ReadOnly = true;
        IssueDate.ReadOnly = true;
        Department.ReadOnly = true;
        Quantity.ReadOnly = true;
        IssueType.ReadOnly = true;
        IssueQuantity.ReadOnly = true;
        Reason.ReadOnly = true;
        IssuePosition.ReadOnly = true;
        Gender.ReadOnly = true;
        Military.ReadOnly = true;
        AgeMax.ReadOnly = true;
        AgeMin.ReadOnly = true;
        Education.ReadOnly = true;
        Subject.ReadOnly = true;
        Technical.ReadOnly = true;
        JobDetail.ReadOnly = true;
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        if (base.isNew())
        {
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IssueDate", IssueDate.ValueText);
            objects.setData("Department", Department.ValueText);
            objects.setData("Quantity", Quantity.ValueText);
            string[] s = IssueType.ValueText.Split(new string[] { "_" }, StringSplitOptions.None);
            objects.setData("IssueType", s[0]);
            objects.setData("EmpType", s[1]);
            objects.setData("IssueQuantity", IssueQuantity.ValueText);
            objects.setData("Reason", Reason.ValueText);
            objects.setData("IssuePosition", IssuePosition.ValueText);
            objects.setData("Gender", Gender.ValueText);
            objects.setData("Military", Military.ValueText);
            objects.setData("AgeMax", AgeMax.ValueText);
            objects.setData("AgeMin", AgeMin.ValueText);
            objects.setData("Education", Education.ValueText);
            objects.setData("Subject", Subject.ValueText);
            objects.setData("Technical", Technical.ValueText);
            objects.setData("JobDetail", JobDetail.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        if (base.isNew())
        {
            if (isNecessary(IssueDate))
            {
                if (IssueDate.ValueText.Equals(""))
                {
                    pushErrorMessage("必須選擇預定錄用日期");
                    result = false;
                }
            }
            if (isNecessary(Department))
            {
                if (Department.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫申請部門");
                    result = false;
                }
            }
            if (isNecessary(Quantity))
            {
                if (Quantity.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫現有編制人數");
                    result = false;
                }
            }
            if (isNecessary(IssueQuantity))
            {
                if (IssueQuantity.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫需求人數");
                    result = false;
                }
            }
            if (isNecessary(IssuePosition))
            {
                if (IssuePosition.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫需求職位");
                    result = false;
                }
            }
            if (isNecessary(Reason))
            {
                if (Reason.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫需求原因");
                    result = false;
                }
            }
            if (isNecessary(Subject))
            {
                if (Subject.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫科系");
                    result = false;
                }
            }
            if (isNecessary(Technical))
            {
                if (Technical.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫技能與人格特質");
                    result = false;
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        return getSubmitInfo(engine, objects, si);
    }
    
    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];//填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];//表單關系人
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
    /// <param name="engine">WebFormPT</param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        return "";
    }


    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine">WebFormPT</param>
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
    /// <param name="engine">WebFormPT</param>
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
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
    }
}
