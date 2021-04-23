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

public partial class Help_DefaultObjectMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        string outStr = "";

        Assembly asm = Assembly.LoadFile(Server.MapPath("~/Bin/WebServerProject.dll"));
        Type[] type = asm.GetTypes();
        type = sort(type);
        for (int i = 0; i < type.Length; i++)
        {
            if (type[i].BaseType.Name.Equals("DataObject"))
            {
                outStr += "<li><a href='DefaultObjectDetail.aspx?objectName="+type[i].FullName+"' target=Content>" + type[i].Name + "</a>";
            }
        }

        Literal1.Text = outStr;
    }
    private Type[] sort(Type[] ori)
    {
        for (int i = 0; i < (ori.Length-1); i++)
        {
            for (int j = i + 1; j < ori.Length; j++)
            {
                if (ori[j].Name.CompareTo(ori[i].Name) < 0)
                {
                    Type c = ori[i];
                    ori[i] = ori[j];
                    ori[j] = c;
                }
            }
        }
        return ori;
    }
}
