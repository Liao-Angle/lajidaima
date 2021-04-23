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
using com.dsc.kernal.databean;
using WebServerProject.maintain.SMVR;

public partial class Program_System_Maintain_SMVRC_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
        maintainIdentity = "SMVRC";
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string[,] ids = new string[12, 2];
                for (int i = 0; i < ids.GetLength(0); i++)
                {
                    ids[i, 0] = string.Format("{0:00}", i + 1);
                    ids[i, 1] = ids[i, 0];
                }
                SMVRAAC0031.setListItem(ids);
                ids = new string[31, 2];
                for (int i = 0; i < ids.GetLength(0); i++)
                {
                    ids[i, 0] = string.Format("{0:00}", i + 1);
                    ids[i, 1] = ids[i, 0];
                }
                SMVRAAC0032.setListItem(ids);

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                SMVRCAgent agent = new SMVRCAgent();
                agent.engine = engine;
                agent.query("");
                engine.close();

                DataObjectSet dos = agent.defaultData;

                ACTable.HiddenField = new string[] { "SMVRAAC001" };
                ACTable.dataSource = dos;
                ACTable.updateTable();
            }
        }
    }
    protected void ACTable_ShowRowData(com.dsc.kernal.databean.DataObject objects)
    {
        SMVRAAC ac = (SMVRAAC)objects;
        SMVRAAC002.ValueText = ac.SMVRAAC002;
        string[] tag = ac.SMVRAAC003.Split(new char[] { '/' });
        SMVRAAC0031.ValueText = tag[0];
        SMVRAAC0032.ValueText = tag[1];
    }
    protected bool ACTable_SaveRowData(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
        if (SMVRAAC002.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvrc_maintain_aspx.language.ini", "message", "QueryError1", "請填寫國定假日名稱"));
            return false;
        }

        SMVRAAC ac = (SMVRAAC)objects;
        if (isNew)
        {
            ac.SMVRAAC001 = com.dsc.kernal.utility.IDProcessor.getID("");
        }
        ac.SMVRAAC002 = SMVRAAC002.ValueText;
        ac.SMVRAAC003 = SMVRAAC0031.ValueText + "/" + SMVRAAC0032.ValueText;


        return true;
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

            SMVRCAgent agent = new SMVRCAgent();
            agent.engine = engine;
            agent.defaultData = ACTable.dataSource;
            if (!agent.update())
            {
                throw new Exception(engine.errorString);
            }
            engine.close();
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvrc_maintain_aspx.language.ini", "message", "QueryError2", "儲存成功"));
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            writeLog(te);
            throw te;
        }
    }
}
