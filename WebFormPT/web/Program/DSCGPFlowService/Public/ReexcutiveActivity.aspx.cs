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
using com.dsc.kernal.factory;
using com.dsc.kernal.databean;
using com.dsc.kernal.utility;
using com.dsc.flow.server;
using com.dsc.flow.data;
using WebServerProject;

public partial class Program_DSCGPFlowService_Public_ReexcutiveActivity : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                //底下這段是取得原表單畫面相關參數設定. 若要傳其它參數, 可仿照平台基本寫法, 
                //將資料存在以原本PageUniqueID命名的Session中. 系統會自動將原本畫面的PageUniqueID傳入
                string PGID = Request.QueryString["PGID"];
                string SMWDAAA001 = Request.QueryString["SMWDAAA001"];

                string workItemOID = (string)base.getSession(PGID, "WorkItemOID");

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                //取得可退回關卡列表
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

                BackActivity[] bary = adp.getReexecutableActivity(workItemOID);

                adp.logout();

                engine.close();

                setSession("BackActivityAry", bary);

                string[,] ids = new string[bary.Length, 2];
                for (int i = 0; i < ids.GetLength(0); i++)
                {
                    ids[i, 0] = bary[i].actID;
                    //國昌20100614-mantis 0016935: 退回重辦關卡選擇時顯示原簽核者姓名，避免無法識別同名關卡
                    ids[i, 1] = bary[i].actName + "-" + bary[i].processorName;
                }
                BackActID.setListItem(ids);

                if (ids.GetLength(0) == 0)
                {
                    Response.Write("<script language=javascript>");
                    //Response.Write("alert('此關卡為簽核第一關卡, 無法退回重辦');");
                    Response.Write("alert('" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_reexcutiveactivity_aspx.language", "message", "ResponMsg", "此關卡為簽核第一關卡, 無法退回重辦") + "');");
                    Response.Write("window.close();");
                    Response.Write("</script>");
                }

                OpenWin1.clientEngineType = (string)Session["engineType"];
                OpenWin1.connectDBString = (string)Session["connectString"];

            }
        }
    }
    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        string backData = "";
        if (BackType0.Checked)
        {
            backData += "0";
        }
        else
        {
            backData += "2";
        }
        Session["tempBackActID"] = BackActID.ValueText;
        Session["tempBackOpinion"] = SignOpinion.ValueText;
        Session["tempBackType"] = backData;

        com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
        switch (resultType)
        {
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE:
                Response.Write("window.top.close();");
                Response.Write("window.top.returnValue='YES';");
                break;
            default:
                string js = "";
                js += "window.parent.parent.$('#ecpDialog').attr('ret', 'YES');";
                js += "window.parent.parent.$('#ecpDialog').dialog('close');  ";
                Response.Write(js);
                break;
        }                    

    }
    protected void PhraseButton_Click(object sender, EventArgs e)
    {
        OpenWin1.PageUniqueID = this.PageUniqueID;
        OpenWin1.identityID = "0001";
        OpenWin1.paramString = "SMVLAAA002";
        OpenWin1.whereClause = "SMVLAAA002='" + (string)Session["UserID"] + "'";
        OpenWin1.openWin("SMVLAAA", "001", false, "0001");

    }
    protected void OpenWin1_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            SignOpinion.ValueText += values[0, 2];
        }

    }
}
