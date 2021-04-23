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

public partial class Program_DSCAuditService_Maintain_SMXE_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SMXE";
        ApplicationID = "SYSTEM";
        ModuleID = "SMXAA";

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string[,] ids = new string[,]{
                    {DSCWebControl.BarGraph.H_BAR.ToString(), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauditservice_maintain_smxe_maintain_aspx.language.ini", "message", "H_BAR", "水平長條圖")},
                    {DSCWebControl.BarGraph.V_BAR.ToString(), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauditservice_maintain_smxe_maintain_aspx.language.ini", "message", "V_BAR", "垂直長條圖")},
                    {DSCWebControl.BarGraph.PIE.ToString(), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauditservice_maintain_smxe_maintain_aspx.language.ini", "message", "PIE", "圓餅圖")}
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
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauditservice_maintain_smxe_maintain_aspx.language.ini", "message", "QueryError1", "請輸入起始時間"));
                return;
            }
            try
            {
                DateTime dt = DateTime.Parse(EndTime.ValueText);
            }
            catch
            {
                //MessageBox("請輸入結束時間");
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauditservice_maintain_smxe_maintain_aspx.language.ini", "message", "QueryError2", "請輸入結束時間"));
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
                sql += "substring(SMXCAAA009, 1, 7) as FLD from SMXCAAA where SMXCAAA009>='" + Utility.filter(StartTime.ValueText) + "' and SMXCAAA009<='" + Utility.filter(EndTime.ValueText) + "' group by substring(SMXCAAA009, 1, 7) order by substring(SMXCAAA009, 1, 7) asc";
            }
            else if (TimeInterval.Checked && TIDay.Checked)
            {
                sql += "substring(SMXCAAA009, 1, 10) as FLD from SMXCAAA where SMXCAAA009>='" + Utility.filter(StartTime.ValueText) + "' and SMXCAAA009<='" + Utility.filter(EndTime.ValueText) + "' group by substring(SMXCAAA009, 1, 10) order by substring(SMXCAAA009, 1, 10) asc";
            }
            else if (TimeInterval.Checked && TIHour.Checked)
            {
                sql += "substring(SMXCAAA009, 1, 13) as FLD from SMXCAAA where SMXCAAA009>='" + Utility.filter(StartTime.ValueText) + "' and SMXCAAA009<='" + Utility.filter(EndTime.ValueText) + "' group by substring(SMXCAAA009, 1, 13) order by substring(SMXCAAA009, 1, 13) asc";
            }
            else if (TimeInterval.Checked && TIMinute.Checked)
            {
                sql += "substring(SMXCAAA009, 1, 16) as FLD from SMXCAAA where SMXCAAA009>='" + Utility.filter(StartTime.ValueText) + "' and SMXCAAA009<='" + Utility.filter(EndTime.ValueText) + "' group by substring(SMXCAAA009, 1, 16) order by substring(SMXCAAA009, 1, 16) asc";
            }
            else if (TypeInterval.Checked && TYApplication.Checked)
            {
                sql += "SMXCAAA003 as FLD from SMXCAAA where SMXCAAA009>='" + Utility.filter(StartTime.ValueText) + "' and SMXCAAA009<='" + Utility.filter(EndTime.ValueText) + "' group by SMXCAAA003 order by SMXCAAA003 asc";
            }
            else if (TypeInterval.Checked && TYModule.Checked)
            {
                sql += "SMXCAAA005 as FLD from SMXCAAA where SMXCAAA009>='" + Utility.filter(StartTime.ValueText) + "' and SMXCAAA009<='" + Utility.filter(EndTime.ValueText) + "' group by SMXCAAA005 order by SMXCAAA005 asc";
            }
            else if (TypeInterval.Checked && TYProgram.Checked)
            {
                sql += "SMXCAAA007 as FLD from SMXCAAA where SMXCAAA009>='" + Utility.filter(StartTime.ValueText) + "' and SMXCAAA009<='" + Utility.filter(EndTime.ValueText) + "' group by SMXCAAA007 order by SMXCAAA007 asc";
            }
            else if (TypeInterval.Checked && TYUser.Checked)
            {
                sql += "SMXCAAA013 as FLD from SMXCAAA where SMXCAAA009>='" + Utility.filter(StartTime.ValueText) + "' and SMXCAAA009<='" + Utility.filter(EndTime.ValueText) + "' group by SMXCAAA013 order by SMXCAAA013 asc";
            }
            else if (TypeInterval.Checked && TYLevel.Checked)
            {
                sql += "SMXCAAA008 as FLD from SMXCAAA where SMXCAAA009>='" + Utility.filter(StartTime.ValueText) + "' and SMXCAAA009<='" + Utility.filter(EndTime.ValueText) + "' group by SMXCAAA008 order by SMXCAAA008 asc";
            }
            else if (SQLInterval.Checked)
            {
                sql = SQLField.ValueText;
            }

            DataSet ds = engine.getDataSet(sql, "TEMP");
            Session["SMXEAAADS"] = ds;
            Session["SMXEAAATYPE"] = BarType.ValueText;
            engine.close();

            showPanelWindow(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauditservice_maintain_smxe_maintain_aspx.language.ini", "message", "PanelWindow", "稽核紀錄統計結果"), "Program/DSCAuditService/Maintain/SMXE/Detail.aspx", 0, 0, "", true, true);
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
