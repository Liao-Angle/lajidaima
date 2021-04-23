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

public partial class DSCWebControlUI_SingleDateTimeWindow_Default : System.Web.UI.Page
{
    public string targetURL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        targetURL = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["url"]);
        string qs = "";
        string tmpurl = "";
        string[] keys = Request.QueryString.AllKeys;

        if (keys.Length > 1)
        {
            qs = "?";
            for (int i = 0; i < keys.Length; i++)
            {
                if (!keys[i].Equals("url"))
                {
                    tmpurl = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString[keys[i]]);
                    qs += keys[i] + "=" + Server.UrlEncode(tmpurl) + "&";
                }
            }
            qs = qs.Substring(0, qs.Length - 1);
        }
        targetURL += qs;        
        targetURL= targetURL.Replace("'","");
    }

}
