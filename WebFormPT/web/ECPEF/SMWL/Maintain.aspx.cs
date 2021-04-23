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
//using WebServerProject.flow.SMWY;
//using WebServerProject.flow.SMWG;
using WebServerProject;
using DSCWebControl;
using System.Collections.Specialized;
using System.Text;

public partial class ECPEF_SMWL_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SMWL";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";

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
                string FromMail ="";
                if (Request.QueryString["FromMail"] != null)
                    FromMail = Request.QueryString["FromMail"];
                setSession("BoxID", BoxID);
                setSession("FromMail", FromMail);
                Session["QQPUID"] = PageUniqueID; //儲存PageUniqueID 以供開啟表單使用
                Session["QQCurPanelID"] = CurPanelID;

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                //EndTime.ValueText = DateTimeUtility.getSystemTime2(null);
                //StartTime.ValueText = DateTime.Parse(DateTimeUtility.getSystemTime2(null).ToString()).AddMonths(-3).ToString();

                string sql = "select * from SMWIAAA where SMWIAAA002='" + Utility.filter(BoxID) + "'";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    engine.close();
                    Response.Redirect("NoSetting.aspx");
                }

                string[,] ids;

                if (ds.Tables[0].Rows[0]["SMWIAAA007"].ToString().Equals("W"))
                {
                    //待辦事項
                    setSession("BoxQueryType", "W");

                    ids = new string[,]{
                        {"",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids1", "不限定")},
                        {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids2", "正常指派")},
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids3", "他人重新指派(退回重辦)")},
                        {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids4", "抽回重辦工作")}
                    };
                    AssignType.setListItem(ids);


                    sql = "select SMWHAAB005, SMWHAAB004, SMWHAAB003 from SMWHAAB where SMWHAAB002='" + Utility.filter(ds.Tables[0].Rows[0]["SMWIAAA008"].ToString()) + "'";
                    DataSet ds2 = engine.getDataSet(sql, "TEMP");
                    ids = new string[ds2.Tables[0].Rows.Count, 2];
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        ids[i, 0] = ds2.Tables[0].Rows[i][0].ToString() + ":::" + ds2.Tables[0].Rows[i][2].ToString();
                        ids[i, 1] = ds2.Tables[0].Rows[i][1].ToString();
                    }
                    signResult.setListItem(ids);

                    if (ds.Tables[0].Rows[0]["SMWIAAA006"].ToString().Equals("N"))
                    {
                        GroupSignBox.Display = false;
                    }
                    else
                    {
                        GroupSignBox.Display = true;
                    }
                }
                else
                {
                    //通知
                    setSession("BoxQueryType", "N");

                    //通知資料夾時預設查詢起迄為三個月
                    EndTime.ValueText = DateTimeUtility.getSystemTime2(null);
                    StartTime.ValueText = DateTime.Parse(DateTimeUtility.getSystemTime2(null).ToString()).AddMonths(-3).ToString();

                    ids = new string[,]{
                        {"",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids1", "不限定")},
                        {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids5", "系統自動通知")},
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids6", "私人轉寄")},
                        //{"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids7", "一般轉寄, 顯示在流程追蹤")},
                        //{"3",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids8", "被代理通知")},
                        {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids9", "流程被撤銷或工作被回收")}
                    };
                    AssignType.setListItem(ids);
                    GroupSignBox.Display = false;
                }
                //得取次數過濾
                ids = new string[,]{
                    {"A",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids10", "全部")},
                    {"U",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids11", "未讀取")},
                    {"R",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids12", "已讀取")}
                };
                ViewTimes.setListItem(ids);

                //取得顯示欄位
                ArrayList hFields = new ArrayList();
                if (ds.Tables[0].Rows[0]["CURRENTSTATE"].ToString().Equals("N"))
                {
                    hFields.Add("CURRENTSTATE");
                }
                if (ds.Tables[0].Rows[0]["PROCESSNAME"].ToString().Equals("N"))
                {
                    hFields.Add("PROCESSNAME");
                }
                if (ds.Tables[0].Rows[0]["SHEETNO"].ToString().Equals("N"))
                {
                    hFields.Add("SHEETNO");
                }
                if (ds.Tables[0].Rows[0]["WORKITEMNAME"].ToString().Equals("N"))
                {
                    hFields.Add("WORKITEMNAME");
                }
                if (ds.Tables[0].Rows[0]["SUBJECT"].ToString().Equals("N"))
                {
                    hFields.Add("SUBJECT");
                }
                if (ds.Tables[0].Rows[0]["USERNAME"].ToString().Equals("N"))
                {
                    hFields.Add("USERNAME");
                }
                if (ds.Tables[0].Rows[0]["CREATETIME"].ToString().Equals("N"))
                {
                    hFields.Add("CREATETIME");
                }
                if (ds.Tables[0].Rows[0]["VIEWTIMES"].ToString().Equals("N"))
                {
                    hFields.Add("VIEWTIMES");
                }
                if (ds.Tables[0].Rows[0]["IMPORTANT"].ToString().Equals("N"))
                {
                    hFields.Add("IMPORTANT");
                }
                if (ds.Tables[0].Rows[0]["ATTACH"].ToString().Equals("N"))
                {
                    hFields.Add("ATTACH");
                }

                setSession("HiddenFields", hFields);

                //取得流程定義條件
                string processIDS = "";
                if (ds.Tables[0].Rows[0]["SMWIAAA004"].ToString().Equals("0"))
                {
                    sql = "select SMWBAAA003 from SMWBAAA where SMWBAAA003 not in (select SMWIAAB003 from SMWIAAB where SMWIAAB002='" + Utility.filter(ds.Tables[0].Rows[0]["SMWIAAA001"].ToString()) + "')";
                }
                else
                {
                    sql = "select SMWIAAB003 from SMWIAAB where SMWIAAB002='" + Utility.filter(ds.Tables[0].Rows[0]["SMWIAAA001"].ToString()) + "'";
                }
                DataSet temp = engine.getDataSet(sql, "TEMP");
                for (int i = 0; i < temp.Tables[0].Rows.Count; i++)
                {
                    processIDS += temp.Tables[0].Rows[i][0].ToString() + ";";
                }
                if (processIDS.Length > 0)
                {
                    processIDS = processIDS.Substring(0, processIDS.Length - 1);
                }
                setSession("ProcessID", processIDS); //可取得流程
                //設定可取得流程
                string[] lists = processIDS.Split(new char[] { ';' });
                string ztag = "'*'";
                for (int i = 0; i < lists.Length; i++)
                {
                    ztag += ",'" + Utility.filter(lists[i]) + "'";
                }
                sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA where SMWBAAA003 in (" + ztag + ")";
                DataSet ddd = engine.getDataSet(sql, "TEMP");


                //Amos
                string SP8str = GetINDUSConnSTR("EF2KWebDB", engine);//EF DB connection String
                AbstractEngine sp8engine = factory.getEngine(EngineConstants.SQL, SP8str);
                DataSet sp8ds = sp8engine.getDataSet("select resca001, resca002 from resca where resca086='2' and resca001<>'PER013' and resca001<>'PER006OVT'", "TEMP");
                sp8engine.close();

                ids = new string[ddd.Tables[0].Rows.Count + 1 + sp8ds.Tables[0].Rows.Count, 2];
                ids[0, 0] = "";
                ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " idsA", "不限定");
                for (int i = 0; i < ddd.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1, 0] = ddd.Tables[0].Rows[i][0].ToString();
                    ids[i + 1, 1] = ddd.Tables[0].Rows[i][1].ToString();
                }
                for (int i = 0; i < sp8ds.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1 + ddd.Tables[0].Rows.Count, 0] = sp8ds.Tables[0].Rows[i][0].ToString();
                    ids[i + 1 + ddd.Tables[0].Rows.Count, 1] = "(EF)" + sp8ds.Tables[0].Rows[i][1].ToString() + "(" + sp8ds.Tables[0].Rows[i][0].ToString() + ")";
                }
                /*
                ids = new string[ddd.Tables[0].Rows.Count + 1, 2];
                ids[0, 0] = "";
                ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " idsA", "不限定");
                for (int i = 0; i < ddd.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1, 0] = ddd.Tables[0].Rows[i][0].ToString();
                    ids[i + 1, 1] = ddd.Tables[0].Rows[i][1].ToString();
                }*/
                ProcessIDList.setListItem(ids);

                //設定不可取得流程
                sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA where SMWBAAA003 not in (" + ztag + ")";
                ddd = engine.getDataSet(sql, "TEMP");
                string denyProcessID = "";
                for (int i = 0; i < ddd.Tables[0].Rows.Count; i++)
                {
                    denyProcessID += ddd.Tables[0].Rows[i][0].ToString() + ";";
                }
                if (denyProcessID.Length > 0)
                {
                    denyProcessID = denyProcessID.Substring(0, denyProcessID.Length - 1);
                }
                setSession("denyProcessID", denyProcessID);

                //取得不可流程角色（工作名稱)
                string denyWorkItems = "";
                if (ds.Tables[0].Rows[0]["SMWIAAA005"].ToString().Equals("1"))
                {
                    sql = "select SMWCAAA002 from SMWCAAA where SMWCAAA002 not in (select SMWIAAC003 from SMWIAAC where SMWIAAC002='" + Utility.filter(ds.Tables[0].Rows[0]["SMWIAAA001"].ToString()) + "')";
                }
                else
                {
                    sql = "select SMWIAAC003 from SMWIAAC where SMWIAAC002='" + Utility.filter(ds.Tables[0].Rows[0]["SMWIAAA001"].ToString()) + "'";
                }
                temp = engine.getDataSet(sql, "TEMP");
                for (int i = 0; i < temp.Tables[0].Rows.Count; i++)
                {
                    denyWorkItems += temp.Tables[0].Rows[i][0].ToString() + ";";
                }
                if (denyWorkItems.Length > 0)
                {
                    denyWorkItems = denyWorkItems.Substring(0, denyWorkItems.Length - 1);
                }
                setSession("denyWorkItems", denyWorkItems);

                //取得欄位顯示順序
                string[] fieldOrder = ds.Tables[0].Rows[0]["FIELDORDER"].ToString().Split(new char[] { ';' });
                ListTable.reOrderField(fieldOrder);
                setSession("fieldOrder", fieldOrder);

                engine.close();
		if (getSession("FromMail").ToString().Equals("1"))
		{
                	//queryData(true);

		}	
		else
		{
			queryData(false);
		}
		setSession("FromMail", "");
            }
        }
    }

    private void queryData(bool isFetchFewItems)
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
                int iIdx = con1.IndexOf("//") + 2;

                FlowFactory ff = new FlowFactory();
                AbstractFlowAdapter adp = ff.getAdapter(flowType);
                adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
                adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

                string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
                fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

                adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

                WorkItem[] wi = null;
                string strEndTime = EndTime.ValueText;
                if (!strEndTime.Equals(""))
                    strEndTime += " 23:59:59";

                string BoxQueryType = (string)getSession("BoxQueryType");
                if (BoxQueryType.Equals("W"))
                {
                    if (isDB.Equals("N"))
                    {
                        if (isFetchFewItems)
                        {
                            wi = adp.fetchPerformableWorkItems((string)Session["UserID"], "1", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, ProcessIDList.ValueText);
                        }
                        else
                        {
                            wi = adp.fetchPerformableWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, ProcessIDList.ValueText);
                        }
                    }
                    else
                    {
                        if (isFetchFewItems)
                        {
                            wi = adp.fetchPerformableWorkItems((string)Session["UserID"], "1", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, ProcessIDList.ValueText, engine);
                        }
                        else
                        {
                            //MessageBox("edward");
                            wi = adp.fetchPerformableWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, ProcessIDList.ValueText, engine);
                            //wi = adp.fetchPerformableWorkItems((string)Session["UserID"], "10", "1", "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, ProcessIDList.ValueText, engine);
                        }

                    }
                    //代理人處理機制
                    //if(false)
                    if (isSubstitute.Equals("Y"))
                    {
                        //Amos
                        SysParam sp = new SysParam(engine);
                        double WaitTime = Convert.ToDouble(sp.getParam("WaitTime")) * 60 * 60; //以秒來計算

                        setSession("SUBSLIST", new StringCollection());
                        string CurrUserId = (string)Session["UserID"];
                        string CurrUserName = (string)Session["UserName"];


                        //string strSQL3 = " SELECT UserID,workItemOID,processDefinitionId , processInstanceName  ,createdTime, subject, workItemName, requesterName, serialNumber FROM PerformableWorkItemList PFW";
                        //strSQL3 += " LEFT JOIN ECPFlowNoticeLog LOG1 ON PFW.workItemOID = LOG1.EFNL002 and LOG1.EFNL005 = '1' ";
                        //strSQL3 += " LEFT JOIN ECPFlowNoticeLog LOG2 ON PFW.workItemOID = LOG2.EFNL002 and LOG2.EFNL005 = '2' ";
                        //strSQL3 += " WHERE ((UserID='" + CurrUserId + "') ";
                        //strSQL3 += " or (UserID<>'" + CurrUserId + "' and ISNULL(LOG2.EFNL003,LOG1.EFNL003)='" + CurrUserId + "')) AND isNotice='0'";
                        //DataSet dsPWD = engine.getDataSet(strSQL3, "TEMP");
                        //if (dsPWD.Tables[0].Rows.Count > 0)
                        //{
                        //    for (int i = 0; i < dsPWD.Tables[0].Rows.Count; i++)
                        //    {
                        //        WorkItem[] wiNew = null;
                        //        if (isDB.Equals("N"))
                        //        {
                        //            wiNew = adp.fetchPerformableWorkItems(dsPWD.Tables[0].Rows[i]["UserID"].ToString(), "1000000", "1", dsPWD.Tables[0].Rows[i]["workItemOID"].ToString(), Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, "");
                        //        }
                        //        else
                        //        {
                        //            wiNew = adp.fetchPerformableWorkItems(dsPWD.Tables[0].Rows[i]["UserID"].ToString(), "1000000", "1", dsPWD.Tables[0].Rows[i]["workItemOID"].ToString(), Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, "", engine);
                        //        }
                        //        ArrayList arrayWI = new ArrayList();
                        //        for (int j = 0; j < wiNew.Length; j++)
                        //        {
                        //            WorkItem wii = wiNew[j];
                        //            if (!arrayWI.Contains(wii))
                        //            {
                        //                arrayWI.Add(wii);
                        //            }
                        //        }
                        //        wi = mergeWorkItem(wi, arrayWI);
                        //    }
                        //}

                        //找出目前使用者所代理的人
                        //加入取得FrmSignAgt所設定的代理人
                        //string sSQL = "SELECT oriSignUserID FROM FrmSignAgt WHERE SignAgent='" + CurrUserId + "' ";
                        //sSQL += "UNION ";
                        //sSQL += "SELECT DISTINCT SMVKAAA003 ";
                        //sSQL += "FROM FrmAgtA INNER JOIN FrmAgtC ON SMVKAAA001=SMVKAAC002 WHERE SMVKAAA012='3' ";
                        //sSQL += "AND (SMVKAAC006 LIKE '" + CurrUserId + "%' OR SMVKAAC008 LIKE '" + CurrUserId + "%') ";
                        ////sSQL += "AND (GETDATE() BETWEEN CONVERT(DATETIME,SMVKAAA008+' 00:00:00') AND CONVERT(DATETIME,SMVKAAA009+' 23:59:59') OR SMVKAAC013='Y')";
                        //sSQL += "UNION ";
                        //sSQL += "SELECT DISTINCT SMVKAAA003 ";
                        //sSQL += "FROM FrmAgtA WHERE SMVKAAA012='3' ";
                        //sSQL += "AND ( SMVKAAA005 LIKE '" + CurrUserId + "%' OR SMVKAAA007 LIKE '" + CurrUserId + "%')";
                        //sSQL += "UNION ";
                        //sSQL += "SELECT DISTINCT SMVKAAA003 ";
                        //sSQL += "FROM FrmAgtA WHERE SMVKAAA012='2' ";
                        //sSQL += "AND (SMVKAAA005 LIKE '" + CurrUserId + "%' OR SMVKAAA007 LIKE '" + CurrUserId + "%') ";
                        //sSQL += "UNION ";
                        //sSQL += "SELECT DISTINCT SMVKAAA003 ";
                        //sSQL += "FROM FrmAgtA WHERE SMVKAAA012='1' ";
                        //sSQL += "AND (SMVKAAA005 LIKE '" + CurrUserId + "%' OR SMVKAAA007 LIKE '" + CurrUserId + "%')";
                        ////Add by 20110506:加入計畫代理人
                        //sSQL += "UNION ";
                        //sSQL += "Select DISTINCT PROJPM from ( ";
                        //sSQL += "  Select FORMID,PROJNO,PROJPM,PROJAGT,(case when PROJSTDATE in ('',null) then p20_begdate_8 else PROJSTDATE end) as PROJSTDATE,(case when PROJEDDATE in ('',null) then p20_enddate_8 else PROJEDDATE end) as PROJEDDATE from ProjAgt ";
                        //sSQL += "  join FrmAgtA on PROJPM=SMVKAAA003 and GETDATE() BETWEEN CONVERT(DATETIME,SMVKAAA008+' 00:00:00') AND CONVERT(DATETIME,SMVKAAA009+' 23:59:59') ";
                        //sSQL += "  where PROJAGT like '" + CurrUserId + "%'  ";
                        //sSQL += ") aa ";
                        //sSQL += "where GETDATE() BETWEEN CONVERT(DATETIME,PROJSTDATE+' 00:00:00') AND CONVERT(DATETIME,PROJEDDATE+' 23:59:59') ";

                        string sSQL = " SELECT distinct UserID FROM PerformableWorkItemList PFW";
                        sSQL += " LEFT JOIN ECPFlowNoticeLog LOG1 ON PFW.workItemOID = LOG1.EFNL002 and LOG1.EFNL005 = '1' ";
                        sSQL += " LEFT JOIN ECPFlowNoticeLog LOG2 ON PFW.workItemOID = LOG2.EFNL002 and LOG2.EFNL005 = '2' ";
                        sSQL += " WHERE (UserID<>'" + CurrUserId + "' and ISNULL(LOG2.EFNL003,LOG1.EFNL003)='" + CurrUserId + "') AND isNotice='0'";
                        DataSet ds = engine.getDataSet(sSQL, "TEMP");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string AUser = ds.Tables[0].Rows[i][0].ToString();
                                string AUserId = GetSomethingID(AUser);//被代理人
                                WorkItem[] wiNew = null;
                                if (isDB.Equals("N"))
                                {
                                    if (isFetchFewItems)
                                    {
                                        wiNew = adp.fetchPerformableWorkItems(AUserId, "1", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, "");
                                    }
                                    else
                                    {
                                        wiNew = adp.fetchPerformableWorkItems(AUserId, "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, "");
                                    }
                                }
                                else
                                {
                                    if (isFetchFewItems)
                                    {
                                        wiNew = adp.fetchPerformableWorkItems(AUserId, "1", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, "", engine);
                                    }
                                    else
                                    {
                                        wiNew = adp.fetchPerformableWorkItems(AUserId, "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, "", engine);
                                    }
                                }
                                ArrayList arrayWI = new ArrayList();

                                for (int j = 0; j < wiNew.Length; j++)
                                {
                                    WorkItem wii = wiNew[j];
                                    string strSQL3 = " SELECT serialNumber FROM PerformableWorkItemList PFW";
                                    strSQL3 += " LEFT JOIN ECPFlowNoticeLog LOG1 ON PFW.workItemOID = LOG1.EFNL002 and LOG1.EFNL005 = '1' ";
                                    strSQL3 += " LEFT JOIN ECPFlowNoticeLog LOG2 ON PFW.workItemOID = LOG2.EFNL002 and LOG2.EFNL005 = '2' ";
                                    strSQL3 += " WHERE (UserID<>'" + CurrUserId + "' and ISNULL(LOG2.EFNL003,LOG1.EFNL003)='" + CurrUserId + "') AND isNotice='0' and PFW.serialNumber='" + wii.processSerialNumber + "'";
                                    DataSet dsPWD = engine.getDataSet(strSQL3, "TEMP");
                                    if (dsPWD.Tables[0].Rows.Count > 0)
                                    {
                                        if (!arrayWI.Contains(wii))
                                        {
                                            arrayWI.Add(wii);
                                        }
                                    }
                                }
                                wi = mergeWorkItem(wi, arrayWI);
                            }
                        }
                    }
                }
                else
                {
                    if (isDB.Equals("N"))
                    {
                        wi = adp.fetchNoticeWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, ProcessIDList.ValueText);
                    }
                    else
                    {
                        wi = adp.fetchNoticeWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, strEndTime, AssignType.ValueText, ProcessIDList.ValueText, engine);
                    }

                    //這裡要讀取CUSOMENOTICE資料表
                    sql = "select * from CUSTOMENOTICE where RECEIVERID='" + (string)Session["UserID"] + "' and PROCESSTYPE ='INFO'";
                    if (!Subject.ValueText.Equals(""))
                    {
                        sql += " and SUBJECT like '%" + Subject.ValueText + "%'";
                    }
                    if (!StartTime.ValueText.Equals(""))
                    {
                        sql += " and left(SENDTIME,10) >= '" + StartTime.ValueText + "'";
                    }
                    if (!EndTime.ValueText.Equals(""))
                    {
                        sql += " and left(SENDTIME,10) <= '" + EndTime.ValueText + "'";
                    }
                    DataSet dsc = engine.getDataSet(sql, "TEMP");
                    ArrayList twi = new ArrayList();
                    for (int i = 0; i < dsc.Tables[0].Rows.Count; i++)
                    {
                        WorkItem wis = new WorkItem();
                        wis.batchPerformable = false;
                        wis.canRollback = false;
                        wis.createdTime = dsc.Tables[0].Rows[i]["SENDTIME"].ToString();
                        wis.currentState = "11";
                        wis.limits = 0;
                        wis.manualReassignType = "0";
                        wis.noticeType = "1";
                        wis.notifiable = false;
                        wis.passedWorkingTime = 0;
                        wis.processId = dsc.Tables[0].Rows[i]["PDID"].ToString();
                        wis.processInstanceOID = "";
                        wis.processName = dsc.Tables[0].Rows[i]["PROCESSNAME"].ToString();
                        wis.processSerialNumber = dsc.Tables[0].Rows[i]["PROCESSSERIALNUMBER"].ToString();
                        wis.requesterName = dsc.Tables[0].Rows[i]["SENDERNAME"].ToString();
                        wis.secured = false;
                        wis.subject = dsc.Tables[0].Rows[i]["SUBJECT"].ToString();
                        wis.viewTimes = 0;

                        twi.Add(wis);
                    }

                    WorkItem[] wiu = new WorkItem[wi.Length + twi.Count];
                    for (int i = 0; i < wi.Length; i++)
                    {
                        wiu[i] = wi[i];
                    }
                    for (int i = 0; i < twi.Count; i++)
                    {
                        wiu[i + wi.Length] = (WorkItem)twi[i];
                    }
                    wi = wiu;
                }
                //開始根據denyProcessID過濾以及denyWorkItems
                string[] denyProcessID = ((string)getSession("denyProcessID")).Split(new char[] { ';' });
                string[] denyWorkItems = ((string)getSession("denyWorkItems")).Split(new char[] { ';' });

                ArrayList tempWI = new ArrayList();
                for (int i = 0; i < wi.Length; i++)
                {
                    if (ProcessIDList.ValueText.Equals(""))
                    {
                        //所有流程, 這時候要過濾denyProcess
                        bool hasF = false;
                        for (int z = 0; z < denyProcessID.Length; z++)
                        {
                            if (wi[i].processId.Equals(denyProcessID[z]))
                            {
                                hasF = true;
                                break;
                            }
                        }
                        if (hasF)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!wi[i].processId.Equals(ProcessIDList.ValueText))
                        {
                            continue;
                        }
                    }
                    //過濾denyWorkItems
                    bool hasFound = false;
                    //國昌20100614:自訂的通知（原稿發起）不過濾
                    if (!wi[i].workItemName.Equals(""))
                    {
                        for (int z = 0; z < denyWorkItems.Length; z++)
                        {
                            if (wi[i].workItemName.Equals(denyWorkItems[z]))
                            {
                                hasFound = true;
                                break;
                            }
                        }
                    }
                    if (hasFound)
                    {
                        continue;
                    }

                    //過濾工作性質  hjlin 20100902
                    if (!AssignType.ValueText.Equals(""))
                    {
                        if (!wi[i].noticeType.Equals(AssignType.ValueText))
                        {
                            continue;
                        }
                    }

                    //過濾是否讀取
                    if (ViewTimes.ValueText.Equals("R"))
                    {
                        if (wi[i].viewTimes <= 0)
                        {
                            continue;
                        }
                    }
                    else if (ViewTimes.ValueText.Equals("U"))
                    {
                        if (wi[i].viewTimes > 0)
                        {
                            continue;
                        }
                    }
                    tempWI.Add(wi[i]);
                }
                //還原
                wi = new WorkItem[tempWI.Count];
                for (int i = 0; i < tempWI.Count; i++)
                {
                    wi[i] = (WorkItem)tempWI[i];
                }

                //Amos
                DataSet dsSP8 = new DataSet();
                DataSet dsNotice = new DataSet();
                dsSP8.Tables.Add("QQ1");
                dsNotice.Tables.Add("QQ2");
                if (BoxQueryType.Equals("W"))
                {
                    //取得EasyFlow的待簽核資料
                    dsSP8 = getSP8FormInBox(engine, isFetchFewItems);

                }
                else if (BoxQueryType.Equals("N"))
                {
                    //取得EF通知資料匣資料
                    dsNotice = getSP8FormInfoBox(engine);
                }
                setSession("init_WI", wi);
                setSession("init_DSSP8", dsSP8);
                setSession("init_DSNOTICE", dsNotice);

                if (BoxQueryType.Equals("W"))
                {
                    ArrayList orders = new ArrayList();
                    for (int i = 0; i < wi.Length; i++)
                    {
                        orders.Add(wi[i]);
                    }
                    for (int i = 0; i < dsSP8.Tables[0].Rows.Count; i++)
                    {
                        orders.Add(dsSP8.Tables[0].Rows[i]);
                    }
                    for (int i = 0; i < orders.Count; i++)
                    {
                        for (int j = i + 1; j < orders.Count; j++)
                        {
                            object ob1 = orders[i];
                            object ob2 = orders[j];
                            string ct1 = "";
                            string ct2 = "";
                            if (ob1.GetType().Name.Equals("WorkItem"))
                            {
                                ct1 = ((WorkItem)ob1).createdTime;
                            }
                            else
                            {
                                ct1 = ((DataRow)ob1)["resdd009"].ToString();
                            }
                            if (ob2.GetType().Name.Equals("WorkItem"))
                            {
                                ct2 = ((WorkItem)ob2).createdTime;
                            }
                            else
                            {
                                ct2 = ((DataRow)ob2)["resdd009"].ToString();
                            }
                            if (ct1.CompareTo(ct2) < 0)
                            {
                                //orders.Remove(ob1);
                                //orders.Insert(i, ob1);
                                object ptr = orders[i];
                                orders[i] = orders[j];
                                orders[j] = ptr;
                            }
                        }
                    }
                    setSession("init_ORDER", orders);
                    TotalRows.Text = "共" + orders.Count.ToString() + "筆資料";
                    int totalpages = (int)com.dsc.kernal.utility.Utility.Round((decimal)(orders.Count / ListTable.getPageSize()), 0);
                    if (totalpages * ListTable.getPageSize() < orders.Count)
                    {
                        totalpages++;
                    }
                    setSession("TOTALPAGES", totalpages);
                    TotalPages.Text = "/共" + totalpages.ToString() + "頁";
                }
                else
                {
                    ArrayList orders = new ArrayList();
                    for (int i = 0; i < wi.Length; i++)
                    {
                        orders.Add(wi[i]);
                    }

                    for (int i = 0; i < dsNotice.Tables[0].Rows.Count; i++)
                    {
                        orders.Add(dsNotice.Tables[0].Rows[i]);
                    }
                    for (int i = 0; i < orders.Count; i++)
                    {
                        for (int j = i + 1; j < orders.Count; j++)
                        {
                            object ob1 = orders[i];
                            object ob2 = orders[j];
                            string ct1 = "";
                            string ct2 = "";
                            if (ob1.GetType().Name.Equals("WorkItem"))
                            {
                                ct1 = ((WorkItem)ob1).createdTime;
                            }
                            else
                            {
                                ct1 = ((DataRow)ob1)["CREATETIME"].ToString();
                            }
                            if (ob2.GetType().Name.Equals("WorkItem"))
                            {
                                ct2 = ((WorkItem)ob2).createdTime;
                            }
                            else
                            {
                                ct2 = ((DataRow)ob2)["CREATETIME"].ToString();
                            }
                            if (ct1.CompareTo(ct2) < 0)
                            {
                                //orders.Remove(ob1);
                                //orders.Insert(i, ob1);
                                object ptr = orders[i];
                                orders[i] = orders[j];
                                orders[j] = ptr;
                            }
                        }
                    }
                    setSession("init_ORDER", orders);
                    TotalRows.Text = "共" + orders.Count.ToString() + "筆資料";
                    int totalpages = (int)com.dsc.kernal.utility.Utility.Round((decimal)(orders.Count / ListTable.getPageSize()), 0);
                    if (totalpages * ListTable.getPageSize() < orders.Count)
                    {
                        totalpages++;
                    }
                    setSession("TOTALPAGES", totalpages);
                    TotalPages.Text = "/共" + totalpages.ToString() + "頁";
                }

                //if (wi.Length > 0)
                if ((wi.Length > 0) || (dsSP8.Tables[0].Rows.Count > 0) || (dsNotice.Tables[0].Rows.Count > 0))
                {
                    /*
                    //如果一致, 則需要到SMWDAAA取得一致的資料, 並再次檢查SMWYAAA的018欄位（必須一致）
                    ArrayList extfield = new ArrayList();


                    string qstr = "select ";
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        qstr += ds.Tables[0].Columns[i].Caption + ", ";
                    }
                    qstr = qstr.Substring(0, qstr.Length - 2);
                    qstr += " from " + TableList.ValueText;
                    */

                    //檢查是否為單一流程單一角色
                    string checkProcessId = "";
                    string checkWorkItemName = "";
                    bool isSame = true;
                    for (int i = 0; i < wi.Length; i++)
                    {
                        if (checkProcessId.Equals(""))
                        {
                            checkProcessId = wi[i].processName;
                            checkWorkItemName = wi[i].workItemName;
                        }
                        else
                        {
                            string checkerId = wi[i].processName;
                            string checkerWN = wi[i].workItemName;
                            if ((!checkerId.Equals(checkProcessId)) || (!checkerWN.Equals(checkWorkItemName)))
                            {
                                isSame = false;
                                break;
                            }
                        }
                    }
                    /*
                    //新版, 先檢查AgentSchema有沒有一致
                    isSame = true;
                    string tempDA13 = "";
                    string tempDA01 = "";
                    sql = "select SMWDAAA013, SMWDAAA001, SMWDAAA003, SMWDAAA004 from SMWDAAA";
                    DataSet daset = engine.getDataSet(sql, "TEMP");
                    for (int i = 0; i < wi.Length; i++)
                    {
                        string da13 = "";
                        string da01 = "";
                        for (int j = 0; j < daset.Tables[0].Rows.Count; j++)
                        {
                            if ((daset.Tables[0].Rows[j][2].ToString().Equals(wi[i].processId)) && (daset.Tables[0].Rows[j][3].ToString().Equals(wi[i].workItemName)))
                            {
                                da13 = daset.Tables[0].Rows[j][0].ToString();
                                da01 = daset.Tables[0].Rows[j][1].ToString();
                                break;
                            }
                        }
                        if ((da13.Equals("")) || (da01.Equals("")))
                        {
                            isSame = false;
                            break;
                        }
                        if (tempDA13.Equals(""))
                        {
                            tempDA13 = da13;
                        }
                        else
                        {
                            if (!tempDA13.Equals(da13))
                            {
                                isSame = false;
                                break;
                            }
                        }
                        tempDA01 += "'" + da01 + "',";
                    }
                    //若AgentSchema都一致, 檢查
                    if (isSame)
                    {
                    }
                    */

                    //取得單號
                    string taging = "'*'";
                    for (int i = 0; i < wi.Length; i++)
                    {
                        taging += ",'" + Utility.filter(wi[i].processSerialNumber) + "'";
                    }
                    sql = "select SMWYAAA002, SMWYAAA005, SMWYAAA019, SMWYAAA007 from SMWYAAA where SMWYAAA005 in (" + taging + ") union select SMWYAAA002, FLOWGUID as SMWYAAA005, SMWYAAA019, SMWYAAA007 from SMWYAAA inner join FORMRELATION on ORIGUID=SMWYAAA019 where FLOWGUID in (" + taging + ")";
                    DataSet sh = engine.getDataSet(sql, "TEMP");

                    sql = "select SMWYAAA005, count(*) from SMWYAAA inner join FILEITEM on SMWYAAA019=JOBID group by SMWYAAA005";
                    DataSet att = engine.getDataSet(sql, "TEMP");

                    string qstr = "";

                    if (getSession("FromMail").ToString().Equals("1"))
                        qstr = "select top 1 GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME ";
                    else
                        qstr = "select GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME ";

                    DataSet extData = null;
                    DataSet det = null;
                    if (isSame)
                    {
                        //要從SMWDAAA取得延伸欄位
                        sql = "select SMWDAAA013, SMWDAAA001 from SMWDAAA join SMWAAAA on SMWAAAA001 = SMWDAAA005 where SMWDAAA003='" + Utility.filter(checkProcessId) + "' and SMWDAAA004='" + Utility.filter(checkWorkItemName) + "' and SMWAAAA005 not like N'%Maintain%'";
                        DataSet dus = engine.getDataSet(sql, "TEMP");

                        if (dus.Tables[0].Rows.Count > 0)
                        {
                            NLAgent agent = new NLAgent();
                            agent.loadSchema(dus.Tables[0].Rows[0][0].ToString());
                            agent.engine = engine;
                            agent.query("1=2");

                            string tableName = agent.defaultData.getTableName();

                            string tempsql = "select GUID";
                            sql = "select SMWDAAC003, SMWDAAC004, SMWDAAC005 from SMWDAAC where SMWDAAC002='" + Utility.filter(dus.Tables[0].Rows[0][1].ToString()) + "'";
                            det = engine.getDataSet(sql, "TEMP");
                            for (int i = 0; i < det.Tables[0].Rows.Count; i++)
                            {
                                qstr += "," + det.Tables[0].Rows[i][0].ToString();
                                tempsql += "," + det.Tables[0].Rows[i][1].ToString();
                            }

                            if (det.Tables[0].Rows.Count > 0)
                            {
                                string objectGUIDS = "'*'";
                                for (int j = 0; j < sh.Tables[0].Rows.Count; j++)
                                {
                                    objectGUIDS += ",'" + Utility.filter(sh.Tables[0].Rows[j][2].ToString()) + "'";
                                }
                                tempsql += " from " + tableName + " where GUID in (" + objectGUIDS + ")";
                                extData = engine.getDataSet(tempsql, "TEMP");
                            }
                        }
                        else
                        {
                            isSame = false;
                        }
                    }
                    qstr += " from SMWL";


                    DataObjectSet dos = new DataObjectSet();
                    StringBuilder sbddd = new StringBuilder();
                    sbddd.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    sbddd.AppendLine("<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">");
                    sbddd.AppendLine("<queryStr>" + qstr + "</queryStr>");
                    sbddd.AppendLine("  <isCheckTimeStamp>true</isCheckTimeStamp>");
                    sbddd.AppendLine("  <fieldDefinition>");

                    sbddd.AppendLine("    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"40\" defaultValue=\"\" displayName=\"重要性\" showName=\"0:低;1:中;2:高\"/>");
                    sbddd.AppendLine("    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已通知;4:已撤銷;5:已中止\"/>");
                    sbddd.AppendLine("    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程名稱\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"CREATETIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作性質\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"VIEWTIMES\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"讀取次數\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKASSIGNMENT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作指派識別\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"ASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作類型\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"REASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"轉派類型\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>");

                    if (det != null)
                    {
                        for (int i = 0; i < det.Tables[0].Rows.Count; i++)
                        {
                            sbddd.AppendLine("    <field dataField=\"" + det.Tables[0].Rows[i][0].ToString() + "\" typeField=\"STRING\" lengthField=\"50000\" defaultValue=\"\" displayName=\"" + det.Tables[0].Rows[i][2].ToString() + "\" showName=\"\"/>");
                        }
                    }

                    sbddd.AppendLine("  </fieldDefinition>");
                    sbddd.AppendLine("  <identityField>");
                    sbddd.AppendLine("    <field dataField=\"GUID\"/>");
                    sbddd.AppendLine("  </identityField>");
                    sbddd.AppendLine("  <keyField>");
                    sbddd.AppendLine("    <field dataField=\"SHEETNO\"/>");
                    sbddd.AppendLine("    <field dataField=\"PROCESSNAME\"/>");
                    sbddd.AppendLine("  </keyField>");
                    sbddd.AppendLine("  <allowEmptyField>");

                    sbddd.AppendLine("    <field dataField=\"GUID\"/>");
                    sbddd.AppendLine("    <field dataField=\"ATTACH\"/>");
                    sbddd.AppendLine("    <field dataField=\"IMPORTANT\"/>");
                    sbddd.AppendLine("    <field dataField=\"CURRENTSTATE\"/>");
                    sbddd.AppendLine("    <field dataField=\"PROCESSNAME\"/>");
                    sbddd.AppendLine("    <field dataField=\"SHEETNO\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKITEMNAME\"/>");
                    sbddd.AppendLine("    <field dataField=\"SUBJECT\"/>");
                    sbddd.AppendLine("    <field dataField=\"USERNAME\"/>");
                    sbddd.AppendLine("    <field dataField=\"CREATETIME\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKTYPE\"/>");
                    sbddd.AppendLine("    <field dataField=\"VIEWTIMES\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKASSIGNMENT\"/>");
                    sbddd.AppendLine("    <field dataField=\"ASSIGNMENTTYPE\"/>");
                    sbddd.AppendLine("    <field dataField=\"REASSIGNMENTTYPE\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_INSERTUSER\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_INSERTTIME\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_MODIFYUSER\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_MODIFYTIME\"/>");

                    if (det != null)
                    {
                        for (int i = 0; i < det.Tables[0].Rows.Count; i++)
                        {
                            sbddd.AppendLine("    <field dataField=\"" + det.Tables[0].Rows[i][1].ToString() + "\" />");
                        }
                    }

                    sbddd.AppendLine("  </allowEmptyField>");
                    sbddd.AppendLine("  <nonUpdateField>");
                    sbddd.AppendLine("  </nonUpdateField>");
                    sbddd.AppendLine("</DataObject>");
                    dos.dataObjectSchema = sbddd.ToString();

                    dos.isNameLess = true;


                    //MessageBox("Edward");
                    for (int i = 0; i < 0; i++) //edward:20121203
                    //for (int i = 0; i < wi.Length; i++)
                    {
                        DataObject ddo = dos.create();
                        //國昌20100614:自訂的通知（原稿發起）不過濾
                        if (!wi[i].workItemOID.Equals(""))
                        {
                            ddo.setData("GUID", wi[i].workItemOID);
                        }
                        else
                        {
                            ddo.setData("GUID", IDProcessor.getID(""));
                        }
                        ddo.setData("CURRENTSTATE", wi[i].currentState);
                        ddo.setData("PROCESSNAME", wi[i].processName);

                        string objG = "";
                        for (int j = 0; j < sh.Tables[0].Rows.Count; j++)
                        {
                            if (wi[i].processSerialNumber.Equals(sh.Tables[0].Rows[j][1].ToString()))
                            {
                                ddo.setData("SHEETNO", sh.Tables[0].Rows[j][0].ToString());
                                objG = sh.Tables[0].Rows[j][2].ToString();
                                ddo.setData("IMPORTANT", sh.Tables[0].Rows[j][3].ToString());
                            }
                        }
                        bool hasAtt = false;
                        for (int j = 0; j < att.Tables[0].Rows.Count; j++)
                        {
                            if (wi[i].processSerialNumber.Equals(att.Tables[0].Rows[j][0].ToString()))
                            {
                                hasAtt = true;
                                break;
                            }
                        }
                        if (hasAtt)
                        {
                            ddo.setData("ATTACH", "{[font color=red]}！{[/font]}");
                        }
                        else
                        {
                            ddo.setData("ATTACH", "");
                        }
                        ddo.setData("WORKITEMNAME", wi[i].workItemName);
                        ddo.setData("SUBJECT", wi[i].subject);
                        ddo.setData("USERNAME", wi[i].requesterName);
                        ddo.setData("CREATETIME", wi[i].createdTime);
                        if (BoxQueryType.Equals("W"))
                        {
                            if (wi[i].assignmentType.Length == 1)
                            {
                                if (wi[i].assignmentType.Equals("0"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids2", "正常指派"));
                                }
                                else if (wi[i].assignmentType.Equals("1"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids3", "他人重新指派(退回重辦)"));
                                }
                                else
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids4", "抽回重辦工作"));
                                }

                                ddo.setData("ASSIGNMENTTYPE", wi[i].assignmentType);
                                ddo.setData("REASSIGNMENTTYPE", "");
                            }
                            else
                            {
                                string ast = wi[i].assignmentType.Substring(1, 1);
                                if (ast.Equals("0"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids13", "代理轉派"));
                                }
                                else if (ast.Equals("1"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids14", "系統代理人轉派"));
                                }
                                else if (ast.Equals("2"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids15", "系統逾時轉派"));
                                }
                                else if (ast.Equals("3"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids16", "管理員代理轉派"));
                                }
                                else if (ast.Equals("4"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids17", "負責人代理轉派"));
                                }
                                else if (ast.Equals("5"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids18", "工作取回"));
                                }
                                else if (ast.Equals("6"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids19", "工作轉派"));
                                }
                                else if (ast.Equals("7"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids20", "管理員工作轉派"));
                                }
                                else if (ast.Equals("8"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids21", "負責人工作轉派"));
                                }
                                ddo.setData("ASSIGNMENTTYPE", "0");
                                ddo.setData("REASSIGNMENTTYPE", ast);
                            }
                        }
                        else
                        {
                            if (wi[i].noticeType.Equals("0"))
                            {
                                ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids5", "系統自動通知"));
                            }
                            else if (wi[i].noticeType.Equals("1"))
                            {
                                ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids6", "私人轉寄"));
                            }
                            else if (wi[i].noticeType.Equals("2"))
                            {
                                ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids7", "一般轉寄, 顯示在流程追蹤"));
                            }
                            else if (wi[i].noticeType.Equals("3"))
                            {
                                ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids8", "被代理通知"));
                            }
                            else
                            {
                                ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids9", "流程被撤銷或工作被回收"));
                            }
                        }
                        ddo.setData("VIEWTIMES", wi[i].viewTimes.ToString());
                        ddo.setData("WORKASSIGNMENT", wi[i].workAssignmentOID);
                        ddo.setData("D_INSERTUSER", "SYSTEM");
                        ddo.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                        ddo.setData("D_MODIFYUSER", "");
                        ddo.setData("D_MODIFYTIME", "");

                        if (isSame)
                        {
                            for (int z = 0; z < det.Tables[0].Rows.Count; z++)
                            {
                                string fName = det.Tables[0].Rows[z][0].ToString();
                                string fields = det.Tables[0].Rows[z][1].ToString();
                                for (int j = 0; j < extData.Tables[0].Rows.Count; j++)
                                {
                                    if (extData.Tables[0].Rows[j]["GUID"].ToString().Equals(objG))
                                    {
                                        ddo.setData(fName, extData.Tables[0].Rows[j][fields].ToString());
                                    }
                                }
                            }
                        }
                        ddo.Tag = wi[i];
                        dos.addDraft(ddo);

                    }
                    //Amos
                    #region 處理SP8
                    if (BoxQueryType.Equals("W"))
                    {
                        int intTEMP = 1;
                        if (!getSession("FromMail").ToString().Equals("1"))
                            //intTEMP = dsSP8.Tables[0].Rows.Count;
                            intTEMP = 0; //edward: 20121203
                        for (int i = 0; i < intTEMP; i++)
                        {
                            DataObject ddo = dos.create();

                            ddo.setData("GUID", dsSP8.Tables[0].Rows[i]["resda001"].ToString() + "-" + dsSP8.Tables[0].Rows[i]["resda002"].ToString());
                            if (i < 10)
                            {
                                ddo.setData("CURRENTSTATE", "0");
                                ddo.setData("PROCESSNAME", "\t" + dsSP8.Tables[0].Rows[i]["resca002"].ToString());
                                ddo.setData("SHEETNO", dsSP8.Tables[0].Rows[i]["resda002"].ToString());
                                if (dsSP8.Tables[0].Rows[i]["resda032"].ToString().Equals("0"))
                                {
                                    ddo.setData("IMPORTANT", "低");
                                }
                                else if (dsSP8.Tables[0].Rows[i]["resda032"].ToString().Equals("1"))
                                {
                                    ddo.setData("IMPORTANT", "普通");
                                }
                                else
                                {
                                    ddo.setData("IMPORTANT", "高");
                                }
                                if (dsSP8.Tables[0].Rows[i]["resde002"].ToString() != "")
                                {
                                    ddo.setData("ATTACH", "{[font color=red]}！{[/font]}");
                                }
                                else
                                {
                                    ddo.setData("ATTACH", "");
                                }
                                //ddo.setData("WORKITEMNAME", "");
                                ddo.setData("SUBJECT", dsSP8.Tables[0].Rows[i]["resda031"].ToString());
                                ddo.setData("USERNAME", dsSP8.Tables[0].Rows[i]["resda_017"].ToString());
                                ddo.setData("CREATETIME", dsSP8.Tables[0].Rows[i]["resdd009"].ToString());
                                //ddo.setData("WORKTYPE", "");
                                ddo.setData("ASSIGNMENTTYPE", dsSP8.Tables[0].Rows[i]["AssignType"].ToString());
                                //ddo.setData("REASSIGNMENTTYPE", "");
                                ddo.setData("VIEWTIMES", dsSP8.Tables[0].Rows[i]["resdd013"].ToString());
                                //ddo.setData("WORKASSIGNMENT", "");
                                //ddo.setData("D_INSERTUSER", "SYSTEM");
                                //ddo.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                                //ddo.setData("D_MODIFYUSER", "");
                                //ddo.setData("D_MODIFYTIME", "");
                            }
                            ddo.Tag = dsSP8.Tables[0].Rows[i];

                            dos.addDraft(ddo);

                        }
                    }
                    else if (BoxQueryType.Equals("N"))
                    {
                        for (int i = 0; i < 0; i++)
                        //for (int i = 0; i < dsNotice.Tables[0].Rows.Count; i++)
                        {
                            DataObject fdo = dos.create();
                            fdo.setData("GUID", dsNotice.Tables[0].Rows[i]["GUID"].ToString());
                            fdo.setData("CURRENTSTATE", dsNotice.Tables[0].Rows[i]["CURRENTSTATE"].ToString());
                            fdo.setData("PROCESSNAME", "\t" + dsNotice.Tables[0].Rows[i]["PROCESSNAME"].ToString());
                            fdo.setData("SHEETNO", dsNotice.Tables[0].Rows[i]["SHEETNO"].ToString());
                            fdo.setData("IMPORTANT", dsNotice.Tables[0].Rows[i]["IMPORTANT"].ToString());
                            fdo.setData("ATTACH", dsNotice.Tables[0].Rows[i]["ATTACH"].ToString());
                            fdo.setData("WORKITEMNAME", dsNotice.Tables[0].Rows[i]["WORKITEMNAME"].ToString());
                            fdo.setData("SUBJECT", dsNotice.Tables[0].Rows[i]["SUBJECT"].ToString());
                            fdo.setData("USERNAME", dsNotice.Tables[0].Rows[i]["USERNAME"].ToString());
                            fdo.setData("CREATETIME", dsNotice.Tables[0].Rows[i]["CREATETIME"].ToString());
                            fdo.setData("WORKTYPE", dsNotice.Tables[0].Rows[i]["WORKTYPE"].ToString());
                            fdo.setData("ASSIGNMENTTYPE", dsNotice.Tables[0].Rows[i]["ASSIGNMENTTYPE"].ToString());
                            fdo.setData("REASSIGNMENTTYPE", dsNotice.Tables[0].Rows[i]["REASSIGNMENTTYPE"].ToString());
                            fdo.setData("VIEWTIMES", dsNotice.Tables[0].Rows[i]["VIEWTIMES"].ToString());
                            fdo.setData("WORKASSIGNMENT", dsNotice.Tables[0].Rows[i]["WORKASSIGNMENT"].ToString());
                            fdo.setData("D_INSERTUSER", dsNotice.Tables[0].Rows[i]["D_INSERTUSER"].ToString());
                            fdo.setData("D_INSERTTIME", dsNotice.Tables[0].Rows[i]["D_INSERTTIME"].ToString());
                            fdo.setData("D_MODIFYUSER", dsNotice.Tables[0].Rows[i]["D_MODIFYUSER"].ToString());
                            fdo.setData("D_MODIFYTIME", dsNotice.Tables[0].Rows[i]["D_MODIFYTIME"].ToString());
                            fdo.Tag = dsNotice.Tables[0].Rows[i];
                            dos.addDraft(fdo);
                        }
                    }
                    #endregion

                    ListTable.IsGeneralUse = false;
                    ListTable.InputForm = "Detail.aspx";
                    ListTable.CurPageUID = PageUniqueID;
                    ArrayList hFields = (ArrayList)getSession("HiddenFields");
                    string[] hf = new string[hFields.Count + 5];
                    hf[0] = "GUID";
                    hf[1] = "WORKASSIGNMENT";
                    hf[2] = "ASSIGNMENTTYPE";
                    hf[3] = "REASSIGNMENTTYPE";
                    hf[4] = "WORKTYPE";
                    for (int i = 0; i < hFields.Count; i++)
                    {
                        hf[i + 5] = (string)hFields[i];
                    }
                    ListTable.HiddenField = hf;
                    ListTable.WidthMode = 1;
                    ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                    String[,] orderby = new string[,] { { "CREATETIME", DataObjectConstants.DESC } };
                    dos.sort(orderby);
                    ListTable.dataSource = dos;

                    string[] fieldOrder = (string[])getSession("fieldOrder");
                    ListTable.reOrderField(fieldOrder);
                    ListTable.updateTable();
                    //代理人處理機制
                    if (BoxQueryType.Equals("W") && isSubstitute.Equals("Y"))
                    {
                        //篩選代理清單
                        StringCollection filteredSB = new StringCollection();
                        StringCollection allSB = (StringCollection)getSession("SUBSLIST");
                        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
                        {
                            //Amos ECP的表單才執行
                            if (dos.getAvailableDataObject(i).Tag is com.dsc.flow.data.WorkItem)
                            {
                                //紀錄代理的流程工作項目OID
                                if (allSB.Contains(((WorkItem)dos.getAvailableDataObject(i).Tag).workItemOID))
                                {
                                    filteredSB.Add(((WorkItem)dos.getAvailableDataObject(i).Tag).workItemOID);
                                }
                            }
                        }
                        setSession("SUBSLIST", filteredSB);
                    }
                    setSession("CURLIST", dos);
                    setSession("CURRENTPAGE", 1);
                    //changePage(1);
			ListTable_ShowPagingClick();
                }
                else
                {
                    string qstr = "select GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME from SMWL";
                    DataObjectSet dos = new DataObjectSet();
                    StringBuilder sbddd = new StringBuilder();
                    sbddd.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    sbddd.AppendLine("<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">");
                    sbddd.AppendLine("<queryStr>" + qstr + "</queryStr>");
                    sbddd.AppendLine("  <isCheckTimeStamp>true</isCheckTimeStamp>");
                    sbddd.AppendLine("  <fieldDefinition>");

                    sbddd.AppendLine("    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"重要性\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>");
                    sbddd.AppendLine("    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程名稱\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"CREATETIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作性質\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"VIEWTIMES\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"讀取次數\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKASSIGNMENT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作指派識別\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"ASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作類型\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"REASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"轉派類型\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>");

                    sbddd.AppendLine("  </fieldDefinition>");
                    sbddd.AppendLine("  <identityField>");
                    sbddd.AppendLine("    <field dataField=\"GUID\"/>");
                    sbddd.AppendLine("  </identityField>");
                    sbddd.AppendLine("  <keyField>");
                    sbddd.AppendLine("    <field dataField=\"GUID\"/>");
                    sbddd.AppendLine("  </keyField>");
                    sbddd.AppendLine("  <allowEmptyField>");

                    sbddd.AppendLine("    <field dataField=\"GUID\"/>");
                    sbddd.AppendLine("    <field dataField=\"ATTACH\"/>");
                    sbddd.AppendLine("    <field dataField=\"IMPORTANT\"/>");
                    sbddd.AppendLine("    <field dataField=\"CURRENTSTATE\"/>");
                    sbddd.AppendLine("    <field dataField=\"PROCESSNAME\"/>");
                    sbddd.AppendLine("    <field dataField=\"SHEETNO\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKITEMNAME\"/>");
                    sbddd.AppendLine("    <field dataField=\"SUBJECT\"/>");
                    sbddd.AppendLine("    <field dataField=\"USERNAME\"/>");
                    sbddd.AppendLine("    <field dataField=\"CREATETIME\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKTYPE\"/>");
                    sbddd.AppendLine("    <field dataField=\"VIEWTIMES\"/>");
                    sbddd.AppendLine("    <field dataField=\"WORKASSIGNMENT\"/>");
                    sbddd.AppendLine("    <field dataField=\"ASSIGNMENTTYPE\"/>");
                    sbddd.AppendLine("    <field dataField=\"REASSIGNMENTTYPE\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_INSERTUSER\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_INSERTTIME\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_MODIFYUSER\"/>");
                    sbddd.AppendLine("    <field dataField=\"D_MODIFYTIME\"/>");

                    sbddd.AppendLine("  </allowEmptyField>");
                    sbddd.AppendLine("  <nonUpdateField>");
                    sbddd.AppendLine("  </nonUpdateField>");
                    sbddd.AppendLine("</DataObject>");
                    dos.dataObjectSchema = sbddd.ToString();

                    dos.isNameLess = true;

                    ListTable.IsGeneralUse = false;
                    ListTable.WidthMode = 1;
                    ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                    ListTable.InputForm = "Detail.aspx";
                    ListTable.CurPageUID = PageUniqueID;
                    ListTable.HiddenField = new string[] { "GUID", "WORKASSIGNMENT", "ASSIGNMENTTYPE", "REASSIGNMENTTYPE" };
                    String[,] orderby = new string[,] { { "CREATETIME", DataObjectConstants.DESC } };
                    dos.sort(orderby);
                    ListTable.dataSource = dos;
                    string[] fieldOrder = (string[])getSession("fieldOrder");
                    ListTable.reOrderField(fieldOrder);
                    ListTable.updateTable();

                    setSession("CURLIST", dos);
                }
                adp.logout();
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
                //WriteLOGLine(te);
            }

            //MessageBox("已取得清單");
        }
    }
    protected void FilterButton_Click(object sender, EventArgs e)
    {
        queryData(false);     
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

            string boxType = (string)getSession("BoxQueryType");
            if (boxType.Equals("W"))
            {
                //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
                //目前都給他為ProcessModify
                sql = "select SMWDAAA006, SMWDAAA005 from SMWDAAA join SMWAAAA on SMWAAAA001 = SMWDAAA005 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(wi.processId) + "' and SMWDAAA004='" + Utility.filter(wi.workItemName) + "' and SMWAAAA005 not like N'%Maintain%'";
                DataSet ds2 = engine.getDataSet(sql, "TEMP");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    if (ds2.Tables[0].Rows[0][0].ToString().Equals("New"))
                    {
                        fd.UIStatus = FlowStatusData.ProcessNew;

                    }
                    else
                    {

                        fd.UIStatus = FlowStatusData.ProcessModify;

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //有原稿資料
                            //有可能是ProcessNew之後的ProcessModify, 必須考慮替換fd.ObjectGUID
                            if (!ds.Tables[0].Rows[0][1].ToString().Equals(ds2.Tables[0].Rows[0][1].ToString()))
                            {
                                //若原稿的SMWAAAA001跟目前關卡的SMWAAAA001不同, 表示要替換
                                sql = "select CURGUID from FORMRELATION where ORIGUID='" + Utility.filter(fd.ObjectGUID) + "'";
                                ds2 = engine.getDataSet(sql, "TEMP");
                                fd.ObjectGUID = ds2.Tables[0].Rows[0][0].ToString();
                            }
                        }
                    }
                }
                else
                {
                    fd.UIStatus = FlowStatusData.ProcessModify;
                }
            }
            else
            {
                //國昌20100614:自訂的通知（原稿發起）
                if (wi.workItemOID.Equals(""))
                {
                    fd.UIStatus = FlowStatusData.FormReadOnly;
                }
                else
                {
                    fd.UIStatus = FlowStatusData.FormNotify;
                    fd.IsAllowWithDraw = false;
                }
                if (wi.currentState.Equals("11"))
                    fd.UIStatus = FlowStatusData.FormNotify;
            }

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
            //try
            //{
            //    engine.close();
            //}
            //catch { };
            //return null;

            //Amos
            //建立FlowStatusData物件
            FlowStatusData fd = new FlowStatusData();
            DataRow dr = (DataRow)objects.Tag;
            //ListTable.FormTitle = "\t" + dr["resca002"].ToString() + "(" + dr["resda002"].ToString() + ")";
            if (((string)getSession("BoxQueryType")).Equals("W"))
            {
                //ListTable.FormTitle = "\t" + dr["resca002"].ToString() + "(" + dr["resda002"].ToString() + ")";
                ListTable.FormTitle = "EasyFlow 電子表單";
		fd.PDID = dr["resda001"].ToString();
                fd.ObjectGUID = "";
                fd.FlowGUID = dr["resda002"].ToString();
                fd.WorkItemOID = dr["resdd003"].ToString();
                fd.TargetWorkItemOID = dr["resdd004"].ToString();
                fd.workAssignmentOID = dr["resdd005"].ToString();
                fd.assignmentType = dr["FormStatus"].ToString();//拿來存放CALL EF 的FormStatus
            }
            else
            {
                //ListTable.FormTitle = "\t" + dr["PROCESSNAME"].ToString() + "(" + dr["GUID"].ToString().Split('-')[1] + ")";
                ListTable.FormTitle = "EasyFlow 電子表單";
		fd.PDID = dr["GUID"].ToString().Split('-')[0];//dr["resdd001"].ToString();
                fd.ObjectGUID = "";
                //fd.FlowGUID = dr["GUID"].ToString().Split('-')[1];//dr["resdd002"].ToString();
                fd.FlowGUID = dr["resda002"].ToString();
                //fd.WorkItemOID = "";//dr["GUID"].ToString();//dr["resdd003"].ToString();
                //fd.TargetWorkItemOID = "";// dr["GUID"].ToString();// dr["resdd004"].ToString();
                //fd.workAssignmentOID = "";// dr["GUID"].ToString(); //dr["resdd005"].ToString();
                fd.WorkItemOID = dr["resdd003"].ToString();
                fd.TargetWorkItemOID = dr["resdd004"].ToString();
                fd.workAssignmentOID = dr["resdd005"].ToString();
                fd.assignmentType = dr["FormStatus"].ToString();//拿來存放CALL EF 的FormStatus
            }
            return fd;
        }
    }

    protected void ListTable_RefreshButtonClick()
    {
        if (getSession("FromMail").ToString().Equals("1"))
        {
            queryData(true);
        }
        else
        {
            queryData(false);
        }
    }

    protected void GroupSign_Click(object sender, EventArgs e)
    {
        AbstractEngine engine = null;

        try
        {
            DataObject[] ary = ListTable.getSelectedItem();
            if (ary.Length == 0)
            {
                //MessageBox("請選擇要群簽的項目");
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " QueryError1", "請選擇要群簽的項目"));
                return;
            }
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            string sql = "";
            DataSet ds = null;

            engine = factory.getEngine(engineType, connectString);
            sql = "select SMVPAAA009 from SMVPAAA";
            ds = engine.getDataSet(sql, "TEMP");

            //取得不可群簽之設定
            DataSet dsG = null;
            sql = "select SMWIAAD003 from SMWIAAD where SMWIAAD002 = (select SMWIAAA001 from SMWIAAA where SMWIAAA002 = '" + getSession("BoxID").ToString() + "');";
            dsG = engine.getDataSet(sql, "TEMP");

            string isDebugPage = "N";
            if (ds.Tables[0].Rows.Count == 0)
            {
                isDebugPage = "N";
            }
            else
            {
                isDebugPage = ds.Tables[0].Rows[0][0].ToString();
            }

            SysParam sp = new SysParam(engine);
            string siteName = sp.getParam("SiteName");
            //engine.close();

            StringCollection noGroupSignProc = new StringCollection();

            //<ECP 0.0.11.2 Build1 群簽呼叫方式改變>                           
            string RefProcessExist = "";
            StringCollection scGroupSignWorkOIDList = new StringCollection();
            for (int i = 0; i < ary.Length; i++)
            {
                //Amos ECP的表單才執行
                if (ary[i].Tag is com.dsc.flow.data.WorkItem)
                {
                    WorkItem wi = (WorkItem)ary[i].Tag;
                    if (dsG.Tables[0].Select("SMWIAAD003 = '" + wi.processId + "'").Length > 0)
                    {
                        if (!noGroupSignProc.Contains(wi.processName))
                        {
                            noGroupSignProc.Add(wi.processName);
                        }
                    }
                    else
                    {
                        string objectGUID = "";
                        sql = "select SMWYAAA019, SMWYAAA018 from SMWYAAA where SMWYAAA005='" + Utility.filter(wi.processSerialNumber) + "'";
                        ds = engine.getDataSet(sql, "TEMP");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objectGUID = ds.Tables[0].Rows[0][0].ToString();
                        }
                        else
                        {
                            //無緣稿資料, 表示為發起參考流程的
                            sql = "select CURGUID from FORMRELATION where FLOWGUID='" + Utility.filter(wi.processSerialNumber) + "'";
                            DataSet dd3 = engine.getDataSet(sql, "TEMP");
                            objectGUID = dd3.Tables[0].Rows[0][0].ToString();
                        }

                        sql = "Select SMWDAAA030 from SMWDAAA join SMWAAAA on SMWAAAA001 = SMWDAAA005 inner join SMWBAAA on SMWDAAA003=SMWBAAA004  where SMWBAAA003='" + wi.processId + "' and SMWDAAA006='Display' and SMWDAAA004='" + wi.workItemName + "' and SMWAAAA005 not like N'%Maintain%'";

                        DataSet dsa = engine.getDataSet(sql, "TEMP");
                        String checkWaitForReference = "";
                        if (dsa.Tables[0].Rows.Count > 0)
                        {
                            checkWaitForReference = dsa.Tables[0].Rows[0][0].ToString();
                        }
                        if (checkWaitForReference.Equals("Y"))
                        {
                            //檢查是否有參考流程
                            string sch = "select FORMRELATION.GUID from FORMRELATION left outer join DATARELATION on CURGUID=CURRENTGUID where ORIGUID='" + objectGUID + "' and RELATIONTYPE='1' and isnull(DATA_STATUS,'N')<>'Y'";
                            DataSet che = engine.getDataSet(sch, "TEMP");
                            if (che.Tables[0].Rows.Count > 0)
                            {
                                RefProcessExist = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError15", "部份流程所發起的參考流程尚未全部簽核完畢");
                                continue;
                                //throw new Exception("所發起的參考流程尚未全部簽核完畢");
                                //throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError15", "所發起的參考流程尚未全部簽核完畢"));
                            }
                        }
                        scGroupSignWorkOIDList.Add(wi.workItemOID);
                    }
                }
            }

            if (scGroupSignWorkOIDList.Count > 0)
            {
                string flowType = sp.getParam("FlowAdapter");
                string con1 = sp.getParam("NaNaWebService");
                string con2 = sp.getParam("DotJWebService");
                string account = sp.getParam("FlowAccount");
                string password = sp.getParam("FlowPassword");

                FlowFactory ff = new FlowFactory();
                GPFlowAdapter.GPFlow adp = (GPFlowAdapter.GPFlow)ff.getAdapter(flowType);

                adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
                adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

                string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
                fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
                adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, true);

                //代理轉派機制
                StringCollection subsWorkOIDList = (StringCollection)getSession("SUBSLIST");
                StringCollection ReassignWorkItemFailedList = null;
                if (subsWorkOIDList != null)
                {
                    ReassignWorkItemFailedList = new StringCollection();
                    for (int i = 0; i < scGroupSignWorkOIDList.Count; i++)
                    {
                        if (subsWorkOIDList.Contains(scGroupSignWorkOIDList[i]))
                        {
                            try
                            {
                                adp.managementReassignWorkItem((string)Session["UserGUID"], scGroupSignWorkOIDList[i], "系統自動轉派至代理人");
                            }
                            catch
                            {
                                //如果有轉派失敗之情況
                                ReassignWorkItemFailedList.Add(scGroupSignWorkOIDList[i]);
                            }
                        }
                    }
                }
                //有轉派失敗之情況, 須將例外之標的移除
                if (ReassignWorkItemFailedList != null && ReassignWorkItemFailedList.Count > 0)
                {
                    for (int i = 0; i < ReassignWorkItemFailedList.Count; i++)
                    {
                        if (scGroupSignWorkOIDList.Contains(ReassignWorkItemFailedList[i]))
                        {
                            scGroupSignWorkOIDList.Remove(ReassignWorkItemFailedList[i]);
                        }
                    }
                }
                string finalSignResult = signResult.ValueText.Split(new string[] { ":::" }, StringSplitOptions.None)[0];

                if (finalSignResult.Equals("Y"))
                {
                    adp.completeBatchWorkItem((string)Session["UserID"], scGroupSignWorkOIDList, signResult.ReadOnlyText.Split(new string[] { ":::" }, StringSplitOptions.None)[0], signOpinion.ValueText);
                }
                else
                {
                    adp.BatchTerminateProcess((string)Session["UserID"], scGroupSignWorkOIDList, signResult.ReadOnlyText.Split(new string[] { ":::" }, StringSplitOptions.None)[0], signOpinion.ValueText);
                }
                adp.logout();


                if (!String.IsNullOrEmpty(RefProcessExist))
                {
                    MessageBox(RefProcessExist);
                }
            }
            //</ECP 0.0.11.2 Build1 群簽呼叫方式改變>

            //for (int i = 0; i < ary.Length; i++)
            //{
            //    engine = factory.getEngine(engineType, connectString);
            //    WorkItem wi = (WorkItem)ary[i].Tag;

            //    if (dsG.Tables[0].Select("SMWIAAD003 = '" + wi.processId + "'").Length > 0)
            //    {
            //        if (!noGroupSignProc.Contains(wi.processName))
            //        {
            //            noGroupSignProc.Add(wi.processName);
            //        }
            //    }
            //    else
            //    {
            //        //取得資料物件代碼, 由原稿取得
            //        sql = "select SMWYAAA019, SMWYAAA018 from SMWYAAA where SMWYAAA005='" + Utility.filter(wi.processSerialNumber) + "'";
            //        ds = engine.getDataSet(sql, "TEMP");

            //        string objectGUID = "";

            //        string urlP = "";

            //        urlP += "ParentPanelID=" + CurPanelID + "&DataListID=ListTable";
            //        urlP += "&UIType=Process";

            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            objectGUID = ds.Tables[0].Rows[0][0].ToString();
            //        }
            //        else
            //        {
            //            //無緣稿資料, 表示為發起參考流程的
            //            sql = "select CURGUID from FORMRELATION where FLOWGUID='" + Utility.filter(wi.processSerialNumber) + "'";
            //            DataSet dd3 = engine.getDataSet(sql, "TEMP");
            //            objectGUID = dd3.Tables[0].Rows[0][0].ToString();
            //        }
            //        //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
            //        //目前都給他為ProcessModify
            //        sql = "select SMWDAAA006, SMWDAAA005 from SMWDAAA inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(wi.processId) + "' and SMWDAAA004='" + Utility.filter(wi.workItemName) + "'";
            //        DataSet ds2 = engine.getDataSet(sql, "TEMP");
            //        if (ds2.Tables[0].Rows.Count > 0)
            //        {
            //            if (ds2.Tables[0].Rows[0][0].ToString().Equals("New"))
            //            {
            //                MessageBox(wi.processName + ":" + wi.workItemName + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " QueryError2", "為流程中新增表單資料, 無法群簽, 請點選該單據進入填寫資料."));
            //                continue;
            //            }
            //            else
            //            {
            //                //有可能是ProcessNew之後的ProcessModify, 必須考慮替換fd.ObjectGUID
            //                if (!ds.Tables[0].Rows[0][1].ToString().Equals(ds2.Tables[0].Rows[0][1].ToString()))
            //                {
            //                    //若原稿的SMWAAAA001跟目前關卡的SMWAAAA001不同, 表示要替換
            //                    sql = "select CURGUID from FORMRELATION where ORIGUID='" + Utility.filter(objectGUID) + "'";
            //                    ds2 = engine.getDataSet(sql, "TEMP");
            //                    objectGUID = ds2.Tables[0].Rows[0][0].ToString();
            //                }
            //            }
            //        }
            //        urlP += "&ObjectGUID=" + objectGUID;
            //        urlP += "&HistoryGUID=" + "";
            //        urlP += "&FlowGUID=" + wi.processSerialNumber;
            //        urlP += "&ACTID=" + "";
            //        urlP += "&PDID=" + wi.processId;
            //        urlP += "&PDVer=" +  "";
            //        urlP += "&ACTName=" + wi.workItemName;
            //        urlP += "&UIStatus=5";
            //        urlP += "&WorkItemOID=" + wi.workItemOID;
            //        urlP += "&GroupSign=Y";
            //        urlP += "&signResult=" + Server.UrlEncode(signResult.ValueText.Split(new string[] { ":::" }, StringSplitOptions.None)[0] + "||" + signResult.ReadOnlyText);
            //        urlP += "&signOpinion=" + Server.UrlEncode(signOpinion.ValueText);
            //        urlP += "&EFLogonID=" + (string)Session["UserID"];

            //        //檢查是否為被代理之流程
            //        StringCollection subsWorkOIDList = (StringCollection)getSession("SUBSLIST");
            //        if (subsWorkOIDList != null && subsWorkOIDList.Contains(wi.workItemOID))
            //        {
            //            urlP += "&isSubs=Y";
            //        }

            //        //取得頁面URL
            //        string url = "";
            //        sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(wi.processId) + "' and SMWDAAA004='" + Utility.filter(wi.workItemName) + "'";
            //        ds = engine.getDataSet(sql, "TEMP");
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            url = ds.Tables[0].Rows[0][0].ToString();
            //        }
            //        else
            //        {
            //            sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(wi.processId) + "' and SMWDAAA006='Default'";
            //            ds = engine.getDataSet(sql, "TEMP");
            //            if (ds.Tables[0].Rows.Count == 0)
            //            {
            //                MessageBox(wi.processName + ":" + wi.workItemName + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " QueryError3", "找不到作業處理畫面. 請洽系統管理員"));
            //                continue;
            //            }
            //            url = ds.Tables[0].Rows[0][0].ToString();
            //        }
            //        url = Page.ResolveUrl("~/" + url);
            //        url = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "httpHead") + "://" + siteName + url;

            //        engine.close();
            //        HTTPProcessor.isSendCookie = false;                                        
            //        string returnString = HTTPProcessor.sendGet(url, urlP);                    
            //        HTTPProcessor.isSendCookie = true;
            //        //string returnString = "aaa";
            //        if (isDebugPage.Equals("Y"))
            //        {
            //            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
            //            fname = Server.MapPath(@"~\LogFolder\" + fname + "_callback.log");
            //            System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);

            //            sw.WriteLine("***************");
            //            sw.WriteLine("GroupSign result:");
            //            //sw.WriteLine(returnString);
            //            sw.Close();
            //        }
            //    }
            //}

            ListTable.UnCheckAllData();
            engine.close();
            MessageBox("完成!請更新清單");
            queryData(false);
            if (noGroupSignProc.Count > 0)
            {
                string proclist = String.Empty;
                string[] proclistary = new string[noGroupSignProc.Count];
                noGroupSignProc.CopyTo(proclistary, 0);
                proclist = String.Join(",\n", proclistary);
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " QueryError4", "以下流程無法進行群簽，請逐一簽核該流程：\n#0#", new string[] { proclist }));
            }
        }
        catch (Exception ue)
        {
            try
            {
                engine.close();
            }
            catch { };
            try
            {
                string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                fname = Server.MapPath(@"~\LogFolder\" + fname + "_callback.log");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);

                sw.WriteLine("***************");
                sw.WriteLine("GroupSign_Click Exception:");
                sw.WriteLine(ue.StackTrace);
                sw.WriteLine(ue.Message);
                sw.Close();
            }
            catch (Exception te)
            {
                MessageBox(te.Message);
            }
            MessageBox(ue.Message);
            queryData(false);
        }
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
    private string mappingUserID(AbstractEngine engine)
    {
        return (string)Session["UserID"];
    }

    private DataSet getSP8FormInBox(AbstractEngine engine,bool isFetchFewItems)
    {
        string userid = mappingUserID(engine);
        string SP8str = GetINDUSConnSTR("EF2KWebDB", engine);//EF DB connection String

        IOFactory factory = new IOFactory();
        AbstractEngine SP8engine = factory.getEngine(EngineConstants.SQL, SP8str);

        string strSQL = "";
        if (isFetchFewItems)
        {
            strSQL += "Select top 1 * from (";
        }
        else
        {
            strSQL += "Select * from (";
        }
        strSQL += "SELECT DISTINCT resda.resda001,resda.resda002,resdd.resdd001, resca.resca002, resdd.resdd002, resdd.resdd003, resdd.resdd004, resdd.resdd005, ";
        strSQL += "resdd.resdd006, resdd.resdd008, resdd.resdd009, resdd.resdd014, resdd.resdd013, resdd.resdd015, resda.resda031, ";
        strSQL += "resda.resda032, resdb.resdb027, resde.resde002, resak1.resak002 AS resda_016, resak2.resak002 AS resda_017, ";
        strSQL += "case when resdd.resdd015 = '5' then '2' when resdd.resdd006 <> '01' then '1' else '0' end as AssignType, ";
        strSQL += "case when resdd.resdd003 = '0000' and resdd007<> N'" + userid + "' and resdd020 = N'" + userid + "' and resdd.resdd015 <> '5' then '128' else '8' end as FormStatus ";
        strSQL += "FROM resdd ";
        strSQL += "LEFT OUTER JOIN resde ON resdd.resdd001 = resde.resde001 AND resdd.resdd002 = resde.resde002 ";
        strSQL += "LEFT OUTER JOIN resda ON resdd.resdd001 = resda.resda001 AND resdd.resdd002 = resda.resda002 ";
        strSQL += "LEFT OUTER JOIN resak AS resak1 ON resda.resda016 = resak1.resak001 ";
        strSQL += "LEFT OUTER JOIN resak AS resak2 ON resda.resda017 = resak2.resak001 ";
        strSQL += "LEFT OUTER JOIN resdb ON resdd.resdd001 = resdb.resdb001 AND resdd.resdd002 = resdb.resdb002 AND resdb.resdb003=resdd.resdd003 AND resdb.resdb004=resdd.resdd004 ";
        strSQL += "LEFT OUTER JOIN resca ON resdd.resdd001 = resca.resca001 ";
        strSQL += "WHERE (resdd.resdd007 = N'" + userid + "' OR resdd.resdd020 = N'" + userid + "') ";
        //strSQL += "AND (resdd.resdd019 = N'Y') AND (resdd.resdd003 <> N'0000') ";
        strSQL += "AND (resdd.resdd019 = N'Y') ";
        strSQL += "AND (resdd.resdd015 IN (N'1',N'5')) ) aa where 1=1 ";
        //strSQL += "AND (resdd.resdd015 IN (N'1',N'5')) ) aa where 1=2 ";

        if (!AssignType.ValueText.Equals(""))
        {
            strSQL += " AND AssignType ='" + AssignType.ValueText + "' ";
        }
        if (ViewTimes.ValueText.Equals("U"))
        {
            strSQL += " AND resdd013 ='0' ";
        }
        else if (ViewTimes.ValueText.Equals("R"))
        {
            strSQL += " AND resdd013 <>'0' ";
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
        DataSet ds = SP8engine.getDataSet(strSQL, "TEMP");

        SP8engine.close();
        return ds;
    }

    private string GetSomethingID(string something)
    {
        if (something != "")
        {
            string sID = something.Split(new char[] { '^' })[0];
            return sID;
        }
        else
            return "";
    }

    /// <summary>
    /// 取得EF通知資料匣
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    private DataSet getSP8FormInfoBox(AbstractEngine engine)
    {
        string userid = mappingUserID(engine);
        string SP8str = GetINDUSConnSTR("EF2KWebDB", engine);//EF DB connection String

        IOFactory factory = new IOFactory();
        AbstractEngine SP8engine = factory.getEngine(EngineConstants.SQL, SP8str);

        string strSQL = "";

        #region //EF通知
        //strSQL += "SELECT newid() AS GUID, ";
        //strSQL += "SELECT resdd001 + '-' +resdd002 + '-' +resdd003 + '-' +resdd004 + '-' +resdd005 AS GUID, ";
        strSQL += "SELECT resdd001 + '-' + resdd002 AS GUID, resda002,";
        strSQL += "resdd001, resdd002, resdd003, resdd004, resdd005, resdd006, ";
        strSQL += "case resda021 when '1' then '1' when '2'  then '3' when '3'  then '5' when '4'  then '4' end AS CURRENTSTATE, resca002 AS PROCESSNAME, resdd002 AS SHEETNO, ";
        strSQL += "CASE resda032 WHEN '0' THEN '低' WHEN '1' THEN '普通' ELSE '高' END AS IMPORTANT, ";
        strSQL += "case when resde002 is null THEN '' ELSE '{[font color=red]}！{[/font]}' END AS ATTACH, ";
        strSQL += "'' AS WORKITEMNAME, resda031 AS SUBJECT, '' AS USERNAME, resdd009 AS CREATETIME, '系統自動通知' AS WORKTYPE, ";
        strSQL += "AssignType AS ASSIGNMENTTYPE, '' AS REASSIGNMENTTYPE, resdd013 AS VIEWTIMES, '0' AS WORKASSIGNMENT, 'SYSTEM' AS D_INSERTUSER, ";
        strSQL += "GETDATE() AS D_INSERTTIME, '' AS D_MODIFYUSER, '' AS D_MODIFYTIME, '16' AS FormStatus ";
        strSQL += "FROM (SELECT DISTINCT resdd.resdd001, resca.resca002, resdd.resdd002, resdd.resdd003, resdd.resdd004, ";
        strSQL += "resdd.resdd005, resdd.resdd006, resdd.resdd008, resdd.resdd009, resdd.resdd014, resdd.resdd013, ";
        strSQL += "resdd.resdd015, resda.resda021,resda.resda031, resda.resda032, resde.resde002, resda.resda002,'0' AS AssignType ";
        strSQL += "FROM resdd ";
        strSQL += "LEFT OUTER JOIN resde ON resdd.resdd001 = resde.resde001 AND resdd.resdd002 = resde.resde002 ";
        strSQL += "LEFT OUTER JOIN resda ON resdd.resdd001 = resda.resda001 AND resdd.resdd002 = resda.resda002 ";
        strSQL += "LEFT OUTER JOIN resca ON resdd.resdd001 = resca.resca001 ";
        strSQL += "WHERE (resdd.resdd007 = N'" + userid + "' OR resdd.resdd020 = N'" + userid + "') ";
        strSQL += "AND (resdd.resdd019 = N'Y') AND (resdd.resdd018 = 16) ";
        
        if (ViewTimes.ValueText.Equals("U"))
        {
            strSQL += " AND resdd013 ='0' ";
        }
        else if (ViewTimes.ValueText.Equals("R"))
        {
            strSQL += " AND resdd013 <>'0' ";
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
        strSQL += ") NtDt ";
        if (!AssignType.ValueText.Equals(""))
        {
            strSQL += " Where AssignType ='" + AssignType.ValueText + "' ";
        }
        //strSQL += "ORDER BY resdd009 DESC";
        #endregion

        #region //回函
        /*
        strSQL += "Select resda001 +'-' +resda002 as GUID,resda002,'0' as CURRENTSTATE,resca002 as PROCESSNAME,resda002 as SHEETNO,";
        strSQL += " case resda032 when '0' then '低' when '1' then '普通' else '高' end as IMPORTANT,";
        strSQL += " case resde002 when '' then '' else '{[font color=red]}！{[/font]}' end as ATTACH,";
        strSQL += " '' as WORKITEMNAME,resda031 as SUBJECT,'' as USERNAME,resda018 as CREATETIME,";
        strSQL += "'' as WORKTYPE,AssignType as ASSIGNMENTTYPE,'' as REASSIGNMENTTYPE, '0' as VIEWTIMES,'0' as WORKASSIGNMENT,";
        strSQL += "'SYSTEM' as D_INSERTUSER,getdate() as D_INSERTTIME,'' as D_MODIFYUSER,'' as D_MODIFYTIME, '4' as FormStatus from (";

        strSQL += "select distinct resda.resda001, resda.resda002, resda.resda015, resda.resda016, ";
        strSQL += "resda.resda018, resda.resda019, resda.resda020, resda.resda021, resca.resca002, ";
        strSQL += "resda.resda031, resda.resda032, resda.resda033, resda.resda035, resda.resda041, resde.resde002,'0' as AssignType ";
        strSQL += "from resda  ";
        strSQL += "left outer join resde on resda.resda001 = resde.resde001 and resda.resda002 = resde.resde002 ";
        strSQL += "left outer join resca on resda.resda001 = resca.resca001 ";
        strSQL += "LEFT OUTER JOIN resdd ON resda.resda001 = resdd.resdd001 and resda.resda002 = resdd.resdd002 ";
        strSQL += "Where (resda.resda016 = N'" + userid + "') and (resda.resda034 = N'Y') ";

        if (ViewTimes.ValueText.Equals("U"))
        {
            strSQL += " AND VIEWTIMES ='0' ";
        }
        else if (ViewTimes.ValueText.Equals("R"))
        {
            strSQL += " AND VIEWTIMES <>'0' ";
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
        strSQL += ") ReDt ";
        if (!AssignType.ValueText.Equals(""))
        {
            strSQL += " Where AssignType ='" + AssignType.ValueText + "' ";
        }
        //strSQL += " ORDER BY resda.resda018 DESC";
        */
        #endregion

        #region //逐級通知
        /*
        strSQL += "Union all ";
        strSQL += "Select resdf002 +'-' +resdf003 as GUID,'0' as CURRENTSTATE,resca002 as PROCESSNAME,resdf003 as SHEETNO,";
        strSQL += " case resda032 when '0' then '低' when '1' then '普通' else '高' end as IMPORTANT,";
        strSQL += " case resde002 when '' then '' else '{[font color=red]}！{[/font]}' end as ATTACH,";
        strSQL += " '' as WORKITEMNAME,resda031 as SUBJECT,'' as USERNAME,resdf009 as CREATETIME,";
        strSQL += "'' as WORKTYPE,AssignType as ASSIGNMENTTYPE,'' as REASSIGNMENTTYPE, '0' as VIEWTIMES,'0' as WORKASSIGNMENT,";
        strSQL += "'SYSTEM' as D_INSERTUSER,getdate() as D_INSERTTIME,'' as D_MODIFYUSER,'' as D_MODIFYTIME, '512' as FormStatus from (";
        strSQL += "select distinct resdf.resdf001, resca.resca002, resdf.resdf002, resdf.resdf003, resdf.resdf004, resdf.resdf005, resdf.resdf006, resdf.resdf007, resdf.resdf008, resdf.resdf009, cast(resdf.resdf010 as nvarchar(255)) as resdf010, resdf.resdf011, resda.resda020, resda.resda021, resda.resda031, resda.resda032, resde.resde002, resak.resak002 ,'0' as AssignType ";
        strSQL += "FROM resdf ";
        strSQL += "LEFT OUTER JOIN resde ON resdf.resdf002 = resde.resde001 AND resdf.resdf003 = resde.resde002 ";
        strSQL += "LEFT OUTER JOIN resda ON resdf.resdf002 = resda.resda001 AND resdf.resdf003 = resda.resda002 ";
        strSQL += "LEFT OUTER JOIN resak ON resda.resda016 = resak.resak001 ";
        strSQL += "LEFT OUTER JOIN resca ON resdf.resdf002 = resca.resca001 ";
        strSQL += "LEFT OUTER JOIN resdd ON resdf.resdf002 = resdd.resdd001 and resdf.resdf003 = resdd.resdd002 ";
        strSQL += "WHERE (resdf.resdf001 = N'" + userid + "') ";

        if (ViewTimes.ValueText.Equals("U"))
        {
            strSQL += " AND VIEWTIMES ='0' ";
        }
        else if (ViewTimes.ValueText.Equals("R"))
        {
            strSQL += " AND VIEWTIMES <>'0' ";
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
        strSQL += ") NtDt ";
        if (!AssignType.ValueText.Equals(""))
        {
            strSQL += " Where AssignType ='" + AssignType.ValueText + "' ";
        }
        //strSQL += " ORDER BY resdf.resdf009 DESC";
        */
        #endregion

        #region //轉寄

        strSQL += "Union all ";
        strSQL += "Select resdh002 +'-' +resdh003 as GUID,resda002,'' as resdd001,'' as resdd002,'' as resdd003,'' as resdd004,'' as resdd005,'' as resdd006,case resda021 when '1' then '1' when '2'  then '3' when '3'  then '5' when '4'  then '4' end as CURRENTSTATE,resca002 as PROCESSNAME,resdh003 as SHEETNO,";
        strSQL += " case resda032 when '0' then '低' when '1' then '普通' else '高' end as IMPORTANT,";
        strSQL += " case when resde002 is null then '' else '{[font color=red]}！{[/font]}' end as ATTACH,";
        strSQL += " '' as WORKITEMNAME,resda031 as SUBJECT,'' as USERNAME,resdh004 as CREATETIME,";
        strSQL += "'私人轉寄' as WORKTYPE,AssignType as ASSIGNMENTTYPE,'' as REASSIGNMENTTYPE, case resdh005 when 'N' then '0' when 'Y' then '1' end as VIEWTIMES,'1' as WORKASSIGNMENT,";
        strSQL += "'SYSTEM' as D_INSERTUSER,getdate() as D_INSERTTIME,'' as D_MODIFYUSER,'' as D_MODIFYTIME, '32' as FormStatus from (";
        strSQL += "select distinct resdh.resdh001, resca.resca002, resdh.resdh002, resdh.resdh003, resdh.resdh004, resdh.resdh005, resdh.resdh006,resda.resda002, resda.resda020, resda.resda021, resda.resda031, resda.resda032, resde.resde002, resak.resak002, '1' as AssignType ";
        strSQL += "FROM resdh  ";
        strSQL += "LEFT OUTER JOIN resde ON resdh.resdh002 = resde.resde001 AND resdh.resdh003 = resde.resde002  ";
        strSQL += "LEFT OUTER JOIN resda ON resdh.resdh002 = resda.resda001 AND resdh.resdh003 = resda.resda002  ";
        strSQL += "LEFT OUTER JOIN resak ON resdh.resdh006 = resak.resak001 LEFT OUTER JOIN resca ON resdh.resdh002 = resca.resca001  ";
        //strSQL += "LEFT OUTER JOIN resdd ON resdh.resdh002 = resdd.resdd001 and resdh.resdh003 = resdd.resdd002  ";
        strSQL += "WHERE (resdh.resdh001 = N'" + userid + "')  ";

        if (ViewTimes.ValueText.Equals("U"))
        {
            strSQL += " AND resdh005 ='N' ";
        }
        else if (ViewTimes.ValueText.Equals("R"))
        {
            strSQL += " AND resdh005 <>'Y' ";
        }
        if (!StartTime.ValueText.Equals(""))
        {
            strSQL += " AND left(resda018,10)>='" + StartTime.ValueText + "' ";
        }
        if (!EndTime.ValueText.Equals(""))
        {
            strSQL += " AND left(resda018,10)<='" + EndTime.ValueText + "' ";
        }
        if (!ProcessIDList.ValueText.Equals(""))
        {
            strSQL += " AND resda001='" + ProcessIDList.ValueText + "' ";
        }
        if (!Subject.ValueText.Equals(""))
        {
            strSQL += " AND resda031 like '%" + Subject.ValueText + "%' ";
        }
        strSQL += ") fwDt ";
        if (!AssignType.ValueText.Equals(""))
        {
            strSQL += " Where AssignType ='" + AssignType.ValueText + "' ";
        }




        //strSQL += " ORDER BY resdh.resdh004 DESC";

        #endregion

        #region //抽單或撤簽(填表人)
        strSQL += "Union all ";
        strSQL += "Select resda001 +'-' +resda002 as GUID,resda002,'' as resdd001,'' as resdd002,'' as resdd003,'' as resdd004,'' as resdd005,'' as resdd006,'4' as CURRENTSTATE,resca002 as PROCESSNAME,resda002 as SHEETNO,";
        strSQL += " case resda032 when '0' then '低' when '1' then '普通' else '高' end as IMPORTANT,";
        strSQL += " case when resde002 is null then '' else '{[font color=red]}！{[/font]}' end as ATTACH,";
        strSQL += " '' as WORKITEMNAME,resda031 as SUBJECT,'' as USERNAME,resda018 as CREATETIME,";
        strSQL += "'流程被撤銷或工作被回收' as WORKTYPE,AssignType as ASSIGNMENTTYPE,'' as REASSIGNMENTTYPE, '0' as VIEWTIMES,'4' as WORKASSIGNMENT,";
        strSQL += "'SYSTEM' as D_INSERTUSER,getdate() as D_INSERTTIME,'' as D_MODIFYUSER,'' as D_MODIFYTIME, '4' as FormStatus from (";

        strSQL += "select distinct resda.resda001, resda.resda002, resda.resda015, resda.resda016, ";
        strSQL += "resda.resda018, resda.resda019, resda.resda020, resda.resda021, resca.resca002, ";
        strSQL += "resda.resda031, resda.resda032, resda.resda033, resda.resda035, resda.resda041, resde.resde002,'4' as AssignType ";
        strSQL += "from resda  ";
        strSQL += "left outer join resde on resda.resda001 = resde.resde001 and resda.resda002 = resde.resde002 ";
        strSQL += "left outer join resca on resda.resda001 = resca.resca001 ";
        //strSQL += "LEFT OUTER JOIN resdd ON resda.resda001 = resdd.resdd001 and resda.resda002 = resdd.resdd002 ";
        strSQL += "Where (resda.resda016 = N'" + userid + "') and (resda.resda021 = '4') ";

        if (ViewTimes.ValueText.Equals("R"))
        {
            strSQL += " AND (1=2)";
        }
        if (!StartTime.ValueText.Equals(""))
        {
            strSQL += " AND left(resda018,10)>='" + StartTime.ValueText + "' ";
        }
        if (!EndTime.ValueText.Equals(""))
        {
            strSQL += " AND left(resda018,10)<='" + EndTime.ValueText + "' ";
        }
        if (!ProcessIDList.ValueText.Equals(""))
        {
            strSQL += " AND resda001='" + ProcessIDList.ValueText + "' ";
        }
        if (!Subject.ValueText.Equals(""))
        {
            strSQL += " AND resda031 like '%" + Subject.ValueText + "%' ";
        }
        strSQL += ") ReDt ";
        if (!AssignType.ValueText.Equals(""))
        {
            strSQL += " Where AssignType ='" + AssignType.ValueText + "' ";
        }
        //strSQL += " ORDER BY resda.resda018 DESC";

        #endregion




        DataSet ds = new DataSet();
        //ds.EnforceConstraints = false;
        //ds.EnforceConstraints = true;

        //ds.Tables.Add();
        ds = SP8engine.getDataSet(strSQL, "TEMP");

        SP8engine.close();
        return ds;
    }

    protected void WriteLOGLine(string value)
    {
        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
        fname = Server.MapPath("~/LogFolder/" + fname + "_SMWL_Log.log");
        System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
        sw.WriteLine((string)Session["UserID"] + " " + System.DateTime.Now.ToString("yyyy/mm/dd hh:mm:ss.fff") + ":" + value);
        sw.Close();
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
        ArrayList ary = (ArrayList)getSession("init_ORDER");
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
    private void changePage(int pageNum)
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


            WorkItem[] wi = null;

            string BoxQueryType = (string)getSession("BoxQueryType");

            wi = (WorkItem[])getSession("init_WI");
            DataSet dsSP8 = (DataSet)getSession("init_DSSP8");
            DataSet dsNotice = (DataSet)getSession("init_DSNOTICE");
            ArrayList arys=(ArrayList)getSession("init_ORDER");

            //Amos
            if(arys.Count>0)
            //if ((wi.Length > 0) || (dsSP8.Tables[0].Rows.Count > 0) || (dsNotice.Tables[0].Rows.Count > 0))
            {

                //檢查是否為單一流程單一角色
                string checkProcessId = "";
                string checkWorkItemName = "";
                bool isSame = true;
                for (int i = 0; i < wi.Length; i++)
                {
                    if (checkProcessId.Equals(""))
                    {
                        checkProcessId = wi[i].processName;
                        checkWorkItemName = wi[i].workItemName;
                    }
                    else
                    {
                        string checkerId = wi[i].processName;
                        string checkerWN = wi[i].workItemName;
                        if ((!checkerId.Equals(checkProcessId)) || (!checkerWN.Equals(checkWorkItemName)))
                        {
                            isSame = false;
                            break;
                        }
                    }
                }

                //取得單號
                string taging = "'*'";
                for (int i = 0; i < wi.Length; i++)
                {
                    taging += ",'" + Utility.filter(wi[i].processSerialNumber) + "'";
                }
                sql = "select SMWYAAA002, SMWYAAA005, SMWYAAA019, SMWYAAA007 from SMWYAAA where SMWYAAA005 in (" + taging + ") union select SMWYAAA002, FLOWGUID as SMWYAAA005, SMWYAAA019, SMWYAAA007 from SMWYAAA inner join FORMRELATION on ORIGUID=SMWYAAA019 where FLOWGUID in (" + taging + ")";
                DataSet sh = engine.getDataSet(sql, "TEMP");

                sql = "select SMWYAAA005, count(*) from SMWYAAA inner join FILEITEM on SMWYAAA019=JOBID group by SMWYAAA005";
                DataSet att = engine.getDataSet(sql, "TEMP");

                string qstr = "";



                DataObjectSet dos = ListTable.dataSource;
                dos.clear();
                setSession("CURRENTPAGE", pageNum);
                CurrentPage.ValueText = ((int)getSession("CURRENTPAGE")).ToString();

                //MessageBox("Edward");
                //for(int i=0;i<10;i++)

                int start = ListTable.getPageSize() * (pageNum - 1);
                int end = start + ListTable.getPageSize();
                if (end >arys.Count ) end = arys.Count;
                if (true)
                {
                    for (int i = start; i < end; i++)
                    {
                        DataObject ddo = dos.create();
                        object ob = arys[i];
                        //MessageBox(ob.GetType().Name);
                        if (ob.GetType().Name.Equals("WorkItem"))
                        {
                            WorkItem wis = (WorkItem)ob;
                            //國昌20100614:自訂的通知（原稿發起）不過濾
                            if (!wis.workItemOID.Equals(""))
                            {
                                ddo.setData("GUID", wis.workItemOID);
                            }
                            else
                            {
                                ddo.setData("GUID", IDProcessor.getID(""));
                            }
                            ddo.setData("CURRENTSTATE", wis.currentState);
                            ddo.setData("PROCESSNAME", wis.processName);

                            string objG = "";
                            for (int j = 0; j < sh.Tables[0].Rows.Count; j++)
                            {
                                if (wis.processSerialNumber.Equals(sh.Tables[0].Rows[j][1].ToString()))
                                {
                                    ddo.setData("SHEETNO", sh.Tables[0].Rows[j][0].ToString());
                                    objG = sh.Tables[0].Rows[j][2].ToString();
                                    ddo.setData("IMPORTANT", sh.Tables[0].Rows[j][3].ToString());
                                }
                            }
                            bool hasAtt = false;
                            for (int j = 0; j < att.Tables[0].Rows.Count; j++)
                            {
                                if (wis.processSerialNumber.Equals(att.Tables[0].Rows[j][0].ToString()))
                                {
                                    hasAtt = true;
                                    break;
                                }
                            }
                            if (hasAtt)
                            {
                                ddo.setData("ATTACH", "{[font color=red]}！{[/font]}");
                            }
                            else
                            {
                                ddo.setData("ATTACH", "");
                            }
                            ddo.setData("WORKITEMNAME", wis.workItemName);
                            ddo.setData("SUBJECT", wis.subject);
                            ddo.setData("USERNAME", wis.requesterName);
                            ddo.setData("CREATETIME", wis.createdTime);
                            if (BoxQueryType.Equals("W"))
                            {
                                if (wis.assignmentType.Length == 1)
                                {
                                    if (wis.assignmentType.Equals("0"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids2", "正常指派"));
                                    }
                                    else if (wis.assignmentType.Equals("1"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids3", "他人重新指派(退回重辦)"));
                                    }
                                    else
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids4", "抽回重辦工作"));
                                    }

                                    ddo.setData("ASSIGNMENTTYPE", wis.assignmentType);
                                    ddo.setData("REASSIGNMENTTYPE", "");
                                }
                                else
                                {
                                    string ast = wis.assignmentType.Substring(1, 1);
                                    if (ast.Equals("0"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids13", "代理轉派"));
                                    }
                                    else if (ast.Equals("1"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids14", "系統代理人轉派"));
                                    }
                                    else if (ast.Equals("2"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids15", "系統逾時轉派"));
                                    }
                                    else if (ast.Equals("3"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids16", "管理員代理轉派"));
                                    }
                                    else if (ast.Equals("4"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids17", "負責人代理轉派"));
                                    }
                                    else if (ast.Equals("5"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids18", "工作取回"));
                                    }
                                    else if (ast.Equals("6"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids19", "工作轉派"));
                                    }
                                    else if (ast.Equals("7"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids20", "管理員工作轉派"));
                                    }
                                    else if (ast.Equals("8"))
                                    {
                                        ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids21", "負責人工作轉派"));
                                    }
                                    ddo.setData("ASSIGNMENTTYPE", "0");
                                    ddo.setData("REASSIGNMENTTYPE", ast);
                                }
                            }
                            else
                            {
                                if (wis.noticeType.Equals("0"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids5", "系統自動通知"));
                                }
                                else if (wis.noticeType.Equals("1"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids6", "私人轉寄"));
                                }
                                else if (wis.noticeType.Equals("2"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids7", "一般轉寄, 顯示在流程追蹤"));
                                }
                                else if (wis.noticeType.Equals("3"))
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids8", "被代理通知"));
                                }
                                else
                                {
                                    ddo.setData("WORKTYPE", com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids9", "流程被撤銷或工作被回收"));
                                }
                            }
                            ddo.setData("VIEWTIMES", wis.viewTimes.ToString());
                            ddo.setData("WORKASSIGNMENT", wis.workAssignmentOID);
                            ddo.setData("D_INSERTUSER", "SYSTEM");
                            ddo.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                            ddo.setData("D_MODIFYUSER", "");
                            ddo.setData("D_MODIFYTIME", "");

                            ddo.Tag = wis;
                            dos.addDraft(ddo);
                        }
                        else
                        {
                            if (BoxQueryType.Equals("W"))
                            {
                                DataObject ddot = dos.create();

                                DataRow ddr = (DataRow)ob;
                                ddot.setData("GUID", ddr["resda001"].ToString() + "-" + ddr["resda002"].ToString());
                                //if (i < 10)
                                if(true)
                                {
                                    ddot.setData("CURRENTSTATE", "0");
                                    ddot.setData("PROCESSNAME", "\t" + ddr["resca002"].ToString());
                                    ddot.setData("SHEETNO", ddr["resda002"].ToString());
                                    if (ddr["resda032"].ToString().Equals("0"))
                                    {
                                        ddot.setData("IMPORTANT", "低");
                                    }
                                    else if (ddr["resda032"].ToString().Equals("1"))
                                    {
                                        ddot.setData("IMPORTANT", "普通");
                                    }
                                    else
                                    {
                                        ddot.setData("IMPORTANT", "高");
                                    }
                                    if (ddr["resde002"].ToString() != "")
                                    {
                                        ddot.setData("ATTACH", "{[font color=red]}！{[/font]}");
                                    }
                                    else
                                    {
                                        ddot.setData("ATTACH", "");
                                    }
                                    //ddot.setData("WORKITEMNAME", "");
                                    ddot.setData("SUBJECT", ddr["resda031"].ToString());
                                    ddot.setData("USERNAME", ddr["resda_017"].ToString());
                                    ddot.setData("CREATETIME", ddr["resdd009"].ToString());
                                    //ddot.setData("WORKTYPE", "");
                                    ddot.setData("ASSIGNMENTTYPE", ddr["AssignType"].ToString());
                                    //ddot.setData("REASSIGNMENTTYPE", "");
                                    ddot.setData("VIEWTIMES", ddr["resdd013"].ToString());
                                    //ddot.setData("WORKASSIGNMENT", "");
                                    //ddot.setData("D_INSERTUSER", "SYSTEM");
                                    //ddot.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                                    //ddot.setData("D_MODIFYUSER", "");
                                    //ddot.setData("D_MODIFYTIME", "");
                                }
                                ddot.Tag = ddr;

                                dos.addDraft(ddot);
                            }
                            else
                            {
                                DataObject fdo = dos.create();
                                DataRow ddr = (DataRow)ob;

                                fdo.setData("GUID", ddr["GUID"].ToString());
                                fdo.setData("CURRENTSTATE", ddr["CURRENTSTATE"].ToString());
                                fdo.setData("PROCESSNAME", "\t" + ddr["PROCESSNAME"].ToString());
                                fdo.setData("SHEETNO", ddr["SHEETNO"].ToString());
                                fdo.setData("IMPORTANT", ddr["IMPORTANT"].ToString());
                                fdo.setData("ATTACH", ddr["ATTACH"].ToString());
                                fdo.setData("WORKITEMNAME", ddr["WORKITEMNAME"].ToString());
                                fdo.setData("SUBJECT", ddr["SUBJECT"].ToString());
                                fdo.setData("USERNAME", ddr["USERNAME"].ToString());
                                fdo.setData("CREATETIME", ddr["CREATETIME"].ToString());
                                fdo.setData("WORKTYPE", ddr["WORKTYPE"].ToString());
                                fdo.setData("ASSIGNMENTTYPE", ddr["ASSIGNMENTTYPE"].ToString());
                                fdo.setData("REASSIGNMENTTYPE", ddr["REASSIGNMENTTYPE"].ToString());
                                fdo.setData("VIEWTIMES", ddr["VIEWTIMES"].ToString());
                                fdo.setData("WORKASSIGNMENT", ddr["WORKASSIGNMENT"].ToString());
                                fdo.setData("D_INSERTUSER", ddr["D_INSERTUSER"].ToString());
                                fdo.setData("D_INSERTTIME", ddr["D_INSERTTIME"].ToString());
                                fdo.setData("D_MODIFYUSER", ddr["D_MODIFYUSER"].ToString());
                                fdo.setData("D_MODIFYTIME", ddr["D_MODIFYTIME"].ToString());
                                fdo.Tag = ddr;
                                dos.addDraft(fdo);
                            }
                        }
                    }
                    //Amos
                }

                ListTable.IsGeneralUse = false;
                ListTable.InputForm = "Detail.aspx";
                ListTable.CurPageUID = PageUniqueID;
                ArrayList hFields = (ArrayList)getSession("HiddenFields");
                string[] hf = new string[hFields.Count + 5];
                hf[0] = "GUID";
                hf[1] = "WORKASSIGNMENT";
                hf[2] = "ASSIGNMENTTYPE";
                hf[3] = "REASSIGNMENTTYPE";
                hf[4] = "WORKTYPE";
                for (int i = 0; i < hFields.Count; i++)
                {
                    hf[i + 5] = (string)hFields[i];
                }
                ListTable.HiddenField = hf;
                ListTable.WidthMode = 1;
                ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                String[,] orderby = new string[,] { { "CREATETIME", DataObjectConstants.DESC } };
                //dos.sort(orderby);
                ListTable.dataSource = dos;
                string[] fieldOrder = (string[])getSession("fieldOrder");
                ListTable.reOrderField(fieldOrder);
                //ListTable.setCurrentPage(pageNum);
                ListTable.updateTable();
                //ListTable.setCurrentPage(pageNum);
                //代理人處理機制
                if (BoxQueryType.Equals("W") && isSubstitute.Equals("Y"))
                {
                    //篩選代理清單
                    StringCollection filteredSB = new StringCollection();
                    StringCollection allSB = (StringCollection)getSession("SUBSLIST");
                    for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
                    {
                        //Amos ECP的表單才執行
                        if (dos.getAvailableDataObject(i).Tag is com.dsc.flow.data.WorkItem)
                        {
                            //紀錄代理的流程工作項目OID
                            if (allSB.Contains(((WorkItem)dos.getAvailableDataObject(i).Tag).workItemOID))
                            {
                                filteredSB.Add(((WorkItem)dos.getAvailableDataObject(i).Tag).workItemOID);
                            }
                        }
                    }
                    setSession("SUBSLIST", filteredSB);
                }
                setSession("CURLIST", dos);
            }
            else
            {
                string qstr = "select GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME from SMWL";
                DataObjectSet dos = new DataObjectSet();
                StringBuilder sbddd = new StringBuilder();
                sbddd.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sbddd.AppendLine("<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">");
                sbddd.AppendLine("<queryStr>" + qstr + "</queryStr>");
                sbddd.AppendLine("  <isCheckTimeStamp>true</isCheckTimeStamp>");
                sbddd.AppendLine("  <fieldDefinition>");

                sbddd.AppendLine("    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"重要性\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>");
                sbddd.AppendLine("    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程名稱\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"CREATETIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"WORKTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作性質\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"VIEWTIMES\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"讀取次數\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"WORKASSIGNMENT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作指派識別\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"ASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作類型\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"REASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"轉派類型\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>");
                sbddd.AppendLine("    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>");

                sbddd.AppendLine("  </fieldDefinition>");
                sbddd.AppendLine("  <identityField>");
                sbddd.AppendLine("    <field dataField=\"GUID\"/>");
                sbddd.AppendLine("  </identityField>");
                sbddd.AppendLine("  <keyField>");
                sbddd.AppendLine("    <field dataField=\"GUID\"/>");
                sbddd.AppendLine("  </keyField>");
                sbddd.AppendLine("  <allowEmptyField>");

                sbddd.AppendLine("    <field dataField=\"GUID\"/>");
                sbddd.AppendLine("    <field dataField=\"ATTACH\"/>");
                sbddd.AppendLine("    <field dataField=\"IMPORTANT\"/>");
                sbddd.AppendLine("    <field dataField=\"CURRENTSTATE\"/>");
                sbddd.AppendLine("    <field dataField=\"PROCESSNAME\"/>");
                sbddd.AppendLine("    <field dataField=\"SHEETNO\"/>");
                sbddd.AppendLine("    <field dataField=\"WORKITEMNAME\"/>");
                sbddd.AppendLine("    <field dataField=\"SUBJECT\"/>");
                sbddd.AppendLine("    <field dataField=\"USERNAME\"/>");
                sbddd.AppendLine("    <field dataField=\"CREATETIME\"/>");
                sbddd.AppendLine("    <field dataField=\"WORKTYPE\"/>");
                sbddd.AppendLine("    <field dataField=\"VIEWTIMES\"/>");
                sbddd.AppendLine("    <field dataField=\"WORKASSIGNMENT\"/>");
                sbddd.AppendLine("    <field dataField=\"ASSIGNMENTTYPE\"/>");
                sbddd.AppendLine("    <field dataField=\"REASSIGNMENTTYPE\"/>");
                sbddd.AppendLine("    <field dataField=\"D_INSERTUSER\"/>");
                sbddd.AppendLine("    <field dataField=\"D_INSERTTIME\"/>");
                sbddd.AppendLine("    <field dataField=\"D_MODIFYUSER\"/>");
                sbddd.AppendLine("    <field dataField=\"D_MODIFYTIME\"/>");

                sbddd.AppendLine("  </allowEmptyField>");
                sbddd.AppendLine("  <nonUpdateField>");
                sbddd.AppendLine("  </nonUpdateField>");
                sbddd.AppendLine("</DataObject>");
                dos.dataObjectSchema = sbddd.ToString();

                dos.isNameLess = true;

                ListTable.IsGeneralUse = false;
                ListTable.WidthMode = 1;
                ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                ListTable.InputForm = "Detail.aspx";
                ListTable.CurPageUID = PageUniqueID;
                ListTable.HiddenField = new string[] { "GUID", "WORKASSIGNMENT", "ASSIGNMENTTYPE", "REASSIGNMENTTYPE" };
                String[,] orderby = new string[,] { { "CREATETIME", DataObjectConstants.DESC } };
                dos.sort(orderby);
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
            //WriteLOGLine(te);
        }

        //MessageBox("已取得清單");

    }
    protected void ListTable_ShowAllPageClick()
    {
        ListTable.isShowAll = false;
        ArrayList ary = (ArrayList)getSession("init_ORDER");
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
        ArrayList ary = (ArrayList)getSession("init_ORDER");
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
