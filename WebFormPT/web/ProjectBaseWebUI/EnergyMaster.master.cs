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
using WebServerProject.auth;
using BaseWebUI;
using com.dsc.kernal.agent;
using com.dsc.locale;


public partial class ProjectBaseWebUI_EnergyMaster : System.Web.UI.MasterPage
{
    public string maintainIdentity = "";
    /// <summary>
    /// 應用程式代號(稽核模組使用)
    /// </summary>
    public string ApplicationID = "";

    /// <summary>
    /// 模組代號(稽核模組使用)
    /// </summary>
    public string ModuleID = "";

    /// <summary>
    /// 預設系統錯誤層級
    /// </summary>
    public int errorLevel = 5;
    /// <summary>
    /// 單頭使用AgentSchema
    /// </summary>
    public string AgentSchema;
    /// <summary>
    /// 關聯資訊
    /// </summary>
    public string[,] RelationInfo;
    /// <summary>
    /// 隱藏欄位陣列
    /// </summary>
    public string[] HiddenField;
    /// <summary>
    /// 檢視欄位
    /// </summary>
    public string[] ReadOnlyField;
    /// <summary>
    /// 頁面Title
    /// </summary>
    public string title = "";
    /// <summary>
    /// 按下新增或編輯資料所要開啟的畫面
    /// </summary>
    public string inputForm = "";
    public delegate string BeforeSendButtonClickEvent(string whereClause);
    public event BeforeSendButtonClickEvent BeforeSendButtonClick;

    public delegate void SendButtonClickEvent(string whereClause);
    public event SendButtonClickEvent SendButtonClick;

    public delegate void AfterSendButtonClickEvent(string[,] orderby);
    public event AfterSendButtonClickEvent AfterSendButtonClick;

    public delegate bool BeforeDeleteDataEvent();
    public event BeforeDeleteDataEvent BeforeDeleteData;

    public delegate void initParameterEvent();
    public event initParameterEvent initParameter;

    public DataObjectSet basedos
    {
        get
        {
            return ListTable.dataSource;
        }
        set
        {
            ListTable.dataSource = value;
        }
    }

    public DSCWebControl.GlassButton getSendButton()
    {
        return SendButton;
    }
    public DSCWebControl.GlassButton getClearButton()
    {
        return ClearButton;
    }
    public DSCWebControl.QueryPage getQueryPage()
    {
        return QueryPages;
    }
    public DSCWebControl.OutDataList getListTable()
    {
        return ListTable;
    }

    public void InitData(com.dsc.kernal.databean.DataObject obj)
    {
        initParameter();
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);


        AUTHAgent authagent = new AUTHAgent();
        authagent.engine = engine;

        int auth = authagent.getAuth(maintainIdentity, (string)Session["UserID"], (string[])Session["usergroup"]);

        engine.close();

        if (auth == 0)
        {
            Response.Redirect("~/NoAuth.aspx");
        }
        SendButton.Enabled = true;
        ListTable.NoAdd = true;
        ListTable.NoDelete = true;

        if (authagent.parse(auth, AUTHAgent.ADD))
        {
            ListTable.NoAdd = false;
        }
        if (authagent.parse(auth, AUTHAgent.DELETE))
        {
            ListTable.NoDelete = false;
        }


        this.RelationInfo = RelationInfo;
        QueryPages.dataSource = obj;
        QueryPages.HiddenField = HiddenField;

