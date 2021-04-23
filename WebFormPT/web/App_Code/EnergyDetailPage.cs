using System;
using System.Collections.Generic;
using System.Text;
using com.dsc.kernal.factory;
using com.dsc.kernal.agent;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using System.Collections;

public class EnergyDetailPage : BaseWebUI.GeneralWebPage
{
    private ArrayList errMsg = new ArrayList();
    #region 繼承類別必須設定參數
    /// <summary>
    /// 處理Agent的ClassString
    /// </summary>
    public string AgentSchema = "";
    /// <summary>
    /// 隱藏欄位名稱陣列
    /// </summary>
    public string[] GridHiddenField = new string[0];
    #endregion

    protected override void OnInit(EventArgs e)
    {
        if (Request.QueryString["SavePage"] != null)
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            //讀取debugPage
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(acs.engineType, acs.connectString);
            string sql = "select SMVPAAA009, SMVPAAA014, SMVPAAA021, SMVPAAA022 from SMVPAAA";
            System.Data.DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows[0][0].ToString().Equals("Y"))
            {
                Session["DebugPage"] = true;
            }
            else
            {
                Session["DebugPage"] = false;
            }
            Session["MaxRecordCount"] = ds.Tables[0].Rows[0][1];
            Session["FlowProcessCount"] = ds.Tables[0].Rows[0][2]; //流程引擎呼叫處理次數
            Session["FlowProcessWaiting"] = ds.Tables[0].Rows[0][3]; //流程引擎呼叫錯誤時等待毫秒

