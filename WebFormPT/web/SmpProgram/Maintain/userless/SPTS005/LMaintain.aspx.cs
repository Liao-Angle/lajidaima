using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class SmpProgram_maintain_SPTS005_LMaintain : BaseWebUI.GeneralWebPage
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
        Master.maintainIdentity = "SPTS005M";
        Master.ApplicationID = "SMPFORM";
        Master.ModuleID = "SPTS"; //教育訓練模組

        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.maintain.SPTS005.SmpTSCourseForm");
        
        Master.HiddenField = new string[] { "GUID", "CompanyCode", "SchDetailGUID", "BriefIntro", "LecturerGUID", "DeptGUID", "UndertakerGUID", "StartTime","EndTime",
                                            "Hours","Place","UndertakerName","Way","WrittenTest","Implement","Satisfaction", "InOther","InOtherDesc",
                                            "Presentation","Certificate","Report","OJT",
                                            "OutOther","OutOtherDesc","Remark","CourseSource","IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
       
        Master.DialogHeight = 700;
        Master.DialogWidth = 920;
        Master.getListTable().showExcel = true;
        Master.getListTable().IsOpenWinUse = false;
        //Master.getListTable().IsPanelWindow = true;
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
        agent.loadSchema("WebServerProject.maintain.SPTS005.SmpTSCourseFormAgent");
        agent.engine = engine;

        //get CompanyCode
        string userGUID = (string)Session["UserGUID"];
        SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
        string companyCode = tsmp.getCompanyCode(engine, userGUID);
        if (companyCode != null && companyCode.Length > 0)
        {
            if (whereClause.Equals(""))
            {
                whereClause = " (CompanyCode in (" + companyCode + ")) ";
            }
            else
            {
                whereClause += " and (CompanyCode in (" + companyCode + ")) ";
            }
        }

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
        ListTable.NoDelete = true;
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
        agent.loadSchema("WebServerProject.maintain.SPTS005.SmpTSCourseFormAgent");
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
           
         
    }
}