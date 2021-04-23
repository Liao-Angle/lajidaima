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
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.flow.data;
using com.dsc.flow.server;
using com.dsc.kernal.agent;
using WebServerProject.flow.SMWO;
using WebServerProject.flow.SMWS;
using WebServerProject;
using DSCWebControl;

public partial class Program_DSCGPFlowService_Maintain_SMWO_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        maintainIdentity = "SMWO";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                if (Request.QueryString["BoxID"] == null)
                {
                    setSession("BoxID", "");
                }
                else
                {
                    setSession("BoxID", Request.QueryString["BoxID"]);

                    SMWSAgent agent = new SMWSAgent();
                    agent.engine = engine;
                    agent.query("SMWSAAA002='" + com.dsc.kernal.utility.Utility.filter(Request.QueryString["BoxID"]) + "'");
                    if (agent.defaultData.getAvailableDataObjectCount() == 0)
                    {
                        engine.close();
                        Response.Redirect("NoSetting.aspx");
                    }
                    setSession("SMWSAAA", agent.defaultData.getAvailableDataObject(0));
                }
                //是否開啟批次取回所有代理清單
                string sql = "select SMVPAAA037 from SMVPAAA";
                string enableRetrieveAll = (string)engine.executeScalar(sql);
                engine.close();
                if (enableRetrieveAll.Equals("Y"))
                    RetrieveAllButton.Visible = true;
                else
                    RetrieveAllButton.Visible = false;

                ListTable.CurPanelID = CurPanelID;
                ListTable.NoDelete = true;
                ListTable.NoAdd = true;

                string[,] ids = new string[,]{
                    {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwo_maintain_aspx.language.ini", "message", "ids0", "全部")},
                    {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwo_maintain_aspx.language.ini", "message", "ids1", "已結案")},
                    {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwo_maintain_aspx.language.ini", "message", "ids2", "未結案")}
                };
                instanceCompleteType.setListItem(ids);

                queryData();
            }
        }
    }

    private void queryData()
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string sqlx = "select SMVPAAA026 from SMVPAAA";
            string isDB = (string)engine.executeScalar(sqlx);
            
            //SysParam sp = new SysParam(engine);
            //string flowType = sp.getParam("FlowAdapter");
            //string con1 = sp.getParam("NaNaWebService");
            //string con2 = sp.getParam("DotJWebService");
            //string account = sp.getParam("FlowAccount");
            //string password = sp.getParam("FlowPassword");
            string flowType = (String)Session["FlowAdapter"];
            string con1 = (String)Session["NaNaWebService"];
            string con2 = (String)Session["DotJWebService"];
            string account = (String)Session["FlowAccount"];
            string password = (String)Session["FlowPassword"];

            FlowFactory ff = new FlowFactory();
            AbstractFlowAdapter adp = ff.getAdapter(flowType);
            adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
            adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
            fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

            adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

            ReAssignActivity[] wi = null;

            if (isDB.Equals("N"))
            {
                wi = adp.fetchReassignedWorkItems((string)Session["UserID"], ListTable.getPageSize().ToString(), ListTable.getCurrentPage().ToString(), "", Subject.ValueText, "", StartTime.ValueText, "", instanceCompleteType.ValueText);
            }
            else
            {
                wi = adp.fetchReassignedWorkItems((string)Session["UserID"], ListTable.getPageSize().ToString(), ListTable.getCurrentPage().ToString(), "", Subject.ValueText, "", StartTime.ValueText, "", instanceCompleteType.ValueText, engine);
            }

            adp.logout();

            string BoxID = (string)getSession("BoxID");
            SMWSAAA aa = null;
            if (!BoxID.Equals(""))
            {
                aa = (SMWSAAA)getSession("SMWSAAA");
            }

            //DataObjectSet dos = new DataObjectSet();
            //dos.setAssemblyName("WebServerProject");
            //dos.setChildClassString("WebServerProject.flow.SMWO.SMWOAAA");
            //dos.setTableName("SMWOAAA");

            #region 重新宣告SMWO XML結構
            string qstr = "select '' as SMWOAAA101 ,'' as SMWOAAA102 ," +
                                " SMWOAAA.SMWOAAA001,SMWOAAA.SMWOAAA002,SMWOAAA.SMWOAAA003,SMWOAAA.SMWOAAA004,SMWOAAA.SMWOAAA005," +
                                " SMWOAAA.SMWOAAA006,SMWOAAA.SMWOAAA007,SMWOAAA.SMWOAAA008,SMWOAAA.SMWOAAA009,SMWOAAA010,SMWOAAA011," +
                                " SMWOAAA.D_INSERTUSER,SMWOAAA.D_INSERTTIME,SMWOAAA.D_MODIFYUSER,SMWOAAA.D_MODIFYTIME " +
                                " from SMWOAAA ";

            DataObjectSet dos = new DataObjectSet();
            string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">";
            schema += "<queryStr>" + qstr + "</queryStr>";
            schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
            schema += "  <fieldDefinition>";

            schema += "    <field dataField=\"SMWOAAA101\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"表單代號\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA102\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"表單單號\" showName=\"\"/>";            
            schema += "    <field dataField=\"SMWOAAA001\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"唯一識別碼\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA002\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA003\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"流程代號\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA004\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"流程名稱\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA005\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"流程實例序號\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA006\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA007\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"處理者\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA008\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"轉派方式\" showName=\"0:工作受託者直接指派;1:工作受託者不在;2:工作受託者逾時未處理;3:系統管理員直接指派;4:流程負責人直接指派;5:原工作受託者取回\"/>";
            schema += "    <field dataField=\"SMWOAAA009\" typeField=\"STRING\" lengthField=\"20\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA010\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"WorkAssignmentOID\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWOAAA011\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"角色名稱\" showName=\"\"/>";
            schema += "    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>";
            schema += "    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
            schema += "    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>";
            schema += "    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>";

            schema += "  </fieldDefinition>";
            schema += "  <identityField>";
            schema += "    <field dataField=\"SMWOAAA001\"/>";
            schema += "  </identityField>";
            schema += "  <keyField>";
            schema += "    <field dataField=\"SMWOAAA001\"/>";
            schema += "  </keyField>";
            schema += "  <allowEmptyField>";

            schema += "    <field dataField=\"SMWOAAA002\"/>";
            schema += "    <field dataField=\"SMWOAAA101\"/>";
            schema += "    <field dataField=\"SMWOAAA102\"/>";
            schema += "    <field dataField=\"D_INSERTUSER\"/>";
            schema += "    <field dataField=\"D_INSERTTIME\"/>";
            schema += "    <field dataField=\"D_MODIFYUSER\"/>";
            schema += "    <field dataField=\"D_MODIFYTIME\"/>";

            schema += "  </allowEmptyField>";
            schema += "  <nonUpdateField>";
            schema += "  </nonUpdateField>";
            schema += "</DataObject>";
            dos.dataObjectSchema = schema;

            dos.isNameLess = true;
            #endregion

            #region 先把WorkItem裡面的GP OID記錄在DataTable裡面，然後再去原稿撈表單單號、表單代號
            DataTable WorkItemDt = new DataTable();
            #region 宣告此DataTable結構
            DataColumn WorkItemDc = new DataColumn();
            //OID
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "OID";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //workItemOID
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "workItemOID";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //workItemName
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "workItemName";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //processInstanceOID
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "processInstanceOID";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //processName
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "processName";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //processSerialNumber
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "processSerialNumber";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //subject
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "subject";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //currentPerformerName
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "currentPerformerName";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //reassignmentType
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "reassignmentType";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //workItemCreatedTime
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "workItemCreatedTime";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            #endregion
            #endregion

            for (int i = 0; i < wi.Length; i++)
            {
                if (!BoxID.Equals(""))
                {
                    bool hasFound = false;
                    for (int z = 0; z < aa.getChild("SMWSAAB").getAvailableDataObjectCount(); z++)
                    {
                        if (wi[i].processName.Equals(aa.getChild("SMWSAAB").getAvailableDataObject(z).getData("SMWSAAB004")))
                        {
                            hasFound = true;
                            break;
                        }
                    }
                    if (aa.SMWSAAA004.Equals("0") && hasFound)
                    {
                        continue;
                    }
                    if (aa.SMWSAAA004.Equals("1") && (!hasFound))
                    {
                        continue;
                    }

                    hasFound = false;
                    for (int z = 0; z < aa.getChild("SMWSAAC").getAvailableDataObjectCount(); z++)
                    {
                        if (wi[i].workItemName.Equals(aa.getChild("SMWSAAC").getAvailableDataObject(z).getData("SMWSAAC003")))
                        {
                            hasFound = true;
                            break;
                        }
                    }
                    if (aa.SMWSAAA005.Equals("0") && hasFound)
                    {
                        continue;
                    }
                    if (aa.SMWSAAA005.Equals("1") && (!hasFound))
                    {
                        continue;
                    }
                }

                DataRow WorkItemDr = WorkItemDt.NewRow();
                WorkItemDr["OID"] = wi[i].OID;
                WorkItemDr["workItemOID"] = wi[i].workItemOID;
                WorkItemDr["workItemName"] = wi[i].workItemName;
                WorkItemDr["processInstanceOID"] = wi[i].processInstanceOID;
                WorkItemDr["processName"] = wi[i].processName;
                WorkItemDr["processSerialNumber"] = wi[i].processSerialNumber;
                WorkItemDr["subject"] = wi[i].subject;
                WorkItemDr["currentPerformerName"] = wi[i].currentPerformerName;
                ReAssignHistory his = (ReAssignHistory)wi[i].history[0];
                WorkItemDr["reassignmentType"] = his.reassignmentType;
                WorkItemDr["workItemCreatedTime"] = wi[i].workItemCreatedTime;
                WorkItemDt.Rows.Add(WorkItemDr);

            }

            ArrayList arys = new ArrayList();

            DataSet SMWYDs = engine.getDataSet("Select * from SMWYAAA", "TEMP");
            DataTable SMWYDt = SMWYDs.Tables[0];
            if (SMWYDt.Rows.Count > 0)
            {
                DataTable FiliterDt = DataTableJoin(SMWYDt, WorkItemDt, "SMWYAAA005", "processSerialNumber");
                for (int i = 0; i < FiliterDt.Rows.Count; i++)
                {
                    arys.Add(FiliterDt.Rows[i]);
                }
            }

            for (int i = 0; i < arys.Count; i++)
            {
                for (int j = i + 1; j < arys.Count; j++)
                {
                    DataRow ob1 = (DataRow)arys[i];
                    DataRow ob2 = (DataRow)arys[j];
                    string ct1 = "";
                    string ct2 = "";
                    if (ob1.Table.Columns[0].ColumnName.Equals("resda001"))
                    {
                        ct1 = ob1["resdd009"].ToString();
                    }
                    else
                    {
                        ct1 = ob1["workItemCreatedTime"].ToString();
                    }
                    if (ob2.Table.Columns[0].ColumnName.Equals("resda001"))
                    {
                        ct2 = ob2["resdd009"].ToString();
                    }
                    else
                    {
                        ct2 = ob2["workItemCreatedTime"].ToString();
                    }
                    if (ct1.CompareTo(ct2) < 0)
                    {
                        object ptr = arys[i];
                        arys[i] = arys[j];
                        arys[j] = ptr;

                    }
                }
            }
            setSession("init_DATA", arys);
            TotalRows.Text = "共" + arys.Count.ToString() + "筆資料";


            ListTable.IsGeneralUse = false;
            ListTable.InputForm = "Detail.aspx";
            ListTable.HiddenField = new string[] { "SMWOAAA001", "SMWOAAA002", "SMWOAAA003", "SMWOAAA005", "SMWOAAA010", "SMWOAAA011" };
            ListTable.dataSource = dos;
            ListTable.updateTable();

            setSession("CURRENTPAGE", 1);
            ListTable_ShowPagingClick();

            engine.close();

        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(te.Message);
            writeLog(te);
        }
        //setSession("CURLIST", dos);

        //MessageBox("已取得清單");

    }
    protected void FilterButton_Click(object sender, EventArgs e)
    {

        queryData();
    }
    protected DataObject ListTable_CustomDisplayTitle(DataObject objects)
    {
        return objects;
    }
    protected DSCWebControl.FlowStatusData ListTable_GetFlowStatusString(DataObject objects, bool isAddNew)
    {
        //ReAssignActivity wi = (ReAssignActivity)objects.Tag;
        DataObject wi = objects;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "";
        DataSet ds = null;

        //建立FlowStatusData物件
        FlowStatusData fd = new FlowStatusData();
        
        //取得資料物件代碼, 由原稿取得
        sql = "select SMWYAAA019, SMWYAAA003, SMWYAAA004, SMWYAAA002 from SMWYAAA where SMWYAAA005='" + Utility.filter(wi.getData("SMWOAAA005")) + "'";
        ds = engine.getDataSet(sql, "TEMP");
        string objectGUID = ds.Tables[0].Rows[0][0].ToString();

        fd.ACTID = "";
        fd.ACTName = Server.UrlEncode(wi.getData("SMWOAAA011"));
        fd.FlowGUID = wi.getData("SMWOAAA005");
        fd.HistoryGUID = "";
        fd.ObjectGUID = objectGUID;
        fd.PDID = ds.Tables[0].Rows[0][1].ToString();
        fd.PDVer = "";
        //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
        //目前都給他為ProcessModify
        fd.UIStatus = FlowStatusData.FromRedirect;
        fd.WorkItemOID = wi.getData("SMWOAAA010");
        fd.TargetWorkItemOID = wi.getData("SMWOAAA010");

        //這裡要根據SMWDAAA判定是否有ProcessNew
        ListTable.FormTitle = ds.Tables[0].Rows[0]["SMWYAAA004"].ToString() + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwo_maintain_aspx.language.ini", "message", "List", "(單號:") + ds.Tables[0].Rows[0]["SMWYAAA002"].ToString() + ")";

        engine.close();
        return fd;
    }
    protected void ListTable_RefreshButtonClick()
    {
        queryData();
    }
    protected void RetrieveAllButton_Click(object sender, EventArgs e)
    {
        //取回所有已轉派之工作清單
        AbstractEngine engine = null;
        try
        {
            string flowType = (String)Session["FlowAdapter"];
            string con1 = (String)Session["NaNaWebService"];
            string con2 = (String)Session["DotJWebService"];
            string account = (String)Session["FlowAccount"];
            string password = (String)Session["FlowPassword"];

            FlowFactory ff = new FlowFactory();
            AbstractFlowAdapter adp = ff.getAdapter(flowType);
            adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
            adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
            fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
            adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

            ReAssignActivity[] wi = null;

            //取出所有尚未處理之流程實例
            wi = adp.fetchReassignedWorkItems((string)Session["UserID"], ListTable.getPageSize().ToString(), ListTable.getCurrentPage().ToString(), "", "", "", "", "", "2");
            
            //將所有尚未處理之流程實例依序取回
            for (int i = 0; i < wi.Length; i++)
            {
                writeLog(new Exception((string)Session["UserID"] + " | " + wi[i].workItemOID + " | " + "系統批次取回代理"));
                adp.retrieveWorkAssignment((string)Session["UserID"], wi[i].OID, "系統批次取回代理");
            }

            adp.logout();

            queryData();

            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwo_maintain_aspx.language.ini", "message", "QueryError1", "取回成功"));
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(te.Message);
            writeLog(te);
        }
    }
    private void changePage(int pageNum)
    {
        bool boflag = true;
        setSession("CURRENTPAGE", pageNum);
        CurrentPage.ValueText = ((int)getSession("CURRENTPAGE")).ToString();

        if (boflag)
        {

            try
            {

                string BoxID = (string)getSession("BoxID");
                SMWSAAA aa = null;
                if (!BoxID.Equals(""))
                {
                    aa = (SMWSAAA)getSession("SMWSAAA");
                }


                ArrayList arys = (ArrayList)getSession("init_DATA");
                DataObjectSet dos = ListTable.dataSource;
                dos.clear();

                int start = ListTable.getPageSize() * (pageNum - 1);
                int end = start + ListTable.getPageSize();
                if (end > arys.Count) end = arys.Count;



                for (int i = start; i < end; i++)
                {
                    DataRow ddr = (DataRow)arys[i];
                    if (ddr.Table.Columns[0].ColumnName.Equals("resda001"))
                    {
                        DataObject ddo = dos.create();
                        ddo.setData("SMWOAAA101", ddr["resdd001"].ToString());
                        ddo.setData("SMWOAAA102", ddr["resdd002"].ToString());
                        ddo.setData("SMWOAAA001", IDProcessor.getID(""));
                        ddo.setData("SMWOAAA002", ddr["resdd001"].ToString() + "-" + ddr["resdd002"].ToString() + "-" + ddr["resdd003"].ToString() + "-" + ddr["resdd004"].ToString() + "-" + ddr["resdd005"].ToString());
                        ddo.setData("SMWOAAA003", ddr["resdd009"].ToString());
                        ddo.setData("SMWOAAA004", ddr["resca002"].ToString());
                        ddo.setData("SMWOAAA005", ddr["FormStatus"].ToString());
                        ddo.setData("SMWOAAA006", ddr["resda031"].ToString());
                        ddo.setData("SMWOAAA007", ddr["resdd008C"].ToString());
                        ddo.setData("SMWOAAA008", "系統管理員直接指派");
                        ddo.setData("SMWOAAA009", ddr["resdd009"].ToString());
                        ddo.Tag = ddr;
                        dos.addDraft(ddo);

                    }
                    else
                    {
                        DataObject ddo = dos.create();
                        ddo.setData("SMWOAAA101", ddr["SMWYAAA003"].ToString());
                        ddo.setData("SMWOAAA102", ddr["SMWYAAA002"].ToString());
                        ddo.setData("SMWOAAA001", IDProcessor.getID(""));
                        ddo.setData("SMWOAAA002", "");
                        ddo.setData("SMWOAAA003", ddr["processInstanceOID"].ToString());
                        ddo.setData("SMWOAAA004", ddr["processName"].ToString());
                        ddo.setData("SMWOAAA005", ddr["processSerialNumber"].ToString());
                        ddo.setData("SMWOAAA006", ddr["subject"].ToString());
                        ddo.setData("SMWOAAA007", ddr["currentPerformerName"].ToString());
                        ddo.setData("SMWOAAA010", ddr["OID"].ToString());
                        ddo.setData("SMWOAAA011", ddr["workItemName"].ToString());
                        switch (ddr["reassignmentType"].ToString())
                        {
                            case "0":
                                ddo.setData("SMWOAAA008", "直接轉派");
                                break;
                            case "1":
                                ddo.setData("SMWOAAA008", "代理轉派");
                                break;
                            case "2":
                                ddo.setData("SMWOAAA008", "逾時代理轉派");
                                break;
                            case "3":
                                ddo.setData("SMWOAAA008", "系統管理員轉派");
                                break;
                            case "4":
                                ddo.setData("SMWOAAA008", "流程負責人轉派");
                                break;
                            case "5":
                                ddo.setData("SMWOAAA008", "取回");
                                break;
                            case "6":
                                ddo.setData("SMWOAAA008", "工作擁有權移交轉派");
                                break;
                            case "7":
                                ddo.setData("SMWOAAA008", "系統管理員工作擁有權移交轉派");
                                break;
                            case "8":
                                ddo.setData("SMWOAAA008", "流程負責人工作擁有權移交轉派");
                                break;


                        }
                        //ddo.setData("SMWOAAA008", ddr["reassignmentType"].ToString());
                        ddo.setData("SMWOAAA009", ddr["workItemCreatedTime"].ToString());
                        ddo.Tag = ddr;
                        dos.addDraft(ddo);
                    }
                }



                ListTable.IsGeneralUse = false;
                ListTable.InputForm = "Detail.aspx";
                ListTable.HiddenField = new string[] { "SMWOAAA001", "SMWOAAA002", "SMWOAAA003", "SMWOAAA005" };
                dos.sort(new string[,] { { "SMWOAAA009", DataObjectConstants.DESC } });
                ListTable.dataSource = dos;
                ListTable.updateTable();

            }
            catch (Exception te)
            {
                MessageBox(te.Message);
                //writeLog(te);
            }
            //setSession("CURLIST", dos);

            //MessageBox("已取得清單");
        }
    }
    protected bool ListTable_NextPageClick()
    {
        if (((int)getSession("TOTALPAGES")) == 0) return false;
        if (ListTable.isShowAll) return false;
        int cup = (int)getSession("CURRENTPAGE");
        int pageNum = cup + 1;
        int total = (int)getSession("TOTALPAGES");
        if (pageNum > total) pageNum = total;
        changePage(pageNum);
        setSession("CURRENTPAGE", pageNum);
        CurrentPage.ValueText = pageNum.ToString();
        DataObjectSet dos = ListTable.dataSource;
        dos.setCurrentPageNum(1);
        ListTable.setCurrentPage(1);

        return true;
    }
    protected bool ListTable_PrevPageClick()
    {
        if (((int)getSession("TOTALPAGES")) == 0) return false;
        if (ListTable.isShowAll) return false;
        int cup = (int)getSession("CURRENTPAGE");
        int pageNum = cup - 1;
        if (pageNum == 0) pageNum = 1;
        changePage(pageNum);
        setSession("CURRENTPAGE", pageNum);
        CurrentPage.ValueText = pageNum.ToString();
        DataObjectSet dos = ListTable.dataSource;
        dos.setCurrentPageNum(1);
        ListTable.setCurrentPage(1);

        return true;
    }
    protected bool ListTable_FirstPageClick()
    {
        if (((int)getSession("TOTALPAGES")) == 0) return false;
        if (ListTable.isShowAll) return false;
        changePage(1);
        setSession("CURRENTPAGE", 1);
        CurrentPage.ValueText = "1";
        DataObjectSet dos = ListTable.dataSource;
        dos.setCurrentPageNum(1);
        ListTable.setCurrentPage(1);

        return true;
    }
    protected bool ListTable_LastPageClick()
    {
        if (((int)getSession("TOTALPAGES")) == 0) return false;
        if (ListTable.isShowAll) return false;
        int tp = (int)getSession("TOTALPAGES");
        changePage(tp);
        setSession("CURRENTPAGE", tp);
        CurrentPage.ValueText = tp.ToString();
        DataObjectSet dos = ListTable.dataSource;
        dos.setCurrentPageNum(tp);
        ListTable.setCurrentPage(tp);

        return true;
    }
    protected void CurrentPage_TextChanged(object sender, EventArgs e)
    {
        if (((int)getSession("TOTALPAGES")) == 0) return;
        int cp = 1;
        try
        {
            cp = int.Parse(CurrentPage.ValueText);
        }
        catch
        {
            cp = 1;
        }
        int tp = (int)getSession("TOTALPAGES");
        if (cp > tp) cp = tp;
        changePage(cp);
    }
    protected bool ListTable_ChangePageSizeClick(int pagesize)
    {
        if (((int)getSession("TOTALPAGES")) == 0) return false;
        ArrayList ary = (ArrayList)getSession("init_DATA");
        int tp = (int)com.dsc.kernal.utility.Utility.Round((decimal)(ary.Count / pagesize), 0);
        if (tp * pagesize < ary.Count)
        {
            tp++;
        }
        setSession("TOTALPAGES", tp);
        TotalPages.Text = "/共" + tp.ToString() + "頁";
        setSession("CURRENTPAGE", 1);
        changePage(1);

        return true;
    }
    protected void ListTable_ShowAllPageClick()
    {
        ListTable.isShowAll = false;
        ArrayList ary = (ArrayList)getSession("init_DATA");
        ListTable.setCurrentPage(1);
        ListTable.dataSource.setCurrentPageNum(1);
        ListTable.PageSize = ary.Count;
        ListTable.dataSource.setPageSize(ary.Count);
        setSession("TOTALPAGES", 1);
        TotalPages.Text = "/共1頁";
        setSession("CURRENTPAGE", 1);
        changePage(1);
        ListTable.isShowAll = true;
    }
    protected void ListTable_ShowPagingClick()
    {
        ArrayList ary = (ArrayList)getSession("init_DATA");
        //ListTable.setCurrentPage(1);
        ListTable.dataSource.setCurrentPageNum(1);
        int pagesize = 10;
        ListTable.PageSize = pagesize;
        ListTable.isShowAll = false;
        ListTable.dataSource.setPageSize(pagesize);
        int tp = (int)com.dsc.kernal.utility.Utility.Round((decimal)(ary.Count / pagesize), 0);
        if (tp * pagesize < ary.Count)
        {
            tp++;
        }
        if (ary.Count == 0)
        {
            CurrentPage.ValueText = "0";
        }
        else
        {
            CurrentPage.ValueText = "1";
        }
        setSession("TOTALPAGES", tp);
        TotalPages.Text = "/共" + tp.ToString() + "頁";
        setSession("CURRENTPAGE", 1);
        changePage(1);
    }
    ///<summary>
    /// DataTableJoin:將兩個DataTable 以傳入的關聯欄位 Join (如SQL Inner JOIN)
    ///</summary>
    /// <param name="dtA">Master Table</param>
    /// <param name="dtB">Detail Table</param>
    /// <param name="ColA">Master Table關聯欄位(多個)</param>
    /// <param name="ColB">Detail Table關聯欄位(多個)</param>
    /// <returns>Join之後的DataTble</returns>
    public DataTable DataTableJoin(DataTable dtA, DataTable dtB, DataColumn[] ColA, DataColumn[] ColB)
    {

        DataTable tJoinDt = new DataTable("JoinDt");
        DataSet tDs = new DataSet();
        tDs.Tables.AddRange(new DataTable[] { dtA.Copy(), dtB.Copy() });
        DataColumn[] ColMas = new DataColumn[ColA.Length];
        for (int i = 0; i < ColMas.Length; i++)
        {
            ColMas[i] = tDs.Tables[0].Columns[ColA[i].ColumnName];
        }
        DataColumn[] ColDet = new DataColumn[ColB.Length];
        for (int i = 0; i < ColDet.Length; i++)
        {
            ColDet[i] = tDs.Tables[1].Columns[ColB[i].ColumnName];
        }
        DataRelation DRel = new DataRelation("JoinRelation", ColMas, ColDet, false);
        tDs.Relations.Add(DRel);
        for (int i = 0; i < dtA.Columns.Count; i++)
        {
            tJoinDt.Columns.Add(dtA.Columns[i].ColumnName, dtA.Columns[i].DataType);
        }
        for (int i = 0; i < dtB.Columns.Count; i++)
        {
            if (!tJoinDt.Columns.Contains(dtB.Columns[i].ColumnName))
            {
                tJoinDt.Columns.Add(dtB.Columns[i].ColumnName, dtB.Columns[i].DataType);
            }
            else
            {
                tJoinDt.Columns.Add(dtB.Columns[i].ColumnName + "_2", dtB.Columns[i].DataType);
            }
        }
        tJoinDt.BeginLoadData();

        foreach (DataRow drA in tDs.Tables[0].Rows)
        {
            DataRow[] drA_Child = drA.GetChildRows(DRel);
            if (drA_Child != null && drA_Child.Length > 0)
            {
                object[] ArrMas = drA.ItemArray;
                foreach (DataRow drB in drA_Child)
                {
                    object[] ArrDet = drB.ItemArray;
                    object[] ArrJoin = new object[ArrMas.Length + ArrDet.Length];
                    Array.Copy(ArrMas, 0, ArrJoin, 0, ArrMas.Length);
                    Array.Copy(ArrDet, 0, ArrJoin, ArrMas.Length, ArrDet.Length);
                    tJoinDt.LoadDataRow(ArrJoin, true);
                }
            }
        }
        tJoinDt.EndLoadData();
        return tJoinDt;
    }
    public DataTable DataTableJoin(DataTable dtA, DataTable dtB, string ColA, string ColB)
    {
        return DataTableJoin(dtA, dtB, new DataColumn[] { dtA.Columns[ColA] }, new DataColumn[] { dtB.Columns[ColB] });
    }
    public DataTable DataTableJoin(DataTable dtA, DataTable dtB, DataColumn ColA, DataColumn ColB)
    {
        return DataTableJoin(dtA, dtB, new DataColumn[] { ColA }, new DataColumn[] { ColB });
    }
}
