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
using com.dsc.kernal.databean;

public partial class Calendar_showDetail : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                DSCLabel1.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string013", "行事曆類型");
                DSCLabel2.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string014", "日期");
                DSCLabel3.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string015", "起始時間");
                DSCLabel4.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string016", "結束時間");
                DSCLabel5.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string017", "主旨");
                DSCLabel6.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string018", "內容");
                string tmpGUID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["GUID"]);
                showContent(tmpGUID);
            }
        }
    }

    private void showContent(string guid)
    {
        AbstractEngine engine = null;
        try
        {

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string sql = "";

            sql = "select SMVFAAA003, SMVGAAA006, SMVGAAA007, SMVGAAA008, SMVGAAA009, SMVGAAA010 from SMVGAAA inner join SMVFAAA on SMVGAAA003=SMVFAAA001 where SMVGAAA001='" + Utility.filter(guid) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");

            if (ds.Tables[0].Rows.Count > 0)
            {
                SMVGAAA003.ValueText = ds.Tables[0].Rows[0]["SMVFAAA003"].ToString();
                SMVGAAA006.ValueText = ds.Tables[0].Rows[0]["SMVGAAA006"].ToString();
                SMVGAAA007.ValueText = ds.Tables[0].Rows[0]["SMVGAAA007"].ToString();
                SMVGAAA008.ValueText = ds.Tables[0].Rows[0]["SMVGAAA008"].ToString();
                SMVGAAA009.ValueText = ds.Tables[0].Rows[0]["SMVGAAA009"].ToString();
                SMVGAAA010.ValueText = ds.Tables[0].Rows[0]["SMVGAAA010"].ToString();
            }
            ds.Dispose();
            ds = null;
            engine.close();
        }
        catch
        {
            try
            {
                engine.close();
            }
            catch { };
        }
    }
}
