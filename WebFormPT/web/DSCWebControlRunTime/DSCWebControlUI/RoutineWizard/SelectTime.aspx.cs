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

public partial class DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_SelectTime : BaseWebUI.GeneralWebPage
{
    protected override void OnPreRenderComplete(EventArgs e)
    {
        DSCLabel1.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string66", "請選擇工作開始的時間及日期：");
        SINGLETIME.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string67", "指定時間：");
        HOURTIME.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string68", "每整點執行");
        HALFTIME.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string69", "每半小時執行");
        PrevPage.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string37", "上一步");
        CancelButton.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string39", "取消");

        base.OnPreRenderComplete(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string tmpfromMode = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["fromMode"]);
                setSession("fromMode", tmpfromMode);

                string dataID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Page.Request.QueryString["dataID"]);
                setSession("dataID", dataID);
                string clientID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Page.Request.QueryString["clientID"]);
                setSession("clientID", clientID);

                //初始化畫面
                string xml = (string)Session[dataID + "_" + clientID + "_tempValueText"];
                XMLProcessor xp = new XMLProcessor(xml, 1);

                XmlNode xn = xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/STARTTIME");
                if (xn.Attributes["MODE"].Value.Equals("0"))
                {
                    SINGLETIME.Checked = true;
                    STARTTIME.ValueText = xn.Attributes["TIME"].Value;
                }
                else if (xn.Attributes["MODE"].Value.Equals("1"))
                {
                    HOURTIME.Checked = true;
                }
                else
                {
                    HALFTIME.Checked = true;
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
        XmlNode xn = xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/STARTTIME");

        if (SINGLETIME.Checked)
        {
            xn.Attributes["MODE"].Value = "0";
            if (xn.Attributes["TIME"] == null)
            {
                XmlAttribute dbid = xp.getXmlDocument().CreateAttribute("TIME");
                dbid.Value = "";
                xn.Attributes.Append(dbid);
            }
            xn.Attributes["TIME"].Value = STARTTIME.ValueText;
        }
        else if (HOURTIME.Checked)
        {
            xn.Attributes["MODE"].Value = "1";
        }
        else
        {
            xn.Attributes["MODE"].Value = "2";
        }

        Session[dataID + "_" + clientID + "_tempValueText"] = xp.getXmlString();
    }
    protected void PrevPage_Click(object sender, EventArgs e)
    {
        saveXML();
        string fromMode = (string)getSession("fromMode");

        string url = "";
        if (fromMode.Equals("0"))
        {
            url = "DayMode.aspx?dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
        }
        else if (fromMode.Equals("1"))
        {
            url = "WeekMode.aspx?dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
        }
        else
        {
            url = "MonthMode.aspx?dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
        }
        Response.Write("window.location.href='" + url + "';");

    }
    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        saveXML();
        com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
        switch (resultType)
        {
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE:
                Response.Write("window.top.close();");
                Response.Write("window.top.returnValue='1';");
                break;
            default:
                string js = "";
                js += "window.parent.parent.$('#ecpDialog').attr('ret', '1');";
                js += "window.parent.parent.$('#ecpDialog').dialog('close');  ";
                Response.Write(js);
                break;
        } 
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
