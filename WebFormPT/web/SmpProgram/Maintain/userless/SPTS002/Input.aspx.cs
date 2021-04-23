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

                //公司別
                string userGUID = (string)Session["UserGUID"];
                SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
                ids = tsmp.getCompanyCodeName(engine, userGUID);
                CompanyCode.setListItem(ids);
                                
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
        Session["SPTS002_ICompCode"] = objects.getData("CompanyCode");
        CompanyCode.ValueText = objects.getData("CompanyCode");

        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPTS002.SmpTSSubjectDetail");
            detail.setTableName("SmpTSSubjectDetail");
            detail.loadFileSchema();
            objects.setChild("SmpTSSubjectDetail", detail);
        }
        else
        {
            detail = objects.getChild("SmpTSSubjectDetail");			
            CompanyCode.ReadOnly = true;			
        }

        //detail
        DataListSubject.dataSource = detail;
        DataListSubject.InputForm = "Detail.aspx";
        DataListSubject.DialogHeight = 260;
        DataListSubject.DialogWidth = 600;
        DataListSubject.HiddenField = new string[] { "GUID", "SubjectFormGUID", "DeptGUID", "CompanyCode", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        DataListSubject.reSortCondition("課程代號", DataObjectConstants.ASC);
        DataListSubject.updateTable();
		
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

            DataObjectSet detail = DataListSubject.dataSource;
	        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
	        {
	            DataObject dt = detail.getAvailableDataObject(i);
	            dt.setData("SubjectFormGUID", objects.getData("GUID"));
				if (isNew)
				{
                    dt.setData("SubjectNo", getCustomCourseNo(engine, "SMPTSSubjectSeqNo", companyCode));
				}
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
        agent.loadSchema("WebServerProject.maintain.SPTS002.SmpTSSubjectAgent");
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
	
	
	//年度教育訓練計劃開窗前
    protected bool DataListSubject_BeforeOpenWindow(DataObject objects, bool isAddNew)
    {
        DataObjectSet set = DataListSubject.dataSource;
        ArrayList subjectInfo = new ArrayList();
        
        for(int i=0; i< set.getAvailableDataObjectCount(); i++)
        {
            DataObject obj = set.getAvailableDataObject(i);
            subjectInfo.Add(obj.getData("GUID") + "," + obj.getData("SubjectName"));
        }
        setSession((string)Session["UserID"], "SubjectInfo", subjectInfo);
        
        return true;
    }
	
	/// <summary>
    /// 取得編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getCustomCourseNo(AbstractEngine engine, string code, string strCompanyID)
    {
        string sheetNo = "";
        try
        {                    
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("CompanyID", strCompanyID);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return sheetNo;
    }

    protected void CompanyCode_SelectChanged(string value)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        string sql = null;
        DataSet ds = null;
        int count = 0;
        string strMessage = "";
        string strId = CompanyCode.ValueText;

        if (!strId.Equals(""))
        {
            sql = " select count(*) qty from SmpTSSubjectForm where CompanyCode='" + strId + "' ";
            //sw.WriteLine("Tips SQL => " + sql);
            ds = engine.getDataSet(sql, "TEMP");
            count = ds.Tables[0].Rows.Count;

            if (count > 1)
            {
                strMessage += "課程主檔公司別已存在，請由查詢進入公司別後，再新增課程主檔!\n";
            }            
        }
        if (!strMessage.Equals(""))
        {
            MessageBox(strMessage);
            CompanyCode.ValueText = "";
            CompanyCode.Focus();
        }
    }


    protected bool DataListSubject_BeforeDeleteData()
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        string sql = "";
        string strMessage = "";
        int cnt = 0;
        string subjectDetailGUID = "";
        string subjectName = "";
        //System.IO.StreamWriter sw1 = null;

        try {
            //sw1 = new System.IO.StreamWriter(@"d:\temp\SPTS002.log", true); 

            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

            DataObject[] subj = DataListSubject.getSelectedItem();
            DataObjectSet setSubj = DataListSubject.dataSource;

            for (int i = 0; i < subj.Length; i++)
            {
                DataObject subjDataObject = subj[i];
                if (subjDataObject != null)
                {
                    subjectDetailGUID = subjDataObject.getData("GUID");
                    subjectName = subjDataObject.getData("SubjectName");
                    
                    //若年度開課計劃已存在, 課程主檔不可刪除
                    sql = "select count(*) from SmpTSSchDetail where SubjectDetailGUID='" + subjectDetailGUID + "' ";
                    //sw1.WriteLine("sql :" + sql);
                    cnt = (int)engine.executeScalar(sql);
                    if (cnt > 0)
                    {
                        strMessage += "年度開課計劃已存在, 課程主檔 [" + subjectName + "] 不可刪除";
                        DataListSubject.UnCheckAllData();
                        DataListSubject.Focus();
                    }
                }
            }            

            if (!strMessage.Equals(""))
            {
                MessageBox(strMessage);
                DataListSubject.UnCheckAllData();
                DataListSubject.Focus();
            }            
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
                engine.close();
            //if (sw1 != null)
            //    sw1.Close();            
        }       

        
        return true;
    }
}
