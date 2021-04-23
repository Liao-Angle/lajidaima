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
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class closeEngine : System.Web.UI.Page
{
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
        bool res = au.writeLog("", "", "", "", 5, e.Message, e.StackTrace, "CloseEngine", "CloseEngine", Request.ServerVariables["REMOTE_ADDR"], Request.ServerVariables["HTTP_USER_AGENT"]);
        if (logEngine != null)
        {
            logEngine.close();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        com.dsc.kernal.factory.AbstractEngine engine = null;
        engine = (com.dsc.kernal.factory.AbstractEngine)getSession("progressEngine");
        if (engine != null) {
            try
            {           
                engine.rollback();                                                         
            }
            catch (Exception ue) { }
            try 
            {
                engine.close();                
            }               
            catch (Exception ue)
            {
                writeLog(ue);
            }
        }
    }
    protected string PageUniqueID
    {
        get
        {
            this.EnsureChildControls();
            return Request.QueryString ["pageUniqueID"];
        }
    }
    protected object getSession(string sessionName)
    {
        return getSession(PageUniqueID, sessionName);
    }
    protected object getSession(string pageUniqueID, string sessionName)
    {
        string ptag = pageUniqueID + "_" + sessionName;
        return Session[ptag];
    }
    protected void setSession(string sessionName, object value)
    {
        setSession(PageUniqueID, sessionName, value);
    }
    protected void setSession(string pageUniqueID, string sessionName, object value)
    {
        string ptag = pageUniqueID + "_" + sessionName;
        Session[ptag] = value;
    }
}
