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
using System.IO;
using System.Xml;
using WebServerProject.auth;
using NPOI.HSSF.UserModel;

public partial class SmpProgram_maintain_SPTS002_Input : BaseWebUI.DataListSaveForm
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

                string userGUID = (string)Session["UserGUID"];
                string sql = "select h.CompanyCode from SmpTSAdmForm h, SmpTSAdmDetail l where l.AdmType='1'  and AdmTypeGUID='" + userGUID + "' and h.GUID = l.AdmFormGUID " +
                    "union select h.CompanyCode from SmpTSAdmForm h, SmpTSAdmDetail l, Group_User u where  l.AdmType='21' and u.UserOID='" + userGUID + "' and h.GUID = l.AdmFormGUID and l.AdmTypeGUID=u.GroupOID " +
                    "union select h.CompanyCode from SmpTSAdmForm h, SmpTSAdmDetail l, Functions f where  l.AdmType='9' and f.occupantOID='" + userGUID + "' and h.GUID = l.AdmFormGUID and l.AdmTypeGUID=f.organizationUnitOID";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                int count = ds.Tables[0].Rows.Count;
                ids = new string[1 + count, 2];
                ids[0, 0] = "";
                ids[0, 1] = "";

                for (int i = 0; i < count; i++)
                {
                    string companyCode = ds.Tables[0].Rows[i][0].ToString();
                    string companyName = "";
                    ids[1 + i, 0] = companyCode;
                    if (companyCode.Equals("SMP"))
                    {
                        companyName = "新普科技";
                    }
                    else if (companyCode.Equals("TP"))
                    {
                        companyName = "中普科技";
                    }
                    ids[1 + i, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", companyCode, companyName);
                }
                CompanyCode.setListItem(ids);

                //已產生年度開課計劃
                ids = new string[,]{
                    {"",""},
                    {"Y","是"},
                    {"N","否"}              
                };
                ProduceSch.setListItem(ids);
                ProduceSch.ValueText = "N";
				
				//file Adapter
                WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
                string fileAdapter = sp.getParam("FileAdapter"); //系統參數
                AbstractEngine fileEngine = factory.getEngine(engineType, connectString);

                //匯入檔案元件初始化
                FileUploadPlan.FileAdapter = fileAdapter;
                FileUploadPlan.engine = fileEngine;
                FileUploadPlan.tempFolder = Server.MapPath("~/tempFolder");
                FileUploadPlan.maxLength = 10485760 * 3;
                FileUploadPlan.readFile("");
                FileUploadPlan.Display = true;
				               
                //AdmTypeGUID.clientEngineType = (string)Session["engineType"];
                //AdmTypeGUID.connectDBString = (string)Session["connectString"];
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
        PlanYear.ValueText = objects.getData("PlanYear");
        ProduceSch.ValueText = objects.getData("ProduceSch");
        Remark.ValueText = objects.getData("Remark");
		
		Session["SPTS002_IPlanYear"] = objects.getData("PlanYear");

        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPTS002.SmpTSPlanDetail");
            detail.setTableName("SmpTSPlanDetail");
            detail.loadFileSchema();
            objects.setChild("SmpTSPlanDetail", detail);
			
			TSSchCreateButton.Enabled = false;
			ProduceSch.ReadOnly = true;
        }
        else
        {
            detail = objects.getChild("SmpTSPlanDetail");
			
            CompanyCode.ReadOnly = true;
			PlanYear.ReadOnly = true;
			ProduceSch.ReadOnly = true;
			Remark.ReadOnly = true;
			
        }

        //detail
        DataListPlan.dataSource = detail;
        DataListPlan.InputForm = "Detail.aspx";
        DataListPlan.DialogHeight = 450;
        DataListPlan.DialogWidth = 700;
        DataListPlan.HiddenField = new string[] { "GUID", "PlanFormGUID", "DeptGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        DataListPlan.reSortCondition("預計開課月份", DataObjectConstants.ASC);
        DataListPlan.updateTable();
		
		if (objects.getData("ProduceSch").Equals("Y")){
			TSSchCreateButton.Enabled = false;
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
	        string companyCode = CompanyCode.ValueText;
	        string companyName = "";

	        if (companyCode.Equals("SMP"))
	        {
	            companyName = "新普科技";
	        }
	        else if (companyCode.Equals("TP"))
	        {
	            companyName = "中普科技";
	        }
	        objects.setData("CompanyCode", companyCode);
	        objects.setData("CompanyName", companyName);
			
	        if (isNew)
	        {
	            objects.setData("GUID", IDProcessor.getID(""));
	            objects.setData("IS_DISPLAY", "Y");
	            objects.setData("IS_LOCK", "N");
	            objects.setData("DATA_STATUS", "Y");
	        }  
			objects.setData("CompanyCode", CompanyCode.ValueText);
			objects.setData("PlanYear", PlanYear.ValueText);
			objects.setData("ProduceSch", ProduceSch.ValueText);
	        objects.setData("Remark", Remark.ValueText);

            DataObjectSet detail = DataListPlan.dataSource;
	        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
	        {
	            DataObject dt = detail.getAvailableDataObject(i);
	            dt.setData("PlanFormGUID", objects.getData("GUID"));
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
        agent.loadSchema("WebServerProject.maintain.SPTS002.SmpTSPlanAgent");
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
	
	protected void CreateButton_Click(object sender, EventArgs e)
    {
		string connectString = (string)Session["connectString"];
	    string engineType = (string)Session["engineType"];

	    IOFactory factory = new IOFactory();
	    AbstractEngine engine = factory.getEngine(engineType, connectString);
			
		string strCompanyCode = CompanyCode.ValueText;
		string strPlanYear = PlanYear.ValueText;
		
		try
        {     
	        
			string sql= "";
			
			sql  = " insert into SmpTSSchDetail ";
			sql += "  select lower(newid()),(select GUID from SmpTSSchForm where CompanyCode='"+strCompanyCode+"' and CourseYear='"+strPlanYear+"'),  pd.GUID, '1', EstimateMonth, CourseNo, CourseName, DeptGUID, TrainingHours, NumberOfPeople, TrainingObject, InOut, CourseType, Fees, EvaluationLevel, TTQS, 'N', 'Y','N','Y', 'ece65fccd36c1004815bdfcbc6e1a37a', getdate(), 'ece65fccd36c1004815bdfcbc6e1a37a', getdate() ";
			sql += " from SmpTSPlanDetail pd, SmpTSPlanForm pf where pf.GUID=pd.PlanFormGUID and pf.CompanyCode='" + strCompanyCode + "' and pf.PlanYear='"+ strPlanYear +"'  ";
			sql += " and  pd.GUID not in ( select distinct PlanDetailGUID from SmpTSSchDetail )  ";	
			engine.executeSQL(sql);
	        //engine.close();
			//Page.Response.Write("alert('sql:" + sql + "');");

	        sql = "update SmpTSPlanForm set  ProduceSch='Y'  where CompanyCode='" + strCompanyCode + "' and  PlanYear='"+ strPlanYear +"'  ";
	        engine.executeSQL(sql);
	        engine.close();

	        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_input_aspx.language.ini", "message", "QueryError2", "產生完畢"));
		}
        catch (Exception ex)
        {
            writeLog(ex);
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_input_aspx.language.ini",
                        "message", "QueryError1", "處理檔案發生錯誤. 可能無可新增資料, 或是系統處理錯誤. 錯誤訊息為: ") + ex.Message);
        }
        finally
        {
           // if (engine != null)
          //      engine.close();
          //  if (sw1 != null)
          //      sw1.Close();
        }
    }
	
	
	
	 /// <summary>
    /// 取得Excel檔案內容
    /// </summary>
    /// <param name="fName"></param>
    /// <returns></returns>
    private ArrayList getData(string fName)
    {
        ArrayList aryData = new ArrayList();
        string filePath = string.Format(fName);
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        HSSFWorkbook wk = new HSSFWorkbook(fs);
        HSSFSheet hst = (HSSFSheet)wk.GetSheetAt(0);
        for (int i = 0; i <= hst.LastRowNum; i++)
        {
            if (i == 0)
            {
                continue;
            }
            HSSFRow row = (HSSFRow)hst.GetRow(i);
            int cellNum = row.LastCellNum;
            string[] values = new string[cellNum];
            for (int j = 0; j < cellNum; j++)
            {
                string cellValue = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
                values[j] = cellValue;
                //MessageBox(cellValue);
            }
            aryData.Add(values);
        }
        fs.Close();
        return aryData;
    }

	
	/// <summary>
    /// 開啟上傳年度教育訓練計劃檔案
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PlanImport_Click(object sender, EventArgs e)
    {
        FileUploadPlan.openFileUploadDialog();
    }


    /// <summary>
    /// 上傳年度教育訓練計劃檔案後執行匯入動作
    /// </summary>
    /// <param name="currentObject"></param>
    /// <param name="isNew"></param>
    protected void Plan_AddOutline(DSCWebControl.FileItem currentObject, bool isNew)
    {
		//Page.Response.Write("alert('"+ aaaaaa+"');");  
        IOFactory factory = null;
        AbstractEngine engine = null;
        System.IO.StreamWriter sw1 = null;
		
        try
        {            
            sw1 = new System.IO.StreamWriter(@"d:\temp\SPTS002.log", true);
            sw1.WriteLine("log...");
           
            PlanFileName.ValueText = currentObject.FILENAME + "." + currentObject.FILEEXT;
           
            //檔案上傳入TempFolder                          
            string tempPath = Server.MapPath("~/tempFolder") + "\\";
            string fName = tempPath + currentObject.FILEPATH ;
			
                      
            //取得匯入資料                    
            ArrayList aryData = getData(fName);
            sw1.WriteLine("aryData :" + aryData.Count);
            //取得欄位資料加入datalist
            DataObjectSet dos = DataListPlan.dataSource;
            string fileMsg = "";
            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
            //string sqlUser = "select OID from Users where id = ";
            string sqlDept = "select OID from SmpOrgUnitAll where deptId = ";

            for (int i = 0; i < aryData.Count; i++)
            {
                DataObject objects = dos.create();
                string[] data = (string[])aryData[i];
				//sw1.WriteLine("data[0] :" + data[0]);
                if (data[0].Equals("ADD"))
                {
                    //string employeeGuid = (string)engine.executeScalar(sqlUser + "'" + data[1] + "'");
                    string deptGuid = (string)engine.executeScalar(sqlDept + "'" + data[12] + "'");
					//sw1.WriteLine("deptGuid :" + data[12]+"-"+ data[13]);
                    if (deptGuid != null)
                    {
                        objects.setData("GUID", IDProcessor.getID(""));
                        objects.setData("PlanFormGUID", "temp");                        
                        objects.setData("EstimateMonth", data[1]);
                        objects.setData("CourseNo", data[2]);
						objects.setData("CourseName", data[3]);
                        objects.setData("TrainingHours", data[4]);
                        objects.setData("NumberOfPeople", data[5]);
                        objects.setData("TrainingObject", data[6]);
                        objects.setData("InOut", data[7]);
                        objects.setData("CourseType", data[8]);
                        objects.setData("Fees", data[9]);
                        objects.setData("EvaluationLevel", data[10]);
                        objects.setData("TTQS", data[11]);
                        objects.setData("DeptGUID", deptGuid);
                        objects.setData("deptId", data[12]);
                        objects.setData("deptName", data[13]);
						//objects.setData("id", data[14]);
                        //objects.setData("organizationUnitName", data[15]);
                        //objects.setData("CompanyCode", data[16]);
                        //objects.setData("PlanYear", data[17]);
                        //objects.setData("ProduceSch", data[18]);
						//objects.setData("Remark", data[19]);
						objects.setData("IS_LOCK", "N");
                        objects.setData("IS_DISPLAY", "Y");
                        objects.setData("DATA_STATUS", "Y");
                        fileMsg += "第[" + i + "]列加入清單中:" + objects.saveXML();
                        dos.add(objects);
                    }
                }
            }
            DataListPlan.dataSource = dos;
            DataListPlan.updateTable();

            //刪除上傳檔案
            System.IO.File.Delete(fName);

            string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_input_aspx.language.ini",
                "message", "returnMsg", "Excel讀取完成");
				
				
        }
        catch (Exception e)
        {
            writeLog(e);
			/*
            alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_import_aspx.language.ini",
                        "message", "alertMsg", "處理檔案發生錯誤. 可能您上傳的檔案格式不和, 或是系統處理錯誤. 錯誤訊息為: ") + e.Message);
			Page.Response.Write("alert('"+ eMsg+"');");  			
			*/
        }
        finally
        {
            if (engine != null)
                engine.close();
            if (sw1 != null)
                sw1.Close();
        }
			
    }
	
	//check 計劃年度
    protected void PlanYear_TextChanged(object sender, EventArgs e)
    {
        int n;
        if ((!int.TryParse(PlanYear.ValueText, out n)) || (PlanYear.ValueText.Length !=4))
        {
            MessageBox("計劃年度，請輸入西元年(YYYY)");
            PlanYear.ValueText = "";
            PlanYear.Focus();
        }

    }

}
