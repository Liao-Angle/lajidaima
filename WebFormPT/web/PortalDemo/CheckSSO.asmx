<%@ WebService Language="C#" Class="CheckSSO" %>

using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// CheckSSO 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CheckSSO : System.Web.Services.WebService
{

    public CheckSSO()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string checkSSO(string SSOToken)
    {
        string res = "N";
        try
        {
            string folder = Server.MapPath("~/tempFolder/" + SSOToken);
            if (System.IO.File.Exists(folder))
            {
                res = "Y";
            }
            else
            {
                res = "N";
            }
        }
        catch (Exception te)
        {
            res = "N";
        }
        return res;
    }

}

