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
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using WebServerProject.maintain.SMVR;

public partial class Program_System_Maintain_SMVR_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVR";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        string[,] ids;
        ids = new string[20, 2];
        for (int i = 0; i < 20; i++)
        {
            ids[i, 0] = string.Format("{0}", 2008 + i);
            ids[i, 1] = string.Format("{0}", 2008 + i);
        }
        YEARF.setListItem(ids);
        YEARF2.setListItem(ids);


        SMVRAAA obj = (SMVRAAA)objects;
        DataObjectSet abset = null;
        bool isAddNew = (bool)getSession("isNew");
        if (isAddNew)
        {
            abset = new DataObjectSet();
            abset.setAssemblyName("WebServerProject");
            abset.setChildClassString("WebServerProject.maintain.SMVR.SMVRAAB");
            abset.setTableName("SMVRAAB");
            obj.setChild("SMVRAAB", abset);
        }
        else
        {
            abset = obj.getChild("SMVRAAB");
        }

        SMVRAAA002.ValueText = obj.SMVRAAA002;
        SMVRAAA003.ValueText = obj.SMVRAAA003;
        SMVRAAA004.ValueText = obj.SMVRAAA004;

        string[,] orderby = new string[,]{
            {"SMVRAAB003",DataObjectConstants.ASC}
        };
        abset.sort(orderby);

        ABList.HiddenField = new string[] { "SMVRAAB001", "SMVRAAB002" };
        ABList.DialogHeight = 350;
        ABList.InputForm = "Schedule.aspx";
        ABList.dataSource = abset;
        ABList.updateTable();
    }
    protected override void saveData(DataObject objects)
    {
        SMVRAAA obj = (SMVRAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVRAAA001 = IDProcessor.getID("");
        }
        obj.SMVRAAA002 = SMVRAAA002.ValueText;
        obj.SMVRAAA003 = SMVRAAA003.ValueText;
        obj.SMVRAAA004 = SMVRAAA004.ValueText;
        for (int i = 0; i < ABList.dataSource.getAvailableDataObjectCount(); i++)
        {
            SMVRAAB ab = (SMVRAAB)ABList.dataSource.getAvailableDataObject(i);
            ab.SMVRAAB002 = obj.SMVRAAA001;
        }
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVRAgent agent = new SMVRAgent();
        agent.engine = engine;
        agent.query("1=2");

        //取得資料
        bool result = agent.defaultData.add(objects);
        if (!result)
        {
            engine.close();
            throw new Exception(agent.defaultData.errorString);
        }
        else
        {
            result = agent.update();
            engine.close();
            if (!result)
            {
                throw new Exception(engine.errorString);
            }
        }
    }
    protected void GenerateSchedule_Click(object sender, EventArgs e)
    {
        if (SMVRAAB004.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvr_detail_aspx.language.ini", "message", "QueryError1", "請輸入上班時間"));
            return;
        }
        if (SMVRAAB005.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvr_detail_aspx.language.ini", "message", "QueryError2", "請輸入下班時間"));
            return;
        }
        if (SMVRAAB007.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvr_detail_aspx.language.ini", "message", "QueryError3", "請輸入午休起始時間"));
            return;
        }
        if (SMVRAAB008.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvr_detail_aspx.language.ini", "message", "QueryError4", "請輸入午休結束時間"));
            return;
        }
        if (SMVRAAB005.ValueText.CompareTo(SMVRAAB004.ValueText) < 0)
        {
            if (((PrevDay1.Checked) && (NextDay1.Checked)) || ((!PrevDay1.Checked) && (!NextDay1.Checked)))
            {
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvr_detail_aspx.language.ini", "message", "QueryError5", "下班時間早於上班時間, 請擇一勾選前一天或是後一天"));
                return;
            }
        }
        if (SMVRAAB008.ValueText.CompareTo(SMVRAAB007.ValueText) < 0)
        {
            if (((PrevDay2.Checked) && (NextDay2.Checked)) || ((!PrevDay2.Checked) && (!NextDay2.Checked)))
            {
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvr_detail_aspx.language.ini", "message", "QueryError6", "午休結束時間早於開始時間, 請擇一勾選前一天或是後一天"));
                return;
            }
        }
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string sql = "select * from SMVRAAC";
            DataSet ac = engine.getDataSet(sql, "TEMP");
            engine.close();

            DataObjectSet dos = ABList.dataSource;
            if (true)
            {
                for (int i = 0; i < dos.getDataObjectCount(); i++)
                {
                    SMVRAAB ab = (SMVRAAB)dos.getDataObject(i);
                    if (ab.SMVRAAB003.Substring(0, 4).Equals(YEARF.ValueText))
                    {
                        ab.delete();
                    }
                }
            }

            DateTime startDay = DateTime.Parse(YEARF.ValueText + "/01/01");
            while(true)
            {
                if (startDay.Year != int.Parse(YEARF.ValueText))
                {
                    break;
                }
                if ((startDay.DayOfWeek.Equals(DayOfWeek.Sunday)) && (!SUNDAY.Checked))
                {
                    startDay = startDay.AddDays(1);
                    continue;
                }
                if ((startDay.DayOfWeek.Equals(DayOfWeek.Monday)) && (!MONDAY.Checked))
                {
                    startDay = startDay.AddDays(1);
                    continue;
                }
                if ((startDay.DayOfWeek.Equals(DayOfWeek.Tuesday)) && (!TUESDAY.Checked))
                {
                    startDay = startDay.AddDays(1);
                    continue;
                }
                if ((startDay.DayOfWeek.Equals(DayOfWeek.Wednesday)) && (!WEDNESDAY.Checked))
                {
                    startDay = startDay.AddDays(1);
                    continue;
                }
                if ((startDay.DayOfWeek.Equals(DayOfWeek.Thursday)) && (!THURSDAY.Checked))
                {
                    startDay = startDay.AddDays(1);
                    continue;
                }
                if ((startDay.DayOfWeek.Equals(DayOfWeek.Friday)) && (!FRIDAY.Checked))
                {
                    startDay = startDay.AddDays(1);
                    continue;
                }
                if ((startDay.DayOfWeek.Equals(DayOfWeek.Saturday)) && (!SATURDAY.Checked))
                {
                    startDay = startDay.AddDays(1);
                    continue;
                }
                string checkD = com.dsc.kernal.utility.DateTimeUtility.convertDateTimeToString(startDay).Substring(0, 10);
                bool hasFound = false;
                for (int j = 0; j < ac.Tables[0].Rows.Count; j++)
                {
                    if (checkD.Substring(5, 5).Equals(ac.Tables[0].Rows[j]["SMVRAAC003"].ToString()))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    SMVRAAB ab = (SMVRAAB)dos.create();
                    ab.SMVRAAB001 = IDProcessor.getID("");
                    ab.SMVRAAB002 = "TEMP";
                    ab.SMVRAAB003 = checkD;
                    if (!PrevDay1.Checked)
                    {
                        ab.SMVRAAB004 = checkD + " " + SMVRAAB004.ValueText;
                    }
                    else
                    {
                        DateTime temp = new DateTime(startDay.Ticks);
                        temp = temp.AddDays(-1);
                        ab.SMVRAAB004 = com.dsc.kernal.utility.DateTimeUtility.convertDateTimeToString(temp).Substring(0, 10) + " " + SMVRAAB004.ValueText;
                    }
                    if (!NextDay1.Checked)
                    {
                        ab.SMVRAAB005 = checkD + " " + SMVRAAB005.ValueText;
                    }
                    else
                    {
                        DateTime temp = new DateTime(startDay.Ticks);
                        temp = temp.AddDays(+1);
                        ab.SMVRAAB005 = com.dsc.kernal.utility.DateTimeUtility.convertDateTimeToString(temp).Substring(0, 10) + " " + SMVRAAB005.ValueText;
                    }
                    if (!PrevDay2.Checked)
                    {
                        ab.SMVRAAB007 = checkD + " " + SMVRAAB007.ValueText;
                    }
                    else
                    {
                        DateTime temp = new DateTime(startDay.Ticks);
                        temp = temp.AddDays(-1);
                        ab.SMVRAAB007 = com.dsc.kernal.utility.DateTimeUtility.convertDateTimeToString(temp).Substring(0, 10) + " " + SMVRAAB007.ValueText;
                    }
                    if (!NextDay2.Checked)
                    {
                        ab.SMVRAAB008 = checkD + " " + SMVRAAB008.ValueText;
                    }
                    else
                    {
                        DateTime temp = new DateTime(startDay.Ticks);
                        temp = temp.AddDays(+1);
                        ab.SMVRAAB008 = com.dsc.kernal.utility.DateTimeUtility.convertDateTimeToString(temp).Substring(0, 10) + " " + SMVRAAB008.ValueText;
                    }
                    DateTime s1 = DateTime.Parse(ab.SMVRAAB004);
                    DateTime e1 = DateTime.Parse(ab.SMVRAAB005);
                    DateTime s2 = DateTime.Parse(ab.SMVRAAB007);
                    DateTime e2 = DateTime.Parse(ab.SMVRAAB008);

                    TimeSpan t1 = e1.Subtract(s1);
                    TimeSpan t2 = e2.Subtract(s2);
                    TimeSpan t3 = t1.Subtract(t2);

                    decimal minutes = (decimal)t3.TotalMinutes;
                    decimal data = com.dsc.kernal.utility.Utility.Round((decimal)(minutes / 60), 2);
                    ab.SMVRAAB006 = data.ToString();
                    dos.add(ab);
                }
                startDay = startDay.AddDays(1);
            }

            string[,] orderby = new string[,]{
            {"SMVRAAB003",DataObjectConstants.ASC}
        };
            ABList.dataSource.sort(orderby);
            ABList.updateTable();
            DSCTabControl1.SelectedTab = 0;
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvr_detail_aspx.language.ini", "message", "QueryError7", "建立成功"));
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            writeLog(te);
            MessageBox(te.Message);
        }
    }
    
    protected void DeleteSchedule_Click(object sender, EventArgs e)
    {
        DataObjectSet dos = ABList.dataSource;
        if (true)
        {
            for (int i = 0; i < dos.getDataObjectCount(); i++)
            {
                SMVRAAB ab = (SMVRAAB)dos.getDataObject(i);
                if (ab.SMVRAAB003.Substring(0, 4).Equals(YEARF2.ValueText))
                {
                    ab.delete();
                }
            }
        }
        string[,] orderby = new string[,]{
            {"SMVRAAB003",DataObjectConstants.ASC}
        };
        ABList.dataSource.sort(orderby);
        ABList.updateTable();
        DSCTabControl1.SelectedTab = 0;
        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvr_detail_aspx.language.ini", "message", "QueryError8", "刪除成功"));

    }
}
