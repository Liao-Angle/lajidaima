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

public partial class Program_SCQ_Form_EH010201_Form : SmpBasicFormPage
{
    private string Responsible = "N";

    protected override void init()
    {
        ProcessPageID = "EH010201";
        AgentSchema = "WebServerProject.form.EH010201.EH010201Agent";
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
        SheetNo.Display = false;
        Subject.Display = false;

        string sqlbm = @"select PartNo from   [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where EmpNo ='" +si.fillerID + "'";
        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
        string SQLstr = "";
        try
        {
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
                SQLstr = "1=2";
                EmpNo.whereClause = SQLstr;
            }

            EmpNo.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "")
            {
                SQLstr = "PartNo like '%" + si.fillerOrgID + "%'";
                EmpNo.whereClause = SQLstr;
            }
            else
            {
                SQLstr = "1=2";
                EmpNo.whereClause = SQLstr;
            }
            EmpNo.DoEventWhenNoKeyIn = false;
        }
      
        string[,] hids = new string[24, 2];
        for (int i = 0; i < 24; i++)
        {
            hids[i, 0] = i.ToString("00");
            hids[i, 1] = i.ToString("00");
        }
        STime1.setListItem(hids);
        ETime1.setListItem(hids);
        STime1.ValueText = "17";

        string[,] mids = new string[4, 2] { { "00", "00" }, { "15", "15" }, { "30", "30" }, { "45", "45" } };
        STime3.setListItem(mids);
        ETime3.setListItem(mids);

        if (base.isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.EH010201.EH010201B");
            dos.setTableName("EH010201B");
            dos.loadFileSchema();
            objects.setChild("EH010201B", dos);
            OverTimeList.dataSource = dos;
            OverTimeList.updateTable();
        }

        OverTimeList.clientEngineType = engineType;
        OverTimeList.connectDBString = connectString;
        OverTimeList.HiddenField = new string[] { "GUID", "SheetNo" };


          
          //--------------------------以下页面2

              if (dtbm.Rows.Count > 0)
              {

                    int j = dtbm.Rows.Count;
                    string[,] strs = new string[j+1, 2];
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

           

//  string sqlxb = @"select distinct PerField6 from [10.3.11.92\SQL2008].SCQHRDB.DBO.PEREMPLOYEE Where PerField6<>'' and " + SQLstr;
	string sqlxb = @"select distinct PerField6 from HRUSERS Where PerField6<>'' and " + SQLstr +"order by PerField6";
 
              DataTable dt = engine.getDataSet(sqlxb, "PEREMPLOYEE").Tables["PEREMPLOYEE"];
              if (dt.Rows.Count > 0)
              {
                    
                          int j = dt.Rows.Count;
                          string[,] str3 = new string[j + 1, 2];
//Reason.ValueText = sqlxb +j.ToString();
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

                ///初始化上傳控件
              try
              {
                  SysParam ssp = new SysParam(engine);
                 // string fileAdapter = ssp.getParam("FileAdapter");
                  FileUpload1.FileAdapter = "加班文件|*.xls";
                 
                  FileUpload1.engine = engine;
                  FileUpload1.tempFolder = Server.MapPath("~/tempFolder/OWork/");//加班臨時資料夾
                  FileUpload1.readFile("");
                  FileUpload1.updateTable();
              }
              catch { }

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

          
    
        if (objects.getData("Responsible").ToString() == Responsible)
        {
            OverTimeList.dataSource = objects.getChild("EH010201B");
            OverTimeList.dataSource.sort(new string[,] { { "WorkDate", DataObjectConstants.ASC }, { "EmpNo", DataObjectConstants.ASC } });
            OverTimeList.updateTable();
        }
        SheetNo.Display = false;
        Subject.Display = false;
        LBEmpNo.Visible = false;
        EmpNo.Visible = false;
        LBWorkDate.Visible = false;
        WorkDate.Visible = false;
        STime1.Visible = false;
        STime3.Visible = false;
        ETime1.Visible = false;
        ETime3.Visible = false;
        LBHours.Visible = false;
        Hours.Visible = false;
        LBReason.Visible = false;
        Reason.Visible = false;
        LBNote.Visible = false;
        Note.Visible = false;
        Rows.Visible = false;
        Rows1.Visible = false;
        Rows2.Visible = false;
        Rows3.Visible = false;
        Rows4.Visible = false;
        Rows5.Visible = false;
        Rows6.Visible = false;
        OverTimeList.IsShowCheckBox = false;
        OverTimeList.ReadOnly = true;


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
        //主旨含單號
        base.saveData(engine, objects);

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("Responsible", Responsible);
            objects.setData("ForwardHR", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            
            //將B表匯進來
            foreach (DataObject obj in OverTimeList.dataSource.getAllDataObjects())
            {
                obj.setData("SheetNo", objects.getData("SheetNo"));
            }
            objects.setChild("EH010201B", OverTimeList.dataSource);

            //主旨
            //Subject.ValueText = objects.getData("Subject");

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
        if (OverTimeList.dataSource.getAvailableDataObjectCount() == 0)
        {
            pushErrorMessage("必須填寫加班資料");
            result = false;
        }


  	  SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0"&&Session["UserID"].ToString()==si.fillerID)
            {
 		pushErrorMessage("MFG-1 部門 請選擇簽核主管");
                result = false;
            }

        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
              //values = base.getUserInfo(engine, RequestList.ValueText);
              string subject = "【 加班申請單 】";
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
	string str="";
		if(si.fillerOrgID=="GQC2200-GA"||si.fillerOrgID=="GQC2100-HR"||si.fillerOrgID=="GQM0110-FAC")
		{
			str="1";
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

 	string xml = "";
            xml += "<EH010201>";
            xml += "<bumen DataType=\"java.lang.String\">" +str+ "</bumen>";
            xml += "<managerId DataType=\"java.lang.String\">" + managerId + "</managerId>";
            xml += "</EH010201>";
            param["EH010201"] = xml;
            return "EH010201";
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
                throw new Exception("更新EH010201 ForwardHR資料錯誤. 單號: " + id);
            }
        }
    }


      protected void STime1_SelectChanged(string value)
      {
          try
          {
              string s1 = STime1.ValueText;
              string s3 = STime3.ValueText;
              string e1 = ETime1.ValueText;
              string E3 = ETime3.ValueText;

              DateTime dt1 = Convert.ToDateTime(s1 + ":" + s3);
              DateTime dt2 = Convert.ToDateTime(e1 + ":" + E3);
              TimeSpan st = (dt2 - dt1);

              int hs = st.Hours;
              if (Convert.ToInt32(s1) <= 12 && Convert.ToInt32(e1) >= 13)
              {
                  hs--;
              }

              Hours.ValueText = hs.ToString() + "." + Convert.ToInt32(Convert.ToDouble(st.Minutes) / (Double)60 * 100).ToString();

          }
          catch (Exception w) { Hours.ValueText = ""; }
      }




      public Hashtable selectovertime(AbstractEngine engine, string datetime, string PartNo)
      {
          Hashtable hs = new Hashtable();
          hs.Add("G01", "");
          hs.Add("OUT1", "");
          hs.Add("OverTime", "");
          hs.Add("WTID", "");
          hs.Add("InCumbency", "");
          try
          {
              string SQL = "select EmpNo,GO1,OUT1,OverTime,WTID,InCumbency from HrAttDayData where YYMMDD='" + datetime + "' and EmpNo='" + PartNo + "'";
              DataRow dr = engine.getDataSet(SQL, "hrsk").Tables["hrsk"].Rows[0];

              hs["G01"] = dr["GO1"].ToString();
              hs["OUT1"] = dr["OUT1"].ToString();
              hs["OverTime"] = dr["OverTime"].ToString();
              hs["WTID"] = dr["WTID"].ToString();
              hs["InCumbency"] = dr["InCumbency"].ToString();
          }
          catch { }
          return hs;
      }


      protected bool RequestList_SaveRowData(DataObject objects, bool isNew)
      {

          OverTimeList.dataSource.compact();
          OverTimeList.updateTable();


	 bool gc = true;
          // Reason.ValueText = objects.getData("Subject");// Subject.ValueText;
          // Note.ValueText = SheetNo.ValueText;


          if (WorkDate.ValueText.Equals(""))
          {
              MessageBox("必須輸入加班日期");
              return false;
          }
          if (Reason.ValueText.Equals(""))
          {
              MessageBox("必須填寫加班理由");
              return false;
          }
          if (Hours.ValueText.Equals(""))
          {
              MessageBox("必須填寫加班時數");
              return false;
          }
          double db = 0;
          double.TryParse(Hours.ValueText, out db);
          if (db < 0)
          {
              MessageBox("加班時數錯誤");
              return false;
          }

          //

          string connectString = (string)Session["connectString"];
          string engineType = (string)Session["engineType"];

          IOFactory factory = new IOFactory();
          AbstractEngine engine = factory.getEngine(engineType, connectString);
          try
          {
              if (DSCTabControl1.SelectedTab == 0)
              {

                  DataObject[] objs = OverTimeList.dataSource.getAllDataObjects();
                  for (int i = 0; i < objs.Length; i++)
                  {
                      if (EmpNo.ValueText == objs[i].getData("EmpNo") && WorkDate.ValueText == objs[i].getData("WorkDate"))
                      {
                          MessageBox("已有重複資料");
                          return false;
                      }
                  }
                  if (retBool(engine, WorkDate.ValueText, EmpNo.ValueText))
                  {
                      MessageBox("資料庫已有重複資料");
                      return false;
                  }
                  else
                  {
                      Hashtable hs = selectovertime(engine, WorkDate.ValueText.Replace("/", ""), EmpNo.ValueText);
                      objects.setData("GUID", IDProcessor.getID(""));
                      //SheetNo.ValueText = "SMEM0106201500101";
                      // objects.setData("SheetNo", "SMEM0106201500101");
                      objects.setData("EmpNo", EmpNo.ValueText);
                      objects.setData("EmpName", EmpNo.ReadOnlyValueText);
                      objects.setData("WorkDate", WorkDate.ValueText);
                      objects.setData("STime", STime1.ValueText + ":" + STime3.ValueText);
                      objects.setData("ETime", ETime1.ValueText + ":" + ETime3.ValueText);
                      objects.setData("Hours", Hours.ValueText);
                      objects.setData("Reason", Reason.ValueText);
                      objects.setData("Note", Note.ValueText);
                      objects.setData("G01", hs["G01"].ToString());
                      objects.setData("OUT1", hs["OUT1"].ToString());
                      objects.setData("OverTime", hs["OverTime"].ToString());
                      objects.setData("WTID", hs["WTID"].ToString());
                      objects.setData("InCumbency", hs["InCumbency"].ToString());
                  }

              } //-----------------部门&&線別
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
                      dos.setChildClassString("WebServerProject.form.EH010201.EH010201B");
                      dos.setTableName("EH010201B");
                      dos.loadFileSchema();

                      DataObject obj = new DataObject();
                      obj.loadFileSchema(OverTimeList.dataSource.getChildClassString());

                      bool cc = false;
                     
                      for (int i = 0; i < dt.Rows.Count; i++)
                      {


                          DataObject[] objs = OverTimeList.dataSource.getAllDataObjects();
                          for (int j = 0; j < objs.Length; j++)
                          {
                              if (dt.Rows[i][0].ToString() == objs[j].getData("EmpNo"))
                              {
                                  cc = true;
                                  break;
                              }
                          }
                          if (cc)
                          { cc = false; continue; }

                          if (retBool(engine,WorkDate.ValueText, dt.Rows[i][0].ToString()))
                          {  continue; }
                          Hashtable hs = selectovertime(engine, WorkDate.ValueText.Replace("/", ""), dt.Rows[i][0].ToString());

                              obj.setData("GUID", IDProcessor.getID(""));
                              obj.setData("SheetNo", base.PageUniqueID);
                              obj.setData("EmpNo", dt.Rows[i][0].ToString());
                              obj.setData("EmpName", dt.Rows[i][1].ToString());
                              obj.setData("WorkDate", WorkDate.ValueText);
                              obj.setData("STime", STime1.ValueText + ":" + STime3.ValueText);
                              obj.setData("ETime", ETime1.ValueText + ":" + ETime3.ValueText);
                              obj.setData("Hours", Hours.ValueText);
                              obj.setData("Reason", Reason.ValueText);
                              obj.setData("Note", Note.ValueText);
                              obj.setData("G01", hs["G01"].ToString());
                              obj.setData("OUT1", hs["OUT1"].ToString());
                              obj.setData("OverTime", hs["OverTime"].ToString());
                              obj.setData("WTID", hs["WTID"].ToString());
                              obj.setData("InCumbency", hs["InCumbency"].ToString());


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
			
                      OverTimeList.dataSource = dos;
                      OverTimeList.dataSource.sort(new string[,] { { "WorkDate", DataObjectConstants.ASC }, { "EmpNo", DataObjectConstants.ASC } });

                  }


              }

          }
          catch (Exception ES)
          {
              return false;
          }
          finally { OverTimeList.updateTable(); try { engine.close(); } catch { } }


              return true;
 
      }


      public bool retBool(AbstractEngine engine, string datetime, string EmpNo)
      {
          string strSql = @"select a.*,b.IS_LOCK from [EH010201B] a,[EH010201A] b where a.SheetNo=b.SheetNo and EmpNo='" + EmpNo + "' and WorkDate='" + datetime + "' and IS_LOCK<>'Y'";
	// @"  select * from dbo.EH010201B where EmpNo='" + dt.Rows[i][0].ToString() + "' and WorkDate='" + WorkDate.ValueText + "'";
          DataTable hst = engine.getDataSet(strSql, "bmxz").Tables["bmxz"];
          DateTime dt1 = Convert.ToDateTime(datetime + " " + STime1.ValueText + ":" + STime3.ValueText), dt2 = Convert.ToDateTime(datetime + " " + ETime1.ValueText + ":" + ETime3.ValueText);
          if (hst != null && hst.Rows.Count > 0)
          {
              for (int j = 0; j < hst.Rows.Count; j++)
              {
                  if (Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["STime"].ToString()) < dt1 && Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["ETime"].ToString()) > dt1)
                  {
                      return true;
                  }
                  else if (Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["STime"].ToString()) < dt2 && Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["ETime"].ToString()) > dt2)
                  {
                      return true;
                  }
                  else if (Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["STime"].ToString()) < dt1 && Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["ETime"].ToString()) > dt2)
                  {
                      return true;
                  } 
		  else if (Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["STime"].ToString()) == dt1 || Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["ETime"].ToString()) == dt2)
                  {
                      return true;
                  }
              }
          }
          return false;
      }

      protected void GlassButton1_Click(object sender, EventArgs e)
      {
          try
          {

          }
          catch (Exception a) { }
      }


      protected void FileUpload1_AddOutline(DSCWebControl.FileItem currentObject, bool isNew)
      {
          try
          {
              FileUpload1.CheckData(0);
              FileUpload1.updateTable();
              DSCWebControl.FileItem[] fi = FileUpload1.getSelectedItem();
              //  MessageBox(fi[0].FILEPATH);




              DataSet ds = new DataSet();
              //构建连接字符串
              string path = Server.MapPath(@"../../../tempFolder/OWork/" + fi[0].FILEPATH); //@"C:\ECP\WebFormPT\web\tempFolder\OWork\c54a57e3-cc4f-402f-a19b-12264ce473ea.xls"; 

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
              OverTimeList.dataSource.clear();

              //關閉鏈接刪除文件
              Conn.Close();
              System.IO.File.Delete(path);


              string connectString = (string)Session["connectString"];
              string engineType = (string)Session["engineType"];

              IOFactory factory = new IOFactory();
              AbstractEngine engine = factory.getEngine(engineType, connectString);
	      int i=0;
  	      ArrayList ar = new ArrayList();
              for (int j = 1; j < dt.Rows.Count; j++)
              {
                   if (retBool(engine, dt.Rows[j][2].ToString(), dt.Rows[j][0].ToString(), dt.Rows[j][3].ToString(), dt.Rows[j][4].ToString())|| retBool2(engine, dt.Rows[j][0].ToString()))//
                  {	i++;
                      continue;
                  }
                  else
                  {
                  DataObject obj = new DataObject();
                  obj.loadFileSchema(OverTimeList.dataSource.getChildClassString());
                  obj.setData("GUID", IDProcessor.getID(""));
                  obj.setData("SheetNo", base.PageUniqueID);
                  obj.setData("EmpNo", dt.Rows[j][0].ToString());
                  obj.setData("EmpName", dt.Rows[j][1].ToString());
                  obj.setData("WorkDate", dt.Rows[j][2].ToString());
                  obj.setData("STime", dt.Rows[j][3].ToString());
                  obj.setData("ETime", dt.Rows[j][4].ToString());
                  obj.setData("Hours", dt.Rows[j][5].ToString());
                  obj.setData("Reason", dt.Rows[j][6].ToString());
                  obj.setData("Note", dt.Rows[j][7].ToString());

                  Hashtable hs = selectovertime(engine, dt.Rows[j][2].ToString().Replace("/", "").Replace("-", "-"), dt.Rows[j][0].ToString());
                  obj.setData("G01", hs["G01"].ToString());
                  obj.setData("OUT1", hs["OUT1"].ToString());
                  obj.setData("OverTime", hs["OverTime"].ToString());
                  obj.setData("WTID", hs["WTID"].ToString());
                  obj.setData("InCumbency", hs["InCumbency"].ToString());
                      bool acg=false;

                      string[] str = new string[4] { dt.Rows[j][0].ToString(), dt.Rows[j][2].ToString(), dt.Rows[j][3].ToString(), dt.Rows[j][4].ToString() };
                     
                      for (int s = 0; s < ar.Count; s++)
                      {
                          if (ischeak((string[])ar[s], str))
                          {
                              acg = true;
                          }
                      }
                         
                          
                          
                          if(acg)
                          {
                               i++;
                              continue;
                          }
                          OverTimeList.dataSource.add(obj);
                          ar.Add(str);
                  }
              }
              OverTimeList.dataSource.sort(new string[,] { { "WorkDate", DataObjectConstants.ASC }, { "EmpNo", DataObjectConstants.ASC } });
              OverTimeList.dataSource.compact();
              OverTimeList.updateTable();
        	 MessageBox("去除重複/異常項:"+i.ToString());  
	}
          catch (Exception exc)
          {
              MessageBox(exc.Message);
          }
          FileUpload1.dataSource.clear();
          FileUpload1.updateTable();
      }

      public bool ischeak(string[] sin,string[] cos)
      {//0工號 1WorkDate 2STime 3ETime
          if (sin == null)
          { return false; }
          if (cos[0] == null || cos[1] == null || cos[2] == null || cos[3] == null)
          { return true; }

          DateTime dr1 = Convert.ToDateTime(sin[1].ToString() + " " + sin[2].ToString()), dr2 = Convert.ToDateTime(sin[1].ToString() + " " + sin[3].ToString()), dt1 = Convert.ToDateTime(cos[1].ToString() + " " + cos[2].ToString()), dt2 = Convert.ToDateTime(cos[1].ToString() + " " + cos[3].ToString());
          
          if (sin[0].ToString() == cos[0].ToString())
          {
              if (sin[1].ToString() == cos[1].ToString())
              {
                  if (dr1 < dt1 && dr2 > dt1)
                  {
                      return true;
                  }
                  else if (dr1 < dt2 && dr2 > dt2)
                  {
                      return true;
                  }
                  else if (dr1 < dt1 &&dr2 > dt2)
                  {
                      return true;
                  }
                  else if (dr1 == dt1 || dr2 ==dt2)
                  {
                      return true;
                  }
              }
          }

          return false;
      }



      public bool retBool(AbstractEngine engine, string datetime, string EmpNo,string Stime,string Etime)
      {
          string strSql = @"select a.*,b.IS_LOCK from [EH010201B] a,[EH010201A] b where a.SheetNo=b.SheetNo and EmpNo='" + EmpNo + "' and WorkDate='" + datetime + "' and IS_LOCK<>'Y'";
          // @"  select * from dbo.EH010201B where EmpNo='" +   dt.Rows[i][0].ToString() + "' and WorkDate='" + WorkDate.ValueText + "'";
          DataTable hst = engine.getDataSet(strSql, "bmxz").Tables["bmxz"];
          DateTime dt1 = Convert.ToDateTime(datetime + " " + Stime), dt2 = Convert.ToDateTime(datetime + " " + Etime);
          if (hst != null && hst.Rows.Count > 0)
          {
              for (int j = 0; j < hst.Rows.Count; j++)
              {
                  if (Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["STime"].ToString()) < dt1 && Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["ETime"].ToString()) > dt1)
                  {
                      return true;
                  }
                  else if (Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["STime"].ToString()) < dt2 && Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["ETime"].ToString()) > dt2)
                  {
                      return true;
                  }
                  else if (Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["STime"].ToString()) < dt1 && Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["ETime"].ToString()) > dt2)
                  {
                      return true;
                  }
                  else if (Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["STime"].ToString()) == dt1 || Convert.ToDateTime(hst.Rows[j]["WorkDate"].ToString() + " " + hst.Rows[j]["ETime"].ToString()) == dt2)
                  {
                      return true;
                  }
              }
          }
          return false;
      }



      public bool retBool2(AbstractEngine engine, string EmpNo)
      {
	bool rest=false;
          try
          {
              SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
              string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where EmpNo ='" + si.fillerID + "'";
              DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];

              string sqlbmgr = @"select PartNo from HRUSERS where EmpNo ='" + EmpNo + "'";
              DataTable dtbmgr = engine.getDataSet(sqlbmgr, "bmEgr").Tables["bmEgr"];

              for (int i = 0; i < dtbm.Rows.Count; i++)
              {
                  if (dtbmgr.Rows[0][0].ToString().Trim() == dtbm.Rows[i][0].ToString().Trim())
                  {
                    return   false;
			
                  }
		else
		{
		rest=true;
		}
              }

          }catch(Exception e){return true;}
              return rest;
      }
}
