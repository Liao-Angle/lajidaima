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

public partial class SmpProgram_Form_SPKM005_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick += new ProjectBaseWebUI_GeneralMaintain.SendButtonClickEvent(Master_SendButtonClick);
        //Master.DeleteButtonClick += new ProjectBaseWebUI_GeneralMaintain.DeleteButtonClickEvent(Master_DeleteButtonClick);
        base.OnInit(e);
    }

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

    public void start()
    {
        Master.maintainIdentity = "SPKM005";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SPKM";
        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.form.SPKM005.SMWYAAA");

        Master.HiddenField = new string[] { "GUID", "Status", "ConfidentialLevel", "AuthorId", "AuthorOrgUnitId", "Site", "KeyWords", "ExpiryDate", "SheetNo", "External", "OriginatorOrgUnitId", "OriginatorOrgUnitName", "RevGUID", "FormGUID", "IndexCardGUID", "Released", "LatestFlag", "MajorTypeGUID", "SubTypeGUID", "DocTypeGUID", "DocPropertyGUID", "DocGUID", "AuthorGUID", "AuthorOrgUnitGUID", "D_INSERTUSER" };

        Master.getListTable().IsGeneralUse = true;
        Master.DialogHeight = 470;
        Master.inputForm = "Form.aspx";
        Master.InitData(obj);
        Master.getListTable().NoAdd = true;
        Master.getListTable().showExcel = true;
        Master.getListTable().FormTitle = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_form_spkm005_maintain_aspx.language.ini", "message", "Title", "文件檢視");

        string userId = (string)Session["UserID"];
        string whereClause = (string)Session[userId + "_SPKM005_Maintain_whereClause"];
        if (whereClause != null && !whereClause.Equals(""))
        {
            Master_SendButtonClick(whereClause);
            Master.getResultTab().SelectedTab = 1;
            Master.getListTable().isShowAll = true;
            
        }
        Session.Remove("SPKM005_whereClause");
    }

    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.form.SPKM005.SMWYAAAAgent");
        agent.engine = engine;

        if (whereClause.Equals(""))
        {
            whereClause = "(Released = 'Y' and LatestFlag='Y')";
        }
        else
        {
            whereClause += " and ((Released = 'Y' and LatestFlag='Y'))";
        }

        bool res = agent.query(whereClause);
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }

        string[,] orderby = new string[,] { { "DocNumber", DataObjectConstants.DESC } };
        agent.defaultData.sort(orderby);
        Master.basedos = agent.defaultData;
    }

    public void Master_DeleteButtonClick()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.form.SPKM005.SMWYAAAAgent");
        agent.engine = engine;
        agent.defaultData = Master.basedos;

        bool result = agent.update();
        engine.close();
        if (!result)
        {
            throw new Exception(engine.errorString);
        }
    }
}
