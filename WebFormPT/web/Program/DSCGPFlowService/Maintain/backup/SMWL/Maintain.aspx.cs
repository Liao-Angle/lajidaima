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

public partial class Program_DSCGPFlowService_Maintain_SMWL_Maintain : BaseWebUI.GeneralWebPage
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
                string BoxID = Request.QueryString["BoxID"];
                setSession("BoxID", BoxID);

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);


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

                    ids = new string[,]{
                        {"",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids1", "不限定")},
                        {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids5", "系統自動通知")},
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids6", "私人轉寄")},
                        {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids7", "一般轉寄, 顯示在流程追蹤")},
                        {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids8", "被代理通知")},
                        {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids9", "流程被撤銷或工作被回收")}
                    };
                    AssignType.setListItem(ids);

                    GroupSignBox.Display = false;
                }
                //得取次數過濾
                ids = new string[,]{
                    {"A",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids10", "全部")},
                    {"U",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids11", "未讀取")},
                    {"R",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids12", "已讀取")},
                    //CL_Chang 2013/07/12 增加選項
                    {"V",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids1201", "未保留")},
                    {"D",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " ids1201", "已保留")}
                };
                ViewTimes.setListItem(ids);
                ViewTimes.ValueText = "V";

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
                    //sql = "select SMWBAAA003 from SMWBAAA where SMWBAAA003 not in (select SMWIAAB003 from SMWIAAB where SMWIAAB002='" + Utility.filter(ds.Tables[0].Rows[0]["SMWIAAA001"].ToString()) + "')";
					//CL_Chang 濾除ERP單據
					sql = "select SMWBAAA003 from SMWBAAA where SMWBAAA003 not in (select SMWIAAB003 from SMWIAAB where SMWIAAB002='" + Utility.filter(ds.Tables[0].Rows[0]["SMWIAAA001"].ToString()) + "') "
                        + "and ISNULL(SMWBAAA901,'') = ''";
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
                sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA where SMWBAAA003 in (" + ztag + ") order by SMWBAAA004";
                DataSet ddd = engine.getDataSet(sql, "TEMP");
                ids = new string[ddd.Tables[0].Rows.Count + 1, 2];
                ids[0, 0] = "";
                ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " idsA", "不限定");
                for (int i = 0; i < ddd.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1, 0] = ddd.Tables[0].Rows[i][0].ToString();
                    ids[i + 1, 1] = ddd.Tables[0].Rows[i][1].ToString();
                }
                ProcessIDList.setListItem(ids);

                //設定不可取得流程
                sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA where SMWBAAA003 not in (" + ztag + ") order by SMWBAAA004";
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
				//string[] fieldOrder = ds.Tables[0].Rows[0]["CREATETIME"].ToString().Split(new char[] { ';' });
                ListTable.reOrderField(fieldOrder);
                setSession("fieldOrder", fieldOrder);

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

            FlowFactory ff = new FlowFactory();
            AbstractFlowAdapter adp = ff.getAdapter(flowType);
            adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
            adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
            fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

            adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

            WorkItem[] wi = null;

            string BoxQueryType = (string)getSession("BoxQueryType");

            if (BoxQueryType.Equals("W"))
            {
                if (isDB.Equals("N"))
                {
                    wi = adp.fetchPerformableWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, ProcessIDList.ValueText);
                }
                else
                {
                    wi = adp.fetchPerformableWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, ProcessIDList.ValueText, engine);
                }
                //代理人處理機制
                if (isSubstitute.Equals("Y"))
                {
                    //取出並宣告系統參數定義之存取組件
                    setSession("SUBSLIST", new StringCollection());
                    SysParam sp = new SysParam(engine);
                    string subsComponent = sp.getParam("SubstituteComponent");
                    SubstituteFactory fac = new SubstituteFactory();
                    AbstractSubstitute asub = fac.getSubstituteUtility(subsComponent.Split(new char[] { '.' })[0], subsComponent);
                    //取出被代理人清單
                    string[,] substituteList = asub.getSubstituteList((string)Session["UserID"],engine);

                    //依據List取出對應的WorkItem並合併
                    if (substituteList.Length > 0)
                    {
                        string substituteUser = string.Empty;
                        WorkItem[] wiNew = null;
                        ArrayList arrayWI = new ArrayList();
                        for (int i = 0; i < substituteList.Length / 3; i++)
                        {
                            //一個ID只抓一次WorkItem
                            if (!substituteUser.Equals(substituteList[i, 0]))
                            {
                                //更新UserID
                                substituteUser = substituteList[i, 0];
                                //依據被代理人ID抓取WorkItem
                                if (isDB.Equals("N"))
                                {
                                    wiNew = adp.fetchPerformableWorkItems(substituteUser, "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, "");
                                }
                                else
                                {
                                    wiNew = adp.fetchPerformableWorkItems(substituteUser, "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, "", engine);
                                }
                            }
                            //篩選ProcessID與發起部門
                            string substituteProcID = substituteList[i, 1];
                            string substituteOrg = substituteList[i, 2];
                            arrayWI = new ArrayList();
                            for (int j = 0; j < wiNew.Length;j++)
                            {
                                //filter the ProcID
                                if (wiNew[j].processId.Equals(substituteProcID))
                                {
                                    if ((string.IsNullOrEmpty(substituteOrg)) || (substituteOrg==""))
                                    {
                                        if (!arrayWI.Contains(wiNew[j]))
                                            arrayWI.Add(wiNew[j]);
                                    }
                                    else
                                    {
                                        //filter the Org-2010/05/10國昌修正, SMWYAAA0005->SMWYAAA005, 並將wiNew[j].processInstanceOID->processSerialNumber
                                        sql = "select SMWYAAA016 from SMWYAAA where SMWYAAA005 = '{0}'";
                                        string org = (string)engine.executeScalar(string.Format(sql, wiNew[j].processSerialNumber));
                                        if (substituteOrg.Equals(org))
                                        {
                                            if (!arrayWI.Contains(wiNew[j]))
                                                arrayWI.Add(wiNew[j]);
                                        }
                                    }
                                }
                            }
                            //合併
                            wi = mergeWorkItem(wi, arrayWI);
                        }
                    }
                }
            }
            else
            {
                if (isDB.Equals("N"))
                {
                    wi = adp.fetchNoticeWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, ProcessIDList.ValueText);
                }
                else
                {
                    wi = adp.fetchNoticeWorkItems((string)Session["UserID"], "1000000", "1", "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, AssignType.ValueText, ProcessIDList.ValueText, engine);
                }

                //這裡要讀取CUSOMENOTICE資料表
                sql = "select * from CUSTOMENOTICE where RECEIVERID='" + (string)Session["UserID"] + "' and PROCESSTYPE ='INFO'";
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
                DataSet dsc = engine.getDataSet(sql, "TEMP");
                ArrayList twi = new ArrayList();
                for (int i = 0; i < dsc.Tables[0].Rows.Count; i++)
                {
                    WorkItem wis = new WorkItem();
                    wis.batchPerformable = false;
                    wis.canRollback = false;
                    wis.createdTime = dsc.Tables[0].Rows[i]["SENDTIME"].ToString();
                    wis.currentState = "3";
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
            DataSet ds = engine.getDataSet("select * from QUEUEWAITING", "TEMP");

            //cl_chang 2014/08/25
            sql = "select b.SMWYAAA005 from SmpReserve a, SMWYAAA b where a.SheetNo = b.SMWYAAA002 and UserGUID='" + (string)Session["UserGUID"] + "'";
            DataSet dataSet = engine.getDataSet(sql, "TEMP");
            List<string> listReserve = new List<string>();
            int rows = dataSet.Tables[0].Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                string processSerialNumber = dataSet.Tables[0].Rows[i][0].ToString();
                listReserve.Add(processSerialNumber);
            }

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
                    if (!hasF)
                    {
                        DataRow[] drs = ds.Tables[0].Select("KEYS='" + wi[i].processId + "'");
                        if (drs.Length > 0)
                        {
                            hasF = true;
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
                if (!hasFound)
                {
                    DataRow[] drs = ds.Tables[0].Select("KEYS='" + wi[i].workItemOID + "'");
                    if (drs.Length > 0)
                    {
                        hasFound = true;
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
                //CL_Chang 2014/08/25 
                else if (ViewTimes.ValueText.Equals("V"))
                {
                    if (listReserve.IndexOf(wi[i].processSerialNumber) != -1)
                    {
                        continue;
                    }
                }
                else if (ViewTimes.ValueText.Equals("D"))
                {
                    if (listReserve.IndexOf(wi[i].processSerialNumber) == -1)
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

            if (wi.Length > 0)
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

                sql = "select SMWYAAA002, SMWYAAA005, SMWYAAA019, SMWYAAA007, SMWYAAA022 from SMWYAAA where SMWYAAA005 in (" + taging + ") union select SMWYAAA002, FLOWGUID as SMWYAAA005, SMWYAAA019, SMWYAAA007, SMWYAAA022 from SMWYAAA inner join FORMRELATION on ORIGUID=SMWYAAA019 where FLOWGUID in (" + taging + ")";
                DataSet sh = engine.getDataSet(sql, "TEMP");

                sql = "select SMWYAAA005, count(*) from SMWYAAA inner join FILEITEM on SMWYAAA019=JOBID group by SMWYAAA005";
                DataSet att = engine.getDataSet(sql, "TEMP");

                string qstr = "select GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME ";

                DataSet extData = null;
                DataSet det = null;
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

                for (int i = 0; i < wi.Length; i++)
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

                    dos.add(ddo);
                    
                }
                ListTable.IsGeneralUse = false;
                ListTable.InputForm = "Detail.aspx";
                ListTable.CurPageUID = PageUniqueID;
                ArrayList hFields = (ArrayList)getSession("HiddenFields");
                string[] hf = new string[hFields.Count + 4];
                hf[0] = "GUID";
                hf[1] = "WORKASSIGNMENT";
                hf[2] = "ASSIGNMENTTYPE";
                hf[3] = "REASSIGNMENTTYPE";
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
                //ListTable.addSortCondition("流程名稱", DataObjectConstants.ASC);
                ListTable.reSortCondition("單號", DataObjectConstants.ASC); //CL_Chang
                ListTable.updateTable();
                
                //代理人處理機制
                if (BoxQueryType.Equals("W") && isSubstitute.Equals("Y"))
                {
                    //篩選代理清單
                    StringCollection filteredSB = new StringCollection();
                    StringCollection allSB = (StringCollection)getSession("SUBSLIST");
                    for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
                    {
                        //紀錄代理的流程工作項目OID
                        if (allSB.Contains(((WorkItem)dos.getAvailableDataObject(i).Tag).workItemOID))
                        {
                            filteredSB.Add(((WorkItem)dos.getAvailableDataObject(i).Tag).workItemOID);
                        }
                    }
                    setSession("SUBSLIST", filteredSB);
                }

                setSession("CURLIST", dos);
                setSession("flowPerformers", systemInfo.UserID + ";");
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
                ListTable.HiddenField = new string[] { "GUID", "WORKASSIGNMENT", "ASSIGNMENTTYPE", "REASSIGNMENTTYPE" };
                ListTable.dataSource = dos;
                string[] fieldOrder = (string[])getSession("fieldOrder");
                ListTable.reOrderField(fieldOrder);
                //ListTable.addSortCondition("流程名稱", DataObjectConstants.ASC);
                ListTable.reSortCondition("單號", DataObjectConstants.ASC); //CL_Chang
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
            try
            {
                engine.close();
            }
            catch { };
            return null;
        }
    }
    
    protected void ListTable_RefreshButtonClick()
    {
        queryData();
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

                    

                    sql = "Select SMWDAAA030 from SMWDAAA inner join SMWBAAA on SMWDAAA003=SMWBAAA004  where SMWBAAA003='" + wi.processId + "' and SMWDAAA006='Display' and SMWDAAA004='" + wi.workItemName + "'";
                    DataSet dsa = engine.getDataSet(sql, "TEMP");
                    String checkWaitForReference = "";
                    if (dsa.Tables[0].Rows.Count > 0)
                    {
                        checkWaitForReference = dsa.Tables[0].Rows[0][0].ToString();
                        dsa.Dispose();
                    }
                    else {
                        sql = "Select SMWDAAA030 from SMWDAAA inner join SMWBAAA on SMWDAAA003=SMWBAAA004  where SMWBAAA003='" + wi.processId + "' and SMWDAAA006='New' and SMWDAAA004='" + wi.workItemName + "'";
                        dsa = engine.getDataSet(sql, "TEMP");
                        if (dsa.Tables[0].Rows.Count > 0)
                        {
                            checkWaitForReference = dsa.Tables[0].Rows[0][0].ToString();
                            dsa.Dispose();
                        }
                        else //流程畫面關聯狀態不是流程中新增也不是流程處理 ; 便找預設畫面
                        {
                            sql = "Select SMWDAAA030 from SMWDAAA inner join SMWBAAA on SMWDAAA003=SMWBAAA004  where SMWBAAA003='" + wi.processId + "' and SMWDAAA006='Default' and SMWDAAA004='" + wi.workItemName + "'";
                            dsa = engine.getDataSet(sql, "TEMP");
                            if (dsa.Tables[0].Rows.Count > 0)
                            {
                                checkWaitForReference = dsa.Tables[0].Rows[0][0].ToString();
                                dsa.Dispose();
                            }
                        }
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

            if (scGroupSignWorkOIDList.Count > 0)
            {
                    string flowType =sp.getParam( "FlowAdapter");
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
                    if (subsWorkOIDList != null) {
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
            if (!String.IsNullOrEmpty(RefProcessExist))
            {
                MessageBox(RefProcessExist);
            }
            else 
            {
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " QueryError5", "群簽完成"));                
            }
            queryData();
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
            queryData();
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

    /// <summary>
    /// cl_chang 保留/復原
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ToggleButton_Click(object sender, EventArgs e)
    {
        DataObject[] ddo = ListTable.getSelectedItem();
        if (ddo.Length > 0)
        {
            string uptag = "";
            if (ViewTimes.ValueText.Equals("D"))
            {
                uptag = "Y";
            }
            else
            {
                uptag = "D";
                ViewTimes.ValueText = "V";
            }

            string xtag = "'*'";
            for (int i = 0; i < ddo.Length; i++)
            {
                xtag += ",'" + ddo[i].getData("SHEETNO") + "'";
            }

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;

            try
            {
                engine = factory.getEngine(engineType, connectString);
                if (uptag.Equals("D"))
                {
                    for (int i = 0; i < ddo.Length; i++)
                    {
                        string guid = IDProcessor.getID("");
                        string userGUID = (string)Session["UserGUID"];
                        string sheetNo = ddo[i].getData("SHEETNO");
                        string now = DateTimeUtility.getSystemTime2(null);
                        string sql = "insert into SmpReserve (GUID, UserGUID, SheetNo, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) values ";
                        sql += "('" + guid + "','" + userGUID + "','" + sheetNo + "','Y','N','Y','" + userGUID + "','" + now + "', '', '')";
                        engine.executeSQL(sql);
                    }
                }
                else
                {
                    string sql = "delete SmpReserve where UserGUID='" + (string)Session["UserGUID"] + "' and SheetNo in (" + xtag + ")";
                    engine.executeSQL(sql);
                }

                engine.close();

                queryData();
            }
            catch (Exception ue)
            {
                try
                {
                    engine.close();
                }
                catch { };
                MessageBox(ue.Message);
            }
        }
    }
}
