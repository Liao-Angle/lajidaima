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

public partial class Program_SCQ_Form_EM0112_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EM0112";
        AgentSchema = "WebServerProject.form.EM0112.EM0112Agent";
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
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];


        string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + si.fillerID + "'";

        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
        ///--------------------------帶出使用者信息
        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
        EmpNo.DoEventWhenNoKeyIn = false;
        EmpNo.ValueText = si.fillerID;
        EmpNo.doValidate();
        UpdateData(si.fillerID);
        EmpNo.ReadOnly = true;

        if (isNew() || Session["UserID"].ToString() == si.fillerID)
        {
            string[,] xgqhzg = new string[,]{ {"0","請選擇"},
                {"1","張華謙"},
                {"2","田軍祥"},	};
            sqzszg.setListItem(xgqhzg);
            ///
            if (si.fillerOrgID == "NQM010101")
            {
                showzg.Visible = true;
            }
        }


        //改變工具列順序
        base.initUI(engine, objects);
    }

    private void UpdateData(string id)
    {
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            Hashtable h1 = base.getHRUsers(engine, id);
            partNouser.ValueText = h1["PartNo"].ToString();
            partNouser.ReadOnly = true;
            //  Mobile.ValueText = h1["Mobile"].ToString();
            Hashtable h2 = base.getADUserData(engine, id);
            mobileuser.ValueText = h2["telephonenumber"].ToString();
        }
        catch { }
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
        Subject.ValueText = objects.getData("Subject");
        mobileuser.ValueText = objects.getData("mobileuser");
        partNouser.ValueText = objects.getData("partNouser");
        
        reason.ValueText = objects.getData("reason");
        QTLY.ValueText = objects.getData("QTLY");
        remark.ValueText = objects.getData("remark");
        BGDN.ValueText = objects.getData("BGDN");
        CXDN.ValueText = objects.getData("CXDN");
        GCDNP.ValueText = objects.getData("GCDNP");
        GCDNZ.ValueText = objects.getData("GCDNZ");
        GYDN6.ValueText = objects.getData("GYDN6");
        GYDN9.ValueText = objects.getData("GYDN9");
        GYDN8.ValueText = objects.getData("GYDN8");

        mobileuser.ReadOnly = true;
        partNouser.ReadOnly = true;
        BGDN.ReadOnly = true;
        CXDN.ReadOnly = true;
        GCDNP.ReadOnly = true;
        GCDNZ.ReadOnly = true;
        GYDN6.ReadOnly = true;
        GYDN9.ReadOnly = true;
        GYDN8.ReadOnly = true;
        reason.ReadOnly = true;
        QTLY.ReadOnly = true;
        remark.ReadOnly = true;
        

        //顯示發起資料

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
            //顯示要Save的資料
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo", objects.getData("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("Uname", si.fillerID);
            objects.setData("CName", si.fillerName);
            objects.setData("mobileuser", mobileuser.ValueText);
            objects.setData("partNouser", partNouser.ValueText);
            objects.setData("BGDN", BGDN.ValueText);
            objects.setData("CXDN", CXDN.ValueText);
            objects.setData("GCDNP", GCDNP.ValueText);
            objects.setData("GCDNZ", GCDNZ.ValueText);
            objects.setData("GYDN6", GYDN6.ValueText);
            objects.setData("GYDN8", GYDN8.ValueText);
            objects.setData("GYDN9", GYDN9.ValueText);
            objects.setData("reason", reason.ValueText);
            objects.setData("QTLY", QTLY.ValueText);
            objects.setData("remark", remark.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

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
        //新增判斷資料
        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 整機領用單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }
        }

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0" && Session["UserID"].ToString() == si.fillerID)
        {
            pushErrorMessage("MFG-1 部門 請選擇簽核主管");
            result = false;
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
        string usermrg = "";
        string[] values = base.getUserManagerInfoID(engine, si.fillerID);
        if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText != "0")
        {
            if (sqzszg.ValueText == "1")
            {
                usermrg = "Q1100135";
            }
            else if (sqzszg.ValueText == "2")
            {
                usermrg = "Q1608418";
            }
            
        }
        else
        {
            usermrg = values[1];  //申請人的主管 工號
        }

        string xml = "";
        xml += "<EM0112>";
        xml += "<usermrg DataType=\"java.lang.String\">" + usermrg + "</usermrg>";
        xml += "</EM0112>";
        param["EM0112"] = xml;
        return "EM0112";

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
        //"Y"代表同意
        if (result == "Y")
        {
            //開發同意簽核後自動化流程
        }
    }
}
