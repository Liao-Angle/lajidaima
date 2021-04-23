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
using WebServerProject.audit.SMXA;

public partial class Program_DSCAuditService_Maintain_SMXA_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMXA";
        ApplicationID = "SYSTEM";
        ModuleID = "SMXAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        try
        {
            connectString = (string)Session["connectString"];
            engineType = (string)Session["engineType"];


            SMXAAAA obj = (SMXAAAA)objects;
            SMXAAAA002.ValueText = obj.SMXAAAA002;
            SMXAAAA003.ValueText = obj.SMXAAAA003;

        }
        catch (Exception ze)
        {
            Response.Write("alert('"+ze.Message+"');");
        }
    }

    protected override void saveData(DataObject objects)
    {

        bool isAddNew = (bool)getSession("isNew");
        SMXAAAA obj = (SMXAAAA)objects;
        if (isAddNew)
        {
            obj.SMXAAAA001 = IDProcessor.getID("");
        }
        obj.SMXAAAA002 = SMXAAAA002.ValueText;
        obj.SMXAAAA003 = SMXAAAA003.ValueText;

    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMXAAgent agent = new SMXAAgent();
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
