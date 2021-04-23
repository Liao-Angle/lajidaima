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

public partial class DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_WeekMode : BaseWebUI.GeneralWebPage
{
    protected override void OnPreRenderComplete(EventArgs e)
    {
        Dsclabel4.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string31", "這個工作的執行方式：");
        DSCLabel1.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string34", "每隔");
        DSCLabel2.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string71", "週");
        DSCLabel3.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string72", "請由以下選取工作日期：");
        SUNDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string17", "星期日");
        MONDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string18", "星期一");
        TUESDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string19", "星期二");
        WEDNESDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string20", "星期三");
        THURSDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string21", "星期四");
        FRIDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string22", "星期五");
        SATURDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string23", "星期六");

        
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
                string[,] ids = new string[52, 2];
                for (int i = 1; i <= 52; i++)
                {
                    ids[i - 1, 0] = i.ToString();
                    ids[i - 1, 1] = i.ToString();
                }
                WEEKINTERVAL.setListItem(ids);

                string dataID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Page.Request.QueryString["dataID"]);
                setSession("dataID", dataID);
                string clientID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Page.Request.QueryString["clientID"]);
                setSession("clientID", clientID);

                //初始化畫面
                string xml = (string)Session[dataID + "_" + clientID + "_tempValueText"];
                XMLProcessor xp = new XMLProcessor(xml, 1);

                WEEKINTERVAL.ValueText = xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/WEEKINTERVAL").InnerText;
                int wd = int.Parse(xp.selectSingleNodeText(@"/ROUTINEWIZARD/RUNMODE/WEEKDAY"));
                if ((wd & 1) > 0)
                {
                    SUNDAY.Checked = true;
                }
                else
                {
                    SUNDAY.Checked = false;
                }
                if ((wd & 2) > 0)
                {
                    MONDAY.Checked = true;
                }
                else
                {
                    MONDAY.Checked = false;
                }
                if ((wd & 4) > 0)
                {
                    TUESDAY.Checked = true;
                }
                else
                {
                    TUESDAY.Checked = false;
                }
                if ((wd & 8) > 0)
                {
                    WEDNESDAY.Checked = true;
                }
                else
                {
                    WEDNESDAY.Checked = false;
                }
                if ((wd & 16) > 0)
                {
                    THURSDAY.Checked = true;
                }
                else
                {
                    THURSDAY.Checked = false;
                }
                if ((wd & 32) > 0)
                {
                    FRIDAY.Checked = true;
                }
                else
                {
                    FRIDAY.Checked = false;
                }
                if ((wd & 64) > 0)
                {
                    SATURDAY.Checked = true;
                }
                else
                {
                    SATURDAY.Checked = false;
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
        xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/WEEKINTERVAL").InnerText = WEEKINTERVAL.ValueText;
        int dt = 0;
        if (SUNDAY.Checked) dt += 1;
        if (MONDAY.Checked) dt += 2;
        if (TUESDAY.Checked) dt += 4;
        if (WEDNESDAY.Checked) dt += 8;
        if (THURSDAY.Checked) dt += 16;
        if (FRIDAY.Checked) dt += 32;
        if (SATURDAY.Checked) dt += 64;
        xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/WEEKDAY").InnerText = dt.ToString();

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
        int dt = 0;
        if (SUNDAY.Checked) dt += 1;
        if (MONDAY.Checked) dt += 2;
        if (TUESDAY.Checked) dt += 4;
        if (WEDNESDAY.Checked) dt += 8;
        if (THURSDAY.Checked) dt += 16;
        if (FRIDAY.Checked) dt += 32;
        if (SATURDAY.Checked) dt += 64;
        if (dt == 0)
        {
            MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string70", "請至少勾選一日期"));
            return;
        }
        saveXML();
        string url = "SelectTime.aspx?fromMode=1&dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
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
