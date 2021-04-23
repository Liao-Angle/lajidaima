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
using System.Xml;
using com.dsc.kernal.utility;

public partial class FrameEnterprise_SysInfo : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string serverI = "";
                serverI = "<table width=100% border=0 cellspacing=0 id='FormHeadTable1' cellpadding=2 style='border-left-style:solid;border-top-style:solid;border-width:1px' class=FormHeadBorder>";
                serverI += "<tr>";
                serverI += "<td width=150px style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionHead>"+com.dsc.locale.LocaleString.getMainFrameLocaleString("SysInfo.aspx.language.ini", "global", "string001", "主機位置")+"</td>";
                serverI += "<td style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionDetail>" + Request.ServerVariables["LOCAL_ADDR"] + "</td>";
                serverI += "</tr>";
                serverI += "<tr>";
                serverI += "<td width=150px style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionHead>" + com.dsc.locale.LocaleString.getMainFrameLocaleString("SysInfo.aspx.language.ini", "global", "string002", "主機名稱") + "</td>";
                serverI += "<td style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionDetail>" + Request.ServerVariables["SERVER_NAME"] + "</td>";
                serverI += "</tr>";
                serverI += "</table>";
                ServerInfo.Text = serverI;

                XMLProcessor xp = new XMLProcessor(Server.MapPath("~/SettingFiles/Version.xml"), 0);
                XmlNodeList xnl = xp.selectAllNodes(@"/VersionRoot/Modules/Module");
                string html = "<table width=100% border=0 cellspacing=0 id='FormHeadTable2' cellpadding=2 style='border-left-style:solid;border-top-style:solid;border-width:1px' class=FormHeadBorder>";
                html += "<tr>";
                html += "<td style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionHead>" + com.dsc.locale.LocaleString.getMainFrameLocaleString("SysInfo.aspx.language.ini", "global", "string003", "模組代號") + "</td>";
                html += "<td style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionHead>" + com.dsc.locale.LocaleString.getMainFrameLocaleString("SysInfo.aspx.language.ini", "global", "string004", "模組名稱") + "</td>";
                html += "<td style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionHead>" + com.dsc.locale.LocaleString.getMainFrameLocaleString("SysInfo.aspx.language.ini", "global", "string005", "版本") + "</td>";
                html += "</tr>";

                foreach (XmlNode xn in xnl)
                {
                    html += "<tr>";
                    html += "<td style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionDetail>" + xn.Attributes["id"].Value + "</td>";
                    html += "<td style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionDetail>" + xn.Attributes["name"].Value + "</td>";
                    html += "<td style='border-style:solid;border-width:1px;border-top-style:none;border-left-style:none' class=OpinionDetail>" + xn.Attributes["version"].Value + "</td>";
                    html += "</tr>";

                }
                html += "</table>";

                VersionFile.Text = html;

                HelpButton.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("SysInfo.aspx.language.ini", "global", "string006", "線上說明");
                DefaultObjectButton.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("SysInfo.aspx.language.ini", "global", "string007", "平台物件說明");
                ProjectObjectButton.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("SysInfo.aspx.language.ini", "global", "string008", "專案物件說明");
            }
        }
    }
    protected void HelpButton_Click(object sender, EventArgs e)
    {
        base.showOpenWindow(Page.ResolveUrl("~/Help/Default.aspx"), "OnlineHelp", "no", "500", "0", "", "no", "yes", "yes", "no", "yes", "no", "0", "800", "no", true);
    }
    protected void DefaultObjectButton_Click(object sender, EventArgs e)
    {
        base.showOpenWindow(Page.ResolveUrl("~/Help/DefaultObject.aspx"), "OnlineHelp", "no", "500", "0", "", "no", "yes", "yes", "no", "yes", "no", "0", "800", "no", true);
    }
    protected void ProjectObjectButton_Click(object sender, EventArgs e)
    {
        base.showOpenWindow(Page.ResolveUrl("~/Help/ProjectObject.aspx"), "OnlineHelp", "no", "500", "0", "", "no", "yes", "yes", "no", "yes", "no", "0", "800", "no", true);
    }
}
