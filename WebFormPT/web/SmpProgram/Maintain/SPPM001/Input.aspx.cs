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
using smp.pms.utility;

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
                //string connectString = (string)Session["connectString"];
                //string engineType = (string)Session["engineType"];
                //IOFactory factory = new IOFactory();
                //AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

                string[,] ids = SmpPmMaintainUtil.getYesNoIds(null);
                Active.setListItem(ids);

                //engine.close();
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
        Active.ValueText = objects.getData("Active");

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
        objects.setData("Active", Active.ValueText);

        //檢查欄位資料
        string errMsg = checkFieldData(objects, engine);
        

        

        engine.close();
        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="engine"></param>
    /// <returns></returns>
    public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
        try
        {
            DataObjectSet detail = SmpPmEvaluationDetailList.dataSource;
            DataObject[] datas = detail.getAllDataObjects();
            for (int i = 0; i < datas.Length; i++)
            {
                DataObject data = datas[i];
                if (data.isDelete())
                {
                    string guid = data.getData("GUID");
                    string itemNo = data.getData("ItemNo");
                    string sql = "select count(*) from SmpPmAssessmentScoreDetail where EvaluationDetailGUID='" + guid + "'";
                    DataSet ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                        if (count > 0)
                        {
                            errMsg += "編號:" + itemNo + "不能刪除, 此筆資料已被使用!\n";
                        }
                    }
                }
            }

            if (Active.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_YES))
            {
                double totalMaxFraction = 0;
                for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
                {
                    DataObject dt = detail.getAvailableDataObject(i);
                    dt.setData("EvaluationGUID", objects.getData("GUID")); //將明細的關聯至單頭

                    string maxFraction = dt.getData("MaxFraction");
                    string ItemNum = dt.getData("ItemNum");
                    string ItemWeight = dt.getData("ItemWeight");
                    int num = 0;
                    int Weight = 0;
                    int number = 0;
                    bool isNumeric = int.TryParse(maxFraction, out number);
                    isNumeric = int.TryParse(ItemNum, out num);
                    isNumeric = int.TryParse(ItemWeight, out Weight);

                    totalMaxFraction += (Convert.ToDouble(Weight)/(Convert.ToDouble(number)*Convert.ToDouble(num)))*Convert.ToDouble(number);
                }

                if (totalMaxFraction.ToString() != "100")
                {
                    errMsg += "總分必需為100!";
                }
            }
        }
        catch (Exception e)
        {
            errMsg += e.Message;
        }

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

    protected bool SmpPmEvaluationDetailList_BeforeDeleteData()
    {
        
        return true;
    }
}
