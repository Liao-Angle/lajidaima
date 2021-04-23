<%@ WebService Language="C#" Class="FlowGPMail" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using WebServerProject;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.Collections;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FlowGPMail  : System.Web.Services.WebService {

    [WebMethod]
    public string sendMail(String pUserId, String pSubject, String pMessage, String pAddress, String pSenderAddress)
    {
        AbstractEngine engine=null;

        try
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(acs.engineType, acs.connectString);

            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"D:\ASPNET平台\mail.txt", true);
            sw.WriteLine("**************");
            sw.WriteLine(pUserId);
            sw.WriteLine(pSubject);
            sw.WriteLine(pMessage);
            sw.WriteLine(pAddress);
            sw.WriteLine(pSenderAddress);
            sw.Close();

            engine.close();

            return "";
        }
        catch (Exception ze)
        {
            try
            {
                engine.close();
            }
            catch { };
            return ze.StackTrace;
        }
    }    
}

