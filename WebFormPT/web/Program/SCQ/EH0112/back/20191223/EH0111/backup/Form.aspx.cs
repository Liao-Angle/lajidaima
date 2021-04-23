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
using System.Data.SqlClient;
using DSCWebControl;

public partial class Program_SCQ_Form_EH0111_Form : SmpBasicFormPage
{
    private int MaxDSC = 4;
    private string DSCName = "DSCCheckBox";
    protected override void init()
    {
        ProcessPageID = "EH0111";
        AgentSchema = "WebServerProject.form.EH0110.EH0110Agent";
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
        try
        {

            string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart";
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

        //--------------------------部門選擇
        string sqlb = @"select distinct PartNo from   [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart 
                        where PartNo not like '%支援%' and  PartNo not like '%駐廠%' and PartNo <>'6M0303-QS'  order by PartNo";
        DataTable dtb = engine.getDataSet(sqlb, "bmE").Tables["bmE"];

        if (dtb.Rows.Count > 0)
        {

            int j = dtb.Rows.Count;
            string[,] strs = new string[j + 1, 2];
            strs[0, 0] = "-1";
            strs[0, 1] = "請選擇";

            for (int i = 0; i < j; i++)
            {
                strs[i + 1, 0] = dtb.Rows[i][0].ToString();
                strs[i + 1, 1] = dtb.Rows[i][0].ToString();
            }
            NPartNo.setListItem(strs);
        }
        else
        {
            NPartNo.setListItem(new string[1, 2] { { "0", "未查询到部门" } });
        }

        //--------------------------職務選擇

        string[,] zw = new string[11, 2] { { "請選擇", "請選擇" }, { "作業員", "作業員" }, { "辦事員", "辦事員" }, { "領班", "領班" }, { "技術員", "技術員" }, { "組長", "組長" }, { "副工程師", "副工程師" }, { "副管理師", "副管理師" }, { "工程師", "工程師" }, { "管理師", "管理師" }, { "副主任", "副主任" } };
        NDtName.setListItem(zw);

        //--------------------------親屬關係

        string[,] qs = new string[5, 2] { { "無", "無" }, { "親戚", "親戚" }, { "朋友", "朋友" }, { "同學", "同學" }, { "夫妻", "夫妻" } };
        qinshu.setListItem(qs);

        //--------------------------上下級關係

        string[,] sx = new string[2, 2] { { "否", "否" }, { "是", "是" } };
        sxj.setListItem(sx);

        //--------------------------職等選擇

        string[,] sids = new string[4, 2] { { "請選擇", "請選擇" }, { "一職等", "一職等" }, { "二職等", "二職等" }, { "三職等", "三職等" } };
        NDt.setListItem(sids);

        //--------------------------職等選擇

        string[,] tx = new string[5, 2] { { "00", "請選擇" }, { "01", "過試用期" }, { "02", "定期調整" }, { "03", "升遷調整" }, { "06", "其它" } };
        txlx.setListItem(tx);
        //--------------------------薪資默認
        string[,] bxstr = new string[13, 2] { { "0", "0" }, { "1500", "1500" }, { "1600", "1600" }, { "1650", "1650" }, { "1870", "1870" }, { "2090", "2090" }, { "2310", "2310" }, { "2420", "2420" }, { "2530", "2530" }, { "2750", "2750" }, { "2640", "2640" }, { "2860", "2860" }, { "2970", "2970" } };
        Nbx.setListItem(bxstr);
        string[,] zwstr = new string[4, 2] { { "0", "0" }, { "120", "120" }, { "300", "300" }, { "600", "600" } };
        Nzwjg.setListItem(zwstr);
        string[,] zgstr = new string[6, 2] { { "0", "0" }, { "100", "100" }, { "200", "200" }, { "300", "300" }, { "500", "500" }, { "800", "800" } };
        Nzgjg.setListItem(zgstr);
        Nzyjg.ValueText = "0";
        Njbbt.ValueText = "0";
        Ojbbt.ValueText = "0";
        Ojs.ValueText = Obx.ValueText;
        Njbbt.ReadOnly = true;
        OPartNo.ReadOnly = true;
        ODtName.ReadOnly = true;
        Ojbbt.ReadOnly = true;
        Ojs.ReadOnly = true;
        ODt.ReadOnly = true;
        if(qinshu.ValueText=="無" && sxj.ValueText=="否")
        {
            qempno.ReadOnly = true;
            qempname.ReadOnly = true;
            qdtname.ReadOnly = true;
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
        base.showData(engine, objects);
        string actName = (string)getSession("ACTName");
        string check = objects.getData("Privilege");
        for (int i = 1; i <= MaxDSC; i++)
        {
            DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
            if (dsc != null && i <= check.Length && check.Substring(i - 1, 1) == "Y")
            {
                dsc.Checked = true;
            }
            if (dsc != null)
            {
                dsc.ReadOnly = true;
            }
        }
        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        ComeDate.ValueText = objects.getData("ComeDate");
        py.ValueText = objects.getData("py");
        NPartNo.ValueText = objects.getData("NPartNo");
        NDtName.ValueText = objects.getData("NDtName");
        NDt.ValueText = objects.getData("NDt");
        Owork.ValueText = objects.getData("Owork");
        Nwork.ValueText = objects.getData("Nwork");
        txlx.ValueText = objects.getData("txlx");
        Etime.ValueText = objects.getData("Etime");
        qinshu.ValueText = objects.getData("qinshu");
        sxj.ValueText = objects.getData("sxj");
        qempno.ValueText = objects.getData("qempno");
        qempname.ValueText = objects.getData("qempname");
        qdtname.ValueText = objects.getData("qdtname");
        //zhize.ValueText = objects.getData("zhize");
        //SingleField3.ValueText = objects.getData("SingleField3");
        OldInfo(engine);

        if (actName.ToString() == "HR承辦" || actName.ToString() == "通知")
        {
            HRXZ.Visible = true;
        }

        OPartNo.ReadOnly = true;
        ODtName.ReadOnly = true;
        Obx.ReadOnly = true; 
        Ozgjg.ReadOnly = true;
        Ozwjg.ReadOnly = true;
        Ozyjg.ReadOnly = true;
        Ojbbt.ReadOnly = true;
        Oqx.ReadOnly = true; 
        Ojs.ReadOnly = true;
        Nbx.ReadOnly = true;
        Nzgjg.ReadOnly = true;
        Nzwjg.ReadOnly = true;
        //Nzyjg.ReadOnly = true;
        Njbbt.ReadOnly = true;
        Nqx.ReadOnly = true;
        Njs.ReadOnly = true;
        movelist.ReadOnly = true;

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
            string p = string.Empty;
            for (int i = 1; i <= MaxDSC; i++)
            {
                DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
                if (dsc != null && dsc.Checked)
                {
                    p += "Y";
                }
                else
                {
                    p += "N";
                }
            }
            objects.setData("Privilege", p);
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("ComeDate", ComeDate.ValueText);
            objects.setData("py", py.ValueText);
            objects.setData("NPartNo", NPartNo.ValueText);
            objects.setData("NDtName", NDtName.ValueText);
            objects.setData("NDt", NDt.ValueText);
            //objects.setData("NEmpTp", NEmpTp.ValueText);
            objects.setData("Obx", Obx.ValueText);
            objects.setData("Ozgjg", Ozgjg.ValueText);
            objects.setData("Ozwjg", Ozwjg.ValueText);
            objects.setData("Ozyjg", Ozyjg.ValueText);
            objects.setData("Ojbbt", Ojbbt.ValueText);
            objects.setData("Oqx", Oqx.ValueText);
            objects.setData("Ojs", Ojs.ValueText);
            objects.setData("Nbx", Nbx.ValueText);
            objects.setData("Nzgjg", Nzgjg.ValueText);
            objects.setData("Nzwjg", Nzwjg.ValueText);
            objects.setData("Nzyjg", Nzyjg.ValueText);
            objects.setData("Njbbt", Njbbt.ValueText);
            objects.setData("Nqx", Nqx.ValueText);
            objects.setData("Njs", Njs.ValueText);
            objects.setData("Owork", Owork.ValueText);
            objects.setData("Nwork", Nwork.ValueText);
            objects.setData("qinshu", qinshu.ValueText);
            objects.setData("sxj", sxj.ValueText);
            objects.setData("qempno", qempno.ValueText);
            objects.setData("qempname", qempname.ValueText);
            objects.setData("qdtname", qdtname.ValueText);
            objects.setData("movelist", movelist.ValueText);
            objects.setData("ForwardHR", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");




            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

        }
        else
        {
            objects.setData("txlx", txlx.ValueText);
            objects.setData("Etime", Etime.ValueText);
            objects.setData("Nzyjg", Nzyjg.ValueText);
            objects.setData("Nqx", Nqx.ValueText);
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
            string subject = "【 人員異動單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }
        }
        if (Obx.ValueText == "")
        {
            MessageBox("請解密薪資信息");
            result = false;
        }
        if (qinshu.ValueText != "無" || sxj.ValueText != "否")
        {
            if (qempno.ValueText == "")
            {
                MessageBox("請填寫關係人工號");
                result = false;
            }
        }
        if (NDtName.ValueText == "作業員" && NDt.ValueText != "一職等")
        {
            MessageBox("作業員為一職等員工");
            result = false;
        }
        if (NDtName.ValueText == "辦事員" && NDt.ValueText != "二職等")
        {
            MessageBox("辦事員為二職等員工");
            result = false;
        }
        if (NDtName.ValueText == "技術員" && NDt.ValueText != "二職等")
        {
            MessageBox("技術員為二職等員工");
            result = false;
        }
        if (NDtName.ValueText == "領班" && NDt.ValueText != "二職等")
        {
            MessageBox("領班為二職等員工");
            result = false;
        }
        if (NDtName.ValueText == "組長" && NDt.ValueText != "二職等")
        {
            MessageBox("組長為二職等員工");
            result = false;
        }
        if (NDtName.ValueText == "副工程師" && NDt.ValueText != "三職等")
        {
            MessageBox("副工程師為三職等員工");
            result = false;
        }
        if (NDtName.ValueText == "副管理師" && NDt.ValueText != "三職等")
        {
            MessageBox("副管理師為三職等員工");
            result = false;
        }
        if (NDtName.ValueText == "工程師" && NDt.ValueText != "三職等")
        {
            MessageBox("工程師為三職等員工");
            result = false;
        }
        if (NDtName.ValueText == "管理師" && NDt.ValueText != "三職等")
        {
            MessageBox("管理師為三職等員工");
            result = false;
        }
        if (NDtName.ValueText == "副主任" && NDt.ValueText != "三職等")
        {
            MessageBox("副主任為三職等員工");
            result = false;
        }
        if (NDt.ValueText == "一職等" && Convert.ToDouble(Nzyjg.ValueText) > 600)
        {
            MessageBox("一職等專業加給不得大於600");
            result = false;
        }
        if (NDt.ValueText == "二職等" && Convert.ToDouble(Nzyjg.ValueText) > 1800)
        {
            MessageBox("二職等專業加給不得大於1800");
            result = false;
        }
        if (NDt.ValueText == "三職等" && Convert.ToDouble(Nzyjg.ValueText) > 1800)
        {
            MessageBox("三職等專業加給不得大於1800");
            result = false;
        }
        if (NPartNo.ValueText == "-1")
        {
            MessageBox("請選擇異動后部門");
            result = false;
        }
        if (DSCCheckBox1.Checked == false && DSCCheckBox2.Checked == false && DSCCheckBox3.Checked == false && DSCCheckBox4.Checked == false)
        {
            MessageBox("請選擇調任類型");
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
        string isbm = "";
        string issj = "";
        string isold = "1";
        string bmzg = "";
        string mrgmrg = "";
        string oldmrg = "";
        //string org="";
        string zjl = "Q1508126";
        string creatorId = si.fillerID;  //填表人
        string[] values = base.getUserManagerInfoID(engine, creatorId);
        string[] BmInfo = base.getOrgUnit(engine, si.ownerOrgID);
        Hashtable h1 = base.getHRUsers(engine, EmpNo.ValueText);
        //org = h1["PartNo"].ToString().Substring(0, 7);
        string oldbmorg = h1["PartNo"].ToString().Substring(0, 7);
        string oldbm = base.getOrgUnit(engine,oldbmorg)[3];

        if (base.getUserManagerInfoID(engine, oldbm)[1]==zjl)
        {
            oldmrg = oldbm;
        }
        else
        {
            oldmrg=base.getUserManagerInfoID(engine, oldbm)[1];
        }

        if (creatorId != BmInfo[3])
        {
            bmzg = values[1];
            mrgmrg = base.getUserManagerInfoID(engine, bmzg)[1];
            isbm = "1";

            if (mrgmrg != zjl)
            {
                issj = "1";
            }
            if (oldmrg == creatorId || oldmrg == bmzg || oldmrg == mrgmrg)
            {
                isold = "";
            }

        }
        else
        {
            isbm = "";
            if(values[1]!=zjl)
            {
                mrgmrg = values[1];
                issj = "1";
            }
            if (oldmrg == creatorId || oldmrg == mrgmrg)
            {
                isold = "";
            }
        }
       

        xml += "<EH0111>";
        xml += "<bmzg DataType=\"java.lang.String\">" + bmzg + "</bmzg>";//部門主管
        xml += "<mrgmrg DataType=\"java.lang.String\">" + mrgmrg + "</mrgmrg>";//部門最高主管
        xml += "<oldmrg DataType=\"java.lang.String\">" + oldmrg + "</oldmrg>";//原部門最高主管
        xml += "<isbm DataType=\"java.lang.String\">" + isbm + "</isbm>";//部門是否簽核
        xml += "<issj DataType=\"java.lang.String\">" + issj + "</issj>";//部門最高主管是否簽核
        xml += "<isold DataType=\"java.lang.String\">" + isold + "</isold>";//部門最高主管是否簽核
        xml += "</EH0111>";

        writeLog("xml: " + xml);

        param["EH0111"] = xml;
        return "EH0111";
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
    //列印單據
    protected void PrintButton_OnClick(object sender, EventArgs e)
    {
        //MessageBox("SheetNo : " + SheetNo.ValueText);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string connectionString = "Data Source=10.3.11.92\\SQL2008;Initial Catalog=SCQHRDB;User ID=sa;Password=simplohr";
        string sql = "exec ecp_empchange '" + EmpNo.ValueText + "','" + System.DateTime.Now.ToString("yyyyMMdd") + "'";
        SqlConnection conn = new SqlConnection(connectionString);

        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);

        int result = cmd.ExecuteNonQuery();
        conn.Close();
        Session["EH0111_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
        base.showOpenWindow(url, "列印任用單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }

    private void OldInfo(AbstractEngine engine)
    {
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine hrengine = factory.getEngine(engineType, connectString);

            Hashtable h1 = base.getHRUsers(hrengine, EmpNo.ValueText);
            string a = h1["ComeDate"].ToString().Replace("上午 12:00:00","");
            ComeDate.ValueText = a;
            OPartNo.ValueText = h1["PartNo"].ToString();
            ODtName.ValueText = h1["DtName"].ToString();
            //OEmpTp.ValueText = h1["EmpTypeName"].ToString();
            if (h1["DtName"].ToString() == "作業員")
            {
                ODt.ValueText = "一職等";
            }
            if (h1["DtName"].ToString() == "領班" || h1["DtName"].ToString() == "辦事員" || h1["DtName"].ToString() == "技術員" || h1["DtName"].ToString() == "司機"||h1["DtName"].ToString() == "組長")
            {
                ODt.ValueText = "二職等";
            }
            if (h1["DtName"].ToString() == "工程師" || h1["DtName"].ToString() == "副工程師" || h1["DtName"].ToString() == "副管理師" || h1["DtName"].ToString() == "管理師" || h1["DtName"].ToString() == "副主任")
            {
                ODt.ValueText = "三職等";
            }

            Hashtable H2 = base.getJC(hrengine,EmpNo.ValueText);

            jdg.ValueText = H2["dg"].ToString();
            jxg.ValueText = H2["xg"].ToString();
            jj.ValueText = H2["jj"].ToString();
            fdg.ValueText = H2["dgg"].ToString();
            fxg.ValueText = H2["xgg"].ToString();
            sj.ValueText = H2["sj"].ToString();
            
            Hashtable H3 = base.getLeave(hrengine, EmpNo.ValueText);
            SingleField1.ValueText = H3["sj"].ToString().Substring(0,4);
            SingleField2.ValueText = H3["bj"].ToString().Substring(0, 3);
            SingleField4.ValueText = H3["kg"].ToString().Substring(0, 3);

            Hashtable H4 = base.getHRjx(hrengine, EmpNo.ValueText);
            Mkh.ValueText = H4["Field004"].ToString();
            Ekh.ValueText = H4["Field005"].ToString();

            
        }
        catch (Exception ex)
        {
            throw new Exception("查詢info錯誤, " + ex.Message.ToString());
        }

    }

    protected void JEmpNo_SingleOpenWindowButtonClick(string[,] values)
    {

        try
        {
            
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            string connectionString = "Data Source=10.3.11.92\\SQL2008;Initial Catalog=SCQHRDB;User ID=sa;Password=simplohr";
            string sql = "exec ecp_empchange '" + EmpNo.ValueText + "','" + System.DateTime.Now.ToString("yyyyMMdd") + "'";
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            int result = cmd.ExecuteNonQuery();
            conn.Close();

            Hashtable H5 = base.getHRtx(engine, EmpNo.ValueText);
            string ms = H5["valued"].ToString();
            string msg = "";
            if (ms.Length < 130)
            {
                msg = ms + "                                                              ";
            }
            else
            {
                msg = ms;
            }
            string[] sArray = ms.Split('-');
            string msg2 = "";
            foreach (string i in sArray)
            {
                msg2 = i.ToString();
            }

            Hashtable h1 = base.getHRUsers(engine, EmpNo.ValueText);
            
            string Emp = EmpNo.ValueText;
            Hashtable H6 = base.getHRtx(engine, EmpNo.ValueText);
            string bm = h1["PartNo"].ToString();

            if (bm.Contains(si.fillerOrgID.ToString().Substring(0,7)))
            {
                
                Obx.ValueText = H6["benxin"].ToString();
                Ozgjg.ValueText = H6["zhuguanjiagei"].ToString();
                Ozwjg.ValueText = H6["zhiwujiagei"].ToString();
                Ozyjg.ValueText = H6["zhuanyejiagei"].ToString();
                Ojbbt.ValueText = "0";
                Oqx.ValueText = H6["quanxin"].ToString();
                Ojs.ValueText = H6["benxin"].ToString();

                //movelist.ValueText = msg.Substring(0, 42) + "\n" + msg.Substring(40, 50).Replace('-', ' ').TrimStart() + "\n" + msg.Substring(90, 50).Replace('-', ' ').TrimStart();

                movelist.ValueText = msg.Substring(0, 42) + "\n" + msg.Substring(40, 50).Replace('-', ' ').TrimStart() + "\n" + msg.Substring(90, 50).Replace('-', ' ').TrimStart() + "\n" + msg2;
                
            }
            else
            {
                Obx.ValueText = "";
                Ozgjg.ValueText = "";
                Ozwjg.ValueText = "";
                Ozyjg.ValueText = "";
                Ojbbt.ValueText = "0";
                Oqx.ValueText = "";
                Ojs.ValueText = "";
                movelist.ValueText = "";
            }
            OldInfo(engine);


        }
        catch (Exception ex)
        {
            throw new Exception("查詢HRUSERS錯誤, " + ex.Message.ToString());
        }

        

    }

    protected void xz_Click1(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        Hashtable H7 = base.getjiemi(engine, EmpNo.ValueText);
        if (SingleField3.ValueText == "38899")
        {
            Obx.ValueText = H7["Obx"].ToString();
            Ozgjg.ValueText = H7["Ozgjg"].ToString();
            Ozwjg.ValueText = H7["Ozwjg"].ToString();
            Ozyjg.ValueText = H7["Ozyjg"].ToString();
            Ojbbt.ValueText = H7["Ojbbt"].ToString();
            Oqx.ValueText = H7["Oqx"].ToString();
            Ojs.ValueText = H7["Ojs"].ToString();
            Nbx.ValueText = H7["Nbx"].ToString();
            Nzgjg.ValueText = H7["Nzgjg"].ToString();
            Nzwjg.ValueText = H7["Nzwjg"].ToString();
            Nzyjg.ValueText = H7["Nzyjg"].ToString();
            Njbbt.ValueText = H7["Njbbt"].ToString();
            Nqx.ValueText = H7["Nqx"].ToString();
            Njs.ValueText = H7["Njs"].ToString();
            movelist.ValueText = H7["movelist"].ToString();
        }
        else 
        {
            MessageBox("請核對您的密碼是否正確");
        }
        
        
    }

    string[,] bxstr1 = new string[4, 2] { { "1500", "1500" }, { "1650", "1650" }, { "1600", "1600" }, { "1870", "1870" } };
    string[,] bxstr2 = new string[6, 2] { { "1870", "1870" }, { "2090", "2090" }, { "2310", "2310" }, { "2420", "2420" }, { "2530", "2530" }, { "2750", "2750" } };
    string[,] bxstr3 = new string[6, 2] { { "2420", "2420" }, { "2530", "2530" }, { "2640", "2640" }, { "2750", "2750" }, { "2860", "2860" }, { "2970", "2970" } };
    string[,] zwstr1 = new string[61, 2] { { "0", "0" }, { "5", "5" }, { "10", "10" }, { "15", "15" }, { "20", "20" }, { "25", "25" }, { "30", "30" }, { "35", "35" }, { "40", "40" }, { "45", "45" }, { "50", "50" }, { "55", "55" }, { "60", "60" }, { "65", "65" }, { "70", "70" }, { "75", "75" }, { "80", "80" }, { "85", "85" }, { "90", "90" }, { "95", "95" }, { "100", "100" }, { "105", "105" }, { "110", "110" }, { "115", "115" }, { "120", "120" }, { "125", "125" }, { "130", "130" }, { "135", "135" }, { "140", "140" }, { "145", "145" }, { "150", "150" }, { "155", "155" }, { "160", "160" }, { "165", "165" }, { "170", "170" }, { "175", "175" }, { "180", "180" }, { "185", "185" }, { "190", "190" }, { "195", "195" }, { "200", "200" }, { "205", "205" }, { "210", "210" }, { "215", "215" }, { "220", "220" }, { "225", "225" }, { "230", "230" }, { "235", "235" }, { "240", "240" }, { "245", "245" }, { "250", "250" }, { "255", "255" }, { "260", "260" }, { "265", "265" }, { "270", "270" }, { "275", "275" }, { "280", "280" }, { "285", "285" }, { "290", "290" }, { "295", "295" }, { "300", "300" } };
    string[,] zwstr2 = new string[3, 2] { { "300", "300" }, { "500", "500" }, { "1000", "1000" } };
    string[,] zwstr3 = new string[3, 2] { { "500", "500" }, { "1000", "1000" }, { "1500", "1500" } };
    string[,] zgstr0 = new string[1, 2] { { "0", "0" } };
    string[,] zgstr1 = new string[3, 2] { { "100", "100" }, { "300", "300" }, { "500", "500" } };
    string[,] zgstr2 = new string[3, 2] { { "200", "200" }, { "500", "500" }, { "800", "800" } };
    protected void Dt_SelectChanged(string value)
    {
        try
        {
            if (NDt.ValueText.Equals("一職等"))
            {
                Nbx.setListItem(bxstr1);
                Nzwjg.setListItem(zwstr1);
                Nzgjg.setListItem(zgstr0);
                Nzyjg.ValueText = "0";
            }


            if (NDt.ValueText == "二職等")
            {
                if (NDtName.ValueText == "領班" || NDtName.ValueText == "組長")
                {
                    Nbx.setListItem(bxstr2);
                    Nzwjg.setListItem(zwstr2);
                    Nzgjg.setListItem(zgstr1);

                }
                else
                {
                    Nbx.setListItem(bxstr2);
                    Nzwjg.setListItem(zwstr2);
                    Nzgjg.setListItem(zgstr0);

                }

            }


            if (NDt.ValueText == "三職等")
            {
                if (NDtName.ValueText == "副主任")
                {
                    Nbx.setListItem(bxstr3);
                    Nzwjg.setListItem(zwstr3);
                    Nzgjg.setListItem(zgstr2);
                }
                else
                {
                    Nbx.setListItem(bxstr3);
                    Nzwjg.setListItem(zwstr3);
                    Nzgjg.setListItem(zgstr0);
                }
            }

            if (NDt.ValueText == "請選擇")
                MessageBox("請選擇");

            getQX();
        }
        catch (Exception)
        {
            MessageBox("請選擇職等");
        }
    }
    private void getQX()
    {
        double bxNUM = 0;
        double zgjgNUM = 0;
        double zwjgNUM = 0;
        double zyjgNUM = 0;
        double jbbtNUM = 0;

        try
        {
            if (!string.IsNullOrEmpty(Nbx.ValueText))
            {
                bxNUM = Convert.ToDouble(Nbx.ValueText);
            }
            if (!string.IsNullOrEmpty(Nzgjg.ValueText))
            {
                zgjgNUM = Convert.ToDouble(Nzgjg.ValueText);
            }
            if (!string.IsNullOrEmpty(Nzwjg.ValueText))
            {
                zwjgNUM = Convert.ToDouble(Nzwjg.ValueText);
            }
            if (!string.IsNullOrEmpty(Nzyjg.ValueText))
            {
                zyjgNUM = Convert.ToDouble(Nzyjg.ValueText);
            }
            if (NDt.ValueText == "一職等" && Convert.ToDouble(Nzyjg.ValueText) > 600)
            {
                MessageBox("一職等專業加給不得大於600");
                Nzyjg.ValueText = "0";
            }
            if (NDt.ValueText == "二職等" && Convert.ToDouble(Nzyjg.ValueText) > 1800)
            {
                MessageBox("二職等專業加給不得大於1800");
                Nzyjg.ValueText = "0";

            }
            if (NDt.ValueText == "三職等" && Convert.ToDouble(Nzyjg.ValueText) > 1800)
            {
                MessageBox("三職等專業加給不得大於1800");
                Nzyjg.ValueText = "0";

            }

        }
        catch (Exception)
        {
            MessageBox("不是一個數字");

        }

        Nqx.ValueText = (bxNUM + zgjgNUM + zwjgNUM + zyjgNUM + jbbtNUM) + "";
        Njs.ValueText = bxNUM + "";
    }
    protected void zy_Click(object sender, EventArgs e)
    {
        getQX();
    }
    protected void sxj_SelectChanged(string value)
    {
        if (qinshu.ValueText != "無" || sxj.ValueText != "否")
        {
            qempno.ReadOnly = false;
        }
        else
        {
            qempno.ReadOnly = true; 
        }
        
    }
    protected void qempno_TextChanged(object sender, EventArgs e)
    {
        try
        {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        Hashtable H9 = base.getHRUsers(engine, qempno.ValueText);
        if (qempno.ValueText != "")
        {
            qempname.ValueText = H9["EmpName"].ToString();
            qdtname.ValueText = H9["DtName"].ToString();

        }
        else { MessageBox("請輸入工號"); }
        }
        catch (Exception) { MessageBox("請輸入正確工號"); }
       
    }
    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\EH0111.log", true, System.Text.Encoding.Default);
            sw.WriteLine(line);
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
