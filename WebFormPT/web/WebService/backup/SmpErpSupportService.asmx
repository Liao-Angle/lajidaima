<%@ WebService Language="C#" Class="SmpErpSupportService" %>

using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.OracleClient;
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
/// 
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SmpErpSupportService  : System.Web.Services.WebService {

    public SmpErpSupportService()
    {
        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }
    
    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyId"></param>
    /// <param name="action"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    [WebMethod]
    public string responseNotification(int keyId, string action, string note)
    {
        string result = "";
        AbstractEngine engine = null;
        OracleConnection conn = null;
        try
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(acs.engineType, acs.connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = Convert.ToString(engine.executeScalar("select SMVHAAA004 from SMVHAAA where SMVHAAA002='OracleERPDB'"));
            engine.close();
            conn = new OracleConnection(connStr);

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "smp_ecp_respond_notification";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_key_id", OracleType.VarChar).Value = keyId;
            objCmd.Parameters.Add("p_action", OracleType.VarChar).Value = action;
            objCmd.Parameters.Add("p_approve_user_note", OracleType.VarChar).Value = note;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            result = e.StackTrace;
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
    /// <param name="number"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    [WebMethod]
    public string approvePr(string number, string note)
    {
        string result = "";
        AbstractEngine engine = null;
        OracleConnection conn = null;
        try
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();
            
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(acs.engineType, acs.connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = Convert.ToString(engine.executeScalar("select SMVHAAA004 from SMVHAAA where SMVHAAA002='ERPPortalDB'"));
            engine.close();
            conn = new OracleConnection(connStr);
            
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "SMP_IMPORT_PR.PR_APPROVE";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_PR_NUMBER", OracleType.VarChar).Value = number;
            objCmd.Parameters.Add("P_NOTE", OracleType.VarChar).Value = note;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            result = e.StackTrace;
        }
        finally
        {
            conn.Close();
        }
        return result;
    }
}