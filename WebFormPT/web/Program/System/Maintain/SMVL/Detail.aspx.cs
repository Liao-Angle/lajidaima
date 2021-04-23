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
using WebServerProject.maintain.SMVL;

public partial class Program_System_Maintain_SMVL_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVL";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        SMVLAAA obj = (SMVLAAA)objects;
        SMVLAAA003.ValueText = obj.SMVLAAA003;
        
    }
    protected override void saveData(DataObject objects)
    {
        SMVLAAA obj = (SMVLAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVLAAA001 = IDProcessor.getID("");
            obj.SMVLAAA002 = (string)Session["UserID"];
        }
        obj.SMVLAAA003 = SMVLAAA003.ValueText;
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVLAgent agent = new SMVLAgent();
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
    protected override void afterSetReadOnlyForm(DataObject objects)
    {
        //SMVLAAA003.ReadOnly = false;
    }
}
