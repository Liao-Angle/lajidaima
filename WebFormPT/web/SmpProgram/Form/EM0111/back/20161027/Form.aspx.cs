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

public partial class Program_SCQ_Form_EM0111_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EM0111";
        AgentSchema = "WebServerProject.form.EM0111.EM0111Agent";
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


        string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where EmpNo ='" + si.fillerID + "'";

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
        zb.ValueText = objects.getData("zb");
        cpu.ValueText = objects.getData("cpu");
        cupfs.ValueText = objects.getData("cupfs");
        disk.ValueText = objects.getData("disk");
        powerx.ValueText = objects.getData("powerx");
        powerd.ValueText = objects.getData("powerd");
        mouse.ValueText = objects.getData("mouse");
        keyboardx.ValueText = objects.getData("keyboardx");
        keyboardd.ValueText = objects.getData("keyboardd");
        bzwk.ValueText = objects.getData("bzwk");
        qzwk.ValueText = objects.getData("qzwk");
        wx.ValueText = objects.getData("wx");
        memory.ValueText = objects.getData("memory");
        telphone.ValueText = objects.getData("telphone");
        xk.ValueText = objects.getData("xk");
        reason.ValueText = objects.getData("reason");
        QTLY.ValueText = objects.getData("QTLY");
        remark.ValueText = objects.getData("remark");

        mobileuser.ReadOnly = true;
        partNouser.ReadOnly = true;
        zb.ReadOnly = true;
        cpu.ReadOnly = true;
        cupfs.ReadOnly = true;
        disk.ReadOnly = true;
        powerx.ReadOnly = true;
        powerd.ReadOnly = true;
        mouse.ReadOnly = true;
        keyboardx.ReadOnly = true;
        keyboardd.ReadOnly = true;
        bzwk.ReadOnly = true;
        qzwk.ReadOnly = true;
        wx.ReadOnly = true;
		xk.ReadOnly = true;
        telphone.ReadOnly = true;
        memory.ReadOnly = true;
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
            objects.setData("zb", zb.ValueText);
            objects.setData("cpu", cpu.ValueText);
            objects.setData("cupfs", cupfs.ValueText);
            objects.setData("disk", disk.ValueText);
            objects.setData("powerx", powerx.ValueText);
            objects.setData("powerd", powerd.ValueText);
            objects.setData("mouse", mouse.ValueText);
            objects.setData("keyboardd", keyboardd.ValueText);
            objects.setData("keyboardx", keyboardx.ValueText);
            objects.setData("bzwk", bzwk.ValueText);
            objects.setData("qzwk", qzwk.ValueText);
            objects.setData("wx", wx.ValueText);
            objects.setData("memory", memory.ValueText);
            objects.setData("telphone", telphone.ValueText);
            objects.setData("xk", xk.ValueText);
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
            string subject = "【 設備領用單 】";
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
                usermrg = "QG1200421";
            }
            
        }
        else
        {
            usermrg = values[1];  //申請人的主管 工號
        }

        string xml = "";
        xml += "<EM0111>";
        xml += "<usermrg DataType=\"java.lang.String\">" + usermrg + "</usermrg>";
        xml += "</EM0111>";
        param["EM0111"] = xml;
        return "EM0111";

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
