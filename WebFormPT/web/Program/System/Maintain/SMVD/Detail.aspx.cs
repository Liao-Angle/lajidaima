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
using WebServerProject.maintain.SMVD;

public partial class Program_System_Maintain_SMVD_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVD";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");
        if (!isAddNew)
        {
            SMVDAAA003.ReadOnly = true;
            SMVDAAA004.ReadOnly = true;
            SMVDAAA007.ReadOnly = true;
            SMVDAAA008.ReadOnly = true;
            SMVDAAA009.ReadOnly = true;

        }
        string[,] ids = new string[,]{
                {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvd_detail_aspx.language.ini", "message", "ids0", "一般訊息")},
                {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvd_detail_aspx.language.ini", "message", "ids1", "重要訊息")},
                {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvd_detail_aspx.language.ini", "message", "ids2", "行事曆通知")}
            };
        SMVDAAA003.setListItem(ids);

        SMVDAAA obj = (SMVDAAA)objects;
        SMVDAAA003.ValueText = obj.SMVDAAA003;
        SMVDAAA004.ValueText = obj.SMVDAAA004;
        SMVDAAA007.ValueText = obj.SMVDAAA007;
        SMVDAAA008.ValueText = obj.SMVDAAA008;
        SMVDAAA009.ValueText = obj.SMVDAAA009;

        if (isAddNew)
        {
            SMVDAAA004.ValueText = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
            DataObjectSet dos = null;
            SMVDAgent agent = new SMVDAgent();
            dos = agent.createEmptyDataObjectSet("SMVDAAB");
            obj.setChild("SMVDAAB", dos);
        }

        DataObjectSet child = obj.getChild("SMVDAAB");
        ReadList.HiddenField = new string[] { "SMVDAAB001", "SMVDAAB002" };
        ReadList.dataSource = child;
        ReadList.updateTable();
    }

    protected override void saveData(DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");

        SMVDAAA obj = (SMVDAAA)objects;
        if (isAddNew)
        {
            obj.SMVDAAA001 = IDProcessor.getID("");
        }
        obj.SMVDAAA003 = SMVDAAA003.ValueText;
        obj.SMVDAAA004 = SMVDAAA004.ValueText;
        obj.SMVDAAA007 = SMVDAAA007.ValueText;
        obj.SMVDAAA008 = SMVDAAA008.ValueText;
        obj.SMVDAAA009 = SMVDAAA009.ValueText;
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVDAgent agent = new SMVDAgent();
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
}
