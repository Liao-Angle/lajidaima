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
using WebServerProject.maintain.LoginLog;
using com.dsc.kernal.logon;
using com.dsc.kernal.global;

public partial class FrameEnterprise_ShowLogonUser : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                //這裡要把資料秀出來
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                SHIFTUSER.clientEngineType = engineType;
                SHIFTUSER.connectDBString = connectString;

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                LoginDataAgent agent = new LoginDataAgent();
                agent.engine = engine;
                bool res = agent.query("LOGOUTTIME=''");

                //處理是否切換
                string sql = "select SMVPAAA032, SMVPAAA033 from SMVPAAA";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                
                if (ds.Tables[0].Rows[0]["SMVPAAA032"].ToString().Equals("Y"))
                {
                    bool isTesting = false;
                    if (Session["isTesting"] != null)
                    {
                        isTesting = (bool)Session["isTesting"];
                    }

                    if (isTesting)
                    {
                        DSCTabControl1.TabPages[1].Hidden = false;
                        DSCTabControl1.SelectedTab = 1;
                    }
                    else
                    {
                        string uguid = (string)Session["UserGUID"];
                        if (uguid.Equals(ds.Tables[0].Rows[0]["SMVPAAA033"].ToString()))
                        {
                            DSCTabControl1.TabPages[1].Hidden = false;
                            DSCTabControl1.SelectedTab = 1;
                        }
                        else
                        {
                            DSCTabControl1.TabPages[1].Hidden = true;
                            DSCTabControl1.SelectedTab = 0;
                        }
                    }
                }
                else
                {
                    DSCTabControl1.TabPages[1].Hidden = true;
                    DSCTabControl1.SelectedTab = 0;
                }
                engine.close();

                DList.HiddenField = new string[] { "GUID", "SESSIONID", "PROCESSID","LOGOUTTIME" };
                DList.ReadOnly = true;
                DList.dataSource = agent.defaultData;
                DList.updateTable();

                string[,] ids = new string[,]{
                    {"0",com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string001", "一般訊息")},
                    {"1",com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string002", "重要訊息")}
                };
                MessageType.setListItem(ids);

                DSCLabel1.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string007", "發送訊息给勾選的使用者:");
                DSCLabel2.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string008", "標題: ");
                DSCLabel3.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string009", "訊息內容: ");
                DSCLabel4.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string010", "URL: ");
                SendMessage.Text = com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string011", "發送");

            }
        }
    }
    protected void SendMessage_Click(object sender, EventArgs e)
    {
        try
        {
            if (DList.getSelectedItem().Length == 0)
            {
                MessageBox(com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string003", "請勾選要發送訊息的對象"));
                return;
            }
            if (MessageTitle.ValueText.Trim().Equals(""))
            {
                MessageBox(com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string004", "請填寫要發送的訊息標題"));
                return;
            }

            if (MessageContent.ValueText.Trim().Equals(""))
            {
                MessageBox(com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string005", "請填寫要發送的訊息內容"));
                return;
            }

            string title = MessageTitle.ValueText.Trim();
            string msg = MessageContent.ValueText.Trim();

            WebServerProject.system.alert.SystemMessage sm = new WebServerProject.system.alert.SystemMessage();
            DataObject[] ddo = DList.getSelectedItem();
            for (int i = 0; i < ddo.Length; i++)
            {
                LoginData ld = (LoginData)ddo[i];
                sm.addAlertMessage(ld.UserID, MessageType.ValueText, DateTimeUtility.getSystemTime2(null), title, msg, MessageURL.ValueText.Trim());
            }

            DList.UnCheckAllData();
            MessageTitle.ValueText = "";
            MessageContent.ValueText = "";
            MessageURL.ValueText = "";
            MessageBox(com.dsc.locale.LocaleString.getMainFrameLocaleString("showLogonUser.aspx.language.ini", "global", "string006", "訊息已發送"));
        }
        catch (Exception te)
        {
            MessageBox(te.StackTrace);
        }
    }
    protected void SHIFTBUTTON_Click(object sender, EventArgs e)
    {
        if (SHIFTUSER.GuidValueText.Equals(""))
        {
            return;
        }
        Session["isTesting"] = true;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string olduserID = Convert.ToString(engine.executeScalar("select id from Users with(nolock) where id = '" + SHIFTUSER.ValueText + "'"));
        loginSSO(engine, olduserID);

        engine.close();
    }
    public void loginSSO(AbstractEngine engine, string userid)
    {

        String LogonUser = userid;

        LogonFactory lfac = new LogonFactory();
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string loginStr = sp.getParam("SSOLogin");
        if (loginStr.Equals(""))
        {
            loginStr = "WebServerProject.system.login.SSOUserLogin";
        }
        string loginAsm = loginStr.Split(new char[] { '.' })[0].Trim();
        AbstractLogon log = lfac.getLogonObject(loginAsm, loginStr);

        string retlog = log.startLogin(engine, "", LogonUser, "");

        if (retlog.Equals(AbstractLogon.NOSUCHUSER))
        {
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('登入錯誤！資料庫中沒有您的身分資料，請洽詢系統管理員。')\",100);");
            Response.Write("</script>");
        }
        else if (retlog.Equals(AbstractLogon.USEREMPTY))
        {
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('登入失敗！請確認所有資訊皆已輸入!!')\",100);");
            Response.Write("</script>");
        }
        else if (retlog.Equals(AbstractLogon.NOAUTH))
        {
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('登入失敗！無登入使用權限!!')\",100);");
            Response.Write("</script>");
        }
        else if (retlog.Equals(AbstractLogon.SUCCESS))
        {
            Session["UserGUID"] = log.UserID;
            Session["UserID"] = log.UserCode;
            Session["UserName"] = log.UserName;
            Session["Locale"] = log.LanguageType;

            if (log.UserCode.ToLower().Equals("administrator"))
            {
                Session["IsSysAdmin"] = true;
            }
            else
            {
                Session["IsSysAdmin"] = false;
            }

            Session["usergroup"] = log.UserAuthGroup;
            Session["processid"] = log.ProcessID;

            //初始化com.dll
            GlobalCache.setValue("UserGUID", log.UserID);

            Response.Write("top.window.isChangeUser=true;");
            Response.Write("top.window.location.href='MainFrame.aspx';");
        }
        else
        {
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('登入失敗！" + retlog + "')\",100);");
            Response.Write("</script>");
        }
    }
}
