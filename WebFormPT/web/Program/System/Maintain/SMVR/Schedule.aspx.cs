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

public partial class Program_System_Maintain_SMVR_Schedule : BaseWebUI.DataListInlineForm
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


        
        SMVRAAB obj = (SMVRAAB)objects;
        DataObjectSet abset = null;


        SMVRAAB003.ValueText = obj.SMVRAAB003;
        SMVRAAB004.ValueText = obj.SMVRAAB004;
        SMVRAAB005.ValueText = obj.SMVRAAB005;
        SMVRAAB007.ValueText = obj.SMVRAAB007;
        SMVRAAB008.ValueText = obj.SMVRAAB008;

    }
    protected override void saveData(DataObject objects)
    {
        calSMVRAAB006();
        if (decimal.Parse(SMVRAAB006.ValueText) == 0)
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvr_schedule_aspx.language.ini", "message", "QueryError", "班別設定有誤"));
        }

        SMVRAAB obj = (SMVRAAB)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVRAAB001 = IDProcessor.getID("");
            obj.SMVRAAB002 = "temp";
        }
        obj.SMVRAAB003 = SMVRAAB003.ValueText;
        obj.SMVRAAB004 = SMVRAAB004.ValueText;
        obj.SMVRAAB005 = SMVRAAB005.ValueText;
        obj.SMVRAAB006 = SMVRAAB006.ValueText;
        obj.SMVRAAB007 = SMVRAAB007.ValueText;
        obj.SMVRAAB008 = SMVRAAB008.ValueText;

    }
    private void calSMVRAAB006()
    {
        try
        {
            DateTime s1 = DateTime.Parse(SMVRAAB004.ValueText);
            DateTime e1 = DateTime.Parse(SMVRAAB005.ValueText);
            DateTime s2 = DateTime.Parse(SMVRAAB007.ValueText);
            DateTime e2 = DateTime.Parse(SMVRAAB008.ValueText);

            TimeSpan t1 = e1.Subtract(s1);
            TimeSpan t2 = e2.Subtract(s2);
            TimeSpan t3 = t1.Subtract(t2);

            decimal minutes = (decimal)t3.TotalMinutes;
            decimal data = com.dsc.kernal.utility.Utility.Round((decimal)(minutes / 60), 2);
            SMVRAAB006.ValueText = data.ToString();
        }
        catch (Exception te)
        {
            MessageBox(te.Message);
            SMVRAAB006.ValueText = "0";
        }
        
    }
    protected void SMVRAAB004_DateTimeClick(string values)
    {
        calSMVRAAB006();
    }
    protected void SMVRAAB005_DateTimeClick(string values)
    {
        calSMVRAAB006();
    }
    protected void SMVRAAB007_DateTimeClick(string values)
    {
        calSMVRAAB006();
    }
    protected void SMVRAAB008_DateTimeClick(string values)
    {
        calSMVRAAB006();
    }
}
