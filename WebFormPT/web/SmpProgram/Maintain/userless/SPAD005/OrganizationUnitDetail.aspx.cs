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
using System.Xml;
using WebServerProject.org.Organization;
using WebServerProject.auth;

public partial class SmpProgram_Maintain_SPAD005_OrganizationUnitDetail : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                //權限判斷
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                AUTHAgent authagent = new AUTHAgent();
                authagent.engine = engine;

                int auth = authagent.getAuth("SPAD005M", (string)Session["UserID"], (string[])Session["usergroup"]);

                engine.close();

                //OriginatorGUID.clientEngineType = engineType;
                //OriginatorGUID.connectDBString = connectString;

                string mstr = "";
                if (auth == 0)
                {
                    Response.Redirect("~/NoAuth.aspx");
                }
                string UserID = (string)Session["UserID"];
                
                queryData();
            }
        }
    }
     protected void queryData()
    {
         string where = "";
         string OID = com.dsc.kernal.utility.Utility.filter(Request.QueryString["OID"]);
         //string Kind = com.dsc.kernal.utility.Utility.filter(Request.QueryString["KIND"]);
         if (!string.IsNullOrEmpty(OID))
         {
             where = " deptOID = '" + OID + "'";
             //break;
             
             query(where);
         }
    }

    protected void query(string where)
    {

        IOFactory factory = new IOFactory();
        AbstractEngine engine2 = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPAD005.SmpOrganizationAgent");
        agent.engine = engine2;
        if (!where.Equals(""))
        {
            where += " and (empLeaveDate is null or empLeaveDate = '') and len(empNumber)<8 ";
            //string empNo = OriginatorGUID.GuidValueText;
			string strEngName = empEName.ValueText.ToLower();
			string strChtName = empCName.ValueText;
            //string abstractValue = AbstractValue.ValueText;
            if (!strEngName.Equals(""))
            {
                where += " and lower(empEName) like '%" + strEngName + "%' ";
            }
			if (!strChtName.Equals(""))
            {
                where += " and lower(empName) like '%" + strChtName + "%' ";
            }
            agent.query(where);
        }
        else
        {
            agent.query("1=2");
        }
        
        DataObjectSet dos = agent.defaultData;
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine2);
        //string vieweDocUrl = sp.getParam("eKMViewDocUrl");
        engine2.close();
        string strDeptId = "";
        string strDeptName = "";
        string strDeptManagerName = "";
        string strManagerEmail = "";
		string strManagerTitle = "";
          

        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            DataObject dataObject = dos.getAvailableDataObject(i);

            strDeptId = dataObject.getData("deptId");
            strDeptName = dataObject.getData("deptName");
            strDeptManagerName = dataObject.getData("deptManagerName");
            strManagerEmail = dataObject.getData("deptManagerEmail");
			strManagerTitle = dataObject.getData("deptManagerTitle");
 
            dataObject.setData("deptId", dataObject.getData("deptId"));
            dataObject.setData("deptName", dataObject.getData("deptName"));
            dataObject.setData("deptMgrName", dataObject.getData("deptManagerName"));
            dataObject.setData("deptMgrEmail", dataObject.getData("deptManagerEmail"));
			dataObject.setData("deptMgrTitle", dataObject.getData("deptManagerTitle"));
        }
        FunctionList.setColumnStyle("deptOID", 130, DSCWebControl.GridColumnStyle.LEFT);


        deptId.ValueText = strDeptId;
        deptName.ValueText = strDeptName;
        deptMgrName.ValueText = strDeptManagerName;
        deptMgrEmail.ValueText = strManagerEmail;
		deptMgrTitle.ValueText = strManagerTitle;

        int pagesize = FunctionList.PageSize;
        FunctionList.Height = 30 * pagesize;
        FunctionList.dataSource = dos;

        FunctionList.showSettingPages = new Boolean[] { false, false, false, false, false, false, false, false, false, false };
        FunctionList.HiddenField = new string[] { "empGUID", "funcName", "funcOID", "managerId", "managerOID", "deptOID", "deptName", "deptManagerId", "deptManagerOID", "External", "OriginatorOrgUnitId", "OriginatorOrgUnitName", "empLeaveDate", "managerLeaveDate", "orgId", "orgOID", "titleOID", "deptManagerTitle", "deptManagerId","deptManagerName","deptId", "orgName", "deptManagerEmail" };
        FunctionList.updateTable();
        FunctionList.ReadOnly = true;
        FunctionList.InputForm = null;
        
    }

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        queryData();
    }
                
}
