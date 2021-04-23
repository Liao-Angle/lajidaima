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
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.Xml;
using WebServerProject.auth;


public partial class SmpProgram_Form_SPKM005_ViewDetail : BaseWebUI.GeneralWebPage
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
                int auth = authagent.getAuth("SPKM005", (string)Session["UserID"], (string[])Session["usergroup"]);
                engine.close();
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
         string Kind = com.dsc.kernal.utility.Utility.filter(Request.QueryString["KIND"]);
         if (!string.IsNullOrEmpty(OID))
         {
             switch (Kind)
             {
                 case "Major":
                     where = "MajorTypeGUID = '" + OID + "'";
                     break;
                 case "Sub":
                     where = "SubTypeGUID = '" + OID + "'";
                     break;
                 case "Doc":
                     where = "DocTypeGUID = '" + OID + "'";
                     break;
             }
             query(where);
         }
    }

    protected void query(string where)
    {

        IOFactory factory = new IOFactory();
        AbstractEngine engine2 = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.form.SPKM005.SMWYAAAAgent");
        agent.engine = engine2;
        if (!where.Equals(""))
        {
            where += " and ((Released = 'Y' and LatestFlag='Y'))";
            string docName = DocName.ValueText;
            string abstractValue = AbstractValue.ValueText;
            if (!docName.Equals(""))
            {
                where += " and lower(Name) like '%" + docName.ToLower() + "%'";
            }
            if (!abstractValue.Equals(""))
            {
                where += " and lower(Abstract) like '%" + abstractValue.ToLower() + "%'";
            }
            agent.query(where);
        }
        else
        {
            agent.query("1=2");
        }
        DataObjectSet dos = agent.defaultData;
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine2);
        string vieweDocUrl = sp.getParam("eKMViewDocUrl");
        engine2.close();

        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            DataObject dataObject = dos.getAvailableDataObject(i);
            string guid = dataObject.getData("GUID");
            string docNumber = dataObject.getData("DocNumber");
            string docName = dataObject.getData("Name");
            string url = vieweDocUrl + "?ParentPanelID=&DataListID=DocList&ParentPageUID=&UIType=General&UIStatus=8&ObjectGUID=" + guid + "&isMaintain=Y";
            string href = "javascript:openDocWindow('檢視文件','" + url + "', 100, 100, '', true, true);";
            string docNumberUrl = "{[a href=\"" + href + "\"]}" + docNumber + "{[/a]}";
            string docNameUrl = "{[a href=\"" + href + "\"]}" + docName + "{[/a]}";
            dataObject.setData("DocNumber", docNumberUrl);
        }
        DocList.setColumnStyle("DocNumber", 130, DSCWebControl.GridColumnStyle.LEFT);
        int pagesize = DocList.PageSize;
        DocList.Height = 30 * pagesize;
        DocList.dataSource = dos;

        DocList.showSettingPages = new Boolean[] { false, false, false, false, false, false, false, false, false, false };
        DocList.HiddenField = new string[] { "GUID", "Status", "ConfidentialLevel", "AuthorId", "AuthorOrgUnitId", "Site", "KeyWords", "ExpiryDate", "SheetNo", "External", "OriginatorOrgUnitId", "OriginatorOrgUnitName", "RevGUID", "FormGUID", "IndexCardGUID", "Released", "LatestFlag", "MajorTypeGUID", "SubTypeGUID", "DocTypeGUID", "DocPropertyGUID", "DocGUID", "AuthorGUID", "AuthorOrgUnitGUID", "D_INSERTUSER" };
        DocList.updateTable();
    }

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        queryData();
    }
}