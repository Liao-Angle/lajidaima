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

public partial class SmpProgram_Maintain_SPKM001_Detail_1_2 : BaseWebUI.DataListSaveForm
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
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_1_2_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_1_2_aspx.language.ini", "message", "ids2", "失效")}
                };
                Enable.setListItem(ids);
                this.MajorName.ValueText = (string)Session["SPKM001_input_MajorName"];
                this.MajorDesc.ValueText = (string)Session["SPKM001_input_MajorDesc"];

                //this.SaveButton_Click += new System.EventHandler(this.MyEvent);
            }
        }
    }

    private void MyEvent(object sender, EventArgs e)
    {
        MessageBox("請至主分類視窗繼續按[儲存]動作！");
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
        DataObjectSet detail3 = null;
        if (isNew)
        {
            detail1 = new DataObjectSet();
            detail1.isNameLess = true;
            detail1.setAssemblyName("WebServerProject");
            detail1.setChildClassString("WebServerProject.maintain.SPKM001.SmpSubTypeAdm");
            detail1.setTableName("SmpSubTypeAdm");
            detail1.loadFileSchema();
            objects.setChild("SmpSubTypeAdm", detail1);
            detail2 = new DataObjectSet();
            detail2.isNameLess = true;
            detail2.setAssemblyName("WebServerProject");
            detail2.setChildClassString("WebServerProject.maintain.SPKM001.SmpSubTypeBelongGroup");
            detail2.setTableName("SmpSubTypeBelongGroup");
            detail2.loadFileSchema();
            objects.setChild("SmpSubTypeBelongGroup", detail2);
            detail3 = new DataObjectSet();
            detail3.isNameLess = true;
            detail3.setAssemblyName("WebServerProject");
            detail3.setChildClassString("WebServerProject.maintain.SPKM001.SmpDocType");
            detail3.setTableName("SmpDocType");
            detail3.loadFileSchema();
            objects.setChild("SmpDocType", detail3);

            Session["SPKM001_SubTypeGUID"] = IDProcessor.getID("");
        }
        else
        {
            detail1 = objects.getChild("SmpSubTypeAdm");
            detail2 = objects.getChild("SmpSubTypeBelongGroup");
            detail3 = objects.getChild("SmpDocType");

            Session["SPKM001_SubTypeGUID"] = objects.getData("GUID"); ;
        }
        //header
        Name.ValueText = objects.getData("Name");
        Desc.ValueText = objects.getData("Description");
        Enable.ValueText = objects.getData("Enable");
        //detail1
        AdmList.dataSource = detail1;
        AdmList.InputForm = "Detail_2_1.aspx";
        AdmList.HiddenField = new string[] { "GUID", "SubTypeAdmUserGUID", "SubTypeGUID", "MajorTypeGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        AdmList.reSortCondition("員工代號", DataObjectConstants.ASC);
        AdmList.updateTable();
        //detail2
        GroupList.dataSource = detail2;
        GroupList.InputForm = "Detail_2_2.aspx";
        GroupList.HiddenField = new string[] { "GUID", "BelongGroupType", "BelongGroupGUID", "SubTypeGUID", "MajorTypeGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        GroupList.reSortCondition("歸屬群組", DataObjectConstants.ASC);
        GroupList.updateTable();
        //detail3
        DocList.dataSource = detail3;
        DocList.InputForm = "Detail_2_3.aspx";
        DocList.HiddenField = new string[] { "GUID", "SubTypeGUID", "MajorTypeGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        DocList.reSortCondition("名稱", DataObjectConstants.ASC);
        DocList.updateTable();
        //Privilege Control
        string majorGuid = Convert.ToString(Session["SPKM001_MajorTypeGUID"]);
        string subGuid = objects.getData("GUID");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string UserID = (string)Session["UserID"];
        string[] usergroup = (string[])Session["usergroup"];
        var findgroup = usergroup.Where(p => p == "SPIT_ALL").FirstOrDefault();
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
            AdmList.NoAdd = true;
            AdmList.NoDelete = true;
            AdmList.NoModify = true;
            GroupList.NoAdd = true;
            GroupList.NoDelete = true;
            GroupList.NoModify = true;
            DocList.NoAdd = true;
            DocList.NoDelete = true;
            DocList.NoModify = true;
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
            objects.setData("GUID", Convert.ToString(Session["SPKM001_SubTypeGUID"]));

            //objects.setData("MajorTypeGUID", "temp");
            objects.setData("MajorTypeGUID", Convert.ToString(Session["SPKM001_MajorTypeGUID"]));

            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        //header
        objects.setData("Name", Name.ValueText);
        objects.setData("Description", Desc.ValueText);
        objects.setData("Enable", Enable.ValueText);
        //detail1
        DataObjectSet detail1 = AdmList.dataSource;
        for (int i = 0; i < detail1.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail1.getAvailableDataObject(i);
            dt.setData("SubTypeGUID", objects.getData("GUID"));
            dt.setData("MajorTypeGUID", objects.getData("MajorTypeGUID"));
        }
        //detail2
        DataObjectSet detail2 = GroupList.dataSource;
        for (int i = 0; i < detail2.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail2.getAvailableDataObject(i);
            dt.setData("SubTypeGUID", objects.getData("GUID"));
            dt.setData("MajorTypeGUID", objects.getData("MajorTypeGUID"));
        }
        //detail3
        DataObjectSet detail3 = DocList.dataSource;
        for (int i = 0; i < detail3.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail3.getAvailableDataObject(i);
            dt.setData("SubTypeGUID", objects.getData("GUID"));
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
        if (DocList.dataSource.getAvailableDataObjectCount() == 0)
            throw new Exception("沒有設定文件類別資料，請先設定！");
        else
            if (GroupList.dataSource.getAvailableDataObjectCount() == 0)
                throw new Exception("沒有設定歸屬群組資料，請先設定！");
            else
            {
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                NLAgent agent = new NLAgent();  //SmpSubTypeAdm
                //agent.loadSchema("WebServerProject.maintain.SPKM001.SmpMajorSubTypesAgent");
                agent.loadSchema("WebServerProject.maintain.SPKM001.SmpSubTypesAgent");
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