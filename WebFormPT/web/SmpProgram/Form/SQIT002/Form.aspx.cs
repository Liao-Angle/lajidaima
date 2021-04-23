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

public partial class Program_SCQ_Form_SQIT002_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "SQIT002";
        AgentSchema = "WebServerProject.form.SQIT002.SQIT002Agent";
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

        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;

        string[,] ids = null;
        DataSet ds = null;
        int count = 0;

        //主旨不顯示於發起單據畫面
        SheetNo.Display = false;
        Subject.Display = false;
        try
        {

            string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where EmpNo ='" + Session["UserID"].ToString() + "'";
            DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
            string SQLstr = "";
            if (dtbm.Rows[0][0].ToString() != "")
            {
                SQLstr = "(PartNo = '" + dtbm.Rows[0][0].ToString() + "' ";
                for (int i = 1; i < dtbm.Rows.Count; i++)
                {
                    SQLstr = SQLstr + " or PartNo = '" + dtbm.Rows[i][0].ToString() + "' ";
                } SQLstr = SQLstr + ")";
                EmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


            }
            else
            {
                EmpNo.whereClause = "1=2";
            }

            EmpNo.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "")
            {
                EmpNo.whereClause = "PartNo like '%" + si.fillerOrgID + "%'";
            }
            else
            {
                EmpNo.whereClause = "1=2";
            }
            EmpNo.DoEventWhenNoKeyIn = false;
        }

        string[,] gs = new string[2, 2] { { "新普科技（重慶）有限公司", "新普科技（重慶）有限公司" }, { "重慶貽百電子有限公司", "重慶貽百電子有限公司" }};
        OrgNo.setListItem(gs);

        string[,] lb = new string[2, 2] { { "新增", "新增" }, { "修改", "修改" } };
        RqType.setListItem(lb);

        string[,] xm = new string[2, 2] { { "FORM", "FORM" }, { "REPORT", "REPORT" } };
        Rqxm.setListItem(xm);

        //string[,] it = new string[4, 2] { { "AP", "AP" }, { "MES", "MES" }, { "OA", "OA" }, { "弱電", "弱電" } };
        string[,] it = new string[2, 2] { { "AP", "AP" }, { "MES", "MES" } };
        OwnerType.setListItem(it);

        //MIS User
        ds = engine.getDataSet("select '','' from EmployeeInfo  union select empGUID, empName from EmployeeInfo where deptId in ('GQC2300','GIC2300') and (empLeaveDate is null or empLeaveDate ='') ", "TEMP");
        count = ds.Tables[0].Rows.Count;
        ids = new string[0 + count, 2];
        ids[0, 0] = "";
        ids[0, 1] = "";

        for (int i = 1; i < count; i++)
        {
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
        }
        MisOwnerGUID.setListItem(ids);

        bool isAddNew = base.isNew();
        if (isAddNew)
        {

            MisOwnerGUID.ReadOnly = true;
            MisOwnerGUID.ValueText = "";
        }
        else
        {
            MisOwnerGUID.ReadOnly = true;
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
        string actName = Convert.ToString(getSession("ACTName"));

        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        BillNo.Text = objects.getData("BillNo");
        OrgNo.ValueText = objects.getData("OrgNo");
        SystemNo.ValueText = objects.getData("SystemNo");
        PartNo.ValueText = objects.getData("PartNo");
        Rqdate.ValueText = objects.getData("Rqdate");
        RqType.ValueText = objects.getData("RqType");
        Rqxm.ValueText = objects.getData("Rqxm");
        OwnerType.ValueText = objects.getData("OwnerType");
        mark.ValueText = objects.getData("mark");
        Reason.ValueText = objects.getData("Reason");
        fxbg.ValueText = objects.getData("fxbg");
        fxed.ValueText = objects.getData("fxed");
        kfbg.ValueText = objects.getData("kfbg");
        kfed.ValueText = objects.getData("kfed");
        itbg.ValueText = objects.getData("itbg");
        ited.ValueText = objects.getData("ited");
        khbg.ValueText = objects.getData("khbg");
        khed.ValueText = objects.getData("khed");
        jfdate.ValueText = objects.getData("jfdate");
        xzcs.ValueText = objects.getData("xzcs");
        xgcs.ValueText = objects.getData("xgcs");
        xzbb.ValueText = objects.getData("xzbb");
        xgbb.ValueText = objects.getData("xgbb");
        MisOwnerGUID.ValueText = objects.getData("MisOwnerGUID");


        BillNo.ReadOnly = true;
        OrgNo.ReadOnly = true;
        SystemNo.ReadOnly = true;
        Rqdate.ReadOnly = true;
        RqType.ReadOnly = true;
        Rqxm.ReadOnly = true;
        OwnerType.ReadOnly = true;
        mark.ReadOnly = true;
        PartNo.ReadOnly = true;
        EmpNo.ReadOnly = true;

        Reason.ReadOnly = true;
        fxbg.ReadOnly = true;
        fxed.ReadOnly = true;
        kfbg.ReadOnly = true;
        kfed.ReadOnly = true;
        itbg.ReadOnly = true;
        ited.ReadOnly = true;
        khbg.ReadOnly = true;
        khed.ReadOnly = true;
        jfdate.ReadOnly = true;
        xzcs.ReadOnly = true;
        xgcs.ReadOnly = true;
        xzbb.ReadOnly = true;
        xgbb.ReadOnly = true;

        if (actName.Equals("資訊窗口") || actName.Equals("審核"))
        {
            MisOwnerGUID.ReadOnly = false;
        }
        if (actName.Equals("資訊窗口") || actName.Equals("審核") || actName.Equals("MIS承辦"))
        {
            Reason.ReadOnly = false;
            fxbg.ReadOnly = false;
            fxed.ReadOnly = false;
            kfbg.ReadOnly = false;
            kfed.ReadOnly = false;
            itbg.ReadOnly = false;
            ited.ReadOnly = false;
            khbg.ReadOnly = false;
            khed.ReadOnly = false;
            jfdate.ReadOnly = false;
            xzcs.ReadOnly = false;
            xgcs.ReadOnly = false;
            xzbb.ReadOnly = false;
            xgbb.ReadOnly = false;
        }

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
            objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("BillNo", BillNo.Text);
            objects.setData("OrgNo", OrgNo.ValueText);
            objects.setData("SystemNo", SystemNo.ValueText);
            objects.setData("PartNo", PartNo.ValueText);
            objects.setData("Rqdate", Rqdate.ValueText);
            objects.setData("RqType", RqType.ValueText);
            objects.setData("Rqxm", Rqxm.ValueText);
            objects.setData("OwnerType", OwnerType.ValueText);
            objects.setData("mark", mark.ValueText);
            
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");




            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

        }
        else
        {
            objects.setData("Reason", Reason.ValueText);
            objects.setData("fxbg", fxbg.ValueText);
            objects.setData("fxed", fxed.ValueText);
            objects.setData("kfbg", kfbg.ValueText);
            objects.setData("kfed", kfed.ValueText);
            objects.setData("itbg", itbg.ValueText);
            objects.setData("ited", ited.ValueText);
            objects.setData("khbg", khbg.ValueText);
            objects.setData("khed", khed.ValueText);
            objects.setData("jfdate", jfdate.ValueText);
            objects.setData("xzcs", xzcs.ValueText);
            objects.setData("xgcs", xgcs.ValueText);
            objects.setData("xzbb", xzbb.ValueText);
            objects.setData("xgbb", xgbb.ValueText);
            objects.setData("MisOwnerGUID", MisOwnerGUID.ValueText);
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
        string actName = Convert.ToString(getSession("ACTName"));
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 服務需求申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
            }
        }

        if (actName.Equals("MIS承辦") && (fxbg.ValueText.Equals("") || fxed.ValueText.Equals("") || kfbg.ValueText.Equals("") || kfed.ValueText.Equals("") || itbg.ValueText.Equals("") || ited.ValueText.Equals("") || khbg.ValueText.Equals("") || khed.ValueText.Equals("") || jfdate.ValueText.Equals("")))
        {
            pushErrorMessage("預計時程不能為空！");
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
        
        string xml = "";
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            //MIS處理人員
            string misOwnerGUID = MisOwnerGUID.ValueText;
            string MISOwner1 = "";
            string MISwindow = "Q1101039";
            string[] values = base.getUserInfo(engine, misOwnerGUID);
            if (OwnerType.ValueText == "AP")
            {
                MISwindow = "Q1101039";
            }
            else if (OwnerType.ValueText == "MES")
            {
                MISwindow = "Q1100464";
            }
            else if (OwnerType.ValueText == "OA")
            {
                MISwindow = "Q1100189";
            }
            else
            {
                MISwindow = "Q1202406";
            }

            if (!misOwnerGUID.Equals(""))
            {
                MISOwner1 = values[0];
            }
            //MessageBox(misOwnerGUID);


            xml += "<SPIT002>";
            xml += "<MISOwner1 DataType=\"java.lang.String\">" + MISOwner1 + "</MISOwner1>";//IT承辦人
            xml += "<MISwindow DataType=\"java.lang.String\">" + MISwindow + "</MISwindow>";//MIS窗口負責人
            xml += "</SPIT002>";
        }
        catch (Exception e)
        {
            writeLog(e);

        }

        param["SPIT002"] = xml;
        return "SPIT002";
    }


    /// <summary>   
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
    protected void JEmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h1 = base.getHRUsers(engine, EmpNo.ValueText);
        PartNo.ValueText = h1["PartNo"].ToString();
        BillNo.Text = System.DateTime.Now.ToString("yyyyMMddhhmm");

           
    }
    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SQIT002.log", true, System.Text.Encoding.Default);
            //sw.WriteLine(line);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }
}
