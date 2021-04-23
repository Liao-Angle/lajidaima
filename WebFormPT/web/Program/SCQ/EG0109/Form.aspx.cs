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

public partial class Program_SCQ_EG0109_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EG0109";
        AgentSchema = "WebServerProject.form.EG0109.EG0109Agent";
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
        bool isAddNew = base.isNew(); //base 父類別   
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string userId = (string)Session["UserId"];
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        SheetNo.Display = false;
        Subject.Display = false;
        sfEmpName.Display = false;
        ts.ReadOnly = true;

        //類型

        string[,] lx = new string[3, 2] { { "支援", "支援" }, { "支援(無卡號)", "支援(無卡號)" }, { "駐廠", "駐廠" } };
        Rqtype.setListItem(lx);

        //性別
        string[,] SexName = new string[,] { { "男", "男" }, { "女", "女" } };
        Sex.setListItem(SexName);

        //類別
        string[,] lb = new string[,] { { "請選擇", "請選擇" }, { "臺派", "臺派" }, { "責任制", "責任制" }, { "非責任制", "非責任制" } };
        EmpType.setListItem(lb);

        //靜電帽
        string[,] mz = new string[,] { { "否", "否" }, { "是", "是" } };
        Jdm.setListItem(mz);

        //工衣
        string[,] gy = new string[,] { {"否","否"},{ "靜電衣(S)", "靜電衣(S)" }, { "靜電衣(M)", "靜電衣(M)" }, { "靜電衣(L)", "靜電衣(L)" }, { "靜電衣(XL)", "靜電衣(XL)" }, { "靜電衣(XXL)", "靜電衣(XXL)" }, { "靜電衣(XXXL)", "靜電衣(XXXL)" }, { "靜電衣(XXXXL)", "靜電衣(XXXXL)" }, { "夏裝(S)", "夏裝(S)" }, { "夏裝(M)", "夏裝(M)" }, { "夏裝(L)", "夏裝(L)" }, { "夏裝(XL)", "夏裝(XL)" }, { "夏裝(XXL)", "夏裝(XXL)" }, { "夏裝(XXXL)", "夏裝(XXXL)" } };
        Gyi.setListItem(gy);

        //靜電鞋
        string[,] xz = new string[,] { { "否", "否" }, { "33", "33" }, { "34", "34" }, { "35", "35" }, { "36", "36" }, { "37", "37" }, { "38", "38" }, { "39", "39" }, { "40", "40" }, { "41", "41" }, { "42", "42" }, { "43", "43" }, { "44", "44" }, { "45", "45" } };
        Jdx.setListItem(xz);

        //住宿
        string[,] zsf = new string[,] { { "否", "否" }, { "是", "是" } };
        ZS.setListItem(zsf);

        //用餐
        string[,] ycf = new string[,] { { "否", "否" }, { "是", "是" } };
        YC.setListItem(ycf);

        //職務選擇
        string sqldt = @"select distinct DtName FROM HRUSERS";
        DataTable dt = engine.getDataSet(sqldt, "dt").Tables["dt"];

        if (dt.Rows.Count > 0)
        {

            int j = dt.Rows.Count;
            string[,] strs = new string[j + 1, 2];
            strs[0, 0] = "-1";
            strs[0, 1] = "請選擇";

            for (int i = 0; i < j; i++)
            {
                strs[i + 1, 0] = dt.Rows[i][0].ToString();
                strs[i + 1, 1] = dt.Rows[i][0].ToString();
            }
            DtName.setListItem(strs);
        }
        else
        {
            DtName.setListItem(new string[1, 2] { { "0", "未查询到職務" } });
        }


        //職務選擇
        string sqldbm = @"select distinct PartNo FROM HRUSERS order by PartNo";
        DataTable bm = engine.getDataSet(sqldbm, "dt").Tables["dt"];

        if (bm.Rows.Count > 0)
        {

            int j = bm.Rows.Count;
            string[,] strs = new string[j + 1, 2];
            strs[0, 0] = "-1";
            strs[0, 1] = "請選擇";

            for (int i = 0; i < j; i++)
            {
                strs[i + 1, 0] = bm.Rows[i][0].ToString();
                strs[i + 1, 1] = bm.Rows[i][0].ToString();
            }
            PartNo.setListItem(strs);
        }
        else
        {
            PartNo.setListItem(new string[1, 2] { { "0", "未查询到部门" } });
        }

        //申請人員 
        if (Rqtype.ValueText.Equals("支援"))
        {
            LBEmpNo.Text = "申請人信息";
            EmpNo.clientEngineType = engineType;
            EmpNo.connectDBString = connectString;

            sfEmpName.Display = false;
            EmpNo.Enabled = true;
            EmpNo.Display = true;
            sfEmpName.Width = Unit.Pixel(150);
            Company.ReadOnly = true;
        }
        else
        {
            LBEmpNo.Text = "中文名";
            EmpNo.Enabled = false;
            EmpNo.Display = false;
            sfEmpName.Display = true;
            sfEmpName.Width = Unit.Pixel(150);

        }

        if (!isAddNew)
        {
            Company.ReadOnly = true;
            Rqtype.ReadOnly = true;
            EmpNo.ReadOnly = true;
            sfEmpName.ReadOnly = true;
            Ename.ReadOnly = true;
            Sex.ReadOnly = true;
            PartNo.ReadOnly = true;
            ccSDate.ReadOnly = true;
            ccEDate.ReadOnly = true;
            ts.ReadOnly = true;
            DtName.ReadOnly = true;
            EmpType.ReadOnly = true;
            Jdm.ReadOnly = true;
            Gyi.ReadOnly = true;
            Jdx.ReadOnly = true;
            ZS.ReadOnly = true;
            YC.ReadOnly = true;
            PID.ReadOnly = true;
            CardID.ReadOnly = true;
            qtbz.ReadOnly = true;

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
        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        Rqtype.ValueText = objects.getData("Rqtype");
        Company.ValueText = objects.getData("Company");
        DtName.ValueText = objects.getData("DtName");
        sfEmpName.ValueText = objects.getData("sfEmpName");
        EmpType.ValueText = objects.getData("EmpType");
        Sex.ValueText = objects.getData("Sex");
        PartNo.ValueText = objects.getData("PartNo");
        ccSDate.ValueText = objects.getData("ccSDate");
        ccEDate.ValueText = objects.getData("ccEDate");
        ts.ValueText = objects.getData("ts");
        Jdm.ValueText = objects.getData("Jdm");
        Gyi.ValueText = objects.getData("Gyi");
        Jdx.ValueText = objects.getData("Jdx");
        ZS.ValueText = objects.getData("ZS");
        YC.ValueText = objects.getData("YC");
        PID.ValueText = objects.getData("PID");
        CardID.ValueText = objects.getData("CardID");

        EmpNo.ReadOnly = true;
        Rqtype.ReadOnly = true;
        Company.ReadOnly = true;
        DtName.ReadOnly = true;
        sfEmpName.ReadOnly = true;
        EmpType.ReadOnly = true;
        Sex.ReadOnly = true;
        PartNo.ReadOnly = true;
        ccSDate.ReadOnly = true;
        ccEDate.ReadOnly = true;
        ts.ReadOnly = true;
        Jdm.ReadOnly = true;
        Gyi.ReadOnly = true;
        Jdx.ReadOnly = true;
        ZS.ReadOnly = true;
        YC.ReadOnly = true;
        CardID.ReadOnly = true;

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
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("PartNo", PartNo.ValueText);
            objects.setData("Rqtype", Rqtype.ValueText);
            objects.setData("Company", Company.ValueText);
            objects.setData("DtName", DtName.ValueText);
            objects.setData("sfEmpName", sfEmpName.ValueText);
            objects.setData("EmpType", EmpType.ValueText);
            objects.setData("Sex", Sex.ValueText);
            objects.setData("ccSDate", ccSDate.ValueText);
            objects.setData("ccEDate", ccEDate.ValueText);
            objects.setData("ts", ts.ValueText);
            objects.setData("Jdm", Jdm.ValueText);
            objects.setData("Gyi", Gyi.ValueText);
            objects.setData("Jdx", Jdx.ValueText);
            objects.setData("ZS", ZS.ValueText);
            objects.setData("YC", YC.ValueText);
            objects.setData("CardID", CardID.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        else 
        {
            objects.setData("PID", PID.ValueText);
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
            string subject = "【 常駐支援人員申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
            }
        }
	//if (base.attachFile.dataSource.getAvailableDataObjectCount() <= 3)
        //{
        //    MessageBox ("附件需分開上傳4份，分別是：1.面談記錄表，2.人員需求申請單，3.組織架構圖，4.簡歷");
        //}
        if (base.attachFile.dataSource.getAvailableDataObjectCount() <1)
        {
            MessageBox("附件需上傳：出差申請單");
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

        Hashtable h = base.getTrvUsers(engine, EmpNo.ValueText);
        Ename.ValueText = h["EngName"].ToString();
        PID.ValueText = h["CardId"].ToString();
    }
    protected void Rqtype_SelectChanged(string value)
    {
        if (Rqtype.ValueText.Equals("支援"))
        {
            LBEmpNo.Text = "申請人信息";
            sfEmpName.Display = false;
            EmpNo.Enabled = true;
            EmpNo.Display = true;
            sfEmpName.Width = Unit.Pixel(150);
            Company.ReadOnly = true;
            PID.ReadOnly = true;
        }
        else if (Rqtype.ValueText.Equals("駐廠") || Rqtype.ValueText.Equals("支援(無卡號)"))
        {
            LBEmpNo.Text = "中文名";
            EmpNo.Enabled = false;
            EmpNo.Display = false;
            sfEmpName.Display = true;
            sfEmpName.Width = Unit.Pixel(150);

            Company.ValueText = "";
            EmpNo.ValueText = "";
            EmpNo.ReadOnlyValueText = "";
            PartNo.ValueText = "";
            Sex.ValueText = "男";
            PID.ValueText = "";
            DtName.ValueText = "";
            CardID.ValueText = "";
            Ename.ValueText = "";
            Company.ReadOnly = false;
        } 
    }
    protected void ccSDate_DateTimeClick(string values)
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
}