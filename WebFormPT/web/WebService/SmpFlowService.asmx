<%@ WebService Language="C#" Class="SmpFlowService" %>

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
/// SmpFlowService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SmpFlowService : System.Web.Services.WebService
{

    public SmpFlowService()
    {
        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 簽核
    /// </summary>
    [WebMethod]
    public string signFlow(string userId, string serialNumber, string signProcess, string executiveComment)
    {
        string result = "";
        AbstractEngine engine = null;
        try
        {
            engine = getEngine();
            string sql = "select flowId, serialNumber, workItemOID from SmpNonSignList where signUserId='" + userId + "' and serialNumber='" + serialNumber + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string flowid = ds.Tables[0].Rows[0][0].ToString();
                string flowOID = ds.Tables[0].Rows[0][1].ToString();
                string workItemOID = ds.Tables[0].Rows[0][2].ToString();
                if (workItemOID != null)
                {
                    string executiveResult = "";
                    if (signProcess.Equals("Y"))
                    {
                        //completeWorkItem
                        executiveResult = "同意";
                        result = completeWorkItem(userId, engine, workItemOID, executiveResult, executiveComment);
                    }
                    else
                    {
                        //terminateProcess
                        executiveResult = "不同意";
                        terminateProcess(userId, engine, flowOID, executiveResult, executiveComment);
                    }
                }
            }
            else
            {
                result = "查無待簽核表單!";
            }
        }
        catch (Exception e)
        {
            result = e.StackTrace;
        }
        finally
        {
            if (engine != null) engine.close();
        }
        return result;
    }

    /// <summary>
    /// 退回重辦
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="sheetNo"></param>
    /// <param name="executiveComment"></param>
    /// <returns></returns>
    [WebMethod]
    public string rejectProcedure(string userId, string serialNumber, string backActID, string executiveComment, string reexecuteActivityType)
    {
        string result = "";
        AbstractEngine engine = null;
        try
        {
            engine = getEngine();

            string sql = "select flowId, serialNumber, workItemOID, creatorId from SmpNonSignList where signUserId='" + userId + "' and serialNumber='" + serialNumber + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string flowid = ds.Tables[0].Rows[0][0].ToString();
                string flowOID = ds.Tables[0].Rows[0][1].ToString();
                string workItemOID = ds.Tables[0].Rows[0][2].ToString();
                string creatorId = ds.Tables[0].Rows[0][3].ToString();
                string curActID = fetchActivityIDFromWorkItemOID(engine, workItemOID, flowid, userId);
                bool isBackCreator = false;
                //簽核程序
                if (backActID == null || backActID.Equals(""))
                {
                    BackActivity[] bary = getReexecutableActivity(engine, workItemOID);
                    if (bary.Length > 0)
                    {
                        int creatorIdx = bary.Length - 1;
                        backActID = bary[creatorIdx].actID; //填單關卡
                        isBackCreator = true;
                    }
                    else
                    {
                        return "查無退回關卡!";
                    }
                }
                //退回重辦之後選項: 0: 退回後逐級簽核, 1: 退回後跳回
                if (reexecuteActivityType == null || reexecuteActivityType.Equals(""))
                {
                    reexecuteActivityType = "0";
                }
                //退回重辦
                reexecuteActivity(engine, userId, flowOID, workItemOID, curActID, backActID, executiveComment, reexecuteActivityType);

                //退回填單, 作廢單據
                if (isBackCreator)
                {
                    string executiveResult = "不同意";
                    terminateProcess(creatorId, engine, flowOID, executiveResult, "");
                }
            }
            else
            {
                result = "查無待簽核表單!";
            }
        }
        catch (Exception e)
        {
            result = e.StackTrace;
        }
        finally
        {
            if (engine != null) engine.close();
        }
        return result;
    }

    /// <summary>
    /// 取得流程簽核歷程
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns></returns>
    [WebMethod]
    public String getWorkflowOpinion(string serialNumber)
    {
        AbstractEngine engine = null;
        String xml = "";
        try
        {
            engine = getEngine();
            xml = getWorkflowOpinion(engine, serialNumber);
        }
        catch (Exception e)
        {
            xml = e.StackTrace;
        }
        finally
        {
            if (engine != null) engine.close();
        }
        return xml;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private AbstractEngine getEngine()
    {
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(acs.engineType, acs.connectString);
        return engine;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    protected AbstractFlowAdapter getFlowAdapter(AbstractEngine engine)
    {
        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");
        bool debugPage = true;

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = 1; //執行失敗時重新執行的次數。預設為1(代表僅執行1次)
        adp.retryWaitingTime = 1000; //執行失敗後重新執行下一次前的等待時間，單位為毫秒。預設為1000毫秒(1秒)

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, debugPage);

        return adp;
    }

    /// <summary>
    /// 准
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="workItemOID">工作項目識別</param>
    /// <param name="executiveResult">簽核結果</param>
    /// <param name="executiveComment">簽核意見</param>
    protected string completeWorkItem(string userId, AbstractEngine engine, string workItemOID, string executiveResult, string executiveComment)
    {
        string result = "";
        try
        {
            AbstractFlowAdapter adp = getFlowAdapter(engine);
            adp.completeWorkItem(userId, workItemOID, executiveResult, executiveComment);
            adp.logout();
        }
        catch (Exception e)
        {
            result = e.StackTrace;
        }
        return result;
    }

    /// <summary>
    /// 不准
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="processSerialNumber">流程實例序號</param>
    /// <param name="executiveResult">簽核結果</param>
    /// <param name="executiveComment">簽核意見</param>
    protected void terminateProcess(string userId, AbstractEngine engine, string processSerialNumber, string executiveResult, string executiveComment)
    {
        AbstractFlowAdapter adp = getFlowAdapter(engine);
        adp.terminateProcess(userId, processSerialNumber, executiveResult, executiveComment);
        adp.logout();
    }
    
    /// <summary>
    /// 退回重辦
    /// </summary>
    /// <param name="engine">資料連線物件</param>
    /// <param name="userID">退回重辦使用者</param>
    /// <param name="processSerialNumber">流程實例序號</param>
    /// <param name="workItemOID">目前工作項目識別號</param>
    /// <param name="curActID">目前活動代號</param>
    /// <param name="actID">要退回的活動代號</param>
    /// <param name="executiveComment">簽核意見</param>
    /// <param name="reexecuteActivityType">0:退回重辦之後逐關簽核; 2:略過之前已經執行過的關卡</param>
    protected void reexecuteActivity(AbstractEngine engine, string userID, string processSerialNumber, string workItemOID, string curActID, string actID, string executiveComment, string reexecuteActivityType)
    {
        AbstractFlowAdapter adp = getFlowAdapter(engine);
        adp.reexecuteActivity(userID, processSerialNumber, workItemOID, curActID, actID, executiveComment, reexecuteActivityType);
        adp.logout();
    }

    /// <summary>
    /// 由工作項目識別號取得activityID
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="workItemOID">工作項目識別號</param>
    /// <returns>活動代號</returns>
    protected string fetchActivityIDFromWorkItemOID(AbstractEngine engine, string workItemOID, string processID, string userID)
    {
        AbstractFlowAdapter adp = getFlowAdapter(engine);
        string vle = adp.fetchActivityIDFromWorkItemOID(workItemOID, processID, userID);
        adp.logout();
        return vle;
    }

    /// <summary>
    /// 取得關卡陣列
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="workItemOID"></param>
    /// <returns></returns>
    protected BackActivity[] getReexecutableActivity(AbstractEngine engine, string workItemOID) 
    {
        AbstractFlowAdapter adp = getFlowAdapter(engine);
        BackActivity[] bary = adp.getReexecutableActivity(workItemOID);
        return bary;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="processSerialNumber"></param>
    /// <returns></returns>
    protected string getWorkflowOpinion(AbstractEngine engine, string processSerialNumber)
    {
        AbstractFlowAdapter adp = getFlowAdapter(engine);
        string opinionXML = adp.fetchFullProcInstanceWithSerialNoXML(processSerialNumber);
        adp.logout();
        return opinionXML;
    }
}

