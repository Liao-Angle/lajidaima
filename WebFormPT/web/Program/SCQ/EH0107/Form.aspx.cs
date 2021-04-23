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
using System.Data.OleDb;

public partial class Program_SCQ_Form_EH0107_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EH0107";
        AgentSchema = "WebServerProject.form.EH0107.EH0107Agent";
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

        
        

        ///初始化上傳控件
        try
        {
            FileUpload1.FileAdapter = "薪資加扣項文件|*.xls";
            FileUpload1.engine = engine;
            FileUpload1.tempFolder = Server.MapPath("~/tempFolder/XZWork/");
            FileUpload1.readFile("");
            FileUpload1.updateTable();
        }
        catch (Exception ex) { MessageBox(ex.Message); }


        User.clientEngineType = engineType;
        User.connectDBString = connectString;
        User.ValueText = si.fillerID;
        User.doValidate();
        partNouser.ValueText = si.fillerOrgID;
        Cdate.ValueText = DateTime.Now.ToString("yyyy/MM/dd");
        User.ReadOnly = true;
        partNouser.ReadOnly = true;
        Cdate.ReadOnly = true;

        //if (isNew() || Session["UserID"].ToString() == si.fillerID)
        //{
        //    string[,] xgqhzg = new string[,]{ {"0","請選擇"},
        //        {"1","張華謙"},
        //        {"2","田軍祥"},	
        //        //{"3","許文坤"},
        //        };
        //    sqzszg.setListItem(xgqhzg);
        //    ///
        //    if (si.fillerOrgID == "NQM0101")
        //    {
        //        showzg.Visible = true;
        //    }
        //}

        string[,] str = new string[94, 2];
        str[0, 0] = "請選擇";
        str[0, 1] = "請選擇";
        for (int i = 1; i < 94; i++)
        {
            str[i, 0] = (i * 5).ToString();
            str[i, 1] = (i * 5).ToString();
        }
        sMoney.setListItem(str);
        Money.ReadOnly = true;


        //string[,] leibie = new string[4, 2];
        //leibie[0, 0] = "請選擇";
        //leibie[0, 1] = "請選擇";
        //leibie[1, 0] = "高溫補貼";
        //leibie[1, 1] = "高溫補貼";
        //leibie[2, 0] = "特殊崗位津貼";
        //leibie[2, 1] = "特殊崗位津貼";
        //leibie[3, 0] = "崗位補貼";
        //leibie[3, 1] = "崗位補貼";
        //Account.setListItem(leibie);
        string sqlaccNo = @"select AccNo,Account from dbo.EH0107C WHERE 1=1";
        if (si.fillerOrgID == "GIC2200" || si.fillerOrgID == "GQC2200")
        {
            sqlaccNo = sqlaccNo + "AND RqParNo IN('GA','ALL')";
        }
        else if (si.fillerOrgID == "GQC2100" || si.fillerOrgID == "GIC2100")
        {
            sqlaccNo = sqlaccNo + "AND RqParNo IN('HR','ALL')";
        }
        else if (si.fillerOrgID == "NQM0101" || si.fillerOrgID == "PIM0101" || si.fillerOrgID == "NQM0504")
        {
            sqlaccNo = sqlaccNo + "AND RqParNo IN('MFG','ALL')";
        }
        else if (si.fillerOrgID == "GQC2300"|| si.fillerOrgID == "GIC2300")
        {
            sqlaccNo = sqlaccNo + "AND RqParNo IN('MIS','ALL')";
        }
        else if (si.fillerOrgName.Contains("PCP"))
        {
            sqlaccNo = sqlaccNo + "AND RqParNo IN('PCP','ALL')";
        }
        else if (si.fillerOrgName.Contains("SMT"))
        {
            sqlaccNo = sqlaccNo + "AND RqParNo IN('SMT','ALL')";
        }
        else if (si.fillerOrgID == "NQM0301")
        {
            sqlaccNo = sqlaccNo + "AND RqParNo ('QC','ALL')";
        }
        else if (si.fillerOrgID == "NQM0302")
        {
            sqlaccNo = sqlaccNo + "AND RqParNo ('QA','ALL')";
        }
        else
        {
            sqlaccNo = "select AccNo,Account from dbo.EH0107C WHERE 1=1";
        }
        DataTable dtNO = engine.getDataSet(sqlaccNo, "bmE").Tables["bmE"];

        if (dtNO.Rows.Count > 0)
        {

            int j = dtNO.Rows.Count;
            string[,] strs = new string[j + 1, 2];
            strs[0, 0] = "-1";
            strs[0, 1] = "未选择";

            for (int i = 0; i < j; i++)
            {
                strs[i + 1, 0] = dtNO.Rows[i]["AccNo"].ToString();
                strs[i + 1, 1] = dtNO.Rows[i]["Account"].ToString();
            }
            Account.setListItem(strs);
        }

        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;

        string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + si.fillerID + "'";
        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];

        string sqlALL = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart";

        DataTable dtALL = engine.getDataSet(sqlALL, "bmE").Tables["bmE"];
        string SQLstr = "";

        try
        {
            if (si.fillerOrgID == "GIC2200" || si.fillerOrgID == "GQC2200" || si.fillerOrgID == "GQC2100" || si.fillerOrgID == "GIC2100"||si.fillerOrgID == "GQC2300"|| si.fillerOrgID == "GIC2300")
            {
                SQLstr = "(PartNo = '" + dtALL.Rows[0][0].ToString() + "' ";
                for (int i = 1; i < dtALL.Rows.Count; i++)
                {
                    SQLstr = SQLstr + " or PartNo = '" + dtALL.Rows[i][0].ToString() + "' ";
                } SQLstr = SQLstr + ")";
                EmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


            }
            else
            {
                SQLstr = "(PartNo = '" + dtbm.Rows[0][0].ToString() + "' ";
                for (int i = 1; i < dtbm.Rows.Count; i++)
                {
                    SQLstr = SQLstr + " or PartNo = '" + dtbm.Rows[i][0].ToString() + "' ";
                } SQLstr = SQLstr + ")";
                EmpNo.whereClause = SQLstr;
            }

            EmpNo.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "GIC2200" && si.fillerOrgID != "GQC2200" && si.fillerOrgID != "GQC2100" && si.fillerOrgID != "GIC2100" && si.fillerOrgID != "GQC2300" && si.fillerOrgID != "GIC2300")
            {
                EmpNo.whereClause = "PartNo like '%" + si.fillerOrgID + "%'";
            }
            else
            {
                EmpNo.whereClause = "1=1";
            }
            EmpNo.DoEventWhenNoKeyIn = false;
        }



        if (isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.EH0107.EH0107B");
            dos.setTableName("EH0107B");
            dos.loadFileSchema();
            objects.setChild("EH0107B", dos);
            LeaveDateList.dataSource = dos;
            LeaveDateList.updateTable();
        }
        LeaveDateList.clientEngineType = engineType;
        LeaveDateList.connectDBString = connectString;
        LeaveDateList.HiddenField = new string[] { "GUID", "SheetNo", "AccNo", "Remark" };


        //--------------------------以下页面2

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
            SingleDropDownList1.setListItem(strs);
        }
        else
        {
            SingleDropDownList1.setListItem(new string[1, 2] { { "0", "未查询到部门" } });
        }

        //--------------------------以下页面3

        string sqlxb = @"select distinct PerField6 from HRUSERS Where PerField6<>'' and " + SQLstr;
        DataTable dt = null;
        if (SQLstr == "")
        {
            dt = null;
        }
        else
        {
            dt = engine.getDataSet(sqlxb, "PEREMPLOYEE").Tables["PEREMPLOYEE"];
        }
        
        if (dt!=null && dt.Rows.Count > 0)
        {

            int j = dt.Rows.Count;
            string[,] str3 = new string[j + 1, 2];
            str3[0, 0] = "-1";
            str3[0, 1] = "未选择";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str3[i + 1, 0] = dt.Rows[i][0].ToString();
                str3[i + 1, 1] = dt.Rows[i][0].ToString();
            }

            SingleDropDownList2.setListItem(str3);

        }
        else
        {
            SingleDropDownList2.setListItem(new string[1, 2] { { "0", "未查询到线别" } });
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
        User.ValueText = objects.getData("SuserID");
        User.ReadOnlyValueText = objects.getData("SuserName");
        partNouser.ValueText = objects.getData("partNouser");
        Cdate.ValueText = objects.getData("CDate");
        Subject.ValueText = objects.getData("Subject");

        LeaveDateList.dataSource = objects.getChild("EH0107B");
        LeaveDateList.updateTable();

        Subject.Display = false;
        SheetNo.Display = false;

        User.ReadOnly = true;
        Cdate.ReadOnly = true;
        partNouser.ReadOnly = true;
        Note.ReadOnly = true;
        Remark.ReadOnly = true;


        User1S.Visible = false;
        User2S.Visible = false;
        User3S.Visible = false;
        User4S.Visible = false;
        User5S.Visible = false;

        LeaveDateList.IsShowCheckBox = false;
        LeaveDateList.ReadOnly = true;

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

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("SuserID", User.ValueText);
            objects.setData("SuserName", User.ReadOnlyValueText);
            objects.setData("partNouser", partNouser.ValueText);
            objects.setData("CDate", Cdate.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            //將B表匯進來
            foreach (DataObject obj in LeaveDateList.dataSource.getAllDataObjects())
            {
                obj.setData("SheetNo", objects.getData("SheetNo"));
            }

            objects.setChild("EH0107B", LeaveDateList.dataSource);
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

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        //if (Session["UserID"].ToString() == si.fillerID && si.fillerOrgID == "NQM0101" && sqzszg.ValueText == "0")
        //{
        //    pushErrorMessage("MFG-1 部門 請選擇簽核主管");
        //    result = false;
        //}
        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 薪資加扣項申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }
        }
        //金額卡控

        DataObject[] objs = LeaveDateList.dataSource.getAllDataObjects();
        string temp1String = string.Empty;
        string temp2String = string.Empty;
        for (int i = 0; i < objs.Length; i++)
        {
            if (i == 0)
            {
                temp2String = objs[i].getData("Account");
            }
            else
            {
                if (objs[i].getData("Account") != temp2String)
                {
                    temp1String = "同一申請單只能申請同一種科目類型;";
                    break;
                }
            }

            Hashtable h1 = base.getHRUsers(engine, objs[i].getData("EmpNo"));
            string dtn = h1["DtName"].ToString();
            string dt = "";
            string lx=objs[i].getData("Account");
            string gwn = objs[i].getData("Gwname");
            string zhiji = h1["LevelLevel"].ToString();
            string gw = "";
            if (gwn.Contains("多能工"))
            {
                gw = "多能工";
            }
            else if (gwn == "儲備" || dtn == "儲備" || zhiji == "1-3級")
            {
                dt = "儲備";
            }
            else
            {
                gw = gwn;
            }

            string gt=objs[i].getData("Gwtype");
            double money=Convert.ToDouble(objs[i].getData("Money"));

            if (lx == "高溫補貼"&&money>465)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }

            if (lx == "特殊崗位津貼" && dt == "儲備" && gt == "一般崗位" && money>200)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && dt != "儲備" && gw != "多能工" && gt == "一般崗位" && money > 50)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && dt == "儲備" && gt == "特殊崗位" && money > 200)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && dt == "儲備" && gt == "重點崗位" && money > 200)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && dt == "儲備" && gt == "特殊崗位+重點崗位" && money > 200)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && gw == "多能工" && gt == "一般崗位"  && money > 200)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && gw == "多能工" && gt == "特殊崗位" && money > 200)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && gw == "多能工" && gt == "重點崗位" && money > 200)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && gw == "多能工" && gt == "特殊崗位+重點崗位" && money > 200)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && dt != "儲備" && gw != "多能工" && gt == "一般崗位" && money > 50)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && dt != "儲備" && gw != "多能工" && gt == "特殊崗位" && money > 140)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && dt != "儲備" && gw != "多能工" && gt == "重點崗位" && money > 140)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            if (lx == "特殊崗位津貼" && dt != "儲備" && gw != "多能工" && gt == "特殊崗位+重點崗位" && money > 140)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
            

            if (lx == "崗位補貼" && money > 200)
            {
                temp1String += objs[i].getData("EmpNo") + ";";
            }
        }

        if (!string.IsNullOrEmpty(temp1String))
        {
            pushErrorMessage(temp1String + "\n" + "請確認以上人員崗位，科目對應金額是否超標");
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
        //create 文字檔
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"D:\ECP\WebFormPT\web\LogFolder\EH0107.log", true);

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        DataObject[] objs = LeaveDateList.dataSource.getAllDataObjects();
        string managerId = "";
        string isqh = "";
        string ishj = "";
        string[] values = base.getUserManagerInfoID(engine, si.fillerID);
        //if (si.fillerOrgID == "NQM0101" && sqzszg.ValueText != "0")
        //{
        //    if (sqzszg.ValueText == "1")
        //    {
        //        managerId = "Y1900522";
        //    }
        //    else if (sqzszg.ValueText == "2")
        //    {
        //        managerId = "Q1608418";
        //    }
        //}
        //else
        //{
            managerId = values[1];  //申請人的主管 工號
        //}

        string AccNo = objs[0].getData("AccNo");
        string sqlaccNo = @"select iszjl from dbo.EH0107C WHERE AccNo='"+AccNo+"'";
        DataTable dtNO = engine.getDataSet(sqlaccNo, "bmE").Tables["bmE"];

        if (dtNO.Rows.Count>0)
        {
            isqh = dtNO.Rows[0]["iszjl"].ToString().Trim();
        }

        string sqlbm = @"select PartNo from dbo.HRUSERS WHERE EmpNo='"+managerId+"'";
        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
        string managerdept = dtbm.Rows[0]["PartNo"].ToString().Trim();

        if (managerdept.Contains("HR"))
        {
            ishj = "";
        }
        else
        {
            ishj = "1";
        }

        string xml = "";
        xml += "<EH0107>";
        xml += "<CREATOR DataType=\"java.lang.String\">" + si.fillerID + "</CREATOR>";
        xml += "<ManagerID DataType=\"java.lang.String\">" + managerId + "</ManagerID>";
        xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";
        xml += "<ishj DataType=\"java.lang.String\">" + ishj + "</ishj>";
        xml += "</EH0107>";

        param["EH0107"] = xml;
        writeLog("xml:" + xml);
        return "EH0107";
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
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string[] values = base.getUserManagerInfoID(engine, si.fillerID);
        string managerId = values[1];
        string sqlbm = @"select PartNo from dbo.HRUSERS WHERE EmpNo='" + managerId + "'";
        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
        string managerdept = dtbm.Rows[0]["PartNo"].ToString().Trim();
        writeLog("managerdept:" + managerdept + managerId);
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

    protected void sMoney_SelectChanged(string value)
    {
        if (value != "請選擇")
        {
            Money.ValueText = sMoney.ValueText;
        }
    }

    public Hashtable selectovertime(AbstractEngine engine, string EmpNo)
    {
        Hashtable hs = new Hashtable();
        hs.Add("Gwtype", "");
        hs.Add("Gwname", "");
        try
        {
            string sql = @"select QtName,QTTypeName,QuartersNo,PerField6 from [10.3.11.96].MIS_DB.dbo.PerEmployeeMD where EmpNo='" + EmpNo + "' AND Status='Y' ";

            DataRow dr = engine.getDataSet(sql, "hrsk").Tables["hrsk"].Rows[0];

            hs["Gwtype"] = dr["QTTypeName"].ToString();
            hs["Gwname"] = dr["QtName"].ToString();

        }
        catch { }
        return hs;
    }

    protected bool LeaveDateList_SaveRowData(DataObject objects, bool isNew)
    {
        bool gc = true;
        if (WorkDate.ValueText.Replace('/', ' ').Trim().ToString() == "" || WorkDate.ValueText.Replace('/', ' ').Trim().ToString() == string.Empty)
        {
            MessageBox("必須選擇時間");
            return false;
        }

        if (Account.ValueText == "請選擇")
        {
            MessageBox("必須選擇類別");
            return false;
        }


        if (sMoney.ValueText == "請選擇" || Money.ValueText == "")
        {
            MessageBox("必須選擇金額");
            return false;
        }

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        try
        {
            if (DSCTabControl1.SelectedTab == 0)
            {
                if (!retBool(engine, WorkDate.ValueText, Account.ValueText, EmpNo.ValueText,Convert.ToDouble(Money.ValueText)))
                {
                    MessageBox("資料庫已有重複資料");
                    return false;
                }
                else
                {
                    Hashtable hs = selectovertime(engine, EmpNo.ValueText);
                    objects.setData("GUID", IDProcessor.getID(""));
                    objects.setData("EmpNo", EmpNo.ValueText);
                    objects.setData("EmpName", EmpNo.ReadOnlyValueText);
                    objects.setData("Gwtype", hs["Gwtype"].ToString());
                    objects.setData("Gwname", hs["Gwname"].ToString());
                    objects.setData("Date", WorkDate.ValueText);
                    objects.setData("AccNo", Account.ValueText);
                    objects.setData("Account", Account.ReadOnlyText);
                    objects.setData("Money", Money.ValueText);
                    objects.setData("Note", Note.ValueText);
                    objects.setData("Remark", Remark.ValueText);
                }
            }
            else
            {
                DataTable dt = null;
                if (DSCTabControl1.SelectedTab == 1)
                {
                    string bmxz = SingleDropDownList1.ValueText;

                    //-------------------------部門選擇
                    string strSql = @"select EmpNo,EmpName from HRUSERS  
                                            WHERE PartNo='" + bmxz + "' and (LeaveDate is null or LeaveDate>convert(nvarchar(10),getdate()-30,23))";

                    dt = engine.getDataSet(strSql, "bmxz").Tables["bmxz"];
                }


                else if (DSCTabControl1.SelectedTab == 2)
                {
                    string xbxz = SingleDropDownList2.ValueText;

                    //-------------------------線別選擇
                    string strSql = @"select EmpNo,EmpName from HRUSERS WHERE PerField6='" + xbxz + "' and (LeaveDate is null or LeaveDate>convert(nvarchar(10),getdate()-30,23))";

                    dt = engine.getDataSet(strSql, "bmxz").Tables["bmxz"];
                }

                //----------------------數據裝載
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataObjectSet dos = new DataObjectSet();
                    dos.isNameLess = true;
                    dos.setAssemblyName("WebServerProject");
                    dos.setChildClassString("WebServerProject.form.EH0107.EH0107B");
                    dos.setTableName("EH0107B");
                    dos.loadFileSchema();

                    DataObject obj = new DataObject();
                    obj.loadFileSchema(LeaveDateList.dataSource.getChildClassString());

                    bool cc = false;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        DataObject[] objs = LeaveDateList.dataSource.getAllDataObjects();
                        for (int j = 0; j < objs.Length; j++)
                        {
                            if (dt.Rows[i][0].ToString() == objs[j].getData("EmpNo") && dt.Rows[i][2].ToString() == objs[j].getData("Date") && dt.Rows[i][3].ToString() == objs[j].getData("Account"))
                            {
                                cc = true;
                                break;
                            }
                        }
                        if (cc)
                        { cc = false; continue; }

                        if (!retBool(engine, WorkDate.ValueText, Account.ValueText, dt.Rows[i][0].ToString(),Convert.ToDouble(Money.ValueText)))
                        { continue; }
                        obj.setData("GUID", IDProcessor.getID(""));
                        obj.setData("EmpNo", dt.Rows[i][0].ToString());
                        obj.setData("EmpName", dt.Rows[i][1].ToString());
                        obj.setData("Date", WorkDate.ValueText);
                        obj.setData("AccNo", Account.ValueText);
                        obj.setData("Account", Account.ReadOnlyText);
                        obj.setData("Money", Money.ValueText);
                        obj.setData("Note", Note.ValueText);
                        obj.setData("Remark", Remark.ValueText);

                        if (gc)
                        {
                            gc = false;
                            objects.setHashtable(obj.clone().getHashtable());
                        }
                        else
                        {
                            dos.add(obj.clone());
                        }
                    }

                    if (gc)
                    {
                        MessageBox("已有重複資料");
                        return false;
                    }

                    LeaveDateList.dataSource = dos;
                    LeaveDateList.dataSource.sort(new string[,] { { "Date", DataObjectConstants.ASC }, { "EmpNo", DataObjectConstants.ASC } });

                }
            }

        }
        catch (Exception ES)
        {
            MessageBox(ES.Message);
            return false;
        }
        finally { LeaveDateList.updateTable(); }

        return true;
    }



    public bool retBool(AbstractEngine engine, string Day, string AccNo, string EmpNo,double money)
    {
        bool ret = true;
        string strSql = @"select b.*,a.IS_LOCK from EH0107A a,EH0107B b where a.SheetNo=b.SheetNo and b.EmpNo='" + EmpNo + "' and b.Date='" + Day + "' and a.IS_LOCK<>'Y'";
        DataTable hst = engine.getDataSet(strSql, "Tmp").Tables["Tmp"];

        for (int i = 0; i < hst.Rows.Count; i++)
        {
            if (EmpNo == hst.Rows[i]["EmpNo"].ToString() && Day == hst.Rows[i]["Date"].ToString() && AccNo == hst.Rows[i]["AccNo"].ToString() && money == Convert.ToDouble(hst.Rows[i]["Money"].ToString()))
            {
                ret = false;
            }
        }

        return ret;
    }

    public bool retNa(AbstractEngine engine, string EmpNo)
    {
        bool ret = true;

        string strSql = @"select EmpNo,EmpName from HRUSERS where EmpNo='" + EmpNo + "'";

        DataTable hst = engine.getDataSet(strSql, "Tmp").Tables["Tmp"];
        if (hst.Rows.Count > 0)
        {
            ret = false;
        }

        return ret;
    }

    protected void FileUpload1_AddOutline(DSCWebControl.FileItem currentObject, bool isNew)
    {
        try
        {
            FileUpload1.CheckData(0);
            FileUpload1.updateTable();
            DSCWebControl.FileItem[] fi = FileUpload1.getSelectedItem();

            DataSet ds = new DataSet();
            //构建连接字符串
            string path = Server.MapPath(@"../../../tempFolder/XZWork/" + fi[0].FILEPATH); //@"C:\ECP\WebFormPT\web\tempFolder\OWork\c54a57e3-cc4f-402f-a19b-12264ce473ea.xls"; 
            //2003
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";

            //  MessageBox(ConnStr);
            OleDbConnection Conn = new OleDbConnection(ConnStr);
            Conn.Open();

            //填充数据到表
            string sql = string.Format("select * from [{0}$]", "Sheet1");
            OleDbDataAdapter da = new OleDbDataAdapter(sql, ConnStr);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            //刪除頁面數據
            LeaveDateList.dataSource.clear();

            //關閉鏈接刪除文件
            Conn.Close();
            System.IO.File.Delete(path);


            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            string accname = "";
            int i = 0;
            string message = "";
            ArrayList ar = new ArrayList();
            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (!retBool(engine, dt.Rows[j][2].ToString(), dt.Rows[j][3].ToString(), dt.Rows[j][0].ToString(), Convert.ToDouble(dt.Rows[j][4].ToString().Trim())) || retNa(engine, dt.Rows[j][0].ToString()))//
                {
                    i++;
                    message += dt.Rows[j][0].ToString()+"\n";
                    continue;
                }
                else
                {
                    Hashtable hs = selectovertime(engine, dt.Rows[j][0].ToString());
                    DataObject obj = new DataObject();
                    obj.loadFileSchema(LeaveDateList.dataSource.getChildClassString());
                    obj.setData("GUID", IDProcessor.getID(""));
                    obj.setData("SheetNo", base.PageUniqueID);
                    obj.setData("EmpNo", dt.Rows[j][0].ToString());
                    obj.setData("EmpName", dt.Rows[j][1].ToString());
                    obj.setData("Gwtype", hs["Gwtype"].ToString());
                    obj.setData("Gwname", hs["Gwname"].ToString());
                    obj.setData("Date", dt.Rows[j][2].ToString());
                    obj.setData("AccNo", dt.Rows[j][3].ToString().Trim());

                    string sqlaccNo = @"select Account from dbo.EH0107C WHERE AccNo='" + dt.Rows[j][3].ToString() + "'";
                    DataTable dtNO = engine.getDataSet(sqlaccNo, "bmE").Tables["bmE"];
                    if (dtNO.Rows.Count > 0)
                    {
                        accname = dtNO.Rows[0]["Account"].ToString().Trim();
                    }
                    string mm = dt.Rows[j][4].ToString().Trim();
                    obj.setData("Account", accname);
                    obj.setData("Money", mm);
                    obj.setData("Note", dt.Rows[j][5].ToString());

                    if (dt.Rows[j][2].ToString().Length != 7)
                    {
                        MessageBox("日期格式不正確，請保證7位數！");
                        return;
                    }
                    if (Convert.ToDouble(dt.Rows[j][4].ToString().Trim()) < 0.5)
                    {
                        MessageBox("請確認導入金額是否正確！");
                        return;
                    }

                    LeaveDateList.dataSource.add(obj);
                }
            }
            LeaveDateList.dataSource.sort(new string[,] { { "Date", DataObjectConstants.ASC }, { "EmpNo", DataObjectConstants.ASC } });
            LeaveDateList.dataSource.compact();
            LeaveDateList.updateTable();
            MessageBox("去除重複/異常項:" + i.ToString()+"筆"+"\n"+message);
        }
        catch (Exception exc)
        {
            MessageBox(exc.Message);
        }
        FileUpload1.dataSource.clear();
        FileUpload1.updateTable();
    }

    /// <summary>
    /// 日誌
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\EH0107.log", true, System.Text.Encoding.Default);
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
