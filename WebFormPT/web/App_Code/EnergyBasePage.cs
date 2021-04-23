using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using com.dsc.kernal.factory;
using com.dsc.kernal.databean;
using com.dsc.kernal.agent;
using com.dsc.kernal.utility;
using com.dsc.flow.data;
using com.dsc.flow.server;
using System.Data;
using System.Xml;

public class EnergyBasePage : BaseWebUI.GeneralWebPage
{
    /// <summary>
    /// 檢視狀態
    /// </summary>
    public static string isReadOnly = "isReadOnly";
    /// <summary>
    /// 編輯狀態
    /// </summary>
    public static string isEdit = "isEdit";
    /// <summary>
    /// 新增狀態
    /// </summary>
    public static string isAddNew = "isAddNew";

    #region 繼承類別必須設定參數
    /// <summary>
    /// iframe資訊 [iframe ID, iframe URL, FK]
    /// 若FK為空字串則代表與單頭無關係
    /// 請客制查詢結果
    /// </summary>
    public string[,] iframeInfo;
    /// <summary>
    /// 處理Agent的ClassString
    /// </summary>
    public string AgentSchema = "";
    /// <summary>
    ///  程式代號-程式權限使用
    /// </summary>
    public string baseIdentity = "";
    #endregion

    public string getPageState()
    {
        return (string)getSession("PageState");
    }

