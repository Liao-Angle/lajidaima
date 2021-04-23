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

public partial class Program_SCQ_Form_EG0103_Form : SmpBasicFormPage
{
    protected override void init()
    {
          ProcessPageID = "EG0103";
          AgentSchema = "WebServerProject.form.EG0103.EG0103Agent";
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

        string[,] ids = new string[3, 2] { { "0", "公司派車" }, { "1", "自駕" }, { "2", "叫車" } };
        LeaveTypeID.setListItem(ids);


        string[,] idsg = new string[3, 2] { { "0", "公司一般用車" }, { "1", "公司商務用車" }, { "2", "客戶用車" } };
        Genre.setListItem(idsg);


        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];


	string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + si.fillerID + "'";
      //string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where EmpNo ='" + si.fillerID + "'";
       // string sqlbm = @"select distinct PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerEmployee where PartNo not like '%支援%' and PartNo not like '%駐廠%'";
        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
        ///--------------------------帶出使用者信息
        try
        {


            EmpNo.clientEngineType = engineType;
            EmpNo.connectDBString = connectString;
            EmpNo.DoEventWhenNoKeyIn = false;
            EmpNo.ValueText = si.fillerID;
            EmpNo.doValidate();
            UpdateData(si.fillerID);
            EmpNo.ReadOnly = true;
            if (dtbm.Rows.Count > 0)
            {
                string[,] bms = new string[dtbm.Rows.Count+1, 2];
                bms[0, 0] = "0";
                bms[0, 1] = "請選擇用車單位";
                for (int ii = 0; ii < dtbm.Rows.Count; ii++)
                {
                    bms[ii + 1, 0] = (ii + 1).ToString();
                    bms[ii + 1, 1] = dtbm.Rows[ii][0].ToString();
                }

                Department.setListItem(bms);
            }
        }
        catch {                 string[,] bms = new string[2, 2];
                bms[0, 0] = "0";
                bms[0, 1] = "請選擇用車單位";
 		bms[1, 0] = "0";
                bms[1, 1] = base.getHRUsers(engine, si.fillerID)["PartNo"].ToString();
 		Department.setListItem(bms);
}

        ///-----------------------------------------
        Subject.Display = false;
        SheetNo.Display = false;

