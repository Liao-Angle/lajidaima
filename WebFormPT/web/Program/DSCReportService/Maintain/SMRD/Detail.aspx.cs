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
using WebServerProject.report.SMRD;
using WebServerProject;
using com.dsc.kernal.document;
using ReportGenerator.format;

public partial class Program_DSCReportService_Maintain_SMRD_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMRD";
        ApplicationID = "SYSTEM";
        ModuleID = "SMRDA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");
        SMRDAAA obj = (SMRDAAA)objects;
       
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        SMRDAAA002.connectDBString = connectString;

        if (isAddNew)
        {
        }
        else
        {
        }

        SMRDAAA002.ValueText = obj.SMRDAAA002;
        SMRDAAA002.doValidate();
        SMRDAAA003.ValueText = obj.SMRDAAA003;

        SMRDAAA004.ValueText = "";
        if (obj.SMRDAAA004.Equals(""))
        {
            NoPWD.Checked = true;
        }
        else
        {
            NoPWD.Checked = false;
        }
    }
    protected override void saveData(DataObject objects)
    {
        if (SMRDAAA002.ValueText.Equals(""))
        {
            //throw new Exception("必需填寫格式定義代號");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smrd_detail_aspx.language.ini", "message", "QueryError1", "必需填寫權限群組代號"));
        }
        if (SMRDAAA003.ValueText.Equals(""))
        {
            //throw new Exception("必需填寫格式定義名稱");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smrd_detail_aspx.language.ini", "message", "QueryError2", "必需填寫登入帳號"));
        }

        SMRDAAA obj = (SMRDAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMRDAAA001 = IDProcessor.getID("");
        }
        obj.SMRDAAA002 = SMRDAAA002.ValueText;
        obj.SMRDAAA003 = SMRDAAA003.ValueText;

        if (SMRDAAA004.ValueText.Equals(""))
        {
            if (isAddNew)
            {
                obj.SMRDAAA004 = "";
            }
            else if (NoPWD.Checked)
            {
                obj.SMRDAAA004 = "";
            }
        }
        else
        {
            WebServerProject.SysParam sp = new WebServerProject.SysParam(null);
            obj.SMRDAAA004 = sp.encode(SMRDAAA004.ValueText);
        }
    }
    protected override void saveDB(DataObject objects)
    {
        SMRDAAA obj = (SMRDAAA)objects;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);


        //修改:設定所使用的agent
        SMRDAgent agent = new SMRDAgent();
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
    protected void NoPWD_Click(object sender, EventArgs e)
    {
        if (NoPWD.Checked)
        {
            SMRDAAA004.ValueText = "";
        }
    }
}
