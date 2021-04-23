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
using System.Linq;

public partial class SmpProgram_Maintain_SPKM001_Input : BaseWebUI.DataListSaveForm
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
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_input_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_input_aspx.language.ini", "message", "ids2", "失效")}
                };
                Enable.setListItem(ids);
                string[] usergroup = (string[])Session["usergroup"];
                var findgroup = usergroup.Where(p => p == "SPIT_ALL").FirstOrDefault();
                if (string.IsNullOrEmpty(findgroup))
                {
                    Name.ReadOnly = true;
                    Desc.ReadOnly = true;
                    Enable.ReadOnly = true;
                    DeptGUID.ReadOnly = true;
                }
            }
        }
        DeptGUID.clientEngineType = (string)Session["engineType"];
        DeptGUID.connectDBString = (string)Session["connectString"];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        DataObjectSet detail1 = null;
        DataObjectSet detail2 = null;
        if (isNew)
        {
            detail1 = new DataObjectSet();
            detail1.isNameLess = true;
            detail1.setAssemblyName("WebServerProject");
            detail1.setChildClassString("WebServerProject.maintain.SPKM001.SmpMajorTypeAdm");
            detail1.setTableName("SmpMajorTypeAdm");
            detail1.loadFileSchema();
            objects.setChild("SmpMajorTypeAdm", detail1);
            detail2 = new DataObjectSet();
            detail2.isNameLess = true;
            detail2.setAssemblyName("WebServerProject");
            detail2.setChildClassString("WebServerProject.maintain.SPKM001.SmpSubType");
            detail2.setTableName("SmpSubType");
            detail2.loadFileSchema();
            objects.setChild("SmpSubType", detail2);

            Session["SPKM001_MajorTypeGUID"] = IDProcessor.getID("");

        }
        else
        {
            detail1 = objects.getChild("SmpMajorTypeAdm");
            detail2 = objects.getChild("SmpSubType");

            Session["SPKM001_MajorTypeGUID"] = objects.getData("GUID"); ;

        }
        //header
        Name.ValueText = objects.getData("Name");
        Desc.ValueText = objects.getData("Description");
        Enable.ValueText = objects.getData("Enable");
        DeptGUID.Display = true;
        DeptGUID.GuidValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
        //detail1
        AdmList.dataSource = detail1;
        AdmList.InputForm = "Detail_1_1.aspx";
        AdmList.HiddenField = new string[] { "GUID", "MajorTypeAdmUserGUID", "MajorTypeGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        AdmList.reSortCondition("員工代號", DataObjectConstants.ASC);
        AdmList.updateTable();
        //detail2
        SubList.dataSource = detail2;
        SubList.DialogHeight = 540;
        SubList.DialogWidth = 640;
        SubList.InputForm = "Detail_1_2.aspx";
        SubList.HiddenField = new string[] { "GUID", "MajorTypeGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        SubList.reSortCondition("名稱", DataObjectConstants.ASC);
        SubList.updateTable();
        //Privilege Control
        string majorGuid = Convert.ToString(Session["SPKM001_MajorTypeGUID"]);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string UserID = (string)Session["UserID"];
        string[] usergroup = (string[])Session["usergroup"];
        var findgroup = usergroup.Where(p => p == "SPIT_ALL").FirstOrDefault();
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string[][] subadm = getSubTypeAdmGUID(engine, majorGuid);
        string[][] majadm = getMajorTypeAdmGUID(engine, majorGuid);
        var fm = majadm.Where(x => x.Contains(UserID));
        var fs = subadm.Where(x => x.Contains(UserID));
        if ((fs.Count() > 0) && (fm.Count() == 0) && (string.IsNullOrEmpty(findgroup)))
        {
            AdmList.NoAdd = true;
            AdmList.NoDelete = true;
            AdmList.NoModify = true;
            SubList.NoAdd = true;
            SubList.NoDelete = true;
        }
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
            //objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("GUID", Convert.ToString(Session["SPKM001_MajorTypeGUID"]));

            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        //header
        objects.setData("DeptGUID", DeptGUID.GuidValueText);
        objects.setData("id", DeptGUID.ValueText);
        objects.setData("organizationUnitName", DeptGUID.ReadOnlyValueText);


        objects.setData("Name", Name.ValueText);
        objects.setData("Description", Desc.ValueText);
        objects.setData("Enable", Enable.ValueText);
        //detail1
        DataObjectSet detail1 = AdmList.dataSource;
        for (int i = 0; i < detail1.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail1.getAvailableDataObject(i);
            dt.setData("MajorTypeGUID", objects.getData("GUID"));
        }
        //detail2
        DataObjectSet detail2 = SubList.dataSource;
        for (int i = 0; i < detail2.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail2.getAvailableDataObject(i);
            dt.setData("MajorTypeGUID", objects.getData("GUID"));
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
        if (AdmList.dataSource.getAvailableDataObjectCount() == 0)
            throw new Exception("沒有設定管理者資料，請先設定！");
        else
        {
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPKM001.SmpMajorSubTypesAgent");
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


    protected bool SubList_BeforeOpenWindow(DataObject objects, bool isAddNew)
    {
        Session["SPKM001_input_MajorName"] = this.Name.ValueText;
        Session["SPKM001_input_MajorDesc"] = this.Desc.ValueText;
        return true;
    }


    /// <summary>
    /// 傳入主類別回傳子分類管理者, [0]: id
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="GUID"></param>
    /// <returns>string[]</returns>
    protected string[][] getSubTypeAdmGUID(AbstractEngine engine, string GUID)
    {
        string sql = "	SELECT DISTINCT b.id "+
                     "    FROM dbo.SmpSubTypeAdm a " +
                     "    LEFT JOIN dbo.Users b on b.OID = a.SubTypeAdmUserGUID " +
	                 "   WHERE a.MajorTypeGUID = '" + Utility.filter(GUID) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[1];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();
        }
        return result;
    }

    /// <summary>
    /// 傳入主類別回傳主分類管理者, [0]: id
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="GUID"></param>
    /// <returns>string[]</returns>
    protected string[][] getMajorTypeAdmGUID(AbstractEngine engine, string GUID)
    {
        string sql = "	SELECT DISTINCT b.id " +
                     "    FROM dbo.SmpMajorTypeAdm a " +
                     "    LEFT JOIN dbo.Users b on b.OID = a.MajorTypeAdmUserGUID " +
                     "   WHERE a.MajorTypeGUID = '" + Utility.filter(GUID) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[1];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();
        }
        return result;
    }
}