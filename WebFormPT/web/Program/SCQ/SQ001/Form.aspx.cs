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

public partial class Program_SCQ_Form_SQ001_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "SQ001";
        AgentSchema = "WebServerProject.form.SQ001.SQ001Agent";
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
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
        dlr.clientEngineType = engineType;
        dlr.connectDBString = connectString;

        
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
                dlr.whereClause = "";


            }
            else
            {
                EmpNo.whereClause = "1=2";
                dlr.whereClause = "1=2";
            }

            EmpNo.DoEventWhenNoKeyIn = false;
            dlr.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "")
            {
                EmpNo.whereClause = "PartNo like '%" + si.fillerOrgID + "%'";
                dlr.whereClause = "";
            }
            else
            {
                EmpNo.whereClause = "1=2";
                dlr.whereClause = "1=2";
            }
            EmpNo.DoEventWhenNoKeyIn = false;
            dlr.DoEventWhenNoKeyIn = false;
        }

        string[,] md = new string[5, 2] { { "請選擇", "請選擇" }, { "公務", "公務" }, { "培訓", "培訓" }, { "休假", "休假" }, { "其他", "其他" } };
        mudi.setListItem(md);

        string[,] xz = new string[3, 2] { { "請選擇", "請選擇" }, { "國內", "國內" }, { "國外", "國外" } };
        CCXZ.setListItem(xz);

        Department.ReadOnly = true;
        danwei.ReadOnly = true;
        ts.ReadOnly = true;
        heji.ReadOnly = true;


        if (base.isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.SQ001.SQ001B");
            dos.setTableName("SQ001B");
            dos.loadFileSchema();
            objects.setChild("SQ001B", dos);
            OutDataList.dataSource = dos;
            OutDataList.updateTable();
        }

        OutDataList.clientEngineType = engineType;
        OutDataList.connectDBString = connectString;
        OutDataList.HiddenField = new string[] { "GUID", "SheetNo" };

        SheetNo.Display = false;
        Subject.Display = false;

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
        
        OutDataList.dataSource = objects.getChild("SQ001B");
        OutDataList.updateTable();
        OutDataList.IsShowCheckBox = false;
        OutDataList.ReadOnly = true;
        SheetNo.Display = false;
        Subject.Display = false;

        Subject.ValueText = objects.getData("Subject");
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        Department.ValueText = objects.getData("Department");
        CCXZ.ValueText = objects.getData("CCXZ");
        sxrs.ValueText = objects.getData("sxrs");
        mudi.ValueText = objects.getData("mudi");
        qtbz.ValueText = objects.getData("qtbz");
        ccSDate.ValueText = objects.getData("ccSDate");
        ccEDate.ValueText = objects.getData("ccEDate");
        ts.ValueText = objects.getData("ts");
        dlr.ValueText = objects.getData("dlr").Substring(0, 8);
        dlr.ReadOnlyValueText = objects.getData("dlr").Substring(objects.getData("dlr").LastIndexOf("-")+1);
        danwei.ValueText = objects.getData("danwei");
        jipiao.ValueText = objects.getData("jipiao");
        jiaotong.ValueText = objects.getData("jiaotong");
        shanfei.ValueText = objects.getData("shanfei");
        qita.ValueText = objects.getData("qita");
        heji.ValueText = objects.getData("heji");
        bibie.ValueText = objects.getData("bibie");
        guishu.ValueText = objects.getData("guishu");

        EmpNo.ReadOnly = true;
        Department.ReadOnly = true;
        CCXZ.ReadOnly = true;
        sxrs.ReadOnly = true;
        mudi.ReadOnly = true;
        qtbz.ReadOnly = true;
        ccSDate.ReadOnly = true;
        ccEDate.ReadOnly = true;
        dlr.ReadOnly = true;
        danwei.ReadOnly = true;
        jipiao.ReadOnly = true;
        jiaotong.ReadOnly = true;
        shanfei.ReadOnly = true;
        qita.ReadOnly = true;
        bibie.ReadOnly = true;
        guishu.ReadOnly = true;


        //顯示發起資料
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
            //objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("Department", Department.ValueText);
            objects.setData("CCXZ", CCXZ.ValueText);
            objects.setData("sxrs", sxrs.ValueText);
            objects.setData("mudi", mudi.ValueText);
            objects.setData("qtbz", qtbz.ValueText);
            objects.setData("ccSDate", ccSDate.ValueText);
            objects.setData("ccEDate", ccEDate.ValueText);
            objects.setData("ts", ts.ValueText);
            objects.setData("dlr", dlr.ValueText + "-" + dlr.ReadOnlyValueText);
            objects.setData("danwei", danwei.ValueText);
            objects.setData("jipiao", jipiao.ValueText);
            objects.setData("jiaotong", jiaotong.ValueText);
            objects.setData("shanfei", shanfei.ValueText);
            objects.setData("qita", qita.ValueText);
            objects.setData("heji", heji.ValueText);
            objects.setData("bibie", bibie.ValueText);
            objects.setData("guishu", guishu.ValueText);
            objects.setData("ForwardHR", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            //將B表匯進來
            foreach (DataObject obj in OutDataList.dataSource.getAllDataObjects())
            {
                obj.setData("SheetNo", objects.getData("SheetNo"));
            }
            objects.setChild("SQ001B", OutDataList.dataSource);
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
            string subject = "【 長途出差申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
            }
            
        }

        if (mudi.ValueText=="請選擇")
        {
            pushErrorMessage("請選擇出差目的");
            result = false;
        }

        if (CCXZ.ValueText == "請選擇")
        {
            pushErrorMessage("請選擇出差性質");
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

        string xml = "";
        string zjl = "";
        string daili = "";

        if (!string.IsNullOrEmpty(dlr.ValueText.ToString()))
        {
            daili = dlr.ValueText;
        }


        if (CCXZ.ValueText == "國外")
        {
            zjl = "1";
        }


        xml += "<SQ0001>";
        xml += "<daili DataType=\"java.lang.String\">" + daili + "</daili>";//代理人是否簽核
        xml += "<zjl DataType=\"java.lang.String\">" + zjl + "</zjl>";//董事長是否簽核
        xml += "</SQ0001>";

        param["SQ0001"] = xml;
        return "SQ0001";
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
        Department.ValueText = h1["PartNo"].ToString();
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
                if (qiqidian.ValueText == objs[i].getData("qidian") && zhidian.ValueText == objs[i].getData("zhidian") && fromdate.ValueText == objs[i].getData("fromdate") && todate.ValueText == objs[i].getData("todate"))
                {
                    MessageBox("已有重複資料");
                    return false;
                }
            }
            
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("SheetNo", base.PageUniqueID);
                objects.setData("qidian", qiqidian.ValueText);
                objects.setData("zhidian", zhidian.ValueText);
                objects.setData("fromdate", fromdate.ValueText);
                objects.setData("todate", todate.ValueText);
                objects.setData("jtong", jtong.ValueText);
                objects.setData("zhusu", zhusu.ValueText);
        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
        }
        finally { OutDataList.updateTable(); try { engine.close(); } catch { } }
        return true;
    }
    protected void dlr_SingleOpenWindowButtonClick(string[,] values)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h2 = base.getHRUsers(engine, dlr.ValueText);
        danwei.ValueText = h2["PartNo"].ToString();
    }
    protected void ccEDate_DateTimeClick(string values)
    {
        getdays();
    }
    private void getdays()
    {
        DateTime a = Convert.ToDateTime(ccSDate.ValueText);
        DateTime b = Convert.ToDateTime(ccEDate.ValueText);

        TimeSpan c = b - a;
        string d = c.TotalDays.ToString();
        ts.ValueText = Convert.ToString(Convert.ToInt32(d) + 1);
    }
    protected void qita_TextChanged(object sender, EventArgs e)
    {
        getheji();
    }
    private void getheji()
    {
        double jp = 0;
        double jt = 0;
        double sf = 0;
        double qt = 0;

        try
        {
            if (!string.IsNullOrEmpty(jipiao.ValueText))
            {
                jp = Convert.ToDouble(jipiao.ValueText);
            }
            if (!string.IsNullOrEmpty(jiaotong.ValueText))
            {
                jt = Convert.ToDouble(jiaotong.ValueText);
            }
            if (!string.IsNullOrEmpty(shanfei.ValueText))
            {
                sf = Convert.ToDouble(shanfei.ValueText);
            }
            if (!string.IsNullOrEmpty(qita.ValueText))
            {
                qt = Convert.ToDouble(qita.ValueText);
            }

        }
        catch (Exception)
        {
            MessageBox("請確認輸入是否為數字");

        }

        heji.ValueText = (jp + jt + sf + qt ) + "";
    }
    
}
