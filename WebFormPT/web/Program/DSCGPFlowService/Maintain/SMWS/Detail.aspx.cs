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
using WebServerProject.flow.SMWS;
using System.Xml;
using MIL.Html;

public partial class Program_DSCGPFlowService_Maintain_SMWS_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWS";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {

        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[,] ids = new string[ds.Tables[0].Rows.Count, 2];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
        }
        SMWSAAB003.setListItem(ids);

        sql = "select SMWCAAA002 from SMWCAAA";
        ds = engine.getDataSet(sql, "TEMP");
        ids = new string[ds.Tables[0].Rows.Count, 2];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ids[i, 0];
        }
        SMWSAAC003.setListItem(ids);

        SMWSAAA obj = (SMWSAAA)objects;
        SMWSAAA002.ValueText = obj.SMWSAAA002;
        SMWSAAA003.ValueText = obj.SMWSAAA003;
        if (obj.SMWSAAA004.Equals("0"))
        {
            SMWSAAA0040.Checked = true;
        }
        else
        {
            SMWSAAA0041.Checked = true;
        }
        if (obj.SMWSAAA005.Equals("0"))
        {
            SMWSAAA0050.Checked = true;
        }
        else
        {
            SMWSAAA0051.Checked = true;
        }

        bool isAddNew = (bool)getSession("isNew");
        DataObjectSet child = null;
        DataObjectSet childc = null;
        if (isAddNew)
        {
            child = new DataObjectSet();
            child.setAssemblyName("WebServerProject");
            child.setChildClassString("WebServerProject.flow.SMWS.SMWSAAB");
            child.setTableName("SMWSAAB");
            obj.setChild("SMWSAAB", child);

            childc = new DataObjectSet();
            childc.setAssemblyName("WebServerProject");
            childc.setChildClassString("WebServerProject.flow.SMWS.SMWSAAC");
            childc.setTableName("SMWSAAC");
            obj.setChild("SMWSAAC", childc);
        }
        else
        {
            child = obj.getChild("SMWSAAB");
            childc = obj.getChild("SMWSAAC");
        }
        ABTable.HiddenField = new string[] { "SMWSAAB001", "SMWSAAB002" };
        ABTable.dataSource = child;
        ABTable.updateTable();

        ACTable.HiddenField = new string[] { "SMWSAAC001", "SMWSAAC002" };
        ACTable.dataSource = childc;
        ACTable.updateTable();
    }
    protected override void saveData(DataObject objects)
    {
        SMWSAAA obj = (SMWSAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMWSAAA001 = IDProcessor.getID("");
        }

        obj.SMWSAAA002 = SMWSAAA002.ValueText;
        obj.SMWSAAA003 = SMWSAAA003.ValueText;
        if (SMWSAAA0040.Checked)
        {
            obj.SMWSAAA004 = "0";
        }
        else
        {
            obj.SMWSAAA004 = "1";
        }
        if (SMWSAAA0050.Checked)
        {
            obj.SMWSAAA005 = "0";
        }
        else
        {
            obj.SMWSAAA005 = "1";
        }

        DataObjectSet child = ABTable.dataSource;
        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMWSAAB ab = (SMWSAAB)child.getAvailableDataObject(i);
            ab.SMWSAAB002 = obj.SMWSAAA001;
        }
        DataObjectSet childc = ACTable.dataSource;
        for (int i = 0; i < childc.getAvailableDataObjectCount(); i++)
        {
            SMWSAAC ab = (SMWSAAC)childc.getAvailableDataObject(i);
            ab.SMWSAAC002 = obj.SMWSAAA001;
        }
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWSAgent agent = new SMWSAgent();
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
    protected bool ABTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWSAAB ab = (SMWSAAB)objects;

        if (isNew)
        {
            ab.SMWSAAB001 = IDProcessor.getID("");
            ab.SMWSAAB002 = "TEMP";
        }
        ab.SMWSAAB003 = SMWSAAB003.ValueText;
        ab.SMWSAAB004 = SMWSAAB003.ReadOnlyText;
        return true;
    }
    protected void ABTable_ShowRowData(DataObject objects)
    {
        SMWSAAB ab = (SMWSAAB)objects;

        SMWSAAB003.ValueText = ab.SMWSAAB003;
    }
    protected bool ACTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWSAAC ab = (SMWSAAC)objects;

        if (isNew)
        {
            ab.SMWSAAC001 = IDProcessor.getID("");
            ab.SMWSAAC002 = "TEMP";
        }
        ab.SMWSAAC003 = SMWSAAC003.ValueText;
        return true;
    }
    protected void ACTable_ShowRowData(DataObject objects)
    {
        SMWSAAC ab = (SMWSAAC)objects;

        SMWSAAC003.ValueText = ab.SMWSAAC003;
    }
}
