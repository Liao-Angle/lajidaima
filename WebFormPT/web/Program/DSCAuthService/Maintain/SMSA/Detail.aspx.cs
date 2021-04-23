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
using WebServerProject.auth.SMSA;

public partial class Program_DSCAuthService_Maintain_SMSA_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        try
        {
            connectString = (string)Session["connectString"];
            engineType = (string)Session["engineType"];


            SMSAAAA obj = (SMSAAAA)objects;
            SMSAAAA002.ValueText = obj.SMSAAAA002;
            SMSAAAA003.ValueText = obj.SMSAAAA003;
            SMSAAAA004.ValueText = obj.SMSAAAA004;

        }
        catch (Exception ze)
        {
            Response.Write("alert('"+ze.Message+"');");
        }
    }

    protected override void saveData(DataObject objects)
    {

        bool isAddNew = (bool)getSession("isNew");
        SMSAAAA obj = (SMSAAAA)objects;
        if (isAddNew)
        {
            obj.SMSAAAA001 = IDProcessor.getID("");
        }
        obj.SMSAAAA002 = SMSAAAA002.ValueText;
        obj.SMSAAAA003 = SMSAAAA003.ValueText;
        obj.SMSAAAA004 = SMSAAAA004.ValueText;

    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMSAAgent agent = new SMSAAgent();
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
