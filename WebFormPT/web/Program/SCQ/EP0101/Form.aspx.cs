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

public partial class Program_SCQ_Form_EP0101_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EP0101";
        AgentSchema = "WebServerProject.form.EP0101.EP0101Agent";
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

        EmpNo.ValueText = si.fillerID;
        EmpNo.doValidate();
        UpdateData(si.fillerID);

        if (base.isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.EP0101.EP0101B");
            dos.setTableName("EP0101B");
            dos.loadFileSchema();
            objects.setChild("EP0101B", dos);
            OverTimeList.dataSource = dos;
            OverTimeList.updateTable();
        }
        OverTimeList.clientEngineType = engineType;
        OverTimeList.connectDBString = connectString;
        OverTimeList.HiddenField = new string[] { "GUID", "SheetNo" };

        if (isNew() || Session["UserID"].ToString() == si.fillerID)
        {
            string[,] xgqhzg = new string[,]{ {"0","請選擇"},
                {"1","張華謙"},
                {"2","田軍祥"},	
                {"3","許文坤"},};
            sqzszg.setListItem(xgqhzg);
            ///
            if (si.fillerOrgID == "NQM010101")
            {
                showzg.Visible = true;
            }
        }
        string[,] sgdwxz = new string[,]{ {"0","請選擇"},
                {"1","資訊and廠務"},
                {"2","資訊部"},	
                {"3","廠務部"},};
        sgdw.setListItem(sgdwxz);


      
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
            Depart.ValueText = h1["PartNo"].ToString();
            Depart.ReadOnly = true;
            //  Mobile.ValueText = h1["Mobile"].ToString();
            Hashtable h2 = base.getADUserData(engine, id);
            EmpMobile.ValueText = h2["telephonenumber"].ToString();
            EmpNo.ReadOnly = true;
            SDate.ValueText = DateTime.Now.ToShortDateString();
            SDate.ReadOnly = true;
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
	SDate.ValueText = objects.getData("SDate");
        Title.ValueText = objects.getData("Title");
        aim.ValueText = objects.getData("Aim");
        Area.ValueText = objects.getData("Area");
        ask.ValueText = objects.getData("Ask");
        YWorkDate.ValueText = objects.getData("YWorkDate");
        SWorkDate.ValueText = objects.getData("SworkDate");
        Money.ValueText = objects.getData("Money");
        sgdw.ValueText = objects.getData("sgdw");

        OverTimeList.dataSource = objects.getChild("EP0101B");
        OverTimeList.updateTable();

        OverTimeList.IsShowCheckBox = false;
        OverTimeList.ReadOnly = true;

        Title.ReadOnly = true;
        aim.ReadOnly = true;
        Money.ReadOnly = true;
        ask.ReadOnly = true;
        Area.ReadOnly = true;
        //顯示發起資料
        TR1.Visible = false;
        TR2.Visible = false;
        TR3.Visible = false;


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
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("EmpMobile", EmpMobile.ValueText);
            objects.setData("Depart", Depart.ValueText);

            objects.setData("SDate", SDate.ValueText);
            objects.setData("Title", Title.ValueText);
            objects.setData("aim", aim.ValueText);
            objects.setData("Area", Area.ValueText);
            objects.setData("ask", ask.ValueText);
            objects.setData("Money", Money.ValueText);

            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            foreach (DataObject obj in OverTimeList.dataSource.getAllDataObjects())
            {
                obj.setData("SheetNo", objects.getData("SheetNo"));
            }
            objects.setChild("EP0101B", OverTimeList.dataSource);

        }
        objects.setData("sgdw", sgdw.ValueText);
        objects.setData("YWorkDate", YWorkDate.ValueText);
        objects.setData("SWorkDate", SWorkDate.ValueText);
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
        if (OverTimeList.dataSource.getAvailableDataObjectCount() == 0)
        {
            pushErrorMessage("必須填寫資料");
            result = false;
        }

        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 Layout申請單 】";
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
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string isqh = "";
        string mrgmrg = "";
        string managerId = "";
        string[] values = base.getUserManagerInfoID(engine, si.fillerID);
        if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText != "0")
        {
            if (sqzszg.ValueText == "1")
            {
                managerId = "Q1100135";
            }
            else if (sqzszg.ValueText == "2")
            {
                managerId = "Q1608418";
            }
            else if (sqzszg.ValueText == "3")
            {
                managerId = "Q1210122";
            }
        }
        else
        {
            managerId = values[1];  //申請人的主管 工號
        }
        mrgmrg = base.getUserManagerInfoID(engine, managerId)[1];
        string MISMISQH = "", CWISQH = "";
        if (sgdw.ValueText == "1" || sgdw.ReadOnlyText == "1")
        {
            MISMISQH = "1"; CWISQH = "1";
        }else if (sgdw.ValueText == "2" || sgdw.ReadOnlyText == "2")
        {
            MISMISQH = "1"; CWISQH = "";
        }else if (sgdw.ValueText == "3" || sgdw.ReadOnlyText == "3")
        {
            MISMISQH = "2"; CWISQH = "";
        }

        string xml = "";
        xml += "<EP0101>";
        xml += "<CREATOR DataType=\"java.lang.String\">" + si.fillerID + "</CREATOR>";
        xml += "<ManagerID DataType=\"java.lang.String\">" + managerId + "</ManagerID>";
        xml += "<ACT3 DataType=\"java.lang.String\">" + "" + "</ACT3>";
        xml += "<ACT4 DataType=\"java.lang.String\">" + "" + "</ACT4>";
        xml += "<ManagerTZ DataType=\"java.lang.String\">" + "" + "</ManagerTZ>";

        xml += "<MISManager DataType=\"java.lang.String\">" + "" + "</MISManager>";
        xml += "<MISMISQH DataType=\"java.lang.String\">" + MISMISQH + "</MISMISQH>";

        xml += "<CWManager DataType=\"java.lang.String\">" + "" + "</CWManager>";
        xml += "<CWISQH DataType=\"java.lang.String\">" + CWISQH + "</CWISQH>";

        xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";
        xml += "</EP0101>";
        param["EP0101"] = xml;
        return "EP0101";
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
    protected bool RequestList_SaveRowData(DataObject objects, bool isNew)
    {
        if (Name.ValueText.Trim().Equals(""))
        {
            MessageBox("必須填寫設備名稱");
            return false;
        }
        if (version.ValueText.Trim().Equals(""))
        {
            MessageBox("必須填寫規格");
            return false;
        }
        if (Number.ValueText.Trim().Equals(""))
        {
            MessageBox("必須填寫數量");
            return false;
        }


        objects.setData("GUID", IDProcessor.getID(""));
        //SheetNo.ValueText = "SMEM0106201500101";
        // objects.setData("SheetNo", "SMEM0106201500101");
        objects.setData("Name", Name.ValueText);
        objects.setData("version", version.ValueText);
        objects.setData("Number", Number.ValueText);
        objects.setData("Note", Note.ValueText);

        OverTimeList.updateTable(); 

        return true;
    }
}
