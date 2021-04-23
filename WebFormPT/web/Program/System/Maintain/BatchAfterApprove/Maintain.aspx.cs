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

public partial class Program_System_Maintain_BatchAfterApprove_Maintain : BaseWebUI.GeneralWebPage
{
    //筆對狀況不一致的SQL主體
    private string oriSQL = " SMWYAAA005,currentState,SMWYAAA002 FROM ProcessInstance as PI WITH(NOLOCK) JOIN SMWYAAA WITH(NOLOCK) on SMWYAAA005 = PI.serialNumber AND SMWYAAA020 = 'I' AND currentState <> '1' ";

    private string fname = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
            }
        }
    }
    protected void GlassButton1_Click(object sender, EventArgs e)
    {
        string sql = "select "+oriSQL + ConditionField.ValueText;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;

        try
        {
            engine = factory.getEngine(engineType, connectString);

            DataSet ds = engine.getDataSet(sql, "TEMP");

            engine.close();

            lblDataCount.Text = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_batchafterapprove_maintain_aspx.language.ini", "message", "QueryError1", "共需處理資料: #0# 筆", new string[] { ds.Tables[0].Rows.Count.ToString() });
        }
        catch (Exception ze)
        {
            try
            {
                engine.close();
            }
            catch { };
            lblDataCount.Text = ze.Message;
        }
        
    }
    protected void GlassButton2_Click(object sender, EventArgs e)
    {
        string fname2 = DateTimeUtility.getSystemTime2(null).Substring(0, 16).Replace("/", "").Replace(":","");
        fname = Server.MapPath("~/LogFolder/" + fname2 + "_batchafterapprove.log");


        string xname = fname2 + "_batchafterapprove.log";


        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        WebServerProject.DataOperate ddo = new WebServerProject.DataOperate(engine);

        int scount = 0;
        try
        {
            scount = int.Parse(StepCount.ValueText);
        }
        catch
        {
            scount = 100;
        }
        string sql = "select top " + scount.ToString() + oriSQL + ConditionField.ValueText;

        writeLog2("******************************************************");
        writeLog2("SQL:" + sql);
        
        int successCount = 0;
        int failCount = 0;

        DataSet ds = engine.getDataSet(sql, "TEMP");

        writeLog2(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_batchafterapprove_maintain_aspx.language.ini", "message", "QueryError2", "本次處理筆數:#0#", new string[]{ds.Tables[0].Rows.Count.ToString()}));

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string so = ds.Tables[0].Rows[i][2].ToString();
            string sn = ds.Tables[0].Rows[i][0].ToString();
            string res = "N";
            if (ds.Tables[0].Rows[i][1].ToString().Equals("3"))
            {
                res = "Y";
            }
            else if (ds.Tables[0].Rows[i][1].ToString().Equals("4"))
            {
                res = "W";
            }
            else
            {
                res = "N";
            }
            try
            {
                ddo.process(sn, res, Request.ApplicationPath, Server);
                writeLog2(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_batchafterapprove_maintain_aspx.language.ini", "message", "QueryError3", "處理單號:#0#:流程實例序號:#1#:預計結果:#2#:成功", new string[] { so, sn, res }));
                writeLog2("------------------------------------------------------");
                successCount++;
            }
            catch (Exception te)
            {
                writeLog2(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_batchafterapprove_maintain_aspx.language.ini", "message", "QueryError4", "處理單號:#0#:流程實例序號:#1#:預計結果:#2#:失敗", new string[] { so, sn, res }));
                writeLog2(te.Message);
                writeLog2(te.StackTrace);
                writeLog2("------------------------------------------------------");
                failCount++;
            }
        }

        engine.close();

        writeLog2("======================================================");
        writeLog2(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_batchafterapprove_maintain_aspx.language.ini", "message", "QueryError5", "成功筆數:#0#", new string[] { successCount.ToString() }));
        writeLog2(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_batchafterapprove_maintain_aspx.language.ini", "message", "QueryError6", "錯誤筆數:#0#", new string[] { failCount.ToString() }));
        writeLog2(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_batchafterapprove_maintain_aspx.language.ini", "message", "QueryError7", "處理完成!!"));
        writeLog2("******************************************************");

        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_batchafterapprove_maintain_aspx.language.ini", "message", "QueryError8", "處理完成, 本次Log檔位於:#0#", new string[] { xname }));
    }

    private void writeLog2(string message)
    {
        System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);

        sw.WriteLine(message);

        sw.Close();

    }
}
