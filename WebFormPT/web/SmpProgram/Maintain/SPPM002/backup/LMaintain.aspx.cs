using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class SmpProgram_Maintain_SPPM002_LMaintain : BaseWebUI.GeneralWebPage
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
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script language=javascript>");

                sb.Append(" function clickImport(){");
                sb.Append("   parent.window.openWindowGeneral('自定評核人員匯入作業','../Maintain/SPPM002/Import.aspx' ,'','','',true,true);");
                sb.Append(" }");
                sb.Append("</script>");

                Type ctype = this.GetType();
                ClientScriptManager cm = Page.ClientScript;
                if (!cm.IsStartupScriptRegistered(ctype, "clickSubmitScript"))
                {
                    cm.RegisterStartupScript(ctype, "clickSubmitScript", sb.ToString());
                }

                GlassButtonImport.BeforeClick = "clickImport";
                
                start();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void start()
    {
        Master.maintainIdentity = "SPPM002M";
        Master.ApplicationID = "SMPFORM";
        Master.ModuleID = "SPPM";

        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.maintain.SPPM002.SmpPmUserAssessment");

        Master.HiddenField = new string[] { "GUID", "UserGUID", "deptOID", "FirstAssessUserGUID", "SecondAssessUserGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };

        Master.DialogHeight = 600;
        Master.DialogWidth = 840;
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
        agent.loadSchema("WebServerProject.maintain.SPPM002.SmpPmUserAssessmentAgent");
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
        ListTable.showExcel = true;
        //ListTable.NoDelete = true;
        //ListTable.NoModify = true;
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
        agent.loadSchema("WebServerProject.maintain.SPPM002.SmpPmUserAssessmentAgent");
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