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

public partial class SmpProgram_Maintain_SPAD003_Input : BaseWebUI.DataListSaveForm
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
                EmployeeGUID.clientEngineType = (string)Session["engineType"];
                EmployeeGUID.connectDBString = (string)Session["connectString"];

                ManagerGUID.clientEngineType = (string)Session["engineType"];
                ManagerGUID.connectDBString = (string)Session["connectString"];
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

        if (isNew)
        {

        }
        else
        {
            
        }
        
        //head
        
        EmployeeGUID.GuidValueText = objects.getData("EmployeeGUID");
        EmployeeGUID.doGUIDValidate();
		
		ManagerGUID.GuidValueText = objects.getData("ManagerGUID");
        ManagerGUID.doGUIDValidate();
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("EmployeeGUID", EmployeeGUID.GuidValueText);
        objects.setData("ManagerGUID", ManagerGUID.GuidValueText);

		MessageBox("aaa");
		MessageBox("Manager : " + ManagerGUID.ValueText + " " + ManagerGUID.ReadOnlyValueText );

		//NaNa Functions 直屬主管   
		string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "update NaNa.dbo.Functions set specifiedManagerOID ='" + ManagerGUID.GuidValueText + "' where occupantOID = '" + EmployeeGUID.GuidValueText + "'  and isMain='1' ";

		if (!engine.executeSQL(sql))
			{
		   // MessageBox("update NaNa error!");
				throw new Exception(engine.errorString);
			}
		else 
		{
			//MessageBox("update NaNa OK!");	
			engine.executeSQL(sql);            
		}
		engine.close();	

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
        agent.loadSchema("WebServerProject.maintain.SPAD003.SmpFunctionsQueryAgent");
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
}
