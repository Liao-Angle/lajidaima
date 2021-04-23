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
        BGDN.ValueText = objects.getData("BGDN");
        CXDN.ValueText = objects.getData("CXDN");
        if (objects.getData("Hitch") == "Y")
        {
            HitchY.Checked = true;
        }
        if(objects.getData("Hitch")=="N")
        {
            HitchN.Checked = true;
        }
        else { HitchM.Checked = true; }
        LXSQ.ValueText = objects.getData("LXSQ");
        LJP.ValueText = objects.getData("LJP");
        if (objects.getData("JP") == "D")
        {
            JPD.Checked = true;
        }
        else { JPX.Checked = true; }
        SHUBIAO.ValueText = objects.getData("SHUBIAO");
        DIANHUA.ValueText = objects.getData("DIANHUA");
        QTLY.ValueText = objects.getData("QTLY");
        Remark.ValueText = objects.getData("Remark");
        ZHUJI.ValueText = objects.getData("ZHUJI");
        ZJR.ValueText = objects.getData("ZJR");
        WXS.ValueText = objects.getData("WXS");
        if (objects.getData("WX") == "Y")
        {
            WXY.Checked = true;
        }
        else
        {
            WXN.Checked = true;
        }
        XSQR.ValueText = objects.getData("XSQR");
        SMQW.ValueText = objects.getData("SMQW");
        SMQR.ValueText = objects.getData("SMQR");
        if (objects.getData("SMQ") == "A")
        {
            SMQA.Checked = true;
        }
        if (objects.getData("SMQ") == "B")
        {
            SMQB.Checked = true;
        }
        else { SMQC.Checked = true; }
        WBZ.ValueText = objects.getData("WBZ");
        mobileuser.ReadOnly = true;
        partNouser.ReadOnly = true;
        BGDN.ReadOnly = true;
        CXDN.ReadOnly = true;
        HitchY.ReadOnly = true;
        HitchN.ReadOnly = true;
        HitchM.ReadOnly = true;
        LXSQ.ReadOnly = true;
        LJP.ReadOnly = true;
        JPD.ReadOnly = true;
        JPX.ReadOnly = true;
        SHUBIAO.ReadOnly = true;
        DIANHUA.ReadOnly = true;
        QTLY.ReadOnly = true;
        Remark.ReadOnly = true;
        ZHUJI.ReadOnly = true;
        ZJR.ReadOnly = true;
        WXS.ReadOnly = true;
        WXY.ReadOnly = true;
        WXN.ReadOnly = true;
        XSQR.ReadOnly = true;
        SMQW.ReadOnly = true;
        SMQR.ReadOnly = true;
        SMQA.ReadOnly = true;
        SMQB.ReadOnly = true;
        SMQC.ReadOnly = true;
        WBZ.ReadOnly = true;

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
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);
              
            //顯示要Save的資料 
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo", objects.getData("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("partNouser", partNouser.ValueText);
            objects.setData("BGDN", BGDN.ValueText);
            objects.setData("CXDN", CXDN.ValueText);
            objects.setData("mobileuser", mobileuser.ValueText);
            objects.setData("LXSQ", LXSQ.ValueText);
            objects.setData("LJP", LJP.ValueText);
            objects.setData("SHUBIAO", SHUBIAO.ValueText);
            objects.setData("DIANHUA", DIANHUA.ValueText);
            objects.setData("QTLY", QTLY.ValueText);
            objects.setData("Remark", Remark.ValueText);
            objects.setData("ZHUJI", ZHUJI.ValueText);
            objects.setData("ZJR", ZJR.ValueText);
            objects.setData("WXS", WXS.ValueText);
            objects.setData("XSQR", XSQR.ValueText);
            objects.setData("SMQW", SMQW.ValueText);
            objects.setData("SMQR", SMQR.ValueText);
            objects.setData("WBZ", WBZ.ValueText);
            objects.setData("Uname",si.fillerID);
            objects.setData("CName",si.fillerName);

            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            if (HitchY.Checked)
            {
                objects.setData("Hitch", "Y");
            }
            if (HitchM.Checked)
            {
                objects.setData("Hitch","M");
            }
            else { objects.setData("Hitch", "N"); }

            if (JPD.Checked)
            {
                objects.setData("JP", "D");
            }
            else { objects.setData("JP","X"); }

            if (WXY.Checked)
            {
                objects.setData("WX", "Y");
            }
            else { objects.setData("WX","N"); }

            if (SMQA.Checked)
            {
                objects.setData("SMQ","A");
            }
            if (SMQB.Checked)
            {
                objects.setData("SMQ", "B");
            }
            else { objects.setData("SMQ","C"); } 
            
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
