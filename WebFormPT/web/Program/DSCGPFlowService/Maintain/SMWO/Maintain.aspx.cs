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

            wi = adp.fetchReassignedWorkItems((string)Session["UserID"], ListTable.getPageSize().ToString(), ListTable.getCurrentPage().ToString(), "", Subject.ValueText, "", StartTime.ValueText, EndTime.ValueText, instanceCompleteType.ValueText);

            adp.logout();
            engine.close();

            string BoxID = (string)getSession("BoxID");
            SMWSAAA aa = null;
            if (!BoxID.Equals(""))
            {
                aa = (SMWSAAA)getSession("SMWSAAA");
            }

            DataObjectSet dos = new DataObjectSet();
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.flow.SMWO.SMWOAAA");
            dos.setTableName("SMWOAAA");

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
                SMWOAAA oa = (SMWOAAA)dos.create();
                oa.SMWOAAA001 = IDProcessor.getID("");
                oa.SMWOAAA002 = "";
                oa.SMWOAAA003 = wi[i].processInstanceOID;
                oa.SMWOAAA004 = wi[i].processName;
                oa.SMWOAAA005 = wi[i].processSerialNumber;
                oa.SMWOAAA006 = wi[i].subject;
                oa.SMWOAAA007 = wi[i].currentPerformerName;
                ReAssignHistory his = (ReAssignHistory)wi[i].history[0];
                oa.SMWOAAA008 = his.reassignmentType;
                oa.SMWOAAA009 = wi[i].workItemCreatedTime;
                oa.Tag = wi[i];
                if (!dos.add(oa))
                {
                    MessageBox(dos.errorString);
                }
            }

            ListTable.IsGeneralUse = false;
            ListTable.InputForm = "Detail.aspx";
            ListTable.HiddenField = new string[] { "SMWOAAA001", "SMWOAAA002", "SMWOAAA003", "SMWOAAA005" };
            ListTable.dataSource = dos;
            ListTable.updateTable();

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
        ReAssignActivity wi = (ReAssignActivity)objects.Tag;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "";
        DataSet ds = null;

        //建立FlowStatusData物件
        FlowStatusData fd = new FlowStatusData();
        
        //取得資料物件代碼, 由原稿取得
        sql = "select SMWYAAA019, SMWYAAA003, SMWYAAA004, SMWYAAA002 from SMWYAAA where SMWYAAA005='" + Utility.filter(wi.processSerialNumber) + "'";
        ds = engine.getDataSet(sql, "TEMP");
        string objectGUID = ds.Tables[0].Rows[0][0].ToString();

        fd.ACTID = "";
        fd.ACTName = Server.UrlEncode(wi.workItemName);
        fd.FlowGUID = wi.processSerialNumber;
        fd.HistoryGUID = "";
        fd.ObjectGUID = objectGUID;
        fd.PDID = ds.Tables[0].Rows[0][1].ToString();
        fd.PDVer = "";
        //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
        //目前都給他為ProcessModify
        fd.UIStatus = FlowStatusData.FromRedirect;
        fd.WorkItemOID = wi.workItemOID;
        fd.TargetWorkItemOID = wi.OID;

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
}
