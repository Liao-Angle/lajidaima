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

public partial class Program_System_DSCWebControlSample_SamplePart2 : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string sClass = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "ConnectStringClass");
                com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
                com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
                acs.getConnectionString();
                string connectString = acs.connectString;
                string engineType = acs.engineType;


                com.dsc.kernal.factory.AbstractEngine engine = null;
                try
                {
                    com.dsc.kernal.factory.IOFactory factory = new com.dsc.kernal.factory.IOFactory();
                    engine = factory.getEngine(engineType, connectString);

                    WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
                    string fileAdapter = sp.getParam("FileAdapter");
                    FileUploadSample.FileAdapter = fileAdapter;
                    FileUploadSample.engine = engine;
                    FileUploadSample.tempFolder = Server.MapPath("~/tempFolder/");
                    FileUploadSample.readFile("");
                    FileUploadSample.GridTitle = new string[] { "File Name", "Type", "Description" };
                    FileUploadSample.showSerial = true;
                    FileUploadSample.updateTable();

                    GraphFileUploadSample.FileAdapter = fileAdapter;
                    GraphFileUploadSample.engine = engine;
                    GraphFileUploadSample.tempFolder = Server.MapPath("~/tempFolder/");
                    GraphFileUploadSample.readFile("");
                    //GraphFileUploadSample.GridTitle = new string[] { "File Name", "Type", "Description" };
                    //GraphFileUploadSample.showSerial = true;
                    GraphFileUploadSample.updateTable();


                    string[,] ids = new string[,]{
                        {"Value01", "選項一"},
                        {"Value02", "選項二"},
                        {"Value03", "選項三"},
                        {"Value04", "選項四"}
                    };

                    MDDLSample.setListItem(ids);

                    OpenWinSample.clientEngineType = engineType;
                    OpenWinSample.connectDBString = connectString;

                    WebServerProject.maintain.SMVB.SMVBAgent agent = new WebServerProject.maintain.SMVB.SMVBAgent();
                    agent.engine = engine;
                    bool res = agent.query("");

                    if (!res)
                    {
                        throw new Exception(engine.errorString);
                    }
                    if (!agent.defaultData.errorString.Equals(""))
                    {
                        throw new Exception(agent.defaultData.errorString);
                    }

                    com.dsc.kernal.databean.DataObjectSet dos = agent.defaultData;
                    DataListSample.dataSource = dos;
                    DataListSample.showSettingPages = new bool[] { true, true, true, true, true, true, true, true, true, true };

                    engine.close();
                }
                catch (Exception te)
                {
                    try
                    {
                        engine.close();
                    }
                    catch { };
                    MessageBox("測試頁面載入有誤: "+te.Message);
                    return;
                }
            }
        }
    }

    #region FileUpload
    protected void orderByUploadTimeDescButton_Click(object sender, EventArgs e)
    {
        FileUploadSample.orderByUploadTimeDesc = FileUploadSample.orderByUploadTimeDesc ^ true;
    }
    protected void FUNoAddButton_Click(Object sender, EventArgs e)
    {
        FileUploadSample.NoAdd = FileUploadSample.NoAdd ^ true;
    }
    protected void FNoDeleteButton_Click(Object sender, EventArgs e)
    {
        FileUploadSample.NoDelete = FileUploadSample.NoDelete ^ true;
    }
    protected void FReadOnlyButton_Click(Object sender, EventArgs e)
    {
        FileUploadSample.ReadOnly = FileUploadSample.ReadOnly ^ true;
    }
    #endregion

    #region GraphFileUpload
    protected void GorderByUploadTimeDescButton_Click(object sender, EventArgs e)
    {
        GraphFileUploadSample.orderByUploadTimeDesc = GraphFileUploadSample.orderByUploadTimeDesc ^ true;
    }
    protected void GUNoAddButton_Click(Object sender, EventArgs e)
    {
        GraphFileUploadSample.NoAdd = GraphFileUploadSample.NoAdd ^ true;
    }
    protected void GNoDeleteButton_Click(Object sender, EventArgs e)
    {
        GraphFileUploadSample.NoDelete = GraphFileUploadSample.NoDelete ^ true;
    }
    protected void GReadOnlyButton_Click(Object sender, EventArgs e)
    {
        GraphFileUploadSample.ReadOnly = GraphFileUploadSample.ReadOnly ^ true;
    }
    #endregion

    #region GlassButton
    protected void EnabledButton_Click(object sender, EventArgs e)
    {
        GlassButtonSample.Enabled = GlassButtonSample.Enabled ^ true;
    }
    protected void GlassButtonSample_BeforeClick(object sender, EventArgs e)
    {
        MessageBox("Server Before Click");
    }
    protected void GlassButtonSample_Click(object sender, EventArgs e)
    {
        //MessageBox("Server Click");
        GlassButtonSample.Text = "server After Click!";
    }
    protected void GlassButtonTextButton_Click(object sender, EventArgs e)
    {
        if (GlassButtonSample.Text == "GlassButton")
        {
            GlassButtonSample.Text = "中文";
        }
        else
        {
            GlassButtonSample.Text = "GlassButton";
        }
    }
    protected void GlassButtonReadOnlyButton_Click(object sender, EventArgs e)
    {
        GlassButtonSample.ReadOnly = GlassButtonSample.ReadOnly ^ true;
    }
    #endregion

    #region IPField
    protected void IPFieldSample_TextChange(object sender, EventArgs e)
    {
        MessageBox("IPField TextChanged Event fired");
    }
    protected void IPFValueTextButton_Click(object sender, EventArgs e)
    {
        IPFieldResult.ValueText = IPFieldSample.ValueText;
    }
    protected void IPFCustomCSSButton_Click(object sender, EventArgs e)
    {
        if (IPFieldSample.CustomCSS == "")
        {
            IPFieldSample.CustomCSS = "FormHeadHead";
        }
        else
        {
            IPFieldSample.CustomCSS = "";
        }
    }
    protected void IPFReadOnlyButton_Click(object sender, EventArgs e)
    {
        IPFieldSample.ReadOnly = IPFieldSample.ReadOnly ^ true;
    }
    protected void IPFgetIPPartButton_Click(object sender, EventArgs e)
    {
        IPFieldResult.ValueText = "第一部份為：" + IPFieldSample.getIPPart(0) + "\n";
        IPFieldResult.ValueText += "第二部份為：" + IPFieldSample.getIPPart(1) + "\n";
        IPFieldResult.ValueText += "第三部份為：" + IPFieldSample.getIPPart(2) + "\n";
        IPFieldResult.ValueText += "第四部份為：" + IPFieldSample.getIPPart(3) ;
    }
    #endregion

    #region MultiDropDownList
    protected void MDDLValueTextButton_Click(object sender, EventArgs e)
    {
        string[] v = MDDLSample.ValueText;
        string str = "";
        for (int i = 0; i < v.Length; i++)
        {
            str += v[i] + "\n";
        }
        MDDLResult.ValueText = str;
    }
    protected void MDDLReadOnlyTextButton_Click(object sender, EventArgs e)
    {
        string[] v = MDDLSample.ReadOnlyText;
        string str = "";
        for (int i = 0; i < v.Length; i++)
        {
            str += v[i] + "\n";
        }
        MDDLResult.ValueText = str;
    }
    protected void MDDLReadOnlyButton_Click(object sender, EventArgs e)
    {
        MDDLSample.ReadOnly = MDDLSample.ReadOnly ^ true;
    }
    protected void MDDLSampleChange(string[] value)
    {
        string str = "";
        for (int i = 0; i < value.Length; i++)
        {
            str += value[i] + "\n";
        }
        MDDLResult.ValueText = str;
    }

    #endregion

    #region OpenWin
    protected void DoOpenWinClick(object sender, EventArgs e)
    {
        OpenWinSample.paramString = "id";
        OpenWinSample.whereClause = "";
        OpenWinSample.openWin("Users", "001", false, "0001");
    }
    protected void DoMultiWinClick(object sender, EventArgs e)
    {
        OpenWinSample.paramString = "id";
        OpenWinSample.whereClause = "";
        OpenWinSample.openWin("Users", "001", true, "0001");
    }
    protected void OpenWinSampleClick(string identityID, string[,] values)
    {
        string str = "";
        for (int i = 0; i < values.GetLength(0); i++)
        {
            for (int j = 0; j < values.GetLength(1); j++)
            {
                str += values[i, j] + "\t";
            }
            str += "\n";
        }
        OPResult.ValueText = str;
    }
    #endregion

    #region DataList/OutDataList
    protected void DataListHiddenFieldButton_Click(object sender, EventArgs e)
    {
        if (DataListSample.HiddenField.Length == 0)
        {
            DataListSample.HiddenField = new string[] { "SMVAAAB001" };
        }
        else
        {
            DataListSample.HiddenField = new string[0];
        }
        DataListSample.updateTable();
    }
    protected void ShowSerialButton_Click(object sender, EventArgs e)
    {
        DataListSample.showSerial = DataListSample.showSerial ^ true;
        DataListSample.updateTable();
    }
    protected void IsShowCheckBoxButton_Click(object sender, EventArgs e)
    {
        DataListSample.IsShowCheckBox = DataListSample.IsShowCheckBox ^ true;
        DataListSample.updateTable();
    }
    protected void showTotalRowCountButton_Click(object sender, EventArgs e)
    {
        DataListSample.showTotalRowCount = DataListSample.showTotalRowCount ^ true;
        DataListSample.updateTable();
    }
    protected void showExcelButton_Click(object sender, EventArgs e)
    {
        DataListSample.showExcel = DataListSample.showExcel ^ true;
    }
    protected void showPrintButton_Click(object sender, EventArgs e)
    {
        DataListSample.showPrint = DataListSample.showPrint ^ true;
    }
    protected void showSetupButton_Click(object sender, EventArgs e)
    {
        DataListSample.showSetup = DataListSample.showSetup ^ true;
    }
    protected void MultiLineButton_Click(object sender, EventArgs e)
    {
        if (DataListSample.multiLine)
        {
            DataListSample.multiLine = false;
        }
        else
        {
            DataListSample.multiLine = true;
            DataListSample.multiLineRowCount = 2;
        }
        DataListSample.updateTable();
    }
    protected void IsHideSelectAllButton_Click(object sender, EventArgs e)
    {
        DataListSample.IsHideSelectAllButton = DataListSample.IsHideSelectAllButton ^ true;
        DataListSample.updateTable();
    }
    protected void showAdminColumnButton_Click(object sender, EventArgs e)
    {
        if (DataListSample.showAdminColumn == "N,N")
        {
            DataListSample.showAdminColumn = "Y,Y";
        }
        else
        {
            DataListSample.showAdminColumn = "N,N";
        }
        DataListSample.updateTable();
    }
    protected void CheckDataButton_Click(object sender, EventArgs e)
    {
        DataListSample.CheckData(2, 1);
    }
    protected void UnCheckDataButton_Click(object sender, EventArgs e)
    {
        DataListSample.UnCheckData(2, 1);
    }
    protected void UnCheckAllDataButton_Click(object sender, EventArgs e)
    {
        DataListSample.UnCheckAllData();
    }
    protected void getSelectedItemButton_Click(object sender, EventArgs e)
    {
        string outstr = "";
        com.dsc.kernal.databean.DataObject[] ddo = DataListSample.getSelectedItem();
        for (int i = 0; i < ddo.Length; i++)
        {
            outstr += ddo[i].getData("SMVAAAB002") + ";";
        }
        DataListOutput.ValueText = outstr;
    }
    protected void reOrderFieldButton_Click(object sender, EventArgs e)
    {
        com.dsc.kernal.databean.DataObject ddo = DataListSample.dataSource.getAvailableDataObject(0);
        string[] curs = new string[ddo.dataField.Length];
        for (int i = 0; i < curs.Length; i++)
        {
            if (i == 1)
            {
                curs[i] = ddo.dataField[2];
            }
            else if (i == 2)
            {
                curs[i] = ddo.dataField[1];
            }
            else
            {
                curs[i] = ddo.dataField[i];
            }
        }
        DataListSample.reOrderField(curs);
    }
    #endregion

}
