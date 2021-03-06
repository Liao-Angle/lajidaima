using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;


using WebServerProject;
using WebServerProject.auth;
using com.dsc.flow.server;
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.batch;

/// <summary>
/// BasicFormPage 的摘要描述
/// </summary>
public class SmpErpPoFormPage : SmpBasicPoFormPage
{
    #region
    protected LiteralControl SignCount;
    #endregion

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!base.isNew())
        {
            string tempUIStatus = fixNull(Request.QueryString["UIStatus"]);
            if (tempUIStatus.Equals(ProcessModify))
            {
                base.setSignMode("2");
            }
        }
    }

    /// <summary>
    /// 取得ERPPortal資料庫連線
    /// </summary>
    /// <returns></returns>
    protected OracleConnection getErpPortalConn()
    {
        OracleConnection conn = null;
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam("ERPPortalDB");
            engine.close();
            conn = new OracleConnection(connStr);
        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
        }
        finally
        {
            engine.close();
        }
        return conn;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    protected OracleConnection getOracleErpConn(AbstractEngine engine)
    {
        OracleConnection conn = null;
        try
        {
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam("OracleERPDB");
            conn = new OracleConnection(connStr);
        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
        }
        return conn;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="keyId"></param>
    /// <returns></returns>
    protected string checkErpData(AbstractEngine engine, int keyId)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = getOracleErpConn(engine);
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "smp_ecp_check_erp_data";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_key_id", OracleType.Number).Value = keyId;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            conn.Close();
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="serialNumber"></param>
    /// <param name="notificationId"></param>
    protected void createJob(AbstractEngine engine, string serialNumber, int notificationId)
    {
        WebServerProject.SysParam sps = new SysParam(engine);
        string ip = sps.getParam("QueueIP");
        string port = sps.getParam("QueuePort");
        string[][] usersbase = getGroupdUser(engine, "SPERPADM");
        string smpQueueUserId = usersbase[0][0];//sps.getParam("SmpQueueUserId");
        string token = "&&&&";
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        StringBuilder sb = new StringBuilder();
        string param =
            sb.Append(engineType).Append(token)
            .Append(connectString).Append(token)
            .Append(smpQueueUserId).Append(token)
            .Append(serialNumber).Append(token)
            .Append(notificationId).ToString();

        JobClient jc = new JobClient();
        jc.IP = ip;
        jc.port = int.Parse(port);

        jc.createJob("SmpQueue.CompleteWorkItem", param);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="keyId"></param>
    /// <param name="action"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    protected string responseNotification(AbstractEngine engine, int keyId, string action, string note)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = getOracleErpConn(engine);
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "smp_ecp_respond_notification";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_key_id", OracleType.Int32).Value = keyId;
            objCmd.Parameters.Add("p_action", OracleType.VarChar).Value = action;
            objCmd.Parameters.Add("p_approve_user_note", OracleType.VarChar).Value = note;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.Message);
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
        return result;
    }

    /// <summary>
    /// 取得簽核歷程
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="serialNumber"></param>
    /// <returns></returns>
    protected string getWorkflowOpinion(AbstractEngine engine, string serialNumber)
    {
        string opinion = "";
        try
        {
            string sql = "select wi.workItemName,u.id, u.userName,wi.executiveComment "
                + "from [NaNa].dbo.WorkItem wi, [NaNa].dbo.ProcessInstance pi, [WebFormPT].[dbo].SMWYAAA ya, [NaNa].dbo.Users u "
                + "where SMWYAAA005='" + serialNumber + "' and wi.contextOID=pi.contextOID and pi.serialNumber=ya.SMWYAAA005 and u.OID=wi.performerOID "
                + "and wi.workItemName != '填表人' "
                + "order by wi.createdTime asc";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                string actName = ds.Tables[0].Rows[i][0].ToString();
                string userId = ds.Tables[0].Rows[i][1].ToString();
                string userName = ds.Tables[0].Rows[i][2].ToString();
                string comment = ds.Tables[0].Rows[i][3].ToString();
                opinion += userId + "^" + comment + "^";
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        return opinion;
    }

    protected void checkJobQueueService()
    {
        try
        {
            //string serviceName = "ECP Job Queue Service";
            //ServiceController sc = new ServiceController(serviceName);
            //for (int i = 0; i < 10; i++)
            //{
            //    if ((sc.Status.Equals(ServiceControllerStatus.Stopped)) || (sc.Status.Equals(ServiceControllerStatus.StopPending)))
            //    {
            //        sc.Start();
            //        sc.Refresh();
            //    }
            //    else
            //    {
            //        break;
            //    }
            //    System.Threading.Thread.Sleep(1 * 1000);
            //}
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
    }
}
