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

public partial class ProjectBaseWebUI_ListMaintainWithAddNew : System.Web.UI.MasterPage
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

    public string[] HiddenField;
    public string[] ReadOnlyField;
    public string inputForm="";
    public int DialogHeight = 350;
    public int DialogWidth = 650;

    public delegate string BeforeSendButtonClickEvent(string whereClause);
    public event BeforeSendButtonClickEvent BeforeSendButtonClick;

    public delegate void SendButtonClickEvent(string whereClause);
    public event SendButtonClickEvent SendButtonClick;

    public delegate void AfterSendButtonClickEvent(string[,] orderby);
    public event AfterSendButtonClickEvent AfterSendButtonClick;

    public delegate void DeleteButtonClickEvent();
    public event DeleteButtonClickEvent DeleteButtonClick;

    //start 2011/11/21 hjlin add 
    public delegate void AfterAddNewButtonClickEvent(DataObject obj);
    public event AfterAddNewButtonClickEvent AfterAddNewButtonClick;
    //end 2011/11/21 hjlin add 

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
    public DSCWebControl.GlassButton getAddButton()
    {
        return AddNewButton;
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
    public DSCWebControl.DataList getListTable()
    {
        return ListTable;
    }
    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Form["refreshList"] != null)
            {
                DataObject ddo = (DataObject)Page.Session[Request.Form["ObjectID"]];
                ListTable.PageUniqueID = Request.Form["currentPageUniqueID"];
                ListTable.dataSource.add(ddo);
                ListTable.updateTable();
                //start 2011/11/21 hjlin add 
                if (AfterAddNewButtonClick != null)
                {
                    AfterAddNewButtonClick(ddo);
                }
                //end 2011/11/21 hjlin add 

                try
                {
                    Response.Clear();
                    Response.Write("");
                    Response.End();
                }
                catch { }

            }
        }
    }
    public void InitData(com.dsc.kernal.databean.DataObject obj)
    {
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
        if (authagent.parse(auth, AUTHAgent.MODIFY))
        {
            ListTable.NoModify = false;
        }
        if (authagent.parse(auth, AUTHAgent.DELETE))
        {
            ListTable.NoDelete = false;
        }
        

        QueryPages.dataSource = obj;
        QueryPages.HiddenField = HiddenField;

        ListTable.InputForm = inputForm;
        ListTable.HiddenField = HiddenField;
        ListTable.ReadOnlyField = ReadOnlyField;
        ListTable.DialogHeight = DialogHeight;
        ListTable.DialogWidth = DialogWidth;
        /*
        try
        {
            SendButtonClick("(1=2)");

            //設定DataList元件
            ListTable.connectDBString = (string)Session["connectString"];
            ListTable.clientEngineType = (string)Session["engineType"];
            ListTable.dataSource = basedos;
            ListTable.updateTable();
            DSCTabControl1.SelectedTab = 1;
        }
        catch (Exception es)
        {
            Page.Response.Write("alert('" + es.Message + "');");
            writeLog(es);
        }
        */
    }

    //2009/08/10 hjlin add for mantis 13337
    //將 method protected 修改為 public
    public void SendButton_Click(object sender, EventArgs e)
    {
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
        //if (whereClause.Equals(""))
        //{
        //   whereClause = " 1 = 2 ";
        //}
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

        //g_whereClause = whereClause;
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
            ListTable.dataSource = basedos;
            ListTable.updateTable();
            DSCTabControl1.SelectedTab = 1;

        }
        catch (Exception es)
        {
            Page.Response.Write("alert('"+es.Message+"');");
            writeLog(es);
        }

    }
    protected void ClearButton_Click(object sender, EventArgs e)
    {
        QueryPages.clearQuery();
    }
    protected void AddNewButton_Click(object sender, EventArgs e)
    {
        SendButtonClick("1=2");
        ListTable.connectDBString = (string)Session["connectString"];
        ListTable.clientEngineType = (string)Session["engineType"];
        ListTable.dataSource = basedos;
        ListTable.updateTable();
        DSCTabControl1.SelectedTab = 1;
        BaseWebUI.WebFormBasePage gp = (BaseWebUI.WebFormBasePage)Page;
        string ObjectID = com.dsc.kernal.utility.IDProcessor.getID("");
        Page.Session[ObjectID] = ListTable.dataSource.create();

        ////相對路徑
        string[] pAry = com.dsc.kernal.utility.Utility.getPathArray(Request.Path);
        string url = pAry[pAry.Length - 2] + "/" + ListTable.InputForm + "?ObjectID=123&isNew=true";

        //gp.showModalDialog(ListTable.InputForm, "ObjectID=" + ObjectID + "&isNew=true&NoAdd=0&NoModify=0&NoDelete=0", ListTable.DialogHeight.ToString()+"px", ListTable.DialogWidth.ToString()+"px", "", "", "", "", "", "", "", "", "");
        Page.Response.Write("showDialog('" + ListTable.InputForm + "?ObjectID=" + ObjectID + "&isNew=true&NoAdd=0&NoModify=0&NoDelete=0','','" + ListTable.DialogHeight.ToString() + "px','" + ListTable.DialogWidth.ToString() + "px','"+ObjectID+"');");
    }
    protected void ListTable_DeleteData()
    {
        if (DeleteButtonClick != null)
        {
            try
            {
                DeleteButtonClick();
                //20100412 hjlin edit mantis 16793
                //Response.Write("alert('刪除成功!');");
                string DeleteSuccessMsg = com.dsc.locale.LocaleString.getKernalLocaleString("ProjectBaseWebUI.dll.language.ini", "ListMaintainWithAddNew", "QueryError1", "刪除成功");
                Response.Write("alert('" + DeleteSuccessMsg + "');");
            }
            catch (Exception ue)
            {
                Response.Write("alert('" + ue.Message.Replace("\n", "\\n") + "');");
                writeLog(ue);
            }
        }
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
