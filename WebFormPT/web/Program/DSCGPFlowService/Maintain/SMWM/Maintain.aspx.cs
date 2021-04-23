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
using WebServerProject.flow.SMWM;
using WebServerProject.flow.SMWG;
using WebServerProject;
using DSCWebControl;

public partial class Program_DSCGPFlowService_Maintain_SMWM_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWM";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";

        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                ListTable.CurPanelID = CurPanelID;

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string[,] ids = new string[,]{
                    {"",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_maintain_aspx.language.ini", "message", "ids1", "不限定")},
                    {"open.not_running.not_started",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_maintain_aspx.language.ini", "message", "ids2", "未開始")},
                    {"open.running",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_maintain_aspx.language.ini", "message", "ids3", "進行中")},
                    {"open.not_running.suspended",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_maintain_aspx.language.ini", "message", "ids4", "已暫停")},
                    {"closed.completed",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_maintain_aspx.language.ini", "message", "ids5", "已完成")},
                    {"closed.aborted",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_maintain_aspx.language.ini", "message", "ids6", "已撤銷")},
                    {"closed.terminated",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_maintain_aspx.language.ini", "message", "ids7", "已中止")}
                };
                SMWMAAA008.setListItem(ids);

                string sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                ids = new string[ds.Tables[0].Rows.Count, 2];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                    ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
                }
                SMWMAAA003.setListItem(ids);

                engine.close();

                ListTable.NoDelete = true;
                ListTable.NoAdd = true;

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
            string sTime = null;
            string eTime = null;
            string eState = null;
            if (SMWMAAA005S.ValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_maintain_aspx.language.ini", "message", "war1", "請輸入查詢時間起"));
            }
            if (!SMWMAAA005S.ValueText.Equals(""))
            {
                sTime = SMWMAAA005S.ValueText;
            }
            if (!SMWMAAA005E.ValueText.Equals(""))
            {
                eTime = SMWMAAA005E.ValueText;
            }

            if (!SMWMAAA008.ValueText.Equals(""))
            {
                eState = SMWMAAA008.ValueText;
            }


            ProcessInstance[] pi = adp.fetchProcInstances(SMWMAAA003.ValueText, sTime, eTime, eState);
            adp.logout();

            DataSet dds = new DataSet();
            if (!SMWMAAA004.ValueText.Equals(""))//如果有輸入單號
            {
                sql = "select SMWYAAA002, SMWYAAA005 from SMWYAAA where SMWYAAA002 in ('" + Utility.filter(SMWMAAA004.ValueText) + "')";
                dds = engine.getDataSet(sql, "TEMP");
            }
            else
            {
                //取得單號
                string taging = "'*'";
                for (int i = 0; i < pi.Length; i++)
                {
                    taging += ",'" + pi[i].serialNo + "'";
                }
                sql = "select SMWYAAA002, SMWYAAA005 from SMWYAAA where SMWYAAA005 in (" + taging + ")";
                dds = engine.getDataSet(sql, "TEMP");
            }

            DataObjectSet dos = new DataObjectSet();
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.flow.SMWM.SMWMAAA");
            dos.setTableName("SMWMAAA");

            if (!SMWMAAA004.ValueText.Equals(""))//如果有輸入單號
            {
                for (int z = 0; z < dds.Tables[0].Rows.Count; z++)
                {
                    for (int x = 0; x < pi.Length; x++)
                    {
                        if (dds.Tables[0].Rows[z][1].ToString().Equals(pi[x].serialNo))
                        {
                            SMWMAAA aa = (SMWMAAA)dos.create();
                            aa.SMWMAAA001 = IDProcessor.getID("");
                            aa.SMWMAAA002 = pi[x].processId;
                            aa.SMWMAAA003 = pi[x].processName;
                            aa.SMWMAAA004 = dds.Tables[0].Rows[z][0].ToString();
                            aa.SMWMAAA005 = pi[x].createdTime;
                            aa.SMWMAAA006 = pi[x].requesterId;
                            aa.SMWMAAA007 = pi[x].requesterName;
                            aa.SMWMAAA008 = pi[x].state;
                            aa.SMWMAAA009 = pi[x].OID;
                            aa.SMWMAAA010 = pi[x].serialNo;
                            aa.SMWMAAA011 = pi[x].subject;

                            aa.Tag = pi[x];

                            if (!dos.add(aa))
                            {
                                throw new Exception(dos.errorString);
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < pi.Length; i++)
                {
                    SMWMAAA aa = (SMWMAAA)dos.create();
                    aa.SMWMAAA001 = IDProcessor.getID("");
                    aa.SMWMAAA002 = pi[i].processId;
                    aa.SMWMAAA003 = pi[i].processName;
                    for (int j = 0; j < dds.Tables[0].Rows.Count; j++)
                    {
                        if (pi[i].serialNo.Equals(dds.Tables[0].Rows[j][1].ToString()))
                        {
                            aa.SMWMAAA004 = dds.Tables[0].Rows[j][0].ToString();
                            break;
                        }
                    }
                    aa.SMWMAAA005 = pi[i].createdTime;
                    aa.SMWMAAA006 = pi[i].requesterId;
                    aa.SMWMAAA007 = pi[i].requesterName;
                    aa.SMWMAAA008 = pi[i].state;
                    aa.SMWMAAA009 = pi[i].OID;
                    aa.SMWMAAA010 = pi[i].serialNo;
                    aa.SMWMAAA011 = pi[i].subject;

                    aa.Tag = pi[i];

                    if (!dos.add(aa))
                    {
                        throw new Exception(dos.errorString);
                    }
                }
            }

            string[,] orderby = new string[,]{
                {"SMWMAAA004",DataObjectConstants.DESC}
            };
            dos.sort(orderby);

            ListTable.IsGeneralUse = false;
            ListTable.IsPanelWindow = true;
            ListTable.InputForm = "Detail.aspx";
            ListTable.HiddenField = new string[] { "SMWMAAA001", "SMWMAAA002", "SMWMAAA006", "SMWMAAA009", "SMWMAAA010" };
            ListTable.dataSource = dos;
            ListTable.updateTable();

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
        SMWMAAA wi = (SMWMAAA)objects;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        //建立FlowStatusData物件
        FlowStatusData fd = new FlowStatusData();


        //取得資料物件代碼, 由原稿取得
        fd.ACTID = "";
        fd.ACTName = "";
        fd.FlowGUID = wi.SMWMAAA010;
        fd.HistoryGUID = "";
        fd.ObjectGUID = "";
        fd.PDID = wi.SMWMAAA002;
        fd.PDVer = "";
        //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
        //目前都給他為ProcessModify
        fd.UIStatus = FlowStatusData.FormReadOnly;
        fd.WorkItemOID = "";

        ListTable.FormTitle = wi.SMWMAAA003 + "(" + wi.SMWMAAA004 + ")";
        //engine.close();
        return fd;

    }
    protected void ListTable_RefreshButtonClick()
    {
        queryData();

    }
}
