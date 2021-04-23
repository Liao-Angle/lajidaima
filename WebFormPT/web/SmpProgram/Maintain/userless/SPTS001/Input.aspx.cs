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
                string[,] ids = null;

                ids = new string[,] {
                    {"",""},
                    {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts001m_input_aspx.language.ini", "message", "SMP", "新普科技")},
                    {"TP",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts001m_input_aspx.language.ini", "message", "TP", "中普科技")}
                };
                CompanyCode.setListItem(ids);

                //對象
                ids = new string[,]{
                    {"",""},
                    {"1","人員"},
                    {"2","群組"}              
                };
                AdmType.setListItem(ids);
                AdmType.ValueText = "2";

                AdmTypeGUID.clientEngineType = (string)Session["engineType"];
                AdmTypeGUID.connectDBString = (string)Session["connectString"];
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
        //head
        CompanyCode.ValueText = objects.getData("CompanyCode");             

        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPTS001.SmpTSAdmDetail");
            detail.setTableName("SmpTSAdmDetail");
            detail.loadFileSchema();
            objects.setChild("SmpTSAdmDetail", detail);
        }
        else
        {
            detail = objects.getChild("SmpTSAdmDetail");
            CompanyCode.ReadOnly = true;
        }

       

        //detail
        AdmDetailList.dataSource = detail;
        AdmDetailList.HiddenField = new string[] { "GUID", "AdmFormGUID", "AdmTypeGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS","KindName" };
        AdmDetailList.reSortCondition("對象", DataObjectConstants.ASC);
        AdmDetailList.updateTable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");        
        string companyCode = CompanyCode.ValueText;
        string companyName = "";

        if (companyCode.Equals("SMP"))
        {
            companyName = "新普科技";
        }
        else if (companyCode.Equals("TP"))
        {
            companyName = "中普科技";
        }
        objects.setData("CompanyCode", companyCode);
        objects.setData("CompanyName", companyName);

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }              

        DataObjectSet detail = AdmDetailList.dataSource;
        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail.getAvailableDataObject(i);
            dt.setData("AdmFormGUID", objects.getData("GUID"));
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
        agent.loadSchema("WebServerProject.maintain.SPTS001.SmpTSAdmAgent");
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

    /// <summary>
    ///對象改變 
    /// </summary>
    /// <param name="value"></param>
    protected void AdmType_SelectChanged(string value)
    {
        if (AdmType.ValueText.Equals("1"))
        {
            AdmTypeGUID.tableName = "Users";
            AdmTypeGUID.serialNum = "003";
            AdmTypeGUID.idIndex = 2;
            AdmTypeGUID.valueIndex = 3;
        }
        else
        {
            AdmTypeGUID.tableName = "SPKM001";
            AdmTypeGUID.serialNum = "001";
            AdmTypeGUID.idIndex = 1;
            AdmTypeGUID.valueIndex = 2;
        }

        AdmTypeGUID.ValueText = "";
        AdmTypeGUID.GuidValueText = "";
        AdmTypeGUID.ReadOnlyValueText = "";
    }


    /// <summary>
    ///管理者_SaveRow
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    protected bool AdmDetailList_SaveRowData(DataObject objects, bool isNew)
    {
        string strErrMsg = "";
        if (CompanyCode.ValueText.Equals(""))
        {
            strErrMsg += "請先選擇公司別!\n";
        }

        if (AdmType.ValueText.Equals(""))
        {
            strErrMsg += "請先選擇對象!\n";
        }

        if (AdmTypeGUID.ValueText.Equals(""))
        {
            strErrMsg += "請先選擇管理者名稱!\n";
        }

        if (!strErrMsg.Equals(""))
        {
            MessageBox(strErrMsg);
            return false;
        }

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("AdmFormGUID", "TEMP");          
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");            
        }
        objects.setData("AdmType", AdmType.ValueText);
        objects.setData("AdmTypeGUID", AdmTypeGUID.GuidValueText);
        objects.setData("id", AdmTypeGUID.ValueText);
        objects.setData("Name", AdmTypeGUID.ReadOnlyValueText);       

        if (AdmType.ValueText.Equals("1"))
        {
            objects.setData("KindName", "人員");   
            AdmTypeGUID.valueIndex = 3;
            AdmTypeGUID.GuidValueText = objects.getData("AdmTypeGUID");
            AdmTypeGUID.doGUIDValidate();
        }
        else
        {
            AdmTypeGUID.valueIndex = 3;
            AdmTypeGUID.doGUIDValidate();
            objects.setData("KindName", AdmTypeGUID.ReadOnlyValueText);

            AdmTypeGUID.valueIndex = 4;
            AdmTypeGUID.doGUIDValidate();
            objects.setData("AdmType", AdmTypeGUID.ReadOnlyValueText);

            AdmTypeGUID.valueIndex = 2;
            AdmTypeGUID.GuidValueText = objects.getData("AdmTypeGUID");
            AdmTypeGUID.doGUIDValidate();
        }

        /*
        string[] keys = objects.keyField;
        objects.keyField = new string[] { "AdmType", "AdmTypeGUID" , "CompanyCode" };
        DataObjectSet detailListSet = AdmDetailList.dataSource;
        if (!detailListSet.checkData(objects))
        {
            MessageBox("管理者代號重覆!!");
            objects.keyField = keys;
            return false;
        } */

        return true;
    }

    /// <summary>
    ///管理者_ShowRow
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    protected void AdmDetailList_ShowRowData(DataObject objects)
    {
        if (objects.getData("AdmType").Equals("1"))
        {
            AdmType.ValueText = "1";
            AdmTypeGUID.tableName = "Users";
            AdmTypeGUID.serialNum = "003";
            AdmTypeGUID.idIndex = 2;
            AdmTypeGUID.valueIndex = 3;
        }
        else
        {
            AdmType.ValueText = "2";
            AdmTypeGUID.tableName = "SPKM001";
            AdmTypeGUID.serialNum = "001";
            AdmTypeGUID.idIndex = 1;
            AdmTypeGUID.valueIndex = 2;
        }
        AdmTypeGUID.GuidValueText = objects.getData("AdmTypeGUID");
        AdmTypeGUID.doGUIDValidate();
    }
}
