using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using WebServerProject.auth;
using WebServerProject;

public partial class SmpProgram_Maintain_SPPM010_Maintain : BaseWebUI.GeneralWebPage
{
    List<string> encodeFields = new List<string>("SelfScore,FirstScore,SecondScore,FinalScore".Split(','));

    protected override void OnPreRender(EventArgs e)
    {
        ConfirmButton.ConfirmText = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_changesqlfield_maintain_aspx.language.ini", "message", "QueryError1", "你確定要修改嗎?");
        base.OnPreRender(e);
    }

    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPPM010M";
        ApplicationID = "SYSTEM";
        ModuleID = "SPPM";

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                initPage();
            }
        }
    }

    protected void initPage()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        AUTHAgent authagent = new AUTHAgent();
        authagent.engine = engine;

        int auth = authagent.getAuth(maintainIdentity, (string)Session["UserID"], (string[])Session["usergroup"]);

        if (auth == 0)
        {
            engine.close();
            Response.Redirect("~/NoAuth.aspx");
        }

        if (!authagent.parse(auth, AUTHAgent.MODIFY))
        {
            engine.close();
            Response.Redirect("~/NoAuth.aspx");
        }

        //初始化畫面
        string[,] tables = getTables(engine, "");
        SelectTableName.setListItem(tables);
        SelectTableName_SelectChanged(SelectTableName.ValueText);

        engine.close();
    }

    public string[,] getTables(AbstractEngine engine, string ownerName)
    {
        string[,] TableNameString;

        string sql = "select name from sysobjects where xtype = 'U' and name like 'SmpPm%' order by name";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        TableNameString = new string[ds.Tables[0].Rows.Count, 2];

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TableNameString[i, 0] = ds.Tables[0].Rows[i]["name"].ToString();
            TableNameString[i, 1] = ds.Tables[0].Rows[i]["name"].ToString();
        }

        return TableNameString;
    }

    protected void SelectTableName_SelectChanged(string value)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //設定DataGrid
        setGrid(engine);

        engine.close();
    }

    protected void setGrid(AbstractEngine engine)
    {
        string sql = "select * from " + SelectTableName.ValueText;
        DataSet ds = engine.getDataSet(sql, "TEMP");

        string[,] ids = new string[ds.Tables[0].Columns.Count, 2];

        DataObjectFactory dof = new DataObjectFactory();
        dof.assemblyName = "ChangeSQLField";
        dof.childClassString = "ChangeSQLField.SQL";
        dof.tableName = "SQL";

        bool hasGUID = false;
        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            dof.addFieldDefinition(ds.Tables[0].Columns[i].Caption, DataObjectConstants.STRING, "4096000", "", ds.Tables[0].Columns[i].Caption, "");
            string column = ds.Tables[0].Columns[i].Caption;
            ids[i, 0] = column;
            ids[i, 1] = ids[i, 0];

            if (ds.Tables[0].Columns[i].Caption.Equals("GUID"))
            {
                hasGUID = true;
            }
        }
        dof.allowAllFieldEmpty();

        if (!hasGUID)
        {
            clearUpdateData();
            return;
        }

        dof.addIdentityField("GUID");
        dof.addKeyField("GUID");
        
        DataObjectSet dos = new DataObjectSet();
        dos.setAssemblyName("ChangeSQLField");
        dos.setChildClassString("ChangeSQLField.SQL");
        dos.setTableName("SQL");
        dos.dataObjectSchema = dof.generalXML();
        dos.isNameLess = true;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataObject ddo = dof.generateObject();
            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
            {
                ddo.setData(ds.Tables[0].Columns[j].Caption, ds.Tables[0].Rows[i][j].ToString());
            }
            dos.addDraft(ddo);
        }

        DataGrid.ReadOnly = false;
        DataGrid.dataSource = dos;
        DataGrid.updateTable();

        SelectFieldName.setListItem(ids);

        EncodeField.ReadOnly = false;
        ConfirmButton.ReadOnly = true;
    }

    protected void clearUpdateData()
    {
        DataGrid.ReadOnly = true;
        string[,] ids = new string[,]{
            {"",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_changesqlfield_maintain_aspx.language.ini", "message", "QueryError2", "無")}
        };
        SelectFieldName.setListItem(ids);

        EncodeField.ReadOnly = true;
        ConfirmButton.ReadOnly = true;

        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_changesqlfield_maintain_aspx.language.ini", "message", "QueryError3", "此欄位為系統欄位, 不可修改"));
    }

    protected void EncodeField_Click(object sender, EventArgs e)
    {
        SysParam sp = new SysParam(null);
        DataObject[] ddo = DataGrid.getSelectedItem();
        if (ddo.Length > 0)
        {
            string fieldName = SelectFieldName.ValueText;
            for (int i = 0; i < ddo.Length; i++)
            {
                string fieldValue = ddo[i].getData(fieldName);
                int number = 0;
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    bool isNumeric = int.TryParse(fieldValue, out number);
                    if (isNumeric)
                    {
                        string encodeValue = sp.encode(fieldValue);
                        ddo[i].setData(fieldName, encodeValue);
                    }
                }
                DataGrid.updateTable();
                ConfirmButton.ReadOnly = false;
            }
        }
        else
        {
            MessageBox("請選擇至少1筆資料!");
        }
    }

    protected void DecodeField_Click(object sender, EventArgs e)
    {
        SysParam sp = new SysParam(null);
        DataObject[] ddo = DataGrid.getSelectedItem();
        if (ddo.Length > 0)
        {
            string fieldName = SelectFieldName.ValueText;
            for (int i = 0; i < ddo.Length; i++)
            {
                string fieldValue = ddo[i].getData(fieldName);
                int number = 0;
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    bool isNumeric = int.TryParse(fieldValue, out number);
                    if (!isNumeric)
                    {
                        string decodeValue = sp.decode(fieldValue);
                        ddo[i].setData(fieldName, decodeValue);
                    }
                }
                DataGrid.updateTable();
                ConfirmButton.ReadOnly = false;
            }
        }
        else
        {
            MessageBox("請選擇至少1筆資料!");
        }
    }

    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            engine.startTransaction(IsolationLevel.ReadCommitted);

            //執行更新
            DataObject[] ddo = DataGrid.getSelectedItem();
            if (ddo.Length > 0)
            {
                string fieldName = SelectFieldName.ValueText;
                for (int i = 0; i < ddo.Length; i++)
                {
                    string fieldValue = ddo[i].getData(fieldName);
                    if (!string.IsNullOrEmpty(fieldValue))
                    {
                        string guid = ddo[i].getData("GUID");
                        string sql = "select * from " + SelectTableName.ValueText + " where GUID='" + guid + "'";
                        DataSet ds = engine.getDataSet(sql, "TEMP");
                        string value = ds.Tables[0].Rows[0][fieldName].ToString();
                        writeLog(new Exception(guid + "^" + value + "^" + fieldValue));
                        ds.Tables[0].Rows[0][fieldName] = fieldValue;
                        
                        if (!engine.updateDataSet(ds))
                        {
                            try
                            {
                                engine.rollback();
                            }
                            catch { };
                            try
                            {
                                engine.close();
                            }
                            catch { };
                            MessageBox(engine.errorString);
                        }
                    }
                }
            }

            setGrid(engine);
            engine.commit();
            engine.close();
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_changesqlfield_maintain_aspx.language.ini", "message", "QueryError5", "資料欄位已更新"));
        }
        catch (Exception ue)
        {
            try
            {
                engine.rollback();
            }
            catch { };
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(ue.Message);
        }
    }
}
