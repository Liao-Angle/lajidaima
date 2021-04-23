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

public partial class Program_System_Maintain_SMVQ_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SMVQ";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string[,] ids = new string[,]{
                    {DSCWebControl.BarGraph.H_BAR.ToString(),com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvq_maintain_aspx.language.ini", "message", "ids1", "水平長條圖")},
                    {DSCWebControl.BarGraph.V_BAR.ToString(), com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvq_maintain_aspx.language.ini", "message", "ids2", "垂直長條圖")},
                    {DSCWebControl.BarGraph.PIE.ToString(), com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvq_maintain_aspx.language.ini", "message", "ids3", "圓餅圖")}
                };
                BarType.setListItem(ids);
            }
        }
    }


    protected void OKButton_Click(object sender, EventArgs e)
    {
        if (!SQLInterval.Checked)
        {
            try
            {
                DateTime dt = DateTime.Parse(StartTime.ValueText);
            }
            catch
            {
                //MessageBox("請輸入起始時間");
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvq_maintain_aspx.language.ini", "message", "QueryError1", "請輸入起始時間"));
                return;
            }
            try
            {
                DateTime dt = DateTime.Parse(EndTime.ValueText);
            }
            catch
            {
                //MessageBox("請輸入結束時間");
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvq_maintain_aspx.language.ini", "message", "QueryError2", "請輸入結束時間"));
                return;
            }
        }
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        AbstractEngine engine = null;

        try
        {
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string sql = "select count(*) as CCS, ";
            if (TimeInterval.Checked && TIMonth.Checked)
            {
                sql += "substring(LOGINTIME, 1, 7) as FLD from LOGINDATA where LOGINTIME>='" + Utility.filter(StartTime.ValueText) + "' and LOGINTIME<='" + Utility.filter(EndTime.ValueText) + "' group by substring(LOGINTIME, 1, 7) order by substring(LOGINTIME, 1, 7) asc";
            }
            else if (TimeInterval.Checked && TIDay.Checked)
            {
                sql += "substring(LOGINTIME, 1, 10) as FLD from LOGINDATA where LOGINTIME>='" + Utility.filter(StartTime.ValueText) + "' and LOGINTIME<='" + Utility.filter(EndTime.ValueText) + "' group by substring(LOGINTIME, 1, 10) order by substring(LOGINTIME, 1, 10) asc";
            }
            else if (TimeInterval.Checked && TIHour.Checked)
            {
                sql += "substring(LOGINTIME, 1, 13) as FLD from LOGINDATA where LOGINTIME>='" + Utility.filter(StartTime.ValueText) + "' and LOGINTIME<='" + Utility.filter(EndTime.ValueText) + "' group by substring(LOGINTIME, 1, 13) order by substring(LOGINTIME, 1, 13) asc";
            }
            else if (TimeInterval.Checked && TIMinute.Checked)
            {
                sql += "substring(LOGINTIME, 1, 16) as FLD from LOGINDATA where LOGINTIME>='" + Utility.filter(StartTime.ValueText) + "' and LOGINTIME<='" + Utility.filter(EndTime.ValueText) + "' group by substring(LOGINTIME, 1, 16) order by substring(LOGINTIME, 1, 16) asc";
            }
            else if (TypeInterval.Checked && TYUser.Checked)
            {
                sql += "USERNAME as FLD from LOGINDATA where LOGINTIME>='" + Utility.filter(StartTime.ValueText) + "' and LOGINTIME<='" + Utility.filter(EndTime.ValueText) + "' group by USERNAME order by USERNAME asc";
            }
            else if (TypeInterval.Checked && TYIP.Checked)
            {
                sql += "LOGINIP as FLD from LOGINDATA where LOGINTIME>='" + Utility.filter(StartTime.ValueText) + "' and LOGINTIME<='" + Utility.filter(EndTime.ValueText) + "' group by LOGINIP order by LOGINIP asc";
            }

            else if (SQLInterval.Checked)
            {
                sql = Utility.filter(SQLField.ValueText);
            }

            DataSet ds = engine.getDataSet(sql, "TEMP");
            Session["SMVQAAADS"] = ds;
            Session["SMVQAAATYPE"] = BarType.ValueText;
            engine.close();

            //showPanelWindow("登入紀錄統計結果", "SMVQ/Detail.aspx", 0, 0, "", true, true);
            showPanelWindow(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvq_maintain_aspx.language.ini", "message", "QueryError3", "登入紀錄統計結果"), "Program/System/Maintain/SMVQ/Detail.aspx", 0, 0, "", true, true);
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(te.Message);
            writeLog(te);
        }
    }
}
