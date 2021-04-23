<%@ WebService Language="C#" Class="SmpTestWebService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.kernal.webservice;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SmpTestWebService  : System.Web.Services.WebService {

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public void WriteLog(string message)
    {
        System.IO.StreamWriter sw = new System.IO.StreamWriter(@"D:\ECP\WebFormPT\web\LogFolder\SmpTestWebService.txt", true);
        sw.WriteLine(message);
        sw.Close();
    } 
}