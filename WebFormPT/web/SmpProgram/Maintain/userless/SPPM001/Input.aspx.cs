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
using com.dsc.kernal.agent;

public partial class SmpProgram_Maintain_SPPM001_Input : BaseWebUI.DataListSaveForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        Name.ValueText = objects.getData("Name");
        Description.ValueText = objects.getData("Description");

        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPPM001.SmpPmEvaluationDetail");
            detail.setTableName("SmpPmEvaluationDetail");
            detail.loadFileSchema();
            objects.setChild("SmpPmEvaluationDetail", detail);
        }
        else
        {
            detail = objects.getChild("SmpPmEvaluationDetail");

            //int row = detail.getAvailableDataObjectCount();
            //for (int i = 0; i < row; i++)
            //{
            //    DataObject data = detail.getAvailableDataObject(i);
            //    string content = data.getData("Content");
            //    string styleContent = "{[div style=\"width:400px\"]}" + content + "{[/div]}";
            //    data.setData("content", styleContent);
                
            //}
        }

        //detail
        SmpPmEvaluationDetailList.setColumnStyle("Content", 360, DSCWebControl.GridColumnStyle.LEFT);
        SmpPmEvaluationDetailList.dataSource = detail;
        SmpPmEvaluationDetailList.InputForm = "Detail.aspx";
        SmpPmEvaluationDetailList.DialogHeight = 600;
        SmpPmEvaluationDetailList.DialogWidth = 840;
        SmpPmEvaluationDetailList.HiddenField = new string[] { "GUID", "EvaluationGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        SmpPmEvaluationDetailList.reSortCondition("編號", DataObjectConstants.ASC);
        SmpPmEvaluationDetailList.updateTable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("Name", Name.ValueText);
        objects.setData("Description", Description.ValueText);

        //檢查欄位資料
        string errMsg = checkFieldData(objects, engine);
        

        DataObjectSet detail = SmpPmEvaluationDetailList.dataSource;
        int totalMaxFraction = 0;
        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail.getAvailableDataObject(i);
            dt.setData("EvaluationGUID", objects.getData("GUID")); //將明細的關聯至單頭

            string maxFraction = dt.getData("MaxFraction");
            int number = 0;
            bool isNumeric = int.TryParse(maxFraction, out number);
            totalMaxFraction += number;
        }
        if (totalMaxFraction != 100)
        {
            errMsg += "總分必需為100!";
        }

        engine.close();
        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
    }

    public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
        
        return errMsg;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPPM001.SmpPmEvaluationAgent");
        agent.engine = engine;
        agent.query("1=2");

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
