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
using WebServerProject;
using WebServerProject.maintain.SMVE;

public partial class Program_System_Maintain_SMVE_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVE";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
        if (!IsPostBack) {
            if (!IsProcessEvent) {
                //2011 0902 Eric Hsu : Because RichEdit has not support Brower except for IE
                com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);                
                if (resultType != BrowserProcessor.BrowserType.IE) {
                    Response.Clear();
                    Response.Write("This page only support IE Browser");
                    Response.End();
                }                            
            }
        }
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        string[,] ids = new string[,]{
                {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smve_detail_aspx.language.ini", "message", "idsN", "否")},
                {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smve_detail_aspx.language.ini", "message", "idsY", "是")}
            };
        SMVEAAA004.setListItem(ids);
        SMVEAAA008.setListItem(ids);

        SMVEAAA007.clientEngineType = engineType;
        SMVEAAA007.connectDBString = connectString;

        SMVEAAA obj = (SMVEAAA)objects;
        SMVEAAA002.ValueText = obj.SMVEAAA002;
        SMVEAAA003.ValueText = obj.SMVEAAA003;
        SMVEAAA004.ValueText = obj.SMVEAAA004;
        SMVEAAA005.ValueText = obj.SMVEAAA005;
        SMVEAAA006.ValueText = obj.SMVEAAA006;
        SMVEAAA007.GuidValueText = obj.SMVEAAA007;
        SMVEAAA007.doGUIDValidate();
        SMVEAAA008.ValueText = obj.SMVEAAA008;


        //初始化附件設定
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        SysParam sp = new SysParam(engine);
        string fileAdapter = sp.getParam("FileAdapter");
        attachFile.engine = engine;
        attachFile.FileAdapter = fileAdapter;
        attachFile.tempFolder = Server.MapPath("~/tempFolder/");
        bool isAddNew = (bool)getSession("isNew");
        if (isAddNew)
        {
            attachFile.readFile("");
        }
        else
        {
            attachFile.readFile(obj.SMVEAAA001);
        }
        attachFile.updateTable();
        engine.close();
    }

    protected override void saveData(DataObject objects)
    {
        SMVEAAA obj = (SMVEAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVEAAA001 = IDProcessor.getID("");
        }

        obj.SMVEAAA002 = SMVEAAA002.ValueText;
        obj.SMVEAAA003 = SMVEAAA003.ValueText;
        obj.SMVEAAA004 = SMVEAAA004.ValueText;
        obj.SMVEAAA005 = SMVEAAA005.ValueText;
        obj.SMVEAAA006 = SMVEAAA006.ValueText;
        obj.SMVEAAA007 = SMVEAAA007.GuidValueText;
        obj.SMVEAAA008 = SMVEAAA008.ValueText;
        if (attachFile.dataSource.getAvailableDataObjectCount() > 0)
        {
            obj.SMVEAAA009 = "Y";
        }
        else
        {
            obj.SMVEAAA009 = "N";
        }

    }
    protected void ToAllButton_Click(object sender, EventArgs e)
    {
        SMVEAAA007.GuidValueText = "";
        SMVEAAA007.doGUIDValidate();
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVEAgent agent = new SMVEAgent();
        agent.engine = engine;
        agent.query("1=2");

        //取得資料
        bool result = agent.defaultData.add(objects);

        SMVEAAA obj = (SMVEAAA)objects;
        attachFile.engine = engine;
        attachFile.setJobID(obj.SMVEAAA001);
        attachFile.confirmSave("SMVE", obj.SMVEAAA001);
        attachFile.saveFile();


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
