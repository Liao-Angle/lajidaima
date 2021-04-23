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

public partial class SmpProgram_Maintain_SPAD003_Detail : BaseWebUI.DataListSaveForm  //BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {

        bool isNew = (bool)getSession("isNew");
        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPAD003.TestShips");
            detail.setTableName("TestShips");
            detail.loadFileSchema();
            objects.setChild("TestShips", detail);
        }
        else
        {
            detail = objects.getChild("TestShips");
        }
        //line
        Seq.ValueText = objects.getData("Seq");
        ItemName.ValueText = objects.getData("ItemName");
        Qty.ValueText = objects.getData("Qty");

        //ship
        ShipsList.dataSource = detail;
        ShipsList.InputForm = "Ship.aspx";
        ShipsList.HiddenField = new string[] { "GUID", "LineGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        ShipsList.reSortCondition("序", DataObjectConstants.ASC);
        ShipsList.updateTable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("HeaderGUID", "temp");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("Seq", Seq.ValueText);
        objects.setData("ItemName", ItemName.ValueText);
        objects.setData("Qty", Qty.ValueText);

        // tony
        DataObjectSet ship = ShipsList.dataSource;
        for (int i = 0; i < ship.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = ship.getAvailableDataObject(i);
            dt.setData("LineGUID", objects.getData("GUID"));
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    //protected override void saveDB(DataObject objects)
    //{
    //    string connectString = (string)Session["connectString"];
    //    string engineType = (string)Session["engineType"];

    //    IOFactory factory = new IOFactory();
    //    AbstractEngine engine = factory.getEngine(engineType, connectString);

    //    NLAgent agent = new NLAgent();
    //    agent.loadSchema("WebServerProject.maintain.SPAD003.TestHeadersAgent");
    //    agent.engine = engine;
    //    agent.query("1=2");

    //    bool result = agent.defaultData.add(objects);
    //    if (!result)
    //    {
    //        engine.close();
    //        throw new Exception(agent.defaultData.errorString);
    //    }
    //    else
    //    {
    //        result = agent.update();
    //        engine.close();
    //        if (!result)
    //        {
    //            throw new Exception(engine.errorString);
    //        }
    //    }
    //}


}