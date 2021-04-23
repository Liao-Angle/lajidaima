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

public partial class DSCWebControlRunTime_DSCWebControlUI_RoutineWizard_MonthMode :BaseWebUI.GeneralWebPage
{
    protected override void OnPreRenderComplete(EventArgs e)
    {
        Dsclabel4.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string31", "這個工作的執行方式：");
        DAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string46", "日期：");
        WEEK.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string47", "星期：");
        LASTDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string48", "最後一日");
        LASTWEEKDAY.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string49", "最後一週間日");
        DSCLabel3.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string50", "月份：");

        M01.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string51", "一月");
        M02.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string52", "二月");
        M03.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string53", "三月");
        M04.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string54", "四月");
        M05.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string55", "五月");
        M06.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string56", "六月");
        M07.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string57", "七月");
        M08.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string58", "八月");
        M09.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string59", "九月");
        M10.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string60", "十月");
        M11.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string61", "十一月");
        M12.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string62", "十二月");

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
                string[,] ids = new string[31, 2];
                for (int i = 1; i <= 31; i++)
                {
                    ids[i - 1, 0] = i.ToString();
                    ids[i - 1, 1] = i.ToString();
                }
                DAYS.setListItem(ids);

                ids = new string[,]{
                    {"1",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string40", "第一個")},
                    {"2",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string41", "第二個")},
                    {"3",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string42", "第三個")},
                    {"4",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string43", "第四個")},
                    {"5",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string44", "第五個")}
                };
                ORDERS.setListItem(ids);

                ids = new string[,]{
                    {"0",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string17", "星期日")},
                    {"1",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string18", "星期一")},
                    {"2",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string19", "星期二")},
                    {"3",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string20", "星期三")},
                    {"4",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string21", "星期四")},
                    {"5",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string22", "星期五")},
                    {"6",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string23", "星期六")}
                };
                WEEKDAY.setListItem(ids);

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
                    DAY.Checked = true;
                    DAYS.ValueText = xn.Attributes["DAYS"].Value;
                }
                else if (xn.Attributes["MODE"].Value.Equals("1"))
                {
                    WEEK.Checked = true;
                    ORDERS.ValueText = xn.Attributes["ORDERS"].Value;
                    WEEKDAY.ValueText = xn.Attributes["WEEKDAY"].Value;
                }
                else if (xn.Attributes["MODE"].Value.Equals("2"))
                {
                    LASTDAY.Checked = true;
                }
                else
                {
                    LASTWEEKDAY.Checked = true;
                }

                int wd = int.Parse(xp.selectSingleNodeText(@"/ROUTINEWIZARD/RUNMODE/MONTHS"));
                for (int i = 1; i <= 12; i++)
                {
                    string tag = string.Format("M{0:00}", i);
                    int checkO = (int)Math.Pow(2, i - 1);

                    DSCWebControl.DSCCheckBox cb = (DSCWebControl.DSCCheckBox)this.FindControl(tag);
                    if ((wd & checkO) > 0)
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        cb.Checked = false;
                    }
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

        if (DAY.Checked)
        {
            xn.Attributes["MODE"].Value = "0";
            xn.Attributes["DAYS"].Value = DAYS.ValueText;
        }
        else if (WEEK.Checked)
        {
            xn.Attributes["MODE"].Value = "1";
            xn.Attributes["ORDERS"].Value = ORDERS.ValueText;
            xn.Attributes["WEEKDAY"].Value = WEEKDAY.ValueText;
        }
        else if (LASTDAY.Checked)
        {
            xn.Attributes["MODE"].Value = "2";
        }
        else
        {
            xn.Attributes["MODE"].Value = "3";
        }

        int wd = 0;
        for (int i = 1; i <= 12; i++)
        {
            string tag = string.Format("M{0:00}", i);
            DSCWebControl.DSCCheckBox cb = (DSCWebControl.DSCCheckBox)this.FindControl(tag);
            if (cb.Checked)
            {
                wd += (int)Math.Pow(2, i - 1);
            }
        }
        xp.selectSingleNode(@"/ROUTINEWIZARD/RUNMODE/MONTHS").InnerText = wd.ToString();
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
        int wd = 0;
        for (int i = 1; i <= 12; i++)
        {
            string tag = string.Format("M{0:00}", i);
            DSCWebControl.DSCCheckBox cb = (DSCWebControl.DSCCheckBox)this.FindControl(tag);
            if (cb.Checked)
            {
                wd += (int)Math.Pow(2, i - 1);
            }
        }
        if (wd == 0)
        {
            MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "RoutineWizard", "string45", "請至少勾選一月份"));
            return;
        }
        saveXML();
        string url = "SelectTime.aspx?fromMode=2&dataID=" + (string)getSession("dataID") + "&clientID=" + (string)getSession("clientID");
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
