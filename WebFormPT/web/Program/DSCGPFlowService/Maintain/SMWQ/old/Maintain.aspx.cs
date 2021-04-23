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

                    engine.close();
                }

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

            wi = adp.traceProcInstances((string)Session["UserID"], "1000000", "1", "1", "0", "'" + SMWYAAA003.ValueText + "'", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText);
            
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
            engine.close();

            string BoxID = (string)getSession("BoxID");
            SMWTAAA aa = null;
            if (!BoxID.Equals(""))
            {
                aa = (SMWTAAA)getSession("SMWTAAA");
            }

            DataObjectSet dos = new DataObjectSet();
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.flow.SMWQ.SMWQAAA");
            dos.setTableName("SMWQAAA");

            for (int i = 0; i < wi.Length; i++)
            {
                if (!SMWYAAA020.ValueText.Equals(""))
                {
                    //進行中 1, 已完成 3, 已撤銷 4, 已終止 5
                    if (!wi[i].state.Equals(SMWYAAA020.ValueText))
                        continue;
                    //switch (SMWYAAA020.ValueText)
                    //{
                    //    case "I":
                    //        if (!wi[i].state.Equals("1"))
                    //            continue;
                    //        break;
                    //    case "Y":
                    //        if (!wi[i].state.Equals("3"))
                    //            continue;
                    //        break;
                    //    case "N":
                    //        if (!wi[i].state.Equals("5"))
                    //            continue;
                    //        break;
                    //    case "W":
                    //        if (!wi[i].state.Equals("4"))
                    //            continue;
                    //        break;
                    //    default:
                    //        break;
                    //}
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
                SMWQAAA oa = (SMWQAAA)dos.create();
                oa.SMWQAAA001 = IDProcessor.getID("");
                oa.SMWQAAA002 = wi[i].OID;
                oa.SMWQAAA003 = wi[i].createdTime;
                oa.SMWQAAA004 = wi[i].processName;
                oa.SMWQAAA005 = wi[i].serialNo;
                oa.SMWQAAA006 = wi[i].requesterName;
                oa.SMWQAAA007 = wi[i].state;
                if (wi[i].allowByPass)
                {
                    oa.SMWQAAA008 = "Y";
                }
                else
                {
                    oa.SMWQAAA008 = "N";
                }
                oa.SMWQAAA009 = wi[i].subject;
                oa.SMWQAAA010 = wi[i].runningActNames;
                oa.Tag = wi[i];
                if (!dos.addDraft(oa))
                {
                    MessageBox(dos.errorString);
                }
            }

            ListTable.IsGeneralUse = false;
            ListTable.InputForm = "Detail.aspx";
            ListTable.HiddenField = new string[] { "SMWQAAA001", "SMWQAAA002", "SMWQAAA005", "SMWQAAA008" };
            ListTable.dataSource = dos;
            ListTable.updateTable();
            //CL_Chang 2013/07/15 上下一筆功能
            Session["SMWQ_CURLIS"] = dos;
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
            TraceProcessInstance wi = (TraceProcessInstance)objects.Tag;

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
}
