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

public partial class Program_SCQ_Form_EQ0101_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EQ0101";
        AgentSchema = "WebServerProject.form.EQ0101.EQ0101Agent";
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
            dos.setChildClassString("WebServerProject.form.EQ0101.EQ0101B");
            dos.setTableName("EQ0101B");
            dos.loadFileSchema();
            objects.setChild("EQ0101B", dos);
            OverTimeList.dataSource = dos;
            OverTimeList.updateTable();
        }
        OverTimeList.clientEngineType = engineType;
        OverTimeList.connectDBString = connectString;
        OverTimeList.HiddenField = new string[] { "GUID", "SheetNo" };

        Subject.Display = false;
        SheetNo.Display = false;


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
            PTitle.ValueText = "助理";
            PTitle.ReadOnly = true;
            EmpNo.ReadOnly = true;
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

            OverTimeList.dataSource = objects.getChild("EQ0101B");
            OverTimeList.updateTable();

            OverTimeList.IsShowCheckBox = false;
            OverTimeList.ReadOnly = true;

            ROS1.Visible = false;
            EmpMobile.ReadOnly = true;


            Note.ValueText = objects.getData("Note");
            Strategy.ValueText = objects.getData("Strategy");

            //主旨
            Subject.ValueText = objects.getData("Subject");
           //顯示單號
            base.showData(engine, objects);
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        //產生單號並儲存至資料物件
        base.saveData(engine, objects);

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            //顯示要Save的資料
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("EmpMobile", EmpMobile.ValueText);
            objects.setData("Depart", Depart.ValueText);
            objects.setData("PTitle", PTitle.ValueText);

            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            //將B表匯進來
            foreach (DataObject obj in OverTimeList.dataSource.getAllDataObjects())
            {
                obj.setData("SheetNo", objects.getData("SheetNo"));
            }
            objects.setChild("EQ0101B", OverTimeList.dataSource);


        }
            objects.setData("Note", Note.ValueText);
            objects.setData("Strategy", Strategy.ValueText);
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

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        if (Session["UserID"].ToString() == si.fillerID && si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0")
        {
            pushErrorMessage("MFG-1 部門 請選擇簽核主管");
            result = false;
        }

        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 MES特殊流程申請單 】";
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
        string str = "";
        string isqh = "";
        string mrgmrg = "";
        if (si.fillerOrgID == "GQC2200-GA" || si.fillerOrgID == "GQC2100-HR" || si.fillerOrgID == "GQM0110-FAC")
        {
            str = "1";
        }
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
        //mrgmrg = base.getUserManagerInfoID(engine, managerId)[1];
        string[] megvalues = base.getUserManagerInfoID(engine, managerId);
        if (megvalues[1].Trim().ToString() != "" || megvalues[1].Trim().ToString() != string.Empty)
        {
            mrgmrg = megvalues[1];
            isqh = "1";
        }


        string xml = "";
        xml += "<EQ0101>";
        xml += "<CREATOR DataType=\"java.lang.String\">" + si.fillerID + "</CREATOR>";
        xml += "<managerID DataType=\"java.lang.String\">" + managerId + "</managerID>";
        xml += "<Mrgmrg DataType=\"java.lang.String\">" + managerId + "</Mrgmrg>";
        xml += "<MESCHECK DataType=\"java.lang.String\">" + "" + "</MESCHECK>";
        xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";
        xml += "</EQ0101>";
        param["EQ0101"] = xml;
        return "EQ0101";
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

        if (Line.ValueText.Trim().Equals(""))
        {
            MessageBox("必須填寫線別");
            return false;
        }
        if (Order.ValueText.Trim().Equals(""))
        {
            MessageBox("必須填寫制令單號");
            return false;
        }
        if (Feed.ValueText.Trim().Equals(""))
        {
            MessageBox("必須填寫料號");
            return false;
        }
        if (Number.ValueText.Trim().Equals(""))
        {
            MessageBox("必須填寫數量");
            return false;
        }
        if (Reason.ValueText.Trim().Equals(""))
        {
            MessageBox("必須填寫原因");
            return false;
        }


        objects.setData("GUID", IDProcessor.getID(""));
        //SheetNo.ValueText = "SMEM0106201500101";
        // objects.setData("SheetNo", "SMEM0106201500101");
        objects.setData("Line", Line.ValueText);
        objects.setData("Arder", Order.ValueText);
        objects.setData("Feed", Feed.ValueText);
        objects.setData("Number", Number.ValueText);
        objects.setData("Reason", Reason.ValueText);

        OverTimeList.updateTable(); 

        return true;
    }
}
