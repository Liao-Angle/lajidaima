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
using WebServerProject.maintain.SMVG;

public partial class Calendar_newDetail : BaseWebUI.WebFormBasePage
{
    protected override void OnPreRenderComplete(EventArgs e)
    {
        DSCLabel1.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string013", "行事曆類型");
        DSCLabel2.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string014", "日期");
        DSCLabel3.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string015", "起始時間");
        DSCLabel4.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string016", "結束時間");
        DSCLabel5.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string017", "主旨");
        DSCLabel6.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string018", "內容");
        SaveButton.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string019", "儲存");
        SaveButton.ConfirmText = com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string020", "你確定要儲存此行事曆項目嗎?");

        base.OnPreRenderComplete(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                showContent();
            }
        }
    }

    private void showContent()
    {
        AbstractEngine engine = null;
        try
        {

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string sql = "";

            sql = "select SMVFAAA001, SMVFAAA003 from SMVFAAA where SMVFAAA004='Y'";
            DataSet ds = engine.getDataSet(sql, "TEMP");

            string[,] ids = new string[ds.Tables[0].Rows.Count, 2];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
            }

            SMVGAAA003.setListItem(ids);

            ids = new string[48, 2];
            for (int i = 0; i < 24; i++)
            {
                string tag=string.Format("{0:00}", i);
                ids[i * 2, 0] = tag + ":00";
                ids[i * 2, 1] = ids[i * 2,0];
                ids[i * 2 + 1, 0] = tag + ":30";
                ids[i * 2 + 1, 1] = ids[i * 2 + 1, 0];
            }
            SMVGAAA007.setListItem(ids);
            SMVGAAA008.setListItem(ids);

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
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        try
        {

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string sql = "";

            SMVGAgent agent = new SMVGAgent();
            agent.engine = engine;
            agent.query("(1=2)");
            DataObjectSet dos = agent.defaultData;

            SMVGAAA obj = (SMVGAAA)dos.create();
            obj.SMVGAAA001 = IDProcessor.getID("");
            obj.SMVGAAA002 = (string)Session["UserGUID"];
            obj.SMVGAAA003 = SMVGAAA003.ValueText;
            obj.SMVGAAA004 = "N";
            obj.SMVGAAA005 = "*";
            obj.SMVGAAA006 = SMVGAAA006.ValueText;
            obj.SMVGAAA007 = SMVGAAA007.ValueText;
            obj.SMVGAAA008 = SMVGAAA008.ValueText;
            obj.SMVGAAA009 = SMVGAAA009.ValueText;
            obj.SMVGAAA010 = SMVGAAA010.ValueText;

            if (!dos.add(obj))
            {
                throw new Exception(dos.errorString);
            }

            if (!agent.update())
            {
                throw new Exception(engine.errorString);
            }

            engine.close();
            MessageBox(com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string012", "儲存成功"));
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(te.Message.Replace("\n", "\\n"));
        }

    }
}
