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

public partial class DSCWebControlRunTime_RedirectDialog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string URL = "";

        string tURL=com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["URL"]);
        if ((tURL.Length > 7) && (tURL.ToUpper().Substring(0, 7).Equals("HTTP://")))
        {
            URL = tURL;
        }
        else if ((tURL.Length > 8) && (tURL.ToUpper().Substring(0, 8).Equals("HTTPS://")))
        {
            URL = tURL;
        }
        else
        {
            //國昌2011/07/08 解決ShowModalDialog在ModalDialog中開啟時的路徑少一層問題
            URL = ".." + tURL.Replace("\\", "/"); 
        }
        //下一行是為了解AppScan所掃到的錯誤，但寫了此行後開窗查詢欄位輸入值後按Enter會發生「找不到資源」的錯誤，故先mark暫不處理。
        //URL = System.Web.HttpUtility.UrlEncode(URL);
        string GetParam = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["GetParam"]);
        if (!GetParam.Equals(""))
        {
            URL += "?";
            URL+= com.dsc.kernal.utility.Utility.URLParameterEncode(GetParam);
        }
        ContentLiteral.Text = "<iframe src='" + URL + "' style=\"width:100%;height:100%;overflow: hidden; \"  frameborder=\"0\"></iframe>";        
    }
}
