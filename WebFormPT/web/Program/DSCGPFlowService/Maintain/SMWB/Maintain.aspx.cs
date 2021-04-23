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
using com.dsc.kernal.utility;
using com.dsc.flow.server;
using com.dsc.flow.data;
using WebServerProject.flow.SMWB;
using WebServerProject;

public partial class Program_DSCGPFlowService_Maintain_SMWB_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnPreRender(EventArgs e)
    {
        SyncButton.ConfirmText = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwb_maintain_aspx.language.ini", "message", "QueryError1", "確定要進行同步更新嗎?");
        base.OnPreRender(e);
    }
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick+=new ProjectBaseWebUI_ListMaintain.SendButtonClickEvent(Master_SendButtonClick);
        Master.DeleteButtonClick+=new ProjectBaseWebUI_ListMaintain.DeleteButtonClickEvent(Master_DeleteButtonClick);
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                start();
            }
        }
    }

    public void start()
    {
        Master.maintainIdentity = "SMWB";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMWAA";
        SMWBAAA obj = new SMWBAAA();
        Master.HiddenField = new string[] { "SMWBAAA001", "SMWBAAA002"};

        Master.DialogHeight = 550;
        
        Master.inputForm = "Detail.aspx";
        Master.InitData(obj);

        Master.getListTable().NoAdd = true;
        Master.getListTable().NoDelete = true;
    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMWBAgent agent = new SMWBAgent();
        agent.engine = engine;

        bool res = agent.query(whereClause);
        setSession("whereclause", whereClause);

        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        Master.basedos = agent.defaultData;
    }
    public void Master_DeleteButtonClick()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWBAgent agent = new SMWBAgent();
        agent.engine = engine;
        agent.defaultData = Master.basedos;

        bool result = agent.update();
        engine.close();
        if (!result)
        {
            throw new Exception(engine.errorString);
        }
    }

    protected void SyncButton_Click(object sender, EventArgs e)
    {
        AbstractEngine engine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            engine.startTransaction(IsolationLevel.ReadCommitted);

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

            adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname,(bool)Session["DebugPage"]);

            ProcessPackage[] pp = adp.fetchProcessPackage();

            SMWBAgent ag=new SMWBAgent();
            ag.engine=engine;
            ag.query("");
            DataObjectSet dos=ag.defaultData;

            for (int i = 0; i < pp.Length; i++)
            {
                bool hasF = false;
                for (int j = 0; j < dos.getAvailableDataObjectCount(); j++)
                {
                    SMWBAAA aa=(SMWBAAA) dos.getAvailableDataObject(j);
                    if (pp[i].name.Equals(aa.SMWBAAA004))
                    {
                        hasF = true;
                        //更新流程定義代號以及識別號
                        aa.SMWBAAA002 = pp[i].OID;
                        aa.SMWBAAA003 = pp[i].id;

                        //更新流程參數
                        DataObjectSet child=aa.getChild("SMWBAAB");
                        updateAB(pp[i].relevantDataArray, child, aa.SMWBAAA001);
                        break;
                    }
                }
                if (!hasF)
                {
                    SMWBAAA aa = (SMWBAAA)dos.create();
                    aa.SMWBAAA001 = IDProcessor.getID("");
                    aa.SMWBAAA002 = pp[i].OID;
                    aa.SMWBAAA003 = pp[i].id;
                    aa.SMWBAAA004 = pp[i].name;
                    aa.SMWBAAA005 = "Y";
                    aa.SMWBAAA006 = "Y";
                    aa.SMWBAAA007 = "N";
                    aa.INSERTUSER = (string)Session["UserGUID"];

                    DataObjectSet child = new DataObjectSet();
                    child.setAssemblyName("WebServerProject");
                    child.setChildClassString("WebServerProject.flow.SMWB.SMWBAAB");
                    child.setTableName("SMWBAAB");
                    aa.setChild("SMWBAAB", child);

                    updateAB(pp[i].relevantDataArray, child, aa.SMWBAAA001);

                    dos.add(aa);
                }
            }

            if (SyncMode.Checked)
            {
                for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
                {
                    SMWBAAA aa = (SMWBAAA)dos.getAvailableDataObject(i);
                    bool hasF = false;
                    for (int j = 0; j < pp.Length; j++)
                    {
                        if (pp[j].name.Equals(aa.SMWBAAA004))
                        {
                            hasF = true;
                            break;
                        }
                    }
                    if (!hasF)
                    {
                        aa.delete();
                    }
                }
            }
            ag.update();
            adp.logout();

            //更新SMWIAAB--待處理資料夾定義作業的單身, 根據相同流程定義名稱更新流程定義代號, 並刪除不存在的流程定義
            string sql = "delete from SMWIAAB where SMWIAAB004 not in (select SMWBAAA004 from SMWBAAA)";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }

            sql = "update SMWIAAB set SMWIAAB003=(select SMWBAAA003 from SMWBAAA where SMWBAAA004=SMWIAAB004)";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            engine.commit();
            engine.close();

            //MessageBox("同步完畢");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwb_maintain_aspx.language.ini", "message", "QueryError", "同步完畢"));
        }
        catch (Exception te)
        {
            try
            {
                engine.rollback();
                engine.close();
            }
            catch { };
            MessageBox(te.Message.Replace("\n", "\\n"));
        }

    }
    private void updateAB(ArrayList ary, DataObjectSet dos, string guid)
    {
        for (int i = 0; i < ary.Count; i++)
        {
            RelevantData rd = (RelevantData)ary[i];
            bool hasF = false;
            SMWBAAB tab = null;
            for (int j = 0; j < dos.getAvailableDataObjectCount(); j++)
            {
                SMWBAAB ab = (SMWBAAB)dos.getAvailableDataObject(j);
                if (ab.SMWBAAB003.Equals(rd.id))
                {
                    hasF = true;
                    tab = ab;
                    break;
                }
            }

            if (!hasF)
            {
                SMWBAAB ab = (SMWBAAB)dos.create();
                ab.SMWBAAB001 = IDProcessor.getID("");
                ab.SMWBAAB002 = guid;
                ab.SMWBAAB003 = rd.id;
                ab.SMWBAAB004 = rd.name;
                ab.SMWBAAB005 = rd.length;
                ab.SMWBAAB006 = rd.initialValue;
                if (rd.isFormType)
                {
                    ab.SMWBAAB007 = "Y";
                }
                else
                {
                    ab.SMWBAAB007 = "N";
                }
                ab.SMWBAAB008 = rd.formOID;
                ab.INSERTUSER = (string)Session["UserGUID"];
                if (!dos.add(ab))
                {
                    throw new Exception(dos.errorString);
                }
            }
            else
            {
                tab.SMWBAAB004 = rd.name;
                tab.SMWBAAB005 = rd.length;
                tab.SMWBAAB006 = rd.initialValue;
                if (rd.isFormType)
                {
                    tab.SMWBAAB007 = "Y";
                }
                else
                {
                    tab.SMWBAAB007 = "N";
                }
                tab.SMWBAAB008 = rd.formOID;
            }
        }

        //刪除不存在ary中的dos物件
        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            SMWBAAB ab = (SMWBAAB)dos.getAvailableDataObject(i);
            bool hasF = false;
            for (int j = 0; j < ary.Count; j++)
            {
                RelevantData rd = (RelevantData)ary[j];
                if (ab.SMWBAAB003.Equals(rd.id))
                {
                    hasF = true;
                    break;
                }
            }
            if (!hasF)
            {
                ab.delete();
            }
        }
    }
}
