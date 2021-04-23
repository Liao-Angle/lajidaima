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

public partial class Program_SCQ_Form_EFIN01_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EFIN01";
        AgentSchema = "WebServerProject.form.EFIN01.EFIN01Agent";
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
        string actName = (string)getSession("ACTName");
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
        EmpNoIn.clientEngineType = engineType;
        EmpNoIn.connectDBString = connectString;
        owner.clientEngineType = engineType;
        owner.connectDBString = connectString;

        SheetNo.Display = false;
        Subject.Display = false;
        try
        {

            string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + Session["UserID"].ToString() + "'";
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
                EmpNoIn.whereClause = "";
                owner.whereClause = "";


            }
            else
            {
                EmpNo.whereClause = "1=2";
                EmpNoIn.whereClause = "1=2";
            }

            EmpNo.DoEventWhenNoKeyIn = false;
            EmpNoIn.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "")
            {
                EmpNo.whereClause = "PartNo like '%" + si.fillerOrgID + "%'";
                EmpNoIn.whereClause = "";
            }
            else
            {
                EmpNo.whereClause = "1=2";
                EmpNoIn.whereClause = "1=2";
            }
            EmpNo.DoEventWhenNoKeyIn = false;
            EmpNoIn.DoEventWhenNoKeyIn = false;
        }






        if (base.isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.EFIN01.EFIN01B");
            dos.setTableName("EFIN01B");
            dos.loadFileSchema();
            objects.setChild("EFIN01B", dos);
            OutDataList.dataSource = dos;
            OutDataList.updateTable();
        }

        OutDataList.clientEngineType = engineType;
        OutDataList.connectDBString = connectString;
        OutDataList.HiddenField = new string[] { "GUID", "SheetNo" };

        if (si.fillerID.Substring(0, 1) == "Y")
        {
            this.Panel2.Visible = true;
        }
        else
        {
            this.Panel1.Visible = true;
        }

        this.Panel3.Visible = true;

        PartNo.ReadOnly = true;

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
        string actName = (string)getSession("ACTName");
        OutDataList.dataSource = objects.getChild("EFIN01B");
        OutDataList.updateTable();
        OutDataList.IsShowCheckBox = false;
        OutDataList.ReadOnly = true;
        SheetNo.Display = false;
        Subject.Display = false;

        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        PartNo.ValueText = objects.getData("PartNo");
        SQDate.ValueText = objects.getData("SQDate");
        owner.ValueText = objects.getData("owner");
	owner.ReadOnlyValueText = objects.getData("owname");
        EmpNoIn.ValueText = objects.getData("EmpNoIn");
        EmpNoIn.ReadOnlyValueText = objects.getData("EmpNameIn");
        PartNo1.ValueText = objects.getData("PartNoIn");
        ZRDate.ValueText = objects.getData("ZRDate");
        bz.ValueText = objects.getData("bz");

        EmpNo.ReadOnly = true;
        PartNo.ReadOnly = true;
        SQDate.ReadOnly = true;
        EmpNoIn.ReadOnly = true;
        PartNo1.ReadOnly = true;
        ZRDate.ReadOnly = true;

        //if (actName.ToString() != "申請人")
        //{
        //    this.Panel3.Visible = false;
        //}

        //顯示單號
        base.showData(engine, objects);

        //顯示發起資料

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
            //objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("PartNo", PartNo.ValueText);
            objects.setData("SQDate", SQDate.ValueText);
            objects.setData("owner", owner.ValueText);
            objects.setData("owname", owner.ReadOnlyValueText);
            objects.setData("EmpNoIn", EmpNoIn.ValueText);
            objects.setData("EmpNameIn", EmpNoIn.ReadOnlyValueText);
            objects.setData("PartNoIn", PartNo1.ValueText);
            objects.setData("ZRDate", ZRDate.ValueText);
            objects.setData("bz", bz.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            //將B表匯進來
            foreach (DataObject obj in OutDataList.dataSource.getAllDataObjects())
            {
                obj.setData("SheetNo", objects.getData("SheetNo"));
            }
            objects.setChild("EFIN01B", OutDataList.dataSource);
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
            string subject = "【 財產轉移申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
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
        string xml = "";
        string inowner = owner.ValueText;
        string inmanger = EmpNoIn.ValueText;


        xml += "<EFIN01>";
        xml += "<jinban DataType=\"java.lang.String\">" + inowner + "</jinban>";
        xml += "<inmanger DataType=\"java.lang.String\">" + inmanger + "</inmanger>";
        xml += "</EFIN01>";

        param["EFIN01"] = xml;
        return "EFIN01";
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
    protected void EmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h1 = base.getHRUsers(engine, EmpNo.ValueText);
        PartNo.ValueText = h1["PartNo"].ToString();
    }
    protected void EmpNoIn_SingleOpenWindowButtonClick(string[,] values)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h1 = base.getHRUsers(engine, EmpNoIn.ValueText);
        PartNo1.ValueText = h1["PartNo"].ToString();
    }
    protected bool OutDataList_SaveRowData(DataObject objects, bool isNew)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);


        try
        {
            DataObject[] objs = OutDataList.dataSource.getCurrentPageObjects();
            for (int i = 0; i < objs.Length; i++)
            {
                if (CCBH.ValueText == objs[i].getData("CCBH"))
                {
                    MessageBox("已有重複資料");
                    return false;
                }
            }

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo", base.PageUniqueID);
            objects.setData("CCBH", CCBH.ValueText);
            objects.setData("CCMC", CCMC.ValueText);
            objects.setData("GGE", GGE.ValueText);
            objects.setData("NUM", NUM.ValueText);
            objects.setData("DW", DW.ValueText);
            objects.setData("Reason", Reason.ValueText);
        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
            return false;
        }
        finally { OutDataList.updateTable(); try { engine.close(); } catch { } }
        return true;
    }
}
