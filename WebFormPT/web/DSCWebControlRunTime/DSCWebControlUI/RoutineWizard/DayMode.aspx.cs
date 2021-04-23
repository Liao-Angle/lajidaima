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

public partial class DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_DayMode : BaseWebUI.GeneralWebPage
{
    protected override void OnPreRenderComplete(EventArgs e)
    {
        Dsclabel4.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string31", "這個工作的執行方式：");
        EVERYDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string32", "每日");
        WEEKDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string33", "週間日");
        EVERYINTERVAL.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string34", "每隔");
        Dsclabel3.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string35", "天");
        DSCLabel5.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string36", "開始日期：");
        PrevPage.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string37", "上一步");
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
                string[,] ids = new string[365, 2];
                for (int i = 1; i <= 365; i++)
                {
                    ids[i - 1, 0] = i.ToString();
                    ids[i - 1, 1] = i.ToString();
                }
                DAYINTERVAL.setListItem(ids);

                string dataID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Page.Request.QueryString["dataID"]);
                setSession("dataID", dataID);
                string clientID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Page.Request.QueryString["clientID"]);
                setSession("clientID", clientID);

                //初始化畫面
                string xml = (string)Session[dataID + "_" + clientID + "_tempValueText"];
                XMLProcessor xp = new XMLProcessor(xml, 1);

                XmlNode xn = xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/EXECUTEMODE");
                if (xn.Attributes["MODE"].Value.Equals("0"))
                {
                    EVERYDAY.Checked = true;
                }
                else if (xn.Attributes["MODE"].Value.Equals("1"))
                {
                    WEEKDAY.Checked = true;
                }
                else
                {
                    EVERYINTERVAL.Checked = true;
                    DAYINTERVAL.ValueText = xn.Attributes["DAYINTERVAL"].Value;
                }

                STARTDATE.ValueText = xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/STARTDATE").InnerText;
            }
        }
    }
    private void saveXML()
    {
        string dataID = (string)getSession("dataID");
        string clientID = (string)getSession("clientID");
        string xml = (string)Session[dataID + "_" + clientID + "_tempValueText"];

        XMLProcessor xp = new XMLProcessor(xml, 1);
        XmlNode xn = xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/EXECUTEMODE");

        if (EVERYDAY.Checked)
        {
            xn.Attributes["MODE"].Value = "0";
        }
        else if (WEEKDAY.Checked)
        {
            xn.Attributes["MODE"].Value = "1";
        }
        else
        {
            xn.Attributes["MODE"].Value = "2";
            if (xn.Attributes["DAYINTERVAL"] == null)
            {
                XmlAttribute dbid = xp.getXmlDocument().CreateAttribute("DAYINTERVAL");
                dbid.Value = "";
                xn.Attributes.Append(dbid);
            }
            xn.Attributes["DAYINTERVAL"].Value = DAYINTERVAL.ValueText;
        }
        xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/STARTDATE").InnerText = STARTDATE.ValueText;

        Session[dataID + "_" + clientID + "_tempValueText"] = xp.getXmlString();
    }
    protected void PrevPage_Click(object sender, EventArgs e)
    {
        saveXML();
        string url = "SelectMode.aspx?dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
        Response.Write("window.location.href='" + url + "';");

    }
    protected void NextPage_Click(object sender, EventArgs e)
    {
        if (STARTDATE.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string30", "請選擇開始日期"));
            return;
        }

        saveXML();
        string url = "SelectTime.aspx?fromMode=0&dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
        Response.Write("window.location.href='" + url + "';");
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
