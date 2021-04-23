using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using WebServerProject.auth;
using NPOI.HSSF.UserModel;


public partial class SmpProgram_Maintain_SPTS003_LMaintain : BaseWebUI.GeneralWebPage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {        
		Master.SendButtonClick += new ProjectBaseWebUI_ListMaintain.SendButtonClickEvent(Master_SendButtonClick);        
        Master.DeleteButtonClick += new ProjectBaseWebUI_ListMaintain.DeleteButtonClickEvent(Master_DeleteButtonClick);
        base.OnInit(e);
    }

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
                start();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void start()
    {
        Master.maintainIdentity = "SPTS003M";
        Master.ApplicationID = "SMPFORM";
        Master.ModuleID = "SPTS";

        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.maintain.SPTS003.SmpTSSchForm");

        Master.HiddenField = new string[] { "GUID", "CompanyCode", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
		//Master.HiddenField = new string[] { "GUID", "PlanFormGUID", "CompanyName", "Closed", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };		

        Master.DialogHeight = 480;
		Master.DialogWidth = 700;
        Master.inputForm = "Input.aspx";
        Master.InitData(obj);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="whereClause"></param>
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
				
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPTS003.SmpTSSchAgent");
        agent.engine = engine;
		
		//get CompanyCode
        string userGUID = (string)Session["UserGUID"];
        SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
        string companyCode = tsmp.getCompanyCode(engine, userGUID);
        if (companyCode != null && companyCode.Length > 0)
        {
            if (whereClause.Equals(""))
            {
                whereClause = " (a.CompanyCode in (" + companyCode + ")) ";
            }
            else
            {
                whereClause += " and (a.CompanyCode in (" + companyCode + ")) ";
            }
        }
		//MessageBox("whereClause : " + whereClause);
		
        bool res = agent.query(whereClause);
        engine.close();

        if (!res)
        {
            throw new Exception(engine.errorString);
        }
        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        Master.basedos = agent.defaultData;
				
		DSCWebControl.DataList ListTable = Master.getListTable();
        //ListTable.NoDelete = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Master_DeleteButtonClick()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPTS003.SmpTSSchAgent");
        agent.engine = engine;

        agent.defaultData = Master.basedos;

        bool res = agent.update();

        engine.close();

        if (!res)
        {
            throw new Exception(engine.errorString);
        }
        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        MessageBox("儲存成功");
    }
}