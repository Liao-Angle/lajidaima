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
using WebServerProject;
using System.Data.OleDb;

public partial class Program_SCQ_Form_EH0108_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EH0108";
        AgentSchema = "WebServerProject.form.EH0108.EH0108Agent";
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
        SheetNo.Display = false;
        Subject.Display = false;

        //--------------------------部門選擇
        string sqlbm = @"select distinct PartNo from   [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart order by PartNo";
        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];

        if (dtbm.Rows.Count > 0)
        {

            int j = dtbm.Rows.Count;
            string[,] strs = new string[j + 1, 2];
            strs[0, 0] = "-1";
            strs[0, 1] = "未选择";

            for (int i = 0; i < j; i++)
            {
                strs[i + 1, 0] = dtbm.Rows[i][0].ToString();
                strs[i + 1, 1] = dtbm.Rows[i][0].ToString();
            }
            PartName.setListItem(strs);
        }
        else
        {
            PartName.setListItem(new string[1, 2] { { "0", "未查询到部门" } });
        }

        //--------------------------職務選擇

        string sqlzw = @"select distinct DtName from   [10.3.11.92\SQL2008].SCQHRDB.DBO.PerEmployee where DtName<>'' order by DtName";
        DataTable dtzw = engine.getDataSet(sqlzw, "zw").Tables["zw"];

        if (dtzw.Rows.Count > 0)
        {

            int j = dtzw.Rows.Count;
            string[,] strs = new string[j + 1, 2];
            strs[0, 0] = "-1";
            strs[0, 1] = "未选择";

            for (int i = 0; i < j; i++)
            {
                strs[i + 1, 0] = dtzw.Rows[i][0].ToString();
                strs[i + 1, 1] = dtzw.Rows[i][0].ToString();
            }
            DtName.setListItem(strs);
        }
        else
        {
            DtName.setListItem(new string[1, 2] { { "0", "未查询到職務" } });
        }

        //--------------------------類別選擇

        string sqllb = @"select distinct EmpTypeName from   [10.3.11.92\SQL2008].SCQHRDB.DBO.PerEmployee where EmpTypeName<>'' order by EmpTypeName";
        DataTable dtlb = engine.getDataSet(sqllb, "lb").Tables["lb"];

        if (dtlb.Rows.Count > 0)
        {

            int j = dtlb.Rows.Count;
            string[,] strs = new string[j + 1, 2];
            strs[0, 0] = "-1";
            strs[0, 1] = "未选择";

            for (int i = 0; i < j; i++)
            {
                strs[i + 1, 0] = dtlb.Rows[i][0].ToString();
                strs[i + 1, 1] = dtlb.Rows[i][0].ToString();
            }
            EmpType.setListItem(strs);
        }
        else
        {
            EmpType.setListItem(new string[1, 2] { { "0", "未查询到類別" } });
        }

        //--------------------------職等選擇

        string[,] sids = new string[6, 2] { { "一職等", "一職等" }, { "二職等", "二職等" }, { "三職等", "三職等" }, { "四職等", "四職等" }, { "五職等", "五職等" }, { "六職等", "六職等" } };
        Dt.setListItem(sids);

        


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
        Subject.ValueText = objects.getData("Subject");
        EmpName.ValueText = objects.getData("EmpName");
        PartName.ValueText = objects.getData("PartName");
        zszg.ValueText = objects.getData("zszg");
        DtName.ValueText = objects.getData("DtName");
        EmpType.ValueText = objects.getData("EmpType");
        bx.ValueText = objects.getData("bx");
        zgjg.ValueText = objects.getData("zgjg");
        zwjg.ValueText = objects.getData("zwjg");
        zyjg.ValueText = objects.getData("zyjg");
        jbbt.ValueText = objects.getData("jbbt");
        qx.ValueText = objects.getData("qx");
        jbfjs.ValueText = objects.getData("jbfjs");
        ComeDateY.ValueText = objects.getData("ComeDateY");
        TryDateB.ValueText = objects.getData("TryDateB");
        TryDateE.ValueText = objects.getData("TryDateE");
        zhize.ValueText = objects.getData("zhize");

        EmpName.ReadOnly = true;
        PartName.ReadOnly = true;
        zszg.ReadOnly = true;
        DtName.ReadOnly = true;
        EmpType.ReadOnly = true;
        bx.ReadOnly = true;
        zgjg.ReadOnly = true;
        zwjg.ReadOnly = true;
        zyjg.ReadOnly = true;
        jbbt.ReadOnly = true;
        qx.ReadOnly = true;
        jbfjs.ReadOnly = true;
        ComeDateY.ReadOnly = true;
        TryDateB.ReadOnly = true;
        TryDateE.ReadOnly = true;
        zhize.ReadOnly = true;
        //qx.ValueText = bx.ValueText + zgjg.ValueText + zyjg.ValueText + zwjg.ValueText + jbbt.ValueText;

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
            objects.setData("EmpName", EmpName.ValueText);
            objects.setData("PartName", PartName.ValueText);
            objects.setData("zszg", zszg.ValueText);
            objects.setData("DtName", DtName.ValueText);
            objects.setData("EmpType", EmpType.ValueText);
            objects.setData("bx", bx.ValueText);
            objects.setData("zgjg", zgjg.ValueText);
            objects.setData("zwjg", zwjg.ValueText);
            objects.setData("zyjg", zyjg.ValueText);
            objects.setData("jbbt", jbbt.ValueText);
            objects.setData("qx", qx.ValueText);
            objects.setData("jbfjs", jbfjs.ValueText);
            objects.setData("ComeDateY", ComeDateY.ValueText);
            objects.setData("TryDateB", TryDateB.ValueText);
            objects.setData("TryDateE", TryDateE.ValueText);
            objects.setData("zhize", zhize.ValueText);
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
            string subject = "【 人員任用單 】";
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
        string xml = "";
        string lb = EmpType.ValueText;
        string zd = Dt.ValueText;
        string iscw = "";
        string isdsz = "";

        if (lb != "不限36小時")
        {
            iscw = "1";
        }

        if (zd == "六職等")
        {
            isdsz = "1";
        }


        xml += "<EH0108>";
        //xml += "<bmzg DataType=\"java.lang.String\">" + bmzg + "</bmzg>";//申請人
        //xml += "<zszg DataType=\"java.lang.String\">" + zszg + "</usermrg>";//直屬主管
        //xml += "<cwzg DataType=\"java.lang.String\">" + cwzg + "</mrgmrg>";//上級主管
        xml += "<iscw DataType=\"java.lang.String\">" + iscw + "</iscw>";//財務是否簽核
        //xml += "<dsz DataType=\"java.lang.String\">" + dsz + "</fz>";//董事長
        xml += "<isdsz DataType=\"java.lang.String\">" + isdsz + "</isdsz>";//副總是否簽核
        xml += "</EH0108>";

        param["EH0108"] = xml;
        return "EH0108";


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
        if (backActID.ToUpper().Equals("bmzg"))
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
    protected void Page_Load(object sender, EventArgs e)
    {
        jbfjs.ValueText = bx.ValueText;
    }
}
