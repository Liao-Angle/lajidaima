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
using System.Data;

public partial class SmpProgram_Maintain_SPKM001_LMaintain : BaseWebUI.GeneralWebPage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick += new ProjectBaseWebUI_ListMaintainWithoutAuth.SendButtonClickEvent(Master_SendButtonClick);
        Master.DeleteButtonClick += new ProjectBaseWebUI_ListMaintainWithoutAuth.DeleteButtonClickEvent(Master_DeleteButtonClick);
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
                start();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void start()
    {
        //Session["PDFViewerFile"] = @"~\pdfs\ECP程(CutePdf).pdf";
        //Session["PDFViewerFile"] = @"d:\ECP\WebFormPT\FileStorage\EKM\PDF_TEMP\ECP程(CutePdf).pdf";

        Master.maintainIdentity = "SPKM001M";
        Master.ApplicationID = "SMPFORM";
        Master.ModuleID = "SPKM";
        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.maintain.SPKM001.SmpMajorType");

        Master.HiddenField = new string[] { "GUID", "DeptGUID", "id", "organizationUnitName", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        Master.NoDelete = true;
        //Master.getListTable().NoDelete = true;
        string[] usergroup = (string[])Session["usergroup"];
        var findgroup = usergroup.Where(p => p == "SPIT_ALL").FirstOrDefault();
        if (string.IsNullOrEmpty(findgroup))
            Master.NoAdd = true;

        Master.DialogHeight = 470;
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

        string UserID = (string)Session["UserID"];
        string UserName = (string)Session["UserName"];
        string[] usergroup = (string[])Session["usergroup"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPKM001.SmpMajorSubTypesAgent");
        agent.engine = engine;
        bool res = false;
        var findgroup = usergroup.Where(p => p == "SPIT_ALL").FirstOrDefault();
        if (string.IsNullOrEmpty(findgroup))
        {
            string m_whereClause = whereClause;
            if (whereClause.Equals(""))
                m_whereClause = "dbo.SmpGetMajorTypeAdm(GUID) like '%," + UserID + "%'";
            else
                m_whereClause = whereClause + " and dbo.SmpGetMajorTypeAdm(GUID) like '%," + UserID + "%'";
            res = agent.query(m_whereClause, 1000);
        }
        else
            res = agent.query(whereClause);
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
    }

    /// <summary>
    /// 
    /// </summary>
    public void Master_DeleteButtonClick()
    {
        MessageBox("請失效資料！");
        //string connectString = (string)Session["connectString"];
        //string engineType = (string)Session["engineType"];

        //IOFactory factory = new IOFactory();
        //AbstractEngine engine = factory.getEngine(engineType, connectString);

        //NLAgent agent = new NLAgent();
        //agent.loadSchema("WebServerProject.maintain.SPKM001.SmpMajorSubTypesAgent");
        //agent.engine = engine;

        //agent.defaultData = Master.basedos;

        //bool res = agent.update();

        //engine.close();

        //if (!res)
        //{
        //    throw new Exception(engine.errorString);
        //}
        //if (!engine.errorString.Equals(""))
        //{
        //    throw new Exception(engine.errorString);
        //}
        //MessageBox("儲存成功");
    }


}