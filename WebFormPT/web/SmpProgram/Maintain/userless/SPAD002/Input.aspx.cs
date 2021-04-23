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

public partial class SmpProgram_Maintain_SPAD002_Input : BaseWebUI.DataListSaveForm
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
                SMVKAAA002.clientEngineType = (string)Session["engineType"];
                SMVKAAA002.connectDBString = (string)Session["connectString"];
				
				SMVKAAB005.clientEngineType = (string)Session["engineType"];
                SMVKAAB005.connectDBString = (string)Session["connectString"];
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
		SMVKAAA002.GuidValueText = objects.getData("SMVKAAA002"); //將值放入人員開窗元件中, 資料庫存放GUID
        SMVKAAA002.doGUIDValidate();                             //顯示開窗元件的人員名稱, 因人員為GUID
		SMVKAAA003.ValueText = objects.getData("SMVKAAA003"); 		
		SMVKAAA004.ValueText = objects.getData("SMVKAAA004"); 
		SMVKAAA005.ValueText = objects.getData("SMVKAAA005"); 
		SMVKAAA006.ValueText = objects.getData("SMVKAAA006"); 
				
        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPAD002.SMVKAAB");
            detail.setTableName("SMVKAAB");
            detail.loadFileSchema();
            objects.setChild("SMVKAAB", detail);
        }
        else
        {
            detail = objects.getChild("SMVKAAB");
			SMVKAAA002.ReadOnly = true;
        }

        //detail
        AgentDetailList.dataSource = detail;
        AgentDetailList.HiddenField = new string[] { "SMVKAAB001", "SMVKAAB002", "SMVKAAB003", "SMVKAAB004", "SMVKAAB005", "SMVKAAB006"};
        //AgentDetailList.reSortCondition("對象", DataObjectConstants.ASC);
        AgentDetailList.updateTable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
		IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
		
		bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
			objects.setData("SMVKAAA001", IDProcessor.getID(""));   
        }
		objects.setData("SMVKAAA002", SMVKAAA002.GuidValueText);
        objects.setData("id", SMVKAAA002.ValueText);
        objects.setData("userName", SMVKAAA002.ReadOnlyValueText);
        objects.setData("SMVKAAA003", SMVKAAA003.ValueText);
		objects.setData("SMVKAAA004", SMVKAAA004.ValueText);
		objects.setData("AgentId", SMVKAAB005.ValueText);
		objects.setData("AgentName", SMVKAAB005.ReadOnlyValueText);
		objects.setData("SMVKAAA005", "0");
		objects.setData("SMVKAAA006", "N");

        DataObjectSet detail = AgentDetailList.dataSource;
        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail.getAvailableDataObject(i);
            dt.setData("SMVKAAB002", objects.getData("SMVKAAA001"));
        }
		
		//檢查欄位資料
        string errMsg = checkFieldData(objects, engine);
        engine.close();
        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
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
        agent.loadSchema("WebServerProject.maintain.SPAD002.SMVKAAAAgent");
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
    ///管理者_SaveRow
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    protected bool AgentDetailList_SaveRowData(DataObject objects, bool isNew)
    {
        string strErrMsg = "";		
		
        if (SMVKAAB005.ValueText.Equals(""))
        {
            strErrMsg = "請選擇代理人!\n";
        }

        if (!strErrMsg.Equals(""))
        {
            MessageBox(strErrMsg);
            return false;
        }
		
        if (isNew)
        {
            objects.setData("SMVKAAB001", IDProcessor.getID(""));
            objects.setData("SMVKAAB002", "TEMP");                  
        }
		//MessageBox("SMVKAAA002 guid : " + SMVKAAA002.GuidValueText);
		
		
        objects.setData("SMVKAAB003", SMVKAAA002.GuidValueText);
        objects.setData("SMVKAAB004", "0");
		objects.setData("SMVKAAB005", SMVKAAB005.GuidValueText);
		objects.setData("id", SMVKAAB005.ValueText);
        objects.setData("userName", SMVKAAB005.ReadOnlyValueText);       
		objects.setData("SMVKAAB006", "0");        

        return true;
    }

    /// <summary>
    ///管理者_ShowRow
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    protected void AgentDetailList_ShowRowData(DataObject objects)
    {
        SMVKAAB005.tableName = "Users";
        SMVKAAB005.serialNum = "003";
        SMVKAAB005.idIndex = 2;
        SMVKAAB005.valueIndex = 3;
        
        SMVKAAB005.GuidValueText = objects.getData("SMVKAAB005");
        SMVKAAB005.doGUIDValidate();
    }
	
	public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
	
		string userId = objects.getData("SMVKAAA002");
        string startDate = objects.getData("SMVKAAA003");
		string endDate = objects.getData("SMVKAAA004");
		
		//起始時間不可大於結束時間
		DateTime startDateTime = Convert.ToDateTime(startDate);
        DateTime endDateTime = Convert.ToDateTime(endDate);
        if (startDateTime.CompareTo(endDateTime) > 0)
        {
            errMsg += "代理起始時間不可大於結束時間!\n";
        }
		        
		//檢核代理期間不可重疊
        string sql = "select * from SMVKAAA where SMVKAAA002='" + userId + "' and ('" + startDate + "' < SMVKAAA004 and '" + endDate + "' > SMVKAAA003)";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
			errMsg += "代理期間日期不可重疊, 已有設定的代理期間.";			
            //throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad002_input_aspx.language.ini", "message", "ids1","代理期間日期不可重疊已設定代理期間."));
        }
		engine.close();
		
        return errMsg;
    }

	
}
