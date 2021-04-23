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
using com.dsc.kernal.utility;

public partial class FrameEnterprise_SystemMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string guid = Request.QueryString["GUID"];
        string table = Request.QueryString["alertType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "";
        sql = "select * from " + Utility.filter(table) + " where " + Utility.filter(table) + "001='" + Utility.filter(guid) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        string mType = ds.Tables[0].Rows[0][""+table+"003"].ToString();
        if (mType.Equals("0"))
        {
            MessageType.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string001", "一般訊息");
        }
        else if (mType.Equals("1"))
        {
            MessageType.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string002", "重要訊息");
        }
        else
        {
            MessageType.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string003", "行事曆通知");
        }

        AlertTime.Text = ds.Tables[0].Rows[0][""+table+"004"].ToString();
        Title.Text = ds.Tables[0].Rows[0][""+table+"007"].ToString();
        if (ds.Tables[0].Rows[0][""+table+"009"].ToString().Equals(""))
        {
            URL.Text = "";
        }
        else
        {
            string wording = ds.Tables[0].Rows[0][""+table+"009"].ToString();
            if ((wording.Length > 7) && (wording.ToLower().Substring(0, 7).Equals("http://")))
            {

                URL.Text = "<a href='" + wording + "' target=_blank>"+com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string004", "開啟")+"</a>";
            }
            else if ((wording.Length > 8) && (wording.ToLower().Substring(0, 8).Equals("https://")))
            {

                URL.Text = "<a href='" + wording + "' target=_blank>" + com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string004", "開啟") + "</a>";
            }
            else
            {
                URL.Text = "<a href='#' onclick='" + wording + ";return false;'>" + com.dsc.locale.LocaleString.getMainFrameLocaleString("SystemMessage.aspx.language.ini", "global", "string004", "開啟") + "</a>";
            }

        }
        Content.Text = ds.Tables[0].Rows[0][""+table+"008"].ToString().Replace("\n", "<br>");

        if (table.Equals("SMVCAAA"))
        {
            ds.Tables[0].Rows[0]["" + table + "005"] = "Y";
            engine.updateDataSet(ds);
        }
        else
        {
            sql = "update SMVDAAB set SMVDAAB005='Y', SMVDAAB006='" + DateTimeUtility.getSystemTime2(null) + "' where SMVDAAB002='" + Utility.filter(guid) + "' and SMVDAAB003='" + (String)Session["UserID"] + "'";
            engine.executeSQL(sql);
        }
        engine.close();
    }
}