              if (isNew()||Session["UserID"].ToString()==si.fillerID)
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
       // Hashtable Euser = (Hashtable)base.getHRUsers(engine, "Q1307275");
          //改變工具列順序
        base.initUI(engine, objects);
    }



    private void UpdateData(string id)
    {
try{
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h1 = base.getHRUsers(engine, id);
        partNouser.ValueText = h1["PartNo"].ToString();
        partNouser.ReadOnly = true;
      //  Mobile.ValueText = h1["Mobile"].ToString();
        Hashtable h2 = base.getADUserData(engine, id);
        mobileuser.ValueText = h2["telephonenumber"].ToString();
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
        Subject.ValueText = objects.getData("Subject");
        CName.ValueText = objects.getData("CName");
        Mobile.ValueText = objects.getData("Mobile");
        Department.ValueText = objects.getData("Department");
        Department.ReadOnlyText = objects.getData("Department");
        Departure.ValueText = objects.getData("Departure");
        Destination.ValueText = objects.getData("Destination");
        Peers.ValueText = objects.getData("Peers");
        DepartureTime.ValueText = objects.getData("DepartureTime");
        Reason.ValueText = objects.getData("Reason");
        Remark.ValueText = objects.getData("Remark");

        LeaveTypeID.selectedIndex(Convert.ToInt32(objects.getData("Modus")));
        Genre.selectedIndex(Convert.ToInt32(objects.getData("Genre")));

        if (objects.getData("Hitch")=="Y")
        {
            HitchY.Checked = true;
        }
        else { HitchN.Checked = true; }
        UserName.ValueText = objects.getData("UserName");
        UserMobile.ValueText = objects.getData("UserMobile");
        Plate.ValueText = objects.getData("Plate");
        StartKm.ValueText = objects.getData("StartKm");
        EndKm.ValueText = objects.getData("EndKm");
        goout.ValueText = objects.getData("Goout");
        goback.ValueText = objects.getData("Goback");

        CName.ReadOnly = true;
        Mobile.ReadOnly = true;
        Department.ReadOnly = true;
        Departure.ReadOnly = true;
        Destination.ReadOnly = true;
        Peers.ReadOnly = true;
        DepartureTime.ReadOnly = true;
        Reason.ReadOnly = true;
        HitchN.ReadOnly = true;
        HitchY.ReadOnly = true;
	UserName.ReadOnly = true;
        Plate.ReadOnly = true;
        UserMobile.ReadOnly = true;
        Genre.ReadOnly = true;
        LeaveTypeID.ReadOnly = true;

        string actName = (string)getSession("ACTName");
        if (actName.Equals("GA事務"))
        {
            UserName.ReadOnly=false;
              Plate.ReadOnly=false;
              UserMobile.ReadOnly = false;
            Genre.ReadOnly = false;
            LeaveTypeID.ReadOnly = false;
        }
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
            objects.setData("SheetNo",objects.getData("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("CName",CName.ValueText);
            objects.setData("Mobile",Mobile.ValueText);
            objects.setData("Department",Department.ReadOnlyText);
            objects.setData("Departure",Departure.ValueText);
            objects.setData("Destination",Destination.ValueText);
            objects.setData("Peers",Peers.ValueText);
            objects.setData("DepartureTime",DepartureTime.ValueText);
            objects.setData("Reason",Reason.ValueText);
            objects.setData("Modus", LeaveTypeID.ValueText);
  
            if (HitchY.Checked)
            {
                objects.setData("Hitch", "Y");
            }
            else { objects.setData("Hitch", "N"); }
            objects.setData("Usermessage", "N");
        }

        objects.setData("Genre", Genre.ValueText);
 	objects.setData("Uname",  si.fillerName);
        objects.setData("Remark", Remark.ValueText);
        objects.setData("UserName", UserName.ValueText);
        objects.setData("UserMobile", UserMobile.ValueText);
        objects.setData("Plate", Plate.ValueText);
        objects.setData("StartKm", StartKm.ValueText);
        objects.setData("EndKm", EndKm.ValueText);
        objects.setData("Goout", goout.ValueText);
        objects.setData("Goback", goback.ValueText);




            //產生單號並儲存至資料物件
            base.saveData(engine, objects);


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
        string sr = Department.ReadOnlyText;
        if (Department.ValueText == "0")
        {
           
            pushErrorMessage("請選擇用車部門");
            result = false;
        }
        try{
       Convert.ToInt64(Mobile.ValueText.Trim());
       if (Mobile.ValueText.Trim().Length != 11)
       {
           pushErrorMessage("手機號填寫有誤!請填寫11碼手機號");
           result = false;
       }
        }catch(FormatException ES)
        {
            pushErrorMessage("手機號填寫有誤!");
            result = false;
        }
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0")
        {
            pushErrorMessage("MFG-1 部門 請選擇簽核主管");
            result = false;
        }

        try {
            Convert.ToDateTime(DepartureTime.ValueText);
        }catch(Exception ec){result=false; pushErrorMessage("請選擇去程時間");}

        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 派車申請單 】";
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
        string managerId = "";
        string GAmanager = "Q1100102";
        string managerTz = "Q1508126";
        string GaAwo = "";
        string isqh = "";
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

        string xml = "";
        xml += "<EG0103>";
        xml += "<CREATOR DataType=\"java.lang.String\">" + si.fillerID + "</CREATOR>";
        xml += "<ManagerID DataType=\"java.lang.String\">" + managerId + "</ManagerID>";
        xml += "<GAmanager DataType=\"java.lang.String\">" + GAmanager + "</GAmanager>";
        xml += "<managerTz DataType=\"java.lang.String\">" + managerTz + "</managerTz>";
        xml += "<GaAwo DataType=\"java.lang.String\">" + GaAwo + "</GaAwo>";
        xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";
        xml += "</EG0103>";
        param["EG0103"] = xml;
        return "EG0103";

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
                base.terminateThisProcess();//終止流程
            }
            catch (Exception e)
            {
                base.writeLog(e);
            }
        }
        else
        {
            base.rejectProcedure();//退回重辦
        }
    }

     /// <summary>
      /// 結案程序
      /// </summary>
      /// <param name="engine"></param>
      /// <param name="currentObject"></param>
      /// <param name="result"></param>
      protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
      {
        try
        {
                if (result.Equals("Y"))
                {
                    setMessage();
                }
        }
        catch (Exception e)
        {
                base.writeLog(e);
                throw new Exception(e.Message);
        }
        base.afterApprove(engine, currentObject, result);
      }





    //-------------------短信平台
    public static string GetGUID()
    {
        return Guid.NewGuid().ToString().ToUpper();
    }

    public static string SetJob(string job_id, string domain_name, string user_name, string machine_name, string msg, int user_count, string job_status)
    {
        string sql = string.Format(@"INSERT INTO [dbo].[CMJob] ([JobID],[DomainName],[UserName],[MachineName],[Message],[UserCount],[JobStatus],[CreateTime])
                                       VALUES ('{0}','{1}','{2}','{3}','{4}',{5},'{6}',GETDATE())", job_id, domain_name, user_name, machine_name, msg, user_count, job_status);
        return sql;
    }

    public static string SetReceiver(string job_id, int job_seq_no, string emp_no, string emp_name, string mobile)
    {
        string sql = string.Format(@"INSERT INTO [dbo].[CMReceiver] ([JobID],[JobSeqNo],[MsgID],[MsgSeqNo],[EmpNo],[EmpName],[Mobile],[CreateTime])
                                         VALUES ('{0}',{1},'{2}',{3},'{4}','{5}','{6}', GETDATE())", job_id, job_seq_no, "", 0, emp_no, emp_name, mobile);
        return sql;
    }
    public static string SetStatus(string id, string status_type, string status)
    {
        string sql = string.Format(@"INSERT INTO [dbo].[CMStatus] ([ID],[Type],[Status],[UpdateTime])
                                             VALUES ('{0}','{1}','{2}', GetDate())", id, status_type, status);
        return sql;
    }
    //
    private void setMessage()
    {
        try
        {
            //
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string YUser = CName.ValueText + "您好，時間:" + DepartureTime.ValueText + "出發點:" + Departure.ValueText + "目的地:" + Destination.ValueText + "以上信息如有變動，請及時與管理部聯繫, 司機姓名:" + UserName.ValueText + "司機電話:" + UserMobile.ValueText + "車牌號碼:" + Plate.ValueText + "<管理部>";
            string CUser = "用車信息如下，時間:" + DepartureTime.ValueText + "出發點:" + Departure.ValueText + "目的地:" + Destination.ValueText + "用車人:" + CName.ValueText + "用車人聯繫電話:" + Mobile.ValueText + "<管理部>";
            string setGuid = GetGUID();

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string CMDB = sp.getParam("CMDB");
            engine.close();

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(CMDB);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = conn.CreateCommand();
            //用車人
            {
                cmd.CommandText = SetJob(setGuid, "SMP-CN", si.fillerName, si.fillerID, YUser, 1, "START");
                int i = cmd.ExecuteNonQuery();
                cmd.CommandText = SetReceiver(setGuid, 1, "", CName.ValueText, Mobile.ValueText.Trim());
                i += cmd.ExecuteNonQuery();
                cmd.CommandText = SetStatus(setGuid, "Job", "START");
                i += cmd.ExecuteNonQuery();
            }
            //司機
            {
                cmd.CommandText = SetJob(setGuid, "SMP-CN", si.fillerName, si.fillerID, CUser, 1, "START");
                int i = cmd.ExecuteNonQuery();
                cmd.CommandText = SetReceiver(setGuid, 1, "", UserName.ValueText, UserMobile.ValueText.Trim());
                i += cmd.ExecuteNonQuery();
                cmd.CommandText = SetStatus(setGuid, "Job", "START");
                i += cmd.ExecuteNonQuery();
            }
            conn.Close();
        }catch(Exception e){}

    }





      /// <summary>
      /// Genre changed
      /// </summary>
      /// <param name="value"></param>
      protected void Genre_SelectChanged(string value)
      {

      }

      /// <summary>
      /// 選擇人員
      /// </summary>
      /// <param name="values"></param>
      protected void RequestID_SingleOpenWindowButtonClick(string[,] values)
      {
          if (values == null)
              return;
      }
}
