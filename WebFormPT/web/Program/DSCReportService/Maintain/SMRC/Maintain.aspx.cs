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

public partial class Program_DSCReportService_Maintain_SMRC_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                readData();
            }
        }
    }

    private void readData()
    {
        string[,] ids;

        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smrc_maintain_aspx.language.ini", "message", "msg1", "權限群組對應")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smrc_maintain_aspx.language.ini", "message", "msg2", "帳號對應")}
        };
        SMRCAAA002.setListItem(ids);

        string connectString = (string)Session["ConnectString"];
        string engineType = (string)Session["EngineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select * from SMRCAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        SMRCAAA002.ValueText = ds.Tables[0].Rows[0]["SMRCAAA002"].ToString();
        SMRCAAA003.ValueText = ds.Tables[0].Rows[0]["SMRCAAA003"].ToString();
        SMRCAAA004.ValueText = ds.Tables[0].Rows[0]["SMRCAAA004"].ToString();
        SMRCAAA005.ValueText = ds.Tables[0].Rows[0]["SMRCAAA005"].ToString();
        SMRCAAA006.ValueText = ds.Tables[0].Rows[0]["SMRCAAA006"].ToString();
        if (ds.Tables[0].Rows[0]["SMRCAAA007"].ToString().Equals("Y"))
        {
            SMRCAAA007.Checked = true;
        }
        else
        {
            SMRCAAA007.Checked = false;
        }
        engine.close();
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["ConnectString"];
        string engineType = (string)Session["EngineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select * from SMRCAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        DataRow dr = ds.Tables[0].Rows[0];

        dr["SMRCAAA002"] = SMRCAAA002.ValueText;
        dr["SMRCAAA003"] = SMRCAAA003.ValueText;
        dr["SMRCAAA004"] = SMRCAAA004.ValueText;
        dr["SMRCAAA005"] = SMRCAAA005.ValueText;
        dr["SMRCAAA006"] = SMRCAAA006.ValueText;
        if (SMRCAAA007.Checked)
        {
            dr["SMRCAAA007"] = "Y";
        }
        else
        {
            dr["SMRCAAA007"] = "N";
        }
        if (!engine.updateDataSet(ds))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smrc_maintain_aspx.language.ini", "message", "QueryError1", "儲存錯誤: ") + engine.errorString.Replace("\n", "\\n"));
        }
        else
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smrc_maintain_aspx.language.ini", "message", "QueryError2", "儲存成功"));
        }
        engine.close();
    }
}
