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
using WebServerProject.maintain.SMVF;

public partial class Program_System_Maintain_SMVF_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVF";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        string[,] ids = new string[,]{
                {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvf_detail_aspx.language.ini", "message", "idsY", "是")},
                {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvf_detail_aspx.language.ini", "message", "idsN", "否")}
            };
        SMVFAAA004.setListItem(ids);

        SMVFAAA obj = (SMVFAAA)objects;
        SMVFAAA002.ValueText = obj.SMVFAAA002;
        SMVFAAA003.ValueText = obj.SMVFAAA003;
        SMVFAAA004.ValueText = obj.SMVFAAA004;
        SMVFAAA005.ValueText = obj.SMVFAAA005;
        SMVFAAA006.ValueText = obj.SMVFAAA006;
        SMVFAAA007.ValueText = obj.SMVFAAA007;
        
    }
    private bool checkRGB(string vle)
    {
        try
        {
            int z = int.Parse(vle);
            if ((z < 0) || (z > 255))
            {
                throw new Exception("error");
            }
            return true;
        }
        catch (Exception te)
        {
            return false;
        }
    }
    protected override void saveData(DataObject objects)
    {
        if (!checkRGB(SMVFAAA005.ValueText))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvf_detail_aspx.language.ini", "message", "QueryError1", "顏色R(0-255)輸入不合法"));
        }
        if (!checkRGB(SMVFAAA006.ValueText))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvf_detail_aspx.language.ini", "message", "QueryError2", "顏色G(0-255)輸入不合法"));
        }
        if (!checkRGB(SMVFAAA007.ValueText))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvf_detail_aspx.language.ini", "message", "QueryError3", "顏色B(0-255)輸入不合法"));
        }
        SMVFAAA obj = (SMVFAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVFAAA001 = IDProcessor.getID("");
        }

        obj.SMVFAAA002 = SMVFAAA002.ValueText;
        obj.SMVFAAA003 = SMVFAAA003.ValueText;
        obj.SMVFAAA004 = SMVFAAA004.ValueText;
        obj.SMVFAAA005 = SMVFAAA005.ValueText;
        obj.SMVFAAA006 = SMVFAAA006.ValueText;
        obj.SMVFAAA007 = SMVFAAA007.ValueText;
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVFAgent agent = new SMVFAgent();
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
