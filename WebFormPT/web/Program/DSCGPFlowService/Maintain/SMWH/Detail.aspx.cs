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
using WebServerProject.flow.SMWH;
using System.Xml;
using MIL.Html;

public partial class Program_DSCGPFlowService_Maintain_SMWH_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWH";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        string[,] ids = new string[,]{
            {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwh_detail_aspx.language.ini", "message", " idsY", "完成")},
            {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwh_detail_aspx.language.ini", "message", " idsN", "終止")}
        };
        SMWHAAB005.setListItem(ids);

        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];


        SMWHAAA obj = (SMWHAAA)objects;
        SMWHAAA002.ValueText = obj.SMWHAAA002;
        SMWHAAA003.ValueText = obj.SMWHAAA003;
        SMWHAAA004.ValueText = obj.SMWHAAA004;

        bool isAddNew = (bool)getSession("isNew");
        DataObjectSet child = null;
        if (isAddNew)
        {
            child = new DataObjectSet();
            child.setAssemblyName("WebServerProject");
            child.setChildClassString("WebServerProject.flow.SMWH.SMWHAAB");
            child.setTableName("SMWHAAB");
            obj.setChild("SMWHAAB", child);
        }
        else
        {
            child = obj.getChild("SMWHAAB");
        }
        ListTable.HiddenField = new string[] { "SMWHAAB001", "SMWHAAB002" };
        ListTable.dataSource = child;
        ListTable.updateTable();
    }
    protected override void saveData(DataObject objects)
    {
        SMWHAAA obj = (SMWHAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMWHAAA001 = IDProcessor.getID("");
        }

        obj.SMWHAAA002 = SMWHAAA002.ValueText;
        obj.SMWHAAA003 = SMWHAAA003.ValueText;
        obj.SMWHAAA004 = SMWHAAA004.ValueText;

        DataObjectSet child = ListTable.dataSource;
        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMWHAAB ab = (SMWHAAB)child.getAvailableDataObject(i);
            ab.SMWHAAB002 = obj.SMWHAAA001;
        }
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWHAgent agent = new SMWHAgent();
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
    protected void ListTable_ShowRowData(DataObject objects)
    {
        SMWHAAB ab = (SMWHAAB)objects;

        SMWHAAB003.ValueText = ab.SMWHAAB003;
        SMWHAAB004.ValueText = ab.SMWHAAB004;
        SMWHAAB005.ValueText = ab.SMWHAAB005;
    }
    protected bool ListTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWHAAB ab = (SMWHAAB)objects;

        if (isNew)
        {
            ab.SMWHAAB001 = IDProcessor.getID("");
            ab.SMWHAAB002 = "TEMP";
        }
        ab.SMWHAAB003 = SMWHAAB003.ValueText;
        ab.SMWHAAB004 = SMWHAAB004.ValueText;
        ab.SMWHAAB005 = SMWHAAB005.ValueText;
        return true;
    }
}
