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
using System.Xml;
using WebServerProject.auth;

public partial class SmpProgram_Maintain_SPKM002_SubTypeDetail : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                //權限判斷
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                AUTHAgent authagent = new AUTHAgent();
                authagent.engine = engine;
                int auth = authagent.getAuth("SPKM002M", (string)Session["UserID"], (string[])Session["usergroup"]);
                engine.close();
                string mstr = "";
                if (auth == 0)
                {
                    Response.Redirect("~/NoAuth.aspx");
                }
                string OID = com.dsc.kernal.utility.Utility.filter(Request.QueryString["OID"]);
                engine = factory.getEngine(engineType, connectString);
                NLAgent agent = new NLAgent();
                agent.engine = engine;
                agent.loadSchema("WebServerProject.maintain.SPKM001.SmpSubTypesAgent");
                agent.query("GUID = '"+OID+"'");
                DataObjectSet dos = agent.defaultData;
                DataObject obj = dos.getAvailableDataObject(0);
                Session["SPKM001_SubType_objects"] = obj;
                //header
                Name.ValueText = obj.getData("Name");
                Desc.ValueText = obj.getData("Description");
                Enable.ValueText = obj.getData("Enable");

                DataObjectSet detail1 = null;
                DataObjectSet detail2 = null;
                DataObjectSet detail3 = null;

                detail1 = obj.getChild("SmpSubTypeAdm");
                detail2 = obj.getChild("SmpSubTypeBelongGroup");
                detail3 = obj.getChild("SmpDocType");

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
                string majorGuid = obj.getData("MajorTypeGUID");
                string subGuid = OID;
                string UserID = (string)Session["UserID"];
                string[] usergroup = (string[])Session["usergroup"];
                var findgroup = usergroup.Where(p => p == "SPKMADM").FirstOrDefault();
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
                engine.close();
                string[,] ids = null;
                ids = new string[,] {
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_1_2_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_1_2_aspx.language.ini", "message", "ids2", "失效")}
                };
                Enable.setListItem(ids);
            }
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DataObject objects = (DataObject)Session["SPKM001_SubType_objects"];
        if (DocList.dataSource.getAvailableDataObjectCount() == 0)
            throw new Exception("沒有設定文件類別資料，請先設定！");
        else
            if (GroupList.dataSource.getAvailableDataObjectCount() == 0)
                throw new Exception("沒有設定歸屬群組資料，請先設定！");
            else
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                //string OID = com.dsc.kernal.utility.Utility.filter(Request.QueryString["OID"]);
                engine = factory.getEngine(engineType, connectString);
                NLAgent agent = new NLAgent();
                agent.engine = engine;
                agent.loadSchema("WebServerProject.maintain.SPKM001.SmpSubTypesAgent");
                //agent.query("GUID = '" + OID + "'");
                //DataObjectSet dos = agent.defaultData;
                //DataObject objects = dos.getAvailableDataObject(0);
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
                //saveDB
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
                    MessageBox("儲存完成！");
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
