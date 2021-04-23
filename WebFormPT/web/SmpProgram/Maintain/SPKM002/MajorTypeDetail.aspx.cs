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

public partial class SmpProgram_Maintain_SPKM002_MajorTypeDetail : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DeptGUID.clientEngineType = (string)Session["engineType"];
        DeptGUID.connectDBString = (string)Session["connectString"];
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
                agent.loadSchema("WebServerProject.maintain.SPKM001.SmpMajorTypesAgent");
                agent.query("GUID = '"+OID+"'");
                DataObjectSet dos = agent.defaultData;
                DataObject obj = dos.getAvailableDataObject(0);
                if (obj == null)
                    Response.Redirect("~/NoAuth.aspx");

                Session["SPKM001_MajorType_objects"] = obj;

                Name.ValueText = obj.getData("Name");
                Desc.ValueText = obj.getData("Description");
                Enable.ValueText = obj.getData("Enable");

                DeptGUID.Display = true;
                DeptGUID.GuidValueText = obj.getData("DeptGUID");
                DeptGUID.doGUIDValidate();

                DataObjectSet detail1 = null;
                DataObjectSet detail2 = null;
                detail1 = obj.getChild("SmpMajorTypeAdm");
                detail2 = obj.getChild("SmpSubType");

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
                string majorGuid = OID;
                string UserID = (string)Session["UserID"];
                string[] usergroup = (string[])Session["usergroup"];
                var findgroup = usergroup.Where(p => p == "SPKMADM").FirstOrDefault();
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
                engine.close();
                string[,] ids = null;
                ids = new string[,] {
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_input_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_input_aspx.language.ini", "message", "ids2", "失效")}
                };
                Enable.setListItem(ids);
                if (string.IsNullOrEmpty(findgroup))
                {
                    Name.ReadOnly = true;
                    Desc.ReadOnly = true;
                    Enable.ReadOnly = true;
                    DeptGUID.ReadOnly = true;
                }
            }
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DataObject objects = (DataObject)Session["SPKM001_MajorType_objects"];
        if (AdmList.dataSource.getAvailableDataObjectCount() == 0)
            throw new Exception("沒有設定管理者資料，請先設定！");
        else
        {
            //string OID = com.dsc.kernal.utility.Utility.filter(Request.QueryString["OID"]);
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            engine = factory.getEngine(engineType, connectString);
            NLAgent agent = new NLAgent();
            agent.engine = engine;
            agent.loadSchema("WebServerProject.maintain.SPKM001.SmpMajorTypesAgent");
            //agent.query("GUID = '" + OID + "'");
            //DataObjectSet dos = agent.defaultData;
            //DataObject objects = dos.getAvailableDataObject(0);
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
    /// 傳入主類別回傳子分類管理者, [0]: id
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="GUID"></param>
    /// <returns>string[]</returns>
    protected string[][] getSubTypeAdmGUID(AbstractEngine engine, string GUID)
    {
        string sql = "	SELECT DISTINCT b.id " +
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
