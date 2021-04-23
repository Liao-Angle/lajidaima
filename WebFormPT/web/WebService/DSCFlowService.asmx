<%@ WebService Language="C#" Class="DSCFlowService" %>

using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Data;
using System.Xml;
using System.Text;
using com.dsc.kernal.factory;
using com.dsc.kernal.databean;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;
using com.dsc.flow.server;
using WebServerProject;

/// <summary>
/// DSCFlowService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DSCFlowService : System.Web.Services.WebService
{

    public DSCFlowService()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 由Session取得GP各參數
    /// </summary>
    /// <param name="param">參數名稱</param>
    /// <returns>參數值</returns>
    protected string getGPParam(string param)
    {
        return (String)Session[param];
    }
    /// <summary>
    /// 由使用者代號取得使用者姓名
    /// </summary>
    /// <param name="engine">資料庫連線字串</param>
    /// <param name="id">使用者代號</param>
    /// <returns>使用者姓名</returns>
    protected string getUserName(AbstractEngine engine, string id)
    {
        string sql = "select userName from Users where id='" + id + "'";
        string uName = (string)engine.executeScalar(sql);
        return uName;
    }

    /// <summary>
    /// 由部門代號取得部門名稱
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="id">部門代號</param>
    /// <returns>部門名稱</returns>
    protected string getUnitName(AbstractEngine engine, string id)
    {
        string sql = "select organizationUnitName from OrganizationUnit where id='" + id + "'";
        string uName = (string)engine.executeScalar(sql);
        return uName;
    }

    /// <summary>
    /// 由SMWBAAA001取得流程參數
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="guid">SMWBAAA001</param>
    /// <returns>包含參數內容的Hashtable</returns>
    protected Hashtable getProcessParameter(AbstractEngine engine, string guid)
    {
        string sql = "select SMWBAAB003, SMWBAAB006 from SMWBAAB where SMWBAAB002='" + guid + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        Hashtable hs = new Hashtable();

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            hs.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());
        }
        return hs;
    }

    /// <summary>
    /// 取得單號程序
    /// </summary>
    /// <param name="autoCodeGUID">自動編號識別碼(SMVIAAA.GUID)</param>
    /// <returns>單號</returns>
    protected virtual string getSheetNoProcedure(AbstractEngine engine, string autoCodeGUID, Hashtable sheetNoParam)
    {
        //國昌20090812: engine另開新的，避免LOCK太久
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        string connectString = acs.connectString;
        string engineType = acs.engineType;

        IOFactory factory = new IOFactory();
        AbstractEngine sheetengine = null;

        try
        {

            if ((autoCodeGUID == null) || (autoCodeGUID.Equals("")))
            {
                throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError10", "並未設定此流程單號!"));
            }
            else
            {
                sheetengine = factory.getEngine(engineType, connectString);
                string sql = "select SMVIAAA002 from SMVIAAA where SMVIAAA001='" + autoCodeGUID + "'";
                DataSet ds = sheetengine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    sheetengine.close();
                    throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError10", "並未設定此流程單號!"));
                }
                string autoCodeID = ds.Tables[0].Rows[0][0].ToString();

                WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
                ac.engine = sheetengine;
                string systemSheetNo = ac.getAutoCode(autoCodeID, sheetNoParam);
                sheetengine.close();
                return systemSheetNo;
            }
        }
        catch (Exception ue)
        {
            try
            {
                sheetengine.close();
            }
            catch { };
            throw (ue);
        }
    }

    /// <summary>
    /// 使用設定檔發起流程
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="info">流程發起資訊物件</param>
    /// <param name="flowid">流程定義代號</param>
    /// <param name="subject">主旨</param>
    /// <returns>流程實例序號</returns>
    protected string invokeProcess(AbstractEngine engine, SubmitInfo info, string flowid, string subject, bool debugPage)
    {
        bool isfillerID = false; 
        SysParam sp = new SysParam(engine);
        string flowType = getGPParam("FlowAdapter");
        string con1 = getGPParam("NaNaWebService");
        string con2 = getGPParam("DotJWebService");
        string account = getGPParam("FlowAccount");
        string password = getGPParam("FlowPassword");
        try
        {
            isfillerID = Convert.ToBoolean(sp.getParam("isfillerID"));
        }
        catch { }

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
        adp.init(con1, con2, account, password, "", "172.19.209.120", "172.19.209.120:8080", "EF2KWeb", fname, debugPage);

        string flowOID = "";
        if (isfillerID)
        {
            flowOID = adp.invokeProcess(flowid, info.fillerID, info.submitOrgID, subject);
        }
        else
        {
            flowOID = adp.invokeProcess(flowid, info.ownerID, info.submitOrgID, subject);
        }

        adp.logout();

        return flowOID;
    }

    /// <summary>
    /// 使用設定檔發起流程並參入參數
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="info">流程發起資訊物件</param>
    /// <param name="flowid">流程定義代號</param>
    /// <param name="paramID">流程參數代號</param>
    /// <param name="paramValue">參數內容</param>
    /// <param name="subject">主旨</param>
    /// <returns>流程實例序號</returns>
    protected string invokeProcessByParameter(AbstractEngine engine, SubmitInfo info, string flowid, string paramID, string paramValue, string subject, bool debugPage)
    {
        bool isfillerID = false; 
        SysParam sp = new SysParam(engine);
        string flowType = getGPParam("FlowAdapter");
        string con1 = getGPParam("NaNaWebService");
        string con2 = getGPParam("DotJWebService");
        string account = getGPParam("FlowAccount");
        string password = getGPParam("FlowPassword");
        try
        {
            isfillerID = Convert.ToBoolean(sp.getParam("isfillerID"));
        }
        catch { }

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
        adp.init(con1, con2, account, password, "", "172.19.209.120", "172.19.209.120:8080", "EF2KWeb", fname, debugPage);

        string flowOID = "";
        if (isfillerID)
        {
            flowOID = adp.invokeProcessByParameter(flowid, info.fillerID, info.submitOrgID, paramID, paramValue, subject);
        }
        else
        {
            flowOID = adp.invokeProcessByParameter(flowid, info.ownerID, info.submitOrgID, paramID, paramValue, subject);
        }

        adp.logout();

        return flowOID;
    }

    /// <summary>
    /// 使用設定檔以及表單參數發起流程, 並且傳入流程設定
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="info">流程發起資訊物件</param>
    /// <param name="flowid">流程定義代號</param>
    /// <param name="formOID">表單識別號</param>
    /// <param name="formXML">表單參數內容</param>
    /// <param name="subject">主旨</param>
    /// <param name="actDef">自訂流程內容</param>
    /// <returns>流程實例序號</returns>
    protected string invokeProcessByFormParameterAndAddCusAct(AbstractEngine engine, SubmitInfo info, string flowid, string formOID, string formXML, string subject, string actDef, bool debugPage)
    {
        bool isfillerID = false;
        SysParam sp = new SysParam(engine);
        string flowType = getGPParam("FlowAdapter");
        string con1 = getGPParam("NaNaWebService");
        string con2 = getGPParam("DotJWebService");
        string account = getGPParam("FlowAccount");
        string password = getGPParam("FlowPassword");
        try
        {
            isfillerID = Convert.ToBoolean(sp.getParam("isfillerID"));
        }
        catch { }
        
        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
        adp.init(con1, con2, account, password, "", "172.19.209.120", "172.19.209.120:8080", "EF2KWeb", fname, debugPage);

        string flowOID = "";

        if (isfillerID)
        {
            flowOID = adp.invokeProcessByFormParameterAndAddCusAct(flowid, info.fillerID, info.submitOrgID, formOID, formXML, subject, actDef);
        }
        else
        {
            flowOID = adp.invokeProcessByFormParameterAndAddCusAct(flowid, info.ownerID, info.submitOrgID, formOID, formXML, subject, actDef);
        }

        adp.logout();

        return flowOID;
    }

    /// <summary>
    /// 使用設定檔以及表單參數發起流程
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="info">流程發起資訊物件</param>
    /// <param name="flowid">流程定義代號</param>
    /// <param name="formOID">表單識別號</param>
    /// <param name="formXML">表單參數內容</param>
    /// <param name="subject">主旨</param>
    /// <returns>流程實例序號</returns>
    protected string invokeProcessByFormParameter(AbstractEngine engine, SubmitInfo info, string flowid, string formOID, string formXML, string subject, bool debugPage)
    {
        bool isfillerID = false; 
        SysParam sp = new SysParam(engine);
        string flowType = getGPParam("FlowAdapter");
        string con1 = getGPParam("NaNaWebService");
        string con2 = getGPParam("DotJWebService");
        string account = getGPParam("FlowAccount");
        string password = getGPParam("FlowPassword");
        try
        {
            isfillerID = Convert.ToBoolean(sp.getParam("isfillerID"));
        }
        catch { }

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
        adp.init(con1, con2, account, password, "", "172.19.209.120", "172.19.209.120:8080", "EF2KWeb", fname, debugPage);

        string flowOID = "";
        if (isfillerID)
        {
            flowOID = adp.invokeProcessByFormParameter(flowid, info.fillerID, info.submitOrgID, formOID, formXML, subject);
        }
        else
        {
            flowOID = adp.invokeProcessByFormParameter(flowid, info.ownerID, info.submitOrgID, formOID, formXML, subject);
        }

        adp.logout();

        return flowOID;
    }


    /// <summary>
    /// 發起流程
    /// </summary>
    /// <param name="ApplicationID">應用程式代號(對應稽核模組)</param>
    /// <param name="ModuleID">模組代號(對應稽核模組)</param>
    /// <param name="ProcessPageID">表單代號/作業畫面代號(對應稽核模組/SMWAAAA002)</param>
    /// <param name="sheetNo">表單單號。若為零長度字串，表示使用預設自動編號</param>
    /// <param name="sheetnoparam">若表單單號不指定，此處需指定自動編號時所需的參數EX.<a>1</a><b>2</b></param>
    /// <param name="subject">主旨</param>
    /// <param name="userID">使用者代號</param>
    /// <param name="fillerID">填表人代號</param>
    /// <param name="fillerOrgID">填表人單位代號</param>
    /// <param name="ownerID">關係人代號</param>
    /// <param name="ownerOrgID">關係人單位代號</param>
    /// <param name="submitOrgID">發起單位。</param>
    /// <param name="importance">重要性(SubmitInfo.LOW(0), SubmitInfo.MEDIUM(1), SubmitInfo.HIGH(2))</param>
    /// <param name="AgentSchema">AgentSchema完整類別名稱</param>
    /// <param name="dataXML">表單資料內容</param>
    /// <param name="flowParameter">流程參數內容。零長度字串表示無需傳遞流程參數</param>
    /// <param name="firstParam">送單時即須送出的流程參數名稱。若無參數需傳遞，可為null或零長度字串</param>
    /// <param name="addSignXML">加簽內容。零長度字串表示無需加簽</param>
    /// <param name="localeString">語系代號若空字串表示使用zh_TW</param>
    /// <returns>
    /// XML格式字串:
    /// 正常發起:
    /// 
    /// 有錯誤發生:
    /// 
    /// </returns>
    [WebMethod(EnableSession=true)]
    public string createFlow(string ApplicationID, string ModuleID, string ProcessPageID, string sheetNo, string sheetnoparam, string subject, string userID, string fillerID, string fillerOrgID, string ownerID, string ownerOrgID, string submitOrgID, string importance, string AgentSchema, string dataXML, string flowParameter, string firstParam, string addSignXML, string localeString, bool debugPage)
    {

        AbstractEngine engine = null;
        try
        {            
            //dataXML = Utility.UrlDecode(dataXML);
            
            //flowParameter = Utility.UrlDecode(flowParameter);
            
            //---設定語系
            if ((localeString == null) || (localeString == ""))
            {
                Session["Locale"] = "zh_TW";
            }
            else
            {
                Session["Locale"] = localeString;
            }

            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            string connectString = acs.connectString;
            string engineType = acs.engineType;

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            engine.startTransaction(IsolationLevel.ReadCommitted);

            //設定Session
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            Session["FlowAdapter"] = sp.getParam("FlowAdapter");
            Session["NaNaWebService"] = sp.getParam("NaNaWebService");
            Session["DotJWebService"] = sp.getParam("DotJWebService");
            Session["FlowAccount"] = sp.getParam("FlowAccount");
            Session["FlowPassword"] = sp.getParam("FlowPassword");

            string sql2 = "select SMVPAAA009, SMVPAAA014, SMVPAAA021, SMVPAAA022 from SMVPAAA";
            DataSet ds3 = engine.getDataSet(sql2, "TEMP");
            if (ds3.Tables[0].Rows[0][0].ToString().Equals("Y"))
            {
                Session["DebugPage"] = true;
            }
            else
            {
                Session["DebugPage"] = false;
            }
            Session["MaxRecordCount"] = ds3.Tables[0].Rows[0][1];
            Session["FlowProcessCount"] = ds3.Tables[0].Rows[0][2]; //流程引擎呼叫處理次數
            Session["FlowProcessWaiting"] = ds3.Tables[0].Rows[0][3]; //流程引擎呼叫錯誤時等待毫秒


            //由AgentSchema建立資料物件
            //if (!dataXML.Equals(""))
            //{
            com.dsc.kernal.agent.NLAgent agent = new NLAgent();
            agent.loadSchema(AgentSchema);
            agent.engine = engine;
            if (!agent.query("(1=2)"))
            {
                throw new Exception(engine.errorString);
            }
            DataObjectSet dos = agent.defaultData;

            DataObject currentObject = dos.create();
                        
            currentObject.loadXML(dataXML);

            if (!dos.add(currentObject))
            {
                throw new Exception(dos.errorString);
            }
            //}

            //送單程序
            string afterSendMode = "0"; //送單後模式
            bool isShowFlow = false; //是否顯示流程圖

            string flowOID = ""; //流程實例序號
            string flowid = ""; //流程定義代號
            string flowname = ""; //流程定義名稱
            string sheetno = ""; //單號

            flowOID = com.dsc.kernal.utility.IDProcessor.getID("");


            //要送單, 會取得流程序號以及流程代號
            //取得流程定義代號
            string sql = "select SMWBAAA003, SMWDAAA001, SMWDAAA011,SMWDAAA012,SMWBAAA004, SMWAAAA001,SMWDAAA018, SMWBAAA001, SMWDAAA024 from SMWAAAA inner join SMWDAAA on SMWAAAA001=SMWDAAA005 inner join SMWBAAA on SMWBAAA004=SMWDAAA003 where SMWAAAA002='" + ProcessPageID + "' and SMWDAAA006='Init'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError12", "找不到此作業畫面所要發起的流程定義"));
            }
            flowid = ds.Tables[0].Rows[0][0].ToString();

            string wd001 = ds.Tables[0].Rows[0][1].ToString();
            flowname = ds.Tables[0].Rows[0][4].ToString();
            string wa001 = ds.Tables[0].Rows[0][5].ToString();
            if (ds.Tables[0].Rows[0]["SMWDAAA024"].ToString().Equals("Y"))
            {
                isShowFlow = true;
            }
            else
            {
                isShowFlow = false;
            }

            //取得發起流程資訊
            com.dsc.flow.data.SubmitInfo info = new com.dsc.flow.data.SubmitInfo();
            info.fillerID = fillerID;
            info.fillerName = getUserName(engine, fillerID);
            info.fillerOrgID = fillerOrgID;
            info.fillerOrgName = getUnitName(engine, fillerOrgID);
            info.ownerID = ownerID;
            info.ownerName = getUserName(engine, ownerID);
            info.ownerOrgID = ownerOrgID;
            info.ownerOrgName = getUnitName(engine, ownerOrgID);
            info.submitOrgID = submitOrgID;
            info.objectGUID = currentObject.getData("GUID");
            info.important = importance;


            //取得流程預設參數
            Hashtable paramTemp = getProcessParameter(engine, ds.Tables[0].Rows[0][7].ToString());
            Hashtable param = new Hashtable();

            //取得流程參數--這裡要補上將flowXML建成param(hashtable)的程序
            //string firstParam = setFlowVariables(engine, param, currentObject);

            if ((firstParam != null) && (!firstParam.Equals("")) && (flowParameter != null) && (!flowParameter.Equals("")))
            {
                XMLProcessor xpn = new XMLProcessor(flowParameter, 1);
                IDictionaryEnumerator ie = paramTemp.GetEnumerator();
               
                while (ie.MoveNext())
                {
                    string key = (string)ie.Key;

                    XmlNode xn = xpn.selectSingleNode("//" + key);
                    if (xn != null)
                    {
                        param.Add(key, xn.InnerXml);
                    }
                    else
                    {
                        param.Add(key, "");
                    }
                }
            }

            //發起流程--這裡要改掉
            string setFlowXml = "";
            string IsSetFlow = "";

            if (!addSignXML.Equals(""))
            {
                setFlowXml = addSignXML;
                IsSetFlow = "Y";
            }

            if ((firstParam == null) || (firstParam.Equals("")))
            {
                //沒有設定任何參數
                //要判斷是否有加簽
                if (IsSetFlow.Equals("Y"))
                {
                    flowOID = invokeProcessByFormParameterAndAddCusAct(engine, info, flowid, "", "", subject, setFlowXml, debugPage);
                }
                else
                {
                    flowOID = invokeProcess(engine, info, flowid, subject, debugPage);
                }
            }
            else
            {
                //有參數, 要判定是否為表單參數
                bool isFormParam = false;
                string formOID = "";
                sql = "select SMWBAAB007, SMWBAAB008 from SMWAAAA inner join SMWDAAA on SMWAAAA001=SMWDAAA005 inner join SMWBAAA on SMWBAAA004=SMWDAAA003 inner join SMWBAAB on SMWBAAB002=SMWBAAA001 where SMWAAAA002='" + ProcessPageID + "' and SMWDAAA006='Init' and SMWBAAB003='" + firstParam + "'";
                DataSet dtest = engine.getDataSet(sql, "TEMP");
                if (dtest.Tables[0].Rows.Count > 0)
                {
                    if (dtest.Tables[0].Rows[0][0].ToString().Equals("Y"))
                    {
                        isFormParam = true;
                        formOID = dtest.Tables[0].Rows[0][1].ToString();
                    }
                }
                if (isFormParam)
                {
                    //判斷是否有加簽
                    if (IsSetFlow.Equals("Y"))
                    {
                        string paramValue = (string)param[firstParam];
                        flowOID = invokeProcessByFormParameterAndAddCusAct(engine, info, flowid, formOID, paramValue, subject, setFlowXml, debugPage);
                    }
                    else
                    {
                        string paramValue = (string)param[firstParam];
                        flowOID = invokeProcessByFormParameter(engine, info, flowid, formOID, paramValue, subject, debugPage);
                    }
                }
                else
                {
                    //判斷是否有加簽
                    if (IsSetFlow.Equals("Y"))
                    {
                        //throw new Exception("發起流程時若有設定流程且送出參數, 不可為流程變數");
                        throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError8", "發起流程時若有設定流程且送出參數, 不可為流程變數"));
                    }
                    else
                    {
                        string paramValue = (string)param[firstParam];
                        flowOID = invokeProcessByParameter(engine, info, flowid, firstParam, paramValue, subject, debugPage);
                    }
                }
            }

            //取得單號
            sheetno = sheetNo;

            if (sheetno.Equals(""))
            {
                Hashtable sheetNoParam = new Hashtable();
                //這裡要將sheetnoparam還原成sheetNoParam
                if ((sheetnoparam != null) && (!sheetnoparam.Equals("")))
                {
                    //使用時僅需傳入   <a>1</a><b>2</b> 這邊要處理最外層元素(增加)
                    sheetnoparam = "<doc>" + sheetnoparam + "</doc>";
                    XMLProcessor xpn = new XMLProcessor(sheetnoparam, 1);
                    foreach (XmlNode xn in xpn.doc.ChildNodes[0].ChildNodes)
                    {
                        sheetNoParam.Add(xn.Name, xn.InnerXml);

                    }
                }
                sheetno = getSheetNoProcedure(engine, ds.Tables[0].Rows[0][3].ToString(), sheetNoParam);
            }


            //寫入原稿資料夾
            Hashtable hs = new Hashtable();
            sql = "select * from SMWGAAA";
            DataSet oriFormVar = engine.getDataSet(sql, "TEMP");
            for (int i = 0; i < oriFormVar.Tables[0].Rows.Count; i++)
            {
                hs.Add(oriFormVar.Tables[0].Rows[i]["SMWGAAA003"].ToString(), "");
            }

            sql = "select * from SMWYAAA where (1=2)";
            DataSet oriy = engine.getDataSet(sql, "TEMP");
            DataRow dr = oriy.Tables[0].NewRow();
            dr["SMWYAAA001"] = IDProcessor.getID("");
            dr["SMWYAAA002"] = sheetno;
            dr["SMWYAAA003"] = flowid;
            dr["SMWYAAA004"] = flowname;
            dr["SMWYAAA005"] = flowOID;
            dr["SMWYAAA006"] = subject;
            dr["SMWYAAA007"] = info.important;
            dr["SMWYAAA008"] = info.fillerID;
            dr["SMWYAAA009"] = info.fillerName;
            dr["SMWYAAA010"] = info.fillerOrgID;
            dr["SMWYAAA011"] = info.fillerOrgName;
            dr["SMWYAAA012"] = info.ownerID;
            dr["SMWYAAA013"] = info.ownerName;
            dr["SMWYAAA014"] = info.ownerOrgID;
            dr["SMWYAAA015"] = info.ownerOrgName;
            dr["SMWYAAA016"] = info.submitOrgID;
            dr["SMWYAAA017"] = DateTimeUtility.getSystemTime2(null);
            dr["SMWYAAA018"] = wa001;
            dr["SMWYAAA019"] = info.objectGUID;
            dr["SMWYAAA020"] = "I";
            dr["SMWYAAA022"] = "N";

            for (int i = 1; i <= 20; i++)
            {
                string tag = "SMWYAAA1" + string.Format("{0:00}", i);
                string varName = "";
                bool hasFound = false;
                for (int j = 0; j < oriFormVar.Tables[0].Rows.Count; j++)
                {
                    if (oriFormVar.Tables[0].Rows[j]["SMWGAAA002"].ToString().Equals(tag))
                    {
                        hasFound = true;
                        varName = oriFormVar.Tables[0].Rows[j]["SMWGAAA003"].ToString();
                        break;
                    }
                }
                if (hasFound)
                {
                    string vle = "";
                    IDictionaryEnumerator ie = hs.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        string key = (string)ie.Key;
                        string vl = (string)ie.Value;
                        if (key.Equals(varName))
                        {
                            vle = vl;
                            break;
                        }
                    }
                    dr[tag] = vle;
                }
                else
                {
                    dr[tag] = "";
                }
            }
            dr["D_INSERTUSER"] = (string)Session["UserGUID"];
            dr["D_INSERTTIME"] = DateTimeUtility.getSystemTime2(null);
            dr["D_MODIFYUSER"] = "";
            dr["D_MODIFYTIME"] = "";

            oriy.Tables[0].Rows.Add(dr);
            if (!engine.updateDataSet(oriy))
            {
                throw new Exception(engine.errorString.Replace("\n", "\\n"));
            }

            //--這裡要將資料存入資料庫
            //saveDB(engine, currentObject, oriObject, UIStatus);
            if (!agent.updateFormMode())
            {
                throw new Exception(engine.errorString);
            }


            //處理DATARELATION

            sql = "select SMWAAAA001, SMWAAAA030 from SMWAAAA where SMWAAAA002='" + ProcessPageID + "'";
            DataSet ds2 = engine.getDataSet(sql, "TEMP");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                //throw new Exception("找不到此作業畫面設定檔");
                throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError13", "找不到此作業畫面設定檔"));
            }

            string processGUID = ds2.Tables[0].Rows[0][0].ToString();

            //新增
            sql = "insert into DATARELATION(GUID, LASTGUID, CURRENTGUID, PROCESSGUID, FLOWGUID, FLOWID, AGENTSCHEMA, DATA_STATUS, UPDATEUSER, UPDATETIME) values(";
            sql += "'" + IDProcessor.getID("") + "',";
            sql += "'',";
            sql += "'" + currentObject.getData("GUID") + "',";
            sql += "'" + processGUID + "',";
            sql += "'" + flowOID + "',";
            sql += "'" + flowid + "',";
            sql += "'" + AgentSchema + "',";
            sql += "'R',";
            sql += "'" + userID + "',";
            sql += "'" + DateTimeUtility.getSystemTime2(null) + "')";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }

            engine.commit();
            engine.close();

            //儲存成功
            StringBuilder sb = new StringBuilder();
            sb.Append("<ReturnValue>");
            sb.Append("<Result>Y</Result>");
            sb.Append("<FlowOID>" + flowOID + "</FlowOID>");
            sb.Append("<SheetNo>" + sheetno + "</SheetNo>");
            sb.Append("</ReturnValue>");

            return sb.ToString();

        }
        catch (Exception ue)
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

            StringBuilder sb = new StringBuilder();
            sb.Append("<ReturnValue>");
            sb.Append("<Result>N</Result>");
            sb.Append("<Message>" + ue.Message + "</Message>");
            sb.Append("<StackTrace>" + ue.StackTrace + "</StackTrace>");
            sb.Append("</ReturnValue>");

            return sb.ToString();
        } 
    }

    /// <summary>
    ///// 簽核
    ///// </summary>
    //[WebMethod]
    //public void SignFlow()
    //{
    //    SysParam sp = new SysParam(engine);
    //    string flowType = getGPParam("FlowAdapter");
    //    string con1 = "http://127.0.0.1:8080/NaNaWeb/services/WorkflowService?wsdl"; //預設WebService使用之組件名稱
    //    string con2 = "http://127.0.0.1:8080/NaNaWeb/services/DotJIntegration?wsdl"; //保留WebService使用之組件名稱
    //    string account = "5110"; //登入帳號
    //    string password = "1234"; //登入密碼
    //    string Locale = ""; //登入語系
    //    string SenderIP = "127.0.0.1"; //發送者的IP
    //    string ReceiverIP = "127.0.0.1:8080"; //接收者的IP
    //    string EFSiteName = "EF2KWeb"; //EFSiteName
    //    string logPath = "D:\\ECP\\asd.txt";//紀錄Log的路徑
    //    bool debug = true;//是否要紀錄Log
        
    //    FlowFactory ff = new FlowFactory();
    //    AbstractFlowAdapter adp = ff.getAdapter(flowType);
    //    adp.retryTimes = 1;//執行失敗時重新執行的次數。預設為1(代表僅執行1次)
    //    adp.retryWaitingTime = 1000;//執行失敗後重新執行下一次前的等待時間，單位為毫秒。預設為1000毫秒(1秒)
    //    adp.init(con1, con2, account, password, Locale, SenderIP, ReceiverIP, EFSiteName, logPath, debug);

    //    //取得待辦資料
    //    com.dsc.flow.data.WorkItem[] wis = adp.fetchPerformableWorkItems("5110", "10", "1", "", "", "", "", "", "", "");
    //    string woid = wis[0].workItemOID;//woid為工作項目OID
        
    //    //簽核
    //    //adp.completeWorkItem("送出動作的使用者ID", "工作項目OID", "意見表達結果", "簽核意見");
    //    adp.completeWorkItem("5110", "36c32b94c59e100489884a1fdb0e9a55", "同意", "BBB");

    //    //登出
    //    adp.logout();
    //}  
    
      
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

}

