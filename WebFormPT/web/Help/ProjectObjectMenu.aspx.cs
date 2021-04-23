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
using com.dsc.kernal.factory;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.Remoting;
using System.IO;
using com.dsc.kernal.utility;

public partial class Help_ProjectObjectMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        string outStr = "";

        string[] filed = getObjectFile(Server.MapPath("~/settingFiles/DataObjectFolder"));
        string[] files = new string[filed.Length];
        for (int i = 0; i < filed.Length; i++)
        {
            files[i] = com.dsc.kernal.utility.Utility.getFileName(filed[i]);
        }

        files = sort(files);

        for (int i = 0; i < files.Length; i++)
        {
            string[] dots = files[i].Split(new char[] { '.' });
            outStr += "<li><a href='ProjectObjectDetail.aspx?objectName=" + files[i] + "' target=Content>" + dots[dots.Length - 1] + "</a>";
        }
        Literal1.Text = outStr;
    }
    private string[] getObjectFile(string path)
    {
        string[] ret = new string[0];
        string[] subDir = Directory.GetDirectories(path);
        for (int i = 0; i < subDir.Length; i++)
        {
            string[] fs = getObjectFile(subDir[i]);
            string[] nret = new string[ret.Length + fs.Length];
            for (int x = 0; x < ret.Length; x++)
            {
                nret[x] = ret[x];
            }
            for (int x = 0; x < fs.Length; x++)
            {
                nret[x + ret.Length] = fs[x];
            }
            ret = nret;
        }
        string[] f = Directory.GetFiles(path);
        ArrayList ary = new ArrayList();
        for (int i = 0; i < f.Length; i++)
        {
            XMLProcessor xp = new XMLProcessor(f[i], 0);
            if (xp.selectSingleNode("DataObject") != null)
            {
                ary.Add(f[i]);
            }
        }
        string[] nf = new string[ret.Length + ary.Count];
        for (int x = 0; x < ret.Length; x++)
        {
            nf[x] = ret[x];
        }
        for (int x = 0; x < ary.Count; x++)
        {
            nf[x + ret.Length] = (string)ary[x];
        }
        return nf;
    }
    private string[] sort(string[] ori)
    {
        for (int i = 0; i < (ori.Length-1); i++)
        {
            for (int j = i + 1; j < ori.Length; j++)
            {
                if (ori[j].CompareTo(ori[i]) < 0)
                {
                    string c = ori[i];
                    ori[i] = ori[j];
                    ori[j] = c;
                }
            }
        }
        return ori;
    }
}
