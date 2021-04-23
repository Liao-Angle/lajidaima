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
using com.dsc.flow.server;
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;


/// <summary>
/// BasicFormPage 的摘要描述
/// </summary>
public class SmpAdFormPage : SmpBasicFormPage
{
    /// <summary>
    /// 取得鼎新系統資料庫連線
    /// </summary>
    /// <returns></returns>
    protected AbstractEngine getErpEngine()
    {
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam("WorkFlowERPDB");
            engine.close();
            erpEngine = factory.getEngine(EngineConstants.SQL, connStr);
        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
        }
        finally
        {
            engine.close();
        }
        return erpEngine;
    }

    /// <summary>
    /// 取得鼎新系統資料庫連線
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    protected AbstractEngine getErpEngine(string companyId)
    {
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam(companyId + "WorkFlowERPDB");
            engine.close();
            erpEngine = factory.getEngine(EngineConstants.SQL, connStr);
        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
        }
        finally
        {
            engine.close();
        }
        return erpEngine;
    }

    protected string getDispatchedTitle(string companyId, string userId)
    {
        AbstractEngine engine = getErpEngine(companyId);
        string sql = "SELECT ChinaTitle FROM SMP_DISPATCHED_TITLE where Id = '" + userId + "'";
        string chinaTitle = (string)engine.executeScalar(sql);
        engine.close();
        return chinaTitle;
    }
}
