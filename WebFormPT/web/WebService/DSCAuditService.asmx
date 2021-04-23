<%@ WebService Language="C#" Class="DSCAuditService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.Data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using WebServerProject;
using com.dsc.flow.data;
using com.dsc.flow.server;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DSCAuditService  : System.Web.Services.WebService {


    [WebMethod]
    public string writeLog(string applicationID, string moduleID, string programID, string programName, int level, string message, string stacktrace, string userID, string userName, string IP, string userAgent)
    {
        
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine;

        try
        {
            engine = factory.getEngine(acs.engineType, acs.connectString);

            WebServerProject.Audit au = new Audit(engine);
            bool res=au.writeLog(applicationID, moduleID, programID, programName, level, message, stacktrace, userID, userName, IP, userAgent);
            
            engine.close();
            if (!res)
            {
                throw new Exception("寫入稽核模組時發生錯誤");
            }
            return "";
        }
        catch (Exception te)
        {
            return te.Message;
        }
    }


}

