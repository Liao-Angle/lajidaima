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

public partial class SmpProgram_maintain_SPTS003_Input : BaseWebUI.DataListSaveForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {  
				string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);               
                string[,] ids = null;  
				               
                //公司別
                string userGUID = (string)Session["UserGUID"];
                SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
                ids = tsmp.getCompanyCodeName(engine, userGUID);
                CompanyCode.setListItem(ids);
				
				//是否已結算
		        Closed.ValueText = "N";

                //file Adapter
                WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
                string fileAdapter = sp.getParam("FileAdapter"); //系統參數
                AbstractEngine fileEngine = factory.getEngine(engineType, connectString);

                //匯入檔案元件初始化
                FileUploadSch.FileAdapter = fileAdapter;
                FileUploadSch.engine = fileEngine;
                FileUploadSch.tempFolder = Server.MapPath("~/tempFolder");
                FileUploadSch.maxLength = 10485760 * 3;
                FileUploadSch.readFile("");
                FileUploadSch.Display = true;
			}
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        //head
        CompanyCode.ValueText = objects.getData("CompanyCode");
		//CompanyName.ValueText = objects.getData("CompanyName");
        SchYear.ValueText = objects.getData("SchYear");

        //set session
        Session["SPTS003_ICompanyCode"] = objects.getData("CompanyCode");
        Session["SPTS003_ISchYear"] = objects.getData("SchYear");
		
        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPTS003.SmpTSSchDetail");
            detail.setTableName("SmpTSSchDetail");
            detail.loadFileSchema();
            objects.setChild("SmpTSSchDetail", detail);
        }
        else
        {
            detail = objects.getChild("SmpTSSchDetail");
            CompanyCode.ReadOnly = true;
            SchYear.ReadOnly = true;
            //DeleteButton.Display = Closed.ValueText.Equals("Y") ? false : true;
        }
        //detail
        SchDetailList.dataSource = detail;		
		SchDetailList.InputForm = "Detail.aspx";
		//SchDetailList.NoDelete = true;
		SchDetailList.DialogHeight = 350;
		SchDetailList.DialogWidth = 700;
		SchDetailList.HiddenField = new string[] { "GUID", "SchFormGUID", "DeptGUID", "SubjectDetailGUID", "SchYear", "CompanyCode", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        SchDetailList.reSortCondition("計劃代號", DataObjectConstants.ASC);
        SchDetailList.updateTable();
        		
        //DeleteButton.Display = Closed.ValueText.Equals("Y") ? false : true;
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string sql = "select count(*) from SmpTSSchDetail d, SmpTSSchForm h where h.GUID=d.SchFormGUID and h.SchYear='" 
                + SchYear.ValueText + "' and h.CompanyCode='" + CompanyCode.ValueText + "' and d.Closed='Y' ";
            int cnt = (int)engine.executeScalar(sql);
            if (cnt > 0)
            {
                DeleteButton.Display = false;
            }                        
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            engine.close();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
		try
        {
			string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
			
	        bool isNew = (bool)getSession("isNew");
            string strSchYear = SchYear.ValueText;
            string companyName = "";
	        //objects.setData("CompanyCode", CompanyCode.ValueText);
	        if (isNew)
	        {
	            objects.setData("GUID", IDProcessor.getID(""));
	            objects.setData("IS_DISPLAY", "Y");
	            objects.setData("IS_LOCK", "N");
	            objects.setData("DATA_STATUS", "Y");
	        }  
			objects.setData("CompanyCode", CompanyCode.ValueText);
			//objects.setData("CompanyName", CompanyName.ValueText);
            if (CompanyCode.ValueText.Equals("SMP"))
            {
                companyName = "新普科技";
            }
            else if (CompanyCode.ValueText.Equals("TP"))
            {
                companyName = "中普科技";
            }
            objects.setData("CompanyName", companyName);

            objects.setData("SchYear", SchYear.ValueText);
			//objects.setData("Closed", Closed.ValueText);
			
	        DataObjectSet detail = SchDetailList.dataSource;
	        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
	        {
	            DataObject dt = detail.getAvailableDataObject(i);
	            dt.setData("SchFormGUID", objects.getData("GUID"));
				//if (isNew)
				//{
				//	dt.setData("CourseNo", getCustomCourseNo(engine, "SMPTSCourseSeqNo", strCourseYear));
				//}
	        }
		}
        catch (Exception e)
        {
            writeLog(e);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPTS003.SmpTSSchAgent");
        agent.engine = engine;
        agent.query("1=2");

        bool result = agent.defaultData.add(objects);
        if (!result)
        {
            engine.close();
            throw new Exception(agent.defaultData.errorString);
        }
        else
        {
            result = agent.update();
            engine.close();
            if (!result)
            {
                throw new Exception(engine.errorString);
            }
        }
    }

	
	/// <summary>
    /// 取得編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getCustomCourseNo(AbstractEngine engine, string code, string strYear)
    {
        string sheetNo = "";
        try
        {                    
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("PlanYear", strYear);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return sheetNo;
    }


    protected void SchImport_Click(object sender, EventArgs e)
    {
        FileUploadSch.openFileUploadDialog();
    }

    //check 計劃年度
    protected void SchYear_TextChanged(object sender, EventArgs e)
    {
        double n;
        if (!double.TryParse(SchYear.ValueText, out n) || (SchYear.ValueText.Length != 4)) 
        {
            MessageBox("計劃年度，請輸入西元年(YYYY)");
            SchYear.ValueText = "";
            SchYear.Focus();
        }
    }

    /// <summary>
    /// 上傳檔案後執行匯入動作
    /// </summary>
    /// <param name="currentObject"></param>
    /// <param name="isNew"></param>
    protected void Sch_AddOutline(DSCWebControl.FileItem currentObject, bool isNew)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        System.IO.StreamWriter sw1 = null;
        string fileMsg = "";
        string fName = "";
        string sql = "";
        string subjectGuid = "";
        string userGUID = (string)Session["UserGUID"];
        string now = DateTimeUtility.getSystemTime2(null);
		int counts = 0;
        int cnt = 0;
        try
        {
            sw1 = new System.IO.StreamWriter(@"d:\temp\SPTS003.log", true);    
            SchFileName.ValueText = currentObject.FILENAME + "." + currentObject.FILEEXT;

            //檔案上傳入TempFolder                          
            string tempPath = Server.MapPath("~/tempFolder") + "\\";
            fName = tempPath + currentObject.FILEPATH;

            //取得匯入資料                    
            SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
            ArrayList aryData = tsmp.getExcelData(fName);

            //取得欄位資料加入datalist
            DataObjectSet dos = SchDetailList.dataSource;

            //記錄原有的id,後續判斷是否重覆
            string schOriginal = "";
            for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            {
                DataObject obj = dos.getDataObject(i);
                schOriginal += obj.getData("SchNo") + ",";
            }
            
            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
			
			//判斷課程主檔中, 公司別是否存在, 若不存在
			cnt = (int)engine.executeScalar("select count(*) from SmpTSSubjectForm where CompanyCode = '" + CompanyCode.ValueText + "'");
            if (cnt < 1)
			{
				sql = " insert into SmpTSSubjectForm values ( NEWID(), '" + CompanyCode.ValueText + "','Y','N','Y', '" + userGUID + "', '" + now + "', '',''  ) ";
				sw1.WriteLine("insert into SmpTSSubjectForm :" + sql);	
                if (!engine.executeSQL(sql))
                {
                    throw new Exception(engine.errorString);
                }
			}
			
            string sqlDept = "select OID from SmpOrgUnitAll where deptId = ";
            string sqlCompany = "select o.id from Organization o, OrganizationUnit ou where ou.organizationOID=o.OID and ou.OID= ";
            string sqlSubjectCompany = "select GUID from SmpTSSubjectForm where CompanyCode= ";
            string sqlSubject = "select d.GUID from SmpTSSubjectDetail d, SmpTSSubjectForm h where h.GUID=d.SubjectFormGUID and DeptGUID = "; // and SubjectName=";
            
            for (int i = 0; i < aryData.Count; i++)
            {
                DataObject objects = dos.create();
                string[] data = (string[])aryData[i];
                if (data[0].Equals("ADD") && !data[5].Equals(""))
                {
                    string deptGuid = (string)engine.executeScalar(sqlDept + "'" + data[2] + "'");
                    string companyCode = (string)engine.executeScalar(sqlCompany + "'" + deptGuid + "'");
                    string subjectCompany = (string)engine.executeScalar(sqlSubjectCompany + "'" + companyCode + "'");
					string strSchYear = SchYear.ValueText;
                    string strCompanyCode = CompanyCode.ValueText;
					
					//sw1.WriteLine(sqlDept + "'" + data[2] +" -- getdeptGuid:" + deptGuid);
					//sw1.WriteLine(sqlCompany + "'" + deptGuid + " -get companyCode:" + companyCode);
					//sw1.WriteLine(sqlSubjectCompany + "'" + companyCode + " - get subjectCompany:" + subjectCompany);
					string seqNo = "";

                    //若課程主檔不存在, 先insert 資料至課程主檔
                    sql = "select count(*) from SmpTSSubjectDetail d, SmpTSSubjectForm h where h.GUID=d.SubjectFormGUID and DeptGUID='" + deptGuid + "' and SubjectName='" + data[5] + "' ";
                    sw1.WriteLine("課程主檔是否存在 SQL :" + sql);
                    cnt = (int)engine.executeScalar(sql);
                    if (cnt > 0)
                    {
						sw1.WriteLine("課程主檔已存在 SQL :" + sqlSubject+ "'" + deptGuid + "' and SubjectName = '" + data[5] );
                        subjectGuid = (string)engine.executeScalar(sqlSubject + "'" + deptGuid + "' and SubjectName = '" + data[5] + "' ");
                    }
                    else
                    {
                        try
                        {
                            seqNo = getCustomSubjectNo(engine, "SMPTSSubjectNo", CompanyCode.ValueText);
                            sw1.WriteLine("SMPTSSubjectNo :" + seqNo);
                            sql = " insert into SmpTSSubjectDetail values ( NEWID(), '" + subjectCompany + "', '" + seqNo + "' , '" + data[5] + "' , '"
                                + deptGuid + "', '" + data[6].Replace("_", "") + "', '" + data[9].Substring(0, 1) + "', '" + data[10].Substring(0, 1) + "','','Y','N','Y', '" + userGUID + "', '" + now + "', '',''  ) ";
							//sw1.WriteLine("insert into SmpTSSubjectDetail :" + sql);	
                            if (!engine.executeSQL(sql))
                            {
                                throw new Exception(engine.errorString);
                            }
                            subjectGuid = (string)engine.executeScalar(sqlSubject + "'" + deptGuid + "' and SubjectName = '" + data[5] + "' ");
							sw1.WriteLine("課程主檔不存在 SQL :" + sqlSubject+ "'" + deptGuid + "' and SubjectName = '" + data[5] );
                        }
                        catch (Exception e)
                        {
                            writeLog(e);
                            string eSMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts003_import_aspx.language.ini",
                                            "message", "alertMsg", "產生課程主檔-課程代號異常. 課程名稱為: ") + data[5] + " - errorMessage" + e.Message;
                            Page.Response.Write("alert('" + eSMsg + "');");
                        }
                    }
					                                        
                    if (subjectGuid != null && deptGuid != null && schOriginal.IndexOf(data[1]) == -1)
                    {
                        objects.setData("GUID", IDProcessor.getID(""));
                        objects.setData("SchFormGUID", "temp");
                        //objects.setData("SchNo", data[1]);

                        seqNo = getCustomSchNo(engine, "SMPTSSchNo", strCompanyCode + strSchYear);
                        //Page.Response.Write("alert('seqNO:" + seqNo + "');");
                        objects.setData("SchNo", seqNo);
                        
                        objects.setData("SchMonth", data[1]);
                        objects.setData("Quarter", getQuarter(data[1]));  //由月份取得季
                        objects.setData("DeptGUID", deptGuid);
                        objects.setData("DeptId", data[2]);
                        objects.setData("DeptName", data[3]);
                        objects.setData("SubjectDetailGUID", subjectGuid);
                        //sw1.WriteLine("subjectGuid :" + subjectGuid);
                        //objects.setData("SubjectNo", data[4]);
                        objects.setData("SubjectNo", getNewSubjectNo(engine, data[5], deptGuid));                        
                        objects.setData("SubjectName", data[5]);
                        objects.setData("TrainingHours", data[6].Replace("_", ""));
                        objects.setData("NumberOfPeople", data[7].Replace("_", ""));
                        objects.setData("TrainingObject", data[8]);
                        objects.setData("InOut", data[9].Substring(0, 1));
                        objects.setData("SubjectType", data[10].Substring(0, 1));
                        objects.setData("Fees", data[11].Replace("_", ""));
                        objects.setData("EvaluationLevel", data[12].Substring(0, 1));
                        objects.setData("TTQS", data[13]);
                        objects.setData("SchSource", data[14].Substring(0, 1));
                        objects.setData("Cancel", "N");
                        objects.setData("Closed", "N");
                        objects.setData("IS_LOCK", "N");
                        objects.setData("IS_DISPLAY", "Y");
                        objects.setData("DATA_STATUS", "Y");
						
						dos.add(objects);
                        counts++;
                    }
                    else
                    {
                        fileMsg += (i + 1) + ",";
                    }
					sw1.WriteLine("counts :" + counts );
                }
            }
            SchDetailList.dataSource = dos;
            SchDetailList.updateTable();

            string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts003_import_aspx.language.ini",
                "message", "returnMsg", "Excel讀取完成，匯入筆數:");
            returnMsg += counts;
            if (!fileMsg.Equals(""))
            {
                string tmpMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts003_import_aspx.language.ini",
                "message", "tmpMsg", "以下列數資料不完整或重覆無法匯入:");
                returnMsg += ";" + tmpMsg + fileMsg;
            }
            Page.Response.Write("alert('" + returnMsg + "');");
        }
        catch (Exception e)
        {
            sw1.WriteLine("fileMsg :" + fileMsg);
            writeLog(e);
            string eMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts003_import_aspx.language.ini",
                        "message", "alertMsg", "處理檔案發生錯誤. 可能您上傳的檔案格式不和, 或是系統處理錯誤. 錯誤訊息為: ") + e.Message;
            Page.Response.Write("alert('" + eMsg + "');");
        }
        finally
        {
            if (engine != null)
                engine.close();
            if (sw1 != null)
                sw1.Close();
            //刪除上傳檔案
            System.IO.File.Delete(fName);

        }
    }

    //年度開課計劃前
    protected bool SchDetailList_BeforeOpenWindow(DataObject objects, bool isAddNew)
    {
        DataObjectSet set = SchDetailList.dataSource;
        ArrayList schInfo = new ArrayList();

        for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
        {
            DataObject obj = set.getAvailableDataObject(i);
            schInfo.Add(obj.getData("GUID") + "," + obj.getData("SchNo"));
        }
        setSession((string)Session["UserID"], "SchInfo", schInfo);

        return true;
    }

    /// <summary>
    /// 取得課程主檔編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getCustomSubjectNo(AbstractEngine engine, string code, string strCompanyID)
    {
        string subjectNo = "";
        try
        {
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("CompanyID", strCompanyID);
            subjectNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return subjectNo;
    }
    protected bool SchDetailList_BeforeDeleteData()
    {       
        string strMessage = "";
        string closed = "";
        string subjectName = "";
        //System.IO.StreamWriter sw1 = null;

        try
        {
            //sw1 = new System.IO.StreamWriter(@"d:\temp\SPTS002.log", true); 
            DataObject[] sch = SchDetailList.getSelectedItem();
            DataObjectSet setSch = SchDetailList.dataSource;

            for (int i = 0; i < sch.Length; i++)
            {
                DataObject schDataObject = sch[i];
                if (schDataObject != null)
                {
                    closed = schDataObject.getData("Closed");
                    subjectName = schDataObject.getData("SubjectName");
                    if (closed.Equals("Y")) 
                    {
                        strMessage += "課程名稱 [" + subjectName + "] 已開班授課，不可刪除!\n";
                        SchDetailList.UnCheckAllData();
                        SchDetailList.Focus();
                    }                                      
                }
            }

            if (!strMessage.Equals(""))
            {
                MessageBox(strMessage);
                SchDetailList.UnCheckAllData();
                SchDetailList.Focus();
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {            
            //if (sw1 != null)
            //    sw1.Close();            
        }


        return true;
    }

    /// <summary>
    /// 取得編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getCustomSchNo(AbstractEngine engine, string code, string strYear)
    {
        string sheetNo = "";
        try
        {
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("CompanyIDYear", strYear);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return sheetNo;
    }

    /// <summary>
    /// 取得季別
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getQuarter(string value)
    {
        //重算季
        string strMonth = value;
        string strQuarter = "Q1";
        try
        {
            if (strMonth.Equals("01") || strMonth.Equals("02") || strMonth.Equals("03"))
            {
                strQuarter = Convert.ToString("Q1");
            }
            else if (strMonth.Equals("04") || strMonth.Equals("05") || strMonth.Equals("06"))
            {
                strQuarter = Convert.ToString("Q2");
            }
            else if (strMonth.Equals("07") || strMonth.Equals("08") || strMonth.Equals("09"))
            {
                strQuarter = Convert.ToString("Q3");
            }
            else
            {
                strQuarter = Convert.ToString("Q4");
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return strQuarter;
    }

    /// <summary>
    /// 取得編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getNewSubjectNo(AbstractEngine engine, string subjectName, string deptGUID)
    {
        string strSubjectNo = "";
        try
        {
            string sql = "select d.SubjectNo from SmpTSSubjectDetail d, SmpTSSubjectForm h where h.GUID=d.SubjectFormGUID and DeptGUID='" + deptGUID + "' and SubjectName='" + subjectName + "'";
            //sw1.WriteLine("課程主檔是否存在 SQL :" + sql);
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[][] result = new string[rows][];
            for (int i = 0; i < rows; i++)
            {
                strSubjectNo = ds.Tables[0].Rows[i][0].ToString();
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return strSubjectNo;
    }
}
