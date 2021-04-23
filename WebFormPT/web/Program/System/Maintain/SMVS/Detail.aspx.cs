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
using WebServerProject.maintain.SMVS;

public partial class Program_System_Maintain_SMVS_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVS";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        SMVSAAA005.clientEngineType = engineType;
        SMVSAAA005.connectDBString = connectString;

        SMVSAAA obj = (SMVSAAA)objects;
        SMVSAAA002.ValueText = obj.SMVSAAA002;
        SMVSAAA003.ValueText = obj.SMVSAAA003;
        if (obj.SMVSAAA004.Equals("Y"))
        {
            SMVSAAA004.Checked = true;
        }
        else
        {
            SMVSAAA004.Checked = false;
        }
        SMVSAAA005.ValueText = obj.SMVSAAA005;
        SMVSAAA005.doValidate();
    }
    protected override void saveData(DataObject objects)
    {
        if (SMVSAAA002.ValueText.Equals(""))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvs_detail_aspx.language.ini", "message", "QueryError1", "必需填寫下載標題"));
        }
        if (SMVSAAA003.ValueText.Equals(""))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvs_detail_aspx.language.ini", "message", "QueryError2", "必需填寫HTML"));
        }
        if (SMVSAAA005.ValueText.Equals(""))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvs_detail_aspx.language.ini", "message", "QueryError3", "必需選擇權限項目"));
        }

        SMVSAAA obj = (SMVSAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVSAAA001 = IDProcessor.getID("");
        }
        obj.SMVSAAA002 = SMVSAAA002.ValueText;
        obj.SMVSAAA003 = SMVSAAA003.ValueText;
        if (SMVSAAA004.Checked)
        {
            obj.SMVSAAA004 = "Y";
        }
        else
        {
            obj.SMVSAAA004 = "N";
        }
        obj.SMVSAAA005 = SMVSAAA005.ValueText;
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVSAgent agent = new SMVSAgent();
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
