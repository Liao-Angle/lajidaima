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

public partial class SmpProgram_Maintain_SPKM002_DocTypeDetail : BaseWebUI.GeneralWebPage
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
                string[,] ids = null;
                ids = new string[,] {
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "ids2", "失效")}
                };
                Enable.setListItem(ids);
                string[,] eds = null;
                eds = new string[,] {
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "eds1", "是")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "eds2", "否")}
                };
                External.setListItem(eds);

                string OID = com.dsc.kernal.utility.Utility.filter(Request.QueryString["OID"]);
                engine = factory.getEngine(engineType, connectString);
                NLAgent agent = new NLAgent();
                agent.engine = engine;
                agent.loadSchema("WebServerProject.maintain.SPKM001.SmpDocTypesAgent");
                agent.query("GUID = '"+OID+"'");
                DataObjectSet dos = agent.defaultData;
                DataObject obj = dos.getAvailableDataObject(0);
                Name.ValueText = obj.getData("Name");
                Desc.ValueText = obj.getData("Description");
                Enable.ValueText = obj.getData("Enable");
                External.ValueText = obj.getData("External");

                //Privilege Control
                string majorGuid = obj.getData("MajorTypeGUID");
                string subGuid = obj.getData("SubTypeGUID");
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
                    this.External.ReadOnly = true;
                }
                engine.close();
            }
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string OID = com.dsc.kernal.utility.Utility.filter(Request.QueryString["OID"]);
        engine = factory.getEngine(engineType, connectString);
        NLAgent agent = new NLAgent();
        agent.engine = engine;
        agent.loadSchema("WebServerProject.maintain.SPKM001.SmpDocTypesAgent");
        agent.query("GUID = '" + OID + "'");
        DataObjectSet dos = agent.defaultData;
        DataObject objects = dos.getAvailableDataObject(0);
        objects.setData("Name", Name.ValueText);
        objects.setData("Description", Desc.ValueText);
        objects.setData("Enable", Enable.ValueText);
        objects.setData("External", External.ValueText);
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
