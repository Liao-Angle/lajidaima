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
using WebServerProject;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class Utility_GPAcceptor : System.Web.UI.Page
{
    string isDebugPage = "Y";
    protected void writeLog(Exception e)
    {
        com.dsc.kernal.factory.IOFactory factory = new com.dsc.kernal.factory.IOFactory();
        com.dsc.kernal.factory.AbstractEngine logEngine = null;
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();
        logEngine = factory.getEngine(acs.engineType, acs.connectString);

        WebServerProject.Audit au = new WebServerProject.Audit(logEngine);
        bool res = au.writeLog("", "", "", "", 5, e.Message, e.StackTrace, "GPAcceptor", "GPAcceptor", Request.ServerVariables["REMOTE_ADDR"], Request.ServerVariables["HTTP_USER_AGENT"]);
        if (logEngine != null)
        {
            logEngine.close();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(1500);
        AbstractEngine engine = null;
        string flowOID = Request.Form["SerialNo"];
        try
        {
            string result = Request.Form["Result"];
            if (flowOID == null)
            {
                flowOID = Request.QueryString["SerialNo"];
                result = Request.QueryString["Result"];
            }



            if (com.dsc.kernal.global.GlobalCache.getValue(flowOID) != null)
            {
                engine = (AbstractEngine)com.dsc.kernal.global.GlobalCache.getValue(flowOID);
            }
            else
            {
                IOFactory factory = new IOFactory();
                string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
                com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
                com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
                acs.getConnectionString();
                engine = factory.getEngine(acs.engineType, acs.connectString);
            }
            //engine.startTransaction(System.Data.IsolationLevel.ReadCommitted);

            string sql = "select SMVPAAA009 from SMVPAAA";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count == 0)
            {
                isDebugPage = "N";
            }
            else
            {
                isDebugPage = ds.Tables[0].Rows[0][0].ToString();
            }


            if (isDebugPage.Equals("Y"))
            {
                string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                fname = Server.MapPath("~/LogFolder/" + fname + "_callback.log");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);

                sw.WriteLine("***************");
                sw.WriteLine("SerialNo: " + flowOID);
                sw.WriteLine("result: " + result);
                sw.WriteLine("flowOID: " + flowOID);
                sw.WriteLine("Request.ApplicationPath: " + Request.ApplicationPath);
                sw.WriteLine("Server: " + Server);
                sw.Close();
            }

            DataOperate dot = new DataOperate(engine);
            dot.process(flowOID, result, Request.ApplicationPath, Server);

            if (com.dsc.kernal.global.GlobalCache.getValue(flowOID) == null)
            {
                engine.close();
            }
        }
        catch (Exception ze)
        {
            writeLog(ze);
            if (com.dsc.kernal.global.GlobalCache.getValue(flowOID) == null)
            {
                try
                {
                    //engine.rollback();
                    engine.close();
                }
                catch { };
            }
            if (isDebugPage.Equals("Y"))
            {
                string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                fname = Server.MapPath("~/LogFolder/" + fname + "_callback.log");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
                sw.WriteLine(DateTimeUtility.getSystemTime2(null) + "===============");
                sw.WriteLine(ze.StackTrace);
                sw.WriteLine(ze.Message);
                sw.Close();
            }
            Response.StatusCode = 500;
        }

    }
}
