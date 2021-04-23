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

public partial class ProjectBaseWebUI_EnergyBaseMaster : System.Web.UI.MasterPage
{
    /// <summary>
    /// 程式代號-程式權限使用
    /// </summary>
    public string baseIdentity = "";

    protected override void OnInit(EventArgs e)
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        base.OnInit(e);
        gp.init();
    }
    protected override void OnLoad(EventArgs e)
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
       
        //
        if (!IsPostBack)
        {
            //新頁面讀取或是事件進入
            if (!gp.getDispatchEventState())
            {
                if (Request.Form["InitDetailPageUniqueID"] != null)
                {
                    EnergyBasePage gp2 = (EnergyBasePage)Page;
                    gp.tb.Text = Request.Form["currentPageUniqueID"];
                    Hashtable hs = new Hashtable();
                    string[] tags = Request.Form["DPID"].Split(new char[] { ';' });
                    for (int i = 0; i < tags.Length; i++)
                    {
                        hs[gp2.iframeInfo[i, 0]] = tags[i];
                    }
                    gp2.setSession("DetailPageUniqueID", hs);

                    Response.Clear();
                    Response.Write(Request.Form["DPID"]);
                    Response.End();
                }
                if (Request.Form["saveData"] != null)
                {
                    //gp.MessageBox("OnLoad_in");
                    gp.tb.Text = Request.Form["currentPageUniqueID"];
                    gp.initControl();
                    string guid = SaveButtonClick();
                    try
                    {
                        Response.Clear();
                        Response.Write(guid);
                        Response.End();
                    }
                    catch { }
                }
                if (Request.Form["rollback"] != null)
                {
                    gp.tb.Text = Request.Form["currentPageUniqueID"];
                    gp.initControl();
                    Response.Clear();
                    SaveButtonRollBack();
                    try
                    {
                        Response.End();
                    }
                    catch { }
                }
                if (Request.Form["commit"] != null)
                {
                    gp.tb.Text = Request.Form["currentPageUniqueID"];
                    gp.initControl();
                    string retValue = SaveButtonCommit();
                    //gp.MessageBox(retValue);
                    try
                    {
                        Response.Clear();
                        if (retValue.Equals(""))
                        {
                            gp.refreshAllClientElement();
                        }
                        else
                        {
                            Response.Write(retValue);
                        }
                        Response.End();
                    }
                    catch { }
                }

                EditButton.BackImageUrl = Page.ResolveUrl("~/Images/GeneralWebFormButtonBack.gif");
                SaveButton.BackImageUrl = Page.ResolveUrl("~/Images/GeneralWebFormButtonBack.gif");
                AddButton.BackImageUrl = Page.ResolveUrl("~/Images/GeneralWebFormButtonBack.gif");
                CancelButton.BackImageUrl = Page.ResolveUrl("~/Images/GeneralWebFormButtonBack.gif");
                BackListButton.BackImageUrl = Page.ResolveUrl("~/Images/GeneralWebFormButtonBack.gif");

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = null;

                engine = factory.getEngine(engineType, connectString);


                string ret = "";

                //儲存參數
                saveParameter();

                //初始化UI元件預設內容
                gp.initUIControl(engine);

                //處理PageState預設值(isReadOnly)
                if (Request.QueryString["PageState"] == null)
                {
                    gp.setSession("PageState", EnergyBasePage.isReadOnly);
                }

                //頁面公用script
                registerScript();
                //將畫面初始化按鈕以及下方的Controls
                setInitPage();

                com.dsc.kernal.databean.DataObject objects = null;



                try
                {
                    if (gp.iframeInfo == null)
                    {
                        gp.MessageBox("未設定iframeInfo資訊");
                        throw new Exception("checkiframeInfo err");
                    }

                    ret = checkiframeInfo(gp.iframeInfo);
                    if (!string.IsNullOrEmpty(ret))
                    {
                        throw new Exception("checkiframeInfo檢核錯誤" + ret);
                    }

                    if (((string)gp.getSession("PageState")).Equals(EnergyBasePage.isReadOnly))
                    {
                        initFrameInfo(gp.iframeInfo, "isReadOnly");
                    }
                    else
                    {
                        initFrameInfo(gp.iframeInfo, "isEdit");
                    }
                    if (string.IsNullOrEmpty((string)Request.QueryString["ObjectGUID"]))
                    {
                        objects = gp.readDB(engine, null);
                    }
                    else
                    {
                        objects = gp.readDB(engine, (string)Request.QueryString["ObjectGUID"]);
                    }

                    if (objects != null)
                    {
                        gp.setSession("objects", objects);
                    }


                    if (!((string)gp.getSession("PageState")).Equals(EnergyBasePage.isAddNew))
                    {
                        gp.showData(engine, objects);
                    }


                }
                catch (Exception te)
                {
                    gp.MessageBox(te.Message);
                    gp.writeLog(te);
                }


            }
        }

        base.OnLoad(e);

    }
    #region 設定頁面按鈕/狀態
    private void setReadOnlyForm()
    {
        DSCWebControl.DSCWebControlBase ele = null;
        for (int i = 0; i < this.Controls.Count; i++)
        {
            try
            {
                ele = (DSCWebControl.DSCWebControlBase)this.Controls[i];
                ele.Enabled = false;
                ele.ReadOnly = true;
            }
            catch { };
            setReadOnlyFormRec(this.Controls[i]);
        }
    }
    private void setReadOnlyFormRec(System.Web.UI.Control c)
    {
        DSCWebControl.DSCWebControlBase ele = null;
        for (int i = 0; i < c.Controls.Count; i++)
        {
            try
            {
                ele = (DSCWebControl.DSCWebControlBase)c.Controls[i];
                ele.Enabled = false;
                ele.ReadOnly = true;
            }
            catch { };
            setReadOnlyFormRec(c.Controls[i]);
        }

    }
    private void setInitPage()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        baseIdentity = gp.baseIdentity;

        string state = (string)gp.getSession("PageState");
        if (state.Equals(EnergyBasePage.isReadOnly))
        {
            DSCWebControl.DSCWebControlBase ele = null;
            for (int i = 0; i < this.Controls.Count; i++)
            {
                setReadOnlyFormRec(this.Controls[i]);
            }
            SaveButton.Enabled = false;
            CancelButton.Enabled = false;
            EditButton.Enabled = true;
            AddButton.Enabled = true;
            BackListButton.Enabled = true;
        }
        else if(state.Equals(EnergyBasePage.isEdit))
        {
            setEditPage();
        }
        else
        {
            setAddPage();
        }
        AUTHAgent authagent = new AUTHAgent();
        authagent.engine = engine;

        int auth = authagent.getAuth(baseIdentity, (string)Session["UserID"], (string[])Session["usergroup"]);

        engine.close();

        if (auth == 0)
        {
            Response.Redirect("~/NoAuth.aspx");
        }

        if (!authagent.parse(auth, AUTHAgent.ADD))
        {
            AddButton.Enabled = false;
            SaveButton.Enabled = false;
        }
        if (!authagent.parse(auth, AUTHAgent.MODIFY))
        {
            SaveButton.Enabled = false;
        }
    }
    private void setAddPage()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        try
        {
            gp.addData();
            SaveButton.Enabled = true;
            CancelButton.Enabled = true;
            EditButton.Enabled = false;
            AddButton.Enabled = false;
            BackListButton.Enabled = true;
        }
        catch (Exception xe)
        {
            Page.Response.Write("alert('" + xe.Message + "');");
            gp.writeLog(xe);
        }

    }
    private void setEditPage()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        try
        {
            gp.editData();
            SaveButton.Enabled = true;
            CancelButton.Enabled = true;
            EditButton.Enabled = false;
            AddButton.Enabled = false;
            BackListButton.Enabled = true;
        }
        catch (Exception xe)
        {
            Page.Response.Write("alert('" + xe.Message + "');");
            gp.writeLog(xe);
        }
    }
    private void setReadOnlyPage()
    {
        DSCWebControl.DSCWebControlBase ele = null;
        for (int i = 0; i < this.Controls.Count; i++)
        {
            try
            {
                ele = (DSCWebControl.DSCWebControlBase)this.Controls[i];
                ele.Enabled = false;
            }
            catch { };
            setReadOnlyFormRec(this.Controls[i]);
        }

    }
    private void setReadOnlyPageRec(System.Web.UI.Control c)
    {
        DSCWebControl.DSCWebControlBase ele = null;
        for (int i = 0; i < c.Controls.Count; i++)
        {
            try
            {
                ele = (DSCWebControl.DSCWebControlBase)c.Controls[i];
                ele.Enabled = false;
            }
            catch { };
            setReadOnlyFormRec(c.Controls[i]);
        }

    }
    #endregion

    public void EditButton_Click(object sender, EventArgs e)
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        try
        {
            gp.editData();
            setpageState(EnergyBasePage.isEdit);
            setDetailStatus("isEdit");

        }
        catch (Exception xe)
        {
            Page.Response.Write("alert('" + xe.Message + "');");
            gp.writeLog(xe);
        }
    }
    public void SaveButton_Click(object sender, EventArgs e)
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        setReadOnlyPage();
        gp.afterSaveData();
        setpageState(EnergyBasePage.isReadOnly);
        setDetailStatus("isReadOnly");
    }
    public void AddButton_Click(object sender, EventArgs e)
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        gp.addData();

        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            com.dsc.kernal.databean.DataObject objects = gp.readDB(engine, null);
            engine.close();
            gp.setSession("objects", objects);
        }
        catch
        {
            try
            {
                engine.close();
            }
            catch { };
        }
        gp.setSession("ObjectGUID", "");
        setpageState(EnergyBasePage.isAddNew);
        setDetailStatus("isEdit");
        setDetailPageNew();
    }
    public void CancelButton_Click(object sender, EventArgs e)
    {
        
        EnergyBasePage gp = (EnergyBasePage)Page;
        string state = gp.getPageState();
        try
        {
            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;

            try
            {
                gp.cancelData();

                if (state.Equals(EnergyBasePage.isEdit))
                {
                    string connectString = (string)Session["connectString"];
                    string engineType = (string)Session["engineType"];

                    engine = factory.getEngine(engineType, connectString);
                    gp.showData(engine, (com.dsc.kernal.databean.DataObject)gp.getSession("objects"));
                    engine.close();
                    
                    setReadOnlyPage();
                    gp.cancelData();
                    setpageState(EnergyBasePage.isReadOnly);
                    setDetailStatus("isReadOnly");
                }
                else
                {
                    setpageState(EnergyBasePage.isAddNew);
                    gp.addData();
                    setDetailStatus("isEdit");
                }
                setDetailPageCancel();
            }
            catch (Exception xe)
            {
                try
                {
                    engine.close();
                }
                catch { };
                Page.Response.Write("alert('" + xe.Message + "');");
                gp.writeLog(xe);
            }
        }
        catch (Exception xe)
        {
            Page.Response.Write("alert('" + xe.Message + "');");
            gp.writeLog(xe);
        }
    }
    public void BackListButton_Click(object sender, EventArgs e)
    {
        closeRefresh();
    }

    /// <summary>
    /// 設定編輯鈕屬性
    /// </summary>
    /// <returns>true/false</returns>
    public DSCWebControl.GlassButton setEditButton()
    {
        return EditButton;
    }
    /// <summary>
    /// 設定儲存鈕屬性
    /// </summary>
    /// <returns>true/false</returns>
    public DSCWebControl.GlassButton setSaveButton()
    {
        return SaveButton;
    }
    /// <summary>
    /// 設定新增鈕屬性
    /// </summary>
    /// <returns>true/false</returns>
    public DSCWebControl.GlassButton setAddButton()
    {
        return AddButton;
    }
    /// <summary>
    /// 設定取消鈕屬性
    /// </summary>
    /// <returns>true/false</returns>
    public DSCWebControl.GlassButton setCancelButton()
    {
        return CancelButton;
    }
    /// <summary>
    /// 設定回清單鈕屬性
    /// </summary>
    /// <returns></returns>
    public DSCWebControl.GlassButton setBackListButton()
    {
        return BackListButton;
    }

    public string SaveButtonClick()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            engine.startTransaction(IsolationLevel.ReadCommitted);

            gp.setSession("ENGINE", engine);

            gp.errMsg = new ArrayList();
            if (gp.checkFieldData(engine, (com.dsc.kernal.databean.DataObject)gp.getSession("objects")))
            {
                if (gp.beforeSaveData(engine, (com.dsc.kernal.databean.DataObject)gp.getSession("objects")))
                {
                    gp.saveData(engine, (com.dsc.kernal.databean.DataObject)gp.getSession("objects"));
                    if (gp.afterSaveData(engine, (com.dsc.kernal.databean.DataObject)gp.getSession("objects")))
                    {
                        return ((com.dsc.kernal.databean.DataObject)gp.getSession("objects")).getData("GUID");
                    }
                    else
                    {
                        return "ERROR" + gp.showErrorMessage();
                    }
                }
                else
                {
                    return "ERROR" + gp.showErrorMessage();
                }
            }
            else
            {
                return "ERROR" + gp.showErrorMessage();
            }
        }
        catch (Exception xe)
        {
            try
            {
                engine.rollback();
            }
            catch { };
            try
            {
                engine.close();
            }
            catch { };
            gp.writeLog(xe);
            return "ERROR" + xe.Message;
        }

    }
    public void SaveButtonRollBack()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        AbstractEngine engine = null;
        try
        {
            engine = (AbstractEngine)gp.getSession("ENGINE");
            engine.rollback();
            engine.close();
        }
        catch (Exception xe)
        {
            gp.writeLog(xe);
        }
    }
    public string SaveButtonCommit()
    {

        EnergyBasePage gp = (EnergyBasePage)Page;
        AbstractEngine engine = null;

        try
        {
            if (SaveButton.Enabled)
            {
                engine = (AbstractEngine)gp.getSession("ENGINE");
                string state = (string)gp.getSession("PageState");

                gp.saveDB(engine, (com.dsc.kernal.databean.DataObject)gp.getSession("objects"), null, "");

                if (state.Equals(EnergyBasePage.isAddNew))
                {
                    string maintainPageUniqueID = (string)gp.getSession("MaintainPageUniqueID");
                    string dataListID = (string)gp.getSession("DataListID");
                    DataObjectSet oriDOS = (DataObjectSet)Session[maintainPageUniqueID + "_" + dataListID + "_dataSource"];
                    oriDOS.addDraft((com.dsc.kernal.databean.DataObject)gp.getSession("objects"));
                }
                else
                {
                    string maintainPageUniqueID = (string)gp.getSession("MaintainPageUniqueID");
                    string dataListID = (string)gp.getSession("DataListID");
                    DataObjectSet oriDOS = (DataObjectSet)Session[maintainPageUniqueID + "_" + dataListID + "_dataSource"];
                    DataObject curObj = (com.dsc.kernal.databean.DataObject)gp.getSession("objects");

                    //for (int i = 0; i < oriDOS.getAvailableDataObjectCount(); i++)
                    //{
                    //    if (curObj.getData("GUID").Equals(oriDOS.getAvailableDataObject(i).getData("GUID")))
                    //    {
                    //        oriDOS.getAvailableDataObject(i).delete();
                    //    }
                    //}
                    //oriDOS.compact();
                    //oriDOS.addDraft((com.dsc.kernal.databean.DataObject)gp.getSession("objects"));

                }

                engine.commit();
                engine.close();


                return "alert('儲存成功');";
            }
            else
            {
                //isReadOnly
                return "";
            }
        }
        catch (Exception xe)
        {
            try
            {
                engine.rollback();
            }
            catch { };
            try
            {
                engine.close();
            }
            catch { };
            gp.writeLog(xe);
            string errorString = xe.Message;
            //前端使用eval, 須注意錯誤訊息含換行符之問題
            errorString = errorString.Replace("\n", "\\n");
            return "alert('" + errorString + "');";            
        }
    }

    private void setDetailPageNew()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        string urls = "?ObjectGUID=&PageStatus=isEdit";
        for (int i = 0; i < gp.iframeInfo.GetLength(0); i++)
        {
            gp.executeScript("document.getElementById('" + gp.iframeInfo[i, 0] + "').src='" + gp.iframeInfo[i, 1] + urls + "&FK=" + gp.iframeInfo[i, 2] + "';");
        }
    }
    private void setDetailPageCancel()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        string urls = "?ObjectGUID=" + ((DataObject)gp.getSession("objects")).getData("GUID").ToString() + "&PageStatus=isReadOnly";
        for (int i = 0; i < gp.iframeInfo.GetLength(0); i++)
        {
            gp.executeScript("document.getElementById('" + gp.iframeInfo[i, 0] + "').src='" + gp.iframeInfo[i, 1] + urls + "&FK=" + gp.iframeInfo[i, 2] + "';");
        }
    }
    private string saveDetailPage(string guid)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        engine.close();

        string siteName = sp.getParam("SiteName");
        
        EnergyBasePage gp = (EnergyBasePage)Page;
        for (int i = 0; i < gp.iframeInfo.GetLength(0); i++)
        {
            string url = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "httpHead") + "://" + siteName + Page.ResolveUrl(gp.iframeInfo[i, 1]);
            //return url;
            string detailPageUniqueID = ((Hashtable)gp.getSession("DetailPageUniqueID"))[gp.iframeInfo[i, 0]].ToString();

            string rets = com.dsc.kernal.utility.HTTPProcessor.sendGet(url, "DetailPageUniqueID="+detailPageUniqueID+"&PageUniqueID=" + gp.getPageUniqueID() + "&FK=" + gp.iframeInfo[i, 2] + "&GUID=" + guid + "&EFLogonID=" + (string)Session["UserID"] + "&SavePage=1");
            if (!rets.Equals(""))
            {
                return rets;
            }
        }
        return "";
    }
    private void setDetailStatus(string status)
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        for (int i = 0; i < gp.iframeInfo.GetLength(0); i++)
        {
            //排除與單頭沒關係的單身
            if (gp.iframeInfo[i, 2] != "")
            {
                gp.executeScript("window." + gp.iframeInfo[i, 0] + ".setPageStatus('" + status + "');");
            }
        }
    }
    /// <summary>
    /// 目前為關閉視窗
    /// </summary>
    protected void closeRefresh()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        try
        {
            gp.executeScript("closeRefreshSilence();");
        }
        catch { }
    }


    public void setpageState(string newpageState)
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        string state = (string)gp.getSession("PageState");
        if (newpageState.Equals(EnergyBasePage.isReadOnly))
        {
            if (state.Equals(EnergyBasePage.isAddNew))
            {
                //從isAddNew → isReadOnly

            }
            else
            {
                //從isEdit → isReadOnly

            }
            SaveButton.Enabled = false;
            CancelButton.Enabled = false;
            EditButton.Enabled = true;
            AddButton.Enabled = true;
            BackListButton.Enabled = true;
        }
        else if (newpageState.Equals(EnergyBasePage.isEdit))
        {
            if (state.Equals(EnergyBasePage.isReadOnly))
            {

            }

            SaveButton.Enabled = true;
            CancelButton.Enabled = true;
            EditButton.Enabled = false;
            AddButton.Enabled = false;
            BackListButton.Enabled = true;

        }
        else
        {
            //isAddNew
            if (state.Equals(EnergyBasePage.isReadOnly))
            {
                //新增
            }
            else
            {
                //取銷
            }

            SaveButton.Enabled = true;
            CancelButton.Enabled = true;
            EditButton.Enabled = false;
            AddButton.Enabled = false;
            BackListButton.Enabled = true;

        }
        gp.setSession("PageState", newpageState);

    }

    /// <summary>
    /// 儲存畫面傳入參數
    /// </summary>
    private void saveParameter()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        string tmpParentPanelID = fixNull(Request.QueryString["ParentPanelID"]);
        string tmpDataListID = fixNull(Request.QueryString["DataListID"]);
        string tmpObjectGUID = fixNull(Request.QueryString["ObjectGUID"]);
        string tmpPageState = fixNull(Request.QueryString["PageState"]);
        string tmpCurPanelID = fixNull(Request.QueryString["CurPanelID"]);
        string tmpMaintainPageUniqueID = fixNull(Request.QueryString["MaintainPageUniqueID"]);

        gp.setSession("ParentPanelID", com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(tmpParentPanelID));
        gp.setSession("DataListID", com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(tmpDataListID));
        gp.setSession("ObjectGUID", com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(tmpObjectGUID));
        gp.setSession("PageState", com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(tmpPageState));
        gp.setSession("CurPanelID", com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(tmpCurPanelID));
        gp.setSession("MaintainPageUniqueID", com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(tmpMaintainPageUniqueID));

    }

    public string getRefreshCommand()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        string tmpDataListID = gp.getSession("DataListID").ToString();
        string str = "";
        str += "OutDataList_ReadServer('" + tmpDataListID + "');";
        return str;
    }
    public string generateRefreshScript(bool isRefresh)
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        string tmpDataListID = gp.getSession("DataListID").ToString();


        string str = "";


        com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
        switch (resultType)
        {
            default:
                #region IE
                str += "<script language=javascript>";
                str += "function OutDataList_Refresh_" + tmpDataListID + "()";
                str += "{";
                str += getRefreshCommand();
                str += "}";
                str += "window.attachEvent('onload',OutDataList_Refresh_" + tmpDataListID + ");";
                str += "</script>";
                #endregion
                break;
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.FireFox:
                #region FireFox
                str += "<script language=javascript>";
                str += "function OutDataList_Refresh_" + tmpDataListID + "()";
                str += "{";
                str += getRefreshCommand();
                str += "}";
                str += "window.addEventListener('onload',OutDataList_Refresh_" + tmpDataListID + ",false);";
                str += "</script>";
                #endregion
                break;
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.Chrome:
                #region Safari
                str += "<script language=javascript>";
                str += "function OutDataList_Refresh_" + tmpDataListID + "()";
                str += "{";
                str += getRefreshCommand();
                str += "}";
                str += "window.addEventListener('onload',OutDataList_Refresh_" + tmpDataListID + ",false);";
                str += "</script>";
                #endregion
                break;
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.Safari:
                #region Chrome
                str += "<script language=javascript>";
                str += "function OutDataList_Refresh_" + tmpDataListID + "()";
                str += "{";
                str += getRefreshCommand();
                str += "}";
                str += "window.addEventListener('onload',OutDataList_Refresh_" + tmpDataListID + ",false);";
                str += "</script>";
                #endregion
                break;
        }
        return str;

    }

    /// <summary>
    /// 註冊頁面公用Client Script
    /// </summary>
    private void registerScript()
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        string tmpcur = gp.getSession("CurPanelID").ToString();
        string tmpDataListID = gp.getSession("DataListID").ToString();
        string tmpParentPanelID = gp.getSession("ParentPanelID").ToString();
        if (tmpcur.Length > 1)
        {
            tmpcur = tmpcur.Substring(tmpcur.Length-1, 1);
        }
        if (tmpParentPanelID.Length > 1)
        {
            tmpParentPanelID = tmpParentPanelID.Substring(tmpParentPanelID.Length - 1, 1);
        }

        
        
        string str = "";
        str += "<script language=javascript>";

        str += "function closeRefreshSilence(){";
        str += "try{";
        //str += "window.parent.setZIndex('" + (string)getSession("ParentPanelID") + "');"; //這行可以把指定的panelID帶到最前端
        str += "wobj=window.parent.getPanelWindowObject('" + tmpParentPanelID + "');"; //這行可以取得該panelID的PanelWindow中代表內容的window HTML物件
        str += "wobj.RefreshOutDataList();";
        str += "window.parent.Panel_Close_Silence('" + tmpcur + "');"; //這行可以直接關閉目前視窗
        //str += "}catch(e){alert('closeRefreshSilence錯誤'+e.name+':'+e.message+':'+e.stack)};";
        str += "}catch(e){};";
        str += "}";
        
        //檢查Iframe是否都已loading完畢的script
        str += "document.onmousedown=checkIframe;";
        str += "function checkIframe(){";
        for (int i = 0; i < gp.iframeInfo.Length / 3; i++)
        {
            str += "if(document.getElementById('" + gp.iframeInfo[i,0] + "').readyState!='complete'){";
            str += "     alert('尚有單身尚未載入完畢，請稍後再點選。');";
            str += "     return;}";
        }
        str += "}";
        str += "</script>";
        
        str += "<script src=\"" + Page.ResolveClientUrl("~/JS/ShareScript.js") + "\" language=\"javascript\"></script>";
        
        Type ctype = this.GetType();
        ClientScriptManager cm = Page.ClientScript;

        if (!cm.IsStartupScriptRegistered(ctype, "GeneralWebFormScript"))
        {
            cm.RegisterStartupScript(ctype, "GeneralWebFormScript", str);
        }


    }

    /// <summary>
    /// 檢核iframeInfo
    /// </summary>
    /// <param name="iframeInfo">iframeInfo</param>
    /// <returns>若無錯誤應回傳空字串</returns>
    private string checkiframeInfo(string[,] iframeInfo)
    {
        string ret = "";
        try
        {
            //執行檢核iframeInfo動作
            if (iframeInfo.Length < 1)
            {

            }

            //檢核是否成對

            //檢核其他

            return ret;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
    private void initFrameInfo(string[,] iframeInfo, string pageStatus)
    {
        EnergyBasePage gp = (EnergyBasePage)Page;
        string urls = "?ObjectGUID=" + (string)gp.getSession("ObjectGUID") + "&PageStatus=" + pageStatus + "&CurrentPageUniqueID=" + gp.tb.Text;
        for (int i = 0; i < iframeInfo.GetLength(0); i++)
        {
            gp.executeScript("document.getElementById('" + iframeInfo[i, 0] + "').src='" + iframeInfo[i, 1] + urls + "&FK=" + iframeInfo[i, 2] + "&IFRAMEID=" + iframeInfo[i, 0] + "';");
        }
    }

    /// <summary>
    /// 將輸入字串為null的調整成為零長度字串
    /// </summary>
    /// <param name="ori">輸入字串</param>
    /// <returns>調整後字串</returns>
    protected string fixNull(string ori)
    {
        if (ori == null)
        {
            return "";
        }
        else
        {
            return ori;
        }
    }
    /// <summary>
    /// 將NULL或者零長度字串調整成為&nbsp;
    /// </summary>
    /// <param name="ori">原始字串</param>
    /// <returns>調整後字串</returns>
    protected string fixNbsp(string ori)
    {
        ori = fixNull(ori);
        if (ori.Equals(""))
        {
            return "&nbsp;";
        }
        else
        {
            return ori;
        }
    }

}
