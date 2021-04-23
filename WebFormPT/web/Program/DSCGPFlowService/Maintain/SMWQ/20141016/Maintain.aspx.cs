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
using WebServerProject.flow.SMWQ;
using WebServerProject.flow.SMWT;
using WebServerProject;
using DSCWebControl;

public partial class Program_DSCGPFlowService_Maintain_SMWQ_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        maintainIdentity = "SMWQ";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                //CL_Chang
				string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
				
				//CL_Chang
				string[,] ids = new string[,]{
                    {"",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "ids1", "不限定")},
                    {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsI", "進行中")},
                    {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsY", "已完成")},
                    {"5",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsN", "已終止")},
                    {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsW", "已撤銷")}
                };
                SMWYAAA020.setListItem(ids);

                string sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA order by SMWBAAA004";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ids = new string[ds.Tables[0].Rows.Count + 1, 2];
                    ids[0, 0] = "";
                    ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "ids1", "不限定");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ids[i + 1, 0] = ds.Tables[0].Rows[i][0].ToString();
                        ids[i + 1, 1] = ds.Tables[0].Rows[i][1].ToString();
                    }
                    SMWYAAA003.setListItem(ids);
                }
				
				if (Request.QueryString["BoxID"] == null)
                {
                    setSession("BoxID", "");
                }
                else
                {
                    setSession("BoxID", Request.QueryString["BoxID"]);
					//CL_Chang
                    //string connectString = (string)Session["connectString"];
                    //string engineType = (string)Session["engineType"];
                    //IOFactory factory = new IOFactory();
                    //AbstractEngine engine = factory.getEngine(engineType, connectString);

                    SMWTAgent agent = new SMWTAgent();
                    agent.engine = engine;
                    agent.query("SMWTAAA002='" + com.dsc.kernal.utility.Utility.filter(Request.QueryString["BoxID"]) + "'");
                    if (agent.defaultData.getAvailableDataObjectCount() == 0)
                    {
                        engine.close();
                        Response.Redirect("NoSetting.aspx");
                    }
                    setSession("SMWTAAA", agent.defaultData.getAvailableDataObject(0));
                    //engine.close();
                }
				engine.close();
                ListTable.CurPanelID = CurPanelID;
                ListTable.NoDelete = true;
                ListTable.NoAdd = true;

                //queryData();
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
            string sql = "";

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

            TraceProcessInstance[] wi = null;
			string flowId = SMWYAAA003.ValueText;
            if (!flowId.Equals(""))
            {
                flowId = "'" + flowId + "'";
            }
            wi = adp.traceProcInstances((string)Session["UserID"], "1000000", "1", "1", "0", flowId, Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText);

            //這裡要讀取CUSOMENOTICE資料表
            sql = "select * from CUSTOMENOTICE where RECEIVERID='" + (string)Session["UserID"] + "' and PROCESSTYPE ='RELATED'";
            if (!Subject.ValueText.Equals(""))
            {
                sql += " and SUBJECT like '%" + Subject.ValueText + "%'";
            }
            if (!StartTime.ValueText.Equals(""))
            {
                sql += " and SENDTIME >= '" + StartTime.ValueText + "'";
            }
            if (!EndTime.ValueText.Equals(""))
            {
                sql += " and SENDTIME <= '" + EndTime.ValueText + "'";
            }
			if (!SMWYAAA003.ValueText.Equals(""))
            {
                sql += " and PDID = '" + SMWYAAA003.ValueText + "'";
            }
			
            DataSet dsc = engine.getDataSet(sql, "TEMP");
            ArrayList twi = new ArrayList();
            for (int i = 0; i < dsc.Tables[0].Rows.Count; i++)
            {
                TraceProcessInstance wis = new TraceProcessInstance();
                wis.OID = dsc.Tables[0].Rows[i]["OBJECTGUID"].ToString();
                wis.createdTime = dsc.Tables[0].Rows[i]["SENDTIME"].ToString();
                wis.processName = dsc.Tables[0].Rows[i]["PROCESSNAME"].ToString();
                wis.serialNo = dsc.Tables[0].Rows[i]["PROCESSSERIALNUMBER"].ToString();
                wis.requesterName = dsc.Tables[0].Rows[i]["SENDERNAME"].ToString();
                wis.state = dsc.Tables[0].Rows[i]["RELATEDSTATE"].ToString();
                wis.allowByPass = false;
                wis.subject = dsc.Tables[0].Rows[i]["SUBJECT"].ToString();

                twi.Add(wis);
            }

            TraceProcessInstance[] wiu = new TraceProcessInstance[wi.Length + twi.Count];
            for (int i = 0; i < wi.Length; i++)
            {
                wiu[i] = wi[i];
            }
            for (int i = 0; i < twi.Count; i++)
            {
                wiu[i + wi.Length] = (TraceProcessInstance)twi[i];
            }
            wi = wiu;

            adp.logout();

            string BoxID = (string)getSession("BoxID");
            SMWTAAA aa = null;
            if (!BoxID.Equals(""))
            {
                aa = (SMWTAAA)getSession("SMWTAAA");
            }

            //DataObjectSet dos = new DataObjectSet();
            //dos.setAssemblyName("WebServerProject");
            //dos.setChildClassString("WebServerProject.flow.SMWQ.SMWQAAA");
            //dos.setTableName("SMWQAAA");

            #region 重新宣告SMWQ XML結構
            string qstr = "select '' as SMWQAAA101 ,'' as SMWQAAA102 ," +
                                " SMWQAAA.SMWQAAA001,SMWQAAA.SMWQAAA002,SMWQAAA.SMWQAAA003,SMWQAAA.SMWQAAA004,SMWQAAA.SMWQAAA005," +
                                " SMWQAAA.SMWQAAA006,SMWQAAA.SMWQAAA007,SMWQAAA.SMWQAAA008,SMWQAAA.SMWQAAA009,SMWQAAA.SMWQAAA010," +
                                " SMWQAAA.D_INSERTUSER,SMWQAAA.D_INSERTTIME,SMWQAAA.D_MODIFYUSER,SMWQAAA.D_MODIFYTIME " +
                                " from SMWQAAA ";

            DataObjectSet dos = new DataObjectSet();
            string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">";
            schema += "<queryStr>" + qstr + "</queryStr>";
            schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
            schema += "  <fieldDefinition>";

            schema += "    <field dataField=\"SMWQAAA101\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"表單代號\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWQAAA102\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"表單單號\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWQAAA001\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"唯一識別碼\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWQAAA002\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"流程實例識別碼\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWQAAA003\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"流程發起時間\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWQAAA004\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"流程名稱\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWQAAA005\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"流程實例序號\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWQAAA006\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"發起者姓名\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWQAAA007\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"流程狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>";
            schema += "    <field dataField=\"SMWQAAA008\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"是否允許ByPass\" showName=\"N:否;Y:是\"/>";
            schema += "    <field dataField=\"SMWQAAA009\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
            schema += "    <field dataField=\"SMWQAAA010\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"目前處理活動名稱\" showName=\"\"/>";
            schema += "    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>";
            schema += "    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
            schema += "    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>";
            schema += "    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>";

            schema += "  </fieldDefinition>";
            schema += "  <identityField>";
            schema += "    <field dataField=\"SMWQAAA001\"/>";
            schema += "  </identityField>";
            schema += "  <keyField>";
            schema += "    <field dataField=\"SMWQAAA001\"/>";
            schema += "  </keyField>";
            schema += "  <allowEmptyField>";

            schema += "    <field dataField=\"SMWQAAA002\"/>";
            schema += "    <field dataField=\"SMWQAAA006\"/>";
            schema += "    <field dataField=\"SMWQAAA007\"/>";
            schema += "    <field dataField=\"SMWQAAA009\"/>";
            schema += "    <field dataField=\"SMWQAAA010\"/>";
            schema += "    <field dataField=\"SMWQAAA101\"/>";
            schema += "    <field dataField=\"SMWQAAA102\"/>";
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
            //OID 1
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "OID";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //createdTime 2
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "createdTime";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //processName 3
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "processName";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //serialNo 4
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "serialNo";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //requesterName 5
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "requesterName";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //state 6
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "state";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //allowByPass 7
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.Boolean");
            WorkItemDc.ColumnName = "allowByPass";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //subject 8
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "subject";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            //runningActNames 9
            WorkItemDc = new DataColumn();
            WorkItemDc.DataType = System.Type.GetType("System.String");
            WorkItemDc.ColumnName = "runningActNames";
            WorkItemDc.Unique = false;
            WorkItemDt.Columns.Add(WorkItemDc);
            #endregion

            //for (int i = 0; i < wi.Length; i++)
            //{
            //    DataRow WorkItemDr = WorkItemDt.NewRow();
            //    WorkItemDr["FlowNo"] = wi[i].OID;
            //    WorkItemDt.Rows.Add(WorkItemDr);
            //}
            //DataSet SMWYDs = engine.getDataSet("Select * from SMWYAAA", "TEMP");
            //DataTable SMWYDt = SMWYDs.Tables[0];
            //if (SMWYDt.Rows.Count > 0)
            //{
            //    DataTable FiliterDt = PublicFunc.DataTableJoin(SMWYDt, WorkItemDt, "SMWYAAA005", "FlowNo");
            //}

            #endregion

            for (int i = 0; i < wi.Length; i++)
            {
                //CL_Chang
				if (!SMWYAAA020.ValueText.Equals(""))
                {
					//進行中 1, 已完成 3, 已撤銷 4, 已終止 5
                    if (!wi[i].state.Equals(SMWYAAA020.ValueText))
                        continue;
				}
				if (!BoxID.Equals(""))
                {
                    bool hasFound = false;
                    for (int z = 0; z < aa.getChild("SMWTAAB").getAvailableDataObjectCount(); z++)
                    {
                        if (wi[i].processName.Equals(aa.getChild("SMWTAAB").getAvailableDataObject(z).getData("SMWTAAB004")))
                        {
                            hasFound = true;
                            break;
                        }
                    }
                    if (aa.SMWTAAA004.Equals("0") && hasFound)
                    {
                        continue;
                    }
                    if (aa.SMWTAAA004.Equals("1") && (!hasFound))
                    {
                        continue;
                    }

                    hasFound = false;
                    for (int z = 0; z < aa.getChild("SMWTAAC").getAvailableDataObjectCount(); z++)
                    {
                        if (wi[i].runningActNames.Equals(aa.getChild("SMWTAAC").getAvailableDataObject(z).getData("SMWTAAC003")))
                        {
                            hasFound = true;
                            break;
                        }
                    }
                    if (aa.SMWTAAA005.Equals("0") && hasFound)
                    {
                        continue;
                    }
                    if (aa.SMWTAAA005.Equals("1") && (!hasFound))
                    {
                        continue;
                    }
                }


                DataRow WorkItemDr = WorkItemDt.NewRow();
                WorkItemDr["OID"] = wi[i].OID;
                WorkItemDr["createdTime"] = wi[i].createdTime;
                WorkItemDr["processName"] = wi[i].processName;
                WorkItemDr["serialNo"] = wi[i].serialNo;
                WorkItemDr["requesterName"] = wi[i].requesterName;
                WorkItemDr["state"] = wi[i].state;
                WorkItemDr["allowByPass"] = wi[i].allowByPass;
                WorkItemDr["subject"] = wi[i].subject;
                WorkItemDr["runningActNames"] = wi[i].runningActNames;
                WorkItemDt.Rows.Add(WorkItemDr);
            }

            ArrayList arys = new ArrayList();

            DataSet SMWYDs = engine.getDataSet("Select * from SMWYAAA", "TEMP");
            DataTable SMWYDt = SMWYDs.Tables[0];
            if (SMWYDt.Rows.Count > 0)
            {
                DataTable FiliterDt = DataTableJoin(SMWYDt, WorkItemDt, "SMWYAAA005", "serialNo");
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
                    //if ((i == 0) && (j==1)) MessageBox(ob1.Table.Columns[0].ColumnName);
                    if (ob1.Table.Columns[0].ColumnName.Equals("SMWYAAA001"))
                    {
                        ct1 = ob1["createdTime"].ToString();
                    }
                    else
                    {
                        ct1 = ob1["resdd009"].ToString();
                    }
                    if (ob2.Table.Columns[0].ColumnName.Equals("SMWYAAA001"))
                    {
                        ct2 = ob2["createdTime"].ToString();
                    }
                    else
                    {
                        ct2 = ob2["resdd009"].ToString();
                    }
                    if (ct1.CompareTo(ct2) < 0)
                    {
                        //arys.Remove(ob1);
                        //arys.Insert(i, ob1);
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
            ListTable.HiddenField = new string[] { "SMWQAAA001", "SMWQAAA002", "SMWQAAA005", "SMWQAAA008" };
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
        try
        {
            //TraceProcessInstance wi = (TraceProcessInstance)objects.Tag;
            //解決 DataRow的問題 by Jack 20130510
            TraceProcessInstance wi = new TraceProcessInstance();
            wi.OID = ((DataRow)objects.Tag)[1].ToString();
            wi.createdTime = ((DataRow)objects.Tag)[2].ToString();
            wi.processName = ((DataRow)objects.Tag)[3].ToString();
            wi.serialNo = ((DataRow)objects.Tag)[4].ToString();
            wi.requesterName = ((DataRow)objects.Tag)[5].ToString();
            wi.state = ((DataRow)objects.Tag)[6].ToString();
            //wi.allowByPass = Convert.ToBoolean(((DataRow)objects.Tag)[7]);
            wi.subject = ((DataRow)objects.Tag)[8].ToString();
            wi.runningActNames = ((DataRow)objects.Tag)[9].ToString();
            
            

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            string sql = "";
            DataSet ds = null;

            //建立FlowStatusData物件
            FlowStatusData fd = new FlowStatusData();

            //取得資料物件代碼, 由原稿取得
            sql = "select SMWYAAA019, SMWYAAA003, SMWYAAA004, SMWYAAA002 from SMWYAAA where SMWYAAA005='" + Utility.filter(wi.serialNo) + "'";
            ds = engine.getDataSet(sql, "TEMP");
            string objectGUID = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                objectGUID = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                //原搞不存在, 代表可能為發起參考流程
                sql = "select CURGUID, FLOWID, CURFORMNAME as SMWYAAA004, '' as SMWYAAA002 from FORMRELATION where FLOWGUID='" + Utility.filter(wi.serialNo) + "'";
                ds = engine.getDataSet(sql, "TEMP");
                objectGUID = ds.Tables[0].Rows[0][0].ToString();
            }

            fd.ACTID = "";
            fd.ACTName = Server.UrlEncode(wi.runningActNames);
            fd.FlowGUID = wi.serialNo;
            fd.HistoryGUID = "";
            fd.ObjectGUID = objectGUID;
            fd.PDID = ds.Tables[0].Rows[0][1].ToString();
            fd.PDVer = "";
            //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
            //目前都給他為ProcessModify
            fd.UIStatus = FlowStatusData.FormRollback;
            fd.WorkItemOID = "";
            fd.TargetWorkItemOID = "";

            //這裡要根據SMWDAAA判定是否有ProcessNew
            ListTable.FormTitle = ds.Tables[0].Rows[0]["SMWYAAA004"].ToString() + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwq_maintain_aspx.language.ini", "message", "List", "(單號:") + ds.Tables[0].Rows[0]["SMWYAAA002"].ToString() + ")";

            engine.close();
            return fd;
        }
        catch (Exception ze)
        {
            MessageBox(ze.Message);
            throw (ze);
        }
    }
    
    protected void ListTable_RefreshButtonClick()
    {
        queryData();
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
                SMWTAAA aa = null;
                if (!BoxID.Equals(""))
                {
                    aa = (SMWTAAA)getSession("SMWTAAA");
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
                        ddo.setData("SMWQAAA101", ddr["resdd001"].ToString());
                        ddo.setData("SMWQAAA102", ddr["resdd002"].ToString());
                        ddo.setData("SMWQAAA001", IDProcessor.getID(""));
                        ddo.setData("SMWQAAA002", ddr["resdd001"].ToString() + "-" + ddr["resdd002"].ToString() + "-" + ddr["resdd003"].ToString() + "-" + ddr["resdd004"].ToString() + "-" + ddr["resdd005"].ToString());
                        ddo.setData("SMWQAAA003", ddr["resdd009"].ToString());
                        ddo.setData("SMWQAAA004", ddr["resca002"].ToString());
                        ddo.setData("SMWQAAA005", ddr["FormStatus"].ToString());
                        ddo.setData("SMWQAAA006", ddr["resda016C"].ToString());
                        ddo.setData("SMWQAAA007", ddr["resdd015C"].ToString());
                        ddo.setData("SMWQAAA008", "N");
                        ddo.setData("SMWQAAA009", ddr["resda031"].ToString());
                        ddo.setData("SMWQAAA010", "");
                        ddo.Tag = ddr;
                        dos.addDraft(ddo);

                    }
                    else
                    {
                        DataObject ddo = dos.create();
                        ddo.setData("SMWQAAA101", ddr["SMWYAAA003"].ToString());
                        ddo.setData("SMWQAAA102", ddr["SMWYAAA002"].ToString());
                        ddo.setData("SMWQAAA001", IDProcessor.getID(""));
                        ddo.setData("SMWQAAA002", ddr["OID"].ToString());
                        ddo.setData("SMWQAAA003", ddr["createdTime"].ToString());
                        ddo.setData("SMWQAAA004", ddr["processName"].ToString());
                        ddo.setData("SMWQAAA005", ddr["serialNo"].ToString());
                        ddo.setData("SMWQAAA006", ddr["requesterName"].ToString());
                        ddo.setData("SMWQAAA007", ddr["state"].ToString());
                        if ((bool)ddr["allowByPass"])
                            ddo.setData("SMWQAAA008", "Y");
                        else
                            ddo.setData("SMWQAAA008", "N");
                        ddo.setData("SMWQAAA009", ddr["subject"].ToString());
                        ddo.setData("SMWQAAA010", ddr["runningActNames"].ToString()); ddo.Tag = ddr;
                        dos.addDraft(ddo);
                    }
                }


                dos.sort(new string[,] { { "SMWQAAA003", DataObjectConstants.DESC } });
                ListTable.IsGeneralUse = false;
                ListTable.InputForm = "Detail.aspx";
                ListTable.HiddenField = new string[] { "SMWQAAA001", "SMWQAAA002", "SMWQAAA005", "SMWQAAA008" };
                ListTable.dataSource = dos;
                ListTable.updateTable();

            }
            catch (Exception te)
            {
                MessageBox(te.Message);
                writeLog(te);
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