    /// <summary>
    /// 初始化畫面實作，必須設定iframeInfo 、AgentSchema、baseIdentity資訊
    /// </summary>
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
    /// 此方法需撰寫修改狀態各欄位的初始值以及UI顯示狀態
    /// </summary>
    public virtual void editData()
    {
    }
    /// <summary>
    /// 按下儲存鈕的事件
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="objects">目前畫面資料物件</param>
    /// <returns>true:成功 false:失敗</returns>
    public virtual bool saveData(AbstractEngine engine, DataObject objects)
    {
        return true;
    }
    /// <summary>
    /// 此方法需撰寫新增狀態各欄位的初始值以及UI顯示狀態
    /// </summary>
    public virtual void addData()
    {
    }
    /// <summary>
    /// 按下取消鈕的事件
    /// </summary>
    public virtual void cancelData()
    {
    }
    /// <summary>
    /// 顯示資料的事件
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="objects">目前畫面資料物件</param>
    /// <param name="objects">欲顯示的資料物件</param>
    public virtual void showData(AbstractEngine engine, DataObject objects)
    {
    }
    /// <summary>
    /// 按下儲存鈕之前事件實作
    /// </summary>
    /// <returns>true:成功 false:失敗</returns>
    public virtual bool beforeSaveData()
    {
        return true;
    }
    /// <summary>
    /// 按下儲存鈕之前事件實作(新)
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="objects">目前畫面資料物件</param>
    /// <returns>true:成功 false:失敗</returns>
    public virtual bool beforeSaveData(AbstractEngine engine, DataObject objects)
    {
        return true;
    }
    /// <summary>
    /// 按下儲存鈕之後事件實作
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="objects">目前畫面資料物件</param>
    /// <returns>true:成功 false:失敗</returns>
    public virtual bool afterSaveData()
    {
        return true;
    }
    /// <summary>
    /// 按下儲存鈕之後事件實作(新)
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="objects">目前畫面資料物件</param>
    /// <returns>true:成功 false:失敗</returns>
    public virtual bool afterSaveData(AbstractEngine engine, DataObject objects)
    {
        return true;
    }
    /// <summary>
    /// 欄位檢核
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="objects">目前畫面資料物件</param>
    /// <returns>true:成功 false:失敗</returns>
    public virtual bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        return true;
    }
    /// <summary>
    /// 由資料庫讀取資料到資料物件
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="guid">資料物件的GUID欄位值. 若為新增模式, 此欄位傳入零長度字串</param>
    /// <param name="UIStatus">目前畫面的UIStatus</param>
    /// <returns>包含資料的資料物件</returns>
    public virtual DataObject readDB(AbstractEngine engine, string guid)
    {
        NLAgent agent = new NLAgent();
        agent.loadSchema(AgentSchema);

        agent.engine = engine;
        if (string.IsNullOrEmpty(guid))
        {
            if (!agent.query("(1=2)"))
            {
                throw new Exception(engine.errorString);
            }
        }
        else
        {
            if (!agent.query("GUID='" + guid + "'"))
            {
                throw new Exception(engine.errorString);
            }
        }

        DataObject ddo;
        if (agent.defaultData.getAvailableDataObjectCount() > 0)
        {
            ddo = agent.defaultData.getAvailableDataObject(0);
        }
        else
        {
            ddo = agent.defaultData.create();
            for (int i = 0; i < ddo.dataField.Length; i++)
            {
                ddo.setData(ddo.dataField[i], "");
            }
        }
        if (!agent.defaultData.errorString.Equals(""))
        {
            throw new Exception(agent.defaultData.errorString);
        }
        return ddo;
    }
    /// <summary>
    /// 將資料由資料物件儲存到資料庫
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="objects">要處理的資料物件</param>
    /// <param name="oriObjects">原始的資料物件</param>
    /// <param name="UIStatus">此筆資料狀態</param>
    public virtual void saveDB(AbstractEngine engine, DataObject objects, DataObject oriObjects, string UIStatus)
    {
        NLAgent agent = new NLAgent();
        agent.loadSchema(AgentSchema);

        agent.engine = engine;
        agent.query("(1=2)");
        bool result = agent.defaultData.add(objects);
        if (!result)
        {
            engine.close();
            throw new Exception(agent.defaultData.errorString);
        }
        else
        {
            if (oriObjects != null)
            {
                agent.defaultData.add(oriObjects);
            }
            bool res = agent.update();
            if (!res)
            {
                throw new Exception(engine.errorString);
            }
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
    public void writeLog(Exception te)
    {
        base.writeLog(te);
    }
    public string getPageUniqueID()
    {
        return base.PageUniqueID;
    }
    public void refreshAllClientElement()
    {
        base.refreshAllClientElement();
    }
    /// <summary>
    /// 由各單身頁面的FrameID取得該頁的PageUniqueID
    /// </summary>
    /// <param name="frameID">單身頁面的FrameID</param>
    /// <returns>PageUniqueID</returns>
    public string getDetailPageUniqueID(string frameID)
    {
        if (frameID.Equals(""))
        {
            return this.PageUniqueID;
        }
        else
        {
            string ret = (string)getSession(frameID).ToString();
            if (ret == null)
            {
                throw new Exception("FrameID 不存在，或是該頁面尚未初始化");
            }
            return ret;
        }
    }
    /// <summary>
    /// 取得單身頁面元件的屬性值
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
    /// 設定單身頁面元件的屬性值。當發生錯誤，直接擲出例外
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
    /// 更新單身頁面元件。當指定單身頁面元件值之後，需呼要此方法以便顯示。此方法僅可在ECP元件事件中呼叫
    /// </summary>
    /// <param name="frameID">單身頁面的FrameID</param>
    /// <param name="elementType">元件類型。例如：SingleField。注意大小寫</param>
    /// <param name="clientID">單身頁面中該元件的ClientID。注意，請由該頁面的HTML取得ClientID，並非Server端ClientID</param>
    public void refreshDetailElement(string frameID, string elementType, string clientID)
    {
        Response.Write("dispatchDetailRefreshEvent('"+frameID+"','"+elementType+"_ReadServer','"+clientID+"');");
    }
    public ArrayList errMsg = new ArrayList();
    /// <summary>
    /// 顯示錯誤訊息
    /// </summary>
    public string showErrorMessage()
    {
        string msg = "";
        for (int i = 0; i < errMsg.Count; i++)
        {
            msg += (string)errMsg[i] + "\n";
            //msg += (string)errMsg[i];
        }
        return msg;
    }
    /// <summary>
    /// 要顯示的錯誤訊息
    /// </summary>
    /// <param name="errorMessage">錯誤訊息內容</param>
    public void pushErrorMessage(string errorMessage)
    {
        errMsg.Add(errorMessage);
    }

    public void initControl()
    {
        base.initControl();
    }
    /// <summary>
    /// 取得頁籤中Grid資料物件集
    /// </summary>
    /// <param name="iFrameID">頁籤ID</param>
    /// <returns>DataObjectSet</returns>
    public DataObjectSet getDetailDataObjectSet(string iFrameID)
    {
        return (DataObjectSet)Session[(string)getSession(iFrameID) + "_ctl00_DetailList_dataSource"];
    }

    /// <summary>
    /// 取得頁籤中勾選的資料物件陣列
    /// </summary>
    /// <param name="iFrameID">頁籤ID</param>
    /// <returns>DataObject[]</returns>
    public com.dsc.kernal.databean.DataObject[] getDetailSelectedItem(string iFrameID)
    {
        DataObjectSet dos = (DataObjectSet)Session[(string)getSession(iFrameID) + "_ctl00_DetailList_dataSource"];
        ArrayList AL = (ArrayList)Session[(string)getSession(iFrameID) + "_ctl00_DetailList_Checkers"];
        ArrayList ary = new ArrayList();
        int pagesize = dos.getPageSize();
        for (int i = 0; i < AL.Count; i++)
        {
            bool[] pg = (bool[])AL[i];
            for (int j = 0; j < pg.Length; j++)
            {
                if (pg[j])
                {
                    ary.Add(dos.getAvailableDataObject(i * pagesize + j));
                }
            }
        }
        DataObject[] objary = new DataObject[ary.Count];
        for (int i = 0; i < ary.Count; i++)
        {
            objary[i] = (DataObject)ary[i];
        }
        return objary;
    }

}
