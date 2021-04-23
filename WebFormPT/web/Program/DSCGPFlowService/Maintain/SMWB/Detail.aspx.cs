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
using WebServerProject.flow.SMWB;
using System.Xml;
using MIL.Html;

public partial class Program_DSCGPFlowService_Maintain_SMWB_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWB";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];


        SMWBAAA obj = (SMWBAAA)objects;
        bool isAddNew = (bool)getSession("isNew");
        DataObjectSet abset = null;
        if (isAddNew)
        {
            abset = new DataObjectSet();
            abset.setAssemblyName("WebServerProject");
            abset.setChildClassString("WebServerProject.flow.SMWB.SMWBAAB");
            abset.setTableName("SMWBAAB");

            obj.setChild("SMWBAAB", abset);
        }
        else
        {
            abset = obj.getChild("SMWBAAB");
        }
        SMWBAAA002.ValueText = obj.SMWBAAA002;
        SMWBAAA003.ValueText = obj.SMWBAAA003;
        SMWBAAA004.ValueText = obj.SMWBAAA004;
        if (obj.SMWBAAA005.Equals("Y"))
        {
            SMWBAAA005.Checked = true;
        }
        else
        {
            SMWBAAA005.Checked = false;
        }
        if (obj.SMWBAAA006.Equals("Y"))
        {
            SMWBAAA006.Checked = true;
        }
        else
        {
            SMWBAAA006.Checked = false;
        }
        if (obj.SMWBAAA007.Equals("Y"))
        {
            SMWBAAA007.Checked = true;
        }
        else
        {
            SMWBAAA007.Checked = false;
        }
        ABList.HiddenField = new string[] { "SMWBAAB001", "SMWBAAB002", "SMWBAAB008" };
        ABList.dataSource = abset;
        ABList.updateTable();

        SaveButton.Display = true;
        DeleteButton.Display = false;

    }
    protected override void saveData(DataObject objects)
    {
        SMWBAAA obj = (SMWBAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMWBAAA001 = IDProcessor.getID("");
        }

        obj.SMWBAAA002 = SMWBAAA002.ValueText;
        obj.SMWBAAA003 = SMWBAAA003.ValueText;
        obj.SMWBAAA004 = SMWBAAA004.ValueText;
        if (SMWBAAA005.Checked)
        {
            obj.SMWBAAA005 = "Y";
        }
        else
        {
            obj.SMWBAAA005 = "N";
        }
        if (SMWBAAA006.Checked)
        {
            obj.SMWBAAA006 = "Y";
        }
        else
        {
            obj.SMWBAAA006 = "N";
        }
        if (SMWBAAA007.Checked)
        {
            obj.SMWBAAA007 = "Y";
        }
        else
        {
            obj.SMWBAAA007 = "N";
        }
        for (int i = 0; i < ABList.dataSource.getAvailableDataObjectCount(); i++)
        {
            SMWBAAB ab = (SMWBAAB)ABList.dataSource.getAvailableDataObject(i);
            ab.SMWBAAB002 = obj.SMWBAAA001;
        }


    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWBAgent agent = new SMWBAgent();
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
