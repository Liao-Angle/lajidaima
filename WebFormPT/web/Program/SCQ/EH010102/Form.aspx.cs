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
using System.Text;

public partial class Program_SCQ_Form_EH010101_Form : SmpBasicFormPage
{
    //非異常表單
    private string Abnormal = "Y";
   

    protected override void init()
    {
        ProcessPageID = "EH010102";
        AgentSchema = "WebServerProject.form.EH010102.EH010102Agent";
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
                            }SQLstr =SQLstr +")";
                            EmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


                      }
                      else
                      {
                            EmpNo.whereClause = "1=2";
                      }

                      EmpNo.DoEventWhenNoKeyIn = false;
                }
                catch {
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
        

        string modify = sp.getParam("HRModifyType");
        if (!modify.Equals(""))
        {
            string[] strs = modify.Split(new char[] { ';' });
            string[,] ids = new string[strs.Length, 2];
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].IndexOf('/') > 0 && strs[i].IndexOf('/') < (strs[i].Length - 1))
                {
                    ids[i, 0] = strs[i].Split(new char[] { '/' })[0];
                    ids[i, 1] = strs[i].Split(new char[] { '/' })[1];
                }
            }
            HRModifyID.setListItem(ids);
            HRModifyID.Visible = true;
        }

        string[,] sids = new string[6, 2] { { "常白班", "常白班" }, { "中班", "中班" }, { "小夜班", "小夜班" }, { "冬季白班", "冬季白班" }, { "MFG6夜班", "MFG6夜班" }, { "實時班", "實時班" } };
        Shift.setListItem(sids);
        Shift.Visible = true;



        string[,] ider = new string[7, 2] { { "上班外出未登記", "上班外出未登記" }, { "卡牌未帶", "卡牌未帶" },
        { "卡牌丟失", "卡牌丟失" }, { "卡牌消磁", "卡牌消磁" }, { "公務出差", "公務出差" }, { "未刷卡", "未刷卡" },
        { "報道當日", "報道當日"} };

        Reason.setListItem(ider);

        SheetNo.Display = false;
        Subject.Display = false;



        if (isNew() || Session["UserID"].ToString() == si.fillerID)
        {
            string[,] xgqhzg = new string[,]{ {"0","請選擇"},
                {"1","張華謙"},
                {"2","田軍祥"},	
                //{"3","許文坤"},
				};
            sqzszg.setListItem(xgqhzg);
            ///
            if (si.fillerOrgID == "NQM0101")
            {
                showzg.Visible = true;
            }
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

        if (objects.getData("Abnormal").ToString() == Abnormal)
        {
            Subject.ValueText = objects.getData("Subject");
            EmpNo.ValueText = objects.getData("EmpNo");
		EmpNo.ReadOnlyValueText = objects.getData("EmpName");
           // EmpNo.doValidate();
            EmpNo.Enabled = false;
            HRModifyID.ValueText = objects.getData("HRModify");
            Reason.ValueText = objects.getData("Reason");
            WorkDate.ValueText = objects.getData("WorkDate");
            Shift.ValueText = objects.getData("Shift");
            STime.ValueText = objects.getData("STime");
            ETime.ValueText = objects.getData("ETime");
        }
        SheetNo.Display = false;
        Subject.Display = false;
        EmpNo.ReadOnly = true;
        HRModifyID.ReadOnly = true;
        Reason.ReadOnly = true;
        WorkDate.ReadOnly = true;
        Shift.ReadOnly = true;
        STime.ReadOnly = true;
        ETime.ReadOnly = true;
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        if (base.isNew())
        {
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject",Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("HRModify", HRModifyID.ValueText);
            objects.setData("Reason", Reason.ValueText);
            objects.setData("WorkDate", WorkDate.ValueText);
            objects.setData("Shift", Shift.ValueText);
            objects.setData("STime", STime.ValueText);
            objects.setData("ETime", ETime.ValueText);
            objects.setData("Abnormal", Abnormal);
            objects.setData("ForwardHR", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
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
        string a = "";
        string b = "";
        a = EmpNo.ValueText.ToString();
        b = WorkDate.ValueText.ToString();
        bool result = true;
        if (base.isNew())
        {
            if (EmpNo.ValueText.ToString() == "")
            {
                pushErrorMessage("必須選擇員工");
                result = false;
            }

            if (Reason.ValueText.Equals(""))
            {
                pushErrorMessage("必須填寫修正事由");
                result = false;
            }
            if (WorkDate.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入修正日期");
                result = false;
            }
            if (HRModifyID.ValueText == "H01" && ETime.ValueText != "")
            {
                pushErrorMessage("修正類別為上班卡，請核對修正時間");
                result = false;
            }
            if (HRModifyID.ValueText == "H02" && STime.ValueText != "")
            {
                pushErrorMessage("修正類別為下班卡，請核對修正時間");
                result = false;
            }
            if (HRModifyID.ValueText == "H03" && (STime.ValueText == "" || ETime.ValueText == ""))
            {
                pushErrorMessage("修正類別為全部，請核對修正時間");
                result = false;
            }
            //檢查資料庫數據重複

            if (checkServerData(a, b))
            {
                result = true;
            }
            else
            {
                //pushErrorMessage("當日修正時間重複！");
                result = false;
            }

            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            if (Session["UserID"].ToString() == si.fillerID && si.fillerOrgID == "NQM0101" && sqzszg.ValueText == "0")
            {
                pushErrorMessage("MFG-1 部門 請選擇簽核主管");
                result = false;
            }
            //設定主旨
            if (Subject.ValueText.Equals(""))
            {
                string subject = "【出勤修正單】" + EmpNo.ReadOnlyValueText + " " + WorkDate.ValueText + " " + Reason.ValueText;
                if (Subject.ValueText.Equals(""))
                {
                    Subject.ValueText = subject;
                }
            }
        }
            return result;
    }



    public bool checkServerData(string a, string b)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        AbstractEngine engine = null;
        IOFactory factory = new IOFactory();

        engine = factory.getEngine(engineType, connectString);
        string SSql = @"select * from dbo.EH010101A where EmpNo='" + a + "' and WorkDate='" + b + "'  and IS_LOCK<>'Y'";


        DataTable hst = engine.getDataSet(SSql, "ckdate").Tables["ckdate"];
        if (hst != null && hst.Rows.Count > 0)
        {
            //---------------------------Check補上班
            if (STime.ValueText.Trim() != string.Empty || STime.ValueText.Trim() != "")
            {
                for (int i = 0; i < hst.Rows.Count; i++)
                {
                    if (STime.ValueText.Trim() == hst.Rows[i]["STime"].ToString().Trim())
                    {
                        pushErrorMessage("(上班卡)該筆資料重複");
                        return false;
                    }
                }
            }

            //---------------------------Check補下班
            if (ETime.ValueText.Trim() != string.Empty || ETime.ValueText.Trim() != "")
            {
                for (int i = 0; i < hst.Rows.Count; i++)
                {
                    if (ETime.ValueText.Trim() == hst.Rows[i]["ETime"].ToString().Trim())
                    {
                        pushErrorMessage("(下班卡)該筆資料重複");
                        return false;
                    }
                }
            }
        }
        return true;
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
        string managerId = "";
        string hj = "";
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
        }
        else
        {
            managerId = values[1];  //申請人的主管 工號
        }

        Hashtable htb = base.getHRUsers(engine, EmpNo.ValueText);

        if (htb["DutyNo"].ToString() == "2" || htb["DutyNo"].ToString() == "5" || htb["DutyNo"].ToString() == "9" || htb["DutyNo"].ToString() == "10" || htb["DutyNo"].ToString() == "15" || htb["DutyNo"].ToString() == "16" || htb["DutyNo"].ToString() == "18" || htb["DutyNo"].ToString() == "28" || htb["DutyNo"].ToString() == "34")
        {
            hj = "1";
        }
        else if (htb["DutyNo"].ToString() == "29")
        {
            hj = "0";
        }
        else
        {
            hj = "3";
        }

        string xml = "";
        xml += "<EH010102>";
        xml += "<CREATOR DataType=\"java.lang.String\">" + si.fillerID + "</CREATOR>";
        xml += "<MnagerID DataType=\"java.lang.String\">" + managerId  + "</MnagerID>";
        xml += "<hj DataType=\"java.lang.String\">" + hj + "</hj>";
        //xml += "<MISAP DataType=\"java.lang.String\">" + "Q1310035" + "</MISAP>";
        //xml += "<HRKQ DataType=\"java.lang.String\">" + "Q1310035" + "</HRKQ>";
        xml += "</EH010102>";
        param["EH010102"] = xml;
        return "EH010102";
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
        if (result == "Y")
        {
            //將ForwardHR改為R
            currentObject.setData("ForwardHR", "R");
            string id = currentObject.getData("SheetNo");
            if (!engine.updateData(currentObject))
            {
                throw new Exception("更新EH010101 ForwardHR資料錯誤. 單號: " + id);
            }
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

                string mdata = @"select STAFF_NUM,STAFF_NAME,MACHINE_ID,DEPT,R_DATE,R_TIME,MS from [10.3.11.68].SCQHR_DB.DBO.sgzshuju where STAFF_NUM='" + EmpNo.ValueText + "' and R_DATE='" + WorkDate.ValueText.Replace("/", "") + "' order by R_TIME ";

               DataTable tTb = engine.getDataSet(mdata, "data").Tables["data"];
               string showData = @"
"; 
                for (int i = 0; i < tTb.Rows.Count; i++)
               {
                   showData = showData + tTb.Rows[i]["STAFF_NAME"].ToString().Trim() + "  -  " + tTb.Rows[i]["R_DATE"].ToString() + "  -  " + tTb.Rows[i]["R_TIME"].ToString() + "  -  " + tTb.Rows[i]["MS"].ToString() + @"
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

                string mdata = "select COUNT(*) from EH010101A where EmpNo='" + EmpNo.GuidValueText + "'";
                string mSql = mdata + " and WorkDate like '" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "%'";
                string ySql = mdata + " and WorkDate like '" + DateTime.Now.Year.ToString() + "%'";
		 string stsql = "select convert(nvarchar(10),LeaveDate,23) LeaveDate from HRUSERS where EmpNo='" + EmpNo.GuidValueText + "'";

                string MMM = engine.getDataSet(mSql, "mmm").Tables["mmm"].Rows[0][0].ToString();
                string YYY = engine.getDataSet(ySql, "yyy").Tables["yyy"].Rows[0][0].ToString();
                string STU = engine.getDataSet(stsql, "stu").Tables["stu"].Rows[0][0].ToString();

                if (STU != "")
                {
                    Status.Text = "該員工本月已修正" + MMM + "次，年度累計已修正" + YYY + "次。員工離職日期：" + STU + "";
                }
                else
                {
                    Status.Text = "該員工本月已修正" + MMM + "次，年度累計已修正" + YYY + "次。";
                }

              
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


                //string bbsql = "select WTID_F"+DateTime.Now.Day.ToString().PadLeft(2,'0')+" from dbo.HrWorkType where EmpNo='" + EmpNo.GuidValueText + "' and YYMM='" + WorkDate.ValueText.Replace("/", "").Substring(0,6) + "'";
                string bbsql = "select WTID_F" + WorkDate.ValueText.Replace("/", "").Substring(6, 2) + " from dbo.HrWorkType where EmpNo='" + EmpNo.GuidValueText + "' and YYMM='" + WorkDate.ValueText.Replace("/", "").Substring(0, 6) + "'";
                string dts = engine.getDataSet(bbsql, "date").Tables["date"].Rows[0][0].ToString();
                switch(dts){
                    case "A01": Shift.ValueText = "常白班";
                        break;
                    case "A03": Shift.ValueText = "中班";
                        break;
                    case "A06": Shift.ValueText = "小夜班";
                        break;
                    case "A14": Shift.ValueText = "冬季白班";
                        break;
                    case "A15": Shift.ValueText = "MFG6夜班";
                        break;
                    case "E01": Shift.ValueText = "實時班";
                        break;
                    default:
                        Shift.ValueText = "常白班";
                        break;
                }
                Shift.ReadOnly =true;
            }
        }
        catch { }
        finally { engine.close(); }
    }
}
