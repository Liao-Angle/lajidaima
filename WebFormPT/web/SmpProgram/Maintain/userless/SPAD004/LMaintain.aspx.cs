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


public partial class SmpProgram_Maintain_SPAD004_LMaintain : BaseWebUI.GeneralWebPage
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
        Master.maintainIdentity = "SPAD004M";
        Master.ApplicationID = "SMPFORM";
        Master.ModuleID = "SPAD";

        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.maintain.SPAD004.SmpTripBillSummary");

        Master.HiddenField = new string[] { "GUID", "CompanyCode", "OriginatorGUID", "EmpNumber", "DeptGUID", "DeptNo", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
		Master.DialogHeight = 620;
		Master.DialogWidth = 1100;
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
        string userId = (string)Session["UserID"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
                				
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPAD004.SmpTripBillSummaryAgent");

        if (!userId.Equals("2506") && !userId.Equals("1532") && !userId.Equals("3787") && !userId.Equals("3992") && !userId.Equals("4225")&& !userId.Equals("4019"))
          whereClause += " D_INSERTUSER = '" + (string)Session["UserGUID"] + "' order by D_INSERTTIME ";            
        
        agent.engine = engine;
		
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
        ListTable.NoDelete = true;  //取消刪除按鈕
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
        agent.loadSchema("WebServerProject.maintain.SPAD004.SmpTripBillSummaryAgent");
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