            sql = "select OID, userName, localeString from Users where id='" + com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["EFLogonID"]) + "'";
            ds = engine.getDataSet(sql, "TEMP");

            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["UserGUID"] = ds.Tables[0].Rows[0][0].ToString();
                Session["UserName"] = ds.Tables[0].Rows[0][1].ToString();
                Session["Locale"] = ds.Tables[0].Rows[0][2].ToString();
                Session["IsSysAdmin"] = false;
            }
            else
            {
                Session["UserGUID"] = "";
            }

            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            Session["FlowAdapter"] = sp.getParam("FlowAdapter");
            Session["NaNaWebService"] = sp.getParam("NaNaWebService");
            Session["DotJWebService"] = sp.getParam("DotJWebService");
            Session["FlowAccount"] = sp.getParam("FlowAccount");
            Session["FlowPassword"] = sp.getParam("FlowPassword");

            engine.close();

            Session["connectString"] = acs.connectString;
            Session["engineType"] = acs.engineType;
            Session["dbIndex"] = "0";
            Session["layoutType"] = "Enterprise";
            Session["UserID"] = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["EFLogonID"]);
        } 
        base.OnInit(e);
    }

    public virtual void init()
    {
    }
    /// <summary>
    /// 此方法在初始化畫面時執行, 可撰寫UI元件預設內容值（例如下拉選單)
    /// </summary>
    /// <param name="engine"></param>
    public virtual void initUIControl(AbstractEngine engine)
    {
    }
    /// <summary>
    /// 查詢過濾條件(不包含Whrer)
    /// </summary>
    /// <param name="agent">agent</param>
    public virtual string queryString()
    {
        return "";
    }
    /// <summary>
    /// 顯示資料的事件
    /// </summary>
    /// <param name="objects">欲顯示的資料物件</param>
    public virtual void showData(com.dsc.kernal.databean.DataObject objects)
    {
    }
    /// <summary>
    /// 按下新增鈕的事件
    /// </summary>
    /// <param name="objects">欲儲存的資料物件</param>
    public virtual void addData(com.dsc.kernal.databean.DataObject objects)
    {
    }
    /// <summary>
    /// 按下修改之事件實作
    /// </summary>
    /// <param name="objects">欲修改的資料物件</param>
    public virtual void editData(com.dsc.kernal.databean.DataObject objects)
    {
    }
    /// <summary>
    /// 按下刪除紐前的動作
    /// </summary>
    /// <param name="objects">欲刪除的資料物件</param>
    /// <returns>true:執行後續刪除;false:不刪除</returns>
    public virtual bool beforeDeleteData(com.dsc.kernal.databean.DataObject[] objects)
    {
        return true;
    }
    /// <summary>
    /// 按下刪除之事件實作
    /// </summary>
    /// <param name="objects">欲刪除的資料物件</param>
    public virtual void deleteData(com.dsc.kernal.databean.DataObject[] objects)
    {
    }
    /// <summary>
    /// 使用者按下儲存鈕後，可以在此事件中實作額外資料的處理，亦可於 saveDataEvent 中撰寫，此事件於 saveDataEvent、資料檢核之後執行
    /// </summary>
    /// <param name="objects">欲儲存的資料物件</param>
    public virtual void saveCustomData(com.dsc.kernal.databean.DataObject objects)
    {
    }
    /// <summary>
    /// 按下查詢之後事件實作
    /// </summary>
    /// <param name="dos"></param>
    public virtual void queryAfter(com.dsc.kernal.databean.DataObjectSet dos)
    {

    }

    /// <summary>
    /// 清除查詢條件的事件實做
    /// </summary>
    public virtual void clearParameter()
    {
    }

    public virtual string saveDB(AbstractEngine engine, string FK, string GUID)
    {
        com.dsc.kernal.databean.DataObjectSet dos = (com.dsc.kernal.databean.DataObjectSet)getSession("currentDOS");
        if (dos == null)
        {
            //return "currentDOS is null";
            return "";
        }
        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            dos.getAvailableDataObject(i).setData(FK, GUID);
        }
        NLAgent agent = new NLAgent();
        agent.loadSchema(AgentSchema);

        agent.engine = engine;
        //agent.query("(1=2)");

        agent.defaultData = dos;

        bool res = agent.update();
        if (!res)
        {
            return engine.errorString;
        }
        return "";
    }

    private void setReadOnlyForm()
    {
        DSCWebControl.DSCWebControlBase ele = null;
        for (int i = 0; i < this.Controls.Count; i++)
        {
            try
            {
                ele = (DSCWebControl.DSCWebControlBase)this.Controls[i];
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
                ele.ReadOnly = true;
            }
            catch { };
            setReadOnlyFormRec(c.Controls[i]);
        }

    }


    public void MessageBox(string msg)
    {
        base.MessageBox(msg);
    }
    public bool getDispatchEventState()
    {
        return base.IsProcessEvent;
    }

    public object getSession(string sessionName)
    {
        return base.getSession(sessionName);
    }
    public void setSession(string sessionName, object obj)
    {
        base.setSession(sessionName, obj);
    }
    public void setSession(string pageUniqueID, string sessionName, object value)
    {
        base.setSession(pageUniqueID, sessionName, value);
    }
    public void writeLog(Exception te)
    {
        base.writeLog(te);
    }
    /// <summary>
    /// 由各單身頁面的FrameID取得該頁的PageUniqueID。若FrameID=""則取得單頭頁面
    /// </summary>
    /// <param name="frameID">單身頁面的FrameID</param>
    /// <returns>PageUniqueID</returns>
    public string getDetailPageUniqueID(string frameID)
    {
        if (frameID.Equals(""))
        {
            return (string)getSession("ParentPageUniqueID");
        }
        else
        {
            string ret = (string)Session[(string)getSession("ParentPageUniqueID")+"_"+frameID].ToString();
            if (ret == null)
            {
                throw new Exception("FrameID 不存在，或是該頁面尚未初始化");
            }
            return ret;
        }
    }
    /// <summary>
    /// 取得單身頁面元件的屬性值。若FrameID=""則取得單頭頁面的元件
    /// </summary>
    /// <param name="frameID">單身頁面的FrameID</param>
    /// <param name="clientID">單身頁面中該元件的ClientID。注意，請由該頁面的HTML取得ClientID，並非Server端ClientID</param>
    /// <param name="propertyName">屬性名稱。例如：ValueText</param>
    /// <returns>回傳該屬性值</returns>
    public object getDetailElementProperty(string frameID, string clientID, string propertyName)
    {
        string dpid = getDetailPageUniqueID(frameID);
        object obj = Session[dpid + "_" + clientID + "_" + propertyName];
        return obj;
    }
    /// <summary>
    /// 設定單身頁面元件的屬性值。當發生錯誤，直接擲出例外。若FrameID=""則設定單頭頁面
    /// </summary>
    /// <param name="frameID">單身頁面的FrameID</param>
    /// <param name="clientID">單身頁面中該元件的ClientID。注意，請由該頁面的HTML取得ClientID，並非Server端ClientID</param>
    /// <param name="propertyName">屬性名稱。例如：ValueText</param>
    /// <param name="value">設定的屬性值</param>
    public void setDetailElementProperty(string frameID, string clientID, string propertyName, object value)
    {
        string dpid = getDetailPageUniqueID(frameID);
        Session[dpid + "_" + clientID + "_" + propertyName] = value;
    }
    /// <summary>
    /// 更新單身頁面元件。若FrameID=""則取得單頭頁面的元件。當指定單身頁面元件值之後，需呼要此方法以便顯示。此方法僅可在ECP元件事件中呼叫
    /// </summary>
    /// <param name="frameID">單身頁面的FrameID</param>
    /// <param name="elementType">元件類型。例如：SingleField。注意大小寫</param>
    /// <param name="clientID">單身頁面中該元件的ClientID。注意，請由該頁面的HTML取得ClientID，並非Server端ClientID</param>
    public void refreshDetailElement(string frameID, string elementType, string clientID)
    {
        Response.Write("parent.dispatchDetailRefreshEvent('" + frameID + "','" + elementType + "_ReadServer','" + clientID + "');");
    }

    public virtual bool checkFieldData(AbstractEngine engine, DataObject objects) 
    {
        return true;
    }
    /// <summary>
    /// 要顯示的錯誤訊息
    /// </summary>
    /// <param name="errorMessage">錯誤訊息內容</param>
    public void pushErrorMessage(string errorMessage)
    {
        errMsg.Add(errorMessage);
    }

    /// <summary>
    /// 顯示錯誤訊息
    /// </summary>
    public string showErrorMessage()
    {
        string msg = "";
        for (int i = 0; i < errMsg.Count; i++)
        {
            msg += (string)errMsg[i] + "\\n";
        }
        /*
        if (msg.Length > 0)
        {
            msg = "alert('" + msg + "');";
            Response.Write(msg);
        }
        */
        return msg;
    }
}