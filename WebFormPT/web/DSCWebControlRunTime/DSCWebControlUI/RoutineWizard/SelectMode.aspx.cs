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
using com.dsc.kernal.utility;
using System.Xml;

public partial class DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_SelectMode : BaseWebUI.GeneralWebPage
{
    protected override void OnPreRenderComplete(EventArgs e)
    {
        DSCLabel1.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string31", "這個工作的執行方式：");
        DAYMODE.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string63", "每日");
        WEEKMODE.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string64", "每週");
        MONTHMODE.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string65", "每月");
        NextPage.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string38", "下一步");
        CancelButton.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string39", "取消");

        base.OnPreRenderComplete(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string dataID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Page.Request.QueryString["dataID"]);
                string clientID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Page.Request.QueryString["clientID"]);
                setSession("clientID", clientID);
                setSession("dataID", dataID);

                //初始化畫面
                string xml = (string)Session[dataID+"_"+clientID+"_tempValueText"];
                if ((xml==null) || (xml.Equals("")))
                {
                    xml = "<ROUTINEWIZARD>";
                    xml += "  <RUNMODE MODE='0'>";
                    xml += "    <EXECUTEMODE MODE='0' DAYINTERVAL='1' DAYS='1' ORDERS='1' WEEKDAY='1' />";
                    xml += "    <STARTDATE>" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null).Substring(0, 10) + "</STARTDATE>";
                    xml += "    <WEEKINTERVAL>1</WEEKINTERVAL>";
                    xml += "    <WEEKDAY>1</WEEKDAY>";
                    xml += "    <MONTHS>1</MONTHS>";
                    xml += "    <STARTTIME MODE='0' TIME='01:00:00'></STARTTIME>";
                    xml += "  </RUNMODE>";
                    xml += "</ROUTINEWIZARD>";
                    Session[dataID + "_" + clientID + "_tempValueText"] = xml;
                }

                XMLProcessor xp = new XMLProcessor(xml, 1);
                XmlNode xn = xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE");
                string mode = xn.Attributes["MODE"].Value;

                if (mode.Equals("0"))
                {
                    DAYMODE.Checked = true;
                }
                else if (mode.Equals("1"))
                {
                    WEEKMODE.Checked = true;
                }
                else
                {
                    MONTHMODE.Checked = true;
                }
            }
        }
    }
    private void saveXML()
    {
        string dataID = (string)getSession("dataID");
        string clientID = (string)getSession("clientID");
        string xml = (string)Session[dataID + "_" + clientID + "_tempValueText"];

        XMLProcessor xp = new XMLProcessor(xml, 1);
        XmlNode xn = xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE");
        if (DAYMODE.Checked)
        {
            xn.Attributes["MODE"].Value = "0";
        }
        else if (WEEKMODE.Checked)
        {
            xn.Attributes["MODE"].Value = "1";
        }
        else
        {
            xn.Attributes["MODE"].Value = "2";
        }

        Session[dataID + "_" + clientID + "_tempValueText"] = xp.getXmlString();
    }
    protected void NextPage_Click(object sender, EventArgs e)
    {
        saveXML();
        string url = "";
        if (DAYMODE.Checked)
        {
            url = "DayMode.aspx?dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
        }
        else if (WEEKMODE.Checked)
        {
            url = "WeekMode.aspx?dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
        }
        else
        {
            url = "MonthMode.aspx?dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
        }
        Response.Write("window.location.href='"+url+"';");
    }
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
        switch (resultType)
        {
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE:
                Response.Write("window.top.close();");
                Response.Write("window.top.returnValue='0';");
                break;
            default:
                string js = "";
                js += "window.parent.parent.$('#ecpDialog').attr('ret', '0');";
                js += "window.parent.parent.$('#ecpDialog').dialog('close');  ";
                Response.Write(js);
                break;
        } 
    }
}