        ListTable.HiddenField = HiddenField;
        ListTable.ReadOnlyField = ReadOnlyField;
    }

    public void SendButton_Click(object sender, EventArgs e)
    {
        initParameter();
        String[] cond = QueryPages.whereClause;
        String whereClause = "";
        String[,] orderby = QueryPages.orderBy;

        if (cond != null)
        {
            whereClause += "("; //2009/08/06 hjlin add for mantis 13337
            for (int i = 0; i < cond.Length; i++)
            {
                whereClause += cond[i];
            }
            whereClause += ")"; //2009/08/06 hjlin add for mantis 13337
        }
        try
        {
            if (BeforeSendButtonClick != null)
            {
                whereClause = BeforeSendButtonClick(whereClause);
            }
        }
        catch (Exception ze)
        {
        }

        try
        {
            SendButtonClick(whereClause);
            basedos.sort(orderby);
            try
            {
                if (AfterSendButtonClick != null)
                {
                    AfterSendButtonClick(orderby);
                }
            }
            catch (Exception ze)
            {
            }
            //設定DataList元件
            ListTable.connectDBString = (string)Session["connectString"];
            ListTable.clientEngineType = (string)Session["engineType"];
            ////Mantis-19199
            //if (!((string)Session["Locale"]).Equals("zh_TW"))
            //{
            //    for (int i = 0; i < basedos.getAvailableDataObjectCount(); i++)
            //    {
            //        basedos.getAvailableDataObject(i).loadLanguage();
            //    }
            //}
            ListTable.dataSource = basedos;
            ListTable.updateTable();
            DSCTabControl1.SelectedTab = 1;
        }
        catch (Exception es)
        {
            Page.Response.Write("alert('" + es.Message + "');");
            writeLog(es);
        }

    }
    protected void ClearButton_Click(object sender, EventArgs e)
    {
        initParameter();
        QueryPages.clearQuery();

    }
    private void writeLog(Exception e)
    {
        //這裡要整合稽核模組

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "select SMVAAAB003 from SMVAAAB where SMVAAAB002='" + Utility.filter(maintainIdentity) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        string ProgramName = "未知的程式名稱";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ProgramName = ds.Tables[0].Rows[0][0].ToString();
        }

        WebServerProject.Audit au = new WebServerProject.Audit(engine);
        bool res = au.writeLog(ApplicationID, ModuleID, maintainIdentity, ProgramName, errorLevel, e.Message, e.StackTrace, (string)Session["UserID"], (string)Session["UserName"], Request.ServerVariables["REMOTE_ADDR"], Request.ServerVariables["HTTP_USER_AGENT"]);

        engine.close();

    }
    protected void ListTable_ShowRowData(DataObject objects)
    {
        initParameter();
        GeneralWebPage gp = (GeneralWebPage)Page;
        string tmpurl = "";

        ////相對路徑
        string[] pAry = com.dsc.kernal.utility.Utility.getPathArray(Request.Path);
        string url = pAry[pAry.Length - 2] + "/" + inputForm + "?MaintainPageUniqueID="+gp.tb.Text+"&ObjectGUID=" + objects.getData("GUID").ToString() + "&PageState=isReadOnly&curPanelID=" + Request.QueryString["CurPanelID"] + "&DataListID=" + Request.Form["DSCWebControl"].ToString() + "&ParentPanelID=" + Request.QueryString["CurPanelID"];
        gp.showPanelWindow(this.title, url, 0, 0, "", true, true);
    }
    protected bool ListTable_SaveRowData(DataObject objects, bool isNew)
    {
        GeneralWebPage gp = (GeneralWebPage)Page;
        initParameter();

        string tmpurl = "";
        ////相對路徑
        string[] pAry = com.dsc.kernal.utility.Utility.getPathArray(Request.Path);
        string url = pAry[pAry.Length - 2] + "/" + this.inputForm + "?MaintainPageUniqueID=" + gp.tb.Text + "&DataListID=" + Request.Form["DSCWebControl"].ToString() + "&PageState=isAddNew&curPanelID=" + Request.QueryString["CurPanelID"] + "&ParentPanelID=" + Request.QueryString["CurPanelID"];
        gp.showPanelWindow(this.title, url, 0, 0, "", true, true);
        return false;
    }
    protected void ListTable_DeleteData()
    {
        initParameter();
        GeneralWebPage gp = (GeneralWebPage)Page;
        try
        {
            NLAgent agent = new NLAgent();
            agent.loadSchema(RelationInfo[0, 0].ToString());
            agent.engine = deleteEngine;
            agent.defaultData = this.basedos;

            if (agent.update())
            {
                //20100412 hjlin edit mantis 16793
                //Page.Response.Write("alert('刪除成功');");
                string deleteMsg = LocaleString.getKernalLocaleString("ProjectBaseWebUI.dll.language.ini", "EnergyMaster", "QueryError1", "刪除成功");
                Page.Response.Write("alert('" + deleteMsg + "');");
            }
            else
            {
                //20100412 hjlin edit mantis 16793
                //Page.Response.Write("alert('刪除失敗');");
                string deleteMsg = LocaleString.getKernalLocaleString("ProjectBaseWebUI.dll.language.ini", "EnergyMaster", "QueryError2", "刪除失敗");
                Page.Response.Write("alert('" + deleteMsg + "');");
            }
            deleteEngine.commit();
            deleteEngine.close();

        }
        catch (Exception te)
        {
            try
            {
                deleteEngine.rollback();
            }
            catch { };
            try
            {
                deleteEngine.close();
            }
            catch { };
            Page.Response.Write("alert('" + te.Message.Replace("\n", "\\n") + "');");
        }
    }
    AbstractEngine deleteEngine = null;

    protected bool ListTable_BeforeDeleteData()
    {
        initParameter();
        GeneralWebPage gp = (GeneralWebPage)Page;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string detailerr = "";
        IOFactory factory = new IOFactory();
        deleteEngine = factory.getEngine(engineType, connectString);
        deleteEngine.startTransaction(IsolationLevel.ReadCommitted);
        try
        {
            NLAgent detailagent = new NLAgent();
            DataObject[] doary = ListTable.getSelectedItem();
            for (int y = 0; y < doary.Length; y++)
            {
                for (int k = 1; k < RelationInfo.GetLength(0); k++)
                {
                    detailagent.loadSchema(RelationInfo[k, 0].ToString());
                    detailagent.engine = deleteEngine;
                    detailagent.query(RelationInfo[k, 2].ToString() + " = '" + doary[y].getData(RelationInfo[k, 1].ToString()) + "'");
                    for (int o = 0; o < detailagent.defaultData.getDataObjectCount(); o++)
                    {
                        detailagent.defaultData.getDataObject(o).delete();
                    }
                    if (!detailagent.update())
                    {
                        detailerr = deleteEngine.errorString;
                    }
                }

            }

            return true;
        }
        catch (Exception te)
        {
            try
            {
                deleteEngine.rollback();
            }
            catch { };
            try
            {
                deleteEngine.close();
            }
            catch { };
            Page.Response.Write("alert('" + te.Message.Replace("\n", "\\n") + "');");
            return false;
        }
    }
    public void RefreshOutDataList()
    {

        this.ListTable.generateRefreshScript(true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string js = "";
            js += "<script src=\"" + Page.ResolveClientUrl("~/JS/ShareScript.js") + "\" language=\"javascript\"></script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "shareScript", js, false);
        }
    }
}
