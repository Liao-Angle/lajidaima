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
using WebServerProject.maintain.SMVC;

public partial class Program_System_Maintain_SMVC_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVC";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        string[,] ids = new string[,]{
                {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvc_detail_aspx.language.ini", "message", "idsN", "否")},
                {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvc_detail_aspx.language.ini", "message", "idsY", "是")}
            };
        SMVCAAA005.setListItem(ids);
        SMVCAAA006.setListItem(ids);

        ids = new string[,]{
                {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvc_detail_aspx.language.ini", "message", "ids0", "一般訊息")},
                {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvc_detail_aspx.language.ini", "message", "ids1", "重要訊息")},
                {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvc_detail_aspx.language.ini", "message", "ids2", "行事曆通知")}
            };
        SMVCAAA003.setListItem(ids);

        SMVCAAA obj = (SMVCAAA)objects;
        SMVCAAA003.ValueText = obj.SMVCAAA003;
        SMVCAAA004.ValueText = obj.SMVCAAA004;
        SMVCAAA005.ValueText = obj.SMVCAAA005;
        SMVCAAA006.ValueText = obj.SMVCAAA006;
        SMVCAAA007.ValueText = obj.SMVCAAA007;
        SMVCAAA008.ValueText = obj.SMVCAAA008;
        SMVCAAA009.ValueText = obj.SMVCAAA009;

        obj.SMVCAAA005 = "Y";
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVCAgent agent = new SMVCAgent();
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
