using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class SmpProgram_Maintain_SPTS004_LMaintain : BaseWebUI.GeneralWebPage
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
        Master.maintainIdentity = "SPTS004M";
        Master.ApplicationID = "SMPFORM";
        Master.ModuleID = "SPTS"; //教育訓練模組

        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.maintain.SPTS004.SmpTSLecturer");

        Master.HiddenField = new string[] { "GUID", "CompanyCode", "LecturerSource", "InLecturerGUID", "InLecturerDeptGUID", "ExtCompany", "SpecialSkill", "Experience", "Orientation", "Professional", "Management", "Quality", "EHS", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };

        Master.DialogHeight = 430;
        Master.DialogWidth = 720;
        Master.inputForm = "Input.aspx";
        Master.InitData(obj);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script language=javascript>");

        sb.Append(" function clickImport(){");
        sb.Append("   parent.window.openWindowGeneral('人員資格鑑定說明書匯入作業','../Maintain/SPTS012/Import.aspx' ,'','','',true,true);");
        sb.Append(" }");
        sb.Append("</script>");

        Type ctype = this.GetType();
        ClientScriptManager cm = Page.ClientScript;
        if (!cm.IsStartupScriptRegistered(ctype, "clickSubmitScript"))
        {
            cm.RegisterStartupScript(ctype, "clickSubmitScript", sb.ToString());
        }

        GlassButtonImport.BeforeClick = "clickImport";
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
        agent.loadSchema("WebServerProject.maintain.SPTS004.SmpTSLecturerAgent");
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
        //ListTable.NoModify = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Master_DeleteButtonClick()
    {
        /*
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPTS004.SmpTSLecturerAgent");
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
        */
        
        throw new Exception("不允許刪除!");
    }
}