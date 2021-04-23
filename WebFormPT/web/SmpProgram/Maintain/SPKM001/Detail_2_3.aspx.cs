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

public partial class SmpProgram_Maintain_SPKM001_Detail_2_3 : BaseWebUI.DataListSaveForm
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
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "ids2", "失效")}
                };
                Enable.setListItem(ids);

                string[,] eds = null;
                eds = new string[,] {
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "eds2", "否")},
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "eds1", "是")}
                };
                External.setListItem(eds);
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
        DataObjectSet detail1 = null;
        if (isNew)
        {
            detail1 = new DataObjectSet();
            detail1.isNameLess = true;
            detail1.setAssemblyName("WebServerProject");
            detail1.setChildClassString("WebServerProject.maintain.SPKM001.SmpDocTypeReader");
            detail1.setTableName("SmpDocTypeReader");
            detail1.loadFileSchema();
            objects.setChild("SmpDocTypeReader", detail1);
            Session["SPKM001_DocTypeGUID"] = IDProcessor.getID("");
        }
        else
        {
            detail1 = objects.getChild("SmpDocTypeReader");
            Session["SPKM001_DocTypeGUID"] = objects.getData("GUID"); ;
        }
        //header
        Name.ValueText = objects.getData("Name");
        Desc.ValueText = objects.getData("Description");
        Enable.ValueText = objects.getData("Enable");
        External.ValueText = objects.getData("External");
        //detail1
        ReadList.dataSource = detail1;
        ReadList.InputForm = "Detail_2_3_1.aspx";
        ReadList.HiddenField = new string[] { "GUID", "BelongGroupType", "BelongGroupGUID", "SubTypeGUID", "DocTypeGUID", "MajorTypeGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        ReadList.reSortCondition("名稱", DataObjectConstants.ASC);
        ReadList.updateTable();

        //Privilege Control
        string majorGuid = Convert.ToString(Session["SPKM001_MajorTypeGUID"]);
        string subGuid = Convert.ToString(Session["SPKM001_SubTypeGUID"]);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string UserID = (string)Session["UserID"];
        string[] usergroup = (string[])Session["usergroup"];
        var findgroup = usergroup.Where(p => p == "SPKMADM").FirstOrDefault();
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string[][] subadm = getSubTypeAdmGUID(engine, subGuid);
        string[][] majadm = getMajorTypeAdmGUID(engine, majorGuid);
        var fm = majadm.Where(x => x.Contains(UserID));
        var fs = subadm.Where(x => x.Contains(UserID));
        if ((fs.Count() == 0) && (fm.Count() == 0) && (string.IsNullOrEmpty(findgroup)))
        {
            this.Name.ReadOnly = true;
            this.Desc.ReadOnly = true;
            this.Enable.ReadOnly = true;
            this.External.ReadOnly = true;
            ReadList.NoAdd = true;
            ReadList.NoDelete = true;
            ReadList.NoModify = true;
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
            objects.setData("GUID", IDProcessor.getID(""));
            //objects.setData("GUID", Convert.ToString(Session["SPKM001_DocTypeGUID"]));

            objects.setData("SubTypeGUID", "temp");
            objects.setData("SubTypeGUID", Convert.ToString(Session["SPKM001_SubTypeGUID"]));

            objects.setData("MajorTypeGUID", "temp");
            objects.setData("MajorTypeGUID", Convert.ToString(Session["SPKM001_MajorTypeGUID"]));

            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        //header
        objects.setData("Name", Name.ValueText);
        objects.setData("Description", Desc.ValueText);
        objects.setData("Enable", Enable.ValueText);
        objects.setData("External", External.ValueText);
        //detail1
        DataObjectSet detail1 = ReadList.dataSource;
        for (int i = 0; i < detail1.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail1.getAvailableDataObject(i);
            dt.setData("DocTypeGUID", objects.getData("GUID"));
            dt.setData("SubTypeGUID", objects.getData("SubTypeGUID"));
            dt.setData("MajorTypeGUID", objects.getData("MajorTypeGUID"));
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
        //agent.loadSchema("WebServerProject.maintain.SPKM001.SmpMajorSubTypesAgent");
        agent.loadSchema("WebServerProject.maintain.SPKM001.SmpDocTypesAgent");
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
    /// 傳入子類別回傳子分類管理者, [0]: id
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="GUID"></param>
    /// <returns>string[]</returns>
    protected string[][] getSubTypeAdmGUID(AbstractEngine engine, string GUID)
    {
        string sql = "	SELECT DISTINCT b.id " +
                     "    FROM dbo.SmpSubTypeAdm a " +
                     "    LEFT JOIN dbo.Users b on b.OID = a.SubTypeAdmUserGUID " +
                     "   WHERE a.SubTypeGUID = '" + Utility.filter(GUID) + "'";
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