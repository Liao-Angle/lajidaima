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
using WebServerProject.flow.SMWY;
using WebServerProject.flow.SMWG;
using WebServerProject;
using DSCWebControl;

public partial class smpEasyFlow_system_Info_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        //========修改的部分==========
        maintainIdentity = "smpEFInfo";
        ApplicationID = "SYSTEM";
        ModuleID = "smpEasyFlow";
        //========修改的部分==========

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {

                StartTime.ValueText = DateTime.Now.AddMonths(-1).ToShortDateString();

                EndTime.ValueText = DateTime.Now.ToShortDateString();

                ListTable.CurPanelID = CurPanelID;

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "";
                string[,] ids;

                //if (ds.Tables[0].Rows[0]["SMWIAAA007"].ToString().Equals("W"))
                if(false)
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

                }
                else
                {
                    //通知
                    setSession("BoxQueryType", "N");
                    
                    ids = new string[,]{
                        {"",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids1", "不限定")},
                        {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids5", "系統自動通知")},
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids6", "私人轉寄")},
                        {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids7", "一般轉寄, 顯示在流程追蹤")},
                        {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids8", "被代理通知")},
                        {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids9", "流程被撤銷或工作被回收")}
                    };
                    AssignType.setListItem(ids);

                }
                //得取次數過濾
                ids = new string[,]{
                    {"A",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids10", "全部")},
                    {"U",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids11", "未讀取")},
                    {"R",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids12", "已讀取")}
                };
                ViewTimes.setListItem(ids);

                //取得顯示欄位
                ArrayList hFields=new ArrayList();
                /*
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
                */
                setSession("HiddenFields", hFields);

                //取得流程定義條件
                string processIDS = "";
                /*
                if (ds.Tables[0].Rows[0]["SMWIAAA004"].ToString().Equals("0"))
                {
                    sql = "select SMWBAAA003 from SMWBAAA where SMWBAAA003 not in (select SMWIAAB003 from SMWIAAB where SMWIAAB002='" + Utility.filter(ds.Tables[0].Rows[0]["SMWIAAA001"].ToString()) + "')";
                }
                else
                {
                    sql = "select SMWIAAB003 from SMWIAAB where SMWIAAB002='" + Utility.filter(ds.Tables[0].Rows[0]["SMWIAAA001"].ToString()) + "'";
                }
                */
                sql = "select SMWBAAA003 from SMWBAAA";
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

                SysParam sp = new SysParam(engine);
                string sp7 = sp.getParam("EF2KWebDB");

                AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7);
                DataSet sp7ds = sp7engine.getDataSet("select resca001, resca002 from resca where resca086='2'", "TEMP");
                sp7engine.close();

                ids = new string[ddd.Tables[0].Rows.Count + 1+sp7ds.Tables[0].Rows.Count, 2];
                ids[0, 0] = "";
                ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " idsA", "不限定");
                for (int i = 0; i < ddd.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1, 0] = ddd.Tables[0].Rows[i][0].ToString();
                    ids[i + 1, 1] = ddd.Tables[0].Rows[i][1].ToString();
                }
                for (int i = 0; i < sp7ds.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1 + ddd.Tables[0].Rows.Count, 0] = sp7ds.Tables[0].Rows[i][0].ToString();
                    ids[i + 1 + ddd.Tables[0].Rows.Count, 1] = "(SP7)"+sp7ds.Tables[0].Rows[i][1].ToString();
                }
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
                /*
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
                */
                setSession("denyWorkItems", "");
                
                engine.close();

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
            string sql = "";

            SysParam sp = new SysParam(engine);
            string flowType = sp.getParam("FlowAdapter");
            string con1 = sp.getParam("NaNaWebService");
            string con2 = sp.getParam("DotJWebService");
            string account = sp.getParam("FlowAccount");
            string password = sp.getParam("FlowPassword");

            FlowFactory ff = new FlowFactory();
            AbstractFlowAdapter adp = ff.getAdapter(flowType);
            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
            fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

            adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

            WorkItem[] wi = null;

            string BoxQueryType = (string)getSession("BoxQueryType");

            if (BoxQueryType.Equals("W"))
            {
                wi = adp.fetchPerformableWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, ProcessIDList.ValueText);
            }
            else
            {
                wi = adp.fetchNoticeWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, ProcessIDList.ValueText);
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
                    /*
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
                    */
                }
                else
                {
                    if (!wi[i].processId.Equals(ProcessIDList.ValueText))
                    {
                        continue;
                    }
                }
                /*
                //過濾denyWorkItems
                bool hasFound = false;
                for (int z = 0; z < denyWorkItems.Length; z++)
                {
                    if (wi[i].workItemName.Equals(denyWorkItems[z]))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (hasFound)
                {
                    continue;
                }
                */
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
            
            //取得EasyFlow的待簽核資料
            DataSet sp7 = getSP7FormInfo(engine);             //通知
            DataSet sp7Forward = getSP7FormForward(engine);   //轉寄
            DataSet sp7Step = getSP7FormStep(engine);         //逐級通知

            if ((wi.Length > 0) || (sp7.Tables[0].Rows.Count>0) || (sp7Forward.Tables[0].Rows.Count>0) || (sp7Step.Tables[0].Rows.Count>0))
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
                /*

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
                
                string qstr = "select GUID, SOURCE, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, CONTENT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME ";

                DataSet extData = null;
                DataSet det = null;
                /*
                if (isSame)
                {
                    //要從SMWDAAA取得延伸欄位
                    sql = "select SMWDAAA013, SMWDAAA001 from SMWDAAA where SMWDAAA003='" + Utility.filter(checkProcessId) + "' and SMWDAAA004='" + Utility.filter(checkWorkItemName) + "'";
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
                */
                qstr += " from SMWL";

                DataObjectSet dos = new DataObjectSet();
                string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">";
                schema += "<queryStr>" + qstr + "</queryStr>";
                schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
                schema += "  <fieldDefinition>";

                schema += "    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>";
                schema += "    <field dataField=\"SOURCE\" typeField=\"STRING\" lengthField=\"40\" defaultValue=\"\" displayName=\"來源\" showName=\"0:(ECP)通知;1:(SP7)通知;2:(SP7)轉寄;3:(SP7)簽核通知;4:(SP7)結案通知\"/>";
                schema += "    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>";
                schema += "    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"40\" defaultValue=\"\" displayName=\"重要性\" showName=\"0:低;1:中;2:高\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>";
                schema += "    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"表單名稱\" showName=\"\"/>";
                schema += "    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>";
                schema += "    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
                schema += "    <field dataField=\"CONTENT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"通知內容\" showName=\"\"/>";
                schema += "    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人/轉寄人\" showName=\"\"/>";
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
                schema += "    <field dataField=\"SOURCE\"/>";
                schema += "    <field dataField=\"ATTACH\"/>";
                schema += "    <field dataField=\"IMPORTANT\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\"/>";
                schema += "    <field dataField=\"PROCESSNAME\"/>";
                schema += "    <field dataField=\"SHEETNO\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\"/>";
                schema += "    <field dataField=\"SUBJECT\"/>";
                schema += "    <field dataField=\"CONTENT\"/>";
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

                for (int i = 0; i < wi.Length; i++)
                {
                    DataObject ddo = dos.create();
                    ddo.setData("GUID", wi[i].workItemOID);
                    ddo.setData("SOURCE", "0");
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
                            else if(ast.Equals("1"))
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

                    /*
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
                    */
                    ddo.Tag = wi[i];

                    dos.add(ddo);
                }

                //處理SP7通知
                for (int i = 0; i < sp7.Tables[0].Rows.Count; i++)
                {
                    DataObject ddo = dos.create();
                    ddo.setData("GUID", sp7.Tables[0].Rows[i]["resdd001"].ToString()+"-"+sp7.Tables[0].Rows[i]["resdd002"].ToString());
                    ddo.setData("SOURCE", "1");
                    ddo.setData("CURRENTSTATE", "");
                    ddo.setData("PROCESSNAME", sp7.Tables[0].Rows[i]["resca002"].ToString());
                    ddo.setData("SHEETNO", sp7.Tables[0].Rows[i]["resdd002"].ToString());
                    if (sp7.Tables[0].Rows[i]["resda032"].ToString().Equals("0"))
                    {
                        ddo.setData("IMPORTANT", "低");
                    }
                    else if (sp7.Tables[0].Rows[i]["resda032"].ToString().Equals("1"))
                    {
                        ddo.setData("IMPORTANT", "普通");
                    }
                    else
                    {
                        ddo.setData("IMPORTANT", "高");
                    }
                    if (sp7.Tables[0].Rows[i]["resde002"].ToString() != "")
                    {
                        ddo.setData("ATTACH", "{[font color=red]}！{[/font]}");
                    }
                    else
                    {
                        ddo.setData("ATTACH", "");
                    }
                    ddo.setData("WORKITEMNAME", "");
                    ddo.setData("SUBJECT", sp7.Tables[0].Rows[i]["resda031"].ToString());
                    ddo.setData("USERNAME", sp7.Tables[0].Rows[i]["resda_017"].ToString());
                    ddo.setData("CREATETIME", sp7.Tables[0].Rows[i]["resdd009"].ToString());
                    ddo.setData("WORKTYPE", "");
                    ddo.setData("ASSIGNMENTTYPE", "");
                    ddo.setData("REASSIGNMENTTYPE", "");
                    ddo.setData("VIEWTIMES", sp7.Tables[0].Rows[i]["resdd013"].ToString());
                    ddo.setData("WORKASSIGNMENT", "");
                    ddo.setData("D_INSERTUSER", "SYSTEM");
                    ddo.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                    ddo.setData("D_MODIFYUSER", "");
                    ddo.setData("D_MODIFYTIME", "");
                    ddo.Tag=sp7.Tables[0].Rows[i];
                    dos.add(ddo);

                }
                //處理sp7Forward轉寄
                for (int i = 0; i < sp7Forward.Tables[0].Rows.Count; i++)
                {
                    DataObject ddo = dos.create();
                    ddo.setData("GUID", sp7Forward.Tables[0].Rows[i]["resdh002"].ToString() + "-" + sp7Forward.Tables[0].Rows[i]["resdh003"].ToString());
                    ddo.setData("SOURCE", "2");
                    ddo.setData("CURRENTSTATE", "");
                    ddo.setData("PROCESSNAME", sp7Forward.Tables[0].Rows[i]["resca002"].ToString());
                    ddo.setData("SHEETNO", sp7Forward.Tables[0].Rows[i]["resdh003"].ToString());
                    if (sp7Forward.Tables[0].Rows[i]["resda032"].ToString().Equals("0"))
                    {
                        ddo.setData("IMPORTANT", "低");
                    }
                    else if (sp7Forward.Tables[0].Rows[i]["resda032"].ToString().Equals("1"))
                    {
                        ddo.setData("IMPORTANT", "普通");
                    }
                    else
                    {
                        ddo.setData("IMPORTANT", "高");
                    }
                    if (sp7Forward.Tables[0].Rows[i]["resde002"].ToString() != "")
                    {
                        ddo.setData("ATTACH", "{[font color=red]}！{[/font]}");
                    }
                    else
                    {
                        ddo.setData("ATTACH", "");
                    }
                    ddo.setData("WORKITEMNAME", "");
                    ddo.setData("SUBJECT", sp7Forward.Tables[0].Rows[i]["resda031"].ToString());
                    ddo.setData("USERNAME", sp7Forward.Tables[0].Rows[i]["resak002"].ToString());
                    ddo.setData("CREATETIME", sp7Forward.Tables[0].Rows[i]["resdh004"].ToString());
                    ddo.setData("WORKTYPE", "");
                    ddo.setData("ASSIGNMENTTYPE", "");
                    ddo.setData("REASSIGNMENTTYPE", "");
                    if (sp7Forward.Tables[0].Rows[i]["resdh005"].ToString().Equals("Y"))
                    {
                        ddo.setData("VIEWTIMES", "已閱讀");
                    }
                    else
                    {
                        ddo.setData("VIEWTIMES", "未閱讀");
                    }
                    ddo.setData("WORKASSIGNMENT", "");
                    ddo.setData("D_INSERTUSER", "SYSTEM");
                    ddo.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                    ddo.setData("D_MODIFYUSER", "");
                    ddo.setData("D_MODIFYTIME", "");
                    ddo.Tag = sp7Forward.Tables[0].Rows[i];
                    dos.add(ddo);

                }
                //處理sp7Step逐級通知
                for (int i = 0; i < sp7Step.Tables[0].Rows.Count; i++)
                {
                    DataObject ddo = dos.create();
                    ddo.setData("GUID", sp7Step.Tables[0].Rows[i]["resdf002"].ToString() + "-" + sp7Step.Tables[0].Rows[i]["resdf003"].ToString());
                    if (sp7Step.Tables[0].Rows[i]["resdf008"].ToString().Equals("0"))
                    {
                        ddo.setData("SOURCE", "3");
                    }
                    else
                    {
                        ddo.setData("SOURCE", "4");
                    }
                    ddo.setData("CURRENTSTATE", "");
                    ddo.setData("PROCESSNAME", sp7Step.Tables[0].Rows[i]["resca002"].ToString());
                    ddo.setData("SHEETNO", sp7Step.Tables[0].Rows[i]["resdf003"].ToString());
                    if (sp7Step.Tables[0].Rows[i]["resda032"].ToString().Equals("0"))
                    {
                        ddo.setData("IMPORTANT", "低");
                    }
                    else if (sp7Step.Tables[0].Rows[i]["resda032"].ToString().Equals("1"))
                    {
                        ddo.setData("IMPORTANT", "普通");
                    }
                    else
                    {
                        ddo.setData("IMPORTANT", "高");
                    }
                    if (sp7Step.Tables[0].Rows[i]["resde002"].ToString() != "")
                    {
                        ddo.setData("ATTACH", "{[font color=red]}！{[/font]}");
                    }
                    else
                    {
                        ddo.setData("ATTACH", "");
                    }
                    ddo.setData("WORKITEMNAME", "");
                    ddo.setData("SUBJECT", sp7Step.Tables[0].Rows[i]["resda031"].ToString());
                    //ddo.setData("CONTENT", sp7Step.Tables[0].Rows[i]["resdf010"].ToString());
                    ddo.setData("USERNAME", sp7Step.Tables[0].Rows[i]["resak002"].ToString());
                    ddo.setData("CREATETIME", sp7Step.Tables[0].Rows[i]["resdf009"].ToString());
                    ddo.setData("WORKTYPE", "");
                    ddo.setData("ASSIGNMENTTYPE", "");
                    ddo.setData("REASSIGNMENTTYPE", "");
                    if (sp7Step.Tables[0].Rows[i]["resdf011"].ToString().Equals("Y"))
                    {
                        ddo.setData("VIEWTIMES", "已閱讀");
                    }
                    else
                    {
                        ddo.setData("VIEWTIMES", "未閱讀");
                    }
                    ddo.setData("WORKASSIGNMENT", "");
                    ddo.setData("D_INSERTUSER", "SYSTEM");
                    ddo.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                    ddo.setData("D_MODIFYUSER", "");
                    ddo.setData("D_MODIFYTIME", "");
                    ddo.Tag = sp7Step.Tables[0].Rows[i];
                    dos.add(ddo);

                }

                ListTable.IsGeneralUse = false;
                ListTable.InputForm = "Detail.aspx";
                ArrayList hFields = (ArrayList)getSession("HiddenFields");
                string[] hf = new string[hFields.Count + 5];
                hf[0] = "GUID";
                hf[1] = "WORKASSIGNMENT";
                hf[2] = "ASSIGNMENTTYPE";
                hf[3] = "REASSIGNMENTTYPE";
                hf[4] = "CONTENT";
                for (int i = 0; i < hFields.Count; i++)
                {
                    hf[i + 5] = (string)hFields[i];
                }
                ListTable.HiddenField = hf;
                ListTable.WidthMode = 1;
                ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                ListTable.dataSource = dos;
                ListTable.updateTable();

                setSession("CURLIST", dos);
            }
            else
            {
                string qstr = "select GUID, SOURCE, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, CONTENT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME from SMWL";


                DataObjectSet dos = new DataObjectSet();
                string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">";
                schema += "<queryStr>" + qstr + "</queryStr>";
                schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
                schema += "  <fieldDefinition>";

                schema += "    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>";
                schema += "    <field dataField=\"SOURCE\" typeField=\"STRING\" lengthField=\"40\" defaultValue=\"\" displayName=\"來源\" showName=\"0:(ECP)通知;1:(SP7)通知;2:(SP7)轉寄;3:(SP7)簽核通知;4:(SP7)結案通知\"/>";
                schema += "    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>";
                schema += "    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"重要性\" showName=\"\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>";
                schema += "    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"表單名稱\" showName=\"\"/>";
                schema += "    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>";
                schema += "    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
                schema += "    <field dataField=\"CONTENT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"通知內容\" showName=\"\"/>";
                schema += "    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人/轉寄人\" showName=\"\"/>";
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
                schema += "    <field dataField=\"SOURCE\"/>";
                schema += "    <field dataField=\"ATTACH\"/>";
                schema += "    <field dataField=\"IMPORTANT\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\"/>";
                schema += "    <field dataField=\"PROCESSNAME\"/>";
                schema += "    <field dataField=\"SHEETNO\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\"/>";
                schema += "    <field dataField=\"SUBJECT\"/>";
                schema += "    <field dataField=\"CONTENT\"/>";
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
                ListTable.HiddenField = new string[] { "GUID", "WORKASSIGNMENT", "ASSIGNMENTTYPE", "REASSIGNMENTTYPE","CONTENT" };
                ListTable.dataSource = dos;
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
            writeLog(te);
        }

        //MessageBox("已取得清單");

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
        try
        {
            WorkItem wi = (WorkItem)objects.Tag;

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
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
                sql = "select SMWDAAA006, SMWDAAA005 from SMWDAAA inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(wi.processId) + "' and SMWDAAA004='" + Utility.filter(wi.workItemName) + "'";
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
                fd.UIStatus = FlowStatusData.FormNotify;
                fd.IsAllowWithDraw = false;
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
        catch (Exception zz)
        {
            //建立FlowStatusData物件
            FlowStatusData fd = new FlowStatusData();

            DataRow dr = (DataRow)objects.Tag;
            if (objects.getData("SOURCE").Equals("1"))
            {
                fd.PDID = dr["resdd001"].ToString();
                fd.ObjectGUID = "";
                fd.FlowGUID = dr["resdd002"].ToString();
                fd.WorkItemOID = dr["resdd003"].ToString();
                fd.TargetWorkItemOID = dr["resdd004"].ToString();
                fd.workAssignmentOID = dr["resdd005"].ToString();
                fd.manualReassignType = "1";
                ListTable.FormTitle = "\t"+dr["resca002"].ToString()+"("+dr["resdd002"].ToString()+")";
            }
            else if (objects.getData("SOURCE").Equals("2"))
            {
                fd.PDID = dr["resdh002"].ToString();
                fd.ObjectGUID = "";
                fd.FlowGUID = dr["resdh003"].ToString();
                fd.WorkItemOID = "";
                fd.TargetWorkItemOID = "";
                fd.workAssignmentOID = "";
                fd.manualReassignType = "2";
                ListTable.FormTitle = "\t" + dr["resca002"].ToString() + "(" + dr["resdh003"].ToString() + ")";
            }
            else if ((objects.getData("SOURCE").Equals("3")) || (objects.getData("SOURCE").Equals("4")))
            {
                fd.PDID = dr["resdf002"].ToString();
                fd.ObjectGUID = "";
                fd.FlowGUID = dr["resdf003"].ToString();
                fd.WorkItemOID = dr["resdf004"].ToString();
                fd.TargetWorkItemOID = dr["resdf005"].ToString();
                fd.workAssignmentOID = dr["resdf006"].ToString();
                fd.manualReassignType = "3";
                ListTable.FormTitle = "\t" + dr["resca002"].ToString() + "(" + dr["resdf003"].ToString() + ")";
            }
            //base.showModalDialog("http://www.google.com.tw", "", "", "", "", "", "", "", "", "", "", "", "");
            //base.showOpenWindow("http://www.google.com.tw", "", "", "", "", "", "", "", "", "", "", "", "", "", "", false);
            return fd;
        }
    }
    protected void ListTable_RefreshButtonClick()
    {
        queryData();
    }
    protected void GroupSign_Click(object sender, EventArgs e)
    {
        /*
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
            engine.close();

            for (int i = 0; i < ary.Length; i++)
            {
                engine=factory.getEngine(engineType, connectString);
                WorkItem wi = (WorkItem)ary[i].Tag;

                //取得資料物件代碼, 由原稿取得
                sql = "select SMWYAAA019, SMWYAAA018 from SMWYAAA where SMWYAAA005='" + Utility.filter(wi.processSerialNumber) + "'";
                ds = engine.getDataSet(sql, "TEMP");

                string objectGUID = "";

                string urlP = "";

                urlP += "ParentPanelID=" + CurPanelID + "&DataListID=ListTable";
                urlP += "&UIType=Process";

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
                //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
                //目前都給他為ProcessModify
                sql = "select SMWDAAA006, SMWDAAA005 from SMWDAAA inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(wi.processId) + "' and SMWDAAA004='" + Utility.filter(wi.workItemName) + "'";
                DataSet ds2 = engine.getDataSet(sql, "TEMP");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    if (ds2.Tables[0].Rows[0][0].ToString().Equals("New"))
                    {
                        MessageBox(wi.processName + ":" + wi.workItemName + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " QueryError2", "為流程中新增表單資料, 無法群簽, 請點選該單據進入填寫資料."));
                        continue;
                    }
                    else
                    {
                        //有可能是ProcessNew之後的ProcessModify, 必須考慮替換fd.ObjectGUID
                        if (!ds.Tables[0].Rows[0][1].ToString().Equals(ds2.Tables[0].Rows[0][1].ToString()))
                        {
                            //若原稿的SMWAAAA001跟目前關卡的SMWAAAA001不同, 表示要替換
                            sql = "select CURGUID from FORMRELATION where ORIGUID='" + Utility.filter(objectGUID) + "'";
                            ds2 = engine.getDataSet(sql, "TEMP");
                            objectGUID = ds2.Tables[0].Rows[0][0].ToString();
                        }
                    }
                }
                urlP += "&ObjectGUID=" + objectGUID;
                urlP += "&HistoryGUID=" + "";
                urlP += "&FlowGUID=" + wi.processSerialNumber;
                urlP += "&ACTID=" + "";
                urlP += "&PDID=" + wi.processId;
                urlP += "&PDVer=" + "";
                urlP += "&ACTName=" + wi.workItemName;
                urlP += "&UIStatus=5";
                urlP += "&WorkItemOID=" + wi.workItemOID;
                urlP += "&GroupSign=Y";
                urlP += "&signResult=" + Server.UrlEncode(signResult.ValueText + "||" + signResult.ReadOnlyText);
                urlP += "&signOpinion=" + Server.UrlEncode(signOpinion.ValueText);
                urlP += "&EFLogonID=" + (string)Session["UserID"];

                //取得頁面URL
                string url = "";
                sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(wi.processId) + "' and SMWDAAA004='" + Utility.filter(wi.workItemName) + "'";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    url = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(wi.processId) + "' and SMWDAAA006='Default'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        MessageBox(wi.processName + ":" + wi.workItemName + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " QueryError3", "找不到作業處理畫面. 請洽系統管理員"));
                        continue;
                    }
                    url = ds.Tables[0].Rows[0][0].ToString();
                }
                url = Page.ResolveUrl("~/" + url);
                url = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "httpHead") + "://" + siteName + url;

                engine.close();
                HTTPProcessor.isSendCookie = false;
                string returnString = HTTPProcessor.sendGet(url, urlP);
                HTTPProcessor.isSendCookie = true;
                //string returnString = "aaa";
                if (isDebugPage.Equals("Y"))
                {
                    string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                    fname = Server.MapPath(@"~\LogFolder\" + fname + "_callback.log");
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);

                    sw.WriteLine("***************");
                    sw.WriteLine("GroupSign result:");
                    sw.WriteLine(returnString);
                    sw.Close();
                }
            }

            ListTable.UnCheckAllData();

            //MessageBox("完成!請更新清單");
            queryData();

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
            queryData();
        }
        */
    }

    private DataSet getSP7FormInfo(AbstractEngine engine)
    {
        

        SysParam sp = new SysParam(engine);
        string sp7str = sp.getParam("EF2KWebDB");

        IOFactory factory = new IOFactory();
        AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7str);

        //========修改的部分==========
        string userid = mappingUserID(engine, sp7engine);
        //========修改的部分==========

        string strSQL = "";
        strSQL = "select distinct resdd.resdd001, resca.resca002, resdd.resdd002, resdd.resdd003, ";
        strSQL = strSQL + "resdd.resdd004, resdd.resdd005, resdd.resdd006, resdd.resdd008, resdd.resdd009, ";
        strSQL = strSQL + " resdd.resdd014, resdd.resdd013, resdd.resdd015, resda.resda031, resda.resda032, ";
        strSQL = strSQL + " resde.resde002, ";
        strSQL = strSQL + " resak1.resak002 as resda_016, resak2.resak002 as resda_017 ";
        strSQL = strSQL + " FROM resdd LEFT OUTER JOIN ";
        strSQL = strSQL + " resde ON resdd.resdd001 = resde.resde001 AND resdd.resdd002 = resde.resde002 LEFT OUTER JOIN ";
        strSQL = strSQL + " resda ON resdd.resdd001 = resda.resda001 AND resdd.resdd002 = resda.resda002 LEFT OUTER JOIN ";
        strSQL = strSQL + " resak as resak1 ON resda.resda016 = resak1.resak001 LEFT OUTER JOIN ";
        strSQL = strSQL + " resak as resak2 ON resda.resda017 = resak2.resak001 LEFT OUTER JOIN ";
        strSQL = strSQL + " resdb ON resdd.resdd001 = resdb.resdb001 AND resdd.resdd002 = resdb.resdb002 and resdb.resdb003=resdd.resdd003 and resdb.resdb004=resdd.resdd004 LEFT OUTER JOIN ";
        strSQL = strSQL + " resca ON resdd.resdd001 = resca.resca001 ";
        strSQL = strSQL + " WHERE ";
        strSQL = strSQL + " (NOT (resdd.resdd001 LIKE '%CRM_%')) and ";
        strSQL = strSQL + " (resdd.resdd007 = '" + userid + "' OR resdd.resdd020 = '" + userid + "') ";
        strSQL = strSQL + " and resdd.resdd019 = 'Y' ";
        strSQL = strSQL + " AND resdd.resdd018=16 ";
        if (!StartTime.ValueText.Equals(""))
        {
            strSQL += " AND resdd009>='" + StartTime.ValueText + "' ";
        }
        if (!EndTime.ValueText.Equals(""))
        {
            strSQL += " AND resdd009<='" + EndTime.ValueText + "' ";
        }
        if (!ProcessIDList.ValueText.Equals(""))
        {
            strSQL += " AND resdd001='" + ProcessIDList.ValueText + "' ";
        }

        DataSet ds = sp7engine.getDataSet(strSQL, "TEMP");

        sp7engine.close();
        return ds;
    }
    private DataSet getSP7FormForward(AbstractEngine engine)
    {
        

        SysParam sp = new SysParam(engine);
        string sp7str = sp.getParam("EF2KWebDB");

        IOFactory factory = new IOFactory();
        AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7str);

        //========修改的部分==========
        string userid = mappingUserID(engine, sp7engine);
        //========修改的部分==========

        string strSQL = "";
        strSQL = "select distinct resdh.resdh001, resca.resca002, resdh.resdh002, resdh.resdh003, ";
        strSQL = strSQL + "resdh.resdh004, resdh.resdh005, resdh.resdh006, ";
        strSQL = strSQL + " resda.resda020, resda.resda021, resda.resda031, resda.resda032, ";
        strSQL = strSQL + " resde.resde002, resak.resak002 ";
        strSQL = strSQL + " FROM resdh LEFT OUTER JOIN ";
        strSQL = strSQL + " resde ON resdh.resdh002 = resde.resde001 AND ";
        strSQL = strSQL + " resdh.resdh003 = resde.resde002 LEFT OUTER JOIN ";
        strSQL = strSQL + " resda ON resdh.resdh002 = resda.resda001 AND ";
        strSQL = strSQL + " resdh.resdh003 = resda.resda002 LEFT OUTER JOIN ";
        strSQL = strSQL + " resak ON resdh.resdh006 = resak.resak001 LEFT OUTER JOIN ";
        strSQL = strSQL + " resca ON resdh.resdh002 = resca.resca001 ";
        strSQL = strSQL + " WHERE ";
        strSQL = strSQL + " (NOT (resdh.resdh002 LIKE '%CRM_%')) and ";
        strSQL = strSQL + " resdh.resdh001 = '" + userid + "' ";
        if (!StartTime.ValueText.Equals(""))
        {
            strSQL += " AND resdh004>='" + StartTime.ValueText + "' ";
        }
        if (!EndTime.ValueText.Equals(""))
        {
            strSQL += " AND resdh004<='" + EndTime.ValueText + "' ";
        }
        if (!ProcessIDList.ValueText.Equals(""))
        {
            strSQL += " AND resdh002='" + ProcessIDList.ValueText + "' ";
        }
        if (!Subject.ValueText.Equals(""))
        {
            strSQL += " AND resda031 like '%" + Subject.ValueText + "%' ";
        }
        DataSet ds = sp7engine.getDataSet(strSQL, "TEMP");

        sp7engine.close();
        return ds;
    }
    private DataSet getSP7FormStep(AbstractEngine engine)
    {
        

        SysParam sp = new SysParam(engine);
        string sp7str = sp.getParam("EF2KWebDB");

        IOFactory factory = new IOFactory();
        AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7str);

        //========修改的部分==========
        string userid = mappingUserID(engine, sp7engine);
        //========修改的部分==========

        string strSQL = "";
        strSQL = "select distinct resdf.resdf001, resca.resca002, resdf.resdf002, resdf.resdf003, ";
        //strSQL = "select Top 100 resdf.resdf001, resca.resca002, resdf.resdf002, resdf.resdf003, ";
        strSQL = strSQL + "resdf.resdf004, resdf.resdf005, resdf.resdf006, ";
        strSQL = strSQL + " resdf.resdf007, resdf.resdf008, resdf.resdf009, ";
        strSQL = strSQL + " cast(resdf.resdf010 as nvarchar(255)) as resdf010, resdf.resdf011, resda.resda020, resda.resda021, resda.resda031, resda.resda032, ";
        strSQL = strSQL + " resde.resde002, resak.resak002 ";
        strSQL = strSQL + " FROM resdf LEFT OUTER JOIN ";
        strSQL = strSQL + " resde ON resdf.resdf002 = resde.resde001 AND ";
        strSQL = strSQL + " resdf.resdf003 = resde.resde002 LEFT OUTER JOIN ";
        strSQL = strSQL + " resda ON resdf.resdf002 = resda.resda001 AND ";
        strSQL = strSQL + " resdf.resdf003 = resda.resda002 LEFT OUTER JOIN ";
        strSQL = strSQL + " resak ON resda.resda016 = resak.resak001 LEFT OUTER JOIN ";
        strSQL = strSQL + " resca ON resdf.resdf002 = resca.resca001 ";
        strSQL = strSQL + " WHERE ";
        strSQL = strSQL + " (NOT (resda.resda001 LIKE '%CRM_%')) and ";
        strSQL = strSQL + " resdf.resdf001 = '" + userid + "' ";
        if (!StartTime.ValueText.Equals(""))
        {
            strSQL += " AND resdf009>='" + StartTime.ValueText + "' ";
        }
        if (!EndTime.ValueText.Equals(""))
        {
            strSQL += " AND resdf009<='" + EndTime.ValueText + "' ";
        }
        if (!ProcessIDList.ValueText.Equals(""))
        {
            strSQL += " AND resdf002='" + ProcessIDList.ValueText + "' ";
        }

        DataSet ds = sp7engine.getDataSet(strSQL, "TEMP");

        sp7engine.close();
        return ds;
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

    //========修改的部分==========
    private string mappingUserID(AbstractEngine engine, AbstractEngine sp7engine)
    {
        string ret = "";
        ret = Convert.ToString(sp7engine.executeScalar("select resak001 from resak with(nolock) where resak001='" + Convert.ToString(Session["UserID"]) + "'"));
        if (ret != "")
        {
            return ret;
        }
        else
        {
            return Convert.ToString(Session["UserID"]);
        }
    }
    //========修改的部分==========
}
