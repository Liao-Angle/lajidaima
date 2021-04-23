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
using WebServerProject.flow.SMWC;
using System.Xml;
using MIL.Html;

public partial class Program_DSCGPFlowService_Maintain_SMWC_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWC";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        string[,] ids = new string[,]{
            {"A",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwc_detail_aspx.language.ini", "message", "idsA", "系統定義")},
            {"F",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwc_detail_aspx.language.ini", "message", "idsF", "流程引擎")}
        };
        SMWCAAA003.setListItem(ids);

        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];


        SMWCAAA obj = (SMWCAAA)objects;
        SMWCAAA002.ValueText = obj.SMWCAAA002;
        SMWCAAA003.ValueText = obj.SMWCAAA003;

        SMWCAAA003.ReadOnly = true;

        bool isAddNew = (bool)getSession("isNew");
        if (isAddNew)
        {
            SMWCAAA003.ValueText = "A";
        }

    }
    protected override void saveData(DataObject objects)
    {
        SMWCAAA obj = (SMWCAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMWCAAA001 = IDProcessor.getID("");
        }

        obj.SMWCAAA002 = SMWCAAA002.ValueText;
        obj.SMWCAAA003 = SMWCAAA003.ValueText;

    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWCAgent agent = new SMWCAgent();
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
    protected override void afterDelete(DataObject objects)
    {
        SMWCAAA aa = (SMWCAAA)objects;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "delete from SMWDAAD where SMWDAAD003='" + Utility.filter(aa.SMWCAAA002) + "'";
        if (!engine.executeSQL(sql))
        {
            engine.close();
            throw new Exception(engine.errorString);
        }
        engine.close();
    }
}
