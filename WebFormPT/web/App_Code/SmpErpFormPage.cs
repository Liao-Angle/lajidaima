using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Runtime.InteropServices;
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

/// <summary>
/// BasicFormPage 的摘要描述
/// </summary>
public class SmpErpFormPage : SmpBasicFormPage
{
    /// <summary>
    /// 取得ERPPortal資料庫連線
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    protected AbstractEngine getErpPortalEngine(AbstractEngine engine)
    {
        AbstractEngine engineErpPortal = null;
        try
        {
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam("ERPPortalDB");
            IOFactory factory = new IOFactory();
            engineErpPortal = factory.getEngine(EngineConstants.ORACLE, connStr);
        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
        }
        return engineErpPortal;
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
    /// 取得使用者資訊, [0]: id, [1]: userName, [2]: orgId, [3]: titleId, [4]: titleName, [5]: deptOID
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userGUID"></param>
    /// <returns>string[]</returns>
    protected string[] getUserInfo(AbstractEngine engine, string userGUID)
    {
        string sql = "select empNumber, empName, orgId, titleId, titleName, deptOID from EmployeeInfoAllDept where empGUID='" + Utility.filter(userGUID) + "' and isMain='1'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[6];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
            result[3] = ds.Tables[0].Rows[0][3].ToString();
            result[4] = ds.Tables[0].Rows[0][4].ToString();
			result[5] = ds.Tables[0].Rows[0][5].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "";
            result[4] = "";
			result[5] = "";
        }

        return result;
    }
	
	
	/// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="keyId"></param>
    /// <param name="action"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    protected string update_om(AbstractEngine engine, int keyId, string action, string note)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = getOracleErpConn(engine);
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "smp_update_om_issue_header";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_header_id", OracleType.Int32).Value = keyId;
            objCmd.Parameters.Add("p_approved_status", OracleType.VarChar).Value = action;
            objCmd.Parameters.Add("p_action_history", OracleType.VarChar).Value = note;
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
}
