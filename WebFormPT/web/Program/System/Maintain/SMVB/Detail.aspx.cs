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
using WebServerProject.maintain.SMVB;

public partial class Program_System_Maintain_SMVB_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVB";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        try
        {
            connectString = (string)Session["connectString"];
            engineType = (string)Session["engineType"];

            SMVAAAB009.clientEngineType = engineType;
            SMVAAAB009.connectDBString = connectString;

            SMVAAAB obj = (SMVAAAB)objects;
            SMVAAAB002.ValueText = obj.SMVAAAB002;
            SMVAAAB003.ValueText = obj.SMVAAAB003;
            SMVAAAB004.ValueText = obj.SMVAAAB004;
            SMVAAAB005.ValueText = obj.SMVAAAB005;
            SMVAAAB006.ValueText = obj.SMVAAAB006;

            if (obj.SMVAAAB007.Equals("Y"))
            {
                SMVAAAB007.Checked = true;
            }
            else
            {
                SMVAAAB007.Checked = false;
            }
            SMVAAAB007_Click(null, null);
            if (obj.SMVAAAB008.Equals("Y"))
            {
                SMVAAAB008.Checked = true;
            }
            else
            {
                SMVAAAB008.Checked = false;
            }
            SMVAAAB009.ValueText = obj.SMVAAAB009;
            SMVAAAB009.doValidate();
            SMVAAAB010.ValueText = obj.SMVAAAB010;
            SMVAAAB011.ValueText = obj.SMVAAAB011;
            SMVAAAB012.ValueText = obj.SMVAAAB012;
            SMVAAAB013.ValueText = obj.SMVAAAB013;
            SMVAAAB014.ValueText = obj.SMVAAAB014;
            /*
            bool isAddNew = (bool)getSession("isNew");
            if (!isAddNew)
            {
                SMVAAAB002.ReadOnly = true;
            }
            */
        }
        catch (Exception ze)
        {
            Response.Write("alert('"+ze.Message+"');");
        }
    }

    protected override void saveData(DataObject objects)
    {

        bool isAddNew = (bool)getSession("isNew");
        if (!SMVAAAB007.Checked)
        {
            try
            {
                int x = int.Parse(SMVAAAB005.ValueText);
                if (x < 0)
                {
                    throw new Exception("error");
                }
            }
            catch
            {
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvb_detail_aspx.language.ini", "message", "QueryError1", "視窗寬度必須為大於0的整數"));
                return;
            }
            try
            {
                int x = int.Parse(SMVAAAB006.ValueText);
                if (x < 0)
                {
                    throw new Exception("error");
                }
            }
            catch
            {
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvb_detail_aspx.language.ini", "message", "QueryError2", "視窗高度必須為大於0的整數"));
                return;
            }
        }
        SMVAAAB obj = (SMVAAAB)objects;
        if (isAddNew)
        {
            obj.SMVAAAB001 = IDProcessor.getID("");
        }
        obj.SMVAAAB002 = SMVAAAB002.ValueText;
        obj.SMVAAAB003 = SMVAAAB003.ValueText.Replace("'","-").Replace("\"","-");
        obj.SMVAAAB004 = SMVAAAB004.ValueText;
        obj.SMVAAAB005 = SMVAAAB005.ValueText;
        obj.SMVAAAB006 = SMVAAAB006.ValueText;

        if (SMVAAAB007.Checked)
        {
            obj.SMVAAAB007 = "Y";
        }
        else
        {
            obj.SMVAAAB007 = "N";
        }
        if (SMVAAAB008.Checked)
        {
            obj.SMVAAAB008 = "Y";
        }
        else
        {
            obj.SMVAAAB008 = "N";
        }
        obj.SMVAAAB009 = SMVAAAB009.ValueText;
        obj.SMVAAAB010 = SMVAAAB010.ValueText;
        obj.SMVAAAB011 = SMVAAAB011.ValueText;
        obj.SMVAAAB012 = SMVAAAB012.ValueText;
        obj.SMVAAAB013 = SMVAAAB013.ValueText;
        obj.SMVAAAB014 = SMVAAAB014.ValueText;
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVBAgent agent = new SMVBAgent();
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
    protected void SMVAAAB007_Click(object sender, EventArgs e)
    {
        if (SMVAAAB007.Checked)
        {
            SMVAAAB005.ValueText = "0";
            SMVAAAB006.ValueText = "0";
            SMVAAAB005.ReadOnly = true;
            SMVAAAB006.ReadOnly = true;
        }
        else
        {
            SMVAAAB005.ReadOnly = false;
            SMVAAAB006.ReadOnly = false;
        }

    }
}
