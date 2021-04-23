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
using WebServerProject.flow.SMWT;
using System.Xml;
using MIL.Html;

public partial class Program_DSCGPFlowService_Maintain_SMWT_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWT";
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
        SMWTAAB003.setListItem(ids);

        sql = "select SMWCAAA002 from SMWCAAA";
        ds = engine.getDataSet(sql, "TEMP");
        ids = new string[ds.Tables[0].Rows.Count+1, 2];
        ids[0, 0] = "";
        ids[0, 1] = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ids[i+1, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i+1, 1] = ids[i, 0];
        }
        SMWTAAC003.setListItem(ids);

        SMWTAAA obj = (SMWTAAA)objects;
        SMWTAAA002.ValueText = obj.SMWTAAA002;
        SMWTAAA003.ValueText = obj.SMWTAAA003;
        if (obj.SMWTAAA004.Equals("0"))
        {
            SMWTAAA0040.Checked = true;
        }
        else
        {
            SMWTAAA0041.Checked = true;
        }
        if (obj.SMWTAAA005.Equals("0"))
        {
            SMWTAAA0050.Checked = true;
        }
        else
        {
            SMWTAAA0051.Checked = true;
        }

        bool isAddNew = (bool)getSession("isNew");
        DataObjectSet child = null;
        DataObjectSet childc = null;
        if (isAddNew)
        {
            child = new DataObjectSet();
            child.setAssemblyName("WebServerProject");
            child.setChildClassString("WebServerProject.flow.SMWT.SMWTAAB");
            child.setTableName("SMWTAAB");
            obj.setChild("SMWTAAB", child);

            childc = new DataObjectSet();
            childc.setAssemblyName("WebServerProject");
            childc.setChildClassString("WebServerProject.flow.SMWT.SMWTAAC");
            childc.setTableName("SMWTAAC");
            obj.setChild("SMWTAAC", childc);
        }
        else
        {
            child = obj.getChild("SMWTAAB");
            childc = obj.getChild("SMWTAAC");
        }
        ABTable.HiddenField = new string[] { "SMWTAAB001", "SMWTAAB002" };
        ABTable.dataSource = child;
        ABTable.updateTable();

        ACTable.HiddenField = new string[] { "SMWTAAC001", "SMWTAAC002" };
        ACTable.dataSource = childc;
        ACTable.updateTable();
    }
    protected override void saveData(DataObject objects)
    {
        SMWTAAA obj = (SMWTAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMWTAAA001 = IDProcessor.getID("");
        }

        obj.SMWTAAA002 = SMWTAAA002.ValueText;
        obj.SMWTAAA003 = SMWTAAA003.ValueText;
        if (SMWTAAA0040.Checked)
        {
            obj.SMWTAAA004 = "0";
        }
        else
        {
            obj.SMWTAAA004 = "1";
        }
        if (SMWTAAA0050.Checked)
        {
            obj.SMWTAAA005 = "0";
        }
        else
        {
            obj.SMWTAAA005 = "1";
        }

        DataObjectSet child = ABTable.dataSource;
        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMWTAAB ab = (SMWTAAB)child.getAvailableDataObject(i);
            ab.SMWTAAB002 = obj.SMWTAAA001;
        }
        DataObjectSet childc = ACTable.dataSource;
        for (int i = 0; i < childc.getAvailableDataObjectCount(); i++)
        {
            SMWTAAC ab = (SMWTAAC)childc.getAvailableDataObject(i);
            ab.SMWTAAC002 = obj.SMWTAAA001;
        }
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWTAgent agent = new SMWTAgent();
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
        SMWTAAB ab = (SMWTAAB)objects;

        if (isNew)
        {
            ab.SMWTAAB001 = IDProcessor.getID("");
            ab.SMWTAAB002 = "TEMP";
        }
        ab.SMWTAAB003 = SMWTAAB003.ValueText;
        ab.SMWTAAB004 = SMWTAAB003.ReadOnlyText;
        return true;
    }
    protected void ABTable_ShowRowData(DataObject objects)
    {
        SMWTAAB ab = (SMWTAAB)objects;

        SMWTAAB003.ValueText = ab.SMWTAAB003;
    }
    protected bool ACTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWTAAC ab = (SMWTAAC)objects;

        if (isNew)
        {
            ab.SMWTAAC001 = IDProcessor.getID("");
            ab.SMWTAAC002 = "TEMP";
        }
        ab.SMWTAAC003 = SMWTAAC003.ValueText;
        return true;
    }
    protected void ACTable_ShowRowData(DataObject objects)
    {
        SMWTAAC ab = (SMWTAAC)objects;

        SMWTAAC003.ValueText = ab.SMWTAAC003;
    }
}
