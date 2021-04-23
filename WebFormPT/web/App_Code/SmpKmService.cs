using System;
using System.Net;
using System.Web.Services;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.mail;
using WebServerProject;

/// <summary>
/// Summary description for SmpKmService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SmpKmService : SmpWebService {
    const string ModuleID = "SPKM";
    const string ProcessPageID = "SmpKmService";
    const string ProgramName = "SmpKmService";
    const string UserId = "administrator";
    const string UserName = "系統管理員";

    public SmpKmService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="level"></param>
    /// <param name="message"></param>
    /// <param name="stackTrace"></param>
    public void writeLog(AbstractEngine engine, int level, string message, string stackTrace)
    {
        try
        {
            WebServerProject.Audit au = new Audit(engine);
            string hostName = Dns.GetHostName();
            string environment = this.GetType().ToString();
            bool res = au.writeLog(ApplicationID, ModuleID, ProcessPageID, ProgramName, level, message, stackTrace, UserId, UserName, hostName, environment);
        }
        catch (Exception ex)
        {
           
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="subject"></param>
    /// <param name="content"></param>
    public void sendMail(AbstractEngine engine, string subject, string content)
    {
        SysParam sp = new SysParam(engine);
        string mailclass = sp.getParam("MailClass");
        string SMTP_Server = sp.getParam("SMTP_Server");
        MailFactory fac = new MailFactory();
        AbstractMailUtility au = fac.getMailUtility(mailclass.Split(new char[] { '.' })[0], mailclass);
        string substAdr = GlobalProperty.getProperty("km", "email");
        string pSenderAddress = GlobalProperty.getProperty("simplo", "sendermail");
        au.sendMailHTML("", SMTP_Server, substAdr, "", pSenderAddress, subject, content);
    }
}
