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

public partial class SmpProgram_Input : BaseWebUI.DataListSaveForm
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
                EMPOID.clientEngineType = (string)Session["engineType"];
                EMPOID.connectDBString = (string)Session["connectString"];

                MANAGEROID.clientEngineType = (string)Session["engineType"];
                MANAGEROID.connectDBString = (string)Session["connectString"];
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
        //EMPOID.ValueText = objects.getData("EMPOID");   
		EMPOID.GuidValueText = objects.getData("EMPOID");
        EMPOID.doGUIDValidate();		

        //MANAGEROID.ValueText = objects.getData("MANAGEROID"); 
		MANAGEROID.GuidValueText = objects.getData("MANAGEROID");
        MANAGEROID.doGUIDValidate();		
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");        
        objects.setData("EMPOID", EMPOID.GuidValueText);
		objects.setData("MANAGEROID", MANAGEROID.GuidValueText);
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
        agent.loadSchema("WebServerProject.maintain.SPAD003.FunctionsAgent");
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
			
		string strEmp = EMPOID.ValueText;
		string strMgr = MANAGEROID.ValueText;
		
		try
        {   
			string sql=  "update NaNa.dbo.Functions set  specifiedManagerOID='" +strMgr+ "'  where occupantOID='" + strEmp + "'  and  isMain='1'  ";
	        engine.executeSQL(sql);
	        engine.close();

	        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad003_input_aspx.language.ini", "message", "QueryError2", "更新完畢"));
		}
        catch (Exception ex)
        {
            writeLog(ex);
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad003_input_aspx.language.ini",
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

   
}
