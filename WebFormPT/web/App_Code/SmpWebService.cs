using System;
using System.Web.Services;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

/// <summary>
/// Summary description for SmpWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SmpWebService : System.Web.Services.WebService {
    public const string ApplicationID = "SMPSERVICE";

    public SmpWebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public AbstractEngine getEngine()
    {
        AbstractEngine engine = null;
        try
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(acs.engineType, acs.connectString);
        }
        catch (Exception e)
        {

        }
        return engine;
    }
}
