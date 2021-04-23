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

public partial class Program_System_Maintain_SMVEU_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVEU";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
        SaveButton.Display = false;
        ResetButton.Display = false;
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        SMVEAAA obj = (SMVEAAA)objects;
        SMVEAAA002.ValueText = obj.SMVEAAA002;
        SMVEAAA003.ValueText = obj.SMVEAAA003;
        SMVEAAA005.ValueText = obj.SMVEAAA005;
        SMVEAAA006.ValueText = obj.SMVEAAA006;

        if (obj.SMVEAAA009.Equals("Y"))
        {
            DSCLabel8.Display = true;
            attachFile.Display = true;

            //初始化附件設定
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            SysParam sp = new SysParam(engine);
            string fileAdapter = sp.getParam("FileAdapter");
            attachFile.engine = engine;
            attachFile.FileAdapter = fileAdapter;
            attachFile.tempFolder = Server.MapPath("~/tempFolder/");
            attachFile.readFile(obj.SMVEAAA001);
            attachFile.updateTable();
            attachFile.NoAdd = true;
            attachFile.NoDelete = true;
            engine.close();
        }
    }

}
