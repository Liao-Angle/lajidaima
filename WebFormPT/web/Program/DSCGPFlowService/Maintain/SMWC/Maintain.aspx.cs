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
using WebServerProject.flow.SMWC;
using WebServerProject;

public partial class Program_DSCGPFlowService_Maintain_SMWC_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnPreRender(EventArgs e)
    {
        SyncButton.ConfirmText = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwc_maintain_aspx.language.ini", "message", "QueryError1", "確定要進行同步更新嗎?");
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
        Master.maintainIdentity = "SMWC";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMWAA";
        SMWCAAA obj = new SMWCAAA();
        Master.HiddenField = new string[] { "SMWCAAA001"};

        Master.DialogHeight = 250;
        
        Master.inputForm = "Detail.aspx";
        Master.InitData(obj);

    }
    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMWCAgent agent = new SMWCAgent();
        agent.engine = engine;

        bool res = agent.query(whereClause);
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
        engine.startTransaction(IsolationLevel.ReadCommitted);

        //修改:設定所使用的agent
        SMWCAgent agent = new SMWCAgent();
        agent.engine = engine;
        agent.defaultData = Master.basedos;

        for (int i = 0; i < Master.basedos.getDataObjectCount(); i++)
        {
            if (Master.basedos.getDataObject(i).isDelete())
            {
                SMWCAAA aa = (SMWCAAA)Master.basedos.getDataObject(i);
                string sql = "delete from SMWDAAD where SMWDAAD003='" + Utility.filter(aa.SMWCAAA002) + "'";
                if (!engine.executeSQL(sql))
                {
                    engine.rollback();
                    engine.close();
                    throw new Exception(engine.errorString);
                }
            }
        }
        bool result = agent.update();
        if (!result)
        {
            engine.rollback();
            engine.close();
            throw new Exception(engine.errorString);
        }

        engine.commit();
        engine.close();

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
            adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

            ProcessPackage[] pp = adp.fetchProcessPackage();

            ArrayList ary = new ArrayList();
            for (int i = 0; i < pp.Length; i++)
            {
                for (int x = 0; x < pp[i].activityArray.Count; x++)
                {
                    Activity ac = (Activity)pp[i].activityArray[x];

                    bool hasF = false;
                    for (int j = 0; j < ary.Count; j++)
                    {
                        string tag = (string)ary[j];
                        if (tag.Equals(ac.name))
                        {
                            hasF = true;
                            break;
                        }
                    }
                    if (!hasF)
                    {
                        ary.Add(ac.name);
                    }
                }
            }

            SMWCAgent ag = new SMWCAgent();
            ag.engine = engine;
            //ag.query("SMWCAAA003='F'");
            ag.query("");
            DataObjectSet dos = ag.defaultData;

            for (int i = 0; i < ary.Count; i++)
            {
                string acname = (string)ary[i];
                bool hasF = false;
                for (int j = 0; j < dos.getAvailableDataObjectCount(); j++)
                {
                    SMWCAAA aa = (SMWCAAA)dos.getAvailableDataObject(j);
                    if (acname.Equals(aa.SMWCAAA002))
                    {
                        hasF = true;
                        break;
                    }
                }
                if (!hasF)
                {
                    SMWCAAA aa = (SMWCAAA)dos.create();
                    aa.SMWCAAA001 = IDProcessor.getID("");
                    aa.SMWCAAA002 = acname;
                    aa.SMWCAAA003 = "F";
                    aa.INSERTUSER = (string)Session["UserGUID"];
                    if (!dos.add(aa))
                    {
                        MessageBox(dos.errorString);
                    }
                }
            }

            if (SyncMode.Checked)
            {
                for (int i = 0; i < dos.getDataObjectCount(); i++)
                {
                    SMWCAAA aa = (SMWCAAA)dos.getDataObject(i);
                    bool hasF = false;
                    for (int j = 0; j < ary.Count; j++)
                    {
                        string acname = (string)ary[j];
                        if (acname.Equals(aa.SMWCAAA002))
                        {
                            hasF = true;
                            break;
                        }
                    }
                    if (!hasF)
                    {
                        string sql = "delete from SMWDAAD where SMWDAAD003='" + Utility.filter(aa.SMWCAAA002) + "'";
                        if (!engine.executeSQL(sql))
                        {
                            throw new Exception(engine.errorString);
                        }
                        aa.delete();
                    }
                }
            }
            if (!ag.update())
            {
                MessageBox(engine.errorString);
            }

            adp.logout();

            engine.commit();
            engine.close();

            //MessageBox("同步完畢");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwc_maintain_aspx.language.ini", "message", "QueryError", "同步完畢"));
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
}
