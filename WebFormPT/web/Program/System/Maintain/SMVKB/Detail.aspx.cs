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
using WebServerProject.maintain.SMVKB;

public partial class Program_System_Maintain_SMVKB_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVKB";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        SMVKBAA obj = (SMVKBAA)objects;
        SMVKBAA002.ValueText = obj.SMVKBAA002;
        SMVKBAA003.ValueText = obj.SMVKBAA003;

        DataObjectSet abset = null;

        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            abset = new DataObjectSet();
            abset.setAssemblyName("WebServerProject");
            abset.setChildClassString("WebServerProject.maintain.SMVKB.SMVKBAB");
            abset.setTableName("SMVKBAB");
            obj.setChild("SMVKBAB", abset);
        }
        else
        {
            abset = obj.getChild("SMVKBAB");
        }

        ABList.connectDBString = connectString;
        ABList.clientEngineType = engineType;
        ABList.NoAdd = true;
        ABList.NoModify = true;
        ABList.HiddenField = new string[] { "SMVKBAB001", "SMVKBAB002", "SMVKBAB003" };
        ABList.dataSource = abset;
        ABList.updateTable();
    }

    protected override void saveData(DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");
        SMVKBAA obj = (SMVKBAA)objects;
        if (isAddNew)
        {
            obj.SMVKBAA001 = IDProcessor.getID("");
        }
        obj.SMVKBAA002 = SMVKBAA002.ValueText;
        obj.SMVKBAA003 = SMVKBAA003.ValueText;

        DataObjectSet abset = obj.getChild("SMVKBAB");
        for (int i = 0; i < abset.getDataObjectCount(); i++)
        {
            abset.getDataObject(i).setData("SMVKBAB002", obj.SMVKBAA001);
        }
    }

    protected override void saveDB(DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVKBAgent agent = new SMVKBAgent();
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

    protected void SelectButton_Click(object sender, EventArgs e)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        openWin1.PageUniqueID = this.PageUniqueID;
        openWin1.clientEngineType = engineType;
        openWin1.connectDBString = connectString;
        openWin1.identityID = "0001";
        openWin1.paramString = "id";
        openWin1.whereClause = "";
        openWin1.openWin("Users", "001", true, "0001");
        //openWin1.openWinS("Users", "001", true, "0001", "OpenWin", "openWin1");
    }

    protected void openWin1_OpenWindowButtonClick(string identityID, string[,] values)
    {
        DataObjectSet abset = ABList.dataSource;

        for (int i = 0; i < values.GetLength(0); i++)
        {
            SMVKBAB ab = (SMVKBAB)abset.create();
            ab.SMVKBAB001 = IDProcessor.getID("");
            ab.SMVKBAB002 = "TEMP";
            ab.SMVKBAB003 = values[i, 0];
            ab.userId = values[i, 1];
            ab.userName = values[i, 2];

            abset.add(ab);
        }
        ABList.dataSource = abset;
        ABList.updateTable();
    }
}
