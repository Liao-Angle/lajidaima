

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
using System.DirectoryServices;
using DSCWebControl;

public partial class Program_SCQ_Form_EM0107_Form : SmpBasicFormPage
{
    private int MaxDSC = 9;
    private string DSCName = "DSCCheckBox";
    protected override void init()
    {
        ProcessPageID = "EM0107";
        AgentSchema = "WebServerProject.form.EM0107.EM0107Agent";
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
        
        for (int i = 1; i <= MaxDSC; i++)
        {
            DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
        }

        string[,] aids = new string[3, 2] { { "NEW", "新增" }, { "MODIFY", "修改" }, { "OTHER", "其他" } };
        Action.setListItem(aids);

        string[,] rids = new string[2, 2] { { "Form", "Form" }, { "Report", "Report" } };
        Requirement1.setListItem(rids);

        Requirement2.ReadOnly = true;

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

        string check = objects.getData("MESModules");
        for (int i = 1; i <= MaxDSC; i++)
        {
            DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
            if (dsc != null && i <= check.Length && check.Substring(i - 1, 1) == "Y")
            {
                dsc.Checked = true;
            }
            if (dsc != null)
            {
                dsc.ReadOnly = true;
            }
        }

        Subject.ValueText = objects.getData("Subject");
        Action.ValueText = objects.getData("Action");        
        Requirement2.ValueText = objects.getData("Requirement");
        Reason.ValueText = objects.getData("Reason");
        Benefit.ValueText = objects.getData("Benefit");

        Subject.ReadOnly = true;
        Action.ReadOnly = true;
        Requirement1.Visible = false;
        Requirement2.ReadOnly = true;
        Reason.ReadOnly = true;
        Benefit.ReadOnly = true;
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
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

            objects.setData("GUID", IDProcessor.getID(""));
            string p = string.Empty;
            for (int i = 1; i <= MaxDSC; i++)
            {
                DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
                if (dsc != null && dsc.Checked)
                {
                    p += "Y";
                }
                else
                {
                    p += "N";
                }
            }
            objects.setData("MESModules", p);
            objects.setData("Subject", Subject.ValueText);
            objects.setData("Action", Action.ValueText);
            if (Requirement1.ReadOnly)
            {
                objects.setData("Requirement", Requirement2.ValueText);
            }
            else
            {
                objects.setData("Requirement", Requirement1.ValueText);
            }
            objects.setData("Reason", Reason.ValueText);
            objects.setData("Benefit", Benefit.ValueText);

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
            if (isNecessary(Subject))
            {
                if (Subject.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫需求主題");
                    result = false;
                }
            }
            if (isNecessary(Reason))
            {
                if (Reason.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫需求說明");
                    result = false;
                }
            }
            if (isNecessary(Benefit))
            {
                if (Benefit.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫效益分析");
                    result = false;
                }
            }
            if (Action.ValueText.Equals("OTHER"))
            {
                if (Requirement2.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫需求類型");
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
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string str = si.fillerID;
        string managerId = "";
        string[] values = base.getUserManagerInfoID(engine, si.fillerID);
        managerId = values[1];  //申請人的主管 工號
        string xml = "";
        xml += "<EM0107>";
        xml += "<CREATOR DataType=\"java.lang.String\">" + str + "</CREATOR>";
        xml += "<ManagerID DataType=\"java.lang.String\">" + managerId + "</ManagerID>";
        xml += "<MEScheck DataType=\"java.lang.String\">" + "" + "</MEScheck>";
        xml += "<STCSMEScheck DataType=\"java.lang.String\">" + "" + "</STCSMEScheck>";
        xml += "<STCSMISmanager DataType=\"java.lang.String\">" + "" + "</STCSMISmanager>";
        xml += "<MISmanager DataType=\"java.lang.String\">" + "" + "</MISmanager>";
        xml += "<GQAmanager DataType=\"java.lang.String\">" + "" + "</GQAmanager>";
        xml += "<managerFZ DataType=\"java.lang.String\">" + "" + "</managerFZ>";
        xml += "<isqh DataType=\"java.lang.String\">" + "" + "</isqh>";
        xml += "</EM0107>";
        param["EM0107"] = xml;
        return "EM0107";
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

    protected void Action_SelectChanged(string value)
    {
        if (value == "OTHER")
        {
            Requirement1.ReadOnly = true;
            Requirement2.ReadOnly = false;
        }
        else
        {
            Requirement1.ReadOnly = false;
            Requirement2.ReadOnly = true;
        }
    }
}
