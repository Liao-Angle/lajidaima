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

public partial class SmpProgram_Maintain_SPSMWK_Input : BaseWebUI.DataListSaveForm
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
                    {"Flow",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spsmwkm_input_aspx.language.ini", "message", "ids3", "流程")},
                    {"Maintain",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spsmwkm_input_aspx.language.ini", "message", "ids4", "作業畫面")}
                };
                PrivilegeType.setListItem(ids);

                ids = new string[,] {
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spsmwkm_input_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spsmwkm_input_aspx.language.ini", "message", "ids2", "失效")}
                };
                Active.setListItem(ids);

                FlowGUID.clientEngineType = (string)Session["engineType"];
                FlowGUID.connectDBString = (string)Session["connectString"];
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

        if (isNew)
        {

        }
        else
        {
            
        }

        PrivilegeType.ValueText = objects.getData("PrivilegeType");
        setFlowGUID();

        UserGUID.GuidValueText = objects.getData("UserGUID");
        FlowGUID.GuidValueText = objects.getData("FlowGUID");

        UserGUID.GuidValueText = objects.getData("UserGUID");
        UserGUID.doGUIDValidate();

        if (!UserGUID.GuidValueText.Equals(""))
        {
            UserGUID.doGUIDValidate();
        }

        if (!FlowGUID.GuidValueText.Equals("")) 
        {
            FlowGUID.doGUIDValidate();
        }
        
        Active.ValueText = objects.getData("Active");
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
        objects.setData("UserGUID", UserGUID.GuidValueText);
        objects.setData("PrivilegeType", PrivilegeType.ValueText);
        objects.setData("FlowGUID", FlowGUID.GuidValueText);
        
        objects.setData("Active", Active.ValueText);
        objects.setData("id", UserGUID.ValueText);
        objects.setData("userName", UserGUID.ReadOnlyValueText);
        objects.setData("SMWBAAA004", FlowGUID.ReadOnlyValueText);
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

    protected void PrivilegeType_SelectChanged(string value)
    {
        if (!PrivilegeType.ValueText.Equals(""))
        {
            PrivilegeType.ReadOnly = false;
        }
        else
        {
            PrivilegeType.ReadOnly = true;
        }
        FlowGUID.GuidValueText = "";
        FlowGUID.ValueText = "";
        FlowGUID.ReadOnlyValueText = "";
        setFlowGUID();
    }

    protected void setFlowGUID()
    {
        if (PrivilegeType.ValueText.Equals("Flow"))
        {
            FlowGUID.guidField = "SMWBAAA001";
            FlowGUID.keyField = "SMWBAAA003";
            FlowGUID.serialNum = "001";
            FlowGUID.tableName = "SMWBAAA";
            FlowGUID.idIndex = 1;
            FlowGUID.valueIndex = 2;
        }
        else if (PrivilegeType.ValueText.Equals("Maintain"))
        {
            FlowGUID.guidField = "SMWAAAA001";
            FlowGUID.keyField = "SMWAAAA002";
            FlowGUID.serialNum = "001";
            FlowGUID.tableName = "SMWAAAA";
            FlowGUID.idIndex = 1;
            FlowGUID.valueIndex = 2;
        } 
    }
}
