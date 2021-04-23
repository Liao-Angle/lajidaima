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
using com.dsc.flow.client;
using com.dsc.kernal.agent;
using WebServerProject.flow.SMWY;
using WebServerProject.flow.SMWG;
using WebServerProject;
using DSCWebControl;
using System.Collections.Specialized;

public partial class ECPEF_EFBOX_EFBOX : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "EFBOX";
        ApplicationID = "SYSTEM";
        ModuleID = "EFBOX";

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {

                ListTable.CurPanelID = CurPanelID;
                string BoxID = Utility.CheckCrossSiteScripting(Request.QueryString["BoxID"]);
                setSession("BoxID", BoxID);

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                ListTable.NoDelete = true;

                switch (BoxID)
                {
                    case "InBox":
                        #region 收件夾
                        break;
                        #endregion 收件夾
                    case "Rollback":
                        #region 重辦
                        break;
                        #endregion 重辦
                    case "Fowarded":
                        #region 轉寄
                        break;
                        #endregion 轉寄
                    case "Info":
                        #region 通知
                        break;
                        #endregion 通知
                    case "StepNote":
                        #region 逐級通知
                        break;
                        #endregion 逐級通知
                    case "Sent":
                        #region 原稿
                        break;
                        #endregion 原稿
                    case "Reply":
                        #region 回函
                        break;
                        #endregion 回函
                    case "Load":
                        #region 草稿
                        ListTable.NoDelete = false;
                        break;
                        #endregion 草稿
                    default:
                        Response.Redirect("NoSetting.aspx");
                        break;
                }

                //string sql = "select * from SMWIAAA where SMWIAAA002='" + Utility.filter(BoxID) + "'";
                string sql = "select * from SMWIAAA where SMWIAAA002='InBox'";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    engine.close();
                    Response.Redirect("NoSetting.aspx");
                }

                string[,] ids;

                if (BoxID.Equals("InBox"))
                {
                    ViewTimes.Display = true;
                    //未簽核 已簽核  多國語系要記得
                    ids = new string[,]{
                        {"A",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids10", "全部")},
                        {"U",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids11", "未簽核")},
                        {"R",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids12", "已簽核")}
                    };
                    ViewTimes.setListItem(ids);
                }
                else { ViewTimes.Display = false; }
                //取得顯示欄位
                ArrayList hFields = new ArrayList();
                setSession("HiddenFields", hFields);


                //Amos
                //SysParam sp = new SysParam(engine);
                //string sp8 = sp.getParam("EF2KWebDB");
                string SP8str = GetINDUSConnSTR("EF2KWebDB", engine);//EF DB connection String
                AbstractEngine sp8engine = factory.getEngine(EngineConstants.SQL, SP8str);
                DataSet sp8ds = sp8engine.getDataSet("select resca001, resca002 from resca where resca086='2' and resca001<>'PER013' and resca001<>'PER006OVT'", "TEMP");
                sp8engine.close();

                ids = new string[sp8ds.Tables[0].Rows.Count + 1, 2];
                ids[0, 0] = "";
                ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " idsA", "不限定");
                for (int i = 0; i < sp8ds.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1, 0] = sp8ds.Tables[0].Rows[i][0].ToString();
                    ids[i + 1, 1] = "(SP8)" + sp8ds.Tables[0].Rows[i][1].ToString() + "(" + sp8ds.Tables[0].Rows[i][0].ToString() + ")";
                }
                ProcessIDList.setListItem(ids);

                //取得欄位顯示順序
                string[] fieldOrder = ds.Tables[0].Rows[0]["FIELDORDER"].ToString().Split(new char[] { ';' });
                ListTable.reOrderField(fieldOrder);
                setSession("fieldOrder", fieldOrder);

                engine.close();

                queryData();
            }
        }
    }

    private void queryData()
    {
        bool boflag = true;
        if (!StartTime.ValueText.Equals("") && !EndTime.ValueText.Equals(""))
        {
            DateTime stDate = Convert.ToDateTime(StartTime.ValueText);
            DateTime edDate = Convert.ToDateTime(EndTime.ValueText);
            if (edDate.CompareTo(stDate) < 0)
            {
                MessageBox("工作建立時間迄時需大於起時");
                boflag = false;
            }
        }

        if (boflag)
        {

            AbstractEngine engine = null;
            try
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                string sql = "";

                sql = "select SMVPAAA026 from SMVPAAA";
                string isDB = (string)engine.executeScalar(sql);

                sql = "select SMVPAAA036 from SMVPAAA";
                string isSubstitute = (string)engine.executeScalar(sql);


                //Amos
                //取得EasyFlow的待簽核資料
                DataSet sp8 = getSP8FormInBox(engine, (string)getSession("BoxID"));
                if (sp8.Tables[0].Rows.Count > 0)
                {

                    //取得單號
                    string taging = "'*'";

                    sql = "select SMWYAAA002, SMWYAAA005, SMWYAAA019, SMWYAAA007 from SMWYAAA where SMWYAAA005 in (" + taging + ") union select SMWYAAA002, FLOWGUID as SMWYAAA005, SMWYAAA019, SMWYAAA007 from SMWYAAA inner join FORMRELATION on ORIGUID=SMWYAAA019 where FLOWGUID in (" + taging + ")";
                    DataSet sh = engine.getDataSet(sql, "TEMP");

                    sql = "select SMWYAAA005, count(*) from SMWYAAA inner join FILEITEM on SMWYAAA019=JOBID group by SMWYAAA005";
                    DataSet att = engine.getDataSet(sql, "TEMP");

                    string qstr = "select GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME ";

                    DataSet extData = null;
                    DataSet det = null;

                    qstr += " from SMWL";

                    DataObjectSet dos = new DataObjectSet();
                    string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                    schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">";
                    schema += "<queryStr>" + qstr + "</queryStr>";
                    schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
                    schema += "  <fieldDefinition>";

                    schema += "    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>";
                    schema += "    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>";
                    schema += "    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"40\" defaultValue=\"\" displayName=\"重要性\" showName=\"0:低;1:中;2:高\"/>";
                    schema += "    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>";
                    schema += "    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程名稱\" showName=\"\"/>";
                    schema += "    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>";
                    schema += "    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>";
                    schema += "    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
                    schema += "    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人\" showName=\"\"/>";
                    schema += "    <field dataField=\"CREATETIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                    schema += "    <field dataField=\"WORKTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作性質\" showName=\"\"/>";
                    schema += "    <field dataField=\"VIEWTIMES\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"讀取次數\" showName=\"\"/>";
                    schema += "    <field dataField=\"WORKASSIGNMENT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作指派識別\" showName=\"\"/>";
                    schema += "    <field dataField=\"ASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作類型\" showName=\"\"/>";
                    schema += "    <field dataField=\"REASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"轉派類型\" showName=\"\"/>";
                    schema += "    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>";
                    schema += "    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                    schema += "    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>";
                    schema += "    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>";

                    if (det != null)
                    {
                        for (int i = 0; i < det.Tables[0].Rows.Count; i++)
                        {
                            schema += "    <field dataField=\"" + det.Tables[0].Rows[i][0].ToString() + "\" typeField=\"STRING\" lengthField=\"50000\" defaultValue=\"\" displayName=\"" + det.Tables[0].Rows[i][2].ToString() + "\" showName=\"\"/>";
                        }
                    }

                    schema += "  </fieldDefinition>";
                    schema += "  <identityField>";
                    schema += "    <field dataField=\"GUID\"/>";
                    schema += "  </identityField>";
                    schema += "  <keyField>";
                    schema += "    <field dataField=\"GUID\"/>";
                    schema += "  </keyField>";
                    schema += "  <allowEmptyField>";

                    schema += "    <field dataField=\"GUID\"/>";
                    schema += "    <field dataField=\"ATTACH\"/>";
                    schema += "    <field dataField=\"IMPORTANT\"/>";
                    schema += "    <field dataField=\"CURRENTSTATE\"/>";
                    schema += "    <field dataField=\"PROCESSNAME\"/>";
                    schema += "    <field dataField=\"SHEETNO\"/>";
                    schema += "    <field dataField=\"WORKITEMNAME\"/>";
                    schema += "    <field dataField=\"SUBJECT\"/>";
                    schema += "    <field dataField=\"USERNAME\"/>";
                    schema += "    <field dataField=\"CREATETIME\"/>";
                    schema += "    <field dataField=\"WORKTYPE\"/>";
                    schema += "    <field dataField=\"VIEWTIMES\"/>";
                    schema += "    <field dataField=\"WORKASSIGNMENT\"/>";
                    schema += "    <field dataField=\"ASSIGNMENTTYPE\"/>";
                    schema += "    <field dataField=\"REASSIGNMENTTYPE\"/>";
                    schema += "    <field dataField=\"D_INSERTUSER\"/>";
                    schema += "    <field dataField=\"D_INSERTTIME\"/>";
                    schema += "    <field dataField=\"D_MODIFYUSER\"/>";
                    schema += "    <field dataField=\"D_MODIFYTIME\"/>";

                    if (det != null)
                    {
                        for (int i = 0; i < det.Tables[0].Rows.Count; i++)
                        {
                            schema += "    <field dataField=\"" + det.Tables[0].Rows[i][1].ToString() + "\" />";
                        }
                    }

                    schema += "  </allowEmptyField>";
                    schema += "  <nonUpdateField>";
                    schema += "  </nonUpdateField>";
                    schema += "</DataObject>";
                    dos.dataObjectSchema = schema;

                    dos.isNameLess = true;

                    ArrayList arys = new ArrayList();
                    //Amos
                    //處理SP8
                    for (int i = 0; i < sp8.Tables[0].Rows.Count; i++)
                    {
                        arys.Add(sp8.Tables[0].Rows[i]);
                        /*
                          DataObject ddo = dos.create();
                           ddo.setData("GUID", sp8.Tables[0].Rows[i]["resdd001"].ToString() + "-" + sp8.Tables[0].Rows[i]["resdd002"].ToString());
                           ddo.setData("CURRENTSTATE", "0");
                           ddo.setData("PROCESSNAME", "\t" + sp8.Tables[0].Rows[i]["resca002"].ToString());
                           ddo.setData("SHEETNO", sp8.Tables[0].Rows[i]["resdd002"].ToString());
                           ddo.setData("IMPORTANT", "");
                           ddo.setData("ATTACH", "");
                           ddo.setData("WORKITEMNAME", "");
                           ddo.setData("SUBJECT", sp8.Tables[0].Rows[i]["resda031"].ToString());
                           ddo.setData("USERNAME", "");
                           ddo.setData("CREATETIME", sp8.Tables[0].Rows[i]["resdd009"].ToString());
                           ddo.setData("WORKTYPE", "");
                           ddo.setData("ASSIGNMENTTYPE", "");
                           ddo.setData("REASSIGNMENTTYPE", "");
                           ddo.setData("VIEWTIMES", "");
                           ddo.setData("WORKASSIGNMENT", "");
                           ddo.setData("D_INSERTUSER", "SYSTEM");
                           ddo.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                           ddo.setData("D_MODIFYUSER", "");
                           ddo.setData("D_MODIFYTIME", "");
                           ddo.Tag = sp8.Tables[0].Rows[i];
                           dos.add(ddo);
                       */
                    }

                    setSession("init_DATA", arys);
                    TotalRows.Text = "共" + arys.Count.ToString() + "筆資料";
                    int totalpages = (int)com.dsc.kernal.utility.Utility.Round((decimal)(arys.Count / ListTable.getPageSize()), 0);
                    if (totalpages * ListTable.getPageSize() < arys.Count)
                    {
                        totalpages++;
                    }
                    setSession("TOTALPAGES", totalpages);
                    TotalPages.Text = "/共" + totalpages.ToString() + "頁";

                    ListTable.IsGeneralUse = false;
                    ListTable.InputForm = "Detail.aspx";
                    ListTable.CurPageUID = PageUniqueID;
                    ArrayList hFields = (ArrayList)getSession("HiddenFields");
                    string[] hf = new string[hFields.Count + 12];
                    hf[0] = "GUID";
                    hf[1] = "WORKASSIGNMENT";
                    hf[2] = "ASSIGNMENTTYPE";
                    hf[3] = "REASSIGNMENTTYPE";
                    hf[4] = "CURRENTSTATE";
                    hf[5] = "IMPORTANT";
                    hf[6] = "ATTACH";
                    hf[7] = "USERNAME";
                    hf[8] = "WORKTYPE";
                    hf[9] = "VIEWTIMES";
                    hf[10] = "WORKITEMNAME";
                    hf[11] = "SHEETNO";

                    for (int i = 0; i < hFields.Count; i++)
                    {
                        hf[i + 4] = (string)hFields[i];
                    }
                    ListTable.HiddenField = hf;
                    ListTable.WidthMode = 1;
                    ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                    ListTable.dataSource = dos;
                    string[] fieldOrder = (string[])getSession("fieldOrder");
                    ListTable.reOrderField(fieldOrder);
                    ListTable.updateTable();



                    setSession("CURLIST", dos);
                    setSession("CURRENTPAGE", 1);
                    //changePage(1);
			ListTable_ShowPagingClick();

                }
                else
                {
                    string qstr = "select GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME from SMWL";


                    DataObjectSet dos = new DataObjectSet();
                    string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                    schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">";
                    schema += "<queryStr>" + qstr + "</queryStr>";
                    schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
                    schema += "  <fieldDefinition>";

                    schema += "    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>";
                    schema += "    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>";
                    schema += "    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"重要性\" showName=\"\"/>";
                    schema += "    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>";
                    schema += "    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程名稱\" showName=\"\"/>";
                    schema += "    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>";
                    schema += "    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>";
                    schema += "    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
                    schema += "    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人\" showName=\"\"/>";
                    schema += "    <field dataField=\"CREATETIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                    schema += "    <field dataField=\"WORKTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作性質\" showName=\"\"/>";
                    schema += "    <field dataField=\"VIEWTIMES\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"讀取次數\" showName=\"\"/>";
                    schema += "    <field dataField=\"WORKASSIGNMENT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作指派識別\" showName=\"\"/>";
                    schema += "    <field dataField=\"ASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作類型\" showName=\"\"/>";
                    schema += "    <field dataField=\"REASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"轉派類型\" showName=\"\"/>";
                    schema += "    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>";
                    schema += "    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                    schema += "    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>";
                    schema += "    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>";

                    schema += "  </fieldDefinition>";
                    schema += "  <identityField>";
                    schema += "    <field dataField=\"GUID\"/>";
                    schema += "  </identityField>";
                    schema += "  <keyField>";
                    schema += "    <field dataField=\"GUID\"/>";
                    schema += "  </keyField>";
                    schema += "  <allowEmptyField>";

                    schema += "    <field dataField=\"GUID\"/>";
                    schema += "    <field dataField=\"ATTACH\"/>";
                    schema += "    <field dataField=\"IMPORTANT\"/>";
                    schema += "    <field dataField=\"CURRENTSTATE\"/>";
                    schema += "    <field dataField=\"PROCESSNAME\"/>";
                    schema += "    <field dataField=\"SHEETNO\"/>";
                    schema += "    <field dataField=\"WORKITEMNAME\"/>";
                    schema += "    <field dataField=\"SUBJECT\"/>";
                    schema += "    <field dataField=\"USERNAME\"/>";
                    schema += "    <field dataField=\"CREATETIME\"/>";
                    schema += "    <field dataField=\"WORKTYPE\"/>";
                    schema += "    <field dataField=\"VIEWTIMES\"/>";
                    schema += "    <field dataField=\"WORKASSIGNMENT\"/>";
                    schema += "    <field dataField=\"ASSIGNMENTTYPE\"/>";
                    schema += "    <field dataField=\"REASSIGNMENTTYPE\"/>";
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

                    ListTable.IsGeneralUse = false;
                    ListTable.WidthMode = 1;
                    ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                    ListTable.InputForm = "Detail.aspx";
                    ListTable.CurPageUID = PageUniqueID;
                    ListTable.HiddenField = new string[] { "GUID", "CURRENTSTATE", "IMPORTANT", "ATTACH", "USERNAME", "WORKTYPE", "VIEWTIMES", "WORKITEMNAME", "WORKASSIGNMENT", "ASSIGNMENTTYPE", "REASSIGNMENTTYPE", "SHEETNO" };
                    ListTable.dataSource = dos;
                    string[] fieldOrder = (string[])getSession("fieldOrder");
                    ListTable.reOrderField(fieldOrder);
                    ListTable.updateTable();

                    setSession("CURLIST", dos);
                }


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

            //MessageBox("已取得清單");
        }
    }
    protected void FilterButton_Click(object sender, EventArgs e)
    {
        queryData();
    }
    protected DataObject ListTable_CustomDisplayTitle(DataObject objects)
    {
        /*
        Hashtable hs = (Hashtable)getSession("FieldName");

        IDictionaryEnumerator ie = hs.GetEnumerator();
        while (ie.MoveNext())
        {
            string tag = (string)ie.Key;
            string fname = (string)ie.Value;

            for (int i = 0; i < objects.dataField.Length; i++)
            {
                if (tag.Equals(objects.dataField[i]))
                {
                    objects.fieldDefinition[i, 3] = fname;
                }
            }
        }
        */
        return objects;
    }
    protected DSCWebControl.FlowStatusData ListTable_GetFlowStatusString(DataObject objects, bool isAddNew)
    {
        AbstractEngine engine = null;
        try
        {
            WorkItem wi = (WorkItem)objects.Tag;

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string sql = "";
            DataSet ds = null;

            //建立FlowStatusData物件
            FlowStatusData fd = new FlowStatusData();

            //取得資料物件代碼, 由原稿取得
            sql = "select SMWYAAA019, SMWYAAA018, SMWYAAA004, SMWYAAA002 from SMWYAAA where SMWYAAA005='" + Utility.filter(wi.processSerialNumber) + "'";
            ds = engine.getDataSet(sql, "TEMP");
            string objectGUID = "";

            fd.ACTID = "";
            fd.ACTName = Server.UrlEncode(wi.workItemName);
            fd.FlowGUID = wi.processSerialNumber;
            fd.HistoryGUID = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                fd.ObjectGUID = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                //無緣稿資料, 表示為發起參考流程的
                sql = "select CURGUID from FORMRELATION where FLOWGUID='" + Utility.filter(wi.processSerialNumber) + "'";
                DataSet dd3 = engine.getDataSet(sql, "TEMP");
                fd.ObjectGUID = dd3.Tables[0].Rows[0][0].ToString();
            }
            fd.PDID = wi.processId;
            fd.PDVer = "";

            fd.WorkItemOID = wi.workItemOID;
            fd.workAssignmentOID = wi.workAssignmentOID;
            fd.assignmentType = objects.getData("ASSIGNMENTTYPE");
            fd.reassignmentType = objects.getData("REASSIGNMENTTYPE");
            fd.manualReassignType = wi.manualReassignType;

            if (ds.Tables[0].Rows.Count > 0)
            {
                ListTable.FormTitle = ds.Tables[0].Rows[0]["SMWYAAA004"].ToString() + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " List", "(單號:") + ds.Tables[0].Rows[0]["SMWYAAA002"].ToString() + ")";
            }
            else
            {
                ListTable.FormTitle = wi.processName + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " List", "(單號:") + objects.getData("SHEETNO") + ")";
            }
            engine.close();
            return fd;
        }
        catch (Exception ue)
        {
            //建立FlowStatusData物件
            FlowStatusData fd = new FlowStatusData();

            DataRow dr = (DataRow)objects.Tag;
            ListTable.FormTitle = "\t" + dr["resca002"].ToString() + "(" + dr["resdd002"].ToString() + ")";
            fd.PDID = dr["resdd001"].ToString();
            fd.ObjectGUID = "";
            fd.FlowGUID = dr["resdd002"].ToString();
            fd.WorkItemOID = "";//dr["resdd003"].ToString();
            fd.TargetWorkItemOID = "";//dr["resdd004"].ToString();
            fd.workAssignmentOID = "";//dr["resdd005"].ToString();
            fd.assignmentType = "64";//拿來存放CALL EF 的FormStatus，草稿為64，固定帶即可
            //base.showModalDialog("http://www.google.com.tw", "", "", "", "", "", "", "", "", "", "", "", "");
            //base.showOpenWindow("http://www.google.com.tw", "", "", "", "", "", "", "", "", "", "", "", "", "", "", false);
            return fd;

            //try
            //{
            //    engine.close();
            //}
            //catch { };
            //return null;
        }
    }
    protected void ListTable_RefreshButtonClick()
    {
        queryData();
    }

    protected bool[] ListTable_setEnhancedRow(DataObject[] currentPageObjects)
    {
        bool[] retv = new bool[currentPageObjects.Length];
        for (int i = 0; i < currentPageObjects.Length; i++)
        {
            if (currentPageObjects[i].getData("VIEWTIMES").Equals("0"))
            {
                retv[i] = true;
            }
            else
            {
                retv[i] = false;
            }
        }
        return retv;
    }
    private WorkItem[] mergeWorkItem(WorkItem[] wiOriginal, ArrayList wiNew)
    {
        StringCollection subsWorkOIDList = (StringCollection)getSession("SUBSLIST");
        Array.Resize(ref wiOriginal, wiOriginal.Length + wiNew.Count);
        for (int i = 0; i < wiNew.Count; i++)
        {
            wiOriginal[wiOriginal.Length - wiNew.Count + i] = (WorkItem)wiNew[i];
            subsWorkOIDList.Add(((WorkItem)wiNew[i]).workItemOID);
        }
        setSession("SUBSLIST", subsWorkOIDList);
        return wiOriginal;
    }

    //Amos
    private DataSet getSP8FormInBox(AbstractEngine engine, string BoxID)
    {
        string userid = mappingUserID(engine);
        //SysParam sp = new SysParam(engine);
        //string SP8str = sp.getParam("EF2KWebDB");
        string SP8str = GetINDUSConnSTR("EF2KWebDB", engine);//EF DB connection String

        IOFactory factory = new IOFactory();
        AbstractEngine SP8engine = factory.getEngine(EngineConstants.SQL, SP8str);

        string strSQL = "";

        switch (BoxID)
        {
            case "InBox":
                #region 收件夾
                strSQL += "SELECT DISTINCT resdd.resdd001, resca.resca002, resdd.resdd002, resdd.resdd003, resdd.resdd004, resdd.resdd005, ";
                strSQL += "resdd.resdd006, resdd.resdd008, resdd.resdd009, resdd.resdd014, resdd.resdd013, resdd.resdd015, resda.resda031, ";
                strSQL += "resda.resda032, resdb.resdb027, resde.resde002, resak1.resak002 AS resda_016, resak2.resak002 AS resda_017 ";
                strSQL += "FROM resdd ";
                strSQL += "LEFT OUTER JOIN resde ON resdd.resdd001 = resde.resde001 AND resdd.resdd002 = resde.resde002 ";
                strSQL += "LEFT OUTER JOIN resda ON resdd.resdd001 = resda.resda001 AND resdd.resdd002 = resda.resda002 ";
                strSQL += "LEFT OUTER JOIN resak AS resak1 ON resda.resda016 = resak1.resak001 ";
                strSQL += "LEFT OUTER JOIN resak AS resak2 ON resda.resda017 = resak2.resak001 ";
                strSQL += "LEFT OUTER JOIN resdb ON resdd.resdd001 = resdb.resdb001 AND resdd.resdd002 = resdb.resdb002 AND resdb.resdb003=resdd.resdd003 AND resdb.resdb004=resdd.resdd004 ";
                strSQL += "LEFT OUTER JOIN resca ON resdd.resdd001 = resca.resca001 ";
                strSQL += "WHERE (resdd.resdd007 = N'" + userid + "' OR resdd.resdd020 = N'" + userid + "') ";
                strSQL += "AND (resdd.resdd019 = N'Y') AND (resdd.resdd003 <> N'0000') ";
                strSQL += "AND (resdd.resdd015 IN (N'1',N'5'))";
                break;
                #endregion 收件夾
            case "Rollback":
                #region 重辦
                strSQL += "SELECT DISTINCT resdd.resdd001, resca.resca002, resdd.resdd002, resdd.resdd003, resdd.resdd004, ";
                strSQL += "resdd.resdd005, resdd.resdd006, resdd.resdd008, resdd.resdd009, resdd.resdd014, resdd.resdd013, ";
                strSQL += "resdd.resdd015, resda.resda031, resda.resda032, resdb.resdb027, resde.resde002, ";
                strSQL += "resak1.resak002 AS resda_016, resak2.resak002 AS resda_017 ";
                strSQL += "FROM resdd ";
                strSQL += "LEFT OUTER JOIN resde ON resdd.resdd001 = resde.resde001 AND resdd.resdd002 = resde.resde002 ";
                strSQL += "LEFT OUTER JOIN resda ON resdd.resdd001 = resda.resda001 AND resdd.resdd002 = resda.resda002 ";
                strSQL += "LEFT OUTER JOIN resak AS resak1 ON resda.resda016 = resak1.resak001 ";
                strSQL += "LEFT OUTER JOIN resak AS resak2 ON resda.resda017 = resak2.resak001 ";
                strSQL += "LEFT OUTER JOIN resdb ON resdd.resdd001 = resdb.resdb001 AND resdd.resdd002 = resdb.resdb002 AND resdb.resdb003=resdd.resdd003 AND resdb.resdb004=resdd.resdd004 ";
                strSQL += "LEFT OUTER JOIN resca ON resdd.resdd001 = resca.resca001 ";
                strSQL += "WHERE (resdd.resdd007 = N'" + userid + "') AND (resdd.resdd019 = N'Y') AND (resdd.resdd003='0000') ";
                strSQL += "AND (resdd.resdd015=1)";
                break;
                #endregion 重辦
            case "Fowarded":
                #region 轉寄
                strSQL += "SELECT DISTINCT resdh.resdh001, resca.resca002, resdh.resdh002, resdh.resdh003, resdh.resdh004, ";
                strSQL += "resdh.resdh005, resdh.resdh006, resda.resda020, resda.resda021, resda.resda031, resda.resda032, ";
                strSQL += "resde.resde002, resak.resak002 AS resda_017 ";
                strSQL += "FROM resdh ";
                strSQL += "LEFT OUTER JOIN resde ON resdh.resdh002 = resde.resde001 AND resdh.resdh003 = resde.resde002 ";
                strSQL += "LEFT OUTER JOIN resda ON resdh.resdh002 = resda.resda001 AND resdh.resdh003 = resda.resda002 ";
                strSQL += "LEFT OUTER JOIN resak ON resdh.resdh006 = resak.resak001 ";
                strSQL += "LEFT OUTER JOIN resca ON resdh.resdh002 = resca.resca001 ";
                strSQL += "LEFT OUTER JOIN resdd ON resdh.resdh002 = resdd.resdd001 AND resdh.resdh003 = resdd.resdd002 ";
                strSQL += "WHERE (resdh.resdh001 = N'" + userid + "')";
                break;
                #endregion 轉寄
            case "Info":
                #region 通知
                strSQL += "SELECT DISTINCT resdd.resdd001, resca.resca002, resdd.resdd002, resdd.resdd003, resdd.resdd004, ";
                strSQL += "resdd.resdd005, resdd.resdd006, resdd.resdd008, resdd.resdd009, resdd.resdd014, resdd.resdd013, ";
                strSQL += "resdd.resdd015, resda.resda031, resda.resda032, resde.resde002 ";
                strSQL += "FROM resdd ";
                strSQL += "LEFT OUTER JOIN resde ON resdd.resdd001 = resde.resde001 AND resdd.resdd002 = resde.resde002 ";
                strSQL += "LEFT OUTER JOIN resda ON resdd.resdd001 = resda.resda001 AND resdd.resdd002 = resda.resda002 ";
                strSQL += "LEFT OUTER JOIN resca ON resdd.resdd001 = resca.resca001 ";
                strSQL += "WHERE (resdd.resdd007 = N'" + userid + "' OR resdd.resdd020 = N'" + userid + "') ";
                strSQL += "AND (resdd.resdd019 = N'Y') AND (resdd.resdd018 = 16)";
                break;
                #endregion 通知
            case "StepNote":
                #region 逐級通知
                strSQL += "SELECT DISTINCT resdf.resdf001, resca.resca002, resdf.resdf002, resdf.resdf003, resdf.resdf004, ";
                strSQL += "resdf.resdf005, resdf.resdf006, resdf.resdf007, resdf.resdf008, resdf.resdf009, ";
                strSQL += "CAST(resdf.resdf010 AS nvarchar(255)) AS resdf010, resdf.resdf011, resda.resda020, ";
                strSQL += "resda.resda021, resda.resda031, resda.resda032, resde.resde002, resak.resak002 AS resda_017, resdd.resdd001, ";
                strSQL += "resdf.resdf003 AS resdd002, resdf.resdf009 AS resdd009, resda.resda035 AS resdd013 ";
                strSQL += "FROM resdf ";
                strSQL += "LEFT OUTER JOIN resde ON resdf.resdf002 = resde.resde001 AND resdf.resdf003 = resde.resde002 ";
                strSQL += "LEFT OUTER JOIN resda ON resdf.resdf002 = resda.resda001 AND resdf.resdf003 = resda.resda002 ";
                strSQL += "LEFT OUTER JOIN resak ON resda.resda016 = resak.resak001 ";
                strSQL += "LEFT OUTER JOIN resca ON resdf.resdf002 = resca.resca001 ";
                strSQL += "LEFT OUTER JOIN resdd ON resdf.resdf002 = resdd.resdd001 AND resdf.resdf003 = resdd.resdd002 ";
                strSQL += "WHERE (resdf.resdf001 = N'" + userid + "')";
                break;
                #endregion 逐級通知
            case "Sent":
                #region 原稿
                strSQL += "SELECT DISTINCT resda.resda001, resda.resda002, resda.resda015, resda.resda016, resda.resda018, ";
                strSQL += "resda.resda019, resda.resda020, resda.resda021, resca.resca002, resda.resda031, resda.resda032, ";
                strSQL += "resda.resda033, resda040, resde.resde002 ";
                strSQL += "FROM resda ";
                strSQL += "LEFT OUTER JOIN resde ON resda.resda001 = resde.resde001 AND resda.resda002 = resde.resde002 ";
                strSQL += "LEFT OUTER JOIN resca ON resda.resda001 = resca.resca001 ";
                strSQL += "LEFT OUTER JOIN resdd ON resda.resda001 = resdd.resdd001 AND resda.resda002 = resdd.resdd002 ";
                strSQL += "WHERE (resda.resda016 = N'" + userid + "') AND (resda.resda033 = N'Y')";
                break;
                #endregion 原稿
            case "Reply":
                #region 回函
                strSQL += "SELECT DISTINCT resda.resda001, resda.resda002, resda.resda015, resda.resda016, resda.resda018, ";
                strSQL += "resda.resda019, resda.resda020, resda.resda021, resca.resca002, resda.resda031, resda.resda032, ";
                strSQL += "resda.resda033, resda.resda035, resda.resda041, resde.resde002, resdd.resdd001, resdd.resdd002, ";
                strSQL += "resak2.resak002 AS resda_017, resda.resda018 AS resdd009, resda.resda035 AS resdd013 ";
                strSQL += "FROM resda ";
                strSQL += "LEFT OUTER JOIN resde ON resda.resda001 = resde.resde001 AND resda.resda002 = resde.resde002 ";
                strSQL += "LEFT OUTER JOIN resca ON resda.resda001 = resca.resca001 ";
                strSQL += "LEFT OUTER JOIN resdd ON resda.resda001 = resdd.resdd001 AND resda.resda002 = resdd.resdd002 ";
                strSQL += "LEFT OUTER JOIN resak AS resak2 ON resda.resda017 = resak2.resak001 ";
                strSQL += "WHERE (resda.resda016 = N'" + userid + "') AND (resda.resda034 = N'Y')";
                break;
                #endregion 回函
            case "Load":
                #region 草稿
                strSQL += "select * from (SELECT resca.resca002, ressa.strFormID, ressa.ScriptSheetNo, ressa.FieldValue, ressa.Owner, ";
                strSQL += "ressa.strFormID AS resdd001,ressa.ScriptSheetNo as resdd002, ressa.ScriptSheetNo AS resdd009,'' as resda032,'' as resde002, ressa.FieldValue as resda031 ";
                strSQL += "FROM ressa ";
                strSQL += "LEFT OUTER JOIN resca ON ressa.strFormID = resca.resca001 ";
                strSQL += "WHERE (ressa.Owner = N'" + userid + "' AND ressa.RecordsetName = N'resda') ";
                strSQL += "AND (ressa.FieldName = N'ScriptSubj') and resca086='2' and resca084='1') a where (1=1) ";
                break;
                #endregion 草稿
        }

        if (!StartTime.ValueText.Equals(""))
        {
            strSQL += " AND left(resdd009,10)>='" + StartTime.ValueText + "' ";
        }
        if (!EndTime.ValueText.Equals(""))
        {
            strSQL += " AND left(resdd009,10)<='" + EndTime.ValueText + "' ";
        }
        if (!ProcessIDList.ValueText.Equals(""))
        {
            strSQL += " AND resdd001='" + ProcessIDList.ValueText + "' ";
        }
        if (!Subject.ValueText.Equals(""))
        {
            strSQL += " AND resda031 like '%" + Subject.ValueText + "%' ";
        }
        strSQL += " ORDER BY resdd009 DESC";

        DataSet ds = SP8engine.getDataSet(strSQL, "QOO");

        SP8engine.close();
        return ds;
    }

    //Amos
    private string mappingUserID(AbstractEngine engine)
    {
        return (string)Session["UserID"];
    }
    protected void ListTable_ShowRowData(DataObject objects)
    {
        //defre
    }
    protected void ListTable_ShowRowData1(DataObject objects)
    {
        string sf = "";
        //defre
    }
    protected void ListTable_DeleteData()
    {
        string BoxID = (string)getSession("BoxID");
        string userID = (string)Session["UserID"];
        AbstractEngine engine = null;
        AbstractEngine SP8engine = null;
        try
        {
            if (BoxID.Equals("Load"))
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);

                string SP8str = GetINDUSConnSTR("EF2KWebDB", engine);//EF DB connection String
                SP8engine = factory.getEngine(EngineConstants.SQL, SP8str);

                string sql = "";
                DataObject[] ChkedArr = (DataObject[])getSession("ChkedArr");
                for (int i = 0; i < ChkedArr.Length; i++)
                {
                    string GUID = ChkedArr[i].getData("GUID");
                    string strFormID = GUID.Split('-')[0];
                    string ScriptSheetNo = GUID.Split('-')[1];
                    sql = "Delete From ressa where strFormID=N'" + strFormID + "' and ScriptSheetNo=N'" + ScriptSheetNo + "' and Owner=N'" + userID + "'";
                    SP8engine.executeSQL(sql);
                }
                engine.close();
                SP8engine.close();
                //Delete From ressa where strFormID=N'PER011' and ScriptSheetNo=N'2012/10/15 16:43:00' and Owner=N'770894'
            }
        }
        catch (Exception ex)
        {
            try
            {
                if (engine != null) engine.close();
                if (SP8engine != null) SP8engine.close();
            }
            catch { }
            writeLog(ex);
        }
        setSession("ChkedArr", null);

    }
    protected bool ListTable_BeforeDeleteData()
    {
        string BoxID = (string)getSession("BoxID");
        if (BoxID.Equals("Load"))
        {
            DataObject[] ChkedArr = ListTable.getSelectedItem();
            setSession("ChkedArr", ChkedArr);
        }
        return true;
    }
    private void changePage(int pageNum)
    {
        setSession("CURRENTPAGE", pageNum);
        CurrentPage.ValueText = ((int)getSession("CURRENTPAGE")).ToString();
        try
        {
            //Amos
            //取得EasyFlow的待簽核資料
            ArrayList arys = (ArrayList)getSession("init_DATA");
            if (true)
            {

                DataObjectSet dos = ListTable.dataSource;
                dos.clear();

                int start = ListTable.getPageSize() * (pageNum - 1);
                int end = start + ListTable.getPageSize();
                if (end > arys.Count) end = arys.Count;

                //Amos
                //處理SP8
                for (int i = start; i < end; i++)
                {
                    DataRow ddr = (DataRow)arys[i];

                    DataObject ddo = dos.create();
                    ddo.setData("GUID", ddr["resdd001"].ToString() + "-" + ddr["resdd002"].ToString());
                    ddo.setData("CURRENTSTATE", "0");
                    ddo.setData("PROCESSNAME", "\t" + ddr["resca002"].ToString());
                    ddo.setData("SHEETNO", ddr["resdd002"].ToString());
                    ddo.setData("IMPORTANT", "");
                    ddo.setData("ATTACH", "");
                    ddo.setData("WORKITEMNAME", "");
                    ddo.setData("SUBJECT", ddr["resda031"].ToString());
                    ddo.setData("USERNAME", "");
                    ddo.setData("CREATETIME", ddr["resdd009"].ToString());
                    ddo.setData("WORKTYPE", "");
                    ddo.setData("ASSIGNMENTTYPE", "");
                    ddo.setData("REASSIGNMENTTYPE", "");
                    ddo.setData("VIEWTIMES", "");
                    ddo.setData("WORKASSIGNMENT", "");
                    ddo.setData("D_INSERTUSER", "SYSTEM");
                    ddo.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                    ddo.setData("D_MODIFYUSER", "");
                    ddo.setData("D_MODIFYTIME", "");
                    ddo.Tag = ddr;
                    dos.add(ddo);

                }

                ListTable.IsGeneralUse = false;
                ListTable.InputForm = "Detail.aspx";
                ListTable.CurPageUID = PageUniqueID;
                ArrayList hFields = (ArrayList)getSession("HiddenFields");
                string[] hf = new string[hFields.Count + 12];
                hf[0] = "GUID";
                hf[1] = "WORKASSIGNMENT";
                hf[2] = "ASSIGNMENTTYPE";
                hf[3] = "REASSIGNMENTTYPE";
                hf[4] = "CURRENTSTATE";
                hf[5] = "IMPORTANT";
                hf[6] = "ATTACH";
                hf[7] = "USERNAME";
                hf[8] = "WORKTYPE";
                hf[9] = "VIEWTIMES";
                hf[10] = "WORKITEMNAME";
                hf[11] = "SHEETNO";

                for (int i = 0; i < hFields.Count; i++)
                {
                    hf[i + 4] = (string)hFields[i];
                }
                ListTable.HiddenField = hf;
                ListTable.WidthMode = 1;
                ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                ListTable.dataSource = dos;
                string[] fieldOrder = (string[])getSession("fieldOrder");
                ListTable.reOrderField(fieldOrder);
                ListTable.updateTable();



                setSession("CURLIST", dos);
            }
            else
            {
                string qstr = "select GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME from SMWL";


                DataObjectSet dos = new DataObjectSet();
                string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">";
                schema += "<queryStr>" + qstr + "</queryStr>";
                schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
                schema += "  <fieldDefinition>";

                schema += "    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>";
                schema += "    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>";
                schema += "    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"重要性\" showName=\"\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>";
                schema += "    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程名稱\" showName=\"\"/>";
                schema += "    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>";
                schema += "    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
                schema += "    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人\" showName=\"\"/>";
                schema += "    <field dataField=\"CREATETIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作性質\" showName=\"\"/>";
                schema += "    <field dataField=\"VIEWTIMES\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"讀取次數\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKASSIGNMENT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作指派識別\" showName=\"\"/>";
                schema += "    <field dataField=\"ASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作類型\" showName=\"\"/>";
                schema += "    <field dataField=\"REASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"轉派類型\" showName=\"\"/>";
                schema += "    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>";
                schema += "    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                schema += "    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>";
                schema += "    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>";

                schema += "  </fieldDefinition>";
                schema += "  <identityField>";
                schema += "    <field dataField=\"GUID\"/>";
                schema += "  </identityField>";
                schema += "  <keyField>";
                schema += "    <field dataField=\"GUID\"/>";
                schema += "  </keyField>";
                schema += "  <allowEmptyField>";

                schema += "    <field dataField=\"GUID\"/>";
                schema += "    <field dataField=\"ATTACH\"/>";
                schema += "    <field dataField=\"IMPORTANT\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\"/>";
                schema += "    <field dataField=\"PROCESSNAME\"/>";
                schema += "    <field dataField=\"SHEETNO\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\"/>";
                schema += "    <field dataField=\"SUBJECT\"/>";
                schema += "    <field dataField=\"USERNAME\"/>";
                schema += "    <field dataField=\"CREATETIME\"/>";
                schema += "    <field dataField=\"WORKTYPE\"/>";
                schema += "    <field dataField=\"VIEWTIMES\"/>";
                schema += "    <field dataField=\"WORKASSIGNMENT\"/>";
                schema += "    <field dataField=\"ASSIGNMENTTYPE\"/>";
                schema += "    <field dataField=\"REASSIGNMENTTYPE\"/>";
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

                ListTable.IsGeneralUse = false;
                ListTable.WidthMode = 1;
                ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                ListTable.InputForm = "Detail.aspx";
                ListTable.CurPageUID = PageUniqueID;
                ListTable.HiddenField = new string[] { "GUID", "CURRENTSTATE", "IMPORTANT", "ATTACH", "USERNAME", "WORKTYPE", "VIEWTIMES", "WORKITEMNAME", "WORKASSIGNMENT", "ASSIGNMENTTYPE", "REASSIGNMENTTYPE", "SHEETNO" };
                ListTable.dataSource = dos;
                string[] fieldOrder = (string[])getSession("fieldOrder");
                ListTable.reOrderField(fieldOrder);
                ListTable.updateTable();

                setSession("CURLIST", dos);
            }

        }
        catch (Exception te)
        {
            MessageBox(te.Message);
            writeLog(te);
        }

        //MessageBox("已取得清單");

    }
    protected bool ListTable_NextPageClick()
    {
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
        ListTable.isShowAll=false;
        ListTable.dataSource.setPageSize(pagesize);
        int tp = (int)com.dsc.kernal.utility.Utility.Round((decimal)(ary.Count / pagesize), 0);
        if (tp * pagesize < ary.Count)
        {
            tp++;
        }
        setSession("TOTALPAGES", tp);
        TotalPages.Text = "/共" + tp.ToString() + "頁";
        setSession("CURRENTPAGE", 1);
        changePage(1);
    }
    public string GetINDUSConnSTR(string strConn, AbstractEngine engine)
    {

        SysParam sp = new SysParam(engine);
        string connectString1 = sp.getParam(strConn);

        return connectString1;
    }
}
