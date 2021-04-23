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
using WebServerProject.maintain.SMVKC;

public partial class Program_System_Maintain_SMVKC_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVKC";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                initData();
            }
        }
    }

    public void initData()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        SMVKAAB003.clientEngineType = engineType;
        SMVKAAB003.connectDBString = connectString;
        SMVKAAB005.clientEngineType = engineType;
        SMVKAAB005.connectDBString = connectString;
        FlowID.clientEngineType = engineType;
        FlowID.connectDBString = connectString;
        SMVKAAC003.clientEngineType = engineType;
        SMVKAAC003.connectDBString = connectString;
        SMVKAAC004.clientEngineType = engineType;
        SMVKAAC004.connectDBString = connectString;
        SMVKAAC005.clientEngineType = engineType;
        SMVKAAC005.connectDBString = connectString;

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMVKCBAgent cbAgent = new SMVKCBAgent();
        cbAgent.engine = engine;
        //string whereClause = " (1=1) order by SMVKAAA003,ownerName,SMVKAAB004";
		//201410 Eva調整預設只能看自己的代理人設定
		string whereClause = "  SMVKAAB003 = '" + (string)Session["UserGUID"] + "'  order by SMVKAAA003,ownerName,SMVKAAB004";
        bool res = cbAgent.query(whereClause);

        if (!engine.errorString.Equals(""))
        {
            engine.close();
            throw new Exception(engine.errorString);
        }
        SubsList.NoAdd = true;
        SubsList.NoDelete = true;
        SubsList.NoModify = true;
        SubsList.InputForm = "";
        SubsList.HiddenField = new string[] { "SMVKAAB001", "SMVKAAB002", "SMVKAAB003", "SMVKAAB005", "SMVKAAB006" };
        SubsList.dataSource = cbAgent.defaultData;
        SubsList.updateTable();

        SMVKCCAgent ccAgent = new SMVKCCAgent();
        ccAgent.engine = engine;
        whereClause = " (1=1) order by SMVKAAA003,ownerName,SMVKAAC007";
        res = ccAgent.query(whereClause);
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        ProcessSubList.NoAdd = true;
        ProcessSubList.NoDelete = true;
        ProcessSubList.NoModify = true;
        ProcessSubList.InputForm = "";
        ProcessSubList.HiddenField = new string[] { "SMVKAAC001", "SMVKAAC002", "SMVKAAC003", "SMVKAAC004", "SMVKAAC005", "SMVKAAC006", "SMVKAAC009" };
        ProcessSubList.dataSource = ccAgent.defaultData;
        ProcessSubList.updateTable();
    }

    protected void SubsCancel_Click(object sender, EventArgs e)
    {
        SMVKAAB003.GuidValueText = "";
        SMVKAAB003.doGUIDValidate();
        SMVKAAB005.GuidValueText = "";
        SMVKAAB005.doGUIDValidate();
        SubsDateTime.ValueText = "";
    }
    protected void SubsQuery_Click(object sender, EventArgs e)
    {
        string whereClause = "";
        if (!SMVKAAB003.GuidValueText.Equals(""))
        {//被代理人
            whereClause += " SMVKAAB003 = '" + SMVKAAB003.GuidValueText + "' and ";
        }
        if (!SMVKAAB005.GuidValueText.Equals(""))
        {//代理人
            whereClause += " SMVKAAB005 = '" + SMVKAAB005.GuidValueText + "' and ";
        }
        if (!SubsDateTime.ValueText.Equals(""))
        {//日期
            whereClause += " (SMVKAAA003 <= '" + SubsDateTime.ValueText + "' and SMVKAAA004 >= '" + SubsDateTime.ValueText + "') and ";
        }
        if (!whereClause.Equals(""))
        {
            whereClause = whereClause.Substring(0, whereClause.Length - 4);
            whereClause += " order by SMVKAAA003,ownerName,SMVKAAB004";
        }
        else
        {
            whereClause = " (1=1) order by SMVKAAA003,ownerName,SMVKAAB004";
        }

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        SMVKCBAgent cbAgent = new SMVKCBAgent();
        cbAgent.engine = engine;
        bool res = cbAgent.query(whereClause);
        engine.close();
        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        
        SubsList.dataSource = cbAgent.defaultData;
        SubsList.updateTable();
    }
    protected void ProcessSubCancel_Click(object sender, EventArgs e)
    {
        SMVKAAC003.GuidValueText = "";
        SMVKAAC003.doGUIDValidate();
        SMVKAAC004.GuidValueText = "";
        SMVKAAC004.doGUIDValidate();
        ProcessSubDateTime.ValueText = "";
        FlowID.ValueText = "";
        FlowID.doValidate();
        SMVKAAC005.GuidValueText = "";
        SMVKAAC005.doGUIDValidate();
    }
    protected void ProcessSubQuery_Click(object sender, EventArgs e)
    {
        string whereClause = "";
        if (!SMVKAAC003.GuidValueText.Equals(""))
        {//被代理人
            whereClause += " SMVKAAC003 = '" + SMVKAAC003.GuidValueText + "' and ";
        }
        if (!SMVKAAC004.GuidValueText.Equals(""))
        {//代理人
            whereClause += " SMVKAAC004 = '" + SMVKAAC004.GuidValueText + "' and ";
        }
        if (!ProcessSubDateTime.ValueText.Equals(""))
        {//日期
            whereClause += " (SMVKAAA003 <= '" + ProcessSubDateTime.ValueText + "' and SMVKAAA004 >= '" + ProcessSubDateTime.ValueText + "') and ";
        }
        if (!FlowID.ValueText.Equals(""))
        {//發起流程
            whereClause += " SMVKAAC006 = '" + FlowID.ValueText + "' and ";
        }
        if (!SMVKAAC005.GuidValueText.Equals(""))
        {//發起部門
            whereClause += " SMVKAAC005 = '" + SMVKAAC005.GuidValueText + "' and ";
        }
        if (!whereClause.Equals(""))
        {
            whereClause = whereClause.Substring(0, whereClause.Length - 4);
            whereClause += " order by SMVKAAA003,ownerName,SMVKAAC007";
        }
        else
        {
            whereClause = " (1=1) order by SMVKAAA003,ownerName,SMVKAAC007";
        }

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        SMVKCCAgent ccAgent = new SMVKCCAgent();
        ccAgent.engine = engine;
        bool res = ccAgent.query(whereClause);
        engine.close();
        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }

        ProcessSubList.dataSource = ccAgent.defaultData;
        ProcessSubList.updateTable();
    }
}
