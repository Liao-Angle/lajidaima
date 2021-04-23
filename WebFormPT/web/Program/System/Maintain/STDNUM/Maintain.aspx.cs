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

public partial class Program_System_Maintain_STDNUM_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick+=new ProjectBaseWebUI_GeneralMaintain.SendButtonClickEvent(Master_SendButtonClick);
        Master.getListTable().RefreshButtonClick+=new DSCWebControl.DataList.RefreshButtonClickEvent(Program_System_Maintain_STDNUM_Maintain_RefreshButtonClick);
        Master.getListTable().GetFlowStatusString+=new DSCWebControl.DataList.GetFlowStatusStringEvent(Program_System_Maintain_STDNUM_Maintain_GetFlowStatusString);
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
        Master.maintainIdentity = "STDNUM";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMVAA";
        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.maintain.STDNUM.STDNUM");
        Master.HiddenField = new string[] { "GUID", "IS_LOCK","IS_DISPLAY", "DATA_STATUS"};

        Master.getListTable().IsGeneralUse = false;
        Master.getListTable().FormTitle = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_stdnum_maintain_aspx.language.ini", "message", "Title", "歸檔號申請作業單");
        Master.DialogHeight = 470;
        Master.inputForm = "Form.aspx";
        Master.InitData(obj);

        
    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.STDNUM.STDNUMAgent");
        agent.engine = engine;

        setSession("whereClause", whereClause);

        bool res = agent.query(Master.filterWhereClause(whereClause));
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        Master.basedos = agent.defaultData;
    }
    public void Program_System_Maintain_STDNUM_Maintain_RefreshButtonClick()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.STDNUM.STDNUMAgent");

        agent.engine = engine;
        agent.query(Master.filterWhereClause((string)getSession("whereClause")));


        engine.close();

        Master.basedos = agent.defaultData;
        Master.getListTable().updateTable();
    }
    public DSCWebControl.FlowStatusData Program_System_Maintain_STDNUM_Maintain_GetFlowStatusString(DataObject objects, bool isNew)
    {
        DSCWebControl.FlowStatusData fd = new DSCWebControl.FlowStatusData();

        if (isNew)
        {
            fd.UIStatus = DSCWebControl.FlowStatusData.InitNew;
        }
        else
        {
            fd.UIStatus = DSCWebControl.FlowStatusData.InitModify;
            fd.ObjectGUID = objects.getData("GUID");
        }
        return fd;
    }

}
