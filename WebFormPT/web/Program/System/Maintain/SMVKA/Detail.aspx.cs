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
using WebServerProject.maintain.SMVKA;

public partial class Program_System_Maintain_SMVKA_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVKA";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                connectString = (string)Session["connectString"];
                engineType = (string)Session["engineType"];

                SUBS.clientEngineType = engineType;
                SUBS.connectDBString = connectString;
                SUBS.whereClause = "OID in (select distinct SMVKBAB003 from SMVKBAB where SMVKBAB003<>'" + (string)Session["UserGUID"] + "' and SMVKBAB002 in (select SMVKBAB002 from SMVKBAB where SMVKBAB003='" + (string)Session["UserGUID"] + "'))";
                FlowID.clientEngineType = engineType;
                FlowID.connectDBString = connectString;
                SMVKAAC004.clientEngineType = engineType;
                SMVKAAC004.connectDBString = connectString;
                SMVKAAC004.whereClause = "OID in (select distinct SMVKBAB003 from SMVKBAB where SMVKBAB003<>'" + (string)Session["UserGUID"] + "' and SMVKBAB002 in (select SMVKBAB002 from SMVKBAB where SMVKBAB003='" + (string)Session["UserGUID"] + "'))";
                SMVKAAC005.clientEngineType = engineType;
                SMVKAAC005.connectDBString = connectString;
            }
        }
    }
    protected override void showData(DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        SMVKAAA obj = (SMVKAAA)objects;
        StartTime.ValueText = obj.SMVKAAA003;
        EndTime.ValueText = obj.SMVKAAA004;
        
        bool isAddNew = (bool)getSession("isNew");

        DataObjectSet abset = null;
        if (isAddNew)
        {
            abset = new DataObjectSet();
            abset.setAssemblyName("WebServerProject");
            abset.setChildClassString("WebServerProject.maintain.SMVKA.SMVKAAB");
            abset.setTableName("SMVKAAB");
            obj.setChild("SMVKAAB", abset);
        }
        else
        {
            abset = obj.getChild("SMVKAAB");
        }
        SubsList.HiddenField = new string[] { "SMVKAAB001", "SMVKAAB002", "SMVKAAB003", "SMVKAAB004", "SMVKAAB005", "SMVKAAB006" };
        string[,] orderby = new string[,]{
             {"SMVKAAB004",DataObjectConstants.ASC}
        };
        abset.sort(orderby);
        SubsList.dataSource = abset;
        SubsList.updateTable();

        DataObjectSet acset = null;
        if (isAddNew)
        {
            acset = new DataObjectSet();
            acset.setAssemblyName("WebServerProject");
            acset.setChildClassString("WebServerProject.maintain.SMVKA.SMVKAAC");
            acset.setTableName("SMVKAAC");
            obj.setChild("SMVKAAC", acset);
        }
        else
        {
            acset = obj.getChild("SMVKAAC");
        }
        ProcessSubList.HiddenField = new string[] { "SMVKAAC001", "SMVKAAC002", "SMVKAAC003", "SMVKAAC004", "SMVKAAC005", "SMVKAAC008", "SMVKAAC009" };
        ProcessSubList.dataSource = acset;
        ProcessSubList.updateTable();
    }

    protected override void saveData(DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");
        SMVKAAA obj = (SMVKAAA)objects;
        
        if (!checkDate())
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvka_detail_aspx.language.ini", "message", "ids0", "代理期間：起日不可大於等於迄日."));
        }

        //檢核代理期間不可重疊
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "select * from SMVKAAA where SMVKAAA001<>'" + obj.SMVKAAA001 + "' and SMVKAAA002='" + (string)Session["UserGUID"] + "' and ('" + StartTime.ValueText + "' < SMVKAAA004 and '" + EndTime.ValueText + "' > SMVKAAA003)";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        engine.close();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvka_detail_aspx.language.ini", "message", "ids1","代理期間日期不可重疊已設定代理期間."));
        }

        //通用代理人至少要有一筆資料
        //20110802 Eric Hsu : 非IE瀏覽器obj.getChild("SMVKAAB")取回的DataObjectSet與SubsList.dataSource的數量不一樣 ; 很玄
        //DataObjectSet abset = obj.getChild("SMVKAAB");
        DataObjectSet abset = SubsList.dataSource;
        if (abset.getAvailableDataObjectCount().Equals(0))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvka_detail_aspx.language.ini", "message", "ids2","通用代理人至少要有一筆資料."));
        }

        //saveData
        if (isAddNew)
        {
            obj.SMVKAAA001 = IDProcessor.getID("");
            obj.SMVKAAA002 = (string)Session["UserGUID"];
            obj.SMVKAAA005 = "0";
        }
        obj.SMVKAAA003 = StartTime.ValueText;
        obj.SMVKAAA004 = EndTime.ValueText;
        obj.SMVKAAA006 = "N";

        //DataObjectSet abset = obj.getChild("SMVKAAB");
        for (int i = 0; i < abset.getAvailableDataObjectCount(); i++)
        {
            SMVKAAB ab = (SMVKAAB)abset.getAvailableDataObject(i);
            ab.SMVKAAB002 = obj.SMVKAAA001;
            ab.SMVKAAB004 = i.ToString();
        }

        DataObjectSet acset = obj.getChild("SMVKAAC");
        for (int i = 0; i < acset.getAvailableDataObjectCount(); i++)
        {
            SMVKAAC ac = (SMVKAAC)acset.getAvailableDataObject(i);
            ac.SMVKAAC002 = obj.SMVKAAA001;
        }
    }

    protected override void saveDB(DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVKAAgent agent = new SMVKAAgent();
        agent.engine = engine;
        agent.query("1=2");

        //取得資料
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

    protected void ClearButton_Click(object sender, EventArgs e)
    {
        StartTime.ValueText = "";
        EndTime.ValueText = "";
    }
    protected void SubsList_ShowRowData(DataObject objects)
    {
        SMVKAAB obj = (SMVKAAB)objects;
        SUBS.GuidValueText = obj.SMVKAAB005;
        SUBS.doGUIDValidate();
    }
    protected bool SubsList_SaveRowData(DataObject objects, bool isNew)
    {
        SMVKAAB obj = (SMVKAAB)objects;
        if (isNew)
        {
            obj.SMVKAAB001 = get32OID();
            obj.SMVKAAB002 = "TEMP";
            obj.SMVKAAB003 = (string)Session["UserGUID"];
            obj.SMVKAAB006 = "0";
        }
        obj.SMVKAAB005 = SUBS.GuidValueText;
        obj.userId = SUBS.ValueText;
        obj.userName = SUBS.ReadOnlyValueText;
        return true;
    }
    protected void ProcessSubList_ShowRowData(DataObject objects)
    {
        SMVKAAC obj = (SMVKAAC)objects;
        SMVKAAC004.GuidValueText = obj.SMVKAAC004;
        SMVKAAC004.doGUIDValidate();
        SMVKAAC005.GuidValueText = obj.SMVKAAC005;
        SMVKAAC005.doGUIDValidate();
        FlowID.ValueText = obj.SMVKAAC006;
        FlowID.doValidate();
        //if (obj.SMVKAAC008.Equals("1"))
        //{
        //    SMVKAAC008.Checked = true;
        //}
        //else
        //{
        //    SMVKAAC008.Checked = false;
        //}
    }
    protected bool ProcessSubList_SaveRowData(DataObject objects, bool isNew)
    {
        SMVKAAC obj = (SMVKAAC)objects;
        if (isNew)
        {
            obj.SMVKAAC001 = get32OID();
            obj.SMVKAAC002 = "TEMP";
            obj.SMVKAAC003 = (string)Session["UserGUID"];
            obj.SMVKAAC009 = "1";
        }
        obj.SMVKAAC004 = SMVKAAC004.GuidValueText;
        obj.userId = SMVKAAC004.ValueText;
        obj.userName = SMVKAAC004.ReadOnlyValueText;
        obj.SMVKAAC005 = SMVKAAC005.GuidValueText;
        obj.organizationUnitId = SMVKAAC005.ValueText;
        obj.organizationUnitName = SMVKAAC005.ReadOnlyValueText;
        obj.SMVKAAC006 = FlowID.ValueText;
        obj.SMVKAAC007 = FlowID.ReadOnlyValueText;
        string strChecked = "0";
        //if (SMVKAAC008.Checked)
        //{
        //    strChecked = "1";
        //}
        obj.SMVKAAC008 = strChecked;
        return true;
    }
    private string get32OID()
    {
        string tag = IDProcessor.getID("").Replace("-", "");
        return tag;
    }
    private bool checkDate()
    {
        if (!StartTime.ValueText.Equals("") && !EndTime.ValueText.Equals(""))
        {
            if (DateTime.Parse(StartTime.ValueText) >= DateTime.Parse(EndTime.ValueText))
            {
                return false;
            }
        }
        return true;
    }
}
