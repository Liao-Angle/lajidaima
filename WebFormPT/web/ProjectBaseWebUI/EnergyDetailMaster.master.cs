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
using com.dsc.kernal.factory;
using com.dsc.kernal.agent;
using com.dsc.kernal.utility;

public partial class ProjectBaseWebUI_EnergyDetailMaster : System.Web.UI.MasterPage
{
    //Session:
    //currentDOS: 真正異動的資料：包含新增、修改、刪除
    //displayDOS: 此次查詢顯示的資料

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        gp.init();
    }
    protected override void OnLoad(EventArgs e)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        base.OnLoad(e);
        
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine;
        engine = factory.getEngine(engineType, connectString);


        if (!IsPostBack)
        {
            //新頁面讀取或是事件進入
            if (!gp.getDispatchEventState())
            {
                string js = "";
                js += "<script src=\"" + Page.ResolveClientUrl("~/JS/ShareScript.js") + "\" language=\"javascript\"></script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "shareScript", js, false);
                if (Request.Form["SavePage"] != null)
                {
                    try
                    {
                        string pid = Request.Form["CurrentPageUniqueID"];
                        AbstractEngine engineWithTrans = (AbstractEngine)Session[pid + "_ENGINE"];
                        gp.tb.Text = Request.Form["DetailPageUniqueID"];
                        string FK = Request.Form["FK"];
                        string GUID = Request.Form["GUID"];
                        string errStr = gp.saveDB(engineWithTrans, FK, GUID);
                        try
                        {
                            Response.Clear();
                            Response.Write(errStr);
                            //國昌20100622-mantis0017622
                            gp.setSession("HeadGUID", GUID);
                            Response.End();
                        }
                        catch { }
                    }
                    catch (System.Threading.ThreadAbortException ue)
                    {
                    }
                    catch (Exception te)
                    {
                        Response.Clear();
                        Response.Write(te.Message);
                        Response.End();
                    }
                }

                try
                {
                    gp.setSession(Request.QueryString["CurrentPageUniqueID"], Request.QueryString["IFRAMEID"], gp.tb.Text);
                    gp.setSession("HeadGUID", Request.QueryString["ObjectGUID"]);
                    gp.setSession("FK", Request.QueryString["FK"]);
                    gp.setSession("PageState", Request.QueryString["PageStatus"]);
                    gp.setSession("ParentPageUniqueID", Request.QueryString["CurrentPageUniqueID"]);

                    if (Request.QueryString["PageStatus"].Equals("isReadOnly"))
                    {
                        setReadOnlyButton();
                    }
                    else
                    {
                        setInitButton();
                       
                    }
                    doQuery("1=2");
                }
                catch (Exception te)
                {
                    gp.MessageBox(te.Message);
                    gp.writeLog(te);
                }
            }
        }
        gp.initUIControl(engine);
        engine.close();
    }
    public void doQuery(string sql)
    {

        EnergyDetailPage gp = (EnergyDetailPage)Page;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        AbstractEngine engine = null;

        com.dsc.kernal.databean.DataObjectSet currentDOS = (com.dsc.kernal.databean.DataObjectSet)gp.getSession("currentDOS");
        try
        {
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            if (currentDOS == null)
            {
                currentDOS = queryDB(engine, "1=2");
                gp.setSession("currentDOS", currentDOS);
            }

            com.dsc.kernal.databean.DataObjectSet displayDOS = queryDB(engine, sql);
            com.dsc.kernal.databean.DataObjectSet compareDOS = currentDOS.getFilteredDataObjectSet(sql);

            for (int i = 0; i < compareDOS.getAvailableDataObjectCount(); i++)
            {
                com.dsc.kernal.databean.DataObject cobj = compareDOS.getAvailableDataObject(i);
                com.dsc.kernal.databean.DataObject tempobj = null;
                bool isExist = false;
                for (int j = 0; j < displayDOS.getAvailableDataObjectCount(); j++)
                {
                    tempobj = displayDOS.getAvailableDataObject(j);
                    if (tempobj.getData("GUID").Equals(cobj.getData("GUID")))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    tempobj.delete();
                    displayDOS.addDraft(cobj);
                }
                else
                {
                    displayDOS.addDraft(cobj);
                }
            }
            displayDOS.compact();
            gp.setSession("displayDOS", displayDOS);

            engine.close();
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
        }
        #region 準備砍掉
        /*
        if (currentDOS == null)
        {
            try
            {
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);


                currentDOS = queryDB(engine, gp.queryString());

                gp.setSession("currentDOS", currentDOS);
                com.dsc.kernal.databean.DataObjectSet displayDOS = queryDB(engine, "1=2");
               

                engine.close();

                for (int i = 0; i < currentDOS.getAvailableDataObjectCount(); i++)
                {
                    displayDOS.addDraft(currentDOS.getAvailableDataObject(i));
                }
                gp.setSession("displayDOS", displayDOS);
                CancelButton.ReadOnly = false;
                
            }
            catch (Exception te)
            {
                try
                {
                    engine.close();
                    
                }
                catch { };
                Page.Response.Write("alert('" + te.Message + "');");
                gp.writeLog(te);
                gp.setSession("currentDOS", null);
                gp.setSession("displayDOS", null);
            }
            
        }
        else
        {
            try
            {
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                //Edward:這裡要從currentDOS根據搜尋條件取得displayDOS
                string sql = gp.queryString();

                //step1: 直接由DB取得displayDOS
                com.dsc.kernal.databean.DataObjectSet displayDOS = queryDB(engine, sql);

                engine.close();

                if (displayDOS.getAvailableDataObjectCount() > 0)
                {
                    //step2: 
                    //step2-1:若displayDOS中GUID存在於currentDOS, 
                    //  displayDOS remove此DataObject, 則compareDOS=currentDOS.getFilteredObject()
                    //  step2-1-1: 若displayDOS中GUID不存在於compareDOS, 沒事
                    //  step2-1-2: 若displayDOS中GUID存在於compareDOS, 將currentDOS符合的該筆DataObject的link加入displayDOS
                    //step2-2:若displayDOS中GUID不存在於currentDOS, 則將displayDOS的DataObject的link 加到currentDOS中
                    ArrayList tempList = new ArrayList();
                    ArrayList tempList2 = new ArrayList();
                    for (int i = 0; i < displayDOS.getAvailableDataObjectCount(); i++)
                    {
                        string dGUID = displayDOS.getAvailableDataObject(i).getData("GUID");
                        bool isExist = false;
                        com.dsc.kernal.databean.DataObject exDO = null;
                        for (int j = 0; j < currentDOS.getAvailableDataObjectCount(); j++)
                        {
                            string cGUID = currentDOS.getAvailableDataObject(j).getData("GUID");
                            if (dGUID.Equals(cGUID))
                            {
                                exDO = currentDOS.getAvailableDataObject(j);
                                isExist = true;
                                break;
                            }
                        }
                        if (isExist)
                        {
                            com.dsc.kernal.databean.DataObjectSet compareDOS = currentDOS.getFilteredDataObjectSet(sql);

                            displayDOS.getAvailableDataObject(i).delete();

                            bool isE = false;
                            com.dsc.kernal.databean.DataObject cDOS = null;
                            for (int j = 0; j < compareDOS.getAvailableDataObjectCount(); j++)
                            {
                                if (dGUID.Equals(compareDOS.getAvailableDataObject(j).getData("GUID")))
                                {
                                    isE = true;
                                    cDOS = compareDOS.getAvailableDataObject(j);
                                    break;
                                }
                            }
                            if (isE)
                            {
                                //isplayDOS.addDraft(exDO);
                                tempList.Add(exDO);
                            }
                        }
                        else
                        {
                            //currentDOS.addDraft(displayDOS.getAvailableDataObject(i));
                            tempList2.Add(displayDOS.getAvailableDataObject(i));
                        }
                    }
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        displayDOS.addDraft((com.dsc.kernal.databean.DataObject)tempList[i]);
                    }
                    for (int i = 0; i < tempList2.Count; i++)
                    {
                        currentDOS.addDraft((com.dsc.kernal.databean.DataObject)tempList2[i]);
                    }
                    displayDOS.compact();

                    ArrayList tempL3 = new ArrayList();
                    com.dsc.kernal.databean.DataObjectSet dos2=currentDOS.getFilteredDataObjectSet(sql);
                    for (int i = 0; i < dos2.getAvailableDataObjectCount(); i++)
                    {
                        bool isE = false;
                        for (int j = 0; j < displayDOS.getAvailableDataObjectCount(); j++)
                        {
                            if (dos2.getAvailableDataObject(i).getData("GUID").Equals(displayDOS.getAvailableDataObject(j).getData("GUID")))
                            {
                                isE = true;
                                break;
                            }
                        }
                        if (!isE)
                        {
                            tempL3.Add(dos2.getAvailableDataObject(i));
                        }
                    }
                    for (int i = 0; i < tempL3.Count; i++)
                    {
                        displayDOS.addDraft((com.dsc.kernal.databean.DataObject)tempL3[i]);
                    }
                }
                else
                {
                    com.dsc.kernal.databean.DataObjectSet compareDOS = currentDOS.getFilteredDataObjectSet(sql);
                    for (int i = 0; i < compareDOS.getAvailableDataObjectCount(); i++)
                    {
                        displayDOS.addDraft(compareDOS.getAvailableDataObject(i));
                    }
                }
                gp.setSession("displayDOS", displayDOS);
                //com.dsc.kernal.databean.DataObjectSet newdos = currentDOS.getFilteredDataObjectSet(sql);
                //gp.setSession("displayDOS", newdos);
            }
            catch (Exception te)
            {
                try
                {
                    engine.close();
                }
                catch { };
                Page.Response.Write("alert('" + te.Message + "');");
                gp.writeLog(te);
                gp.setSession("currentDOS", null);
                gp.setSession("displayDOS", null);
            }
        }
        */
        #endregion

        setOutDataList();
        string state = (string)gp.getSession("PageState");
        if (state.Equals("isEdit"))
        {
            SaveButton.Enabled = true;
            DeleteButton.Enabled = true;
        }
        EditButton.Enabled = false;

    }
    public void QueryButton_Click(object sender, EventArgs e)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        doQuery(gp.queryString());
    }
    public void SaveButton_Click(object sender, EventArgs e)
    {
        Page.Response.Write("OutDataListGridAddNew('" + DetailList.ClientID + "');");
    }
    public void EditButton_Click(object sender, EventArgs e)
    {
        Page.Response.Write("OutDataListGridEdit('"+DetailList.ClientID+"');");
        //EditButton.Enabled = false;
    }
    public void DeleteButton_Click(object sender, EventArgs e)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        com.dsc.kernal.databean.DataObject[] doos = DetailList.getSelectedItem();
        bool res = gp.beforeDeleteData(doos);
        if (res)
        {        
            gp.deleteData(doos);
            Page.Response.Write("OutDataListGridDelete('" + DetailList.ClientID + "');");
            EditButton.Enabled = false;
        }
    }
    public void CancelButton_Click(object sender, EventArgs e)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        com.dsc.kernal.databean.DataObjectSet dos = (com.dsc.kernal.databean.DataObjectSet)gp.getSession("currentDOS");
        DetailList.dataSource = dos;
        DetailList.updateTable();
        EditButton.Enabled = false;
    }
    public void ClearButton_Click(object sender, EventArgs e)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        gp.clearParameter();
    }

    /// <summary>
    /// 設定查詢鈕屬性
    /// </summary>
    /// <returns>true/false</returns>
    public DSCWebControl.GlassButton setQueryButton()
    {
        return QueryButton;
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
    /// 設定編輯鈕屬性
    /// </summary>
    /// <returns>true/false</returns>
    public DSCWebControl.GlassButton setEditButton()
    {
        return EditButton;
    }
    /// <summary>
    /// 設定刪除鈕屬性
    /// </summary>
    /// <returns>true/false</returns>
    public DSCWebControl.GlassButton setDeleteButton()
    {
        return DeleteButton;
    }

    private com.dsc.kernal.databean.DataObjectSet queryDB(AbstractEngine engine, string sql)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        NLAgent agent = new NLAgent();
        agent.loadSchema(gp.AgentSchema);
        agent.engine = engine;
        if (string.IsNullOrEmpty((string)gp.getSession("FK")))
        {
            if (sql.Equals(""))
            {
                agent.query("1=1");
            }
            else
            {
                agent.query(sql);
            }
        }
        else
        {
            if (sql.Equals(""))
            {
                sql = " " + (string)gp.getSession("FK") + "='" + (string)gp.getSession("HeadGUID") + "'";
            }
            else
            {
                sql += " and " + (string)gp.getSession("FK") + "='" + (string)gp.getSession("HeadGUID") + "'";
            }
            agent.query(sql);
        }
        return agent.defaultData;
    }

    private void setOutDataList()
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        com.dsc.kernal.databean.DataObjectSet displayDOS = (com.dsc.kernal.databean.DataObjectSet)gp.getSession("displayDOS");

        gp.queryAfter(displayDOS);
        DetailList.dataSource = displayDOS;
        DetailList.HiddenField = gp.GridHiddenField;
        DetailList.updateTable();
    }
    private void setInitButton()
    {
        QueryButton.Enabled = true;
        SaveButton.Enabled = false;
        EditButton.Enabled = false;
        DeleteButton.Enabled = false;
        CancelButton.Enabled = false;
    }
    private void setReadOnlyButton()
    {
        QueryButton.Enabled = true;
        SaveButton.Enabled = false;
        EditButton.Enabled = false;
        DeleteButton.Enabled = false;
        CancelButton.Enabled = false;
    }
    public DSCWebControl.OutDataList getGridTable()
    {
        return DetailList;
    }
    protected void DetailList_ShowRowData(com.dsc.kernal.databean.DataObject objects)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        try
        {
            gp.showData(objects);
            string state = (string)gp.getSession("PageState");
            if (state.Equals("isEdit"))
            {
                EditButton.Enabled = true;
            }
        }
        catch (Exception te)
        {
            Response.Write("alert('" + te.Message + "');");
            gp.writeLog(te);
            return;
        }
    }
    protected bool DetailList_SaveRowData(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        try
        {
            engine = factory.getEngine(engineType, connectString);
            bool isSuccess = gp.checkFieldData(engine, objects);
            if (isSuccess)
            {
                if (isNew)
                {
                    objects.setData("GUID", IDProcessor.getID(""));
                    if (((string)gp.getSession("HeadGUID")).Equals(""))
                    {
                        objects.setData((string)gp.getSession("FK"), "TEMP");
                    }
                    else
                    {
                        objects.setData((string)gp.getSession("FK"), (string)gp.getSession("HeadGUID"));
                    }
                    gp.addData(objects);
                }
                else
                {
                    if (((string)gp.getSession("HeadGUID")).Equals(""))
                    {
                        objects.setData((string)gp.getSession("FK"), "TEMP");
                    }
                    else
                    {
                        objects.setData((string)gp.getSession("FK"), (string)gp.getSession("HeadGUID"));
                    }
                    //
                    gp.editData(objects);
                    DetailList.dataSource.add(objects);
                }
                return true;
            }
            else
            {
                throw new Exception(gp.showErrorMessage());
                return false;
            }
        }
        catch (Exception te)
        {
            Response.Write("alert('" + te.Message + "');");
            gp.writeLog(te);
            return false;
        }
        finally 
        {
            if (engine != null) {
                engine.close();
            }
        }
        

    }
    protected void TransferValue_TextChanged(object sender, EventArgs e)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        string v = TransferValue.ValueText;
        string[] tag = v.Split(new char[] { ':' });
        if (tag[0].Equals("CHANGESTATUS"))
        {
            if (tag[1].Equals("isReadOnly"))
            {
                gp.setSession("PageState", "isReadOnly");
                setReadOnlyButton();
            }
            else
            {
                gp.setSession("PageState", "isEdit");
                setInitButton();
            }
        }
    }
    protected void DetailList_AddOutline(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
        EditButton.Enabled = false;
        addDirtyObjectToCurrentDOS(objects);
    }
    public void addDirtyObjectToCurrentDOS(com.dsc.kernal.databean.DataObject objects)
    {
        EnergyDetailPage gp = (EnergyDetailPage)Page;
        bool isExist = false;
        com.dsc.kernal.databean.DataObjectSet currentDOS = (com.dsc.kernal.databean.DataObjectSet)gp.getSession("currentDOS");
        for (int i = 0; i < currentDOS.getAvailableDataObjectCount(); i++)
        {
            if (currentDOS.getAvailableDataObject(i).getData("GUID").Equals(objects.getData("GUID")))
            {
                isExist = true;
                break;
            }
        }
        if (!isExist)
        {
            currentDOS.addDraft(objects);
        }
    }
    protected void DetailList_DeleteData()
    {
        for (int i = 0; i < DetailList.dataSource.getDataObjectCount(); i++)
        {
            com.dsc.kernal.databean.DataObject tmp = DetailList.dataSource.getDataObject(i);
            if (tmp.isDelete())
            {
                addDirtyObjectToCurrentDOS(tmp);
            }
        }
    }
}
