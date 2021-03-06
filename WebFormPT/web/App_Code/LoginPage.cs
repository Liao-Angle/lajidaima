using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using WebServerProject.system.login;
using com.dsc.kernal.logon;

/// <summary>
/// LoginPage 的摘要描述
/// </summary>
public class LoginPage : System.Web.UI.Page
{
    public LoginPage()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }
    public void initPage()
    {
        //2009.02.16 EricShenq use GlobalProperty
        //Step1 get InjectionList from external setting.ini
        //Step2 give list to GlobalCache called AVOIDINJECTIONLIST
        string list = GlobalProperty.getProperty("global", "InjectionList");
        GlobalCache.setValue("AVOIDINJECTIONLIST", list);

        Session["PortalLink"] = Request.QueryString.ToString();

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        com.dsc.kernal.factory.IOFactory factory = new com.dsc.kernal.factory.IOFactory();
        com.dsc.kernal.factory.AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select LOGOUTTIME from LOGINDATA where SESSIONID='" + Utility.filter(Session.SessionID) + "'";
        System.Data.DataSet ds = engine.getDataSet(sql, "TEMP");
        if ((ds.Tables[0].Rows.Count > 0) && (ds.Tables[0].Rows[0][0].ToString().Equals("")))
        {
            sql = "update LOGINDATA set LOGOUTTIME='" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null) + "' where SESSIONID='" + Utility.filter(Session.SessionID) + "'";
            engine.executeSQL(sql);
        }

		Session["crossSite"] = false;
        //SSO機制, 必須SSOToken, UserID, VerifySite都有值
        //if ((Request.QueryString["SSOToken"] != null) && (Request.QueryString["UserID"] != null) && (Request.QueryString["VerifySite"] != null))
        if (Convert.ToBoolean(Session["isAD"]) == false || Request.QueryString["SID"] !=null)
        {
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string ssoClass = sp.getParam("SSOClient");

            com.dsc.kernal.sso.client.SSOClientFactory fac = new com.dsc.kernal.sso.client.SSOClientFactory();
            com.dsc.kernal.sso.client.AbstractSSOClient sso = fac.getSSOClient(ssoClass.Split(new char[] { '.' })[0], ssoClass);

            bool isCheckSSO = true;
            //string tmpVerifySite = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["VerifySite"]);
            //string tmpSSOToken = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["SSOToken"]);
            string tmpUserID = "";
            string tmpldapID = "";
            string tmpSMVHdata = "";
            if (!String.IsNullOrEmpty(Request.ServerVariables["AUTH_USER"]))
            {
                tmpldapID = Convert.ToString(engine.executeScalar("select ldapid from Users where ldapid COLLATE Chinese_Taiwan_Stroke_CI_AS like N'" + Utility.filter(Convert.ToString(Request.ServerVariables["AUTH_USER"]).Split(new char[] { '\\' })[1].Trim()) + "@%' COLLATE Chinese_Taiwan_Stroke_CI_AS and (leaveDate is null or leaveDate='')"));
                if (tmpldapID.IndexOf('@') > -1)
                {
                    tmpSMVHdata = sp.getParam(tmpldapID.Split(new char[] { '@' })[1].Trim());
                    if (!string.IsNullOrEmpty(tmpldapID) && tmpldapID.IndexOf('@') > 0 && tmpldapID.Split(new char[] { '@' })[0].Trim().ToUpper() == Convert.ToString(Request.ServerVariables["AUTH_USER"]).Split(new char[] { '\\' })[1].Trim().ToUpper() && tmpSMVHdata.ToUpper().IndexOf(Convert.ToString(Request.ServerVariables["AUTH_USER"]).Split(new char[] { '\\' })[0].Trim().ToUpper()) > -1)
                    {
                        tmpUserID = Convert.ToString(Request.ServerVariables["AUTH_USER"]).Split(new char[] { '\\' })[1].Trim();
                    }
                    else
                    {
                        tmpUserID = "";
                    }
                }
                else
                {
                    tmpUserID = "";
                }
            }
            else
            {
                tmpUserID = Request.QueryString["UserID"];
            }


	

            //isCheckSSO = sso.verify(engine, "", "", tmpUserID, Request);

			string SID= "";
			string Token= "";

            if (Request.QueryString["SID"] != null)
            {
                SID = Request.QueryString["SID"].ToString();
                Token = Request.QueryString["Token"].ToString();
                tmpUserID = Request.QueryString["UserID"].ToString();
                Session["crossSite"] = true;
                isCheckSSO = checkToken(SID, Token);
            }
            //驗證通過
            if (isCheckSSO)
            {
                loginSSO(engine, tmpUserID);
            }
        }

        //語系
        DropDownList dp = (DropDownList)this.FindControl("LANGUAGE");
        dp.Items.Add(new ListItem("使用者設定", "DEFAULT"));

        sql = "select LANGUAGEID, LANGUAGENAME from SYSLANGUAGE order by ISDEFAULT desc";
        ds = engine.getDataSet(sql, "TEMP");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            dp.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][0].ToString()));
        }

        sql = "select SMVPAAA015, SMVPAAA017, SMVPAAA012, SMVPAAA034 from SMVPAAA";
        ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows[0][0].ToString().Equals("N"))
        {
            Panel pp = (Panel)this.FindControl("Panel1");
            pp.Visible = false;
            com.dsc.kernal.global.GlobalCache.setValue("UseLocale", false);
        }
        else
        {
            com.dsc.kernal.global.GlobalCache.setValue("UseLocale", true);
        }

        if (ds.Tables[0].Rows[0]["SMVPAAA034"].ToString().Equals("Y"))
        {
            Panel pp = (Panel)this.FindControl("Panel2");
            pp.Visible = true;
        }
        else
        {
            Panel pp = (Panel)this.FindControl("Panel2");
            pp.Visible = false;
        }
        //標題
        WebServerProject.SysParam sps = new WebServerProject.SysParam(engine);
        Literal lt = (Literal)this.FindControl("TITLE");
        lt.Text = sps.getParam("SystemName");

        //版型
        DropDownList ly = (DropDownList)this.FindControl("LAYOUT");

        System.Collections.ArrayList lys=GlobalProperty.GetPropertyNames("layout");
        int defaultindex = -1;
        string defaultValue = "";
        string defaultText = "";
        //先找出預設值
        for (int i = 0; i < lys.Count; i++)
        {
            string[] tags = (string[])lys[i];
            string[] ts = tags[1].Split(new char[] { ';' });
            if (ts[0].Equals("Y"))
            {
                defaultindex = i;
                defaultValue = tags[0];
                defaultText = ts[1];
                break;
            }
        }
        if (defaultindex != -1)
        {
            ly.Items.Add(new ListItem(defaultText, defaultValue));
        }
        for (int i = 0; i < lys.Count; i++)
        {
            if (i == defaultindex) continue;
            string[] tags = (string[])lys[i];
            string[] ts = tags[1].Split(new char[] { ';' });

            ly.Items.Add(new ListItem(ts[1], tags[0]));
        }
	
        engine.close();
    }
    public void loginSSO(AbstractEngine engine, string userid)
    {
        String LogonUser = "";
        if (!string.IsNullOrEmpty(userid))
        {
            if (Request.QueryString["SID"] == null)
            {
                LogonUser = Convert.ToString(engine.executeScalar("select id from Users where ldapid COLLATE Chinese_Taiwan_Stroke_CI_AS like N'" + userid + "@%' COLLATE Chinese_Taiwan_Stroke_CI_AS"));
            }
            else
            {
                LogonUser = userid;
            }
        }
        else
        {
            LogonUser = userid;
        }


        LogonFactory lfac = new LogonFactory();
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string loginStr = sp.getParam("SSOLogin");

        if (loginStr.Equals(""))
        {
            loginStr = "WebServerProject.system.login.SSOUserLogin";
        }
        string loginAsm = loginStr.Split(new char[] { '.' })[0].Trim();
        AbstractLogon log = lfac.getLogonObject(loginAsm, loginStr);
        LogonUser = "administrator";
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
            Session["layoutType"] = "Enterprise";

            //初始化com.dll
            GlobalCache.setValue("UserGUID", log.UserID);

            //紀錄Log
            string gpid = "";
            for (int i = 0; i < log.UserAuthGroup.Length; i++)
            {
                gpid += log.UserAuthGroup[i] + ";";
            }
            if (gpid.Length > 0)
            {
                gpid = gpid.Substring(0, gpid.Length - 1);
            }
            string sql = "insert into LOGINDATA(GUID, SESSIONID, USERID, USERNAME, LOGINIP, LOGINTIME, LOGOUTTIME, PROCESSID, GROUPID) values(";
            sql += "'" + IDProcessor.getID("") + "',";
            sql += "'" + Session.SessionID + "',";
            sql += "'" + log.UserCode + "',";
            sql += "'" + log.UserName + "',";
            sql += "'" + Request.ServerVariables["REMOTE_ADDR"] + "',";
            sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
            sql += "'',";
            sql += "'" + log.ProcessID + "',";
            sql += "'" + gpid + "')";
            engine.executeSQL(sql);

            //判定是否需新增UserSetting檔
            sql = "select * from USERSETTING where USERGUID='" + Utility.filter((string)Session["UserGUID"]) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["GUID"] = IDProcessor.getID("");
                dr["USERGUID"] = (string)Session["UserGUID"];
                dr["VRGUID"] = "";
                dr["RECEIVEMSG"] = "N";
                dr["RECEIVEMAIL"] = "Y";
                dr["LASTPWDCHANGE"] = "";
                ds.Tables[0].Rows.Add(dr);

                engine.updateDataSet(ds);
            }

            //根據系統參數導向
            if (checkPasswordExpires(engine))
            {
                engine.close();
                //判定是否要轉到ChangeUser
                bool hasChangeUser = false;
                for (int i = 0; i < log.UserAuthGroup.Length; i++)
                {
                    if (log.UserAuthGroup[i].Equals("SYSTEMCHANGEGROUP"))
                    {
                        hasChangeUser = true;
                        break;
                    }
                }
                if (!hasChangeUser)
                {
                    Response.Redirect(getLayoutFrame((string)Session["layoutType"]) + "/MainFrame.aspx?" + (string)Session["PortalLink"]);
                }
                else
                {
                    Response.Redirect("ChangeUser.aspx");
                }
            }
            else
            {
                engine.close();
                Response.Redirect("ChangePassword.aspx");
            }
        }
        else
        {
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('登入失敗！" + retlog + "')\",100);");
            Response.Write("</script>");
        }
    }
    public void login(string domain, string userid, string password, string languageType, string layoutType)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        String Domain = domain;
        String LogonUser = userid;
        String Password = password;

        LogonFactory lfac = new LogonFactory();
        //string loginStr = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "LogonClass");
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string loginStr = sp.getParam("LogonClass");
        if (loginStr.Equals(""))
        {
            loginStr = "WebServerProject.system.login.GPUserLogin";
        }
        string loginAsm = loginStr.Split(new char[] { '.' })[0].Trim();
        AbstractLogon log = lfac.getLogonObject(loginAsm, loginStr);

        string retlog = log.startLogin(engine, Domain, LogonUser, Password);

        if (retlog.Equals(AbstractLogon.NOSUCHUSER))
        {
            engine.close();
            Session["Locale"] = languageType;
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('" + com.dsc.locale.LocaleString.getSystemMessageString("web_general.language.ini", "global", "NOSUCHUSER", "登入錯誤！資料庫中沒有您的身分資料，請洽詢系統管理員。") + "')\",100);");
            Response.Write("</script>");
        }
        else if (retlog.Equals(AbstractLogon.USEREMPTY))
        {
            engine.close();
            Session["Locale"] = languageType;
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('" + com.dsc.locale.LocaleString.getSystemMessageString("web_general.language.ini", "global", "USEREMPTY", "登入失敗！請確認所有資訊皆已輸入!!") + "')\",100);");
            Response.Write("</script>");
        }
        else if (retlog.Equals(AbstractLogon.NOAUTH))
        {
            engine.close();
            Session["Locale"] = languageType;
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('" + com.dsc.locale.LocaleString.getSystemMessageString("web_general.language.ini", "global", "NOAUTH", "登入失敗！無登入使用權限!!") + "')\",100);");
            Response.Write("</script>");
        }
        else if(retlog.Equals(AbstractLogon.INVALIDPWD))
        {
            engine.close();
            Session["Locale"] = languageType;
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('" + com.dsc.locale.LocaleString.getSystemMessageString("web_general.language.ini", "global", "INVALIDPWD", "登入失敗！密碼錯誤!!") + "')\",100);");
            Response.Write("</script>");
        }
        else if (retlog.Equals(AbstractLogon.SUCCESS))
        {
            Session["UserGUID"] = log.UserID;
            Session["UserID"] = log.UserCode;
            Session["UserName"] = log.UserName;

            string sql = "select SMVPAAA015 from SMVPAAA";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows[0][0].ToString().Equals("N"))
            {
                Session["Locale"] = "zh_TW";
            }
            else
            {

                if (languageType.Equals("DEFAULT"))
                {
                    Session["Locale"] = log.LanguageType;
                }
                else
                {
                    Session["Locale"] = languageType;
                }
            }
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
            Session["UserPWD"] = Password; //密碼, 不應該這樣做的, 但是位了DotJ整合

            Session["layoutType"] = layoutType; //儲存版型

            //初始化com.dll
            GlobalCache.setValue("UserGUID", log.UserID);

            //紀錄Log
            string gpid = "";
            for (int i = 0; i < log.UserAuthGroup.Length; i++)
            {
                gpid += log.UserAuthGroup[i] + ";";
            }
            if (gpid.Length > 0)
            {
                gpid = gpid.Substring(0, gpid.Length - 1);
            }

            sql = "insert into LOGINDATA(GUID, SESSIONID, USERID, USERNAME, LOGINIP, LOGINTIME, LOGOUTTIME, PROCESSID, GROUPID) values(";
            sql += "'" + IDProcessor.getID("") + "',";
            sql += "'" + Session.SessionID + "',";
            sql += "'" + log.UserCode + "',";
            sql += "'" + log.UserName + "',";
            sql += "'" + Request.ServerVariables["REMOTE_ADDR"] + "',";
            sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
            sql += "'',";
            sql += "'" + log.ProcessID + "',";
            sql += "'" + gpid + "')";
            engine.executeSQL(sql);

            //判定是否需新增UserSetting檔
            sql = "select * from USERSETTING where USERGUID='" + Utility.filter((string)Session["UserGUID"]) + "'";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["GUID"] = IDProcessor.getID("");
                dr["USERGUID"] = (string)Session["UserGUID"];
                dr["VRGUID"] = "";
                dr["RECEIVEMSG"] = "N";
                dr["RECEIVEMAIL"] = "Y";
                dr["LASTPWDCHANGE"] = "";
                ds.Tables[0].Rows.Add(dr);

                engine.updateDataSet(ds);
            }




            string redirectUrl = "";


            //根據系統參數導向
            if (checkPasswordExpires(engine))
            {
                engine.close();
                //判定是否要轉到ChangeUser
                bool hasChangeUser = false;
                for (int i = 0; i < log.UserAuthGroup.Length; i++)
                {
                    if (log.UserAuthGroup[i].Equals("SYSTEMCHANGEGROUP"))
                    {
                        hasChangeUser = true;
                        break;
                    }
                }
                if (!hasChangeUser)
                {
                    Response.Redirect(getLayoutFrame((string)Session["layoutType"]) + "/MainFrame.aspx?" + (string)Session["PortalLink"]);
                }
                else
                {
                    Response.Redirect("ChangeUser.aspx");
                }
            }
            else
            {
                engine.close();
                Response.Redirect("ChangePassword.aspx");
            }
        }
        else
        {
            engine.close();
            Session["Locale"] = languageType;
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('" + com.dsc.locale.LocaleString.getSystemMessageString("web_general.language.ini", "global", "FAILED", "登入失敗！") +  retlog + "')\",100);");
            Response.Write("</script>");
        }
    }

    private bool checkPasswordExpires(AbstractEngine engine)
    {
        string sql = "select SMVPAAA018 from SMVPAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows[0][0].ToString().Equals("N"))
        {
            return true;
        }

        sql = "select LASTPWDCHANGE from USERSETTING where USERGUID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        ds = engine.getDataSet(sql, "TEMP");
        string lDate = ds.Tables[0].Rows[0][0].ToString();

        WebServerProject.maintain.SMVU.SMVUAgent agent = new WebServerProject.maintain.SMVU.SMVUAgent();
        agent.engine = engine;

        if (agent.checkValidateDate((string)Session["UserID"], lDate))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private string getLayoutFrame(string layoutType)
    {
        return  GlobalProperty.getProperty("layout", layoutType).Split(new char[] { ';' })[4];
         
    }
	private string ret="";
	private bool checkToken(string sid, string token)
	{
//return true;
				string tMethodname = "Check";//欲呼叫的WebService的方法名 
                object[] tArgs = new object[2];//參數列表 
                tArgs[0] = sid;
                tArgs[1] = token;				
                string tUrl = GlobalProperty.getProperty("global", "CrossSiteSessionCheckURL");
		string fromIP = Request.QueryString["From"].ToString();
		tUrl="http://"+fromIP+"/ECP/WebService/CheckSessionID.asmx";
				try
				{
					com.dsc.kernal.webservice.WSDLClient client = new com.dsc.kernal.webservice.WSDLClient(tUrl);
					client.dllPath = Server.MapPath("~/tempFolder");
					client.build(false);
					object tReturnValue = client.Sync(tMethodname, tArgs);
					ret = tReturnValue.ToString();
					if(tReturnValue.ToString() =="OK")
					{
						return true;
					}
					else
					{
						return false;	
					}
				
				}
				catch(Exception ue)
				{
				    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("~/tempFolder")+"loginDebug.txt", true);
					sw.WriteLine(ue.Message);
					sw.Close();
					return false;
				}							
	}
}
