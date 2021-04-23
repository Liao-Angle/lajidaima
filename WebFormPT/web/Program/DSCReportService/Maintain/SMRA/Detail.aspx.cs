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
using WebServerProject.report.SMRA;
using WebServerProject;
using com.dsc.kernal.document;
using ReportGenerator.format;

public partial class Program_DSCReportService_Maintain_SMRA_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMRA";
        ApplicationID = "SYSTEM";
        ModuleID = "SMRAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");
        SMRAAAA obj = (SMRAAAA)objects;
       
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //設定上傳或下載檔案所使用的Adapter.預設使用com.dsc.kernal.document.LocalFileAdapter
        SysParam sp = new SysParam(engine);
        string fileAdapter = sp.getParam("FileAdapter");
        ReportFile.FileAdapter = fileAdapter;
        //設定元件所使用的engine
        ReportFile.engine = engine;
        //設定上傳或下載檔案暫時路徑
        ReportFile.tempFolder = Server.MapPath("~/tempFolder/");
        //初始化 新增狀態

        if (isAddNew)
        {
            ReportFile.readFile("");      //於狀態不同中擇一選擇
            SMRAAAA002.ReadOnly = false;
        }
        else
        {
            //初始化 流程中修改資料
            ReportFile.readFile(obj.SMRAAAA001);
            //設定所有檔案物件的JobID.
            ReportFile.setJobID(obj.SMRAAAA001);
            SMRAAAA002.ReadOnly = true;
        }

        SMRAAAA002.ValueText = obj.SMRAAAA002;
        SMRAAAA003.ValueText = obj.SMRAAAA003;

        SMRAAAA004.ValueText = obj.SMRAAAA004;
        SMRAAAA005.ValueText = obj.SMRAAAA005;

        if (obj.SMRAAAA008.Equals("Y"))
        {
            SMRAAAA008.Checked = true;
        }
        else
        {
            SMRAAAA008.Checked = false;
        }
        engine.close();

    }
    protected override void saveData(DataObject objects)
    {
        if (SMRAAAA002.ValueText.Equals(""))
        {
            //throw new Exception("必需填寫格式定義代號");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smra_detail_aspx.language", "message", "QueryError1", "必需填寫格式定義代號"));
        }
        if (SMRAAAA003.ValueText.Equals(""))
        {
            //throw new Exception("必需填寫格式定義名稱");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smra_detail_aspx.language", "message", "QueryError2", "必需填寫格式定義名稱"));
        }
        if (ReportFile.dataSource.getAvailableDataObjectCount() != 1)
        {
            //throw new Exception("必須上傳且僅能上傳一個格式檔案");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smra_detail_aspx.language", "message", "QueryError3", "必須上傳且僅能上傳一個格式檔案"));
        }
        SMRAAAA obj = (SMRAAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMRAAAA001 = IDProcessor.getID("");
        }
        obj.SMRAAAA002 = SMRAAAA002.ValueText;
        obj.SMRAAAA003 = SMRAAAA003.ValueText;
        obj.SMRAAAA004 = ReportFile.dataSource.getAvailableDataObject(0).getData("FILEEXT").ToUpper();
        obj.SMRAAAA005 = SMRAAAA005.ValueText;

        obj.SMRAAAA007 = (String)Session["UserGUID"];
        if (SMRAAAA008.Checked)
        {
            obj.SMRAAAA008 = "Y";
        }
        else
        {
            obj.SMRAAAA008 = "N";
        }
    }
    protected override void saveDB(DataObject objects)
    {
        SMRAAAA obj = (SMRAAAA)objects;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        ReportFile.engine = engine;
        ReportFile.setJobID(obj.SMRAAAA001);
        ReportFile.confirmSave("ReportTemplate", obj.SMRAAAA002);

        obj.SMRAAAA006=ReportFile.dataSource.getAvailableDataObject(0).getData("GUID");

        //修改:設定所使用的agent
        SMRAAgent agent = new SMRAAgent();
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
            ReportFile.saveFile();
            engine.close();
            if (!result)
            {
                throw new Exception(engine.errorString);
            }
        }
    }
    protected void AnaButton_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string filepath = "";

        if (ReportFile.dataSource.getAvailableDataObjectCount() == 0)
        {
            //MessageBox("請選擇一報表檔案");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smra_detail_aspx.language", "message", "QueryError4", "請選擇一報表檔案"));
            SMRAAAA004.ValueText = "";
            SMRAAAA005.ValueText = "";
            return;
        }
        DSCWebControl.FileItem fi = (DSCWebControl.FileItem)ReportFile.dataSource.getAvailableDataObject(0);
        if (ReportFile.dataSource.getAvailableDataObject(0).getData("FILEPATH").Equals(""))
        {

            SysParam sp = new SysParam(engine);
            string fileAdapter = sp.getParam("FileAdapter");
            com.dsc.kernal.document.DocumentAdapterFactory daf = new DocumentAdapterFactory();
            AbstractDocumentAdapter adp=daf.getDocumentAdapter(fileAdapter.Split(new char[] { '.' })[0], fileAdapter);

            filepath = IDProcessor.getID("") + "." + fi.FILEEXT;
            filepath = Server.MapPath("~/tempFolder/" + filepath);

            adp.getFile(filepath, fi.LEVEL1, fi.LEVEL2, fi.GUID);
        }
        else
        {
            filepath = Server.MapPath("~/tempFolder/" + ReportFile.dataSource.getAvailableDataObject(0).getData("FILEPATH"));
        }
        
        engine.close();

        string fileext = fi.FILEEXT.ToUpper();

        AbstractReportGenerator arg = null;
        ReportGeneratorFactory rf = new ReportGeneratorFactory();

        if (fileext.Equals("XLS"))
        {
            arg = rf.getReportGenerator(ReportGeneratorFactory.EXCEL);
        }
        else if (fileext.Equals("DOC"))
        {
            arg = rf.getReportGenerator(ReportGeneratorFactory.WORD);
        }
        else
        {
            SMRAAAA004.ValueText = "";
            SMRAAAA005.ValueText = "";
            //MessageBox("未知的報表格式定義檔案");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscreportservice_maintain_smra_detail_aspx.language", "message", "QueryError5", "未知的報表格式定義檔案"));
            return;
        }
        try
        {
            string xml = arg.getFormatXML(filepath);
            SMRAAAA005.ValueText = xml;
        }
        catch (Exception te)
        {
            MessageBox(te.Message);
            SMRAAAA005.ValueText = "";
            base.writeLog(te);
        }
        
    }
    protected void ReportFile_DeleteData()
    {
        AnaButton_Click(null, null);
    }
}
