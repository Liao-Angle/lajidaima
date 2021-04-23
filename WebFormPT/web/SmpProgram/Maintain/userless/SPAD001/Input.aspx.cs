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

public partial class SmpProgram_Input : BaseWebUI.DataListSaveForm
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
                UserGUID.clientEngineType = (string)Session["engineType"];
                UserGUID.connectDBString = (string)Session["connectString"];

                string[,] ids = null;

                ids = new string[,] {
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad001m_input_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad001m_input_aspx.language.ini", "message", "ids2", "失效")}
                };
                Active.setListItem(ids);

                FlowGUID.clientEngineType = (string)Session["engineType"];
                FlowGUID.connectDBString = (string)Session["connectString"];
                if (Convert.ToString(FlowGUID.ValueText).Equals(""))
                {
                    FlowGUID.ValueText = "PKG13600429351041";
                    FlowGUID.doValidate();
                }
                FlowGUID.ReadOnly = true;
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

        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPAD001.SmpUserFlowDetail");
            detail.setTableName("SmpUserFlowDetail");
            detail.loadFileSchema();
            objects.setChild("SmpUserFlowDetail", detail);
        }
        else
        {
            detail = objects.getChild("SmpUserFlowDetail");
        }
        //head
        string flowGUID = Convert.ToString(objects.getData("FlowGUID"));
        if(!flowGUID.Equals("")) 
        {
            FlowGUID.GuidValueText = flowGUID;
            FlowGUID.doGUIDValidate();
        }
        UserGUID.GuidValueText = objects.getData("UserGUID");
        UserGUID.doGUIDValidate();
        Active.ValueText = objects.getData("Active");

        //detail
        UserFlowDetailList.dataSource = detail;
        UserFlowDetailList.InputForm = "Detail.aspx";
        UserFlowDetailList.HiddenField = new string[] { "GUID", "UserFlowGUID", "StateValueGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        UserFlowDetailList.reSortCondition("關號", DataObjectConstants.ASC);
        UserFlowDetailList.updateTable();
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
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("FlowGUID", FlowGUID.GuidValueText);
        objects.setData("UserGUID", UserGUID.GuidValueText);
        objects.setData("Active", Active.ValueText);
        objects.setData("id", UserGUID.ValueText);
        objects.setData("userName", UserGUID.ReadOnlyValueText);
        objects.setData("SMWBAAA004", FlowGUID.ReadOnlyValueText);

        DataObjectSet detail = UserFlowDetailList.dataSource;
        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail.getAvailableDataObject(i);
            dt.setData("UserFlowGUID", objects.getData("GUID"));
        }
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
        agent.loadSchema("WebServerProject.maintain.SPAD001.SmpUserFlowAgent");
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
