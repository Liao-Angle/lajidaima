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

public partial class Program_SCQ_Form_EH0106_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EH0106";
        AgentSchema = "WebServerProject.form.EH0106.EH0106Agent";
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

        User.clientEngineType = engineType;
        User.connectDBString = connectString;
        User.ValueText = si.fillerID;
        User.doValidate();

        User.ReadOnly = true;
        UpdateData(User.ValueText);

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
            if (si.fillerOrgID == "NQM0101")
            {
                showzg.Visible = true;
            }
        }
       
        string[,] zzrlist = new string[,]{ {"0","請選擇"},
                {"1","助理"},
                {"2","人事"},	
                {"3","資訊"},{"4","部門"},{"5","員工"}};

        zzr.setListItem(zzrlist);



        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
        try
        {

            string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" +si.fillerID + "'";
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


        string[,] tidss = new string[48, 2];
        int st = 6;
        for (int j = 0; st < 30; )
        {
            tidss[j, 0] = st.ToString().PadLeft(2, '0') + ":00";
            tidss[j, 1] = st.ToString().PadLeft(2, '0') + ":00";

            tidss[j + 1, 0] = st.ToString().PadLeft(2, '0') + ":30";
            tidss[j + 1, 1] = st.ToString().PadLeft(2, '0') + ":30";
            j++;
            j++;
            st++;
        }
        STime.setListItem(tidss);
        ETime.setListItem(tidss);




        string[,] CQLX = new string[,]{ {"0","請選擇"},
                {"平時加班","平時加班"},
                {"假日加班","假日加班"},	
                {"節日加班","節日加班"},{"出勤異常","出勤異常"},{"連續七天上班","連續七天上班"},{"其他","其他"}};
        YCLX.setListItem(CQLX);


        string[,] CQBB = new string[,] {{"常白班","常白班"},{"中班","中班"},{"小夜班","小夜班"},{"冬季白班","冬季白班"},{"MFG6夜班","MFG6夜班"},{"實時班","實時班"} };
        BB.setListItem(CQBB);



        //   LeaveDateList.HiddenField = new string[] { "GUID", "SheetNo", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME" };
        if (isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.EH0106.EH0106B");
            dos.setTableName("EH0106B");
            dos.loadFileSchema();
            objects.setChild("EH0106B", dos);
            LeaveDateList.dataSource = dos;
            LeaveDateList.updateTable();
        }
        LeaveDateList.clientEngineType = engineType;
        LeaveDateList.connectDBString = connectString;
        LeaveDateList.HiddenField = new string[] { "GUID", "SheetNo" };


        //改變工具列順序
        base.initUI(engine, objects);
    }



    /// <summary>
    /// 查詢填表人資料加入頁面
    /// </summary>
    /// <param name="id"></param>
    private void UpdateData(string id)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h1 = base.getHRUsers(engine, id);
        partNouser.ValueText = h1["PartNo"].ToString();
        partNouser.ReadOnly = true;
        //  Mobile.ValueText = h1["Mobile"].ToString();
        if (si.fillerID!="Y1600106")
		{
			Hashtable h2 = base.getADUserData(engine, id);
			mobileuser.ValueText = h2["telephonenumber"].ToString();
		}

        //if (si.fillerID != "Y1600106" && userId != "T009666." && userId != "Y1600190")
        //{
        //    Extension.ValueText = ADuser["telephonenumber"].ToString();
        //}
        

    }


    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)//Show里不要調用doValidate
    {

            //顯示單號
            base.showData(engine, objects);
            User.ValueText = objects.getData("SuserID");
            User.ReadOnlyValueText = objects.getData("SuserName");
            mobileuser.ValueText = objects.getData("SuserMobile");
            partNouser.ValueText = objects.getData("SuserPart");
            zzr.ValueText = objects.getData("Liability");
            CYSM.ValueText = objects.getData("Note");
            BZSM.ValueText = objects.getData("Remark");
            LeaveDateList.dataSource = objects.getChild("EH0106B");
            LeaveDateList.dataSource.sort(new string[,] { { "Date", DataObjectConstants.ASC } });
            LeaveDateList.updateTable();



            User.ReadOnly = true;
            mobileuser.ReadOnly = true;
            partNouser.ReadOnly = true;
            zzr.ReadOnly = true;
            CYSM.ReadOnly = true;
            BZSM.ReadOnly = true;


            User1S.Visible = false;
            User2S.Visible = false;
            User3S.Visible = false;
            User4S.Visible = false;
            User5S.Visible = false;

            LeaveDateList.IsShowCheckBox = false;
            LeaveDateList.ReadOnly = true;
    }




    protected void STime1_SelectChanged(string value)
    {
        try
        {
            string s1 = STime.ValueText;
            string e1 = ETime.ValueText;

            DateTime dt1 = Convert.ToDateTime(s1);
            DateTime dt2 = Convert.ToDateTime(e1);
            TimeSpan st = (dt2 - dt1);
            int hs = st.Hours;

                YCTime.ValueText = hs.ToString() + "." + Convert.ToInt32(Convert.ToDouble(st.Minutes) / (Double)60 * 100).ToString();

        }
        catch (Exception w) { YCTime.ValueText = ""; }
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
        if (LeaveDateList.dataSource.getAvailableDataObjectCount() == 0)
        {
            pushErrorMessage("必須填寫加班資料");
            result = false;
        }

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        if (Session["UserID"].ToString() == si.fillerID && si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0")
        {
            pushErrorMessage("MFG-1 部門 請選擇簽核主管");
            result = false;
        }


        if (zzr.ValueText == "請選擇"||zzr.ValueText == "0")
        {
            pushErrorMessage("必須選擇責任人");
            result = false;
        }


        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 薪資異常申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }
        }

        return result;
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

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("Subject", Subject.ValueText);
            objects.setData("SuserID", User.ValueText);
            objects.setData("SuserName", User.ReadOnlyValueText);
            objects.setData("SuserMobile", mobileuser.ValueText);
            objects.setData("SuserPart", partNouser.ValueText);
            objects.setData("Liability", zzr.ValueText);
            objects.setData("Note", CYSM.ValueText);
            objects.setData("Remark", BZSM.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            //將B表匯進來
            foreach (DataObject obj in LeaveDateList.dataSource.getAllDataObjects())
            {
                obj.setData("SheetNo", objects.getData("SheetNo"));
            }

            objects.setChild("EH0106B", LeaveDateList.dataSource);
        }
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
        DataObject[] objs = LeaveDateList.dataSource.getAllDataObjects();
        string managerId = "";
        string HRmanager = "Y1701199";
        string managerTz = "Q1100122";
        string isqh = "";
        string[] values = base.getUserManagerInfoID(engine, si.fillerID);
        if (si.fillerOrgID == "NQM0101" && sqzszg.ValueText != "0")
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

        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].getData("ERRType") == "連續七天上班")
            {
                isqh = "1";
            }
        }

        string xml = "";
        xml += "<EH0106>";
        //xml += "<creator DataType=\"java.lang.String\">" + si.fillerID + "</creator>";
        xml += "<managerID DataType=\"java.lang.String\">" + managerId + "</managerID>";
        xml += "<HRmanager DataType=\"java.lang.String\">" + HRmanager + "</HRmanager>";
        xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";
        xml += "</EH0106>";
        param["EH0106"] = xml;
        return "EH0106";
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




    /// <summary>
    /// 顯示三輥閘數據
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheakData_Click(object sender, EventArgs e)
    {
        if (WorkDate.ValueText.Equals(""))
        {
            pushErrorMessage("必須輸入修正日期");
        }

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        try
        {
            if (EmpNo.ValueText != string.Empty)
            {
                glasse = true;//Key裝載三輥閘數據以及提示信息

                string mdata = "select STAFF_NUM,STAFF_NAME,MACHINE_ID,DEPT,R_DATE,R_TIME from [10.3.11.68].SCQHR_DB.DBO.sgzshuju where STAFF_NUM='" + EmpNo.ValueText + "' and R_DATE='" + WorkDate.ValueText.Replace("/", "") + "' order by R_TIME ";

                DataTable tTb = engine.getDataSet(mdata, "data").Tables["data"];
                string showData = @"
";
                for (int i = 0; i < tTb.Rows.Count; i++)
                {
                    showData = showData + tTb.Rows[i]["STAFF_NAME"].ToString().Trim() + "  -  " + tTb.Rows[i]["R_DATE"].ToString() + "  -  " + tTb.Rows[i]["R_TIME"].ToString() + @"
";
                }

                MessageBox(showData);
            }
        }
        catch { }
        finally { engine.close(); }


    }


    /// <summary>
    /// 重選User
    /// </summary>
    /// <param name="values"></param>
    bool glasse = false;
    protected void EmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        try
        {
            if (EmpNo.ValueText != string.Empty)
            {
                glasse = true;//Key裝載三輥閘數據以及提示信息
            }
        }
        catch { }
        finally { engine.close(); }
    }


    protected void WorkDate_DateTimeClick(string values)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        try
        {
            if (EmpNo.ValueText != string.Empty)
            {


                string bbsql = "select WTID_F" + DateTime.Now.Day.ToString().PadLeft(2, '0') + " from dbo.HrWorkType where EmpNo='" + EmpNo.ValueText + "' and YYMM='" + WorkDate.ValueText.Replace("/", "").Substring(0, 6) + "'";
                string dts = engine.getDataSet(bbsql, "date").Tables["date"].Rows[0][0].ToString();
                switch (dts)
                {
                    case "A01": BB.ValueText = "常白班";
                        break;
                    case "A03": BB.ValueText = "中班";
                        break;
                    case "A06": BB.ValueText = "小夜班";
                        break;
                    case "A14": BB.ValueText = "冬季白班";
                        break;
                    case "A15": BB.ValueText = "MFG6夜班";
                        break;
                    case "E01": BB.ValueText = "實時班";
                        break;
                    default:
                        BB.ValueText = "常白班";
                        break;
                }
                BB.ReadOnly = false;
            }
        }
        catch { }
        finally { engine.close(); }
    }
    protected bool LeaveDateList_SaveRowData(DataObject objects, bool isNew)
    {
        LeaveDateList.dataSource.compact();
        LeaveDateList.updateTable();

        if (WorkDate.ValueText.Equals("") || WorkDate.ValueText.Length<=5)
        {
            MessageBox("必須輸入加班日期");
            return false;
        }
        if (EmpNo.ValueText.Equals(""))
        {
            MessageBox("必須選擇員工");
            return false;
        }
        if (STime.ValueText.Equals("") || ETime.ValueText.Equals("") || YCTime.ValueText.Equals(""))
        {
            MessageBox("必須填寫時數");
            return false;
        }
        double db = 0;
        double.TryParse(YCTime.ValueText, out db);
        if (db < 0)
        {
            MessageBox("加班時數錯誤");
            return false;
        }
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        DataObject[] objs = LeaveDateList.dataSource.getAllDataObjects();
        //for (int i = 0; i < objs.Length; i++)
        //{
         //   if (EmpNo.ValueText == objs[i].getData("EmpNo") && WorkDate.ValueText == objs[i].getData("WorkDate"))
          // {
           //     MessageBox("已有重複資料");
            //    return false;
            //}
        //}
        //if (retBool(engine, WorkDate.ValueText, EmpNo.ValueText))
        //{
         //   MessageBox("資料庫已有重複資料");
          //  return false;
        //}
        //else
        //{
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("Date", WorkDate.ValueText);
            objects.setData("STime", STime.ValueText);
            objects.setData("ETime", ETime.ValueText);
            objects.setData("WorkType", BB.ValueText);
            objects.setData("Time", YCTime.ValueText);
            objects.setData("ERRType", YCLX.ValueText);
        //}
        
        return true;
    }


    public bool retBool(AbstractEngine engine, string Day, string EmpNo)
    {
        string strSql = @"select b.*,a.IS_LOCK from EH0106A a,EH0106B b where a.SheetNo=b.SheetNo and b.EmpNo='"+EmpNo+"' and b.Date='"+Day+"' and a.IS_LOCK<>'Y'";
        DataTable hst = engine.getDataSet(strSql, "Tmp").Tables["Tmp"];
        if (hst != null && hst.Rows.Count > 0&&hst.Rows[0][0].ToString().Trim()!="")
        {
            return true;
        }
        return false;
    }

}
