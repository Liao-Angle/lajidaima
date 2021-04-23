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
using System.Threading;

public partial class DSCWebControlRunTime_GetConfirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = -1;
        Response.Cache.SetExpires(DateTime.Now);

        string sid = Request.Form["SID"];
        string tempPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
        string messagePath = tempPath + "WebFormBasePageConfirm" + sid;
        string answerPath = tempPath + "WebFormBasePageConfirmAnswer" + sid;
        if ((Request.Form["ANS"] != null) && (!Request.Form["ANS"].Equals("")))
        {
            try
            {
                System.IO.File.Delete(messagePath);
            }
            catch { };
            System.IO.StreamWriter sw = new System.IO.StreamWriter(answerPath, false);
            sw.WriteLine(Request.Form["ANS"]);
            sw.Close();

        }
        else
        {
            if (System.IO.File.Exists(messagePath))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(messagePath);
                string msg = sr.ReadToEnd();
                sr.Close();

                try
                {
                    System.IO.File.Delete(messagePath);
                }
                catch { };
                Response.Write(msg);
            }
            else
            {
                Response.Write("");

            }
        }
    }



}
